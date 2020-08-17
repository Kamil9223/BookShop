using System;

namespace CommonLib.Extensions
{
    public static class StringExtensions
    {
        public static string cutSign(this string str, string sign)
        {
            if (str.Contains(sign))
                str = str.Replace(sign, "");
            return str;
        }

        public static bool Empty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
