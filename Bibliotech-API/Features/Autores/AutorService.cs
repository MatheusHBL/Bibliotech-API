using AutoMapper;
using Bibliotech_API.Data;
using Bibliotech_API.Features.Autores.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Features.Autores;

public class AutorService : IAutorService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AutorService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Autor>> GetAllAutoresAsync()
    {
        return await _context.Autores.ToListAsync();
    }

    public async Task<Autor> GetAutorByIdAsync(int id)
    {
        var autor = await _context.Autores.FindAsync(id);
        if (autor == null)
            throw new BadHttpRequestException($"Autor com ID {id} não existe na base de dados.",
                StatusCodes.Status404NotFound);

        return autor;
    }

    public async Task CreateAutorAsync(CreateAutorDto autorDto)
    {
        var alreadyExists = await _context.Autores.AnyAsync(a => a.Nome == autorDto.Nome);
        if (alreadyExists)
            throw new BadHttpRequestException($"Autor com nome {autorDto.Nome} já existe na base de dados.",
                StatusCodes.Status400BadRequest);

        var autor = _mapper.Map<Autor>(autorDto);
        _context.Autores.Add(autor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAutorAsync(int id, UpdateAutorDto autorDto)
    {
        var autor = await GetAutorByIdAsync(id);
        var alreadyExists = await _context.Autores.AnyAsync(a => a.Nome == autorDto.Nome && a.Id != autor.Id);
        if (alreadyExists)
            throw new BadHttpRequestException($"Autor com nome {autorDto.Nome} já existe na base de dados.",
                StatusCodes.Status400BadRequest);

        _mapper.Map(autorDto, autor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAutorAsync(int id)
    {
        var autor = await GetAutorByIdAsync(id);
        _context.Autores.Remove(autor);
        await _context.SaveChangesAsync();
    }
}