using Bibliotech_API.Models.Entities;
using Bibliotech_API.Models.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bibliotech_API.Data;

namespace Bibliotech_API.Services;

public class AutorService
{
    private readonly BibliotechContext _context;
    private readonly IMapper _mapper;

    public AutorService(BibliotechContext blibliotecaContext, IMapper mapper)
    {
        _mapper = mapper;
        _context = blibliotecaContext;
    }
    
    public List<AutorEntity> GetAll()
    {
        return _context.Autor.AsNoTracking().ToList();
    }

    public AutorEntity? GetById(int id)
    {
        return _context.Autor.FirstOrDefault(autor => autor.Id == id);
    }
    
    public void Add(AutorCreateDto autor)
    {
        var autorEntity = _mapper.Map<AutorEntity>(autor);
        _context.Autor.Add(autorEntity);
        _context.SaveChanges();
    }
    
    public void Update(AutorUpdateDto autor)
    {
        var autorEntity = _mapper.Map<AutorEntity>(autor);
        _context.Autor.Update(autorEntity);
        _context.SaveChanges();
    }
    
    public void Remove(int id)
    {
        var autor = GetById(id);
        if (autor == null) throw new HttpRequestException("Autor não encontrado!");

        _context.Autor.Remove(autor);
        _context.SaveChanges();
    }
}