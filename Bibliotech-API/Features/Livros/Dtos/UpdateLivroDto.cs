using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Livros.Dtos;

public class UpdateLivroDto : CreateLivroDto
{
    [Required(ErrorMessage = "O ID do livro é obrigatório.")]
    public required int Id { get; set; }
}