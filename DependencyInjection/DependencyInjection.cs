using AnalisisVentas.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaAnalisisVentas.Application.Interfaces;
using SistemaAnalisisVentas.Application.Interfaces.Api;
using SistemaAnalisisVentas.Application.Interfaces.Db;
using SistemaAnalisisVentas.Application.Interfaces.Repositories.Csv;
using SistemaAnalisisVentas.Application.Interfaces.Services;
using SistemaAnalisisVentas.Application.Services;
using SistemaAnalisisVentas.Application.Services.Api;
using SistemaAnalisisVentas.Infrastructure.Configuration;
using SistemaAnalisisVentas.Infrastructure.Context;
using SistemaAnalisisVentas.Infrastructure.Repositories;
using SistemaAnalisisVentas.Infrastructure.Repositories.Csv;
using SistemaAnalisisVentas.Infrastructure.Services;

namespace SistemaAnalisisVentas.IoC
{
    /// <summary>
    /// Registro central de dependencias para todas las capas del sistema ETL y API.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddSistemaAnalisisVentas(this IServiceCollection services, IConfiguration configuration)
        {
            // ============================================================
            //  1) REGISTRO DE CONFIGURACIÓN BASE
            // ============================================================
            services.AddSingleton(new InfrastructureSettings(configuration));

            // ============================================================
            //  2) REGISTRO DEL DB CONTEXT (Entity Framework Core)
            // ============================================================
            services.AddDbContext<AnalisisVentasDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // ============================================================
            //  3) REGISTRO DE REPOSITORIOS CSV (ETL - YA EXISTENTES)
            // ============================================================
            services.AddScoped<IClienteCsvRepository, ClienteCsvRepository>();
            services.AddScoped<IProductoCsvRepository, ProductoCsvRepository>();
            services.AddScoped<IVentaCsvRepository, VentaCsvRepository>();
            services.AddScoped<IDetalleVentaCsvRepository, DetalleVentaCsvRepository>();

            // ============================================================
            //  4) REGISTRO DE SERVICIOS DE INFRAESTRUCTURA COMUNES
            // ============================================================
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ILogService, LogService>();

            // ============================================================
            //  5) REGISTRO DE SERVICIOS ETL (Extract ya implementado)
            // ============================================================
            services.AddScoped<IExtractionService, ExtractionService>();
            // services.AddScoped<ITransformService, TransformService>();
            // services.AddScoped<ILoadService, LoadService>();

            // ============================================================
            //  6) REGISTRO DE REPOSITORIOS SQL (API)
            // ============================================================
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IVentaRepository, VentaRepository>();

            // ============================================================
            //  7) REGISTRO DE SERVICIOS API
            // ============================================================
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();

            return services;
        }
    }
}
