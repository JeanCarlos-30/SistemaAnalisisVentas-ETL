using SistemaAnalisisVentas.Application.DTOs;

namespace SistemaAnalisisVentas.Application.Validators
{
    /// <summary>
    /// Valida los datos de una venta antes de procesarla o cargarla al Data Warehouse.
    /// </summary>
    public class VentaValidator
    {
        public (bool EsValido, string Mensaje) Validar(VentaDTO venta)
        {
            if (venta == null)
                return (false, MensajesValidacion.VentaNula);

            if (venta.OrderID <= 0)
                return (false, MensajesValidacion.VentaIdInvalido);

            if (string.IsNullOrWhiteSpace(venta.CustomerID))
                return (false, MensajesValidacion.VentaClienteInvalido);

            if (!venta.OrderDate.HasValue || venta.OrderDate.Value > DateTime.UtcNow)
                return (false, MensajesValidacion.VentaFechaInvalida);

            if (string.IsNullOrWhiteSpace(venta.ShipCountry))
                return (false, MensajesValidacion.VentaPaisInvalido);

            return (true, string.Empty);
        }
    }
}
