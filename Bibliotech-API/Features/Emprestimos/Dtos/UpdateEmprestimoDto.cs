using Bibliotech_API.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Emprestimos.Dtos;

public class UpdateEmprestimoDto : CreateEmprestimoDto
{
    [Required(ErrorMessage = "O ID do empréstimo é obrigatório.")]
    public required int Id { get; set; }

    [Required(ErrorMessage = "O status do empréstimo é obrigatório.")]
    [EnumDataType(typeof(StatusEmprestimoEnum), ErrorMessage = "Status de empréstimo inválido.")]
    public required StatusEmprestimoEnum Status { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Formato de data de devolução inválido.")]
    public DateTime? DataDevolucao { get; set; }

    public bool? Danificado { get; set; }
}