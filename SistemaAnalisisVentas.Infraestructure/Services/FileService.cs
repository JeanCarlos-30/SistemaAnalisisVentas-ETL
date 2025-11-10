using SistemaAnalisisVentas.Application.Interfaces.Services;

namespace SistemaAnalisisVentas.Infrastructure.Services
{
    /// <summary>
    /// Implementación del servicio para operaciones de lectura, escritura
    /// y verificación de archivos dentro del sistema ETL.
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Verifica si un archivo existe en la ruta especificada.
        /// </summary>
        /// <param name="ruta">Ruta completa del archivo.</param>
        public async Task<bool> ExisteArchivoAsync(string ruta)
        {
            return await Task.FromResult(File.Exists(ruta));
        }

        /// <summary>
        /// Lee el contenido completo de un archivo de texto.
        /// </summary>
        /// <param name="ruta">Ruta del archivo.</param>
        public async Task<string> LeerArchivoAsync(string ruta)
        {
            if (!File.Exists(ruta))
                throw new FileNotFoundException($"No se encontró el archivo especificado: {ruta}");

            using var reader = new StreamReader(ruta);
            var contenido = await reader.ReadToEndAsync();
            return contenido;
        }

        /// <summary>
        /// Guarda contenido de texto en un archivo. Si el archivo no existe, lo crea.
        /// </summary>
        /// <param name="ruta">Ruta completa del archivo.</param>
        /// <param name="contenido">Texto a guardar.</param>
        public async Task GuardarArchivoAsync(string ruta, string contenido)
        {
            var directorio = Path.GetDirectoryName(ruta);

            if (!string.IsNullOrEmpty(directorio))
                await CrearDirectorioSiNoExisteAsync(directorio);

            await File.WriteAllTextAsync(ruta, contenido);
        }

        /// <summary>
        /// Crea un directorio si no existe.
        /// </summary>
        /// <param name="rutaDirectorio">Ruta del directorio.</param>
        public async Task CrearDirectorioSiNoExisteAsync(string rutaDirectorio)
        {
            if (!Directory.Exists(rutaDirectorio))
                Directory.CreateDirectory(rutaDirectorio);

            await Task.CompletedTask;
        }
    }
}
