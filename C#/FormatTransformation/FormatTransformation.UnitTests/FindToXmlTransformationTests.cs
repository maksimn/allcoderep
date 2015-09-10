using System;
using NUnit.Framework;

namespace FormatTransformation.UnitTests {
    [TestFixture]
    class FindToXmlTransformationTests {
        [Test]
        public void FindToXml_Test1() {
            XMLFormat xml = new XMLFormat(new XMLFormatInitializerTest2());
            ITransformation tr = new FindToXmlTransformation();
            FindFormat ff = new FindFormat();
            ff.n = 6;
            ff.id = new Int32[] { 0, 36, 99, 121, 370, 2092 };
            ff.filepath = new String[] {
                "deo",
                "deo/sek",
                "deo/sek/uig",
                "deo/try",
                "deo/sek/uig/xoj",
                "deo/sek/qvg"
            };

            var resXml = tr.Transform(ff);

            XmlToFindTransformation tr2 = new XmlToFindTransformation();
            var resFind = tr2.Transform(resXml);
            String s = resFind.ToString();

            Assert.True(resXml.Equals(xml));
        }
        [Test]
        public void FindToXml_Test2() {
            XMLFormat xml = new XMLFormat(new XMLFormatInitializerTest1());
            ITransformation tr = new FindToXmlTransformation();
            FindFormat ff = new FindFormat();
            ff.n = 2;
            ff.id = new Int32[] { 0, 12 };
            ff.filepath = new String[] { "site", "site/news" };

            var resXml = tr.Transform(ff);

            Assert.True(resXml.Equals(xml));
        }
    }
}