using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Application.Dto.Usuario.Request;
using ServicioAPISeguridad.Transversal.Common;
using System.Threading.Tasks;

namespace ServicioAPISeguridad.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Task<BaseResponse<AuthResponseDto>> Login(AuthRequestDto authRequestDto);
        Task<BaseResponse<bool>> ValidateByUser(string pUser);
        Task<BaseResponse<int>> UserRegister(UserRegisterRequestDto pUserRegisterDto);
        Task<BaseResponse<bool>> ValidateByEmail(string pEmail);
    }
}
