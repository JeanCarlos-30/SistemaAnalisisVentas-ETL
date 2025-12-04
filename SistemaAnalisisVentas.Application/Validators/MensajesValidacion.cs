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
        public const string ClienteNombreInvalido = "El nombre del cliente (FirstName) no es válido.";
        public const string ClienteApellidoInvalido = "El apellido (LastName) no es válido.";
        public const string ClienteUbicacionInvalida = "El cliente debe tener al menos una ciudad (City) o país (Country) definido.";


        // =========================
        // PRODUCTO
        // =========================
        public const string ProductoNulo = "El producto no puede ser nulo.";
        public const string ProductoIdInvalido = "El identificador del producto (ProductID) es obligatorio y debe ser mayor que cero.";
        public const string ProductoNombreInvalido = "El nombre del producto (ProductName) no puede estar vacío.";
        public const string ProductoPrecioInvalido = "El precio del producto (Price) debe ser mayor que cero.";
        public const string ProductoStockInvalido = "El inventario del producto (Stock) no puede ser negativo.";

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
        public const string DetalleCantidadInvalida = "La cantidad (Quantity) debe ser mayor que cero.";
        public const string DetallePrecioInvalido = "El precio total (TotalPrice) debe ser mayor o igual que cero.";
    }
}
