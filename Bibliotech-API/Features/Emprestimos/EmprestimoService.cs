using AutoMapper;
using Bibliotech_API.Common.Enums;
using Bibliotech_API.Data;
using Bibliotech_API.Features.Emprestimos.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Features.Emprestimos;

public class EmprestimoService : IEmprestimoService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EmprestimoService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Emprestimo>> GetAllEmprestimosAsync()
    {
        return await _context.Emprestimos
            .Include(e => e.Exemplar)
            .ThenInclude(ex => ex.Livro)
            .Include(e => e.UsuarioLeitor)
            .Include(e => e.UsuarioResponsavel)
            .ToListAsync();
    }

    public async Task<Emprestimo> GetEmprestimoByIdAsync(int id)
    {
        var emprestimo = await _context.Emprestimos
            .Include(e => e.Exemplar)
            .ThenInclude(ex => ex.Livro)
            .Include(e => e.UsuarioLeitor)
            .Include(e => e.UsuarioResponsavel)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (emprestimo == null)
            throw new BadHttpRequestException($"Empréstimo com ID {id} não existe na base de dados.",
                StatusCodes.Status404NotFound);

        return emprestimo;
    }

    public async Task CreateEmprestimoAsync(CreateEmprestimoDto emprestimoDto)
    {
        var exemplar = await _context.Exemplares.FindAsync(emprestimoDto.IdExemplar);
        if (exemplar == null)
            throw new BadHttpRequestException($"Exemplar com ID {emprestimoDto.IdExemplar} não existe.",
                StatusCodes.Status400BadRequest);

        var usuarioLeitor = await _context.Usuarios.FindAsync(emprestimoDto.IdUsuarioLeitor);
        if (usuarioLeitor == null)
            throw new BadHttpRequestException($"Usuário leitor com ID {emprestimoDto.IdUsuarioLeitor} não existe.",
                StatusCodes.Status400BadRequest);

        var usuarioResponsavel = await _context.Usuarios.FindAsync(emprestimoDto.IdUsuarioResponsavel);
        if (usuarioResponsavel == null)
            throw new BadHttpRequestException(
                $"Usuário responsável com ID {emprestimoDto.IdUsuarioResponsavel} não existe.",
                StatusCodes.Status400BadRequest);

        if (exemplar.Situacao != SituacaoLivroEnum.Normal && exemplar.Situacao != SituacaoLivroEnum.Danificado)
        {
            throw new BadHttpRequestException($"Não é possível emprestar exemplar com situação '{exemplar.Situacao}'.",
                StatusCodes.Status400BadRequest);
        }

        var isExemplarEmprestado = await _context.Emprestimos.AnyAsync(e =>
            e.IdExemplar == emprestimoDto.IdExemplar &&
            (e.Status == StatusEmprestimoEnum.EmAndamento || e.Status == StatusEmprestimoEnum.Atrasado ||
             e.Status == StatusEmprestimoEnum.Extraviado));
        if (isExemplarEmprestado)
            throw new BadHttpRequestException("Este exemplar já está emprestado ou tem empréstimo em aberto.",
                StatusCodes.Status400BadRequest);

        var reservaPendente = await _context.Reservas.FirstOrDefaultAsync(r =>
            r.IdExemplar == emprestimoDto.IdExemplar &&
            DateTime.Now.Date >= r.DataInicio.Date && DateTime.Now.Date <= r.DataFim.Date &&
            (r.Status == StatusReservaEnum.AguardandoRetirada || r.Status == StatusReservaEnum.Pendente));

        if (reservaPendente != null && reservaPendente.IdUsuario != emprestimoDto.IdUsuarioLeitor)
            throw new BadHttpRequestException("Este exemplar ja está reservado.", StatusCodes.Status400BadRequest);

        var emprestimo = _mapper.Map<Emprestimo>(emprestimoDto);
        emprestimo.Status = StatusEmprestimoEnum.EmAndamento;

        _context.Emprestimos.Add(emprestimo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEmprestimoAsync(int id, UpdateEmprestimoDto emprestimoDto)
    {
        var emprestimo = await GetEmprestimoByIdAsync(id);

        if (emprestimo.Status == StatusEmprestimoEnum.Concluido ||
            emprestimo.Status == StatusEmprestimoEnum.Extraviado ||
            emprestimo.Status == StatusEmprestimoEnum.Cancelado)
            throw new BadHttpRequestException(
                $"Não é possível atualizar um empréstimo com status '{emprestimo.Status}'.",
                StatusCodes.Status400BadRequest);

        if (emprestimoDto.Status == StatusEmprestimoEnum.Extraviado)
        {
            var exemplar = await _context.Exemplares.FindAsync(emprestimo.IdExemplar);
            if (exemplar != null)
            {
                exemplar.Situacao = SituacaoLivroEnum.Extraviado;
                _context.Exemplares.Update(exemplar);
            }
        }

        _mapper.Map(emprestimoDto, emprestimo);

        await _context.SaveChangesAsync();
    }

    public async Task DevolverExemplarAsync(int emprestimoId, bool danificado, string? observacao)
    {
        var emprestimo = await GetEmprestimoByIdAsync(emprestimoId);
        if (emprestimo.Status == StatusEmprestimoEnum.Concluido ||
            emprestimo.Status == StatusEmprestimoEnum.Cancelado ||
            emprestimo.Status == StatusEmprestimoEnum.Extraviado)
            throw new BadHttpRequestException(
                $"Não é possível devolver um exemplar de um empréstimo com status '{emprestimo.Status}'.",
                StatusCodes.Status400BadRequest);

        emprestimo.Status = StatusEmprestimoEnum.Concluido;
        emprestimo.DataDevolucao = DateTime.Today;
        emprestimo.Danificado = danificado;
        emprestimo.Observacao = observacao;

        var exemplar = await _context.Exemplares.FindAsync(emprestimo.IdExemplar);
        if (exemplar != null)
        {
            if (danificado) exemplar.Situacao = SituacaoLivroEnum.Danificado;
            _context.Exemplares.Update(exemplar);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmprestimoAsync(int id)
    {
        var emprestimo = await GetEmprestimoByIdAsync(id);

        if (emprestimo.Status == StatusEmprestimoEnum.EmAndamento ||
            emprestimo.Status == StatusEmprestimoEnum.Atrasado ||
            emprestimo.Status == StatusEmprestimoEnum.Extraviado)
            throw new BadHttpRequestException($"Não é possível deletar um empréstimo com status '{emprestimo.Status}'.",
                StatusCodes.Status400BadRequest);

        _context.Emprestimos.Remove(emprestimo);
        await _context.SaveChangesAsync();
    }
}