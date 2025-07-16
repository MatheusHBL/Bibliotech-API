using Bibliotech_API.Features.Categorias.Dtos;

namespace Bibliotech_API.Features.Categorias;

public interface ICategoriaService
{
    Task<List<Categoria>> GetAllCategoriasAsync();
    Task<Categoria> GetCategoriaByIdAsync(int id);
    Task CreateCategoriaAsync(CreateCategoriaDto categoriaDto);
    Task UpdateCategoriaAsync(int id, UpdateCategoriaDto categoriaDto);
    Task DeleteCategoriaAsync(int id);
}