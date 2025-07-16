using Bibliotech_API.Features.Autores.Dtos;

namespace Bibliotech_API.Features.Autores;

public interface IAutorService
{
    Task<List<Autor>> GetAllAutoresAsync();
    Task<Autor> GetAutorByIdAsync(int id);
    Task CreateAutorAsync(CreateAutorDto autor);
    Task UpdateAutorAsync(int id, UpdateAutorDto autorDto);
    Task DeleteAutorAsync(int id);
}