using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slocker
{
    public class HistoryManager
    {
        protected readonly IHistoryCondition _cond;
        protected readonly IHistoryFetcher _fetcher;
        protected readonly IMessageParser _parser;
        public virtual IEnumerable<History> Load()
        {
            var data = _fetcher.Fetch(_cond, _parser);
            return data;
        }
    }
}
