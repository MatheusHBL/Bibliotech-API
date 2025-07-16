using Bibliotech_API.Features.Autores.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Features.Autores;

[ApiController]
[Route("autores")]
public class AutoresController : ControllerBase
{
    private readonly IAutorService _autorService;

    public AutoresController(IAutorService autorService)
    {
        _autorService = autorService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAutores()
    {
        var autores = await _autorService.GetAllAutoresAsync();
        return Ok(autores);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetAutorById(int id)
    {
        var autor = await _autorService.GetAutorByIdAsync(id);
        return Ok(autor);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAutor([FromBody] CreateAutorDto autorDto)
    {
        await _autorService.CreateAutorAsync(autorDto);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAutor(int id, [FromBody] UpdateAutorDto autorDto)
    {
        await _autorService.UpdateAutorAsync(id, autorDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAutor(int id)
    {
        await _autorService.DeleteAutorAsync(id);
        return NoContent();
    }
}