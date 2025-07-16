using Bibliotech_API.Features.Prateleiras.Dtos;

namespace Bibliotech_API.Features.Prateleiras;

public interface IPrateleiraService
{
    Task<List<Prateleira>> GetAllPrateleirasAsync();
    Task<Prateleira> GetPrateleiraByIdAsync(int id);
    Task CreatePrateleiraAsync(CreatePrateleiraDto prateleiraDto);
    Task UpdatePrateleiraAsync(int id, UpdatePrateleiraDto prateleiraDto);
    Task DeletePrateleiraAsync(int id);
}