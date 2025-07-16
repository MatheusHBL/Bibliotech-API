using Bibliotech_API.Features.Prateleiras.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Features.Prateleiras;

[ApiController]
[Route("prateleiras")]
public class PrateleirasController : ControllerBase
{
    private readonly IPrateleiraService _prateleiraService;

    public PrateleirasController(IPrateleiraService prateleiraService)
    {
        _prateleiraService = prateleiraService;
    }

    [HttpGet]
    public async Task<ActionResult> GetPrateleiras()
    {
        var prateleiras = await _prateleiraService.GetAllPrateleirasAsync();
        return Ok(prateleiras);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetPrateleira(int id)
    {
        var prateleira = await _prateleiraService.GetPrateleiraByIdAsync(id);
        return Ok(prateleira);
    }

    [HttpPost]
    public async Task<ActionResult> PostPrateleira([FromBody] CreatePrateleiraDto prateleiraDto)
    {
        await _prateleiraService.CreatePrateleiraAsync(prateleiraDto);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutPrateleira(int id, [FromBody] UpdatePrateleiraDto prateleiraDto)
    {
        await _prateleiraService.UpdatePrateleiraAsync(id, prateleiraDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePrateleira(int id)
    {
        await _prateleiraService.DeletePrateleiraAsync(id);
        return NoContent();
    }
}