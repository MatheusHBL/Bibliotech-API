using System.ComponentModel.DataAnnotations;

namespace api_biblioteca.Models.DTO;

public class AutorCreateDto
{
    [Required(ErrorMessage = "O nome do autor é obrigatório")]
    [StringLength(150, ErrorMessage = "O nome não pode exceder 150 caracteres")]
    public string nome { get; set; } = string.Empty;
}