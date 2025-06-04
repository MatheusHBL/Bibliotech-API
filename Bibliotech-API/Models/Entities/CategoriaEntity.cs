using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotech_API.Models.Entities;

[Table("categoria")]
public class CategoriaEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Descricao { get; set; }
}