using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Helpers
{
    /// <summary>
    /// Proporciona funciones auxiliares para manejar y formatear fechas.
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// Retorna la fecha actual en formato UTC.
        /// </summary>
        public static DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Convierte una cadena de texto a DateTime, considerando formato y cultura.
        /// </summary>
        public static DateTime? ParseDate(string? dateString, string format = "yyyy-MM-dd")
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return null;

            if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                return result;

            return null;
        }

        /// <summary>
        /// Retorna el nombre del mes según el número (en español).
        /// </summary>
        public static string GetMonthName(int month)
        {
            if (month < 1 || month > 12)
                return "Mes inválido";

            return new CultureInfo("es-ES").DateTimeFormat.GetMonthName(month);
        }

        /// <summary>
        /// Genera una jerarquía de fecha (día, mes, trimestre, año) para la DimDate.
        /// </summary>
        public static (int DayNumber, int MonthNumber, string MonthName, int Quarter, int Year, string DayName)
            GetDateHierarchy(DateTime date)
        {
            var culture = new CultureInfo("es-ES");
            return (
                date.Day,
                date.Month,
                culture.DateTimeFormat.GetMonthName(date.Month),
                (date.Month - 1) / 3 + 1,
                date.Year,
                culture.DateTimeFormat.GetDayName(date.DayOfWeek)
            );
        }
    }
}
