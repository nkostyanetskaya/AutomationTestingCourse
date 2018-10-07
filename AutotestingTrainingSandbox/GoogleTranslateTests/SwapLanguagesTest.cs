using GoogleTranslateTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GoogleTranslateTests
{
    internal class SwapLanguagesTest
    {
        private const string SourceLanguageName = "German";
        private const string TargetLanguageName = "English";

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
            mainPage.GetButtonByText(mainPage.SourceLanguageMenu, SourceLanguageName).Click();

            mainPage.TargetLanguageSelector.Click();
            mainPage.GetButtonByText(mainPage.TargetLanguageMenu, TargetLanguageName).Click();

            mainPage.SwapLanguagesButton.Click();

            var sourceLanguageButtonClass = mainPage
                .GetButtonByText(mainPage.SourceLanguagePanel, TargetLanguageName)
                .GetAttribute("class");
            var targetLanguageButtonClass = mainPage
                .GetButtonByText(mainPage.TargetLanguagePanel, SourceLanguageName)
                .GetAttribute("class");

            Assert.True(sourceLanguageButtonClass.Contains("jfk-button-checked"));
            Assert.True(targetLanguageButtonClass.Contains("jfk-button-checked"));
        }

        [TearDown]
        public void EndTest()
        {
            _driver.Close();
        }
    }
}
