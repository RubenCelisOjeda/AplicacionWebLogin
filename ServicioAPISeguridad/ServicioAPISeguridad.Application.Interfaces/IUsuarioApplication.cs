using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Transversal.Common;

namespace ServicioAPISeguridad.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Response<UserResponseDto> Login(string pUserName, string pPassword);
        void Prueba();
        Response<UserRegisterDto> UserRegister(UserRegisterDto pUserRegisterDto);
    }
}
