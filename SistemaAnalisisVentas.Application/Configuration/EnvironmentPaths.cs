using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Configuration
{
    /// <summary>
    /// Centraliza las rutas de acceso a carpetas y archivos usados durante el proceso ETL.
    /// Evita el uso de rutas fijas en el código y mejora la portabilidad del sistema.
    /// </summary>
    public static class EnvironmentPaths
    {
        /// <summary>
        /// Directorio base del sistema.
        /// </summary>
        public static readonly string BaseDirectory =
            AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Carpeta donde se almacenan los archivos CSV.
        /// </summary>
        public static string CsvDirectory =>
            Path.Combine(BaseDirectory, "Data", "Csv");

        /// <summary>
        /// Carpeta temporal usada durante la transformación de datos.
        /// </summary>
        public static string TempDirectory =>
            Path.Combine(BaseDirectory, "Data", "Temp");

        /// <summary>
        /// Carpeta donde se guardan los logs del proceso ETL.
        /// </summary>
        public static string LogsDirectory =>
            Path.Combine(BaseDirectory, "Logs");

        /// <summary>
        /// Carpeta de salida para reportes generados.
        /// </summary>
        public static string ReportsDirectory =>
            Path.Combine(BaseDirectory, "Reports");

        /// <summary>
        /// Verifica y crea las carpetas necesarias al iniciar el proceso.
        /// </summary>
        public static void EnsureDirectoriesExist()
        {
            Directory.CreateDirectory(CsvDirectory);
            Directory.CreateDirectory(TempDirectory);
            Directory.CreateDirectory(LogsDirectory);
            Directory.CreateDirectory(ReportsDirectory);
        }
    }
}
