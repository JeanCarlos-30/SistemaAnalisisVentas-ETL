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

            // ProductID no puede ser 0 o negativo
            if (producto.ProductID <= 0)
                return (false, MensajesValidacion.ProductoIdInvalido);

            // Nombre obligatorio
            if (string.IsNullOrWhiteSpace(producto.ProductName))
                return (false, MensajesValidacion.ProductoNombreInvalido);

            // Precio mayor a 0
            if (producto.Price <= 0)
                return (false, MensajesValidacion.ProductoPrecioInvalido);

            // Stock no puede ser negativo
            if (producto.Stock < 0)
                return (false, MensajesValidacion.ProductoStockInvalido);

            return (true, string.Empty);
        }

        /// <summary>
        /// Método simplificado para integrarse con el TransformationService.
        /// Retorna solo true/false.
        /// </summary>
        public bool EsValido(ProductoDTO producto)
        {
            var resultado = Validar(producto);
            return resultado.EsValido;
        }
    }
}
