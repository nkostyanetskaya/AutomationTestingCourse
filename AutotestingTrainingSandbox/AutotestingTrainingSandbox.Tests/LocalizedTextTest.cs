using NUnit.Framework;
using AutotestingTrainingSandboxProject;

namespace AutotestingTrainingSandbox.Tests
{
    [TestFixture]
    public class LocalizedTextTest
    {
        [Test]
        public void TestToStringTestMethod()
        {
            var text = new LocalizedText(Language.English, "EnglishText");
            Assert.AreEqual("English: EnglishText", text.ToString());
        }
    }
}
