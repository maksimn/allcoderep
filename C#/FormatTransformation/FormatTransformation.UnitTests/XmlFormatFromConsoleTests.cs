using System;
using NUnit.Framework;

namespace FormatTransformation.UnitTests {
    [TestFixture]
    class XMLFormatFromConsoleTests {
        [Test]
        public void GetTypeNameIdTupleFromLine_Test1() {
            var res = XmlFormatFromConsole.GetTypeNameIdTupleFromLine("<dir name='deode' id='5'>");

            Assert.True(res.Equals(new Tuple<String, String, Int32>("dir", "deode", 5)));
        }
        [Test]
        public void GetTypeNameIdTupleFromLine_Test2() {
            var res = XmlFormatFromConsole.GetTypeNameIdTupleFromLine("  <dir name='uig' id='99'>");

            Assert.True(res.Equals(new Tuple<String, String, Int32>("dir", "uig", 99)));
        }
        [Test]
        public void GetTypeNameIdTupleFromLine_Test3() {
            var res = XmlFormatFromConsole.GetTypeNameIdTupleFromLine("    <file name='deode' id='5'>");

            Assert.True(res.Equals(new Tuple<String, String, Int32>("file", "deode", 5)));
        }
    }
}