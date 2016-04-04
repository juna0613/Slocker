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
        public static ICounter Create(string extension, IEnumerable<RegexConfig> confs, IDictionary<string, ICounter> cache = null)
        {
            if (cache == null)
            {
                return CreateFromConfig(extension, confs);
            }
            ICounter cnt;
            if(!cache.TryGetValue(extension, out cnt))
            {
                cnt = CreateFromConfig(extension, confs);
                cache[extension] = cnt;
            }
            return cnt;
        }

        private static ICounter CreateFromConfig(string extension, IEnumerable<RegexConfig> confs)
        {
            var conf = confs.FirstOrDefault(x => x.Extensions.Contains(extension));
            if (conf == default(RegexConfig))
            {
                return new FileLineCounter();
            }
            var cnt = new SourceCodeCounter(new RegexCoreCounterFactory(conf), extension);
            return cnt;
        }
    }
}
