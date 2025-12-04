using SistemaAnalisisVentas.Application.DTOs;

namespace SistemaAnalisisVentas.Application.Interfaces.Api
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTO>> ObtenerProductosAsync();
        Task<ProductoDTO?> ObtenerProductoPorIdAsync(int id);
        Task<string> CrearProductoAsync(ProductoDTO producto);
        Task<string> ActualizarProductoAsync(ProductoDTO producto);
        Task<string> EliminarProductoAsync(int id);
    }
}
