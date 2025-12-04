using SistemaAnalisisVentas.Application.DTOs.Api;

namespace SistemaAnalisisVentas.Application.Validators
{
    /// <summary>
    /// Valida los datos del cliente antes de procesarlos.
    /// </summary>
    public class ClienteValidator
    {
        public (bool EsValido, string Mensaje) Validar(ClienteDTO cliente)
        {
            if (cliente == null)
                return (false, MensajesValidacion.ClienteNulo);

            if (cliente.CustomerID <= 0)
                return (false, MensajesValidacion.ClienteIdInvalido);

            if (string.IsNullOrWhiteSpace(cliente.FirstName))
                return (false, MensajesValidacion.ClienteNombreInvalido);

            if (string.IsNullOrWhiteSpace(cliente.LastName))
                return (false, MensajesValidacion.ClienteApellidoInvalido);

            if (!string.IsNullOrWhiteSpace(cliente.City) && string.IsNullOrWhiteSpace(cliente.Country))
                return (false, MensajesValidacion.ClienteUbicacionInvalida);

            return (true, string.Empty);
        }

        public bool EsValido(ClienteDTO cliente)
        {
            var resultado = Validar(cliente);
            return resultado.EsValido;
        }
    }
}
