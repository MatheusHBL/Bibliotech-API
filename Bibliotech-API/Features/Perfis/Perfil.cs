using Bibliotech_API.Features.Usuarios;

namespace Bibliotech_API.Features.Perfis;

public class Perfil
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}