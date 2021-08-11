using System;

namespace ServicioAPISeguridad.Domain.Entities.Sesion
{
    public class SesionUsuarioEntities
    {
        public string Token { get; set; }
        public int IdUser { get; set; }
        public DateTime DateStart { get; set; }
        public byte Status { get; set; }
    }
}
