using SistemaAnalisisVentas.Application.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Helpers
{
    /// <summary>
    /// Permite registrar logs del proceso ETL (errores, advertencias, eventos informativos)
    /// en archivos locales.
    /// </summary>
    public static class LoggerHelper
    {
        private static readonly string LogDirectory = EnvironmentPaths.LogsDirectory;

        /// <summary>
        /// Escribe un mensaje informativo en el log.
        /// </summary>
        public static void Info(string message) => Write("INFO", message);

        /// <summary>
        /// Escribe una advertencia en el log.
        /// </summary>
        public static void Warning(string message) => Write("WARNING", message);

        /// <summary>
        /// Escribe un error en el log.
        /// </summary>
        public static void Error(string message, Exception? ex = null)
        {
            var fullMessage = ex == null ? message : $"{message}\n{ex.Message}\n{ex.StackTrace}";
            Write("ERROR", fullMessage);
        }

        /// <summary>
        /// Método interno para registrar mensajes en un archivo diario.
        /// </summary>
        private static void Write(string level, string message)
        {
            try
            {
                Directory.CreateDirectory(LogDirectory);
                var logFile = Path.Combine(LogDirectory, $"log_{DateTime.UtcNow:yyyyMMdd}.txt");
                var logMessage = $"[{DateTime.UtcNow:HH:mm:ss}] [{level}] {message}";
                File.AppendAllText(logFile, logMessage + Environment.NewLine);
            }
            catch
            {
                // Evita romper el flujo ETL por fallos de log
            }
        }
    }
}
