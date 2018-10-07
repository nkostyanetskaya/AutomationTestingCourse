using System;
using GoogleTranslateTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace GoogleTranslateTests
{
    internal class DetectLanguageTest
    {
        private const string LanguageName = "German";
        private const string SourceText = "Hallo Welt!";
        private const string DetectLanguageButtonName = "Detect language";
        private const string DetectGermanLanguageButtonName = LanguageName + " - detected";

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
            mainPage.SourceTextArea.SendKeys(SourceText);
            var detectLanguageButton = mainPage.GetButtonByText(mainPage.SourceLanguagePanel, DetectLanguageButtonName);
            detectLanguageButton.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => detectLanguageButton.Text != DetectLanguageButtonName);
            Assert.AreEqual(DetectGermanLanguageButtonName, detectLanguageButton.Text);
        }

        [TearDown]
        public void EndTest()
        {
            _driver.Close();
        }
    }
}
