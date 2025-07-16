using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Corredores.Dtos;

public class UpdateCorredorDto
{
    [Required(ErrorMessage = "O ID do corredor é obrigatório.")]
    public required int Id { get; set; }
}