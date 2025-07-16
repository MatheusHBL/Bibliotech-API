using Bibliotech_API.Features.Categorias.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Features.Categorias;

[ApiController]
[Route("categorias")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult> GetCategorias()
    {
        var categorias = await _categoriaService.GetAllCategoriasAsync();
        return Ok(categorias);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetCategoria(int id)
    {
        var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult> PostCategoria([FromBody] CreateCategoriaDto categoriaDto)
    {
        await _categoriaService.CreateCategoriaAsync(categoriaDto);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutCategoria(int id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
        await _categoriaService.UpdateCategoriaAsync(id, categoriaDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
        await _categoriaService.DeleteCategoriaAsync(id);
        return NoContent();
    }
}