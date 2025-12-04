using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SistemaAnalisisVentas.Application.DTOs.Api;
using SistemaAnalisisVentas.Application.Interfaces.Repositories.Csv;
using SistemaAnalisisVentas.Domain.Entities.CSV;
using System.Globalization;

namespace SistemaAnalisisVentas.Infrastructure.Repositories.Csv
{
    /// <summary>
    /// Implementación del repositorio encargado de leer clientes desde archivos CSV.
    /// </summary>
    public class ClienteCsvRepository : IClienteCsvRepository
    {
        private readonly ILogger<ClienteCsvRepository> _logger;
        private readonly string _rutaCsv;

        public ClienteCsvRepository(IConfiguration configuration, ILogger<ClienteCsvRepository> logger)
        {
            _logger = logger;

            // Ruta base definida en appsettings.json
            var relativePath = configuration["Paths:CsvDirectoryPath"] ?? "Data\\Csv";
            _rutaCsv = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", relativePath));
        }

        /// <summary>
        /// Lee todos los registros de clientes desde customers.csv
        /// </summary>
        public async Task<IEnumerable<ClienteDTO>> LeerClientesAsync()
        {
            var clientes = new List<ClienteDTO>();
            var rutaArchivo = Path.Combine(_rutaCsv, "customers.csv");

            try
            {
                _logger.LogInformation(" Iniciando lectura de clientes desde {Ruta}", rutaArchivo);

                if (!File.Exists(rutaArchivo))
                {
                    _logger.LogError(" No se encontró el archivo CSV de clientes en {Ruta}", rutaArchivo);
                    return clientes;
                }

                using var reader = new StreamReader(rutaArchivo);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var registros = csv.GetRecords<ClienteCsv>().ToList();

                clientes = registros.Select(r => new ClienteDTO
                {
                    CustomerID = r.CustomerID,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Email = r.Email,
                    Phone = r.Phone,
                    City = r.City,
                    Country = r.Country,
                    FuenteID = null // CSV no contiene fuente ID
                }).ToList();

                _logger.LogInformation(" Se leyeron {Cantidad} clientes desde {Archivo}",
                    clientes.Count, Path.GetFileName(rutaArchivo));
            }
            catch (HeaderValidationException ex)
            {
                _logger.LogError(ex, "Error en encabezados del archivo de clientes ({Archivo})",
                    Path.GetFileName(rutaArchivo));
            }
            catch (CsvHelper.TypeConversion.TypeConverterException ex)
            {
                _logger.LogError(ex, "Error de conversión de tipo en el archivo de clientes ({Archivo})",
                    Path.GetFileName(rutaArchivo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al leer el archivo de clientes ({Archivo})",
                    Path.GetFileName(rutaArchivo));
            }

            return await Task.FromResult(clientes);
        }
    }
}
