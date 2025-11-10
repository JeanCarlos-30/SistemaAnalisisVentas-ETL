namespace SistemaAnalisisVentas.Application.Validators
{
    /// <summary>
    /// Contiene los mensajes de error y validación usados en toda la capa Application.
    /// Centraliza los textos para mantener coherencia y evitar duplicación.
    /// </summary>
    public static class MensajesValidacion
    {
        // =========================
        // CLIENTE
        // =========================
        public const string ClienteNulo = "El cliente no puede ser nulo.";
        public const string ClienteIdInvalido = "El identificador del cliente (CustomerID) es obligatorio.";
        public const string ClienteNombreInvalido = "El nombre de la empresa (CompanyName) no es válido (debe tener al menos 2 caracteres).";
        public const string ClienteUbicacionInvalida = "El cliente debe tener al menos una ciudad (City) o país (Country) definido.";

        // =========================
        // PRODUCTO
        // =========================
        public const string ProductoNulo = "El producto no puede ser nulo.";
        public const string ProductoIdInvalido = "El identificador del producto (ProductID) es obligatorio.";
        public const string ProductoNombreInvalido = "El nombre del producto (ProductName) no es válido.";
        public const string ProductoPrecioInvalido = "El precio unitario (UnitPrice) debe ser mayor que cero.";
        public const string ProductoStockInvalido = "La cantidad en inventario (UnitsInStock) no puede ser negativa.";

        // =========================
        // VENTA
        // =========================
        public const string VentaNula = "La venta no puede ser nula.";
        public const string VentaIdInvalido = "El identificador de la venta (OrderID) es obligatorio.";
        public const string VentaClienteInvalido = "La venta debe tener un CustomerID válido.";
        public const string VentaFechaInvalida = "La fecha de venta (OrderDate) no puede ser nula ni futura.";
        public const string VentaPaisInvalido = "El país de envío (ShipCountry) no puede estar vacío.";

        // =========================
        // DETALLE DE VENTA
        // =========================
        public const string DetalleNulo = "El detalle de venta no puede ser nulo.";
        public const string DetalleRelacionInvalida = "El detalle debe tener OrderID y ProductID válidos.";
        public const string DetallePrecioInvalido = "El precio unitario (UnitPrice) debe ser mayor que cero.";
        public const string DetalleCantidadInvalida = "La cantidad (Quantity) debe ser mayor que cero.";
        public const string DetalleDescuentoInvalido = "El descuento (Discount) debe estar entre 0 y 1.";
        public const string DetalleSubtotalInvalido = "El subtotal calculado del detalle de venta es inválido.";
    }
}
