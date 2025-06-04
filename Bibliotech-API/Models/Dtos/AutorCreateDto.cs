using System.ComponentModel.DataAnnotations;

<<<<<<<< HEAD:Bibliotech-API/Models/Dtos/AutorCreateDto.cs
namespace Bibliotech_API.Model.Dtos;
========
namespace Bibliotech_API.Models.DTO;
>>>>>>>> 74c75e4c62137cdababdb3fd561d0af9abd28e51:Bibliotech-API/Models/Dtos/AuthorCreateDto.cs

public class AuthorCreateDto
{
    [Required(ErrorMessage = "O nome do autor é obrigatório")]
    [StringLength(150, ErrorMessage = "O nome não pode exceder 150 caracteres")]
    public string nome { get; set; } = string.Empty;
}