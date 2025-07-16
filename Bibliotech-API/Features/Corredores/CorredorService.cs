using AutoMapper;
using Bibliotech_API.Data;
using Bibliotech_API.Features.Corredores.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Features.Corredores;

public class CorredorService : ICorredorService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CorredorService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Corredor>> GetAllCorredoresAsync()
    {
        return await _context.Corredores.ToListAsync();
    }

    public async Task<Corredor> GetCorredorByIdAsync(int id)
    {
        var corredor = await _context.Corredores.FirstOrDefaultAsync(c => c.Id == id);
        if (corredor == null)
            throw new BadHttpRequestException($"Corredor com ID {id} não existe na base de dados.",
                StatusCodes.Status404NotFound);

        return corredor;
    }

    public async Task CreateCorredorAsync(CreateCorredorDto corredorDto)
    {
        var corredor = _mapper.Map<Corredor>(corredorDto);
        _context.Corredores.Add(corredor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCorredorAsync(int id, UpdateCorredorDto corredorDto)
    {
        var corredor = await GetCorredorByIdAsync(id);
        _mapper.Map(corredorDto, corredor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCorredorAsync(int id)
    {
        var corredor = await GetCorredorByIdAsync(id);

        var hasAssociatedEstantes = await _context.Estantes.AnyAsync(e => e.IdCorredor == id);
        if (hasAssociatedEstantes)
            throw new BadHttpRequestException("Não é possível deletar um corredor que possui estantes associadas.",
                StatusCodes.Status400BadRequest);

        _context.Corredores.Remove(corredor);
        await _context.SaveChangesAsync();
    }
}