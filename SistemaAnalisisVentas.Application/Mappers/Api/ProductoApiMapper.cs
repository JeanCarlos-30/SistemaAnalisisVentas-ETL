using SistemaAnalisisVentas.Application.DTOs.Api;
using SistemaAnalisisVentas.Domain.Entities.API;

namespace SistemaAnalisisVentas.Application.Mappers.Api
{
    public static class ProductoApiMapper
    {
        public static ProductoDTO ToDto(ProductoApi entity)
        {
            if (entity is null)
                return null;

            return new ProductoDTO
            {
                Id = entity.Id,
                ProductName = entity.ProductName,
                Category = entity.Category,
                Price = entity.Price,
                Stock = entity.Stock,
                FuenteID = entity.FuenteID
            };
        }
    }
}
