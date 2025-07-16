using Bibliotech_API.Features.Exemplares.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Features.Exemplares;

[ApiController]
[Route("exemplares")]
public class ExemplaresController : ControllerBase
{
    private readonly IExemplarService _exemplarService;

    public ExemplaresController(IExemplarService exemplarService)
    {
        _exemplarService = exemplarService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Exemplar>>> GetExemplares()
    {
        var exemplares = await _exemplarService.GetAllExemplaresAsync();
        return Ok(exemplares);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Exemplar>> GetExemplar(int id)
    {
        var exemplar = await _exemplarService.GetExemplarByIdAsync(id);
        return Ok(exemplar);
    }

    [HttpPost]
    public async Task<ActionResult> PostExemplar([FromBody] CreateExemplarDto exemplarDto)
    {
        await _exemplarService.CreateExemplarAsync(exemplarDto);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutExemplar(int id, [FromBody] UpdateExemplarDto exemplarDto)
    {
        await _exemplarService.UpdateExemplarAsync(id, exemplarDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteExemplar(int id)
    {
        await _exemplarService.DeleteExemplarAsync(id);
        return NoContent();
    }
}