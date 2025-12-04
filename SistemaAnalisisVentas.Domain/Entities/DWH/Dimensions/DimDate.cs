namespace SistemaAnalisisVentas.Domain.Entities.DWH.Dimensions
{
    public class DimDate
    {
        public DateTime DateKey { get; set; }      // PK (usaremos date.Date)
        public DateTime FullDate { get; set; }
        public int DayNumber { get; set; }
        public int MonthNumber { get; set; }
        public string MonthName { get; set; } = null!;
        public int Quarter { get; set; }
        public int Year { get; set; }
        public string DayName { get; set; } = null!;
    }
}
