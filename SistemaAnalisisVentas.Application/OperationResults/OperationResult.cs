using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.OperationResults
{
    /// <summary>
    /// Representa el resultado de una operación que devuelve un tipo de dato específico.
    /// </summary>
    public class OperationResult<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public string? Code { get; private set; }
        public T? Data { get; private set; }

        private OperationResult(bool success, string message, T? data = default, string? code = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Code = code;
        }

        public static OperationResult<T> Ok(string message = "Operación completada exitosamente.", T? data = default)
            => new OperationResult<T>(true, message, data);

        public static OperationResult<T> Warning(string message, T? data = default)
            => new OperationResult<T>(false, message, data, "WARNING");

        public static OperationResult<T> Error(string message, string? code = null, T? data = default)
            => new OperationResult<T>(false, message, data, code);
    }
}

