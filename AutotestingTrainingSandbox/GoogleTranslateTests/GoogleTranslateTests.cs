using System;
using GoogleTranslateTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace GoogleTranslateTests
{
    internal class GoogleTranslateTests
    {
        private const string SourceLanguageName = "German";
        private const string SourceText = "Hallo Welt!";
        private const string TargetLanguageName = "English";
        private const string TargetText = "Hello World!";
        private const string DetectLanguageButtonName = "Detect language";
        private const string DetectGermanLanguageButtonName = SourceLanguageName + " - detected";

        private IWebDriver _driver;

        [SetUp]
        public void Initialize()
        {
            _driver = new ChromeDriver { Url = "https://translate.google.com/" };
        }

        [Test]
        public void SourceLanguageSelectionTest()
        {
            var mainPage = new MainPage(_driver);

            mainPage.SourceLanguageSelector.Click();
            mainPage.GetButtonByText(mainPage.SourceLanguageMenu, SourceLanguageName).Click();
            var buttonClass = mainPage.GetButtonByText(mainPage.SourceLanguagePanel, SourceLanguageName).GetAttribute("class");

            Assert.True(buttonClass.Contains("jfk-button-checked"));
        }

        [Test]
        public void TargetLanguageSelectionTest()
        {
            var mainPage = new MainPage(_driver);

            mainPage.TargetLanguageSelector.Click();
            mainPage.GetButtonByText(mainPage.TargetLanguageMenu, TargetLanguageName).Click();
            var buttonClass = mainPage.GetButtonByText(mainPage.TargetLanguagePanel, TargetLanguageName).GetAttribute("class");

            Assert.True(buttonClass.Contains("jfk-button-checked"));
        }

        [Test]
        public void SwapLanguageTest()
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

        [Test]
        public void DetectLanguageTest()
        {
            var mainPage = new MainPage(_driver);

            mainPage.SourceTextArea.SendKeys(SourceText);
            var detectLanguageButton = mainPage.GetButtonByText(mainPage.SourceLanguagePanel, DetectLanguageButtonName);
            detectLanguageButton.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => detectLanguageButton.Text != DetectLanguageButtonName);

            Assert.AreEqual(DetectGermanLanguageButtonName, detectLanguageButton.Text);
        }

        [Test]
        public void TranslateTest()
        {
            var mainPage = new MainPage(_driver);

            mainPage.SourceLanguageSelector.Click();
            mainPage.GetButtonByText(mainPage.SourceLanguageMenu, SourceLanguageName).Click();

            mainPage.TargetLanguageSelector.Click();
            mainPage.GetButtonByText(mainPage.TargetLanguageMenu, TargetLanguageName).Click();

            mainPage.SourceTextArea.SendKeys(SourceText);

            mainPage.TranslateButton.Click();

            Assert.True(mainPage.TargetTextArea.Text.Equals(TargetText));
        }

        [TearDown]
        public void EndTest()
        {
            _driver.Close();
        }
    }
}
