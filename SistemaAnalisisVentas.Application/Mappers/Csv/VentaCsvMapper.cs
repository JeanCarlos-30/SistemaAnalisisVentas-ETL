using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Domain.Entities;
using SistemaAnalisisVentas.Domain.Entities.CSV;

namespace SistemaAnalisisVentas.Application.Mappers.Csv
{
    public static class VentaCsvMapper
    {
        public static VentaDTO ToDto(VentaCsv entity) => new()
        {
            Id = entity.Id,
            OrderID = entity.OrderID,
            CustomerID = entity.CustomerID,
            OrderDate = entity.OrderDate,
            ShipCountry = entity.ShipCountry,
            FuenteOrigen = "CSV",
            FechaActualizacion = DateTime.UtcNow
        };
    }
}
