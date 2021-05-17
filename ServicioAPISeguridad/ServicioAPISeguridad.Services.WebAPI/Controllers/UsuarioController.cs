using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Application.Interfaces;

namespace ServicioAPISeguridad.Services.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApplication _usuarioApplication;

        public UsuarioController(IUsuarioApplication usuarioApplication)
        {
            _usuarioApplication = usuarioApplication;
        }

        [Route("Prueba")]
        [HttpPost]
        public IActionResult Login([FromBody] AuthRequest authRequest)
        {
            //valida los datos
            //if (string.IsNullOrEmpty(pUserName) || string.IsNullOrEmpty(pPassword))
            //{
            //    return Ok(new Response<AuthResponse>
            //    {
            //        Data = null,
            //        CodigoError = Constantes.Error002,
            //        IsSuccess = true,
            //        IsWarning = true,
            //        Message = "Contraseña inválida y/o incorrecta",
            //    });
            //}

            //valida la autenticacion

             _usuarioApplication.Prueba();

            return Ok("");
        }
    }
}
