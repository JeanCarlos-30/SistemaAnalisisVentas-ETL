using SistemaAnalisisVentas.Application.DTOs;

namespace SistemaAnalisisVentas.Application.Interfaces.Db
{
    public interface IProductoRepository
    {
        Task<IEnumerable<ProductoDTO>> ObtenerProductosAsync();
        Task<ProductoDTO?> ObtenerProductoPorIdAsync(int id);
        Task<string> InsertarProductoAsync(ProductoDTO producto);
        Task<string> ActualizarProductoAsync(ProductoDTO producto);
        Task<string> EliminarProductoAsync(int id);
    }
}
