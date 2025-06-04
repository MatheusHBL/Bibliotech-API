using Bibliotech_API.Data;
using Bibliotech_API.Models.Entities;

namespace Bibliotech_API.Repositories;

public class AuthorRepository
{
    private readonly BibliotechContext _context;

    public AuthorRepository(BibliotechContext blibliotecaContext)
    {
        _context = blibliotecaContext;
    }

    public List<AuthorEntity> GetAll()
    {
        return _context.Autor.ToList();
    }

    public AuthorEntity? GetById(int id)
    {
        return _context.Autor.FirstOrDefault(autor => autor.id_autor == id);
    }
    
    public void Add(AuthorEntity author)
    {
        _context.Autor.Add(author);
        _context.SaveChanges();
    }
    
    public void Update(AuthorEntity author)
    {
        _context.Autor.Update(author);
        _context.SaveChanges();
    }
    
    public void Remove(AuthorEntity author)
    {
        _context.Autor.Remove(author);
        _context.SaveChanges();
    }
}