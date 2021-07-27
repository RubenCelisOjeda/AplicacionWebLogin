using ServicioAPISeguridad.Domain.Entities.Sesion;
using ServicioAPISeguridad.Domain.Entities.Usuario;

namespace ServicioAPISeguridad.Domain.Interfaces
{
    public interface IUsuarioDomain
    {
        UserResponseDto Login(string pUserName, string pPassword);
        void GuardarSesion(SesionUsuarioDto pSesionUsuario);
        void UserRegister(UserRegisterDto pUserRegisterDto);
        bool ValidateByUser(string pUser);
        bool ValidateByEmail(string pEmail);
    }
}
