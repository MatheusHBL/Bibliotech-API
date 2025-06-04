using System.ComponentModel.DataAnnotations;

<<<<<<<< HEAD:Bibliotech-API/Models/Dtos/AutorUpdateDto.cs
namespace Bibliotech_API.Model.Dtos;
========
namespace Bibliotech_API.Models.DTO;
>>>>>>>> 74c75e4c62137cdababdb3fd561d0af9abd28e51:Bibliotech-API/Models/Dtos/AuthorUpdateDto.cs

public class AuthorUpdateDto : AuthorCreateDto
{
    [Required(ErrorMessage = "O id do autor é obrigatório")]
    public int id_autor { get; set; }
}