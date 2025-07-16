using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Usuarios.Dtos;

public class CreateUsuarioDto
{
    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 caracteres.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter apenas números.")]
    public required string Cpf { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de email inválido.")]
    [StringLength(100, ErrorMessage = "O email não pode exceder 100 caracteres.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(8, MinimumLength = 4, ErrorMessage = "A senha deve ter entre 4 e 8 caracteres.")]
    public required string Senha { get; set; }

    public string? Endereco { get; set; }

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [StringLength(50, ErrorMessage = "O telefone não pode exceder 50 caracteres.")]
    [Phone(ErrorMessage = "Formato de telefone inválido.")]
    public required string Telefone { get; set; }

    public ICollection<int> PerfisIds { get; set; } = new List<int>();
}