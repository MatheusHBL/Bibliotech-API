using AutoMapper;
using Bibliotech_API.Models.Dtos;
using Bibliotech_API.Models.Entities;

namespace Bibliotech_API.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AutorCreateDto, AutorEntity>();
            CreateMap<AutorUpdateDto, AutorEntity>();
        }
    }
}
