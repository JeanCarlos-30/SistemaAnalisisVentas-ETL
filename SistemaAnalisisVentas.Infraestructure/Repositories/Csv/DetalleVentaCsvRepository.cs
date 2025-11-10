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
    /// Implementación del repositorio encargado de leer detalles de venta desde archivos CSV.
    /// </summary>
    public class DetalleVentaCsvRepository : IDetalleVentaCsvRepository
    {
        private readonly ILogger<DetalleVentaCsvRepository> _logger;
        private readonly string _rutaCsv;

        public DetalleVentaCsvRepository(IConfiguration configuration, ILogger<DetalleVentaCsvRepository> logger)
        {
            _logger = logger;

            // ✅ Lee la ruta base del appsettings.json
            var relativePath = configuration["Paths:CsvDirectoryPath"] ?? "Data\\Csv";
            _rutaCsv = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", relativePath));
        }

        /// <summary>
        /// Lee todos los registros de detalles de venta desde el archivo CSV.
        /// </summary>
        /// <returns>Una lista de objetos DetalleVentaDTO.</returns>
        public async Task<IEnumerable<DetalleVentaDTO>> LeerDetallesVentaAsync()
        {
            var detalles = new List<DetalleVentaDTO>();
            var rutaArchivo = Path.Combine(_rutaCsv, "sales_details.csv");

            try
            {
                _logger.LogInformation("📂 Iniciando lectura de detalles de venta desde {Ruta}", rutaArchivo);

                if (!File.Exists(rutaArchivo))
                {
                    _logger.LogError("❌ No se encontró el archivo CSV de detalles de venta en {Ruta}", rutaArchivo);
                    return detalles;
                }

                using var reader = new StreamReader(rutaArchivo);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var registros = csv.GetRecords<DetalleVentaCsv>().ToList();

                detalles = registros.Select(r => new DetalleVentaDTO
                {
                    OrderID = r.OrderID,
                    ProductID = r.ProductID,
                    UnitPrice = r.UnitPrice,
                    Quantity = r.Quantity,
                    Discount = r.Discount,
                    Subtotal = r.Subtotal, // ya calculado en la entidad
                    FuenteOrigen = "CSV",
                    FechaActualizacion = DateTime.Now
                }).ToList();

                _logger.LogInformation("✅ Se leyeron {Cantidad} detalles de venta desde {Archivo}",
                    detalles.Count, Path.GetFileName(rutaArchivo));
            }
            catch (HeaderValidationException ex)
            {
                _logger.LogError(ex, "Error en encabezados del archivo de detalles ({Archivo})", Path.GetFileName(rutaArchivo));
            }
            catch (CsvHelper.TypeConversion.TypeConverterException ex)
            {
                _logger.LogError(ex, "Error de conversión de tipo en el archivo de detalles ({Archivo})", Path.GetFileName(rutaArchivo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al leer el archivo de detalles ({Archivo})", Path.GetFileName(rutaArchivo));
            }

            return await Task.FromResult(detalles);
        }
    }
}
