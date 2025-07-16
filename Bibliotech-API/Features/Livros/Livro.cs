using Bibliotech_API.Features.Autores;
using Bibliotech_API.Features.Categorias;
using Bibliotech_API.Features.Prateleiras;
using Bibliotech_API.Features.Exemplares;

namespace Bibliotech_API.Features.Livros;

public class Livro
{
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public int IdPrateleira { get; set; }
    public required Prateleira Prateleira { get; set; }
    public ICollection<Autor> Autores { get; set; } = new List<Autor>();
    public ICollection<Categoria> Categorias { get; set; }  = new List<Categoria>();
    public ICollection<Exemplar> Exemplares { get; set; } = new List<Exemplar>();
}