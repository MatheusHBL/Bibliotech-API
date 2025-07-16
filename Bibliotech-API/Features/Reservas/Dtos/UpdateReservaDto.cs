using System.ComponentModel.DataAnnotations;
using Bibliotech_API.Common.Enums;

namespace Bibliotech_API.Features.Reservas.Dtos;

public class UpdateReservaDto : CreateReservaDto
{
    [Required(ErrorMessage = "O ID da reserva é obrigatório.")]
    public required int Id { get; set; }
    
    [EnumDataType(typeof(StatusReservaEnum), ErrorMessage = "Status de reserva inválido.")]
    public StatusReservaEnum? Status { get; set; }
}