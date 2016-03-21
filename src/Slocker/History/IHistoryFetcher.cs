using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slocker
{
    public interface IHistoryFetcher
    {
        IEnumerable<History> Fetch(IHistoryCondition condition, IHisotryParser parser);
        string RepositoryType { get; }
    }
}
