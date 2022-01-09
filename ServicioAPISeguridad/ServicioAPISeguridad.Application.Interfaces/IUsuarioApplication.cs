using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Application.Dto.Usuario.Request;
using ServicioAPISeguridad.Transversal.Common;
using System.Threading.Tasks;

namespace ServicioAPISeguridad.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Task<Response<AuthResponseDto>> Login(AuthRequestDto authRequestDto);
        Task<Response<bool>> ValidateByUser(string pUser);
        Task<Response<int>> UserRegister(UserRegisterRequestDto pUserRegisterDto);
        Response<bool> ValidateByEmail(string pEmail);
    }
}
