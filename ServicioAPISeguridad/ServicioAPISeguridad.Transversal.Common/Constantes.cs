namespace ServicioAPISeguridad.Transversal.Common
{
    public static class Constantes
    {
        //Codigos de respuesta.
        public const string CODIGO_SUCCESS = "0";
        public const string CODIGO_WARNING = "2";
        public const string CODIGO_ERROR = "1";

        //Mensajes de operacion
        public const string MSG_OPERATION_SUCCESS = "Se ejecuto correctamente.";
        public const string MSG_OPERATION_WARNING = "No se pudo ejecutar la operacion correctamente.";
        public const string MSG_OPERATION_ERROR = "Error al ejecutar la operacion.";

        //Mensajes de validacion
        public const string MSG_ENTIDAD_INVALIDA = "Entidad inválida,intente denuevo.";
        public const string MSG_PARAMETRO_INVALIDO = "Envio de parámetros inválido,intente denuevo.";

        public const bool SUCCESS = true;
        public const bool ERROR = false;

        //email
        public const string EMAIL_SERVIDOR = "sistemas.celis@gmail.com";
        public const string EMAIL_SERVIDOR_PASSWORD = "codigolinux89$123";
    }
}
