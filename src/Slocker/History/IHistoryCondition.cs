using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slocker
{
    public interface IHistoryCondition
    {
        string GetQuery();
        string RepositoryType { get; }
    }

    public class AndCondition : IHistoryCondition
    {
        private readonly IHistoryCondition[] _conds;
        private readonly string _typ;
        public AndCondition(params IHistoryCondition[] conditions)
        {
            if (_conds.Select(x => x.RepositoryType).Distinct().Count() > 1) throw new ArgumentException("RepositoryType should be the same");
            _conds = conditions;
            _typ = _conds.First().RepositoryType;
        }

        public string RepositoryType
        {
            get
            {
                return _typ;
            }
        }

        public string GetQuery()
        {
            return string.Join(" ", _conds.Select(x => x.GetQuery()));
        }
    }

}
