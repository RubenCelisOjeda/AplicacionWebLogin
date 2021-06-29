using Dapper;
using ServicioAPISeguridad.Domain.Entities.Sesion;
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

        public void GuardarSesion(SesionUsuarioDto pSesionUsuario)
        {
            using (var connection = _configuration.GetConnectionSeguridad)
            {
                const string procedure = "PROC_I_SesionUsuario";
                var parameters = new DynamicParameters();
                parameters.Add("@pToken", pSesionUsuario.Token, DbType.String);
                parameters.Add("@pIdUser", pSesionUsuario.IdUser, DbType.Int32);
                parameters.Add("@pDateStart", pSesionUsuario.DateStart, DbType.DateTime);
                parameters.Add("@pStatus", pSesionUsuario.Status, DbType.Byte);

                connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public UserResponseDto Login(string pUserName,string pPassword)
        {
            using (var connection = _configuration.GetConnectionSeguridad)
            {
                const string procedure = "PROC_S_Login";
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

        public void UserRegister(UserRegisterDto pUserRegisterDto)
        {
            using (var connection = _configuration.GetConnectionSCM)
            {
                const string procedure = "PROC_I_Usuario";
                var parameters = new DynamicParameters();
                parameters.Add("@pUserName", pUserRegisterDto.UserName, DbType.String);
                parameters.Add("@pEmail", pUserRegisterDto.Password, DbType.String);
                parameters.Add("@pPassword", pUserRegisterDto.Email, DbType.String);
                parameters.Add("@pDateCreate", pUserRegisterDto.DateCreate, DbType.DateTime);
                parameters.Add("@pStatus", pUserRegisterDto.Status, DbType.Byte);

                connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
