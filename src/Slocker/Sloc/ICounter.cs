using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slocker
{
    public interface ICounter
    {
        bool IsTarget(string filename);
        int Count(string input);
    }

    public class FileLineCounter : ICounter
    {
        
        public int Count(string input)
        {
            return input.SplitIntoLines().Count();
        }

        public bool IsTarget(string filename)
        {
            return true;
        }
    }

    public class SourceCodeCounter : ICounter
    {
        private readonly ICoreCounterFactory _factory;
        private readonly Regex[] _patterns;
        private readonly bool _verbose;
        public SourceCodeCounter(ICoreCounterFactory factory, bool verbose = false, params string[] filepatterns)
        {
            _factory = factory;
            _verbose = verbose;
            _patterns = filepatterns.Select(x => x.ToLikeRegex()).ToArray();
        }
        public int Count(string input)
        {
            var data = input.RemoveBlockComments(_factory)
                .SplitIntoLines()
                .Filter(_factory).ToList();
            var cnt = data.Count;
            if (_verbose)
            {
                Console.WriteLine("-----------");
                data.ForEach(Console.WriteLine);
            }
            return cnt;
        }

        public bool IsTarget(string filename)
        {
            return _patterns.Any(x => x.IsMatch(filename));
        }
    }

    internal static class CounterExtension
    {
        internal static string RemoveBlockComments(this string input, ICoreCounterFactory factory)
        {
            return factory == null ? input : factory.RemoveBlockComments(input);
        }

        internal static IEnumerable<string> SplitIntoLines(this string input)
        {
            return input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        internal static IEnumerable<string> Filter(this IEnumerable<string> data, ICoreCounterFactory factory)
        {
            return data.Select(x =>
            {
                var x2 = string.IsNullOrEmpty(x)  ? string.Empty : factory == null ? x : factory.RemoveSingleComment(x);
                return   string.IsNullOrEmpty(x2) ? string.Empty : factory == null ? x2 : factory.RemoveMiscThings(x2);
            }).Where(x => !string.IsNullOrEmpty(x));
        }
    }
}
