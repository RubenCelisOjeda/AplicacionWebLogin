namespace ServicioAPISeguridad.Application.Dto
{
    public class LoginRequest
    {
        public int IdUser { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
