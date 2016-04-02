using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slocker
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
            IEnumerable<T> ret = candidates;
            foreach(var f in _filters)
            {
                ret = f.Filter(ret);
            }
            return ret;
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

    public class NullFilter<T> : IFilter<T>
        where T : IEquatable<T>
    {
        public IEnumerable<T> Filter(IEnumerable<T> candidates)
        {
            return candidates;
        }
    }
}
