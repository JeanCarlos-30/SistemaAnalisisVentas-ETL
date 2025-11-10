using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SistemaAnalisisVentas.Application.Interfaces;

namespace SistemaAnalisisVentas.DWH
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        private const int IntervaloEjecucionSegundos = 10; //  tiempo entre ejecuciones

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(" Servicio ETL iniciado a las {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation(" Iniciando proceso ETL a las {time}", DateTimeOffset.Now);

                    //  Crear un scope para obtener servicios scoped
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var extractionService = scope.ServiceProvider.GetRequiredService<IExtractionService>();

                        await extractionService.ExtraerAsync();

                        // Verificar si se generaron los JSON
                        string outputPath = @"C:\Users\terre\OneDrive\Documentos\6 Cuatrimestre\Electiva 1 BIG DATA\SistemaAnalisisVentas\SistemaAnalisisVentas.DWH\Data\Output";

                        string[] archivos = Directory.GetFiles(outputPath, "*.json");
                        if (archivos.Length > 0)
                            _logger.LogInformation($" {archivos.Length} archivos JSON generados exitosamente en {outputPath}");
                        else
                            _logger.LogWarning($" No se generaron archivos JSON en {outputPath}");
                    }

                    _logger.LogInformation(" Proceso completado correctamente a las {time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, " Error durante la ejecución del proceso ETL.");
                }

                _logger.LogInformation(" Próxima ejecución en {segundos} segundos...", IntervaloEjecucionSegundos);
                await Task.Delay(TimeSpan.FromSeconds(IntervaloEjecucionSegundos), stoppingToken);
            }

            _logger.LogInformation(" Servicio ETL detenido a las {time}", DateTimeOffset.Now);
        }
    }
}
