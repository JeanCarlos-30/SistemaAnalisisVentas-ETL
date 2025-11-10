using Microsoft.Extensions.Logging;
using SistemaAnalisisVentas.Application.Interfaces.Services;

namespace SistemaAnalisisVentas.Infrastructure.Services
{
    /// <summary>
    /// Implementación del servicio de logging que registra información,
    /// advertencias y errores del proceso ETL en consola o archivos según configuración.
    /// </summary>
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;

        public LogService(ILogger<LogService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Registra un mensaje informativo.
        /// </summary>
        public async Task RegistrarInfoAsync(string mensaje)
        {
            try
            {
                _logger.LogInformation(" {Mensaje}", mensaje);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al registrar mensaje informativo: {Mensaje}", mensaje);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Registra una advertencia.
        /// </summary>
        public async Task RegistrarAdvertenciaAsync(string mensaje)
        {
            try
            {
                _logger.LogWarning(" {Mensaje}", mensaje);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al registrar advertencia: {Mensaje}", mensaje);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Registra un error con su excepción asociada.
        /// </summary>
        public async Task RegistrarErrorAsync(string mensaje, Exception? excepcion = null)
        {
            try
            {
                if (excepcion is null)
                    _logger.LogError(" {Mensaje}", mensaje);
                else
                    _logger.LogError(excepcion, " {Mensaje}", mensaje);
            }
            catch
            {
                // En caso de fallo del logger, usar salida de consola como respaldo.
                Console.WriteLine($"[LOG FALLBACK] {mensaje} - {excepcion?.Message}");
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Registra el inicio o fin de una operación ETL.
        /// </summary>
        public async Task RegistrarOperacionAsync(string operacion, bool inicio)
        {
            var estado = inicio ? "▶️ Iniciando" : " Finalizando";
            var mensaje = $"{estado} operación: {operacion}";

            try
            {
                _logger.LogInformation("{Mensaje}", mensaje);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar operación {Operacion}", operacion);
            }

            await Task.CompletedTask;
        }
    }
}
