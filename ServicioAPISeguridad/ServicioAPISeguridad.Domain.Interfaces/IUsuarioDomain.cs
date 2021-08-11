using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Domain.Entities.Auth;
using ServicioAPISeguridad.Domain.Entities.Sesion;
using ServicioAPISeguridad.Domain.Entities.Usuario;

namespace ServicioAPISeguridad.Domain.Interfaces
{
    public interface IUsuarioDomain
    {
        UserResponseEntities Login(AuthRequestEntities authRequestEntities);
        void GuardarSesion(SesionUsuarioEntities pSesionUsuario);
        void UserRegister(UserRegisterDto pUserRegisterDto);
        bool ValidateByUser(string pUser);
        bool ValidateByEmail(string pEmail);
    }
}
