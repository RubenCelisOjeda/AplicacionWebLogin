using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Transversal.Common;

namespace ServicioAPISeguridad.Infraestructure.Interfaces
{
    public interface IUsuarioRepository
    {
        void Prueba();
        UserResponseDto Login (string pUserName, string pPassword);
    }
}
