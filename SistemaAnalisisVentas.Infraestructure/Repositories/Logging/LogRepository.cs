using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SistemaAnalisisVentas.Infrastructure.Repositories.Logging
{
    public class LogRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<LogRepository> _logger;

        public LogRepository(IConfiguration config, ILogger<LogRepository> logger)
        {
            _connectionString = config.GetConnectionString("DefaultConnection") ?? string.Empty;
            _logger = logger;
        }

        public async Task RegistrarEventoAsync(string tipo, string mensaje, string origen = "WorkerService")
        {
            try
            {
                _logger.LogInformation(" Registrando evento en BD: {Tipo} - {Mensaje}", tipo, mensaje);

                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                string query = @"
                    INSERT INTO LogsETL (Tipo, Mensaje, Origen, FechaRegistro)
                    VALUES (@Tipo, @Mensaje, @Origen, @FechaRegistro)";

                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Tipo", tipo);
                command.Parameters.AddWithValue("@Mensaje", mensaje);
                command.Parameters.AddWithValue("@Origen", origen);
                command.Parameters.AddWithValue("@FechaRegistro", DateTime.UtcNow);

                await command.ExecuteNonQueryAsync();
                _logger.LogInformation("Evento registrado en tabla LogsETL");
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, " Error SQL al registrar evento de log ({Mensaje})", mensaje);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Error inesperado al guardar log en base de datos ({Mensaje})", mensaje);
            }
        }
    }
}
