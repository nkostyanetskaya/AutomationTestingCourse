using OpenQA.Selenium;

namespace GoogleTranslateTests.PageObjects
{
    internal class MainPage
    {
        private readonly IWebDriver _driver;

        public IWebElement SourceLanguageSelector => _driver.FindElement(By.Id("gt-sl-gms"));

        public IWebElement TargetLanguageSelector => _driver.FindElement(By.Id("gt-tl-gms"));

        public IWebElement SourceLanguageMenu => _driver.FindElement(By.Id("gt-sl-gms-menu"));

        public IWebElement TargetLanguageMenu => _driver.FindElement(By.Id("gt-tl-gms-menu"));

        public IWebElement SourceLanguagePanel => _driver.FindElement(By.Id("gt-lang-left"));

        public IWebElement TargetLanguagePanel => _driver.FindElement(By.Id("gt-lang-right"));

        public IWebElement SourceTextArea => _driver.FindElement(By.Id("source"));
        public IWebElement TargetTextArea => _driver.FindElement(By.Id("result_box"));

        public IWebElement SwapLanguagesButton => _driver.FindElement(By.Id("gt-swap"));
        public IWebElement TranslateButton => _driver.FindElement(By.Id("gt-submit"));

        public IWebElement GetButtonByText(IWebElement buttonContainer, string text)
        {
            return buttonContainer.FindElement(By.XPath($".//div[contains(text(),'{text}')]"));
        }

        public MainPage(IWebDriver driver)
        {
            _driver = driver;
        }
    }
}
