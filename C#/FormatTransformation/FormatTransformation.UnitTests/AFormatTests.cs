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
            xml2.SetRoot("site");
            xml2.Append(0, new XMLFormat.FileNode { id = 12, name = "news" });

            Boolean isEqual = xml.Equals(xml2); 
            
            Assert.True(isEqual);
        }
        [Test]
        public void XMLFormat_Append_Test1() {
            XMLFormat xml = new XMLFormat();
            xml.SetRoot("a");
            
            xml.Append(0, new XMLFormat.FileNode() { name = "b", id = 1 });

            var node = xml.GetNode(1);
            Assert.True(node.name == "b");
        }
        [Test]
        public void XMLFormat_GetNode_Test1() {
            XMLFormat xml = new XMLFormat(new XMLFormatInitializerTest1());

            var node = xml.GetNode(12);

            Assert.AreEqual("news", node.name);
        } 
        [Test]
        public void XMLFormatInitializerTest2() {
            XMLFormat xml = new XMLFormat(new XMLFormatInitializerTest2());
            XMLFormat xml2 = new XMLFormat();
            xml2.SetRoot("deo");
            xml2.Append(0, new XMLFormat.DirNode { id = 36, name = "sek" });
            xml2.Append(0, new XMLFormat.FileNode { id = 121, name = "try" });
            xml2.Append(36, new XMLFormat.DirNode { id = 99, name = "uig" });
            xml2.Append(36, new XMLFormat.FileNode { id = 2092, name = "qvg" });
            xml2.Append(99, new XMLFormat.FileNode { id = 370, name = "xoj" });

            Boolean isEqual = xml.Equals(xml2);

            Assert.True(isEqual);
        }
        [Test]
        public void XMLFormat_GetNode_Test2() {
            XMLFormat xml = new XMLFormat(new XMLFormatInitializerTest2());

            var node = xml.GetNode(0);

            Assert.AreEqual("deo", node.name);
        }
        [Test]
        public void XMLFormat_GetNode_Test3() {
            XMLFormat xml = new XMLFormat(new XMLFormatInitializerTest2());

            var node = xml.GetNode(36);

            Assert.AreEqual("sek", node.name);
        }
        [Test]
        public void XMLFormat_GetNode_Test4() {
            XMLFormat xml = new XMLFormat(new XMLFormatInitializerTest2());

            var node = xml.GetNode(99);

            Assert.AreEqual("uig", node.name);
        }
    }
}
