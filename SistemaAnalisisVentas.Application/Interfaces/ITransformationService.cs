using SistemaAnalisisVentas.Application.DTOs.Dwh;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces
{
    public interface ITransformationService
    {
        Task TransformAndValidateAsync();
        Task ComputeDerivedValuesAsync();

        // Devuelve los datos transformados para el DWH
        Task<List<DimProductDTO>> TransformarProductosAsync();
        Task<List<DimCustomerDTO>> TransformarClientesAsync();
        Task<List<FactSalesDTO>> TransformarVentasAsync();
        List<DimDateDTO> GenerarDimDate(DateTime inicio, DateTime fin);

    }
}
