using System;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces.Services
{
    /// <summary>
    /// Define las operaciones del sistema de registro (logging) para el proceso ETL.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Registra un mensaje informativo.
        /// </summary>
        /// <param name="mensaje">Mensaje a registrar.</param>
        Task RegistrarInfoAsync(string mensaje);

        /// <summary>
        /// Registra una advertencia.
        /// </summary>
        /// <param name="mensaje">Mensaje de advertencia.</param>
        Task RegistrarAdvertenciaAsync(string mensaje);

        /// <summary>
        /// Registra un error con su excepción asociada.
        /// </summary>
        /// <param name="mensaje">Descripción del error.</param>
        /// <param name="excepcion">Excepción asociada (opcional).</param>
        Task RegistrarErrorAsync(string mensaje, Exception? excepcion = null);

        /// <summary>
        /// Registra el inicio o fin de una operación ETL.
        /// </summary>
        /// <param name="operacion">Nombre de la operación (Extract, Transform, Load).</param>
        /// <param name="inicio">Indica si se está iniciando (true) o finalizando (false).</param>
        Task RegistrarOperacionAsync(string operacion, bool inicio);
    }
}
