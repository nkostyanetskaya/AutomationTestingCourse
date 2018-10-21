using System;
using System.IO;
using GoogleTranslateTests.PageObjects;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
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

        private MainPage _mainPage => new MainPage(_driver);
        private void TakeScreenshotOnFailure()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenshotsDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "Screenshots");
                Directory.CreateDirectory(screenshotsDirectory);
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                var screenshotName = Path.Combine(screenshotsDirectory, TestContext.CurrentContext.Test.Name + "-" + timestamp + ".png");
                screenshot.SaveAsFile(screenshotName);
            }
        }
        [SetUp]
        public void Initialize()
        {
            _driver = new ChromeDriver { Url = "https://translate.google.com/" };
        }

        [Test]
        public void SourceLanguageSelectionTest()
        {
           _mainPage.SourceLanguageSelector.Click();
           _mainPage.GetButtonByText(_mainPage.SourceLanguageMenu, SourceLanguageName).Click();
            var buttonClass = _mainPage.GetButtonByText(_mainPage.SourceLanguagePanel, SourceLanguageName).GetAttribute("class");

            Assert.True(buttonClass.Contains("jfk-button-checked"));
        }

        [Test]
        public void TargetLanguageSelectionTest()
        {
            _mainPage.TargetLanguageSelector.Click();
            _mainPage.GetButtonByText(_mainPage.TargetLanguageMenu, TargetLanguageName).Click();
            var buttonClass = _mainPage.GetButtonByText(_mainPage.TargetLanguagePanel, TargetLanguageName).GetAttribute("class");

            Assert.True(buttonClass.Contains("jfk-button-checked"));
        }

        [Test]
        public void SwapLanguageTest()
        {
            _mainPage.SourceLanguageSelector.Click();
            _mainPage.GetButtonByText(_mainPage.SourceLanguageMenu, SourceLanguageName).Click();

            _mainPage.TargetLanguageSelector.Click();
            _mainPage.GetButtonByText(_mainPage.TargetLanguageMenu, TargetLanguageName).Click();

            _mainPage.SwapLanguagesButton.Click();

            var sourceLanguageButtonClass = _mainPage
                .GetButtonByText(_mainPage.SourceLanguagePanel, TargetLanguageName)
                .GetAttribute("class");
            var targetLanguageButtonClass = _mainPage
                .GetButtonByText(_mainPage.TargetLanguagePanel, SourceLanguageName)
                .GetAttribute("class");

            Assert.True(sourceLanguageButtonClass.Contains("jfk-button-checked"));
            Assert.True(targetLanguageButtonClass.Contains("jfk-button-checked"));
        }

        [Test]
        public void DetectLanguageTest()
        {
            _mainPage.SourceTextArea.SendKeys(SourceText);
            var detectLanguageButton = _mainPage.GetButtonByText(_mainPage.SourceLanguagePanel, DetectLanguageButtonName);
            detectLanguageButton.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => detectLanguageButton.Text != DetectLanguageButtonName);

            Assert.AreEqual(DetectGermanLanguageButtonName, detectLanguageButton.Text);
        }

        [Test]
        public void TranslateTest()
        {
            _mainPage.SourceLanguageSelector.Click();
            _mainPage.GetButtonByText(_mainPage.SourceLanguageMenu, SourceLanguageName).Click();

            _mainPage.TargetLanguageSelector.Click();
            _mainPage.GetButtonByText(_mainPage.TargetLanguageMenu, TargetLanguageName).Click();

            _mainPage.SourceTextArea.SendKeys(SourceText);

            _mainPage.TranslateButton.Click();

            Assert.True(_mainPage.TargetTextArea.Text.Equals(TargetText));
        }
         [TearDown]
        public void EndTest()
        {
            TakeScreenshotOnFailure();
            _driver.Close();
        }

    
    }
}
