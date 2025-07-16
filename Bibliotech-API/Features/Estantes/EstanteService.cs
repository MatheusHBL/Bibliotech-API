using AutoMapper;
using Bibliotech_API.Data;
using Bibliotech_API.Features.Estantes.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Features.Estantes;

public class EstanteService : IEstanteService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public EstanteService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Estante>> GetAllEstantesAsync()
    {
        return await _context.Estantes.ToListAsync();
    }

    public async Task<Estante> GetEstanteByIdAsync(int id)
    {
        var estante = await _context.Estantes.FirstOrDefaultAsync(e => e.Id == id);
        if (estante == null)
            throw new BadHttpRequestException($"Estante com ID {id} não existe na base de dados.",
                StatusCodes.Status404NotFound);

        return estante;
    }

    public async Task CreateEstanteAsync(CreateEstanteDto estanteDto)
    {
        var corredor = await _context.Corredores.FindAsync(estanteDto.IdCorredor);
        if (corredor == null)
            throw new BadHttpRequestException($"Corredor com ID {estanteDto.IdCorredor} não existe.",
                StatusCodes.Status400BadRequest);

        var estante = _mapper.Map<Estante>(estanteDto);
        _context.Estantes.Add(estante);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEstanteAsync(int id, UpdateEstanteDto estanteDto)
    {
        var estante = await GetEstanteByIdAsync(id);

        var corredor = await _context.Corredores.FindAsync(estanteDto.IdCorredor);
        if (corredor == null)
            throw new BadHttpRequestException($"Corredor com ID {estanteDto.IdCorredor} não existe.",
                StatusCodes.Status400BadRequest);
        estante.IdCorredor = estanteDto.IdCorredor;

        _mapper.Map(estanteDto, estante);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEstanteAsync(int id)
    {
        var estante = await GetEstanteByIdAsync(id);

        var hasAssociatedPrateleiras = await _context.Prateleiras.AnyAsync(p => p.IdEstante == id);
        if (hasAssociatedPrateleiras)
            throw new BadHttpRequestException("Não é possível deletar uma estante que possui prateleiras associadas.",
                StatusCodes.Status400BadRequest);

        _context.Estantes.Remove(estante);
        await _context.SaveChangesAsync();
    }
}