using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaAnalisisVentas.Application.Interfaces;
using SistemaAnalisisVentas.Application.Interfaces.Repositories.Csv;
using SistemaAnalisisVentas.Application.Interfaces.Services;
using SistemaAnalisisVentas.Application.Services;
using SistemaAnalisisVentas.Infrastructure.Configuration;
using SistemaAnalisisVentas.Infrastructure.Repositories.Csv;
using SistemaAnalisisVentas.Infrastructure.Services;

namespace SistemaAnalisisVentas.IoC
{
    /// <summary>
    /// Registro central de dependencias para todas las capas del sistema ETL.
    /// Conecta Application con Infrastructure y establece los servicios base.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddSistemaAnalisisVentas(this IServiceCollection services, IConfiguration configuration)
        {
            //  Registrar configuración de infraestructura
            services.AddSingleton(new InfrastructureSettings(configuration));

            //  Registrar repositorios CSV (fuente principal en esta fase del ETL)
            services.AddScoped<IClienteCsvRepository, ClienteCsvRepository>();
            services.AddScoped<IProductoCsvRepository, ProductoCsvRepository>();
            services.AddScoped<IVentaCsvRepository, VentaCsvRepository>();
            services.AddScoped<IDetalleVentaCsvRepository, DetalleVentaCsvRepository>();

            //  Registrar servicios comunes de infraestructura
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ILogService, LogService>();

            //  Registrar servicios de aplicación (fase Extract)
            services.AddScoped<IExtractionService, ExtractionService>();

            //  Las siguientes fases (Transform, Load) se agregan en futuras entregas
            // services.AddScoped<ITransformService, TransformService>();
            // services.AddScoped<ILoadService, LoadService>();

            return services;
        }
    }
}

