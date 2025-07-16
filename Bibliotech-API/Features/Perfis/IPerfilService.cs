using Bibliotech_API.Features.Perfis.Dtos;

namespace Bibliotech_API.Features.Perfis;

public interface IPerfilService
{
    Task<List<Perfil>> GetAllPerfisAsync();
    Task<Perfil> GetPerfilByIdAsync(int id);
    Task CreatePerfilAsync(CreatePerfilDto perfilDto);
    Task UpdatePerfilAsync(int id, UpdatePerfilDto perfilDto);
    Task DeletePerfilAsync(int id);
}