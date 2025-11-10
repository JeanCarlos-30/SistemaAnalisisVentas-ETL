using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces
{
    /// <summary>
    /// Define las operaciones que se encargan de transformar, limpiar y validar los datos
    /// extraídos antes de ser cargados al Data Warehouse.
    /// </summary>
    public interface ITransformationService
    {
        /// <summary>
        /// Aplica validaciones de negocio, limpieza de datos nulos o duplicados,
        /// y normalización de formatos.
        /// </summary>
        Task TransformAndValidateAsync();

        /// <summary>
        /// Realiza los cálculos derivados, como totales, descuentos o conversiones.
        /// </summary>
        Task ComputeDerivedValuesAsync();
    }
}

