using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Domain.Entities.DB
{
    public class VentaDb : BaseEntity
    {
        public int ClienteId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public DateTime FechaVenta { get; set; }

        public decimal Total => (Cantidad * PrecioUnitario) - Descuento;
    }
}
