using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slocker
{
    public class History
    {
        public string Id { get; set; }
        public Message Message { get; set; }
        public IEnumerable<string> TargetSources { get; set; }
    }
}
