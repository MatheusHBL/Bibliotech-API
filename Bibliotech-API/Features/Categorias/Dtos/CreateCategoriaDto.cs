using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Categorias.Dtos;

public class CreateCategoriaDto
{
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [StringLength(255, ErrorMessage = "O nome da categoria não pode exceder 255 caracteres.")]
    [MinLength(3, ErrorMessage = "O nome da categoria deve ter pelo menos 3 caracteres.")]
    public required string Nome { get; set; }
}