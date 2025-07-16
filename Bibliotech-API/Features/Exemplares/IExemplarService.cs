using Bibliotech_API.Features.Exemplares.Dtos;

namespace Bibliotech_API.Features.Exemplares;

public interface IExemplarService
{
    Task<IEnumerable<Exemplar>> GetAllExemplaresAsync();
    Task<Exemplar> GetExemplarByIdAsync(int id);
    Task CreateExemplarAsync(CreateExemplarDto exemplarDto);
    Task UpdateExemplarAsync(int id, UpdateExemplarDto exemplarDto);
    Task DeleteExemplarAsync(int id);
}