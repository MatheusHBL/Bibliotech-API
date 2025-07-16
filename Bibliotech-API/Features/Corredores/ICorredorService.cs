using Bibliotech_API.Features.Corredores.Dtos;

namespace Bibliotech_API.Features.Corredores;

public interface ICorredorService
{
    Task<List<Corredor>> GetAllCorredoresAsync();
    Task<Corredor> GetCorredorByIdAsync(int id);
    Task CreateCorredorAsync(CreateCorredorDto corredorDto);
    Task UpdateCorredorAsync(int id, UpdateCorredorDto corredorDto);
    Task DeleteCorredorAsync(int id);
}