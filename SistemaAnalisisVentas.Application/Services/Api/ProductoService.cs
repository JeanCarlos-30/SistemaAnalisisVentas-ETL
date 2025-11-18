using AnalisisVentas.Api.Services;
using SistemaAnalisisVentas.Application.DTOs.Api;
using SistemaAnalisisVentas.Application.Interfaces.Api;
using SistemaAnalisisVentas.Application.Interfaces.Db;
using SistemaAnalisisVentas.Application.Mappers.Api;

namespace SistemaAnalisisVentas.Application.Services.Api
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repo;

        public ProductoService(IProductoRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ProductoDTO>> ObtenerProductosAsync()
        {
            var entities = await _repo.ObtenerProductosAsync();
            return entities.Select(ProductoApiMapper.ToDto).ToList();
        }

        public async Task<ProductoDTO?> ObtenerProductoPorIdAsync(int id)
        {
            var entity = await _repo.ObtenerProductoPorIdAsync(id);
            return entity == null ? null : ProductoApiMapper.ToDto(entity);
        }
    }
}
