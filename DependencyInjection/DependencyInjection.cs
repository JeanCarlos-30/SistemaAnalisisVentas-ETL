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
using SistemaAnalisisVentas.Application.Validators;
using SistemaAnalisisVentas.Infrastructure.Configuration;
using SistemaAnalisisVentas.Infrastructure.Context;
using SistemaAnalisisVentas.Infrastructure.Repositories;
using SistemaAnalisisVentas.Infrastructure.Repositories.Csv;
using SistemaAnalisisVentas.Infrastructure.Services;

namespace SistemaAnalisisVentas.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSistemaAnalisisVentas(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // ============================================================
            // 1) CONFIGURACIÓN GENERAL
            // ============================================================
            services.AddSingleton(new InfrastructureSettings(configuration));

            // ============================================================
            // 2) DB CONTEXT (SQL SERVER)
            // ============================================================
            services.AddDbContext<AnalisisVentasDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // ============================================================
            // 3) REPOSITORIOS CSV - ETL (Extract)
            // ============================================================
            services.AddScoped<IClienteCsvRepository, ClienteCsvRepository>();
            services.AddScoped<IProductoCsvRepository, ProductoCsvRepository>();
            services.AddScoped<IVentaCsvRepository, VentaCsvRepository>();
            services.AddScoped<IDetalleVentaCsvRepository, DetalleVentaCsvRepository>(); // ← 🔥 CORREGIDO

            // ============================================================
            // 4) SERVICIOS BASE (Infraestructura)
            // ============================================================
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ILogService, LogService>();

            // ============================================================
            // 5) SERVICIOS ETL
            // ============================================================
            services.AddScoped<IExtractionService, ExtractionService>();
            services.AddScoped<ITransformationService, TransformationService>();
            services.AddScoped<ILoadService, LoadService>();

            // ============================================================
            // 6) VALIDADORES
            // ============================================================
            services.AddScoped<ClienteValidator>();
            services.AddScoped<ProductoValidator>();
            services.AddScoped<VentaValidator>();

            // ============================================================
            // 7) SERVICIOS API
            // ============================================================
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();

            return services;
        }
    }
}
