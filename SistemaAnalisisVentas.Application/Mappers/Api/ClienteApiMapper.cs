using SistemaAnalisisVentas.Application.DTOs.Api;
using SistemaAnalisisVentas.Domain.Entities.API;

namespace SistemaAnalisisVentas.Application.Mappers.Api
{
    public static class ClienteApiMapper
    {
        public static ClienteDTO ToDto(ClienteApi entity)
        {
            if (entity is null)
                return null;

            return new ClienteDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone,
                City = entity.City,
                Country = entity.Country
            };
        }
    }
}
