using AutoMapper;
using Bibliotech_API.Data;
using Bibliotech_API.Features.Categorias.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Features.Categorias;

public class CategoriaService : ICategoriaService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CategoriaService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Categoria>> GetAllCategoriasAsync()
    {
        return await _context.Categorias.ToListAsync();
    }

    public async Task<Categoria> GetCategoriaByIdAsync(int id)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Livros)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria == null)
            throw new BadHttpRequestException($"Categoria com ID {id} não existe na base de dados.",
                StatusCodes.Status404NotFound);

        return categoria;
    }

    public async Task CreateCategoriaAsync(CreateCategoriaDto categoriaDto)
    {
        var categoria = _mapper.Map<Categoria>(categoriaDto);
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoriaAsync(int id, UpdateCategoriaDto categoriaDto)
    {
        var categoria = await GetCategoriaByIdAsync(id);
        _mapper.Map(categoriaDto, categoria);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoriaAsync(int id)
    {
        var categoria = await GetCategoriaByIdAsync(id);
        var hasAssociatedBooks = await _context.Livros.AnyAsync(l => l.Categorias.Any(c => c.Id == id));

        if (hasAssociatedBooks)
            throw new BadHttpRequestException("Não é possível deletar uma categoria que possui livros associados.",
                StatusCodes.Status400BadRequest);

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
    }
}