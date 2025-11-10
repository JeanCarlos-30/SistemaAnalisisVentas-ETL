using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Domain.Entities;
using SistemaAnalisisVentas.Domain.Entities.CSV;

namespace SistemaAnalisisVentas.Application.Mappers.Csv
{
    public static class DetalleVentaCsvMapper
    {
        public static DetalleVentaDTO ToDto(DetalleVentaCsv entity) => new()
        {
            Id = entity.Id,
            OrderID = entity.OrderID,
            ProductID = entity.ProductID,
            UnitPrice = entity.UnitPrice,
            Quantity = entity.Quantity,
            Discount = entity.Discount,
            Subtotal = entity.Subtotal,
            FuenteOrigen = "CSV",
            FechaActualizacion = DateTime.UtcNow
        };
    }
}
