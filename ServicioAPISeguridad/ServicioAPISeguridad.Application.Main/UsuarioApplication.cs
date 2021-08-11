using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServicioAPISeguridad.Application.Dto;
using ServicioAPISeguridad.Application.Dto.Sesion.Request;
using ServicioAPISeguridad.Application.Interfaces;
using ServicioAPISeguridad.Domain.Entities.Auth;
using ServicioAPISeguridad.Domain.Entities.Sesion;
using ServicioAPISeguridad.Domain.Entities.Usuario;
using ServicioAPISeguridad.Domain.Interfaces;
using ServicioAPISeguridad.Transversal.Common;
using System;

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

        public Response<AuthResponseDto> Login(AuthRequestDto authRequestDto)
        {
            AuthRequestEntities authRequest = _mapper.Map<AuthRequestEntities>(authRequestDto);
            var response = new Response<AuthResponseDto>();

            try
            {
                //valida el usuario y password
                response.Data = _mapper.Map<AuthResponseDto>(_usuarioDomain.Login(authRequest));

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
                    _usuarioDomain.GuardarSesion(sesionUsuarioEntities);
                }
            }
            catch (Exception ex)
            {
                response.IsWarning = true;
                response.IsSuccess = false;
                response.Message = "Error no se pudo iniciar sesión,intente de nuevo.";
                _logger.LogError(ex,ex.Message);
            }
            return response;
        }

        public Response<UserRegisterDto> UserRegister(UserRegisterDto pUserRegisterDto)
        {
            var response = new Response<UserRegisterDto>();

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

                //valida el usuario y password
                _usuarioDomain.UserRegister(pUserRegisterDto);
                response.Message = "Registro con exito correctamente.";

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

        public Response<bool> ValidateByUser(string pUser)
        {
            var response = new Response<bool>();
            response.IsWarning = false;
            response.IsSuccess = true;
            response.CodigoError = "0";
            response.Data = false;

            try
            {
                var responseExists = _usuarioDomain.ValidateByUser(pUser);

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
