using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces.Services
{
    /// <summary>
    /// Define las operaciones relacionadas con el manejo de archivos del sistema ETL.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Verifica si un archivo existe en la ruta especificada.
        /// </summary>
        /// <param name="ruta">Ruta completa del archivo.</param>
        /// <returns>True si el archivo existe, de lo contrario false.</returns>
        Task<bool> ExisteArchivoAsync(string ruta);

        /// <summary>
        /// Lee el contenido completo de un archivo de texto.
        /// </summary>
        /// <param name="ruta">Ruta del archivo.</param>
        /// <returns>Contenido del archivo como cadena.</returns>
        Task<string> LeerArchivoAsync(string ruta);

        /// <summary>
        /// Guarda contenido en un archivo de texto.
        /// </summary>
        /// <param name="ruta">Ruta del archivo.</param>
        /// <param name="contenido">Texto a guardar.</param>
        /// <returns>Task completada cuando el archivo se guarda correctamente.</returns>
        Task GuardarArchivoAsync(string ruta, string contenido);

        /// <summary>
        /// Crea un directorio si no existe.
        /// </summary>
        /// <param name="rutaDirectorio">Ruta del directorio.</param>
        Task CrearDirectorioSiNoExisteAsync(string rutaDirectorio);
    }
}
