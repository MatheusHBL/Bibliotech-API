using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Models.DTO;

public class AuthorUpdateDto : AuthorCreateDto
{
    [Required(ErrorMessage = "O id do autor é obrigatório")]
    public int id_autor { get; set; }
}