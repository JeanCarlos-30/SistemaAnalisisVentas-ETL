using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces
{
    /// <summary>
    /// Define los métodos para generar reportes y consultas analíticas
    /// basadas en la información consolidada del Data Warehouse.
    /// </summary>
    public interface IReportingService
    {
        /// <summary>
        /// Obtiene el total de ventas global.
        /// </summary>
        Task<decimal> GetTotalSalesAsync();

        /// <summary>
        /// Obtiene el top 5 de productos más vendidos.
        /// </summary>
        Task<IEnumerable<object>> GetTopProductsAsync();

        /// <summary>
        /// Obtiene el top 5 de clientes con más compras.
        /// </summary>
        Task<IEnumerable<object>> GetTopCustomersAsync();

        /// <summary>
        /// Obtiene el total de ventas por mes o trimestre.
        /// </summary>
        Task<IEnumerable<object>> GetSalesByPeriodAsync();

        /// <summary>
        /// Obtiene las ventas comparadas entre periodos.
        /// </summary>
        Task<IEnumerable<object>> GetComparativeSalesAsync();
    }
}

