using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slocker.Svn
{
    public class SvnHistoryFetcher : IHistoryFetcher
    {
        private readonly string _url;
        public SvnHistoryFetcher(string url)
        {
            _url = url;
        }
        public string RepositoryType
        {
            get
            {
                return "Subversion";
            }
        }

        public IEnumerable<History> Fetch(IHistoryCondition condition, IHisotryParser parser)
        {
            var query = string.Format("svn log {0} {1}", _url, condition.GetQuery());
            // do command execute
            var ret = new[] { "" };
            var histories = parser.Parse(ret);
            return histories;
        }
    }
}
