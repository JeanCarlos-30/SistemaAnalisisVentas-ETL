using System;

namespace SistemaAnalisisVentas.Application.DTOs.Dwh
{
    public class FactSalesDTO
    {
        public int ProductKey { get; set; }
        public int CustomerKey { get; set; }
        public DateTime DateKey { get; set; }    // en FactSales es DATETIME
        public int SourceKey { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }    // Total = Quantity * UnitPrice - Discount
    }
}
