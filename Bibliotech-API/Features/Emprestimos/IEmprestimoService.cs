using Bibliotech_API.Features.Emprestimos.Dtos;

namespace Bibliotech_API.Features.Emprestimos;

public interface IEmprestimoService
{
    Task<List<Emprestimo>> GetAllEmprestimosAsync();
    Task<Emprestimo> GetEmprestimoByIdAsync(int id);
    Task CreateEmprestimoAsync(CreateEmprestimoDto emprestimoDto);
    Task UpdateEmprestimoAsync(int id, UpdateEmprestimoDto emprestimoDto);
    Task DeleteEmprestimoAsync(int id);
    Task DevolverExemplarAsync(int emprestimoId, bool danificado, string? observacao);
}