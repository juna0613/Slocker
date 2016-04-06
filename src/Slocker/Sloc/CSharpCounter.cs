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
        private static readonly ICounter _delegator = new SourceCodeCounter(new RegexCoreCounterFactory(
                CSharpRegexSet.BlockComment,
                CSharpRegexSet.SingleComment,
                CSharpRegexSet.NamespaceClause,
                CSharpRegexSet.UsingClause,
                CSharpRegexSet.Brace
                ), false, "*.cs");

        public int Count(string input)
        {
            return _delegator.Count(input);
        }

        public bool IsTarget(string filename)
        {
            return _delegator.IsTarget(filename);
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
    }

}
