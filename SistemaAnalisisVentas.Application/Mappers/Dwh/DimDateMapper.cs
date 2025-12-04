using SistemaAnalisisVentas.Application.DTOs.Dwh;
using SistemaAnalisisVentas.Domain.Entities.DWH.Dimensions;

namespace SistemaAnalisisVentas.Application.Mappers.Dwh
{
    public static class DimDateMapper
    {
        public static DimDate ToEntity(DimDateDTO dto)
        {
            return new DimDate
            {
                DateKey = dto.DateKey,
                FullDate = dto.FullDate,
                DayNumber = dto.DayNumber,
                MonthNumber = dto.MonthNumber,
                MonthName = dto.MonthName,
                Quarter = dto.Quarter,
                Year = dto.Year,
                DayName = dto.DayName
            };
        }
    }
}
