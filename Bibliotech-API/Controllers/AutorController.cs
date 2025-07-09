using Bibliotech_API.Models.Dtos;
using Bibliotech_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Controllers;

[ApiController]
[Route("autor")]
public class AutorController : ControllerBase
{
    private readonly AutorService _autorService;
    
    public AutorController(AutorService autorService)
    {
        _autorService = autorService;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var autores = _autorService.GetAll();
        return Ok(autores);
    }

    [HttpGet(":id")]
    public IActionResult GetById([FromQuery] int id)
    {
        var autor = _autorService.GetById(id);
        return Ok(autor);
    }

    [HttpPost]
    public IActionResult Add([FromQuery] AutorCreateDto autor)
    {
        _autorService.Add(autor);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromQuery] AutorUpdateDto autor)
    {
        _autorService.Update(autor);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Remove([FromQuery] int id)
    {
        _autorService.Remove(id);
        return Ok();
    }
}