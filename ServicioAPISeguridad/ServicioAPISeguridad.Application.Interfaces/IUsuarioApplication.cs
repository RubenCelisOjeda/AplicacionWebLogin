using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Transversal.Common;
using System.Threading.Tasks;

namespace ServicioAPISeguridad.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Task<Response<AuthResponseDto>> Login(AuthRequestDto authRequestDto);
        Response<bool> ValidateByUser(string pUser);
        //Response<UserRegisterDto> UserRegister(UserRegisterDto pUserRegisterDto);
        Response<bool> ValidateByEmail(string pEmail);
    }
}
