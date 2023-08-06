using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace ServicioAPISeguridad.Services.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:Controller
    {
        private readonly IUsuarioDomain _usuarioApplication;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUsuarioDomain usuarioApplication, ILogger<AuthController>  logger)
        {
            _usuarioApplication = usuarioApplication;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginWeb([FromBody] AuthRequestDto pUthRequest)
        {
            try
            {
                var response = await _usuarioApplication.Login(pUthRequest);
                _logger.LogInformation(response.Message, response);

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}
