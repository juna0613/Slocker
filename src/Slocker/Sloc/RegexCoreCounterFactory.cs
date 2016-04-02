using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slocker
{
    public class RegexCoreCounterFactory : ICoreCounterFactory
    {
        private readonly Regex _block, _single;
        private readonly Regex[] _misc;
        public RegexCoreCounterFactory(Regex blockComments, Regex singleLine, params Regex[] misc)
        {
            _block = blockComments;
            _single = singleLine;
            _misc = misc;
        }
        public string RemoveBlockComments(string input)
        {
            return _block.Replace(input, "");
        }

        public string RemoveMiscThings(string input)
        {
            return _misc.Aggregate(input, (line, misc) => misc.Replace(line, ""));
        }

        public string RemoveSingleComment(string input)
        {
            return _single.Replace(input, "");
        }
    }
}
