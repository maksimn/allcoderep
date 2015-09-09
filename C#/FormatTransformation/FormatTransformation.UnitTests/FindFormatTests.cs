using System;
using NUnit.Framework;

namespace FormatTransformation.UnitTests {
    [TestFixture]
    class FindFormatTests {
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
        public void FindFormat_Equals_Test1() {
            FindFormat find = new FindFormat(new FindFormatInitializerTest1());
            FindFormat find1 = new FindFormat();
            find1.n = 2;
            find1.id = new Int32[] { 0, 12 };
            find1.filepath = new String[] { "site", "site/news" };

            Boolean res = find.Equals(find1);

            Assert.True(res);
        }
        [Test]
        public void FindFormat_Equals_NotEqualObjectsTest() {
            FindFormat find = new FindFormat(new FindFormatInitializerTest1());
            FindFormat find1 = new FindFormat();
            find1.n = 2;
            find1.id = new Int32[] { 0, 10 };
            find1.filepath = new String[] { "site", "site/news" };

            Boolean res = find.Equals(find1);

            Assert.False(res);
        }
    }
}
