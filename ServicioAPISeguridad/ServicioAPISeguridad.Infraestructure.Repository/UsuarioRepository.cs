using Dapper;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Infraestructure.Interfaces;
using System.Data;
using System.Linq;

namespace ServicioAPISeguridad.Infraestructure.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly IConnectionFactory _configuration;

        public UsuarioRepository(IConnectionFactory configuration)
        {
            _configuration = configuration;
        }

        public UserResponseDto Login(UserRequestDto pUsuario)
        {
            using (var connection = _configuration.GetConnectionSeguridad)
            {
                var procedure = "";
                var parameters = new DynamicParameters();
                parameters.Add("", pUsuario.Password,DbType.String);
                parameters.Add("", pUsuario.Password, DbType.String);

                return connection.Query<UserResponseDto>(procedure, parameters).FirstOrDefault();
            }
        }

        public void Prueba()
        {
            using (var connection = _configuration?.GetConnectionSeguridad)
            {

            }
        }
    }
}
