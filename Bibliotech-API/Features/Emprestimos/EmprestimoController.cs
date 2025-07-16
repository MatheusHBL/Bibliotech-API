using Bibliotech_API.Features.Emprestimos.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotech_API.Features.Emprestimos;

[ApiController]
[Route("emprestimos")]
public class EmprestimosController : ControllerBase
{
    private readonly IEmprestimoService _emprestimoService;

    public EmprestimosController(IEmprestimoService emprestimoService)
    {
        _emprestimoService = emprestimoService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Emprestimo>>> GetEmprestimos()
    {
        var emprestimos = await _emprestimoService.GetAllEmprestimosAsync();
        return Ok(emprestimos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Emprestimo>> GetEmprestimo(int id)
    {
        var emprestimo = await _emprestimoService.GetEmprestimoByIdAsync(id);
        return Ok(emprestimo);
    }

    [HttpPost]
    public async Task<ActionResult<int>> PostEmprestimo([FromBody] CreateEmprestimoDto emprestimoDto)
    {
        await _emprestimoService.CreateEmprestimoAsync(emprestimoDto);
        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutEmprestimo(int id, [FromBody] UpdateEmprestimoDto emprestimoDto)
    {
        await _emprestimoService.UpdateEmprestimoAsync(id, emprestimoDto);
        return NoContent();
    }

    [HttpPost("{id:int}/devolucao")]
    public async Task<IActionResult> DevolverExemplar(int id, [FromBody] DevolucaoEmprestimoDto devolucaoDto)
    {
        await _emprestimoService.DevolverExemplarAsync(id, devolucaoDto.Danificado, devolucaoDto.Observacao);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmprestimo(int id)
    {
        await _emprestimoService.DeleteEmprestimoAsync(id);
        return NoContent();
    }
}