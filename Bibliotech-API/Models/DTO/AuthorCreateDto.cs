using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Models.DTO;

public class AuthorCreateDto
{
    [Required(ErrorMessage = "O nome do autor é obrigatório")]
    [StringLength(150, ErrorMessage = "O nome não pode exceder 150 caracteres")]
    public string nome { get; set; } = string.Empty;
}