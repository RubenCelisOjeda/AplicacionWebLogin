using Dapper;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Infraestructure.Interfaces;
using ServicioAPISeguridad.Transversal.Common;
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

        public UserResponseDto Login(string pUserName,string pPassword)
        {
            using (var connection = _configuration.GetConnectionSeguridad)
            {
                const string procedure = "SPR_PROC_S_Login";
                var parameters = new DynamicParameters();
                parameters.Add("@pUserName", pUserName, DbType.String);
                parameters.Add("@pPassword", pPassword, DbType.String);

                return connection.Query<UserResponseDto>(procedure, parameters,commandType:CommandType.StoredProcedure).FirstOrDefault();
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
