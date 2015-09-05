using System;
using NUnit.Framework;

namespace FormatTransformation.UnitTests {
    [TestFixture]
    class LogAnalyzerTests {
        [Test]
        public void IsValidLogFileName_BadExtension_ReturnsFalse() {
            var analyzer = new LogAnalyzer();

            Boolean res = analyzer.IsValidLogFileName("filewithbadextension.foo");

            Assert.False(res);
        }
    }
}
