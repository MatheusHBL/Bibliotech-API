using System.ComponentModel.DataAnnotations;
using Bibliotech_API.Common.Enums;

namespace Bibliotech_API.Features.Livros.Dtos;

public class CreateLivroDto
{
    [Required(ErrorMessage = "O título do livro é obrigatório.")]
    [StringLength(255, ErrorMessage = "O título do livro não pode exceder 255 caracteres.")]
    [MinLength(3, ErrorMessage = "O título do livro deve ter pelo menos 3 caracteres.")]
    public required string Titulo { get; set; }
    
    [EnumDataType(typeof(SituacaoLivroEnum), ErrorMessage = "Situação inválida.")]
    public SituacaoLivroEnum Situacao { get; set; } = SituacaoLivroEnum.Normal;

    [Required(ErrorMessage = "O ID da prateleira é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID da prateleira deve ser um número positivo.")]
    public required int IdPrateleira { get; set; }

    [Required(ErrorMessage = "É necessário informar pelo menos um autor para o livro.")]
    [MinLength(1, ErrorMessage = "O livro deve ter pelo menos um autor.")]
    public ICollection<int> AutoresIds { get; set; } = new List<int>();

    [Required(ErrorMessage = "É necessário informar pelo menos uma categoria para o livro.")]
    [MinLength(1, ErrorMessage = "O livro deve ter pelo menos uma categoria.")]
    public ICollection<int> CategoriasIds { get; set; } = new List<int>();
}