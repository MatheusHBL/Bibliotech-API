using Bibliotech_API.Features.Livros;

namespace Bibliotech_API.Features.Autores;

public class Autor
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public ICollection<Livro> Livros { get; set; } = new List<Livro>();
}