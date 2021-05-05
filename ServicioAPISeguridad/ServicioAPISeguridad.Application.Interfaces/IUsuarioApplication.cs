using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Transversal.Common;
using System;

namespace ServicioAPISeguridad.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Response<UserResponseDto> Login(string pUserName, string pPassword);
        void Prueba();
    }
}
