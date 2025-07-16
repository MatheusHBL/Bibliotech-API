using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Perfis.Dtos;

public class CreatePerfilDto
{
    [Required(ErrorMessage = "O nome do perfil é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do perfil não pode exceder 100 caracteres.")]
    [MinLength(3, ErrorMessage = "O nome do perfil deve ter pelo menos 3 caracteres.")]
    public required string Nome { get; set; }

    [StringLength(255, ErrorMessage = "A descrição do perfil não pode exceder 255 caracteres.")]
    public string? Descricao { get; set; }
}