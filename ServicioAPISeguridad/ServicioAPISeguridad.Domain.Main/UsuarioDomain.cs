using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Domain.Entities.Auth;
using ServicioAPISeguridad.Domain.Entities.Sesion;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Domain.Interfaces;
using ServicioAPISeguridad.Infraestructure.Interfaces;
using System.Threading.Tasks;

namespace ServicioAPISeguridad.Domain.Main
{
    public class UsuarioDomain : IUsuarioDomain
    {
        private readonly IUsuarioRepository _usuarioRepository;


        public UsuarioDomain(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<int> GuardarSesion(SesionUsuarioEntities pSesionUsuario)
        {
            var response = await _usuarioRepository.GuardarSesion(pSesionUsuario);
            return response;
        }

        public async Task<UserResponseEntities> Login(AuthRequestEntities authRequestEntities)
        {
            var response = await _usuarioRepository.Login(authRequestEntities);
            return response;
        }

        public int UserRegister(UserRegisterEntities pUserRegisterEntities)
        {
            return _usuarioRepository.UserRegister(pUserRegisterEntities);
        }

        public bool ValidateByUser(string pUser)
        {
            return _usuarioRepository.ValidateByUser(pUser);
        }

        public bool ValidateByEmail(string pEmail)
        {
            return _usuarioRepository.ValidateByEmail(pEmail);
        }
    }
}
