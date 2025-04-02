using System.ComponentModel.DataAnnotations;

namespace api_biblioteca.Models.Entities;

public class AutorEntity
{
    [Key]
    public int id_autor { get; set; }

    [Required]
    public string nome { get; set; } = string.Empty;
}