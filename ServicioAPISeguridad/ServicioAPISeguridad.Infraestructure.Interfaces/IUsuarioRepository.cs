using ServicioAPISeguridad.Domain.Entities.Auth;
using ServicioAPISeguridad.Domain.Entities.Sesion;
using ServicioAPISeguridad.Domain.Entities.Usuario;

namespace ServicioAPISeguridad.Infraestructure.Interfaces
{
    public interface IUsuarioRepository
    {
        UserResponseEntities Login (AuthRequestEntities authRequestEntities);
        void GuardarSesion(SesionUsuarioEntities pSesionUsuarioDto);
        void UserRegister(UserRegisterDto pUserRegisterDto);
        bool ValidateByUser(string pUser);
        bool ValidateByEmail(string pEmail);
    }
}
