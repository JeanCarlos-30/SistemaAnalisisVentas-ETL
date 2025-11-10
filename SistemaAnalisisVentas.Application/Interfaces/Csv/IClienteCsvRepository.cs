using SistemaAnalisisVentas.Application.DTOs;

namespace SistemaAnalisisVentas.Application.Interfaces.Repositories.Csv
{
    /// <summary>
    /// Define las operaciones de lectura de clientes desde archivos CSV.
    /// </summary>
    public interface IClienteCsvRepository
    {
        /// <summary>
        /// Lee todos los registros de clientes desde el archivo CSV configurado.
        /// </summary>
        /// <returns>Una lista de objetos ClienteDTO.</returns>
        Task<IEnumerable<ClienteDTO>> LeerClientesAsync();
    }
}
