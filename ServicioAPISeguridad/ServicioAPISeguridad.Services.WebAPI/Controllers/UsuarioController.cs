using Microsoft.AspNetCore.Mvc;
using ServicioAPISeguridad.Application.Dto.Usuario.Request;
using ServicioAPISeguridad.Application.Interfaces;
using ServicioAPISeguridad.Transversal.Common;
using System;
using System.Threading.Tasks;

namespace ServicioAPISeguridad.Services.WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApplication _usuarioApplication;

        public UsuarioController(IUsuarioApplication usuarioApplication)
        {
            _usuarioApplication = usuarioApplication;
        }
 
        [HttpPost]
        [Route("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestDto pUserRegister)
        {
            //valida el modelo
            if (pUserRegister == null) return BadRequest();

            //valida los datos
            if (string.IsNullOrEmpty(pUserRegister.Email) || 
                string.IsNullOrEmpty(pUserRegister.Password) ||
                string.IsNullOrEmpty(pUserRegister.UserName) )
            {
                return Ok(new
                {
                    CodigoError = Constantes.Error002,
                    IsSuccess = false,
                    IsWarning = true,
                    Message = "No se pudo realizar el registro,intente denuevo.",
                });
            }

            var response = await _usuarioApplication.UserRegister(pUserRegister);
            return Ok(response);
        }

        [HttpGet]
        [Route("validateByUser/{pUser}")]
        public async Task<IActionResult> ValidateByUser(string pUser)
        {
            //valida el modelo
            if (pUser == null) return BadRequest();

            //valida los datos
            if (string.IsNullOrEmpty(pUser))
            {
                return Ok(new
                {
                    CodigoError = Constantes.Error002,
                    IsSuccess = false,
                    IsWarning = true,
                    Message = "No se pudo realizar el registro,intente denuevo.",
                });
            }

            var response = await _usuarioApplication.ValidateByUser(pUser);
            return Ok(response);
        }

        [HttpGet]
        [Route("validateByEmail/{pEmail}")]
        public IActionResult ValidateByEmail(string pEmail)
        {
            //valida el modelo
            if (pEmail == null) return BadRequest();

            //valida los datos
            if (string.IsNullOrEmpty(pEmail))
            {
                return Ok(new
                {
                    CodigoError = Constantes.Error002,
                    IsSuccess = false,
                    IsWarning = true,
                    Message = "No se pudo realizar el registro,intente denuevo.",
                });
            }

            var response = _usuarioApplication.ValidateByEmail(pEmail);
            return Ok(response);
        }
    }
}
