using Bibliotech_API.Features.Livros.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Features.Livros;

[ApiController]
[Route("livros")]
public class LivrosController : ControllerBase
{
    private readonly ILivroService _livroService;

    public LivrosController(ILivroService livroService)
    {
        _livroService = livroService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllLivros()
    {
        var livros = await _livroService.GetAllLivrosAsync();
        return Ok(livros);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetLivroById(int id)
    {
        var livro = await _livroService.GetLivroByIdAsync(id);
        return Ok(livro);
    }

    [HttpPost]
    public async Task<ActionResult> CreateLivro([FromBody] CreateLivroDto livroDto)
    {
        await _livroService.CreateLivroAsync(livroDto);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLivro(int id, [FromBody] UpdateLivroDto livroDto)
    {
        await _livroService.UpdateLivroAsync(id, livroDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLivro(int id)
    {
        await _livroService.DeleteLivroAsync(id);
        return NoContent();
    }
}