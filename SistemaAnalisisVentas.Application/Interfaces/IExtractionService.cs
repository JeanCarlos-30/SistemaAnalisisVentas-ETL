using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces
{
    /// <summary>
    /// Define la interfaz principal para el proceso de extracción de datos
    /// desde las fuentes CSV, API y Base de Datos.
    /// </summary>
    public interface IExtractionService
    {
        /// <summary>
        /// Ejecuta el proceso completo de extracción desde todas las fuentes configuradas.
        /// </summary>
        Task ExtraerAsync();
    }
}
