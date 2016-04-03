using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
namespace Sloc
{
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
            if(invokedVerb == "count")
            {
                var sub = verbOpt as CountOptions;
                DoCount(sub);
            }
        }

        static void DoCount(CountOptions opt)
        {
            if (opt == null)
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
            Console.WriteLine(opt.Filter);
            Console.WriteLine(opt.Target);
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

        [HelpOption]
        public string GetUsage()
        {
            return CommandLine.Text.HelpText.AutoBuild(this);
        }
    }
}
