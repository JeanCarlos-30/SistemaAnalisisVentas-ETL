using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SistemaAnalisisVentas.Infrastructure.Repositories.Db
{
    public class DatabaseRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<DatabaseRepository> _logger;

        public DatabaseRepository(IConfiguration config, ILogger<DatabaseRepository> logger)
        {
            _connectionString = config.GetConnectionString("DefaultConnection") ?? string.Empty;
            _logger = logger;
        }

        public async Task<DataTable> EjecutarConsultaAsync(string query)
        {
            var tabla = new DataTable();

            try
            {
                _logger.LogInformation(" Ejecutando consulta SQL: {Query}", query);

                if (string.IsNullOrWhiteSpace(query))
                {
                    _logger.LogWarning(" La consulta SQL está vacía o nula.");
                    return tabla;
                }

                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand(query, connection);
                using var reader = await command.ExecuteReaderAsync();

                tabla.Load(reader);

                _logger.LogInformation(" Consulta completada: {Filas} filas obtenidas.", tabla.Rows.Count);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, " Error de SQL al ejecutar la consulta: {Query}", query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Error inesperado al ejecutar la consulta SQL: {Query}", query);
            }

            return tabla;
        }
    }
}
