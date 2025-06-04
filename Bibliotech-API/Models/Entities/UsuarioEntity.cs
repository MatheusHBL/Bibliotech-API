using Bibliotech_API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotech_API.Models.Entities;

[Table("usuario")]
public class UsuarioEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Nome { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Telefone { get; set; }

    [Required]
    public required string Endereco { get; set; }

    [Required]
    public required TipoUsuarioEnum Tipo { get; set; }
}