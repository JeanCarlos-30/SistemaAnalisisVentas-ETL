using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Domain.Entities;
using SistemaAnalisisVentas.Domain.Entities.CSV;

namespace SistemaAnalisisVentas.Application.Mappers.Csv
{
    public static class ProductoCsvMapper
    {
        public static ProductoDTO ToDto(ProductoCsv entity) => new()
        {
            Id = entity.Id,
            ProductID = entity.ProductID,
            ProductName = entity.ProductName,
            UnitPrice = entity.UnitPrice,
            UnitsInStock = entity.UnitsInStock,
            FuenteOrigen = "CSV",
            FechaActualizacion = DateTime.UtcNow
        };
    }
}
