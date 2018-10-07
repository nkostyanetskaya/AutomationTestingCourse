using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GoogleTranslateTests
{
    class LanguageSelectionTests
    {
        private const string LanguageName = "German";
        private const string LanguageCode = "de";

        private IWebDriver driver;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "https://translate.google.com/";
        }

        [Test]
        public void SourceLanguageSelectionTest()
        {
            driver.FindElement(By.Id("gt-sl-gms")).Click();
            driver.FindElement(By.XPath($"//div[@id='gt-sl-gms-menu']//div[contains(text(),'{LanguageName}')]")).Click();
            var buttonClass = driver.FindElement(By.CssSelector($"#gt-lang-left div[value={LanguageCode}]")).GetAttribute("class");
            Assert.True(buttonClass.Contains("jfk-button-checked"));
        }
        [Test]
        public void TargetLanguageSelectionTest()
        {
            driver.FindElement(By.Id("gt-tl-gms")).Click();
            driver.FindElement(By.XPath($"//div[@id='gt-tl-gms-menu']//div[contains(text(),'{LanguageName}')]")).Click();
            var buttonClass = driver.FindElement(By.CssSelector($"#gt-lang-right div[value={LanguageCode}]")).GetAttribute("class");
            Assert.True(buttonClass.Contains("jfk-button-checked"));
        }

        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
