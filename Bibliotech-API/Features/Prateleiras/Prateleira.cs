using System.Text.Json.Serialization;
using Bibliotech_API.Features.Estantes;
using Bibliotech_API.Features.Livros;

namespace Bibliotech_API.Features.Prateleiras;

public class Prateleira
{
    public int Id { get; set; }
    public required int Numero { get; set; }
    public required int IdEstante { get; set; }
    public required Estante Estante { get; set; }
    [JsonIgnore]
    public ICollection<Livro> Livros { get; set; } = new List<Livro>();
}