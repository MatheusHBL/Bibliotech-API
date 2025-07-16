using System.ComponentModel.DataAnnotations;
using Bibliotech_API.Common.Enums;

namespace Bibliotech_API.Features.Exemplares.Dtos;

public class CreateExemplarDto
{
    [Required(ErrorMessage = "O ID do livro (título) é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID do livro deve ser um número positivo.")]
    public required int IdLivro { get; set; }

    [EnumDataType(typeof(SituacaoLivroEnum), ErrorMessage = "Situação inválida.")]
    public SituacaoLivroEnum Situacao { get; set; } = SituacaoLivroEnum.Normal;
}