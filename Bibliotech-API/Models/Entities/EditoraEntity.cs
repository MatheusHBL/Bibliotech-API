using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotech_API.Models.Entities;

[Table("editora")]
public class EditoraEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Nome { get; set; }
}