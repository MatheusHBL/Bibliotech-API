using api_biblioteca.Models.DTO;
using api_biblioteca.Models.Entities;
using api_biblioteca.Repositories;
using AutoMapper;

namespace api_biblioteca.Services;

public class AutorService
{
    private readonly AutorRepository _autorRepository;
    private readonly IMapper _mapper;

    public AutorService(AutorRepository autorRepository, IMapper mapper)
    {
        _mapper = mapper;
        _autorRepository = autorRepository;
    }
    
    public List<AutorEntity> GetAll()
    {
        return _autorRepository.GetAll();
    }

    public AutorEntity? GetById(int id)
    {
        return _autorRepository.GetById(id);
    }
    
    public void Add(AutorCreateDto autor)
    {
        var autorEntity = _mapper.Map<AutorEntity>(autor);
        _autorRepository.Add(autorEntity);
    }
    
    public void Update(AutorUpdateDto autor)
    {
        var autorEntity = _mapper.Map<AutorEntity>(autor);
        _autorRepository.Update(autorEntity);
    }
    
    public void Remove(int id)
    {
        var autor = GetById(id);
        if (autor == null) throw new HttpRequestException("Autor não encontrado!");
        
        _autorRepository.Remove(autor);
    }
}