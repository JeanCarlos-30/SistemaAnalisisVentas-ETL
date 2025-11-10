using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Domain.Entities;
using SistemaAnalisisVentas.Domain.Entities.CSV;

namespace SistemaAnalisisVentas.Application.Mappers.Csv
{
    public static class ClienteCsvMapper
    {
        public static ClienteDTO ToDto(ClienteCsv entity) => new()
        {
            Id = entity.Id,
            CustomerID = entity.CustomerID,
            CompanyName = entity.CompanyName,
            Country = entity.Country,
            City = entity.City,
            FuenteOrigen = "CSV",
            FechaActualizacion = DateTime.UtcNow
        };
    }
}
