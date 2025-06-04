using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotech_API.Models.Entities;

[Table("autor")]
public class AutorEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Nome { get; set; }
}