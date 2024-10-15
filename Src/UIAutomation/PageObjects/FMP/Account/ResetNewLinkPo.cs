
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Account
{
    internal class ResetNewLinkPo : FmpBasePo
    {
        public ResetNewLinkPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By RequestNewLinkHeaderTest = By.CssSelector("div[class*='forgot-password-container'] h4");
        private readonly By RequestNewLinkMessageText = By.CssSelector("div[class*='forgot-password-container'] h6");
        private readonly By RequestNewLinkButton = By.XPath("//button[text()='Request New Link']");

        public string GetRequestNewLinkHeaderText()
        {
            return Wait.UntilElementVisible(RequestNewLinkHeaderTest).GetText();
        }

        public string GetRequestNewLinkMessageText()
        {
            return Wait.UntilElementVisible(RequestNewLinkMessageText).GetText();
        }

        public void ClickOnRequestNewLinkButton()
        {
            Wait.UntilElementClickable(RequestNewLinkButton).ClickOn();
            Wait.UntilElementInVisible(RequestNewLinkButton);
        }
    }
}
