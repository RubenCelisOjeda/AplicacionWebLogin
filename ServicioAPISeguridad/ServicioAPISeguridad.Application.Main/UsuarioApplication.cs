using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Application.Dto.Sesion.Request;
using ServicioAPISeguridad.Application.Dto.Usuario.Request;
using ServicioAPISeguridad.Application.Interfaces;
using ServicioAPISeguridad.Domain.Entities.Auth;
using ServicioAPISeguridad.Domain.Entities.Sesion;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Domain.Interfaces;
using ServicioAPISeguridad.Transversal.Common;
using System;
using System.Threading.Tasks;

namespace ServicioAPISeguridad.Application.Main
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly ILogger<UsuarioApplication> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUsuarioDomain _usuarioDomain;
        private readonly IMapper _mapper;

        public UsuarioApplication(ILogger<UsuarioApplication> logger, IMapper mapper, IUsuarioDomain usuarioDomain,IConfiguration configuration)
        {
            _usuarioDomain = usuarioDomain;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Response<AuthResponseDto>> Login(AuthRequestDto authRequestDto)
        {
            AuthRequestEntities authRequest = _mapper.Map<AuthRequestEntities>(authRequestDto);
            var response = new Response<AuthResponseDto>();
            string todo = null;

            try
            {
                if (authRequestDto == null)
                {
                    response.IsSuccess = false;
                    response.IsWarning = true;
                    response.CodigoError = "0";
                    response.Message = "Envio de parametros inválido,intente denuevo.";
                    return response;
                }
                else if (string.IsNullOrEmpty(authRequestDto.Username) ||
                         string.IsNullOrEmpty(authRequestDto.Password))
                {
                    response.IsSuccess = false;
                    response.IsWarning = true;
                    response.CodigoError = "0";
                    response.Message = "Envio de parametros inválido,intente denuevo.";
                    return response;
                }

                //valida el usuario y password
                var responseData = await _usuarioDomain.Login(authRequest);

                response.Data = _mapper.Map<AuthResponseDto>(responseData);
                var ver = Convert.ToInt32(todo);

                if (response.Data == null)
                {
                    response.IsSuccess = false;
                    response.IsWarning = true;
                    response.CodigoError = "0";
                    response.Message = "Usuario y/o contraseña incorrecta.";
                    return response;
                }
                else
                {
                    response.Data.Token = JwtGenerator.CreateToken(_configuration,
                                                                   response.Data.UserName);
                    response.Message = "Inicio de sesión correctamente.";

                    //guardar sesion de usuario
                    var sesion = new SesionUsuarioDto
                    {
                        IdUser = response.Data.Id,
                        Token = response.Data.Token,
                        DateStart = DateTime.Now,
                        Status = 1
                    };
                    var sesionUsuarioEntities = _mapper.Map<SesionUsuarioEntities>(sesion);
                    var responseSesion = await _usuarioDomain.GuardarSesion(sesionUsuarioEntities);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public async Task<Response<int>> UserRegister(UserRegisterRequestDto pUserRegisterDto)
        {
            var response = new Response<int>();

            try
            {
                if (pUserRegisterDto == null)
                {
                    response.IsSuccess = false;
                    response.IsWarning = true;
                    response.CodigoError = "0";
                    response.Message = "Registro no válido,intente de nuevo.";
                    return response;
                }

                //validar el email
                if (this.ValidateByEmail(pUserRegisterDto.Email).Data)
                {
                    response.IsSuccess = false;
                    response.IsWarning = true;
                    response.CodigoError = "0";
                    response.Message = "Ingrese otro email ya esta registrado.";
                    return response;
                }

                var responseUser = await this.ValidateByUser(pUserRegisterDto.UserName);
                if (responseUser.Data)
                {
                    response.IsSuccess = false;
                    response.IsWarning = true;
                    response.CodigoError = "0";
                    response.Message = "Ingrese otro usuario ya esta registrado.";
                    return response;
                }

                //asigna datos
                pUserRegisterDto.DateCreate = DateTime.Now;
                pUserRegisterDto.Status = 1;

                //se mapea los datos
                var userRegisterEntities = _mapper.Map<UserRegisterEntities>(pUserRegisterDto);

                var responseRegister = await _usuarioDomain.UserRegister(userRegisterEntities);
                if (responseRegister == 1)
                {
                    response.Data = responseRegister;
                    response.Message = Constantes.CORRECTO_ADD;
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.IsWarning = true;
                    response.CodigoError = Constantes.Error001;
                    response.Message = Constantes.ERROR_TRANSACCION;
                }
            }
            catch (Exception ex)
            {
                response.IsWarning = true;
                response.IsSuccess = false;
                response.Message = "Error";
                _logger.LogError(ex, ex.Message);
            }
            return response;
        }

        public async Task<Response<bool>> ValidateByUser(string pUser)
        {
            var response = new Response<bool>();
            response.IsWarning = false;
            response.IsSuccess = true;
            response.CodigoError = "0";
            response.Data = false;

            try
            {
                var responseExists = await _usuarioDomain.ValidateByUser(pUser);

                if (responseExists)
                {
                    response.IsWarning = true;
                    response.Data = responseExists;
                    response.Message = "Usuario ingresado ya existe, ingrese otro.";
                }
                else
                {
                    response.Data = responseExists;
                    response.Message = "Usuario ingresado correctamente";
                }
            }
            catch (Exception ex)
            {
                response.IsWarning = true;
                response.IsSuccess = false;
                response.Message = "Error";
                _logger.LogError(ex, ex.Message);
            }
            return response;
        }


        public Response<bool> ValidateByEmail(string pEmail)
        {
            var response = new Response<bool>();
            response.IsWarning = false;
            response.IsSuccess = true;
            response.CodigoError = "0";
            response.Data = false;

            try
            {
                var responseExists = _usuarioDomain.ValidateByEmail(pEmail);
                if (responseExists)
                {
                    response.IsWarning = true;
                    response.Data = responseExists;
                    response.Message = "Email ingresado ya existe, ingrese otro.";
                }
                else
                {
                    response.Data = responseExists;
                    response.Message = "Email ingresado correctamente";
                }
            }
            catch (Exception ex)
            {
                response.IsWarning = true;
                response.IsSuccess = false;
                response.Message = "Error";
                _logger.LogError(ex, ex.Message);
            }
            return response;
        }

    }
}
