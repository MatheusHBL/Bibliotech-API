using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Corredores.Dtos;

public class CreateCorredorDto
{
    [Required(ErrorMessage = "O número do corredor é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O número do corredor deve ser um valor positivo.")]
    public required int Numero { get; set; }
}