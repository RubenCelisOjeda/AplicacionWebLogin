using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Application.Dto.Usuario.Request;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Transversal.Common;
using System.Threading.Tasks;

namespace ServicioAPISeguridad.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Task<Response<AuthResponseDto>> Login(AuthRequestDto authRequestDto);
        Response<bool> ValidateByUser(string pUser);
        Response<int> UserRegister(UserRegisterRequestDto pUserRegisterDto);
        Response<bool> ValidateByEmail(string pEmail);
    }
}
