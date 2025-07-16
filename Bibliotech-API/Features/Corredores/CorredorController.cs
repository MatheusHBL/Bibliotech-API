using Bibliotech_API.Features.Corredores.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Features.Corredores;

[ApiController]
[Route("corredores")]
public class CorredoresController : ControllerBase
{
    private readonly ICorredorService _corredorService;

    public CorredoresController(ICorredorService corredorService)
    {
        _corredorService = corredorService;
    }

    [HttpGet]
    public async Task<ActionResult> GetCorredores()
    {
        var corredores = await _corredorService.GetAllCorredoresAsync();
        return Ok(corredores);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Corredor>> GetCorredor(int id)
    {
        var corredor = await _corredorService.GetCorredorByIdAsync(id);
        return Ok(corredor);
    }

    [HttpPost]
    public async Task<ActionResult<int>> PostCorredor([FromBody] CreateCorredorDto corredorDto)
    {
        await _corredorService.CreateCorredorAsync(corredorDto);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutCorredor(int id, [FromBody] UpdateCorredorDto corredorDto)
    {
        await _corredorService.UpdateCorredorAsync(id, corredorDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCorredor(int id)
    {
        await _corredorService.DeleteCorredorAsync(id);
        return NoContent();
    }
}