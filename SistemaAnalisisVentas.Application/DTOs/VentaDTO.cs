namespace SistemaAnalisisVentas.Application.DTOs
{
    /// <summary>
    /// Representa una venta extraída desde una fuente CSV.
    /// </summary>
    public class VentaDTO
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; } = string.Empty;
        public DateTime? OrderDate { get; set; }
        public string ShipCountry { get; set; } = string.Empty;
        public int? FuenteID { get; set; }
    }

}
