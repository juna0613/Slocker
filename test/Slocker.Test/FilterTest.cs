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
            var filter = "[+]*.cs;*.cpp [-]*Test*;*.c".ToPatternFilter();
            var data = new[] { "hoge.cs", "barTest.cs", "foo.cpp", "bar.rb" };


            var filter2 = new AndFilter<string>(
                new OrFilter<string>(new PatternFilter("*.cs"), new PatternFilter("*.cpp")),
                new AndFilter<string>(new NegatePatternFilter("*Test*"), new NegatePatternFilter("*.c")));
            Assert.That(filter2.Filter(data), Is.EquivalentTo(new[] { "hoge.cs", "foo.cpp"}), "failed on filter2");

            Assert.That(filter.Filter(data), Is.EquivalentTo(filter2.Filter(data)), "failed on filter1");
        }

        [Test]
        public void TestParserResultPlusMinus()
        {
            var filter = "[+]*.cs;*.cpp [-]*Test*;*.c".ToPatternFilter();
            var data = new[] { "hoge.cs", "barTest.cs", "foo.cpp", "bar.rb" };

            Assert.That(filter.Filter(data), Is.EquivalentTo(new[] { "hoge.cs", "foo.cpp"}));
        }

        [TestCase(new[] { "hoge.cs", "barTest.cs", "foo.cpp", "bar.rb" }, "*Test*", new[] { "hoge.cs", "foo.cpp", "bar.rb" })]
        [TestCase(new[] { "hoge.cs", "barTest.cs", "foo.cpp", "bar.rb" }, "*.c", new[] { "hoge.cs", "barTest.cs", "foo.cpp", "bar.rb" })]
        public void TestNegatePatternFilter(string[] source, string negatePattern, string[] expected)
        {
            var filter = new NegatePatternFilter(negatePattern);
            Assert.That(filter.Filter(source), Is.EquivalentTo(expected));
        }

        [TestCase(new[] { "hoge.cs", "barTest.cs", "foo.cpp", "bar.rb" }, "*Test*", "*.cpp", new[] { "hoge.cs", "bar.rb" })]
        [TestCase(new[] { "hoge.cs", "barTest.cs", "foo.cpp", "bar.rb" }, "*.cs", "*.cpp", new[] { "bar.rb" })]
        public void TestNegatePatternAndFilter(string[] source, string negaterPattern1, string negatePattern2, string[] expected)
        {
            var filter = new AndFilter<string>(new NegatePatternFilter(negaterPattern1), new NegatePatternFilter(negatePattern2));
            Assert.That(filter.Filter(source), Is.EquivalentTo(expected));
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
