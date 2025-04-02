using System.ComponentModel.DataAnnotations;

namespace api_biblioteca.Models.DTO;

public class AutorUpdateDto : AutorCreateDto
{
    [Required(ErrorMessage = "O id do autor é obrigatório")]
    public int id_autor { get; set; }
}