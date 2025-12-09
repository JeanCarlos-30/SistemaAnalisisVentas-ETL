using SistemaAnalisisVentas.Application.Interfaces;
using SistemaAnalisisVentas.Application.Interfaces.Services;
using SistemaAnalisisVentas.Application.Helpers;
using SistemaAnalisisVentas.Application.Mappers.Dwh;
using SistemaAnalisisVentas.Application.DTOs.Dwh;
using SistemaAnalisisVentas.Domain.Entities.DWH.Facts;

namespace SistemaAnalisisVentas.Application.Services
{
    public class ETLOrchestratorService : IETLOrchestratorService
    {
        private readonly IExtractionService _extractionService;
        private readonly ITransformationService _transformationService;
        private readonly ILoadService _loadService;

        public ETLOrchestratorService(
            IExtractionService extractionService,
            ITransformationService transformationService,
            ILoadService loadService)
        {
            _extractionService = extractionService;
            _transformationService = transformationService;
            _loadService = loadService;
        }

        /// <summary>
        /// Ejecuta el proceso ETL completo (Extract, Transform, Load)
        /// </summary>
        public async Task EjecutarETLAsync()
        {
            LoggerHelper.Info(" Iniciando proceso ETL...");

            try
            {
                // ------------------------------------------
                // 1. EXTRAER DATOS
                // ------------------------------------------
                var extractionResult = await _extractionService.ExtraerAsync();
                LoggerHelper.Info(" Extracción de datos completada.");

                // ------------------------------------------
                // 2. TRANSFORMAR DATOS
                // ------------------------------------------
                LoggerHelper.Info(" Iniciando transformación de datos...");
                await _transformationService.TransformAndValidateAsync();
                await _transformationService.ComputeDerivedValuesAsync();
                LoggerHelper.Info(" Transformación de datos completada.");

                // ------------------------------------------
                // 3. CARGAR DATOS AL DATA WAREHOUSE
                // ------------------------------------------
                LoggerHelper.Info(" Iniciando carga de datos...");
                await _loadService.LoadDimensionsAsync(); // Carga Dimensiones
                await _loadService.LoadFactsAsync(); // Carga de hechos (FactSales)
                await _loadService.RegisterLoadStatusAsync(); // Registra estado en DimSource
                LoggerHelper.Info(" Carga de datos completada.");

                LoggerHelper.Info(" Proceso ETL completado exitosamente.");
            }
            catch (Exception ex)
            {
                LoggerHelper.Error(" Error durante el proceso ETL", ex);
                throw;
            }
        }
    }
}
