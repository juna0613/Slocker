using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slocker
{
    public class SourceCounterFactory
    {
        public ICounter Create(string filename)
        {
            // this should be configuration (or rule) based way -> should have more extensible way  
            switch(Path.GetExtension(filename))
            {
                case "cs": return new CSharpCounter();
                default  : return default(ICounter);
            }
        }
    }
}
