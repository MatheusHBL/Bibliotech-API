using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Autores.Dtos;

public class CreateAutorDto
{
    [Required(ErrorMessage = "O nome do autor é obrigatório.")]
    [StringLength(255, ErrorMessage = "O nome do autor não pode exceder 255 caracteres.")]
    [MinLength(3, ErrorMessage = "O nome do autor deve ter pelo menos 3 caracteres.")]
    public required string Nome { get; set; }
}