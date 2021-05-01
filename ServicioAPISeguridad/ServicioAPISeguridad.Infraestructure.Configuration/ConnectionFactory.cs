using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServicioAPISeguridad.Infraestructure.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ServicioAPISeguridad.Infraestructure.Configuration
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ConnectionFactory> _logger;
        private SqlConnection connection = null;

        public ConnectionFactory(IConfiguration configuration, ILogger<ConnectionFactory> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IDbConnection GetConnectionSeguridad
        {
            get
            {
                try
                {
                    using (connection = new SqlConnection())
                    {
                        if (connection == null) return null;
                        connection.ConnectionString = _configuration.GetConnectionString("BDSeguridad");
                        connection.Open();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e,e.Message);
                }
                return connection;
            }
        }

        public IDbConnection GetConnectionVenta
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
