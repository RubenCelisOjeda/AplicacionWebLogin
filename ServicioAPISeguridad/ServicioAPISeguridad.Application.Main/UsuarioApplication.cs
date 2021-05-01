using ServicioAPISeguridad.Application.Interfaces;
using ServicioAPISeguridad.Domain.Interfaces;
using System;

namespace ServicioAPISeguridad.Application.Main
{
    public class UsuarioApplication : IUsuarioApplication
    {

        private readonly IUsuarioDomain _usuarioDomain;

        public UsuarioApplication(IUsuarioDomain usuarioDomain)
        {
            _usuarioDomain = usuarioDomain;
        }

        public void Prueba()
        {
            _usuarioDomain.Prueba();
        }
    }
}
