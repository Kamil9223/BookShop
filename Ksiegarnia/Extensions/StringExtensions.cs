using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Extensions
{
    public static class StringExtensions
    {
        public static string cutSign(this String str, string sign)
        {
            if (str.Contains(sign))
                str = str.Replace(sign, "");
            return str;
        }
    }
}
