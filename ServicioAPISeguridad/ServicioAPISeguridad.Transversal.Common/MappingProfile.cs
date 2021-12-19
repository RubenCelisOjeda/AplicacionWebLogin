using AutoMapper;
using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Application.Dto.Sesion.Request;
using ServicioAPISeguridad.Application.Dto.Usuario.Request;
using ServicioAPISeguridad.Domain.Entities.Auth;
using ServicioAPISeguridad.Domain.Entities.Sesion;
using ServicioAPISeguridad.Domain.Entities.Usuario;

namespace ServicioAPISeguridad.Transversal.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Request
            CreateMap<AuthRequestDto, AuthRequestEntities>();
            CreateMap<SesionUsuarioDto, SesionUsuarioEntities>();
            CreateMap<UserRegisterRequestDto, UserRegisterEntities > ();
            
            //Response Souurce -Destino
            CreateMap<UserResponseEntities, AuthResponseDto>();

           
        }
    }
}
