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

        public async Task ExtraerAsync()
        {
            try
            {
                await _logService.RegistrarInfoAsync("Iniciando proceso de extracción desde fuentes CSV...");

                var clientes = await _clienteRepo.LeerClientesAsync();
                var productos = await _productoRepo.LeerProductosAsync();
                var ventas = await _ventaRepo.LeerVentasAsync();
                var detalles = await _detalleRepo.LeerDetallesVentaAsync();

                await _fileService.CrearDirectorioSiNoExisteAsync("TempData");

                await _fileService.GuardarArchivoAsync("TempData/clientes.json", System.Text.Json.JsonSerializer.Serialize(clientes));
                await _fileService.GuardarArchivoAsync("TempData/productos.json", System.Text.Json.JsonSerializer.Serialize(productos));
                await _fileService.GuardarArchivoAsync("TempData/ventas.json", System.Text.Json.JsonSerializer.Serialize(ventas));
                await _fileService.GuardarArchivoAsync("TempData/detalles.json", System.Text.Json.JsonSerializer.Serialize(detalles));

                await _logService.RegistrarInfoAsync("Extracción completada. Archivos temporales generados correctamente.");
            }
            catch (Exception ex)
            {
                await _logService.RegistrarErrorAsync("Error inesperado durante el proceso de extracción.", ex);
            }
        }
    }
}


