using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Domain.Entities.CSV
{
    public class VentaCsv : BaseEntity
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; } = string.Empty;
        public DateTime? OrderDate { get; set; }
        public string ShipCountry { get; set; } = string.Empty;
    }
}
