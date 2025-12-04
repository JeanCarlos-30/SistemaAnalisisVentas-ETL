using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.DTOs.Dwh
{
    public class DimProductDTO
    {
        public int ProductKey { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int StockQty { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
