using System;

namespace ServicioAPISeguridad.Application.Dto.Usuario.Request
{
    public class UserRegisterRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
