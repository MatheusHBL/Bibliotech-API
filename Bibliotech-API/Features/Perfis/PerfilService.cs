using AutoMapper;
using Bibliotech_API.Data;
using Bibliotech_API.Features.Perfis.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Features.Perfis;

public class PerfilService : IPerfilService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PerfilService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Perfil>> GetAllPerfisAsync()
    {
        return await _context.Perfis.ToListAsync();
    }

    public async Task<Perfil> GetPerfilByIdAsync(int id)
    {
        var perfil = await _context.Perfis.FirstOrDefaultAsync(p => p.Id == id);
        if (perfil == null)
            throw new BadHttpRequestException($"Perfil com ID {id} não existe na base de dados.",
                StatusCodes.Status404NotFound);

        return perfil;
    }

    public async Task CreatePerfilAsync(CreatePerfilDto perfilDto)
    {
        var nomeExists = await _context.Perfis.AnyAsync(p => p.Nome == perfilDto.Nome);
        if (nomeExists)
            throw new BadHttpRequestException($"Perfil com o nome '{perfilDto.Nome}' já existe.",
                StatusCodes.Status400BadRequest);

        var perfil = _mapper.Map<Perfil>(perfilDto);
        _context.Perfis.Add(perfil);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePerfilAsync(int id, UpdatePerfilDto perfilDto)
    {
        var perfil = await GetPerfilByIdAsync(id);
        if (!string.IsNullOrEmpty(perfilDto.Nome) && perfilDto.Nome != perfil.Nome)
        {
            var nomeExists = await _context.Perfis.AnyAsync(p => p.Nome == perfilDto.Nome && p.Id != id);
            if (nomeExists)
                throw new BadHttpRequestException($"Perfil com o nome '{perfilDto.Nome}' já existe para outro perfil.",
                    StatusCodes.Status400BadRequest);
        }

        _mapper.Map(perfilDto, perfil);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePerfilAsync(int id)
    {
        var perfil = await GetPerfilByIdAsync(id);

        var hasAssociatedUsers = await _context.Usuarios.AnyAsync(u => u.Perfis.Any(p => p.Id == id));
        if (hasAssociatedUsers)
            throw new BadHttpRequestException("Não é possível deletar um perfil que possui usuários associados.",
                StatusCodes.Status400BadRequest);

        _context.Perfis.Remove(perfil);
        await _context.SaveChangesAsync();
    }
}