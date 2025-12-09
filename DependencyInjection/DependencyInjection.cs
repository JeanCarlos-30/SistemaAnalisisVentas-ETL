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
            // CONFIG GENERAL
            // ============================================================
            services.AddSingleton(new InfrastructureSettings(configuration));

            // ============================================================
            // DB CONTEXT
            // ============================================================
            services.AddDbContext<AnalisisVentasDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // FIX DEL ERROR — registrar DbContext genérico
            services.AddScoped<DbContext>(provider =>
                provider.GetRequiredService<AnalisisVentasDbContext>());

            // ============================================================
            // REPOS CSV - ETL
            // ============================================================
            services.AddScoped<IClienteCsvRepository, ClienteCsvRepository>();
            services.AddScoped<IProductoCsvRepository, ProductoCsvRepository>();
            services.AddScoped<IVentaCsvRepository, VentaCsvRepository>();
            services.AddScoped<IDetalleVentaCsvRepository, DetalleVentaCsvRepository>();

            // ============================================================
            // SERVICIOS BASE
            // ============================================================
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ILogService, LogService>();

            // ============================================================
            // SERVICIOS ETL
            // ============================================================
            services.AddScoped<IExtractionService, ExtractionService>();
            services.AddScoped<ITransformationService, TransformationService>();
            services.AddScoped<ILoadService, LoadService>();

            // ============================================================
            // VALIDADORES
            // ============================================================
            services.AddScoped<ClienteValidator>();
            services.AddScoped<ProductoValidator>();
            services.AddScoped<VentaValidator>();

            // ============================================================
            // SERVICIOS API
            // ============================================================
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();

            // ============================================================
            // ETL ORCHESTRATOR
            // ============================================================
            services.AddScoped<IETLOrchestratorService, ETLOrchestratorService>();

            return services;
        }
    }
}
