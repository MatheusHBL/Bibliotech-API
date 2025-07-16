using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Prateleiras.Dtos;

public class CreatePrateleiraDto
{
    [Required(ErrorMessage = "O número da prateleira é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O número da prateleira deve ser um valor positivo.")]
    public required int Numero { get; set; }

    [Required(ErrorMessage = "O ID da estante é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID da estante deve ser um número positivo.")]
    public required int IdEstante { get; set; }
}