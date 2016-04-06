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
        public static ICounter Create(string extension, IEnumerable<RegexConfig> confs, IDictionary<string, ICounter> cache = null, bool verbose = false)
        {
            if (cache == null)
            {
                return CreateFromConfig(extension, confs, verbose);
            }
            ICounter cnt;
            if(!cache.TryGetValue(extension, out cnt))
            {
                cnt = CreateFromConfig(extension, confs, verbose);
                cache[extension] = cnt;
            }
            return cnt;
        }

        private static ICounter CreateFromConfig(string extension, IEnumerable<RegexConfig> confs, bool verbose)
        {
            var conf = confs.FirstOrDefault(x => x.Extensions.Contains(extension));
            if (conf == default(RegexConfig))
            {
                return new FileLineCounter();
            }
            var cnt = new SourceCodeCounter(new RegexCoreCounterFactory(conf), verbose, extension);
            return cnt;
        }
    }
}
