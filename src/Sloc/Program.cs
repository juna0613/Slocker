using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
namespace Sloc
{
    using Slocker;
    using System.Configuration;
    using System.IO;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            string invokedVerb = string.Empty;
            object verbOpt = null;
            var opt = new Options();
            if(!CommandLine.Parser.Default.ParseArguments(args, opt, (verb, subOpt) =>
            {
                invokedVerb = verb;
                verbOpt = subOpt;
            }))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
            try
            {
                if (invokedVerb == "count")
                {
                    var sub = verbOpt as CountOptions;
                    DoCount(sub);
                }
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.StackTrace);
            }
        }

        static void DoCount(CountOptions opt)
        {
            if (opt == null)
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
            var filter = opt.Filter.ToPatternFilter();
            var settingPath = ConfigurationManager.AppSettings.Get("RegexSettingPath");
            settingPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), 
                settingPath);
            var confgs = RegexConfigExtensions.Load(settingPath);
            var cache = new Dictionary<string, ICounter>();
            var simpleLineCounter = new FileLineCounter();
            foreach(var f in filter.Filter(opt.Target.RecursiveGetFiles()))
            {
                var counter = opt.IsSimpleMode ? simpleLineCounter : SourceCounterFactory.Create(Path.GetExtension(f), confgs, cache, opt.Verbose);
                var data = File.ReadAllText(f);
                var cnt = counter.Count(data);
                Console.WriteLine("{0}{1}{2}", f, opt.Delimitor, cnt);
            }
        }
    }

    public class Options
    {
        public Options()
        {
            CountOpt = new CountOptions();
        }
        [VerbOption("count", HelpText = "Count SLOC for a specified folder")]
        public CountOptions CountOpt { get; set; }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return CommandLine.Text.HelpText.AutoBuild(this, verb);
        }
    }

    public class CountOptions
    {
        [Option('t', "target", Required = true, HelpText = "Full path to target directory")]
        public string Target { get; set; }

        [Option('f', "filters", Required = false, DefaultValue = "[+]*.*", 
            HelpText = "File filters for counting SLOC, like, '[+]*.* [-]*Test*'")]
        public string Filter { get; set; }

        [Option('v', "verbose", DefaultValue = false, HelpText = "Verbose output")]
        public bool Verbose { get; set; }

        [Option('d', "linedelimitor", DefaultValue = '\t', HelpText = "Line Delimitor")]
        public char Delimitor { get; set; }

        [Option('s', "simple-mode", DefaultValue = false, HelpText = "Simple mode, e.g. just remove empty lines")]
        public bool IsSimpleMode { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return CommandLine.Text.HelpText.AutoBuild(this);
        }
    }

    public class HistoricalOptions
    {
        [Option('t', "target", Required = true, HelpText = "Full path to the source repository")]
        public string Target { get; set; }


    }
}
