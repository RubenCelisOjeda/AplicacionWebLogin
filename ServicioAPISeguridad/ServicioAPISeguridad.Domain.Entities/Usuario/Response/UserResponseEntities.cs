namespace ServicioAPISeguridad.Domain.Entities.Usuario
{
    public class UserResponseEntities
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
