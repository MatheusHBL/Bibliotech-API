using System.Text.Json.Serialization;
using Bibliotech_API.Features.Livros;

namespace Bibliotech_API.Features.Categorias;

public class Categoria
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    [JsonIgnore]
    public ICollection<Livro> Livros { get; set; } = new List<Livro>();
}