using SistemaAnalisisVentas.Application.Services;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces
{
    public interface IExtractionService
    {
        Task<ExtractionResult> ExtraerAsync();
    }
}
