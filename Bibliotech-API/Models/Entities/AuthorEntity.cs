using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Models.Entities;

public class AuthorEntity
{
    [Key]
    public int id_autor { get; set; }

    [Required]
    public string nome { get; set; } = string.Empty;
}