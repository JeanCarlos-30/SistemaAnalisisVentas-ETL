using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.DTOs
{
    /// <summary>
    /// Representa un producto extraído desde una fuente CSV.
    /// </summary>
    public class ProductoDTO
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public string? FuenteOrigen { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}


