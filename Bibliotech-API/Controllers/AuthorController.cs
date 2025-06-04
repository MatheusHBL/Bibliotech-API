using Bibliotech_API.Models.DTO;
using Bibliotech_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Controllers;

[ApiController]
[Route("autor")]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _authorService;
    
    public AuthorController(AuthorService authorService)
    {
        _authorService = authorService;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var autores = _authorService.GetAll();
        return Ok(autores);
    }

    [HttpGet(":id")]
    public IActionResult GetById([FromQuery] int id)
    {
        var autor = _authorService.GetById(id);
        return Ok(autor);
    }

    [HttpPost]
    public IActionResult Add([FromQuery] AuthorCreateDto author)
    {
        _authorService.Add(author);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromQuery] AuthorUpdateDto author)
    {
        _authorService.Update(author);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Remove([FromQuery] int id)
    {
        _authorService.Remove(id);
        return Ok();
    }
}