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
        public int Quantity { get; set; }        //  Cambiar short → int
        public decimal Discount { get; set; }    //  Cambiar float → decimal
        public decimal Subtotal { get; set; }    //  decimal está bien
        public string? FuenteOrigen { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
