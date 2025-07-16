using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Estantes.Dtos;

public class CreateEstanteDto
{
    [Required(ErrorMessage = "O número da estante é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O número da estante deve ser um valor positivo.")]
    public required int Numero { get; set; }

    [Required(ErrorMessage = "O ID do corredor é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID do corredor deve ser um número positivo.")]
    public required int IdCorredor { get; set; }
}