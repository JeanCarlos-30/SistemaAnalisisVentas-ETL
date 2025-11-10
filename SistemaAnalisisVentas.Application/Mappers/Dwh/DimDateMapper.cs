using SistemaAnalisisVentas.Application.DTOs;
using SistemaAnalisisVentas.Application.Helpers;
using SistemaAnalisisVentas.Domain.Entities.DWH.Dimensions;

//namespace SistemaAnalisisVentas.Application.Mappers.Dwh
//{
//    public static class DimDateMapper
//    {
//        public static DimDate ToEntity(VentaDTO dto)
//        {
//            var hierarchy = DateHelper.GetDateHierarchy(dto.FechaVenta);
//            return new DimDate
//            {
//                FullDate = dto.FechaVenta,
//                DayNumber = hierarchy.DayNumber,
//                MonthNumber = hierarchy.MonthNumber,
//                MonthName = hierarchy.MonthName,
//                Quarter = hierarchy.Quarter,
//                Year = hierarchy.Year,
//                DayName = hierarchy.DayName
//            };
//        }
//    }
//}
