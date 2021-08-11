using Microsoft.AspNetCore.Mvc;
using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Application.Interfaces;
using ServicioAPISeguridad.Transversal.Common;

namespace ServicioAPISeguridad.Services.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:Controller
    {
        private readonly IUsuarioApplication _usuarioApplication;

        public AuthController(IUsuarioApplication usuarioApplication)
        {
            _usuarioApplication = usuarioApplication;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] AuthRequestDto pUthRequest)
        {
            //valida el modelo
            if (pUthRequest == null) return BadRequest();

            //valida los datos
            if (string.IsNullOrEmpty(pUthRequest.Username) || 
                string.IsNullOrEmpty(pUthRequest.Password))
            {
                return Ok(new 
                {
                    CodigoError = Constantes.Error001,
                    IsSuccess = false,
                    IsWarning = true,
                    Message = "Error: No se puede acceder al servicio de seguridad.",
                });
            }

            //valida el login
            var response = _usuarioApplication.Login(pUthRequest.Username, pUthRequest.Password);

            //crea el token
            return Ok(response);
        }
    }
}
