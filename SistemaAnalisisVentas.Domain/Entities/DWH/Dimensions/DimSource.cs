using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Domain.Entities.DWH.Dimensions
{
    public class DimSource : BaseEntity
    {
        public string SourceName { get; set; } = string.Empty;
        public string SourceType { get; set; } = string.Empty;
        public DateTime LoadDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
