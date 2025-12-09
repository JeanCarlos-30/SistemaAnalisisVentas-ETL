using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.DTOs.Api;
using SistemaAnalisisVentas.Application.Interfaces;
using SistemaAnalisisVentas.Application.Interfaces.Repositories.Csv;
using SistemaAnalisisVentas.Application.Interfaces.Services;

namespace SistemaAnalisisVentas.Application.Services
{
    /// <summary>
    /// Servicio encargado de la extracción de datos desde las fuentes (CSV, API, DB)
    /// dentro del flujo ETL del sistema de análisis de ventas.
    /// </summary>
    public class ExtractionService : IExtractionService
    {
        private readonly IClienteCsvRepository _clienteRepo;
        private readonly IProductoCsvRepository _productoRepo;
        private readonly IVentaCsvRepository _ventaRepo;
        private readonly IDetalleVentaCsvRepository _detalleRepo;
        private readonly IFileService _fileService;
        private readonly ILogService _logService;

        public ExtractionService(
            IClienteCsvRepository clienteRepo,
            IProductoCsvRepository productoRepo,
            IVentaCsvRepository ventaRepo,
            IDetalleVentaCsvRepository detalleRepo,
            IFileService fileService,
            ILogService logService)
        {
            _clienteRepo = clienteRepo;
            _productoRepo = productoRepo;
            _ventaRepo = ventaRepo;
            _detalleRepo = detalleRepo;
            _fileService = fileService;
            _logService = logService;
        }

        /// <summary>
        /// Ejecuta el proceso de extracción y retorna los datos al proceso ETL.
        /// </summary>
        public async Task<ExtractionResult> ExtraerAsync()
        {
            try
            {
                await _logService.RegistrarInfoAsync("🔽 Iniciando proceso de extracción desde fuentes CSV...");

                var clientes = await _clienteRepo.LeerClientesAsync();
                var productos = await _productoRepo.LeerProductosAsync();
                var ventas = await _ventaRepo.LeerVentasAsync();
                var detalles = await _detalleRepo.LeerDetallesVentaAsync();

                // Crear directorio temporal si no existe
                await _fileService.CrearDirectorioSiNoExisteAsync("TempData");

                // Guardar JSON temporal (opcional pero útil para debugging)
                await _fileService.GuardarArchivoAsync("TempData/clientes.json",
                    System.Text.Json.JsonSerializer.Serialize(clientes));

                await _fileService.GuardarArchivoAsync("TempData/productos.json",
                    System.Text.Json.JsonSerializer.Serialize(productos));

                await _fileService.GuardarArchivoAsync("TempData/ventas.json",
                    System.Text.Json.JsonSerializer.Serialize(ventas));

                await _fileService.GuardarArchivoAsync("TempData/detalles.json",
                    System.Text.Json.JsonSerializer.Serialize(detalles));

                await _logService.RegistrarInfoAsync("✔ Extracción finalizada. Archivos temporales generados.");


                // 👉 **Retornar data para que TransformationService la procese**
                return new ExtractionResult
                {
                    Clientes = (List<ClienteDTO>)clientes,
                    Productos = (List<ProductoDTO>)productos,
                    Ventas = (List<VentaDTO>)ventas,
                    Detalles = (List<DetalleVentaDTO>)detalles
                };
            }
            catch (Exception ex)
            {
                await _logService.RegistrarErrorAsync("❌ Error durante la extracción.", ex);
                throw;
            }
        }
    }

    /// <summary>
    /// Resultado estructurado para enviar los datos al TransformationService.
    /// </summary>
    public class ExtractionResult
    {
        public List<ClienteDTO> Clientes { get; set; } = new();
        public List<ProductoDTO> Productos { get; set; } = new();
        public List<VentaDTO> Ventas { get; set; } = new();
        public List<DetalleVentaDTO> Detalles { get; set; } = new();
    }
}
