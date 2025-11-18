using SistemaAnalisisVentas.Application.DTOs.Api;

namespace AnalisisVentas.Api.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> ObtenerProductosAsync();
        Task<ProductoDTO?> ObtenerProductoPorIdAsync(int id);
    }
}
