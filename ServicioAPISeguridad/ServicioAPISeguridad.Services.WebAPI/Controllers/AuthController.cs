using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Services.WebAPI.Core;
using ServicioAPISeguridad.Transversal.Common;

namespace ServicioAPISeguridad.Services.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        
        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Route("GenerarToken")]
        [HttpGet]
        public IActionResult GenerarToken([FromBody] AuthRequest authRequest)
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

            //crea el token
            var token = TokenGenerator.CreateToken(_configuration, authRequest.Username);

            return RedirectToAction("Login", "Usuario",new { pPassword = authRequest.Password, pToken = token, pUserName = authRequest.Username });
        }
    }
}
