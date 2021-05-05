using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServicioAPISeguridad.Infraestructure.Interfaces;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ServicioAPISeguridad.Infraestructure.Configuration
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ConnectionFactory> _logger;

        public ConnectionFactory(IConfiguration configuration, ILogger<ConnectionFactory> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }


        public IDbConnection GetConnectionSeguridad
        {
            get { return GetConnection(_configuration.GetConnectionString("BDSeguridad"));}
        }

        public DbConnection GetConnection(string pDataBase)
        {
            var connection = new SqlConnection();

            if (connection == null) return null;

            connection.ConnectionString = pDataBase;
            connection.Open();

            return connection;
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
