namespace SistemaAnalisisVentas.Application.DTOs
{
    /// <summary>
    /// Representa una venta extraída desde una fuente CSV.
    /// </summary>
    public class VentaDTO
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public string CustomerID { get; set; } = string.Empty;
        public DateTime? OrderDate { get; set; }
        public string ShipCountry { get; set; } = string.Empty;
        public string? FuenteOrigen { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
