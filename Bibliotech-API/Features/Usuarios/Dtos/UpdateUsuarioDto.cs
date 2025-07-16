using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Usuarios.Dtos;

public class UpdateUsuarioDto : CreateUsuarioDto
{
    [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
    public required int Id { get; set; }
}