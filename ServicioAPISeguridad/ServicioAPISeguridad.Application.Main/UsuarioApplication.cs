using Microsoft.Extensions.Logging;
using ServicioAPISeguridad.Application.Interfaces;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Domain.Interfaces;
using ServicioAPISeguridad.Transversal.Common;
using System;

namespace ServicioAPISeguridad.Application.Main
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly ILogger<UsuarioApplication> _logger;
        private readonly IUsuarioDomain _usuarioDomain;

        public UsuarioApplication(ILogger<UsuarioApplication> logger,IUsuarioDomain usuarioDomain)
        {
            _usuarioDomain = usuarioDomain;
            _logger = logger;
        }


        public Response<UserResponseDto> Login(string pUserName, string pPassword)
        {
            var response = new Response<UserResponseDto>();
            response.IsSuccess = true;
            response.CodigoError = "0";

            try
            {
                response.Data = _usuarioDomain.Login(pUserName, pPassword);

                if (response.Data == null)
                {
                    response.IsSuccess = false;
                    response.IsWarning = true;
                    response.CodigoError = "0";
                    return response;
                }
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

        public void Prueba()
        {
            _usuarioDomain.Prueba();
        }
    }
}
