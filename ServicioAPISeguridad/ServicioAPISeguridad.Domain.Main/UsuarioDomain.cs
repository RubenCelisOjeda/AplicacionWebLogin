using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Domain.Interfaces;
using ServicioAPISeguridad.Infraestructure.Interfaces;
using ServicioAPISeguridad.Transversal.Common;

namespace ServicioAPISeguridad.Domain.Main
{
    public class UsuarioDomain : IUsuarioDomain
    {
        private readonly IUsuarioRepository _usuarioRepository;


        public UsuarioDomain(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public UserResponseDto Login(string pUserName, string pPassword)
        {
            return _usuarioRepository.Login(pUserName, pPassword);
        }

        public void Prueba()
        {
            _usuarioRepository.Prueba();
        }
    }
}
