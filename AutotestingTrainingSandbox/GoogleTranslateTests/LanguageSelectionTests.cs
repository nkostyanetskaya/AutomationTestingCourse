using GoogleTranslateTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GoogleTranslateTests
{
    internal class LanguageSelectionTests
    {
        private const string LanguageName = "German";

        private IWebDriver _driver;

        [SetUp]
        public void Initialize()
        {
            _driver = new ChromeDriver();
            _driver.Url = "https://translate.google.com/";
        }

        [Test]
        public void SourceLanguageSelectionTest()
        {
            var mainPage = new MainPage(_driver);
            mainPage.SourceLanguageSelector.Click();
            mainPage.GetButtonByText(mainPage.SourceLanguageMenu, LanguageName).Click();
            var buttonClass = mainPage.GetButtonByText(mainPage.SourceLanguagePanel, LanguageName).GetAttribute("class");
            Assert.True(buttonClass.Contains("jfk-button-checked"));
        }

        [Test]
        public void TargetLanguageSelectionTest()
        {
            var mainPage = new MainPage(_driver);
            mainPage.TargetLanguageSelector.Click();
            mainPage.GetButtonByText(mainPage.TargetLanguageMenu, LanguageName).Click();
            var buttonClass = mainPage.GetButtonByText(mainPage.TargetLanguagePanel, LanguageName).GetAttribute("class");
            Assert.True(buttonClass.Contains("jfk-button-checked"));
        }

        [TearDown]
        public void EndTest()
        {
            _driver.Close();
        }
    }
}
