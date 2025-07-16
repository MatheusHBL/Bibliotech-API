using Bibliotech_API.Common.Enums;
using Bibliotech_API.Features.Emprestimos;
using Bibliotech_API.Features.Livros;
using Bibliotech_API.Features.Reservas;

namespace Bibliotech_API.Features.Exemplares;

public class Exemplar
{
    public int Id { get; set; }
    public required int IdLivro { get; set; }
    public Livro Livro { get; set; } = null!;
    public required SituacaoLivroEnum Situacao { get; set; }
    public ICollection<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}