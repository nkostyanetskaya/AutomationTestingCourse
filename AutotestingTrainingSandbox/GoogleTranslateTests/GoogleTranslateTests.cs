using System;
using System.IO;
using System.Reflection;
using GoogleTranslateTests.PageObjects;
using log4net;
using log4net.Config;
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
        private MainPage _mainPage;

        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private void TakeScreenshot()
        {
            var screenshotsDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "Screenshots");
            Directory.CreateDirectory(screenshotsDirectory);

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
            var screenshotName = Path.Combine(screenshotsDirectory, TestContext.CurrentContext.Test.Name + "-" + timestamp + ".png");
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            
            screenshot.SaveAsFile(screenshotName);
        }
        
        [SetUp]
        public void Initialize()
        {
            XmlConfigurator.Configure();

            _driver = new ChromeDriver { Url = "https://translate.google.com/" };
            _mainPage = new MainPage(_driver);

            _log.Info("Browser is opened.");
            _log.Info("Test " + TestContext.CurrentContext.Test.Name + " started.");
        }

        [Test]
        public void SourceLanguageSelectionTest()
        {
            _mainPage.SourceLanguageSelector.Click();
            _log.Info($"{nameof(_mainPage.SourceLanguageSelector)} is clicked.");

            _mainPage.GetButtonByText(_mainPage.SourceLanguageMenu, SourceLanguageName).Click();
            _log.Info($"{nameof(_mainPage.SourceLanguageMenu)} is clicked.");
            var buttonClass = _mainPage.GetButtonByText(_mainPage.SourceLanguagePanel, SourceLanguageName).GetAttribute("class");
            Assert.True(buttonClass.Contains("jfk-button-checked"));
        }

        [Test]
        public void TargetLanguageSelectionTest()
        {
            _mainPage.TargetLanguageSelector.Click();
            _log.Info($"{nameof(_mainPage.TargetLanguageSelector)} is clicked.");
            _mainPage.GetButtonByText(_mainPage.TargetLanguageMenu, TargetLanguageName).Click();
            _log.Info($"{nameof(_mainPage.TargetLanguageMenu)} is clicked.");
            var buttonClass = _mainPage.GetButtonByText(_mainPage.TargetLanguagePanel, TargetLanguageName).GetAttribute("class");

            Assert.True(buttonClass.Contains("jfk-button-checked"));
        }

        [Test]
        public void SwapLanguageTest()
        {
            _mainPage.SourceLanguageSelector.Click();
            _log.Info($"{nameof(_mainPage.SourceLanguageSelector)} is clicked.");
            _mainPage.GetButtonByText(_mainPage.SourceLanguageMenu, SourceLanguageName).Click();
            _log.Info($"{SourceLanguageName} in {nameof(_mainPage.SourceLanguageMenu)} is clicked.");

            _mainPage.TargetLanguageSelector.Click();
            _log.Info($"{nameof(_mainPage.TargetLanguageSelector)} is clicked.");
            _mainPage.GetButtonByText(_mainPage.TargetLanguageMenu, TargetLanguageName).Click();
            _log.Info($"{TargetLanguageName} in {nameof(_mainPage.TargetLanguageMenu)} is clicked.");

            _mainPage.SwapLanguagesButton.Click();
            _log.Info($"{nameof(_mainPage.SwapLanguagesButton)} is clicked.");

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
            _log.Info($"{SourceText} entered in {nameof(_mainPage.SourceTextArea)}.");

            var detectLanguageButton = _mainPage.GetButtonByText(_mainPage.SourceLanguagePanel, DetectLanguageButtonName);
            detectLanguageButton.Click();
            _log.Info($"{nameof(detectLanguageButton)} is clicked.");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => detectLanguageButton.Text != DetectLanguageButtonName);

            Assert.AreEqual(DetectGermanLanguageButtonName, detectLanguageButton.Text);
        }

        [Test]
        public void TranslateTest()
        {
            _mainPage.SourceLanguageSelector.Click();
            _log.Info($"{nameof(_mainPage.SourceLanguageSelector)} is clicked.");
            _mainPage.GetButtonByText(_mainPage.SourceLanguageMenu, SourceLanguageName).Click();
            _log.Info($"{nameof(_mainPage.SourceLanguageMenu)} is clicked.");

            _mainPage.TargetLanguageSelector.Click();
            _log.Info($"{nameof(_mainPage.TargetLanguageSelector)} is clicked.");
            _mainPage.GetButtonByText(_mainPage.TargetLanguageMenu, TargetLanguageName).Click();
            _log.Info($"{nameof(_mainPage.TargetLanguageMenu)} is clicked.");

            _mainPage.SourceTextArea.SendKeys(SourceText);
            _log.Info($"{SourceText} entered in {nameof(_mainPage.SourceTextArea)}.");

            _mainPage.TranslateButton.Click();
            _log.Info($"{nameof(_mainPage.TranslateButton)} is clicked.");

            Assert.True(_mainPage.TargetTextArea.Text.Equals(TargetText));
        }

        [TearDown]
        public void EndTest()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Success)
            {
                _log.Info("Test " + TestContext.CurrentContext.Test.Name + " passed.");
            }
            else
            {
                TakeScreenshot();

                _log.Error("Test " + TestContext.CurrentContext.Test.Name + " failed.");
            }

            _driver.Close();
            _log.Info("Browser is closed.");
        }
    }
}
