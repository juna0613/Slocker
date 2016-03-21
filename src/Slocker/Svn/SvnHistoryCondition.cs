using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slocker.Svn
{
    public abstract class SvnHistoryCondition : IHistoryCondition
    {
        private const string Tag = "Subversion";
        public string RepositoryType
        {
            get
            {
                return Tag;
            }
        }

        public abstract string GetQuery();
    }

    public class SvnSingleRevision : SvnHistoryCondition
    {
        public override string GetQuery()
        {
            throw new NotImplementedException();
        }
    }

}
