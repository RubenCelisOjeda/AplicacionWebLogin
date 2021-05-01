using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServicioAPISeguridad.Infraestructure.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnectionSeguridad { get; }

        IDbConnection GetConnectionVenta { get; }
    }
}
