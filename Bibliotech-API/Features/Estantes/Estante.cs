using Bibliotech_API.Features.Corredores;
using Bibliotech_API.Features.Prateleiras;

namespace Bibliotech_API.Features.Estantes;

public class Estante
{
    public int Id { get; set; }
    public required int Numero { get; set; }
    public required int IdCorredor { get; set; }
    public required Corredor Corredor { get; set; }
    public ICollection<Prateleira> Prateleiras { get; set; } = new List<Prateleira>();
}