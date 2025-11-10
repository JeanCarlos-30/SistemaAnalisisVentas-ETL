using Microsoft.Extensions.Configuration;

namespace SistemaAnalisisVentas.Infrastructure.Configuration
{
    /// <summary>
    /// Contiene las configuraciones generales de la capa de infraestructura,
    /// como rutas de archivos, cadenas de conexión y endpoints externos.
    /// </summary>
    public class InfrastructureSettings
    {
        public string ConnectionString { get; private set; }
        public string CsvDirectoryPath { get; private set; }
        public string ApiBaseUrl { get; private set; }

        public InfrastructureSettings(IConfiguration configuration)
        {
            // Carga los valores del appsettings.json o de variables de entorno
            ConnectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'.");

            CsvDirectoryPath = configuration["Paths:CsvDirectoryPath"]
                ?? throw new InvalidOperationException("No se encontró la ruta de archivos CSV en la configuración.");

            ApiBaseUrl = configuration["ApiSettings:BaseUrl"]
                ?? throw new InvalidOperationException("No se encontró la URL base de la API en la configuración.");
        }
    }
}
