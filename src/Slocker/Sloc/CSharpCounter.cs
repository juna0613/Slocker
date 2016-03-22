using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slocker
{
    using CSharp;
    public class CSharpCounter : ICounter
    {
        public int Count(string input)
        {
            return input.RemoveBlockComments(CSharpRegexSet.BlockComment)
                .SplitIntoLines()
                .Filter(CSharpRegexSet.Brace, CSharpRegexSet.SingleComment, CSharpRegexSet.UsingClause, CSharpRegexSet.NamespaceClause)
                .Count();
        }
    }

    namespace CSharp
    {
        internal static class CSharpRegexSet
        {
            internal static readonly Regex SingleComment = new Regex(@"^\s*(//|#).*$"); // comment and derective
            internal static readonly Regex Brace = new Regex(@"^\s*[\s{}]\s*$"); // line with only-brace
            internal static readonly Regex BlockComment = new Regex(@"/\*(.|\n|\r)*?\*/"); // *? means shortest match
            internal static readonly Regex UsingClause = new Regex(@"^\s*using\s+\w+(\s*\.\s*\w+)*\s*;\s*"); // using Foo . Bar  ; using Foo; using Foo.Bar;
            internal static readonly Regex NamespaceClause = new Regex(@"^\s*namespace\s+\w+(\s*\.\w+)*\s*$");
        }
        
        internal static class CSharpCounterExtension
        {
            internal static string RemoveBlockComments(this string input, Regex commentRegex)
            {
                return commentRegex.Replace(input, "");
            }

            internal static IEnumerable<string> SplitIntoLines(this string input)
            {
                return input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            }

            internal static IEnumerable<string> Filter(this IEnumerable<string> data, params Regex[] excludeRegexes)
            {
                return data.Where(x => excludeRegexes.All(rgx => !rgx.IsMatch(x)));
            }
        }
    }

}
