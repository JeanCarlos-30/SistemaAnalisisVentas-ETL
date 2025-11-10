using SistemaAnalisisVentas.Application.Interfaces;
using SistemaAnalisisVentas.Application.Helpers;
using Microsoft.EntityFrameworkCore;

namespace SistemaAnalisisVentas.Application.Services
{
    /// <summary>
    /// Servicio encargado de generar consultas analíticas sobre el DWH.
    /// Retorna KPIs, métricas y reportes usados por la capa de presentación.
    /// </summary>
    public class ReportingService : IReportingService
    {
        private readonly DbContext _context;

        public ReportingService(DbContext context)
        {
            _context = context;
        }

        // ---------------------------------
        //  1. Total de ventas
        // ---------------------------------
        public async Task<decimal> GetTotalSalesAsync()
        {
            LoggerHelper.Info("Consultando total de ventas...");
            return await _context.Set<Domain.Entities.DWH.Facts.FactSales>()
                                 .SumAsync(f => f.Quantity * f.UnitPrice - f.Discount);
        }

        // ---------------------------------
        //  2. Top 5 productos
        // ---------------------------------
        public async Task<IEnumerable<object>> GetTopProductsAsync()
        {
            LoggerHelper.Info("Consultando top 5 productos más vendidos...");
            return await _context.Set<Domain.Entities.DWH.Facts.FactSales>()
                .GroupBy(f => f.ProductKey)
                .Select(g => new
                {
                    ProductKey = g.Key,
                    TotalSales = g.Sum(x => x.Quantity * x.UnitPrice - x.Discount)
                })
                .OrderByDescending(x => x.TotalSales)
                .Take(5)
                .ToListAsync();
        }

        // ---------------------------------
        //  3. Top 5 clientes
        // ---------------------------------
        public async Task<IEnumerable<object>> GetTopCustomersAsync()
        {
            LoggerHelper.Info("Consultando top 5 clientes con más compras...");
            return await _context.Set<Domain.Entities.DWH.Facts.FactSales>()
                .GroupBy(f => f.CustomerKey)
                .Select(g => new
                {
                    CustomerKey = g.Key,
                    TotalSpent = g.Sum(x => x.Quantity * x.UnitPrice - x.Discount)
                })
                .OrderByDescending(x => x.TotalSpent)
                .Take(5)
                .ToListAsync();
        }

        // ---------------------------------
        //  4. Ventas por periodo (mes/trimestre)
        // ---------------------------------
        public async Task<IEnumerable<object>> GetSalesByPeriodAsync()
        {
            LoggerHelper.Info("Consultando ventas agrupadas por mes...");
            return await _context.Set<Domain.Entities.DWH.Facts.FactSales>()
                .GroupBy(f => f.DateKey.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalSales = g.Sum(x => x.Quantity * x.UnitPrice - x.Discount)
                })
                .OrderBy(x => x.Month)
                .ToListAsync();
        }

        // ---------------------------------
        //  5. Comparativa de ventas
        // ---------------------------------
        public async Task<IEnumerable<object>> GetComparativeSalesAsync()
        {
            LoggerHelper.Info("Consultando comparación de ventas año a año...");
            return await _context.Set<Domain.Entities.DWH.Facts.FactSales>()
                .GroupBy(f => f.DateKey.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    TotalSales = g.Sum(x => x.Quantity * x.UnitPrice - x.Discount)
                })
                .OrderBy(x => x.Year)
                .ToListAsync();
        }
    }
}

