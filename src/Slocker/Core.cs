using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slocker
{
    public static class Core
    {
        public static bool Like(this string str, string pattern)
        {
            return pattern.ToLikeRegex().IsMatch(str);
        }

        public static Regex ToLikeRegex(this string pattern)
        {
            return new Regex(
                "^" + Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$",
                RegexOptions.IgnoreCase | RegexOptions.Singleline
            );
        }
    }
}
