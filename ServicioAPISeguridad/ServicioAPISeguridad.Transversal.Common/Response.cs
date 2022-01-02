namespace ServicioAPISeguridad.Transversal.Common
{
    public class Response<T>
    {
        public Response()
        {
            this.Message = "";
            this.IsSuccess = true;
            this.IsWarning = false;
            this.CodigoError = Constantes.SIN_ERROR;
        }

        public string CodigoError { get; set; }
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsWarning { get; set; }
        public string Message { get; set; }
    }
}
