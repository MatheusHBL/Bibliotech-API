using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Models.Dtos;

public class AutorUpdateDto : AutorCreateDto
{
    [Required(ErrorMessage = "O id do autor é obrigatório")]
    public int Id_autor { get; set; }
}