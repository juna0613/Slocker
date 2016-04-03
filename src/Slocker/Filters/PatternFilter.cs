using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slocker
{
    public class PatternFilter : IFilter<string>
    {
        private readonly Regex _pattern;
        public PatternFilter(string globPattern) : this(globPattern.ToLikeRegex())
        {
        }
        public PatternFilter(Regex pattern)
        {
            _pattern = pattern;
        }
        public IEnumerable<string> Filter(IEnumerable<string> candidates)
        {
            return candidates.Where(x => _pattern.IsMatch(x));
        }
    }

    public class NegatePatternFilter : IFilter<string>
    {
        private readonly IFilter<string> _delegator;
        public NegatePatternFilter(string globPattern) : this(globPattern.ToLikeRegex())
        {
        }
        public NegatePatternFilter(Regex pattern)
        {
            _delegator = new PatternFilter(pattern);
        }
        public IEnumerable<string> Filter(IEnumerable<string> candidates)
        {
            return candidates.Except(_delegator.Filter(candidates));
        }
    }

    public static class FilterParserExtension
    {
        /// <summary>
        /// [+]**/*.cs;**/*.reg [-]*Test*
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static IFilter<string> ToPatternFilter(this string pattern)
        {
            return pattern.PatternHelp((plus, minus) =>
            {
                var plusFilter = string.IsNullOrEmpty(plus) ? (IFilter<string>)new NullFilter<string>() : 
                                new OrFilter<string>(plus.Split(';').Select(x => new PatternFilter(x)).ToArray());
                var minusFilter = string.IsNullOrEmpty(minus) ? (IFilter<string>)new NullFilter<string>() :
                                new AndFilter<string>(minus.Split(';').Select(x => new NegatePatternFilter(x)).ToArray());
                return new AndFilter<string>(plusFilter, minusFilter);
            });
        }
    }
}
