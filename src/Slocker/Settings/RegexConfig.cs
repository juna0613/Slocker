using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slocker
{
    public class RegexConfig
    {
        public string[] Extensions { get; set; }
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

        public static void Save(this IEnumerable<RegexConfig> conf, string path)
        {
            CreateDirectoryIfNotExist(Path.GetDirectoryName(path));
            using (var writer = new StreamWriter(path, false))
            {
                writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(conf.ToArray()));
            }
        }

        public static RegexConfig[] Load(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<RegexConfig[]>(reader.ReadToEnd());
            }
        }

        private static void CreateDirectoryIfNotExist(string dir)
        {
            var dirs = Path.GetFullPath(dir).Split(Path.DirectorySeparatorChar);
            var top = dirs.First();
            foreach(var subdir in dirs.Skip(1))
            {
                top = Path.Combine(top, subdir);
                if(!Directory.Exists(top))
                {
                    Directory.CreateDirectory(top);
                }
            }
        }
    }
}
