using Bibliotech_API.Features.Reservas.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Features.Reservas;

[ApiController]
[Route("reservas")]
public class ReservasController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservasController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    [HttpGet]
    public async Task<ActionResult> GetReservas()
    {
        var reservas = await _reservaService.GetAllReservasAsync();
        return Ok(reservas);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetReserva(int id)
    {
        var reserva = await _reservaService.GetReservaByIdAsync(id);
        return Ok(reserva);
    }

    [HttpPost]
    public async Task<ActionResult> PostReserva([FromBody] CreateReservaDto reservaDto)
    {
        await _reservaService.CreateReservaAsync(reservaDto);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutReserva(int id, [FromBody] UpdateReservaDto reservaDto)
    {
        await _reservaService.UpdateReservaAsync(id, reservaDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReserva(int id)
    {
        await _reservaService.DeleteReservaAsync(id);
        return NoContent();
    }
}