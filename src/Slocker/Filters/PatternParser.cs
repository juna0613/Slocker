using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slocker
{
    public static class PatternParser
    {
        private static readonly Regex PlusPattern = new Regex(@"^\s*(\[-\].+?)?\s*\[\+\](.+?)\s*(\[-\].+?)?$");
        private static readonly Regex MinusPattern = new Regex(@"^\s*(\[\+\].+?)?\[-\](.+?)\s*(\[\+\].+)?$");

        public static T PatternHelp<T>(this string pattern, Func<string, string, T> plusMinusWorker)
        {
            var plusExp = PlusPattern.IsMatch(pattern) ? PlusPattern.Replace(pattern, "$2") : string.Empty;
            var minusExp = MinusPattern.IsMatch(pattern) ? MinusPattern.Replace(pattern, "$2") : string.Empty;
            return plusMinusWorker(plusExp, minusExp);
        }
    }
}
