using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Prateleiras.Dtos;

public class UpdatePrateleiraDto : CreatePrateleiraDto
{
    [Required(ErrorMessage = "O ID da prateleira é obrigatório.")]
    public required int Id { get; set; }
}