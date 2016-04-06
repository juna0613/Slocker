using System;
using System.Collections.Generic;
using System.IO;
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
        public static IEnumerable<string> RecursiveGetFiles(this string dir, string searchPattern = "*.*")
        {
            return Directory.GetFiles(dir, searchPattern).Union(
                Directory.GetDirectories(dir).SelectMany(sub => sub.RecursiveGetFiles(searchPattern))
            ).Select(x => Path.GetFullPath(x));
        }
    }
}
