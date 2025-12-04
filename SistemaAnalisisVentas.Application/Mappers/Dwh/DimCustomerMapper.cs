using SistemaAnalisisVentas.Application.DTOs.Dwh;
using SistemaAnalisisVentas.Domain.Entities.DWH.Dimensions;

namespace SistemaAnalisisVentas.Application.Mappers.Dwh
{
    public static class DimCustomerMapper
    {
        public static DimCustomer ToEntity(DimCustomerDTO dto)
        {
            return new DimCustomer
            {
                CustomerKey = dto.CustomerKey,
                CustomerId = dto.CustomerId,
                CustomerName = dto.CustomerName,
                ContactTitle = dto.ContactTitle,
                City = dto.City,
                Region = dto.Region,
                Country = dto.Country
            };
        }
    }
}
