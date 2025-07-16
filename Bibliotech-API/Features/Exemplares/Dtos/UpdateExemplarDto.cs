using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Exemplares.Dtos;

public class UpdateExemplarDto : CreateExemplarDto
{
    [Required(ErrorMessage = "O ID do exemplar é obrigatório.")]
    public required int Id { get; set; }
}