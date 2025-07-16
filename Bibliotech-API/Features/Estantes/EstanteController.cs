using Bibliotech_API.Features.Estantes.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Features.Estantes;

[ApiController]
[Route("estantes")]
public class EstantesController : ControllerBase
{
    private readonly IEstanteService _estanteService;

    public EstantesController(IEstanteService estanteService)
    {
        _estanteService = estanteService;
    }

    [HttpGet]
    public async Task<ActionResult> GetEstantes()
    {
        var estantes = await _estanteService.GetAllEstantesAsync();
        return Ok(estantes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetEstante(int id)
    {
        var estante = await _estanteService.GetEstanteByIdAsync(id);
        return Ok(estante);
    }

    [HttpPost]
    public async Task<ActionResult> PostEstante([FromBody] CreateEstanteDto estanteDto)
    {
        await _estanteService.CreateEstanteAsync(estanteDto);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutEstante(int id, [FromBody] UpdateEstanteDto estanteDto)
    {
        await _estanteService.UpdateEstanteAsync(id, estanteDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEstante(int id)
    {
        await _estanteService.DeleteEstanteAsync(id);
        return NoContent();
    }
}