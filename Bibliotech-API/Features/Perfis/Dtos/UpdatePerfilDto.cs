using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Perfis.Dtos;

public class UpdatePerfilDto : CreatePerfilDto
{
    [Required(ErrorMessage = "O ID da prateleira é obrigatório.")]
    public required int Id { get; set; }
}