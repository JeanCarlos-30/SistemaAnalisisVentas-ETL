using SistemaAnalisisVentas.Application.Helpers;
using SistemaAnalisisVentas.Domain.Entities.DWH.Dimensions;

namespace SistemaAnalisisVentas.Application.Mappers.Dwh
{
    public static class DimSourceMapper
    {
        public static DimSource ToEntity(string sourceName, string type, string status)
        {
            return new DimSource
            {
                SourceName = TextHelper.Normalize(sourceName),
                SourceType = type,
                LoadDate = DateHelper.UtcNow,
                Status = status
            };
        }
    }
}
