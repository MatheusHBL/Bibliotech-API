using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Estantes.Dtos;

public class UpdateEstanteDto : CreateEstanteDto
{
    [Required(ErrorMessage = "O ID da estante é obrigatório.")]
    public required int Id { get; set; }
}