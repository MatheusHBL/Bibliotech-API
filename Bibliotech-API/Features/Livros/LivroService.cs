using AutoMapper;
using Bibliotech_API.Data;
using Bibliotech_API.Features.Livros.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Features.Livros;

public class LivroService : ILivroService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public LivroService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Livro>> GetAllLivrosAsync()
    {
        return await _context.Livros
            .Include(l => l.Prateleira)
            .Include(l => l.Autores)
            .Include(l => l.Categorias)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Livro> GetLivroByIdAsync(int id)
    {
        var livro = await _context.Livros
            .Include(l => l.Prateleira)
            .Include(l => l.Autores)
            .Include(l => l.Categorias)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (livro == null)
            throw new BadHttpRequestException($"Livro com ID {id} não existe na base de dados.",
                StatusCodes.Status404NotFound);

        return livro;
    }

    public async Task CreateLivroAsync(CreateLivroDto livroDto)
    {
        var prateleira = await _context.Prateleiras.FindAsync(livroDto.IdPrateleira);
        if (prateleira == null)
            throw new BadHttpRequestException($"Prateleira com ID {livroDto.IdPrateleira} não existe.",
                StatusCodes.Status400BadRequest);

        var autores = await _context.Autores.Where(a => livroDto.AutoresIds.Contains(a.Id)).ToListAsync();
        if (autores.Count != livroDto.AutoresIds.Count)
            throw new BadHttpRequestException("Um ou mais IDs de autores fornecidos são inválidos.",
                StatusCodes.Status400BadRequest);

        var categorias = await _context.Categorias.Where(c => livroDto.CategoriasIds.Contains(c.Id)).ToListAsync();
        if (categorias.Count != livroDto.CategoriasIds.Count)
            throw new BadHttpRequestException("Um ou mais IDs de categorias fornecidos são inválidos.",
                StatusCodes.Status400BadRequest);

        var livro = _mapper.Map<Livro>(livroDto);
        foreach (var autor in autores) livro.Autores.Add(autor);
        foreach (var categoria in categorias) livro.Categorias.Add(categoria);

        _context.Livros.Add(livro);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLivroAsync(int id, UpdateLivroDto livroDto)
    {
        var livro = await GetLivroByIdAsync(id);

        var prateleira = await _context.Prateleiras.FindAsync(livroDto.IdPrateleira);
        if (prateleira == null)
            throw new BadHttpRequestException($"Prateleira com ID {livroDto.IdPrateleira} não existe.",
                StatusCodes.Status400BadRequest);

        var autoresIds = livroDto.AutoresIds;
        var autoresNovos = await _context.Autores.Where(a => autoresIds.Contains(a.Id)).ToListAsync();
        if (autoresNovos.Count != livroDto.AutoresIds.Count)
            throw new BadHttpRequestException("Um ou mais IDs de autores fornecidos são inválidos.",
                StatusCodes.Status400BadRequest);

        var categoriasIds = livroDto.CategoriasIds;
        var categoriasNovas = await _context.Categorias.Where(c => categoriasIds.Contains(c.Id)).ToListAsync();
        if (categoriasNovas.Count != livroDto.CategoriasIds.Count)
            throw new BadHttpRequestException("Um ou mais IDs de categorias fornecidos são inválidos.",
                StatusCodes.Status400BadRequest);

        _mapper.Map(livroDto, livro);
        livro.IdPrateleira = livroDto.IdPrateleira;
        livro.Autores.Clear();
        livro.Categorias.Clear();
        foreach (var autor in autoresNovos) livro.Autores.Add(autor);
        foreach (var categoria in categoriasNovas) livro.Categorias.Add(categoria);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteLivroAsync(int id)
    {
        var livro = await GetLivroByIdAsync(id);

        var hasExemplar = await _context.Exemplares.AnyAsync(e => e.IdLivro == id);
        if (hasExemplar)
            throw new BadHttpRequestException("Não é possível deletar um livro que possui exemplares.",
                StatusCodes.Status400BadRequest);

        _context.Livros.Remove(livro);
        await _context.SaveChangesAsync();
    }
}