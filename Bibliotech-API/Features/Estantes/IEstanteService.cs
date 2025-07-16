using Bibliotech_API.Features.Estantes.Dtos;

namespace Bibliotech_API.Features.Estantes;

public interface IEstanteService
{
    Task<List<Estante>> GetAllEstantesAsync();
    Task<Estante> GetEstanteByIdAsync(int id);
    Task CreateEstanteAsync(CreateEstanteDto estanteDto);
    Task UpdateEstanteAsync(int id, UpdateEstanteDto estanteDto);
    Task DeleteEstanteAsync(int id);
}