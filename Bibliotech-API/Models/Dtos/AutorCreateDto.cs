using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Models.Dtos;

public class AutorCreateDto
{
    [Required(ErrorMessage = "O nome do autor é obrigatório")]
    [StringLength(150, ErrorMessage = "O nome não pode exceder 150 caracteres")]
    public string Nome { get; set; } = string.Empty;
}