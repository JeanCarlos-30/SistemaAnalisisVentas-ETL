using SistemaAnalisisVentas.Application.DTOs;

namespace SistemaAnalisisVentas.Application.Validators
{
    public class VentaValidator
    {
        public (bool EsValido, string Mensaje) Validar(VentaDTO venta)
        {
            if (venta == null)
                return (false, MensajesValidacion.VentaNula);

            // Validar OrderID
            if (venta.OrderID <= 0)
                return (false, MensajesValidacion.VentaIdInvalido);

            // CustomerID ahora es string
            if (string.IsNullOrWhiteSpace(venta.CustomerID))
                return (false, MensajesValidacion.VentaClienteInvalido);

            // OrderDate es nullable y no puede ser futura
            if (!venta.OrderDate.HasValue || venta.OrderDate.Value > DateTime.UtcNow)
                return (false, MensajesValidacion.VentaFechaInvalida);

            // ShipCountry no puede estar vacío
            if (string.IsNullOrWhiteSpace(venta.ShipCountry))
                return (false, MensajesValidacion.VentaPaisInvalido);

            return (true, string.Empty);
        }

        /// <summary>
        /// Método simplificado para integrarse con el TransformationService.
        /// Retorna solo true/false.
        /// </summary>
        public bool EsValido(VentaDTO venta)
        {
            var resultado = Validar(venta);
            return resultado.EsValido;
        }
    }
}
