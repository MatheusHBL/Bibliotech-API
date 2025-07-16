namespace Bibliotech_API.Features.Emprestimos.Dtos;

using System.ComponentModel.DataAnnotations;

public class DevolucaoEmprestimoDto
{
    public bool Danificado { get; set; } = false;

    [StringLength(1000, ErrorMessage = "A observação não pode exceder 1000 caracteres.")]
    public string? Observacao { get; set; }
}