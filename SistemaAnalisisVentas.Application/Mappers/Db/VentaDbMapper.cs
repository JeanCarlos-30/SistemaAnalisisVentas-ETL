using SistemaAnalisisVentas.Application.DTOs.Api;
using SistemaAnalisisVentas.Domain.Entities.DB;

namespace SistemaAnalisisVentas.Application.Mappers.Db
{
    public static class VentaDbMapper
    {
        public static VentaDTO ToDto(VentaDb entity)
        {
            if (entity is null)
                return null;

            return new VentaDTO
            {
                Id = entity.Id,
                ClienteId = entity.ClienteId,
                ProductoId = entity.ProductoId,
                Cantidad = entity.Cantidad,
                PrecioUnitario = entity.PrecioUnitario,
                Descuento = entity.Descuento,
                Total = entity.Total,
                FechaVenta = entity.FechaVenta
            };
        }
    }
}
