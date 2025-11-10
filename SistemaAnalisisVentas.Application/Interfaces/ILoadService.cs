using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Interfaces
{
    /// <summary>
    /// Define las operaciones responsables de cargar los datos ya transformados
    /// hacia el Data Warehouse (tablas de dimensiones y hechos).
    /// </summary>
    public interface ILoadService
    {
        /// <summary>
        /// Carga los datos en las tablas de dimensiones (DimProducts, DimCustomers, DimDate, DimSource).
        /// </summary>
        Task LoadDimensionsAsync();

        /// <summary>
        /// Carga los datos en la tabla de hechos (FactSales).
        /// </summary>
        Task LoadFactsAsync();

        /// <summary>
        /// Registra el estado de carga (fecha, origen, resultado).
        /// </summary>
        Task RegisterLoadStatusAsync();
    }
}

