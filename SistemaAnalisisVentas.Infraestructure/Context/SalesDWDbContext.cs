using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SistemaAnalisisVentas.Infrastructure.Context
{
    /// <summary>
    /// Clase responsable de manejar la conexión a la base de datos del Data Warehouse.
    /// Permite abrir y cerrar conexiones SQL de forma segura.
    /// </summary>
    public class SalesDWDbContext : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public SalesDWDbContext(IConfiguration configuration)
        {
            // Lee la cadena de conexión desde appsettings.json -> "ConnectionStrings:DefaultConnection"
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada.");
        }

        /// <summary>
        /// Devuelve una conexión abierta a la base de datos.
        /// Si ya existe una conexión abierta, la reutiliza.
        /// </summary>
        public IDbConnection GetConnection()
        {
            if (_connection == null)
                _connection = new SqlConnection(_connectionString);

            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            return _connection;
        }

        /// <summary>
        /// Cierra la conexión abierta si está activa.
        /// </summary>
        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();
        }

        /// <summary>
        /// Libera los recursos asociados al contexto.
        /// </summary>
        public void Dispose()
        {
            CloseConnection();
            _connection?.Dispose();
        }
    }
}
