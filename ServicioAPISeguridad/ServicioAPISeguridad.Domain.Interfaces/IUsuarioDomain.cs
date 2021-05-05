using ServicioAPISeguridad.Domain.Entities.Usuario;

namespace ServicioAPISeguridad.Domain.Interfaces
{
    public interface IUsuarioDomain
    {
        UserResponseDto Login(string pUserName, string pPassword);

        void Prueba();
    }
}
