using SistemaAnalisisVentas.Application.DTOs;

namespace SistemaAnalisisVentas.Application.Interfaces.Repositories.Csv
{
    /// <summary>
    /// Define las operaciones de lectura de detalles de ventas desde archivos CSV.
    /// </summary>
    public interface IDetalleVentaCsvRepository
    {
        /// <summary>
        /// Lee todos los registros de detalle de ventas desde el archivo CSV configurado.
        /// </summary>
        /// <returns>Una lista de objetos DetalleVentaDTO.</returns>
        Task<IEnumerable<DetalleVentaDTO>> LeerDetallesVentaAsync();
    }
}
