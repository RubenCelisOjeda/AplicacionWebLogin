using System.Data;

namespace ServicioAPISeguridad.Infraestructure.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnectionSeguridad { get; }

        IDbConnection GetConnectionSCM { get; }
    }
}
