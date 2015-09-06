using System;
using NUnit.Framework;

namespace FormatTransformation.UnitTests {
    [TestFixture]
    class AFormatTests {
        [Test]
        public void FindFormatInitializerTest1() {
            FindFormat find = new FindFormat(new FindFormatInitializerTest1());

            Assert.AreEqual(2, find.n);
        }
        [Test]
        public void FindFormat_ToString_Test1() {
            FindFormat find = new FindFormat(new FindFormatInitializerTest1());

            String s = find.ToString();

            Assert.AreEqual("2\nsite 0\nsite/news 12\n", s);
        }
        [Test]
        public void XMLFormatInitializerTest1() {
            XMLFormat xml = new XMLFormat(new XMLFormatInitializerTest1());
            XMLFormat xml2 = new XMLFormat();
            xml2.SetRoot("site", 0);
            xml2.Append(0, new XMLFormat.FileNode { id = 12, name = "news" });

            Boolean isEqual = xml.Equals(xml2); 
            
            Assert.True(isEqual);
        }
        [Test]
        public void XMLFormat_Append_Test1() {
            Assert.True(false);
        }
    }
}
