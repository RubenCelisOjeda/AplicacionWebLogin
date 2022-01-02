using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace ServicioAPISeguridad.Services.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:Controller
    {
        private readonly IUsuarioApplication _usuarioApplication;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUsuarioApplication usuarioApplication, ILogger<AuthController>  logger)
        {
            _usuarioApplication = usuarioApplication;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto pUthRequest)
        {
            try
            {
                var response = await _usuarioApplication.Login(pUthRequest);
                _logger.LogInformation(response.Message, response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}
