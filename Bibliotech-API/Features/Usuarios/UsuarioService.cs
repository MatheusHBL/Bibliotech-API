using AutoMapper;
using Bibliotech_API.Common.Enums;
using Bibliotech_API.Data;
using Bibliotech_API.Features.Usuarios.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Features.Usuarios;

public class UsuarioService : IUsuarioService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UsuarioService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
    {
        return await _context.Usuarios
            .Include(u => u.Perfis)
            .ToListAsync();
    }

    public async Task<Usuario> GetUsuarioByIdAsync(int id)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Perfis)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario == null)
            throw new BadHttpRequestException($"Usuário com ID {id} não existe na base de dados.",
                StatusCodes.Status404NotFound);

        return usuario;
    }

    public async Task CreateUsuarioAsync(CreateUsuarioDto usuarioDto)
    {
        var cpfExists = await _context.Usuarios.AnyAsync(u => u.Cpf == usuarioDto.Cpf);
        if (cpfExists) throw new BadHttpRequestException("CPF já cadastrado.", StatusCodes.Status400BadRequest);

        var emailExists = await _context.Usuarios.AnyAsync(u => u.Email == usuarioDto.Email);
        if (emailExists) throw new BadHttpRequestException("Email já cadastrado.", StatusCodes.Status400BadRequest);

        var usuario = _mapper.Map<Usuario>(usuarioDto);
        usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha);

        var perfis = await _context.Perfis.Where(p => usuarioDto.PerfisIds.Contains(p.Id)).ToListAsync();
        if (perfis.Count != usuarioDto.PerfisIds.Count)
            throw new BadHttpRequestException("Um ou mais IDs de perfis fornecidos são inválidos.",
                StatusCodes.Status400BadRequest);

        foreach (var perfil in perfis) usuario.Perfis.Add(perfil);

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUsuarioAsync(int id, UpdateUsuarioDto usuarioDto)
    {
        var usuario = await GetUsuarioByIdAsync(id);
        if (!string.IsNullOrEmpty(usuarioDto.Cpf) && usuarioDto.Cpf != usuario.Cpf)
        {
            var cpfExists = await _context.Usuarios.AnyAsync(u => u.Cpf == usuarioDto.Cpf && u.Id != id);
            if (cpfExists)
                throw new BadHttpRequestException("Novo CPF já cadastrado para outro usuário.",
                    StatusCodes.Status400BadRequest);
        }

        if (!string.IsNullOrEmpty(usuarioDto.Email) && usuarioDto.Email != usuario.Email)
        {
            var emailExists = await _context.Usuarios.AnyAsync(u => u.Email == usuarioDto.Email && u.Id != id);
            if (emailExists)
                throw new BadHttpRequestException("Novo Email já cadastrado para outro usuário.",
                    StatusCodes.Status400BadRequest);
        }

        if (!string.IsNullOrEmpty(usuarioDto.Senha))
            usuarioDto.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha);

        _mapper.Map(usuarioDto, usuario);


        var perfisNovos = await _context.Perfis.Where(p => usuarioDto.PerfisIds.Contains(p.Id)).ToListAsync();
        if (perfisNovos.Count != usuarioDto.PerfisIds.Count)
            throw new BadHttpRequestException("Um ou mais IDs de perfis fornecidos são inválidos.",
                StatusCodes.Status400BadRequest);

        usuario.Perfis.Clear();
        foreach (var perfil in perfisNovos)
        {
            usuario.Perfis.Add(perfil);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteUsuarioAsync(int id)
    {
        var usuario = await GetUsuarioByIdAsync(id);

        var hasActiveEmprestimosLeitor = await _context.Emprestimos.AnyAsync(e =>
            e.IdUsuarioLeitor == id &&
            (e.Status == StatusEmprestimoEnum.EmAndamento || e.Status == StatusEmprestimoEnum.Atrasado ||
             e.Status == StatusEmprestimoEnum.Extraviado));
        if (hasActiveEmprestimosLeitor)
            throw new BadHttpRequestException("Não é possível deletar um usuário com empréstimos ativos como leitor.",
                StatusCodes.Status400BadRequest);

        var hasActiveEmprestimosResponsavel = await _context.Emprestimos.AnyAsync(e =>
            e.IdUsuarioResponsavel == id &&
            (e.Status == StatusEmprestimoEnum.EmAndamento || e.Status == StatusEmprestimoEnum.Atrasado ||
             e.Status == StatusEmprestimoEnum.Extraviado));
        if (hasActiveEmprestimosResponsavel)
            throw new BadHttpRequestException(
                "Não é possível deletar um usuário com empréstimos ativos como responsável.",
                StatusCodes.Status400BadRequest);

        var hasActiveReservas = await _context.Reservas.AnyAsync(r =>
            r.IdUsuario == id &&
            (r.Status == StatusReservaEnum.Pendente || r.Status == StatusReservaEnum.AguardandoRetirada));
        if (hasActiveReservas)
            throw new BadHttpRequestException("Não é possível deletar um usuário com reservas ativas.",
                StatusCodes.Status400BadRequest);

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }
}