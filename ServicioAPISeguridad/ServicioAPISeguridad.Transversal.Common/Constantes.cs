namespace ServicioAPISeguridad.Transversal.Common
{
    public static class Constantes
    {
        // No se puede acceder al servicio por valifacion o falta de datos. Error no controlado.
        public const string Error001 = "1";
        public const string SIN_ERROR = "0";

        // Error no controlado,Contraseña inválida y/o incorrecta.
        public const string Error002 = "2";

        public const string CORRECTO_SELECT = "Se consulta correctamente.";
        public const string CORRECTO_ADD = "Se guardo correctamente.";
        public const string CORRECTO_UPDATE = "Se actualizo correctamente.";
        public const string ERROR_TRANSACCION = "No se puedo ejecutar la instrucción.";


        //email
        public const string EMAIL_SERVIDOR = "sistemas.celis@gmail.com";
        public const string EMAIL_SERVIDOR_PASSWORD = "codigolinux89$123";
    }
}
