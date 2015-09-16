using System;
using PathInfo = FindFormat.PathInfo;
using NUnit.Framework;

namespace FormatTransformation.UnitTests {
    [TestFixture]
    class XmlToFindTransformationTests {
        [Test]
        public void XMLFormatTest2_ToFindFormat_Test() {
            XMLFormat xml = new XMLFormat(new XMLFormatInitializerTest2());
            ITransformation tr = new XmlToFindTransformation();
            FindFormat ff = new FindFormat();
            ff.n = 6;
            ff.pathInfo = new PathInfo[6];
            ff.pathInfo[0] = new PathInfo("deo", 0);
            ff.pathInfo[1] = new PathInfo("deo/sek", 36);
            ff.pathInfo[2] = new PathInfo("deo/sek/uig", 99);
            ff.pathInfo[3] = new PathInfo("deo/try", 121);
            ff.pathInfo[4] = new PathInfo("deo/sek/uig/xoj", 370);
            ff.pathInfo[5] = new PathInfo("deo/sek/qvg", 2092);
            var find = tr.Transform(xml);

            Assert.True(ff.Equals(find));
        }
    }
}