using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.Helpers;
using SistemaAnalisisVentas.Domain.Entities.DWH.Dimensions;
/*namespace SistemaAnalisisVentas.Application.Mappers.Dwh
{
    public static class DimCustomerMapper
    {
        public static DimCustomer ToEntity(ClienteDTO dto) => new()
        {
            CustomerKey = dto.Id,
            CustomerId = dto.Id.ToString(),
            CustomerName = TextHelper.Normalize(dto.Nombre),
            ContactTitle = "Cliente",
            City = TextHelper.Normalize(dto.Region),
            Region = TextHelper.Normalize(dto.Region),
            Country = TextHelper.Normalize(dto.Pais)
        };
    }
}*/