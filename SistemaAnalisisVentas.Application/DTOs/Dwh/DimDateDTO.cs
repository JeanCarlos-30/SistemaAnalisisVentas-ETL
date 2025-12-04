using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.DTOs.Dwh
{
    public class DimDateDTO
    {
        public DateTime DateKey { get; set; }
        public DateTime FullDate { get; set; }
        public int DayNumber { get; set; }
        public int MonthNumber { get; set; }
        public string MonthName { get; set; } = string.Empty;
        public int Quarter { get; set; }
        public int Year { get; set; }
        public string DayName { get; set; } = string.Empty;
    }
}
