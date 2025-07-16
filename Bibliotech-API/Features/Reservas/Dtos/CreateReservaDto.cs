using System.ComponentModel.DataAnnotations;

namespace Bibliotech_API.Features.Reservas.Dtos;

public class CreateReservaDto
{
    [Required(ErrorMessage = "O ID do Exemplar é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID do Exemplar deve ser um número positivo.")]
    public int IdExemplar { get; set; }

    [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID do usuário deve ser um número positivo.")]
    public int IdUsuario { get; set; }

    [Required(ErrorMessage = "A data de início da reserva é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "Formato de data de início inválido.")]
    public DateTime DataInicio { get; set; }

    [Required(ErrorMessage = "A data de fim da reserva é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "Formato de data de fim inválido.")]
    public DateTime DataFim { get; set; }
    
    public string? Observacoes { get; set; }
}