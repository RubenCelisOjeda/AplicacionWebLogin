using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServicioAPISeguridad.Application.Interfaces;
using ServicioAPISeguridad.Domain.Entities.Sesion;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Domain.Interfaces;
using ServicioAPISeguridad.Transversal.Common;
using System;

namespace ServicioAPISeguridad.Application.Main
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly ILogger<UsuarioApplication> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUsuarioDomain _usuarioDomain;

        public UsuarioApplication(ILogger<UsuarioApplication> logger,IUsuarioDomain usuarioDomain,IConfiguration configuration)
        {
            _usuarioDomain = usuarioDomain;
            _logger = logger;
            _configuration = configuration;
        }

        public Response<UserResponseDto> Login(string pUserName, string pPassword)
        {
            var response = new Response<UserResponseDto>();
            response.IsSuccess = true;
            response.CodigoError = "0";

            try
            {
                //valida el usuario y password
                response.Data = _usuarioDomain.Login(pUserName, pPassword);

                if (response.Data == null)
                {
                    response.IsSuccess = false;
                    response.IsWarning = true;
                    response.CodigoError = "0";
                    return response;
                }

                if (response.IsSuccess)
                    response.Data.Token = JwtGenerator.CreateToken(_configuration, 
                                                                   response.Data.UserName);

                //guardar sesion de usuario
                var sesion = new SesionUsuarioDto
                {
                    IdUser = response.Data.Id,
                    Token = response.Data.Token,
                    DateStart = DateTime.Now,
                    Status = 1
                };
                _usuarioDomain.GuardarSesion(sesion);

            }
            catch (Exception ex)
            {
                response.IsWarning = true;
                response.IsSuccess = false;
                response.Message = "Error";
                _logger.LogError(ex,ex.Message);
            }
            return response;
        }

        public Response<UserRegisterDto> UserRegister(UserRegisterDto pUserRegisterDto)
        {
            var response = new Response<UserRegisterDto>();
            response.IsSuccess = true;
            response.CodigoError = "0";
            response.Data = null;

            try
            {
                if (pUserRegisterDto == null)
                {
                    response.IsSuccess = false;
                    response.IsWarning = true;
                    response.CodigoError = "0";
                    return response;
                }

                //valida el usuario y password
                _usuarioDomain.UserRegister(pUserRegisterDto);
                response.Message = "Registro con exito correctamente.";

            }
            catch (Exception ex)
            {
                response.IsWarning = true;
                response.IsSuccess = false;
                response.Message = "Error";
                _logger.LogError(ex, ex.Message);
            }
            return response;
        }

        public void Prueba()
        {
            _usuarioDomain.Prueba();
        }
    }
}
