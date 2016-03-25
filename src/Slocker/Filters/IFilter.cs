using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slocker.Filters
{
    public interface IFilter<T> where T : IEquatable<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> candidates);
    }

    public class AndFilter<T> : IFilter<T>
        where T : IEquatable<T>
    {
        private readonly IFilter<T>[] _filters;
        public AndFilter(params IFilter<T>[] filters)
        {
            _filters = filters;
        }
        public IEnumerable<T> Filter(IEnumerable<T> candidates)
        {
            return _filters.Aggregate(candidates, (source, filter) => filter.Filter(source));
        }
    }

    public class OrFilter<T> : IFilter<T>
        where T : IEquatable<T>
    {
        private readonly IFilter<T>[] _filters;
        public OrFilter(params IFilter<T>[] filters)
        {
            _filters = filters;
        }
        public IEnumerable<T> Filter(IEnumerable<T> candidates)
        {
            IEnumerable<T> ret = null;
            foreach(var f in _filters)
            {
                var filtered = f.Filter(candidates);
                if(ret == null)
                {
                    ret = filtered;
                }
                else
                {
                    ret = ret.Union(filtered);
                }
            }
            return ret.Distinct();
        }
    }
}
