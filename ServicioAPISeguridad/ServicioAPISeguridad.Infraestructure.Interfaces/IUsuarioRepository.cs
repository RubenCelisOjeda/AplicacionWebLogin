using ServicioAPISeguridad.Domain.Entities.Auth;
using ServicioAPISeguridad.Domain.Entities.Sesion;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using System.Threading.Tasks;

namespace ServicioAPISeguridad.Infraestructure.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<UserResponseEntities> Login (AuthRequestEntities authRequestEntities);
        Task<int> GuardarSesion(SesionUsuarioEntities pSesionUsuarioDto);
        Task<int> UserRegister(UserRegisterEntities pUserRegisterDto);
        Task<bool> ValidateByUser(string pUser);
        bool ValidateByEmail(string pEmail);
    }
}
