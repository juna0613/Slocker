using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slocker
{
    public class CSharpCounter : ICounter
    {
        private static readonly Regex singleComment = new Regex(@"^\s*(//|#).*"); // comment 
        private static readonly Regex braceExpr = new Regex(@"^\s*({|}|)\s*$"); // line with only-brace or no string in line
        private static readonly Regex blockComments = new Regex(@"/\*(.|\n|\r)*?\*/"); // *? means shortest match
        private static readonly Regex[] skipps = new[]
        {
            singleComment,  braceExpr
        };
        public int Count(string input)
        {
            var commentRemoved = RemoveBlockComments(input);
            var lines = SplitIntoLines(commentRemoved);
            var filtered = Filter(lines);
            return filtered.Count();
        }

        internal static string RemoveBlockComments(string input)
        {
            return blockComments.Replace(input, "");
        }

        internal static IEnumerable<string> SplitIntoLines(string input)
        {
            return input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries );
        }

        internal static IEnumerable<string> Filter(IEnumerable<string> data)
        {
            foreach(var d in data)
            {
                if (skipps.Any(_ => _.IsMatch(d))) continue;
                yield return d;
            }
        }
    }
}
