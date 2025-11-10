using SistemaAnalisisVentas.Application.DTOs;

namespace SistemaAnalisisVentas.Application.Validators
{
    /// <summary>
    /// Valida los datos de un producto antes de la transformación o carga al Data Warehouse.
    /// </summary>
    public class ProductoValidator
    {
        public (bool EsValido, string Mensaje) Validar(ProductoDTO producto)
        {
            if (producto == null)
                return (false, MensajesValidacion.ProductoNulo);

            if (producto.ProductID <= 0)
                return (false, MensajesValidacion.ProductoIdInvalido);

            if (string.IsNullOrWhiteSpace(producto.ProductName))
                return (false, MensajesValidacion.ProductoNombreInvalido);

            if (producto.UnitPrice <= 0)
                return (false, MensajesValidacion.ProductoPrecioInvalido);

            if (producto.UnitsInStock < 0)
                return (false, MensajesValidacion.ProductoStockInvalido);

            return (true, string.Empty);
        }
    }
}
