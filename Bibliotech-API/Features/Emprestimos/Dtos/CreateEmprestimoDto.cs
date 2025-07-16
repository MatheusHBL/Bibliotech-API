using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Emprestimos.Dtos;

public class CreateEmprestimoDto
{
    [Required(ErrorMessage = "O ID do exemplar é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID do exemplar deve ser um número positivo.")]
    public int IdExemplar { get; set; }

    [Required(ErrorMessage = "O ID do usuário leitor é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID do usuário leitor deve ser um número positivo.")]
    public int IdUsuarioLeitor { get; set; }

    [Required(ErrorMessage = "O ID do usuário responsável é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID do usuário responsável deve ser um número positivo.")]
    public int IdUsuarioResponsavel { get; set; }

    [Required(ErrorMessage = "A data de início do empréstimo é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "Formato de data de início inválido.")]
    public DateTime DataInicio { get; set; }

    [Required(ErrorMessage = "A data de fim do empréstimo é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "Formato de data de fim inválido.")]
    public DateTime DataFim { get; set; }

    public string? Observacao { get; set; }
}