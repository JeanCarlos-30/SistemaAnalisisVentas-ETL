using SistemaAnalisisVentas.Application.DTOs.Dwh;
using SistemaAnalisisVentas.Domain.Entities.DWH.Facts;

namespace SistemaAnalisisVentas.Application.Mappers.Dwh
{
    public static class FactSalesMapper
    {
        public static FactSales ToEntity(FactSalesDTO dto)
        {
            return new FactSales
            {
                ProductKey = dto.ProductKey,
                CustomerKey = dto.CustomerKey,
                DateKey = dto.DateKey,
                SourceKey = dto.SourceKey,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                Discount = dto.Discount
            };
        }
    }
}
