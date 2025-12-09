using SistemaAnalisisVentas.Application.DTOs.Dwh;
using SistemaAnalisisVentas.Application.Helpers;
using SistemaAnalisisVentas.Application.Interfaces;
using SistemaAnalisisVentas.Application.Mappers.Dwh;
using SistemaAnalisisVentas.Domain.Entities.DWH.Dimensions;
using SistemaAnalisisVentas.Domain.Entities.DWH.Facts;
using Microsoft.EntityFrameworkCore;

namespace SistemaAnalisisVentas.Application.Services
{
    public class LoadService : ILoadService
    {
        private readonly DbContext _context;
        private readonly ITransformationService _transformService;
        private const int BatchSize = 1000;

        public LoadService(DbContext context, ITransformationService transformService)
        {
            _context = context;
            _transformService = transformService;
        }

        // ============================================================
        // 🔹 1. CARGA DE DIMENSIONES: CUSTOMER, PRODUCT, DATE
        // ============================================================
        public async Task LoadDimensionsAsync()
        {
            try
            {
                LoggerHelper.Info("=== Iniciando carga de Dimensiones ===");

                // ---------------- DIM CUSTOMER ----------------
                var dimCustomers = await _transformService.TransformarClientesAsync();
                await CargarPorLotesAsync(
                    dimCustomers.Select(DimCustomerMapper.ToEntity).ToList(),
                    nameof(DimCustomer)
                );

                // ---------------- DIM PRODUCT ----------------
                var dimProducts = await _transformService.TransformarProductosAsync();
                await CargarPorLotesAsync(
                    dimProducts.Select(DimProductMapper.ToEntity).ToList(),
                    nameof(DimProduct)
                );

                // ---------------- DIM DATE ----------------
                var fechaMin = new DateTime(2020, 1, 1);
                var fechaMax = DateTime.UtcNow.AddYears(1);
                var dimDates = _transformService.GenerarDimDate(fechaMin, fechaMax);

                await CargarPorLotesAsync(
                    dimDates.Select(DimDateMapper.ToEntity).ToList(),
                    nameof(DimDate)
                );

                LoggerHelper.Info("✔ Dimensiones cargadas exitosamente.");
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("❌ Error durante la carga de dimensiones", ex);
                throw;
            }
        }

        // ============================================================
        // 🔹 2. CARGA DE FACT SALES
        // ============================================================
        public async Task LoadFactsAsync()
        {
            try
            {
                LoggerHelper.Info("=== Iniciando carga de FACT SALES ===");

                var factDtos = await _transformService.TransformarVentasAsync();
                var facts = factDtos.Select(FactSalesMapper.ToEntity).ToList();

                await CargarPorLotesAsync(facts, nameof(FactSales));

                LoggerHelper.Info("✔ FactSales cargadas correctamente.");
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("❌ Error durante la carga de FactSales", ex);
                throw;
            }
        }

        // ============================================================
        // 🔹 3. REGISTRO EN DIM SOURCE
        // ============================================================
        public async Task RegisterLoadStatusAsync()
        {
            try
            {
                LoggerHelper.Info("Registrando estado de carga en DimSource...");

                var registro = DimSourceMapper.ToEntity(
                    "ETL Ventas",
                    "LOAD",
                    "Completado"
                );

                _context.Set<DimSource>().Add(registro);
                await _context.SaveChangesAsync();

                LoggerHelper.Info("✔ Estado de carga registrado correctamente.");
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("❌ Error al registrar DimSource", ex);
                throw;
            }
        }

        // ============================================================
        // 🔹 MÉTODO DE CARGA POR LOTES
        // ============================================================
        private async Task CargarPorLotesAsync<T>(List<T> entidades, string nombreTabla) where T : class
        {
            if (entidades == null || entidades.Count == 0)
            {
                LoggerHelper.Warning($"⚠ No hay registros para insertar en {nombreTabla}");
                return;
            }

            LoggerHelper.Info($"Iniciando carga por lotes → {nombreTabla} ({entidades.Count} registros)");

            int total = entidades.Count;
            int procesados = 0;

            while (procesados < total)
            {
                var lote = entidades.Skip(procesados).Take(BatchSize).ToList();

                await _context.Set<T>().AddRangeAsync(lote);
                await _context.SaveChangesAsync();

                procesados += lote.Count;
                LoggerHelper.Info($"✔ {procesados}/{total} registros cargados en {nombreTabla}");
            }

            LoggerHelper.Info($"✔ Carga finalizada en {nombreTabla}");
        }
    }
}
