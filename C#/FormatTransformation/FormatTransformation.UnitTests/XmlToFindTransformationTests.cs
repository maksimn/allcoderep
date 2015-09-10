using System;
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
            ff.id = new Int32[] { 0, 36, 99, 121, 370, 2092 };
            ff.filepath = new String[] {
                "deo",
                "deo/sek",
                "deo/sek/uig",
                "deo/try",
                "deo/sek/uig/xoj",
                "deo/sek/qvg"
            };
            
            var find = tr.Transform(xml);

            Assert.True(ff.Equals(find));
        }
    }
}