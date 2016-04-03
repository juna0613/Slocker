using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slocker
{
    public interface ICounter
    {
        int Count(string input);
    }

    public class SourceCodeCounter : ICounter
    {
        private readonly ICoreCounterFactory _factory;
        public SourceCodeCounter(ICoreCounterFactory factory)
        {
            _factory = factory;
        }
        public int Count(string input)
        {

            return input.RemoveBlockComments(_factory)
                .SplitIntoLines()
                .Filter(_factory)
                .Count();
        }
    }

    internal static class CounterExtension
    {
        internal static string RemoveBlockComments(this string input, ICoreCounterFactory factory)
        {
            return factory.RemoveBlockComments(input);
        }

        internal static IEnumerable<string> SplitIntoLines(this string input)
        {
            return input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        internal static IEnumerable<string> Filter(this IEnumerable<string> data, ICoreCounterFactory factory)
        {
            return data.Select(x =>
            {
                var x2 = string.IsNullOrEmpty(x) ? string.Empty : factory.RemoveSingleComment(x);
                return   string.IsNullOrEmpty(x2) ? string.Empty : factory.RemoveMiscThings(x2);
            }).Where(x => !string.IsNullOrEmpty(x));
        }
    }
}
