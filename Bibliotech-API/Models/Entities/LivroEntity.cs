using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotech_API.Models.Entities;

[Table("livro")]
public class LivroEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Titulo { get; set; }

    [Required]
    public required string Isbn { get; set; }

    [Required]
    public int Quantidade { get; set; } = 1;

    [Required]
    public DateTime DataPublicacao { get; set; }

    [Required]
    public int IdEditoraFk { get; set; }
}