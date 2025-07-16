using Bibliotech_API.Features.Emprestimos;
using Bibliotech_API.Features.Perfis;
using Bibliotech_API.Features.Reservas;

namespace Bibliotech_API.Features.Usuarios;

public class Usuario
{
    public int Id { get; set; }
    public required string Cpf { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }
    public required string Telefone { get; set; }
    public string? Endereco { get; set; }
    public ICollection<Perfil> Perfis { get; set; } = new List<Perfil>();
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    public ICollection<Emprestimo> EmprestimosComoLeitor { get; set; } = new List<Emprestimo>();
    public ICollection<Emprestimo> EmprestimosComoResponsavel { get; set; } = new List<Emprestimo>();
}