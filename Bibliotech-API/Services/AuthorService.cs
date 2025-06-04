using AutoMapper;
using Bibliotech_API.Models.DTO;
using Bibliotech_API.Models.Entities;
using Bibliotech_API.Repositories;

namespace Bibliotech_API.Services;

public class AuthorService
{
    private readonly AuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public AuthorService(AuthorRepository authorRepository, IMapper mapper)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
    }
    
    public List<AuthorEntity> GetAll()
    {
        return _authorRepository.GetAll();
    }

    public AuthorEntity? GetById(int id)
    {
        return _authorRepository.GetById(id);
    }
    
    public void Add(AuthorCreateDto author)
    {
        var autorEntity = _mapper.Map<AuthorEntity>(author);
        _authorRepository.Add(autorEntity);
    }
    
    public void Update(AuthorUpdateDto author)
    {
        var autorEntity = _mapper.Map<AuthorEntity>(author);
        _authorRepository.Update(autorEntity);
    }
    
    public void Remove(int id)
    {
        var autor = GetById(id);
        if (autor == null) throw new HttpRequestException("Autor não encontrado!");
        
        _authorRepository.Remove(autor);
    }
}