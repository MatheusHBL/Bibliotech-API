using Bibliotech_API.Common.Enums;
using Bibliotech_API.Features.Exemplares;
using Bibliotech_API.Features.Usuarios;

namespace Bibliotech_API.Features.Emprestimos;

public class Emprestimo
{
    public int Id { get; set; }
    public required int IdExemplar { get; set; }
    public required int IdUsuarioLeitor { get; set; }
    public required int IdUsuarioResponsavel { get; set; }
    public Exemplar Exemplar { get; set; } = null!;
    public Usuario UsuarioLeitor { get; set; } = null!;
    public Usuario UsuarioResponsavel { get; set; } = null!;
    public required StatusEmprestimoEnum Status { get; set; }
    public required DateTime DataInicio { get; set; }
    public required DateTime DataFim { get; set; }
    public DateTime? DataDevolucao { get; set; }
    public bool? Danificado { get; set; }
    public string? Observacao { get; set; }
}