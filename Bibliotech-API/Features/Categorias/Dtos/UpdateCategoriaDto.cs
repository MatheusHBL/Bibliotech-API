using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Categorias.Dtos;

public class UpdateCategoriaDto : CreateCategoriaDto
{
    [Required(ErrorMessage = "O ID da categoria é obrigatório.")]
    public required int Id { get; set; }
}