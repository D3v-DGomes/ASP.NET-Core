using AutoMapper;
using CrudWithDapper.DTO;
using CrudWithDapper.Models;

namespace CrudWithDapper.Profiles
{
    public class ProfileAutoMapper : Profile   // Passo 7.1: Criação do Profile do AutoMapper
    {
        public ProfileAutoMapper() 
        {
            CreateMap<Usuario, UsuarioListarDto>();  // Mapeamento entre o modelo Usuario e o DTO UsuarioListarDto
        }
    }
}
