using Bibliotech_API.Features.Reservas.Dtos;

namespace Bibliotech_API.Features.Reservas;

public interface IReservaService
{
    Task<List<Reserva>> GetAllReservasAsync();
    Task<Reserva> GetReservaByIdAsync(int id);
    Task CreateReservaAsync(CreateReservaDto reservaDto);
    Task UpdateReservaAsync(int id, UpdateReservaDto reservaDto);
    Task DeleteReservaAsync(int id);
}