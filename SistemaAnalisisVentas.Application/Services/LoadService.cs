using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.Helpers;
using SistemaAnalisisVentas.Application.Interfaces;
using SistemaAnalisisVentas.Application.Mappers.Dwh;
using SistemaAnalisisVentas.Application.OperationResults;
using SistemaAnalisisVentas.Domain.Entities.DWH.Dimensions;
using SistemaAnalisisVentas.Domain.Entities.DWH.Facts;
using Microsoft.EntityFrameworkCore;

//namespace SistemaAnalisisVentas.Application.Services
//{
//    /// <summary>
//    /// Servicio encargado de la fase de carga (Load) del proceso ETL.
//    /// Inserta los datos transformados en el Data Warehouse (DWH) de manera optimizada.
//    /// </summary>
//    public class LoadService : ILoadService
//    {
//        private readonly DbContext _context;
//        private const int BatchSize = 1000; // Ajustable según los recursos del servidor

//        public LoadService(DbContext context)
//        {
//            _context = context;
//        }

//        // -------------------------
//        //  1. Cargar Dimensiones
//        // -------------------------
//        public async Task LoadDimensionsAsync()
//        {
//            try
//            {
//                LoggerHelper.Info("Iniciando carga de dimensiones...");

//                // DimCustomer
//                var clientesDto = await ObtenerClientesTransformadosAsync();
//                var clientesDim = clientesDto.Select(DimCustomerMapper.ToEntity).ToList();
//                await CargarPorLotesAsync(clientesDim, "DimCustomer");

//                // DimProduct
//                var productosDto = await ObtenerProductosTransformadosAsync();
//                var productosDim = productosDto.Select(DimProductMapper.ToEntity).ToList();
//                await CargarPorLotesAsync(productosDim, "DimProduct");

//                LoggerHelper.Info("Carga de dimensiones completada exitosamente.");
//            }
//            catch (Exception ex)
//            {
//                LoggerHelper.Error("Error durante la carga de dimensiones.", ex);
//                throw;
//            }
//        }

//        // -------------------------
//        //  2. Cargar Hechos (FactSales)
//        // -------------------------
//        public async Task LoadFactsAsync()
//        {
//            try
//            {
//                LoggerHelper.Info("Iniciando carga de tabla de hechos (FactSales)...");

//                var ventasDto = await ObtenerVentasTransformadasAsync();

//                // Adaptado a los nuevos nombres del DTO
//                var factSales = ventasDto.Select(dto =>
//                FactSalesMapper.ToEntity(
//                dto,
//                dto.OrderID,              // ProductKey o identificador de la venta
//                dto.CustomerID.GetHashCode(), // CustomerKey temporal (si aún no tienes DimCustomer cargado)
//                dto.OrderDate?.DayOfYear ?? 0, // DateKey
//                1                          // SourceKey o BatchKey
//                )
//                ).ToList();

//                await CargarPorLotesAsync(factSales, "FactSales");

//                LoggerHelper.Info("Carga de tabla de hechos completada exitosamente.");
//            }
//            catch (Exception ex)
//            {
//                LoggerHelper.Error("Error durante la carga de hechos.", ex);
//                throw;
//            }
//        }

//        // -------------------------
//        //  3. Registrar Estado (DimSource)
//        // -------------------------
//        public async Task RegisterLoadStatusAsync()
//        {
//            try
//            {
//                LoggerHelper.Info("Registrando estado de carga...");

//                var source = DimSourceMapper.ToEntity("Proceso ETL Ventas", "Batch", "Completado");
//                _context.Set<DimSource>().Add(source);
//                await _context.SaveChangesAsync();

//                LoggerHelper.Info("Estado de carga registrado correctamente.");
//            }
//            catch (Exception ex)
//            {
//                LoggerHelper.Error("Error al registrar el estado de carga.", ex);
//                throw;
//            }
//        }

//        // -------------------------
//        // MÉTODOS AUXILIARES
//        // -------------------------
//        private async Task CargarPorLotesAsync<T>(List<T> entidades, string tabla) where T : class
//        {
//            int total = entidades.Count;
//            int procesados = 0;

//            while (procesados < total)
//            {
//                var lote = entidades.Skip(procesados).Take(BatchSize).ToList();
//                await _context.Set<T>().AddRangeAsync(lote);
//                await _context.SaveChangesAsync();

//                procesados += lote.Count;
//                LoggerHelper.Info($"Cargados {procesados}/{total} registros en {tabla}.");
//            }
//        }

//        // Ejemplos — estos serían reemplazados por los datos del TransformationService
//        private async Task<List<ClienteDTO>> ObtenerClientesTransformadosAsync()
//        {
//            await Task.Delay(100); // Simulación
//            return new List<ClienteDTO>();
//        }

//        private async Task<List<ProductoDTO>> ObtenerProductosTransformadosAsync()
//        {
//            await Task.Delay(100);
//            return new List<ProductoDTO>();
//        }

//        private async Task<List<VentaDTO>> ObtenerVentasTransformadasAsync()
//        {
//            await Task.Delay(100);
//            return new List<VentaDTO>();
//        }
//    }
//}
