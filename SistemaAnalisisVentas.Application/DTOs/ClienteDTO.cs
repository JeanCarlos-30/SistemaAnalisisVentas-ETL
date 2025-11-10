using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.DTOs
{
    /// <summary>
    /// Representa un cliente extraído desde una fuente CSV.
    /// </summary>
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string CustomerID { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? FuenteOrigen { get; set; } // CSV, API, DB
        public DateTime? FechaActualizacion { get; set; }
    }
}


