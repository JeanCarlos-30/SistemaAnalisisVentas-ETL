using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.Validators;
using SistemaAnalisisVentas.Application.Helpers;

namespace SistemaAnalisisVentas.Application.Validators
{
    /// <summary>
    /// Valida la integridad de los datos de cliente antes de ser procesados.
    /// </summary>
    public class ClienteValidator
    {
        /// <summary>
        /// Verifica que los datos del cliente sean válidos antes de pasar a la fase de transformación o carga.
        /// </summary>
        public (bool EsValido, string Mensaje) Validar(ClienteDTO cliente)
        {
            if (cliente == null)
                return (false, MensajesValidacion.ClienteNulo);

            if (string.IsNullOrWhiteSpace(cliente.CustomerID))
                return (false, MensajesValidacion.ClienteIdInvalido);

            if (string.IsNullOrWhiteSpace(cliente.CompanyName) || cliente.CompanyName.Length < 2)
                return (false, MensajesValidacion.ClienteNombreInvalido);

            if (string.IsNullOrWhiteSpace(cliente.Country) && string.IsNullOrWhiteSpace(cliente.City))
                return (false, MensajesValidacion.ClienteUbicacionInvalida);

            return (true, string.Empty);
        }
    }
}

