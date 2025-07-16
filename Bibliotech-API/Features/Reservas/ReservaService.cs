using AutoMapper;
using Bibliotech_API.Common.Enums;
using Bibliotech_API.Data;
using Bibliotech_API.Features.Reservas.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech_API.Features.Reservas;

public class ReservaService : IReservaService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ReservaService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Reserva>> GetAllReservasAsync()
    {
        return await _context.Reservas
            .Include(r => r.Exemplar)
            .ThenInclude(e => e.Livro)
            .Include(r => r.Usuario)
            .ToListAsync();
    }

    public async Task<Reserva> GetReservaByIdAsync(int id)
    {
        var reserva = await _context.Reservas
            .Include(r => r.Exemplar)
            .ThenInclude(e => e.Livro)
            .Include(r => r.Usuario)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reserva == null)
            throw new BadHttpRequestException($"Reserva com ID {id} não existe na base de dados.",
                StatusCodes.Status404NotFound);

        return reserva;
    }

    public async Task CreateReservaAsync(CreateReservaDto reservaDto)
    {
        var exemplar = await _context.Exemplares.FindAsync(reservaDto.IdExemplar);
        if (exemplar == null)
            throw new BadHttpRequestException($"Exemplar com ID {reservaDto.IdExemplar} não existe.",
                StatusCodes.Status400BadRequest);

        var usuario = await _context.Usuarios.FindAsync(reservaDto.IdUsuario);
        if (usuario == null)
            throw new BadHttpRequestException($"Usuário com ID {reservaDto.IdUsuario} não existe.",
                StatusCodes.Status400BadRequest);

        if (exemplar.Situacao is SituacaoLivroEnum.Extraviado or SituacaoLivroEnum.Descartado)
            throw new BadHttpRequestException("Não é possível reservar um exemplar que está extraviado ou descartado.",
                StatusCodes.Status400BadRequest);

        var isExemplarEmprestado = await _context.Emprestimos.AnyAsync(e =>
            e.IdExemplar == reservaDto.IdExemplar &&
            (e.Status == StatusEmprestimoEnum.EmAndamento || e.Status == StatusEmprestimoEnum.Atrasado ||
             e.Status == StatusEmprestimoEnum.Extraviado));
        if (isExemplarEmprestado)
            throw new BadHttpRequestException("Este exemplar está atualmente emprestado e não pode ser reservado.",
                StatusCodes.Status400BadRequest);

        var duracao = reservaDto.DataFim - reservaDto.DataInicio;
        if (duracao.Days > 30)
            throw new BadHttpRequestException("Os livros só podem ser reservados por no máximo 30 dias.",
                StatusCodes.Status400BadRequest);

        var reserva = _mapper.Map<Reserva>(reservaDto);
        reserva.Status = StatusReservaEnum.Pendente;


        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateReservaAsync(int id, UpdateReservaDto reservaDto)
    {
        var reserva = await GetReservaByIdAsync(id);

        if (reserva.Status == StatusReservaEnum.Concluida ||
            reserva.Status == StatusReservaEnum.Cancelada ||
            reserva.Status == StatusReservaEnum.Expirada)
            throw new BadHttpRequestException($"Não é possível atualizar uma reserva com status '{reserva.Status}'.",
                StatusCodes.Status400BadRequest);

        if (reserva.IdExemplar != reservaDto.IdExemplar)
            throw new BadHttpRequestException("O exemplar da reserva não pode ser alterado.",
                StatusCodes.Status400BadRequest);

        if (reserva.IdUsuario != reservaDto.IdUsuario)
            throw new BadHttpRequestException("O usuário da reserva não pode ser alterado",
                StatusCodes.Status400BadRequest);

        var duracao = reservaDto.DataFim - reservaDto.DataInicio;
        if (duracao.Days > 30)
            throw new BadHttpRequestException("Os livros só podem ser reservados por no máximo 30 dias.",
                StatusCodes.Status400BadRequest);

        _mapper.Map(reservaDto, reserva);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReservaAsync(int id)
    {
        var reserva = await GetReservaByIdAsync(id);
        if (reserva.Status is StatusReservaEnum.Concluida or StatusReservaEnum.Expirada)
        {
            throw new BadHttpRequestException($"Não é possível deletar uma reserva com status '{reserva.Status}'.",
                StatusCodes.Status400BadRequest);
        }

        _context.Reservas.Remove(reserva);
        await _context.SaveChangesAsync();
    }
}