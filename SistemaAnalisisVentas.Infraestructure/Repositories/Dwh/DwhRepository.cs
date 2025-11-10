using Microsoft.Extensions.Logging;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SistemaAnalisisVentas.Infrastructure.Repositories.Dwh
{
    public class DwhRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<DwhRepository> _logger;

        public DwhRepository(IConfiguration config, ILogger<DwhRepository> logger)
        {
            _connectionString = config.GetConnectionString("DataWarehouseConnection") ?? string.Empty;
            _logger = logger;
        }

        public async Task<bool> InsertarDatosAsync(string tablaDestino, DataTable datos)
        {
            try
            {
                _logger.LogInformation("Iniciando inserción en DWH: {Tabla}", tablaDestino);

                if (string.IsNullOrWhiteSpace(tablaDestino))
                {
                    _logger.LogWarning(" Nombre de tabla destino no especificado.");
                    return false;
                }

                if (datos == null || datos.Rows.Count == 0)
                {
                    _logger.LogWarning(" No hay datos para insertar en {Tabla}", tablaDestino);
                    return false;
                }

                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var bulkCopy = new SqlBulkCopy(connection)
                {
                    DestinationTableName = tablaDestino,
                    BatchSize = 1000
                };

                await bulkCopy.WriteToServerAsync(datos);
                _logger.LogInformation(" Inserción completada: {Filas} filas cargadas en {Tabla}",
                    datos.Rows.Count, tablaDestino);

                return true;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, " Error de SQL durante la inserción en {Tabla}", tablaDestino);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Error inesperado durante la carga en DWH ({Tabla})", tablaDestino);
            }

            return false;
        }
    }
}

