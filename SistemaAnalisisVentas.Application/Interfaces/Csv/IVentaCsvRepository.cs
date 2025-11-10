using SistemaAnalisisVentas.Application.DTOs;

namespace SistemaAnalisisVentas.Application.Interfaces.Repositories.Csv
{
    /// <summary>
    /// Define las operaciones de lectura de ventas desde archivos CSV.
    /// </summary>
    public interface IVentaCsvRepository
    {
        /// <summary>
        /// Lee todos los registros de ventas desde el archivo CSV configurado.
        /// </summary>
        /// <returns>Una lista de objetos VentaDTO.</returns>
        Task<IEnumerable<VentaDTO>> LeerVentasAsync();
    }
}
