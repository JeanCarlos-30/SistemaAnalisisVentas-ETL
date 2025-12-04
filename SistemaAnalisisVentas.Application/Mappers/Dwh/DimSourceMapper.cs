using SistemaAnalisisVentas.Domain.Entities.DWH.Dimensions;

namespace SistemaAnalisisVentas.Application.Mappers.Dwh
{
    public static class DimSourceMapper
    {
        public static DimSource ToEntity(string sourceName, string sourceType, string status)
        {
            string keyBase = $"{sourceName}|{sourceType}";

            return new DimSource
            {
                // Surrogate key basada en hash
                SourceKey = keyBase.GetHashCode(),
                SourceName = sourceName,
                SourceType = sourceType,
                LoadDate = DateTime.Now,
                Status = status
            };
        }
    }
}
