using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Model.Dtos;

public class AutorUpdateDto : AutorCreateDto
{
    [Required(ErrorMessage = "O id do autor é obrigatório")]
    public int id_autor { get; set; }
}