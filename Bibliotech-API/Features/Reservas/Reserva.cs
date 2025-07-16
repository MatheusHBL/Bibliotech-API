using Bibliotech_API.Common.Enums;
using Bibliotech_API.Features.Exemplares;
using Bibliotech_API.Features.Usuarios;

namespace Bibliotech_API.Features.Reservas;

public class Reserva
{
    public int Id { get; set; }
    public required StatusReservaEnum Status { get; set; }
    public required DateTime DataInicio { get; set; }
    public required DateTime DataFim { get; set; }
    public required int IdExemplar { get; set; }
    public required int IdUsuario { get; set; }
    public required Exemplar Exemplar { get; set; }
    public required Usuario Usuario { get; set; }
    public string? Observacoes { get; set; }
}