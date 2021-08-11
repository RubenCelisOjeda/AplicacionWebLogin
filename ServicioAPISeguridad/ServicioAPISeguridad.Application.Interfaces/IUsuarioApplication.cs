using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Transversal.Common;

namespace ServicioAPISeguridad.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Response<AuthResponseDto> Login(AuthRequestDto authRequestDto);
        Response<bool> ValidateByUser(string pUser);
        Response<UserRegisterDto> UserRegister(UserRegisterDto pUserRegisterDto);
        Response<bool> ValidateByEmail(string pEmail);
    }
}
