using Dapper;
using ServicioAPISeguridad.Domain.Entities.Auth;
using ServicioAPISeguridad.Domain.Entities.Sesion;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Infraestructure.Interfaces;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ServicioAPISeguridad.Infraestructure.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly IConnectionFactory _configuration;

        public UsuarioRepository(IConnectionFactory configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> GuardarSesion(SesionUsuarioEntities pSesionUsuario)
        {
            using (var connection = _configuration.GetConnectionSeguridad)
            {
                const string procedure = "PROC_I_SesionUsuario";
                var parameters = new DynamicParameters();
                parameters.Add("@pToken", pSesionUsuario.Token, DbType.String);
                parameters.Add("@pIdUser", pSesionUsuario.IdUser, DbType.Int32);
                parameters.Add("@pDateCreate", pSesionUsuario.DateStart, DbType.DateTime);
                parameters.Add("@pStatus", pSesionUsuario.Status, DbType.Byte);

                var response =  await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<UserResponseEntities> Login(AuthRequestEntities authRequestEntities)
        {
            using (var connection = _configuration.GetConnectionSeguridad)
            {
                const string procedure = "PROC_S_Login";
                var parameters = new DynamicParameters();
                parameters.Add("@pEmail", authRequestEntities.Username, DbType.String);
                parameters.Add("@pPassword", authRequestEntities.Password, DbType.String);

                var response = await connection.QueryAsync<UserResponseEntities>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return response.FirstOrDefault();
            }
        }

        public async Task<int> UserRegister(UserRegisterEntities pUserRegisterDto)
        {
            using (var connection = _configuration.GetConnectionSeguridad)
            {
                const string procedure = "PROC_I_Usuario";
                var parameters = new DynamicParameters();
                parameters.Add("@pUserName", pUserRegisterDto.UserName, DbType.String);
                parameters.Add("@pEmail", pUserRegisterDto.Email, DbType.String);
                parameters.Add("@pPassword", pUserRegisterDto.Password, DbType.String);
                parameters.Add("@pDateCreate", pUserRegisterDto.DateCreate, DbType.DateTime);
                parameters.Add("@pStatus", pUserRegisterDto.Status, DbType.Byte);

                var response = await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<bool> ValidateByUser(string pUser)
        {
            using (var connection = _configuration.GetConnectionSeguridad)
            {
                const string query = @"SELECT 1 
                                       FROM Usuario
                                       WHERE UserName = @pUserName";
                var parameters = new DynamicParameters();
                parameters.Add("@pUserName", pUser, DbType.String);

                var response = await connection.QueryAsync<bool>(query, parameters, commandType: CommandType.Text);
                return response.Any();
            }
        }

        public async Task<bool> ValidateByEmail(string pEmail)
        {
            using (var connection = _configuration.GetConnectionSeguridad)
            {
                const string query = @"SELECT 1 
                                       FROM Usuario
                                       WHERE Email = @pEmail";
                var parameters = new DynamicParameters();
                parameters.Add("@pEmail", pEmail, DbType.String);
                
                var response = await connection.QueryAsync<bool>(query, parameters, commandType: CommandType.Text);
                return response.Any();
            }
        }
    }
}
