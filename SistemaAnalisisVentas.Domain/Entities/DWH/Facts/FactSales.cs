using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Domain.Entities.DWH.Facts
{
    public class FactSales : BaseEntity
    {
        public int ProductKey { get; set; }
        public int CustomerKey { get; set; }
        public DateTime DateKey { get; set; }
        public int SourceKey { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total => (Quantity * UnitPrice) - Discount;
    }
}
