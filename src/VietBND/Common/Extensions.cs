using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietBND.Common
{
    public static class Extensions
    {
        public static string ToTitleCase(this string value)
        {
            TextInfo formatter = new CultureInfo("en-US", false).TextInfo;
            return formatter.ToTitleCase(value.ToLower());
        }

        public static string ToStringSql(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "''";
            return $"'{value}'";
        }
        public static int AsInt(this string value)
        {
            if (!int.TryParse(value, out int result))
                return default(int);
            return result;
        }

        public static int AsInt(this object value)
        {
            if (value == null)
                return default(int);
            return (int)value;
        }

        public static string AsArrayJoin(this string[] value)
        {
            if (value.Length == 0)
                return string.Empty;
            return string.Join(',', value);
        }

        public static string AsArrayJoin(this long[] value)
        {
            if (value.Length == 0)
                return string.Empty;
            return string.Join(",", value);
        }

        public static string AsArrayJoin(this int[] value)
        {
            if (value.Length == 0)
                return string.Empty;
            return string.Join(",", value);
        }

        public static string AsDisplayName(this Enum @enum)
        {
            if (@enum == null)
                return string.Empty;
            return Enum.GetName(@enum.GetType(), @enum);
        }
    }
}
