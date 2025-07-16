using Bibliotech_API.Features.Usuarios.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Features.Usuarios;

[ApiController]
[Route("usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult> GetUsuarios()
    {
        var usuarios = await _usuarioService.GetAllUsuariosAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetUsuario(int id)
    {
        var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult> PostUsuario([FromBody] CreateUsuarioDto usuarioDto)
    {
        await _usuarioService.CreateUsuarioAsync(usuarioDto);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutUsuario(int id, [FromBody] UpdateUsuarioDto usuarioDto)
    {
        await _usuarioService.UpdateUsuarioAsync(id, usuarioDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        await _usuarioService.DeleteUsuarioAsync(id);
        return NoContent();
    }
}