using System;
using PathInfo = FindFormat.PathInfo;
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
            ff.pathInfo = new PathInfo[6];
            ff.pathInfo[0] = new PathInfo("deo", 0);
            ff.pathInfo[1] = new PathInfo("deo/sek", 36);
            ff.pathInfo[2] = new PathInfo("deo/sek/uig", 99);
            ff.pathInfo[3] = new PathInfo("deo/try", 121);
            ff.pathInfo[4] = new PathInfo("deo/sek/uig/xoj", 370);
            ff.pathInfo[5] = new PathInfo("deo/sek/qvg", 2092);

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
            ff.pathInfo = new PathInfo[2];
            ff.pathInfo[0] = new PathInfo("site", 0);
            ff.pathInfo[1] = new PathInfo("site/news", 12);

            var resXml = tr.Transform(ff);

            Assert.True(resXml.Equals(xml));
        }
    }
}