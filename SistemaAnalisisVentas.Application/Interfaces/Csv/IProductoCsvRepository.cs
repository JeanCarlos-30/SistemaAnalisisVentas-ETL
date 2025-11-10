using SistemaAnalisisVentas.Application.DTOs;

namespace SistemaAnalisisVentas.Application.Interfaces.Repositories.Csv
{
    /// <summary>
    /// Define las operaciones de lectura de productos desde archivos CSV.
    /// </summary>
    public interface IProductoCsvRepository
    {
        /// <summary>
        /// Lee todos los registros de productos desde el archivo CSV configurado.
        /// </summary>
        /// <returns>Una lista de objetos ProductoDTO.</returns>
        Task<IEnumerable<ProductoDTO>> LeerProductosAsync();
    }
}
