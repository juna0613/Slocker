using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slocker
{
    public interface IMessageParser
    {
        Message Parse(string comment);
    }
}
