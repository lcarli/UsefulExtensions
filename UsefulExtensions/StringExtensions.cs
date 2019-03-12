using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UsefulExtensions
{
    public static class StringExtensions
    {
        public static int Occurrence(this String instr, string search)
        {
            return Regex.Matches(instr, search).Count;
        }

        public static string OnlyDigits(this string value)
        {
            return new string(value?.Where(c => char.IsDigit(c)).ToArray());
        }

        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return Char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        public static decimal? ToDecimal(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return decimal.Parse(str);
            }
            return null;
        }

        public static Guid ToGuid(this string value)
        {
            Guid valor = Guid.Empty;
            if (Guid.TryParse(value, out valor))
            {
                return valor;
            }

            return valor;
        }

        public static IEnumerable<string> SplitCamelCase(this string source)
        {
            const string pattern = @"[A-Z][a-z]*|[a-z]+|\d+";
            var matches = Regex.Matches(source, pattern);
            foreach (Match match in matches)
            {
                yield return match.Value;
            }
        }

        public static string CamelCaseToHumanCase(this string source)
        {
            var words = source.SplitCamelCase();
            string humanCased = string.Join(" ", words);
            return humanCased;
        }
        public static bool Like(this string value, string search)
        {
            return value.Contains(search) || value.StartsWith(search) || value.EndsWith(search);
        }

        public static String FormataMoeda(this Double valor, String formato, IFormatProvider FormatProvider)
        {
            if (string.IsNullOrEmpty(formato))
                formato = "{0:c}";
            try
            {
                if (FormatProvider != null)
                    return String.Format(FormatProvider, formato, valor);
                else
                    return String.Format(formato, valor);
            }
            catch
            {
                return valor.ToString();
            }
        }
    }
}
