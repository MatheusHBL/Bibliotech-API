using AutoMapper;
using Bibliotech_API.Features.Autores;
using Bibliotech_API.Features.Autores.Dtos;
using Bibliotech_API.Features.Categorias;
using Bibliotech_API.Features.Categorias.Dtos;
using Bibliotech_API.Features.Corredores;
using Bibliotech_API.Features.Corredores.Dtos;
using Bibliotech_API.Features.Estantes;
using Bibliotech_API.Features.Estantes.Dtos;
using Bibliotech_API.Features.Exemplares;
using Bibliotech_API.Features.Exemplares.Dtos;
using Bibliotech_API.Features.Livros;
using Bibliotech_API.Features.Livros.Dtos;
using Bibliotech_API.Features.Perfis;
using Bibliotech_API.Features.Perfis.Dtos;
using Bibliotech_API.Features.Prateleiras;
using Bibliotech_API.Features.Prateleiras.Dtos;
using Bibliotech_API.Features.Reservas;
using Bibliotech_API.Features.Reservas.Dtos;
using Bibliotech_API.Features.Usuarios;
using Bibliotech_API.Features.Usuarios.Dtos;

namespace Bibliotech_API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateAutorDto, Autor>();
        CreateMap<UpdateAutorDto, Autor>();

        CreateMap<CreateLivroDto, Livro>();
        CreateMap<UpdateLivroDto, Livro>();

        CreateMap<CreateExemplarDto, Exemplar>();
        CreateMap<UpdateExemplarDto, Exemplar>();

        CreateMap<CreateCategoriaDto, Categoria>();
        CreateMap<UpdateCategoriaDto, Categoria>();

        CreateMap<CreatePrateleiraDto, Prateleira>();
        CreateMap<UpdatePrateleiraDto, Prateleira>();

        CreateMap<CreateEstanteDto, Estante>();
        CreateMap<UpdateEstanteDto, Estante>();

        CreateMap<CreateCorredorDto, Corredor>();
        CreateMap<UpdateCorredorDto, Corredor>();

        CreateMap<CreateReservaDto, Reserva>();
        CreateMap<UpdateReservaDto, Reserva>();

        CreateMap<CreateUsuarioDto, Usuario>().ForMember(dest => dest.Senha, opt => opt.Ignore());
        CreateMap<UpdateUsuarioDto, Usuario>().ForMember(dest => dest.Senha, opt => opt.Ignore());

        CreateMap<CreatePerfilDto, Perfil>();
        CreateMap<UpdatePerfilDto, Perfil>();
    }
}