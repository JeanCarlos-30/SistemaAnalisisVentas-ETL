using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.Interfaces.Api;
using SistemaAnalisisVentas.Application.Interfaces.Db;

namespace SistemaAnalisisVentas.Application.Services.Api
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repo;

        public ProductoService(IProductoRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ProductoDTO>> ObtenerProductosAsync() =>
            _repo.ObtenerProductosAsync();

        public Task<ProductoDTO?> ObtenerProductoPorIdAsync(int id) =>
            _repo.ObtenerProductoPorIdAsync(id);

        public Task<string> CrearProductoAsync(ProductoDTO producto) =>
            _repo.InsertarProductoAsync(producto);

        public Task<string> ActualizarProductoAsync(ProductoDTO producto) =>
            _repo.ActualizarProductoAsync(producto);

        public Task<string> EliminarProductoAsync(int id) =>
            _repo.EliminarProductoAsync(id);
    }
}
