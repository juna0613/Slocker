using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slocker
{
    public class RegexConfig
    {
        public string BlockComment { get; set; }
        public string SingleComment { get; set; }
        public string[] MiscExpressions { get; set; }
    }

    public static class RegexConfigExtensions
    {
        public static Regex ToBlockCommentRegex(this RegexConfig conf)
        {
            return string.IsNullOrEmpty(conf.BlockComment) ? null : new Regex(conf.BlockComment);
        }

        public static Regex ToSingleCommentRegex(this RegexConfig conf)
        {
            return string.IsNullOrEmpty(conf.SingleComment) ? null : new Regex(conf.SingleComment);
        }

        public static IEnumerable<Regex> ToMiscExpressionRegex(this RegexConfig conf)
        {
            return conf.MiscExpressions.Where(x => !string.IsNullOrEmpty(x)).Select(x => new Regex(x));
        }


    }
}
