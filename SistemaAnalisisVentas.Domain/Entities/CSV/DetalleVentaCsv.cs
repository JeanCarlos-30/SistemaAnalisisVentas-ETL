using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Domain.Entities.CSV
{
    public class DetalleVentaCsv : BaseEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }     //  Agregar
        public decimal Subtotal { get; set; }     //  Agregar
    }
}
