using System;

namespace ServicioAPISeguridad.Domain.Entities.Usuario
{
    public class UserRegisterEntities
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreate { get; set; }
        public byte  Status { get; set; }
    }
}
