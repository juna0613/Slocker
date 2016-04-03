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

    public static class PatternParser
    {
        private static readonly Regex PlusPattern = new Regex(@"^\s*(\[-\].+?)?\s*\[\+\](.+?)\s*(\[-\].+?)?$");
        private static readonly Regex MinusPattern = new Regex(@"^\s*(\[\+\].+?)?\[-\](.+?)\s*(\[\+\].+)?$");
        /// <summary>
        /// [+]**/*.cs;**/*.reg [-]*Test*
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static IFilter<string> ToPatternFilter(this string pattern)
        {
            var plusFilter = PlusPattern.IsMatch(pattern) ?
                new OrFilter<string>(PlusPattern.Replace(pattern, "$2").Split(';').Select(x => new PatternFilter(x)).ToArray()) :
                (IFilter<string>) new NullFilter<string>();
            
            var minusFilter = MinusPattern.IsMatch(pattern) ?
                new AndFilter<string>(MinusPattern.Replace(pattern, "$2").Split(';').Select(x => new NegatePatternFilter(x)).ToArray()) :
                (IFilter<string>) new NullFilter<string>();

            return new AndFilter<string>(plusFilter, minusFilter);
        }
    }

}
