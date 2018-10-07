using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace GoogleTranslateTests.PageObjects
{
    class MainPage
    {
        [FindsBy(How = How.Id, Using = "gt-sl-gms")]
        public IWebElement SourceLanguageSelector { get; set; }

        [FindsBy(How = How.Id, Using = "gt-tl-gms")]
        public IWebElement TargetLanguageSelector { get; set; }

        [FindsBy(How = How.Id, Using = "gt-sl-gms-menu")]
        public IWebElement SourceLanguageMenu { get; set; }

        [FindsBy(How = How.Id, Using = "gt-tl-gms-menu")]
        public IWebElement TargetLanguageMenu { get; set; }

        [FindsBy(How = How.Id, Using = "gt-lang-left")]
        public IWebElement SourceLanguagePanel { get; set; }

        [FindsBy(How = How.Id, Using = "gt-lang-right")]
        public IWebElement TargetLanguagePanel { get; set; }

        public IWebElement GetLanguageButton(IWebElement languageMenu, string text)
        {
            return languageMenu.FindElement(By.XPath($"//div[contains(text(),'{text}')]"));
        }
    }
}
