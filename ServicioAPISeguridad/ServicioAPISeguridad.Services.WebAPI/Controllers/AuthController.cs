using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Application.Interfaces;
using ServicioAPISeguridad.Services.WebAPI.Core;
using ServicioAPISeguridad.Transversal.Common;

namespace ServicioAPISeguridad.Services.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioApplication _usuarioApplication;

        public AuthController(IConfiguration configuration, IUsuarioApplication usuarioApplication)
        {
            _configuration = configuration;
            _usuarioApplication = usuarioApplication;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] AuthRequest authRequest)
        {
            //valida el modelo
            if (authRequest == null) return BadRequest();

            //valida los datos
            if (string.IsNullOrEmpty(authRequest.Username) || string.IsNullOrEmpty(authRequest.Password))
            {
                return Ok(new Response<AuthResponse>
                {
                    Data = null,
                    CodigoError = Constantes.Error001,
                    IsSuccess = true,
                    IsWarning = true,
                    Message = "Error: No se puede acceder al servicio de seguridad.",
                });
            }

            //valida el login
            var response = _usuarioApplication.Login(authRequest.Username, authRequest.Password);

            if(response.IsSuccess)
                response.Data.Token = TokenGenerator.CreateToken(_configuration, authRequest.Username);

            //crea el token


            return Ok(response);
        }
    }
}
