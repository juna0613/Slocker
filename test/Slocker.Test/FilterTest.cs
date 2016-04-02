using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace Slocker.Test
{
    [TestFixture]
    public class FilterTest
    {
        [Test]
        public void TestParserPlus()
        {
            var filter = "[+]*.cs;b*.c".ToPatternFilter();
            var data = new[] { "hoge.cs", "foo.cpp", "bar.c" };
            Assert.That(filter.Filter(data), Is.EquivalentTo(new[] { "hoge.cs", "bar.c" }));
        }

        [Test]
        public void TestParserMinus()
        {
            var filter = "[-]*.cs".ToPatternFilter();
            var data = new[] { "hoge.cs", "foo.cpp", "bar.c" };
            Assert.That(filter.Filter(data), Is.EquivalentTo(new[] { "foo.cpp", "bar.c" }));
        }

        [Test]
        public void TestParserPlusMinus()
        {
            var filter = "[-]*Test*;*.c [+]*.cs;*.cpp".ToPatternFilter();
            var data = new[] { "hoge.cs", "barTest.cs", "foo.cpp", "bar.rb" };
            Assert.That(filter.Filter(data), Is.EquivalentTo(new[] { "hoge.cs", "foo.cpp"}));
        }
        [Test]
        public void TestParserPlusMinus2()
        {
            var filter = "[+]*.cs [-]*.cs".ToPatternFilter();
            var data = new[] { "hoge.cs", "barTest.cs", "foo.cpp", "bar.rb" };
            Assert.That(filter.Filter(data), Is.Empty);
        }

        [Test]
        public void TestPatternInner()
        {
            var plus = new PatternFilter("*.cs");
            var minus = new NegatePatternFilter("*.cs");
            var filter = new AndFilter<string>(plus, minus);
            var data = new[] { "hoge.cs", "barTest.cs", "foo.cpp", "bar.rb" };
            Assert.That(filter.Filter(data), Is.Empty);
        }
    }
}
