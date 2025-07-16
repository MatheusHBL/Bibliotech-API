using Bibliotech_API.Features.Perfis.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Features.Perfis;

[ApiController]
[Route("perfis")]
public class PerfisController : ControllerBase
{
    private readonly IPerfilService _perfilService;

    public PerfisController(IPerfilService perfilService)
    {
        _perfilService = perfilService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Perfil>>> GetPerfis()
    {
        var perfis = await _perfilService.GetAllPerfisAsync();
        return Ok(perfis);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Perfil>> GetPerfil(int id)
    {
        var perfil = await _perfilService.GetPerfilByIdAsync(id);
        return Ok(perfil);
    }

    [HttpPost]
    public async Task<ActionResult<int>> PostPerfil([FromBody] CreatePerfilDto perfilDto)
    {
        await _perfilService.CreatePerfilAsync(perfilDto);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutPerfil(int id, [FromBody] UpdatePerfilDto perfilDto)
    {
        await _perfilService.UpdatePerfilAsync(id, perfilDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePerfil(int id)
    {
        await _perfilService.DeletePerfilAsync(id);
        return NoContent();
    }
}