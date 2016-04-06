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
            using System.Threading.Tasks;
            namespace hoge.bar
            {
            public string RepositoryType
            {/* commentout
                get
                {
                    return ""Subversion"";
                }*/
            }
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
                using(var hoge = new DbConnection())
                {
                    hoge.Do();
                }
                var histories = parser.Parse(ret); /* but
                * believe its
                * a good one*/
                return histories;
            }
            #endregion
            ";
            //var commentRemoved = input.RemoveBlockComments(CSharpRegexSet.BlockComment);
            //var splitted = commentRemoved.SplitIntoLines();
            //foreach(var d in splitted)
            //{
            //    Console.WriteLine(d);
            //}
            //Console.WriteLine("--------------");
            //var filtered = splitted.Filter(CSharpRegexSet.SingleComment, CSharpRegexSet.Brace, CSharpRegexSet.UsingClause, CSharpRegexSet.NamespaceClause);
            //foreach(var d in filtered)
            //{
            //    Console.WriteLine(d);
            //}
            //var conter = new CSharpCounter();
            //Console.WriteLine(conter.Count(input: input));
        }
    }
}
