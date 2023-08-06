namespace ServicioAPISeguridad.Application.Main
{
    //public class UsuarioApplication : IUsuarioApplication
    //{
    //    private readonly ILogger<UsuarioApplication> _logger;
    //    private readonly IConfiguration _configuration;
    //    private readonly IUsuarioDomain _usuarioDomain;
    //    private readonly IMapper _mapper;

    //    public UsuarioApplication(ILogger<UsuarioApplication> logger, IMapper mapper, IUsuarioDomain usuarioDomain,IConfiguration configuration)
    //    {
    //        _usuarioDomain = usuarioDomain;
    //        _mapper = mapper;
    //        _logger = logger;
    //        _configuration = configuration;
    //    }

    //    public async Task<BaseResponse<AuthResponseDto>> Login(AuthRequestDto authRequestDto)
    //    {
    //        AuthRequestEntities authRequest = _mapper.Map<AuthRequestEntities>(authRequestDto);
    //        var response = new BaseResponse<AuthResponseDto>();


    //        try
    //        {
    //            if (authRequestDto == null)
    //            {
    //                response.IsSuccess = false;
    //                response.IsWarning = true;
    //                response.CodigoError = Constantes.CODIGO_WARNING;
    //                response.Message = Constantes.MSG_ENTIDAD_INVALIDA;
    //                return response;
    //            }
    //            else if (string.IsNullOrEmpty(authRequestDto.Username) ||
    //                     string.IsNullOrEmpty(authRequestDto.Password))
    //            {
    //                response.IsSuccess = false;
    //                response.IsWarning = true;
    //                response.CodigoError = "0";
    //                response.Message = "Envio de parametros inválido,intente denuevo.";
    //                return response;
    //            }

    //            //valida el usuario y password
    //            var responseData = await _usuarioDomain.Login(authRequest);

    //            response.Data = _mapper.Map<AuthResponseDto>(responseData);
    //            var ver = Convert.ToInt32(todo);

    //            if (response.Data == null)
    //            {
    //                response.IsSuccess = false;
    //                response.IsWarning = true;
    //                response.CodigoError = "0";
    //                response.Message = "Usuario y/o contraseña incorrecta.";
    //                return response;
    //            }
    //            else
    //            {
    //                response.Data.Token = JwtGenerator.CreateToken(_configuration,
    //                                                               response.Data.UserName);
    //                response.Message = "Inicio de sesión correctamente.";

    //                //guardar sesion de usuario
    //                var sesion = new SesionUsuarioDto
    //                {
    //                    IdUser = response.Data.Id,
    //                    Token = response.Data.Token,
    //                    DateStart = DateTime.Now,
    //                    Status = 1
    //                };
    //                var sesionUsuarioEntities = _mapper.Map<SesionUsuarioEntities>(sesion);
    //                var responseSesion = await _usuarioDomain.GuardarSesion(sesionUsuarioEntities);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //        return response;
    //    }

    //    public async Task<BaseResponse<int>> UserRegister(UserRegisterRequestDto pUserRegisterDto)
    //    {
    //        var response = new BaseResponse<int>();

    //        try
    //        {
    //            //asigna datos
    //            pUserRegisterDto.DateCreate = DateTime.Now;
    //            pUserRegisterDto.Status = 1;

    //            if (pUserRegisterDto == null)
    //            {
    //                response.IsSuccess = false;
    //                response.IsWarning = true;
    //                response.CodigoError = "0";
    //                response.Message = "Registro no válido,intente de nuevo.";
    //                return response;
    //            }

    //            var responseEmail = await this.ValidateByEmail(pUserRegisterDto.Email);
    //            if (responseEmail.Data)
    //            {
    //                response.IsSuccess = false;
    //                response.IsWarning = true;
    //                response.CodigoError = "0";
    //                response.Message = "Ingrese otro email ya esta registrado.";
    //                return response;
    //            }

    //            var responseUser = await this.ValidateByUser(pUserRegisterDto.UserName);
    //            if (responseUser.Data)
    //            {
    //                response.IsSuccess = false;
    //                response.IsWarning = true;
    //                response.CodigoError = "0";
    //                response.Message = "Ingrese otro usuario ya esta registrado.";
    //                return response;
    //            }

    //            //se mapea los datos
    //            var userRegisterEntities = _mapper.Map<UserRegisterEntities>(pUserRegisterDto);

    //            var responseRegister = await _usuarioDomain.UserRegister(userRegisterEntities);
    //            if (responseRegister == 1)
    //            {
    //                response.Data = responseRegister;
    //                response.Message = Constantes.CORRECTO_ADD;
    //                return response;
    //            }
    //            else
    //            {
    //                response.IsSuccess = false;
    //                response.IsWarning = true;
    //                response.CodigoError = Constantes.Error001;
    //                response.Message = Constantes.ERROR_TRANSACCION;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            response.IsWarning = true;
    //            response.IsSuccess = false;
    //            response.Message = "Error";
    //            _logger.LogError(ex, ex.Message);
    //        }
    //        return response;
    //    }

    //    public async Task<BaseResponse<bool>> ValidateByUser(string pUser)
    //    {
    //        var response = new BaseResponse<bool>();
    //        response.IsWarning = false;
    //        response.IsSuccess = true;
    //        response.CodigoError = "0";
    //        response.Data = false;

    //        try
    //        {
    //            var responseExists = await _usuarioDomain.ValidateByUser(pUser);
    //            if (responseExists)
    //            {
    //                response.IsWarning = true;
    //                response.Data = responseExists;
    //                response.Message = "Usuario ingresado ya existe, ingrese otro.";
    //            }
    //            else
    //            {
    //                response.Data = responseExists;
    //                response.Message = "Usuario ingresado correctamente";
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            response.IsWarning = true;
    //            response.IsSuccess = false;
    //            response.Message = "Error";
    //            _logger.LogError(ex, ex.Message);
    //        }
    //        return response;
    //    }


    //    public async Task<BaseResponse<bool>> ValidateByEmail(string pEmail)
    //    {
    //        var response = new BaseResponse<bool>();
    //        response.IsWarning = false;
    //        response.IsSuccess = true;
    //        response.CodigoError = "0";
    //        response.Data = false;

    //        try
    //        {
    //            var responseExists = await _usuarioDomain.ValidateByEmail(pEmail);
    //            if (MetGlo.IsEmail(pEmail))
    //            {
    //                response.IsWarning = false;
    //                response.Data = false;
    //                response.Message = "Email ingresado no tiene el formato correcto.";
    //            }
    //            else if (responseExists)
    //            {
    //                response.IsWarning = true;
    //                response.Data = responseExists;
    //                response.Message = "Email ingresado ya existe, ingrese otro.";
    //            }
    //            else
    //            {
    //                response.IsSuccess = false;
    //                response.Data = responseExists;
    //                response.Message = "Email ingresado no existe.";
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            response.IsWarning = true;
    //            response.IsSuccess = false;
    //            response.Message = "Error";
    //            _logger.LogError(ex, ex.Message);
    //        }
    //        return response;
    //    }

    //    public async Task<BaseResponse<bool>> ValidateRecoveryEmail(string pEmail)
    //    {
    //        var response = new BaseResponse<bool>();
    //        response.IsWarning = false;
    //        response.IsSuccess = true;
    //        response.CodigoError = "0";
    //        response.Data = false;

    //        try
    //        {
    //            var responseExists = await _usuarioDomain.ValidateByEmail(pEmail);
    //            if (MetGlo.IsEmail(pEmail))
    //            {
    //                response.IsWarning = false;
    //                response.Data = false;
    //                response.Message = "Email ingresado no tiene el formato correcto.";
    //            }
    //            else if (responseExists)
    //            {
    //                response.IsWarning = true;
    //                response.Data = responseExists;
    //                response.Message = "Email validado correctamente.";
    //                MetGlo.EnviarCorreo();
    //            }
    //            else
    //            {
    //                response.IsSuccess = false;
    //                response.Data = responseExists;
    //                response.Message = "Email ingresado no existe.";
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            response.IsWarning = true;
    //            response.IsSuccess = false;
    //            response.Message = "Error";
    //            _logger.LogError(ex, ex.Message);
    //        }
    //        return response;
    //    }

    //}
}
