using Bibliotech_API.Features.Usuarios.Dtos;

namespace Bibliotech_API.Features.Usuarios;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
    Task<Usuario> GetUsuarioByIdAsync(int id);
    Task CreateUsuarioAsync(CreateUsuarioDto usuarioDto);
    Task UpdateUsuarioAsync(int id, UpdateUsuarioDto usuarioDto);
    Task DeleteUsuarioAsync(int id);
}