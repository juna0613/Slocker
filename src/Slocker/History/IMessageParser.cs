using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slocker
{
    public interface IHisotryParser
    {
        IEnumerable<History> Parse(IEnumerable<string> logs);
    }
}
