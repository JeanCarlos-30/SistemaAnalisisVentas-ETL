namespace SistemaAnalisisVentas.Application.DTOs
{
    /// <summary>
    /// Representa un detalle de venta extraído desde una fuente CSV.
    /// </summary>
    public class DetalleVentaDTO
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal Subtotal { get; set; } // Calculado
        public string? FuenteOrigen { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
