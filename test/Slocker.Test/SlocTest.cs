using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace Slocker.Test
{
    [TestFixture]
    public class SlockTest
    {
        [Test]
        public void Test1()
        {
            var input = @"
            public string RepositoryType
            {/* commentout
                get
                {
                    return ""Subversion"";
                }*/
            }
            /**/
            /*
              ?
            */
            ///<params>
            ///<data>
            #startregion
            public IEnumerable<History> Fetch(IHistoryCondition condition, IHisotryParser parser)
            {   var query = string.Format(""svn log {0} {1}"", _url, condition.GetQuery());
                // do command execute
                var ret = new[] { """" }; /* it's not a good one*/
                var histories = parser.Parse(ret); /* but
                * believe its
                * a good one*/
                return histories;
            }
            #endregion
            ";
            var commentRemoved = CSharpCounter.RemoveBlockComments(input);
            var splitted = CSharpCounter.SplitIntoLines(commentRemoved);
            foreach(var d in splitted)
            {
                Console.WriteLine(d);
            }
            Console.WriteLine("--------------");
            var filtered = CSharpCounter.Filter(splitted);
            foreach(var d in filtered)
            {
                Console.WriteLine(d);
            }
            var conter = new CSharpCounter();
            Console.WriteLine(conter.Count(input: input));
        }
    }
}
