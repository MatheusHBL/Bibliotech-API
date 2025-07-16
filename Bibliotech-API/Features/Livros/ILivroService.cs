using Bibliotech_API.Features.Livros.Dtos;

namespace Bibliotech_API.Features.Livros;

public interface ILivroService
{
    Task<List<Livro>> GetAllLivrosAsync();
    Task<Livro> GetLivroByIdAsync(int id);
    Task CreateLivroAsync(CreateLivroDto livroDto);
    Task UpdateLivroAsync(int id, UpdateLivroDto livroDto);
    Task DeleteLivroAsync(int id);
}