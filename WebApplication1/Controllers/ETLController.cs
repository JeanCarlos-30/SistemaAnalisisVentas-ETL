using Microsoft.AspNetCore.Mvc;
using SistemaAnalisisVentas.Application.Interfaces;
using SistemaAnalisisVentas.Application.Helpers;

namespace AnalisisVentas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ETLController : ControllerBase
    {
        private readonly IExtractionService _extractionService;
        private readonly ITransformationService _transformationService;
        private readonly ILoadService _loadService;

        public ETLController(
            IExtractionService extractionService,
            ITransformationService transformationService,
            ILoadService loadService)
        {
            _extractionService = extractionService;
            _transformationService = transformationService;
            _loadService = loadService;
        }

        // ======================================================================
        // 1. EXTRACT
        // ======================================================================
        [HttpPost("extract")]
        public async Task<IActionResult> Extract()
        {
            LoggerHelper.Info(" Ejecutando EXTRACT desde API...");

            await _extractionService.ExtraerAsync();

            return Ok(new
            {
                Message = " Extracción completada correctamente.",
                Timestamp = DateTime.UtcNow
            });
        }

        // ======================================================================
        // 2. TRANSFORM
        // ======================================================================
        [HttpPost("transform")]
        public async Task<IActionResult> Transform()
        {
            LoggerHelper.Info(" Ejecutando TRANSFORM desde API...");

            await _transformationService.TransformAndValidateAsync();
            await _transformationService.ComputeDerivedValuesAsync();

            return Ok(new
            {
                Message = " Transformación completada correctamente.",
                Timestamp = DateTime.UtcNow
            });
        }

        // ======================================================================
        // 3. LOAD
        // ======================================================================
        [HttpPost("load")]
        public async Task<IActionResult> Load()
        {
            LoggerHelper.Info(" Ejecutando LOAD desde API...");

            await _loadService.LoadDimensionsAsync();
            await _loadService.LoadFactsAsync();

            return Ok(new
            {
                Message = " Carga al Data Warehouse completada.",
                Timestamp = DateTime.UtcNow
            });
        }

        // ======================================================================
        // 4. PROCESO COMPLETO (ETL COMPLETO)
        // ======================================================================
        [HttpPost("run")]
        public async Task<IActionResult> Run()
        {
            LoggerHelper.Info(" Ejecutando PROCESO ETL COMPLETO...");

            await _extractionService.ExtraerAsync();
            await _transformationService.TransformAndValidateAsync();
            await _transformationService.ComputeDerivedValuesAsync();
            await _loadService.LoadDimensionsAsync();
            await _loadService.LoadFactsAsync();

            return Ok(new
            {
                Message = " Proceso ETL completo ejecutado exitosamente.",
                Timestamp = DateTime.UtcNow
            });
        }
    }
}
