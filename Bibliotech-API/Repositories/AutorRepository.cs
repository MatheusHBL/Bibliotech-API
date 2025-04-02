using api_biblioteca.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_biblioteca.Repositories;

public class AutorRepository
{
    private readonly BibliotecaContext _context;

    public AutorRepository(BibliotecaContext blibliotecaContext)
    {
        _context = blibliotecaContext;
    }

    public List<AutorEntity> GetAll()
    {
        return _context.Autor.ToList();
    }

    public AutorEntity? GetById(int id)
    {
        return _context.Autor.FirstOrDefault(autor => autor.id_autor == id);
    }
    
    public void Add(AutorEntity autor)
    {
        _context.Autor.Add(autor);
        _context.SaveChanges();
    }
    
    public void Update(AutorEntity autor)
    {
        _context.Autor.Update(autor);
        _context.SaveChanges();
    }
    
    public void Remove(AutorEntity autor)
    {
        _context.Autor.Remove(autor);
        _context.SaveChanges();
    }
}