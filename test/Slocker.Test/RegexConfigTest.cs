using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
namespace Slocker.Test
{
    [TestFixture]
    public class RegexConfigTest
    {
        private string path = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
        [SetUp]
        public void Setup()
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
        [TearDown]
        public void Teardown()
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
        [Test]
        public void TestSaveAndRead()
        {
            var conf = new[] { new RegexConfig
            {
                BlockComment = "hoge",
                MiscExpressions = new[] { "bar" },
                SingleComment = "zoo"
            }};

            conf.Save(path);

            var conf2 = RegexConfigExtensions.Load(path);

            Assert.That(conf.First().BlockComment, Is.EqualTo(conf2.First().BlockComment));
            Assert.That(conf.First().SingleComment, Is.EqualTo(conf2.First().SingleComment));
            Assert.That(conf.First().MiscExpressions, Is.EquivalentTo(conf2.First().MiscExpressions));
        }

    }
}
