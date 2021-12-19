using AutoMapper;
using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Application.Dto.Sesion.Request;
using ServicioAPISeguridad.Domain.Entities.Auth;
using ServicioAPISeguridad.Domain.Entities.Auth.Response;
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

            //Response Souurce -Destino
            CreateMap<UserResponseEntities, AuthResponseDto>();
        }
    }
}
