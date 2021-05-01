using ServicioAPISeguridad.Domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicioAPISeguridad.Infraestructure.Interfaces
{
    public interface IUsuarioRepository
    {
        void Prueba();
        UserResponseDto Login (UserRequestDto pUsuario);
    }
}
