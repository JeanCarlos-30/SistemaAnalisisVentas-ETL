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
    /// Implementación del repositorio encargado de leer productos desde archivos CSV.
    /// </summary>
    public class ProductoCsvRepository : IProductoCsvRepository
    {
        private readonly ILogger<ProductoCsvRepository> _logger;
        private readonly string _rutaCsv;

        public ProductoCsvRepository(IConfiguration configuration, ILogger<ProductoCsvRepository> logger)
        {
            _logger = logger;

            // Ruta base del CSV
            var relativePath = configuration["Paths:CsvDirectoryPath"] ?? "Data\\Csv";
            _rutaCsv = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", relativePath));
        }

        /// <summary>
        /// Lee todos los productos desde products.csv
        /// </summary>
        public async Task<IEnumerable<ProductoDTO>> LeerProductosAsync()
        {
            var productos = new List<ProductoDTO>();
            var rutaArchivo = Path.Combine(_rutaCsv, "products.csv");

            try
            {
                _logger.LogInformation("📂 Iniciando lectura de productos desde {Ruta}", rutaArchivo);

                if (!File.Exists(rutaArchivo))
                {
                    _logger.LogError("❌ No se encontró el archivo CSV de productos en {Ruta}", rutaArchivo);
                    return productos;
                }

                using var reader = new StreamReader(rutaArchivo);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var registros = csv.GetRecords<ProductoCsv>().ToList();

                productos = registros.Select(r => new ProductoDTO
                {
                    ProductID = r.ProductID,
                    ProductName = r.ProductName,
                    UnitPrice = r.UnitPrice,
                    UnitsInStock = r.UnitsInStock,
                    FuenteID = null // CSV no trae FuenteID
                }).ToList();

                _logger.LogInformation("✅ Se leyeron {Cantidad} productos desde {Archivo}",
                    productos.Count, Path.GetFileName(rutaArchivo));
            }
            catch (HeaderValidationException ex)
            {
                _logger.LogError(ex, "Error en encabezados del archivo de productos ({Archivo})",
                    Path.GetFileName(rutaArchivo));
            }
            catch (CsvHelper.TypeConversion.TypeConverterException ex)
            {
                _logger.LogError(ex, "Error de conversión de tipo en el archivo de productos ({Archivo})",
                    Path.GetFileName(rutaArchivo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al leer el archivo de productos ({Archivo})",
                    Path.GetFileName(rutaArchivo));
            }

            return await Task.FromResult(productos);
        }
    }
}
