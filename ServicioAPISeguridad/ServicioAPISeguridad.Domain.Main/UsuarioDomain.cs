using Microsoft.Extensions.Configuration;
using ServicioAPISeguridad.Domain.Entities.Auth;
using ServicioAPISeguridad.Domain.Entities.Sesion;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Infraestructure.Interfaces;
using ServicioAPISeguridad.Transversal.Common;
using System;
using System.Threading.Tasks;

namespace ServicioAPISeguridad.Domain.Main
{
    public class UsuarioDomain : IUsuarioRepository
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuarioDomain(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<int> GuardarSesion(SesionUsuarioEntities pSesionUsuario)
        {
            var response = await _usuarioRepository.GuardarSesion(pSesionUsuario);
            return response;
        }

        public async Task<BaseResponse<UserResponseEntities>> Login(AuthRequestEntities authRequestEntities)
        {
            var response = new BaseResponse<UserResponseEntities>();

            try
            {
                if (authRequestEntities == null)
                {
                    response.IsSuccess = Constantes.ERROR;
                    response.CodigoError = Constantes.CODIGO_WARNING;
                    response.Message = Constantes.MSG_OPERATION_WARNING;
                    return response;
                }
                else if (string.IsNullOrEmpty(authRequestEntities.Username) ||
                         string.IsNullOrEmpty(authRequestEntities.Password))
                {
                    response.IsSuccess = Constantes.ERROR;
                    response.CodigoError = Constantes.CODIGO_WARNING;
                    response.Message = Constantes.MSG_OPERATION_WARNING;
                    return response;
                }

                //valida el usuario y password
                var responseData = await _usuarioRepository.Login(authRequestEntities);

                response.Data = responseData.Data;

                if (response.Data == null)
                {
                    response.IsSuccess = Constantes.ERROR;
                    response.CodigoError = Constantes.CODIGO_WARNING;
                    response.Message = Constantes.MSG_OPERATION_WARNING;
                    return response;
                }
                else
                {
                    response.Data.Token = JwtGenerator.CreateToken(_configuration,
                                                                   response.Data.UserName);
                    //guardar sesion de usuario
                    var sesion = new SesionUsuarioEntities
                    {
                        IdUser = response.Data.Id,
                        Token = response.Data.Token,
                        DateStart = DateTime.Now,
                        Status = 1
                    };
                    var responseSesion = await _usuarioRepository.GuardarSesion(sesion);

                    response.IsSuccess = Constantes.SUCCESS;
                    response.CodigoError = Constantes.CODIGO_SUCCESS;
                    response.Message = Constantes.MSG_OPERATION_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        //public async Task<int> UserRegister(UserRegisterEntities pUserRegisterEntities)
        //{
        //    var response = new BaseResponse<int>();

        //    try
        //    {
        //        //asigna datos
        //        pUserRegisterDto.DateCreate = DateTime.Now;
        //        pUserRegisterDto.Status = 1;

        //        if (pUserRegisterDto == null)
        //        {
        //            response.IsSuccess = false;
        //            response.IsWarning = true;
        //            response.CodigoError = "0";
        //            response.Message = "Registro no válido,intente de nuevo.";
        //            return response;
        //        }

        //        var responseEmail = await this.ValidateByEmail(pUserRegisterDto.Email);
        //        if (responseEmail.Data)
        //        {
        //            response.IsSuccess = false;
        //            response.IsWarning = true;
        //            response.CodigoError = "0";
        //            response.Message = "Ingrese otro email ya esta registrado.";
        //            return response;
        //        }

        //        var responseUser = await this.ValidateByUser(pUserRegisterDto.UserName);
        //        if (responseUser.Data)
        //        {
        //            response.IsSuccess = false;
        //            response.IsWarning = true;
        //            response.CodigoError = "0";
        //            response.Message = "Ingrese otro usuario ya esta registrado.";
        //            return response;
        //        }

        //        //se mapea los datos
        //        var userRegisterEntities = _mapper.Map<UserRegisterEntities>(pUserRegisterDto);

        //        var responseRegister = await _usuarioDomain.UserRegister(userRegisterEntities);
        //        if (responseRegister == 1)
        //        {
        //            response.Data = responseRegister;
        //            response.Message = Constantes.CORRECTO_ADD;
        //            return response;
        //        }
        //        else
        //        {
        //            response.IsSuccess = false;
        //            response.IsWarning = true;
        //            response.CodigoError = Constantes.Error001;
        //            response.Message = Constantes.ERROR_TRANSACCION;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsWarning = true;
        //        response.IsSuccess = false;
        //        response.Message = "Error";
        //        _logger.LogError(ex, ex.Message);
        //    }
        //    return response;
        //}

        ////public async Task<bool> ValidateByUser(string pUser)
        ////{
        ////    var response = await _usuarioRepository.ValidateByUser(pUser);
        ////    return response;
        ////}

        ////public async Task<bool>ValidateByEmail(string pEmail)
        ////{
        ////    var response = await _usuarioRepository.ValidateByEmail(pEmail);
        ////    return response;
        ////}
    }
}
