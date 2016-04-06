using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace Slocker.Test
{
    [TestFixture]
    public class CoreTest
    {
        [Test]
        public void TestRecursiveGetFiles()
        {
            var files = (Environment.CurrentDirectory + @"\..\").RecursiveGetFiles("*.cs");
            Assert.That(files.Select(x => System.IO.Path.GetExtension(x)).Distinct().Count(), Is.EqualTo(1));
        }
    }
}
