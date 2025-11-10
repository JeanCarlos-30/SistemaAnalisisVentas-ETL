using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SistemaAnalisisVentas.Application.Helpers
{
    /// <summary>
    /// Proporciona funciones auxiliares para limpiar, normalizar y validar texto.
    /// </summary>
    public static class TextHelper
    {
        /// <summary>
        /// Elimina espacios, caracteres especiales y convierte el texto a mayúsculas.
        /// </summary>
        public static string Normalize(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string cleaned = Regex.Replace(input.Trim(), @"\s+", " ");
            cleaned = Regex.Replace(cleaned, @"[^a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]", string.Empty);
            return cleaned.ToUpperInvariant();
        }

        /// <summary>
        /// Valida si una cadena tiene formato de correo electrónico.
        /// </summary>
        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        /// <summary>
        /// Elimina tildes y normaliza el texto a ASCII.
        /// </summary>
        public static string RemoveAccents(string input)
        {
            var normalized = input.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();

            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                    builder.Append(c);
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}