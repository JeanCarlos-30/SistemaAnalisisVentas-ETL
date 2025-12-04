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
        private const int BatchSize = 1000;

        public LoadService(DbContext context)
        {
            _context = context;
        }

        // =====================================================
        // 1. CARGA DE DIMENSIONES
        // =====================================================
        public async Task LoadDimensionsAsync()
        {
            try
            {
                LoggerHelper.Info("Iniciando carga de dimensiones...");

                var clientes = await ObtenerClientesTransformadosAsync();
                var productos = await ObtenerProductosTransformadosAsync();
                var fechas = await ObtenerFechasTransformadasAsync();

                await CargarPorLotesAsync(clientes.Select(DimCustomerMapper.ToEntity).ToList(), "DimCustomers");
                await CargarPorLotesAsync(productos.Select(DimProductMapper.ToEntity).ToList(), "DimProducts");
                await CargarPorLotesAsync(fechas.Select(DimDateMapper.ToEntity).ToList(), "DimDate");

                LoggerHelper.Info("Carga de dimensiones completada.");
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("Error durante la carga de dimensiones.", ex);
                throw;
            }
        }

        // =====================================================
        // 2. CARGA DE FACT SALES
        // =====================================================
        public async Task LoadFactsAsync()
        {
            try
            {
                LoggerHelper.Info("Iniciando carga de hechos (FactSales)...");

                var factsDto = await ObtenerFactSalesTransformadosAsync();
                var facts = factsDto.Select(FactSalesMapper.ToEntity).ToList();

                await CargarPorLotesAsync(facts, "FactSales");

                LoggerHelper.Info("Carga de hechos completada.");
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("Error durante la carga de hechos.", ex);
                throw;
            }
        }

        // =====================================================
        // 3. REGISTRO DEL ESTADO (DIM SOURCE)
        // =====================================================
        public async Task RegisterLoadStatusAsync()
        {
            try
            {
                LoggerHelper.Info("Registrando estado de carga...");

                var source = DimSourceMapper.ToEntity("Proceso ETL Ventas", "LOAD", "Completado");
                _context.Set<DimSource>().Add(source);

                await _context.SaveChangesAsync();
                LoggerHelper.Info("Estado de carga registrado.");
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("Error al registrar el estado.", ex);
                throw;
            }
        }

        // =====================================================
        // MÉTODOS AUXILIARES
        // =====================================================
        private async Task CargarPorLotesAsync<T>(List<T> entidades, string tabla) where T : class
        {
            int total = entidades.Count;
            int procesados = 0;

            while (procesados < total)
            {
                var lote = entidades.Skip(procesados).Take(BatchSize).ToList();
                await _context.Set<T>().AddRangeAsync(lote);
                await _context.SaveChangesAsync();

                procesados += lote.Count;
                LoggerHelper.Info($"Cargados {procesados}/{total} registros en {tabla}.");
            }
        }

        // =====================================================
        // ESTAS FUNCIONES SE REEMPLAZAN POR TransformationService
        // =====================================================

        private async Task<List<DimCustomerDTO>> ObtenerClientesTransformadosAsync()
        {
            await Task.Delay(10);
            return new List<DimCustomerDTO>();
        }

        private async Task<List<DimProductDTO>> ObtenerProductosTransformadosAsync()
        {
            await Task.Delay(10);
            return new List<DimProductDTO>();
        }

        private async Task<List<DimDateDTO>> ObtenerFechasTransformadasAsync()
        {
            await Task.Delay(10);
            return new List<DimDateDTO>();
        }

        private async Task<List<FactSalesDTO>> ObtenerFactSalesTransformadosAsync()
        {
            await Task.Delay(10);
            return new List<FactSalesDTO>();
        }
    }
}
