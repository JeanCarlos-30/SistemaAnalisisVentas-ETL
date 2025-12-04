using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.Interfaces.Repositories.Csv;
using SistemaAnalisisVentas.Domain.Entities.CSV;
using System.Globalization;

namespace SistemaAnalisisVentas.Infrastructure.Repositories.Csv
{
    /// <summary>
    /// Implementación del repositorio encargado de leer ventas desde archivos CSV.
    /// </summary>
    public class VentaCsvRepository : IVentaCsvRepository
    {
        private readonly ILogger<VentaCsvRepository> _logger;
        private readonly string _rutaCsv;

        public VentaCsvRepository(IConfiguration configuration, ILogger<VentaCsvRepository> logger)
        {
            _logger = logger;

            var relativePath = configuration["Paths:CsvDirectoryPath"] ?? "Data\\Csv";
            _rutaCsv = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", relativePath));
        }

        /// <summary>
        /// Lee todas las ventas desde sales.csv
        /// </summary>
        public async Task<IEnumerable<VentaDTO>> LeerVentasAsync()
        {
            var ventas = new List<VentaDTO>();
            var rutaArchivo = Path.Combine(_rutaCsv, "sales.csv");

            try
            {
                _logger.LogInformation(" Iniciando lectura de ventas desde {Ruta}", rutaArchivo);

                if (!File.Exists(rutaArchivo))
                {
                    _logger.LogError(" No se encontró el archivo CSV de ventas en {Ruta}", rutaArchivo);
                    return ventas;
                }

                using var reader = new StreamReader(rutaArchivo);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var registros = csv.GetRecords<VentaCsv>().ToList();

                ventas = registros.Select(r => new VentaDTO
                {
                    OrderID = r.OrderID,
                    CustomerID = r.CustomerID,
                    OrderDate = r.OrderDate,
                    ShipCountry = r.ShipCountry,
                    FuenteID = null // dato no proviene del CSV
                }).ToList();

                _logger.LogInformation(" Se leyeron {Cantidad} ventas desde {Archivo}",
                    ventas.Count, Path.GetFileName(rutaArchivo));
            }
            catch (HeaderValidationException ex)
            {
                _logger.LogError(ex, "Error en encabezados del archivo de ventas ({Archivo})",
                    Path.GetFileName(rutaArchivo));
            }
            catch (CsvHelper.TypeConversion.TypeConverterException ex)
            {
                _logger.LogError(ex, "Error de conversión de tipo en el archivo de ventas ({Archivo})",
                    Path.GetFileName(rutaArchivo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al leer el archivo de ventas ({Archivo})",
                    Path.GetFileName(rutaArchivo));
            }

            return await Task.FromResult(ventas);
        }
    }
}
