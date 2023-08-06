namespace ServicioAPISeguridad.Transversal.Common
{
    public class BaseResponse<T>
    {
        public string CodigoError { get; set; }
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
