using AutoMapper;
using Bibliotech_API.Data;
using Bibliotech_API.Features.Prateleiras.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Features.Prateleiras;

public class PrateleiraService : IPrateleiraService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PrateleiraService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Prateleira>> GetAllPrateleirasAsync()
    {
        return await _context.Prateleiras.ToListAsync();
    }

    public async Task<Prateleira> GetPrateleiraByIdAsync(int id)
    {
        var prateleira = await _context.Prateleiras.FirstOrDefaultAsync(p => p.Id == id);

        if (prateleira == null)
            throw new BadHttpRequestException($"Prateleira com ID {id} não existe na base de dados.",
                StatusCodes.Status404NotFound);

        return prateleira;
    }

    public async Task CreatePrateleiraAsync(CreatePrateleiraDto prateleiraDto)
    {
        var estante = await _context.Estantes.FindAsync(prateleiraDto.IdEstante);
        if (estante == null)
            throw new BadHttpRequestException($"Estante com ID {prateleiraDto.IdEstante} não existe.",
                StatusCodes.Status400BadRequest);

        var prateleira = _mapper.Map<Prateleira>(prateleiraDto);

        _context.Prateleiras.Add(prateleira);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePrateleiraAsync(int id, UpdatePrateleiraDto prateleiraDto)
    {
        var prateleira = await GetPrateleiraByIdAsync(id);

        var estante = await _context.Estantes.FindAsync(prateleiraDto.IdEstante);
        if (estante == null)
            throw new BadHttpRequestException($"Estante com ID {prateleiraDto.IdEstante} não existe.",
                StatusCodes.Status400BadRequest);
        prateleira.IdEstante = prateleiraDto.IdEstante;

        _mapper.Map(prateleiraDto, prateleira);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePrateleiraAsync(int id)
    {
        var prateleira = await GetPrateleiraByIdAsync(id);

        var hasAssociatedBooks = await _context.Livros.AnyAsync(l => l.IdPrateleira == id);
        if (hasAssociatedBooks)
            throw new BadHttpRequestException("Não é possível deletar uma prateleira que possui livros associados.",
                StatusCodes.Status400BadRequest);

        _context.Prateleiras.Remove(prateleira);
        await _context.SaveChangesAsync();
    }
}