using AutoMapper;
using Bibliotech_API.Common.Enums;
using Bibliotech_API.Data;
using Bibliotech_API.Features.Exemplares.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Features.Exemplares;

public class ExemplarService : IExemplarService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ExemplarService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Exemplar>> GetAllExemplaresAsync()
    {
        return await _context.Exemplares.Include(e => e.Livro).ToListAsync();
    }

    public async Task<Exemplar> GetExemplarByIdAsync(int id)
    {
        var exemplar = await _context.Exemplares.Include(e => e.Livro).FirstOrDefaultAsync(e => e.Id == id);
        if (exemplar == null)
            throw new BadHttpRequestException($"Exemplar com ID {id} não existe na base de dados.",
                StatusCodes.Status404NotFound);

        return exemplar;
    }

    public async Task CreateExemplarAsync(CreateExemplarDto exemplarDto)
    {
        var livro = await _context.Livros.FindAsync(exemplarDto.IdLivro);
        if (livro == null)
            throw new BadHttpRequestException($"Livro com ID {exemplarDto.IdLivro} não existe.",
                StatusCodes.Status400BadRequest);

        var exemplar = _mapper.Map<Exemplar>(exemplarDto);
        if (!Enum.IsDefined(typeof(SituacaoLivroEnum), exemplar.Situacao))
            exemplar.Situacao = SituacaoLivroEnum.Normal;

        _context.Exemplares.Add(exemplar);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateExemplarAsync(int id, UpdateExemplarDto exemplarDto)
    {
        var exemplar = await GetExemplarByIdAsync(id);
        if (exemplarDto.IdLivro != exemplar.IdLivro)
            throw new BadHttpRequestException("O livro de um exemplar não pode ser alterado.",
                StatusCodes.Status400BadRequest);

        _mapper.Map(exemplarDto, exemplar);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteExemplarAsync(int id)
    {
        var exemplar = await GetExemplarByIdAsync(id);

        var hasActiveEmprestimos = await _context.Emprestimos.AnyAsync(e =>
            e.IdExemplar == id &&
            (e.Status == StatusEmprestimoEnum.EmAndamento || e.Status == StatusEmprestimoEnum.Atrasado ||
             e.Status == StatusEmprestimoEnum.Extraviado));
        if (hasActiveEmprestimos)
            throw new BadHttpRequestException(
                "Não é possível deletar um exemplar com empréstimos ativos, atrasados ou extraviados.",
                StatusCodes.Status400BadRequest);

        var hasActiveReservas = await _context.Reservas.AnyAsync(r =>
            r.IdExemplar == id &&
            (r.Status == StatusReservaEnum.Pendente || r.Status == StatusReservaEnum.AguardandoRetirada));
        if (hasActiveReservas)
            throw new BadHttpRequestException("Não é possível deletar um exemplar com reservas ativas.",
                StatusCodes.Status400BadRequest);

        _context.Exemplares.Remove(exemplar);
        await _context.SaveChangesAsync();
    }
}