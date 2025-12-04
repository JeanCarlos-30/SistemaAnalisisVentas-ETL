using SistemaAnalisisVentas.Application.DTOs.Dwh;
using SistemaAnalisisVentas.Domain.Entities.DWH.Dimensions;

namespace SistemaAnalisisVentas.Application.Mappers.Dwh
{
    public static class DimProductMapper
    {
        public static DimProduct ToEntity(DimProductDTO dto)
        {
            return new DimProduct
            {
                ProductKey = dto.ProductKey,
                ProductId = dto.ProductId,
                Name = dto.Name,
                CategoryName = dto.CategoryName,
                StockQty = dto.StockQty,
                UnitPrice = dto.UnitPrice
            };
        }
    }
}
