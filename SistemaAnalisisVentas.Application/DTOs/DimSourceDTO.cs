using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.DTOs
{
    /// <summary>
    /// DTO que representa la información de trazabilidad de carga ETL.
    /// </summary>
    public class DimSourceDTO
    {
        public int SourceKey { get; set; }
        public string SourceName { get; set; } = string.Empty;
        public string SourceType { get; set; } = string.Empty;
        public DateTime LoadDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}

