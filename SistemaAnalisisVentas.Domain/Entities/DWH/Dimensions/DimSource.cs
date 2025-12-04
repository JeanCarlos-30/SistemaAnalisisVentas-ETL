using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Domain.Entities.DWH.Dimensions
{
    public class DimSource
    {
        public int SourceKey { get; set; }        // PK (hash de SourceName + SourceType)
        public string SourceName { get; set; } = null!;
        public string SourceType { get; set; } = null!;
        public DateTime LoadDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
