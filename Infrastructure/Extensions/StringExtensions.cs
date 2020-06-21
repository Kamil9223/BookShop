using System;

namespace Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string cutSign(this String str, string sign)
        {
            if (str.Contains(sign))
                str = str.Replace(sign, "");
            return str;
        }

        public static bool Empty(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }
    }
}
