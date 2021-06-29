using ServicioAPISeguridad.Domain.Entities.Sesion;
using ServicioAPISeguridad.Domain.Entities.Usuario;

namespace ServicioAPISeguridad.Infraestructure.Interfaces
{
    public interface IUsuarioRepository
    {
        void Prueba();
        UserResponseDto Login (string pUserName, string pPassword);
        void GuardarSesion(SesionUsuarioDto pSesionUsuarioDto);
        void UserRegister(UserRegisterDto pUserRegisterDto);
    }
}
