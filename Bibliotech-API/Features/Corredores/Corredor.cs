using Bibliotech_API.Features.Estantes;

namespace Bibliotech_API.Features.Corredores;

public class Corredor
{
    public int Id { get; set; }
    public required int Numero { get; set; }
    public ICollection<Estante> Estantes { get; set; } = new List<Estante>();
}