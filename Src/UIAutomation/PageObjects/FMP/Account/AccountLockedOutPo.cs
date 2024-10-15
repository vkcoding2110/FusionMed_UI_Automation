using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Account
{
    internal class AccountLockedOutPo : FmpBasePo
    {
        public AccountLockedOutPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By LockedOutPageHeaderText = By.CssSelector("div[class='forgot-password-container'] h4");
        private readonly By LockedOutPageMessageText = By.CssSelector("div[class='forgot-password-container'] h6");
        private readonly By LockedOutPageResetPasswordButton = By.XPath("//div[@class='forgot-password-container']//div/button[contains(text(),'Reset Password')]");

        public string GetLockedOutPageHeaderText()
        {
            return Wait.UntilElementVisible(LockedOutPageHeaderText).GetText();
        }

        public string GetLockedOutPageMessageText()
        {
            return Wait.UntilElementVisible(LockedOutPageMessageText).GetText();
        }

        public void ClickOnLockedOutPageResetPasswordButton()
        {
            Driver.JavaScriptClickOn(Wait.UntilElementExists(LockedOutPageResetPasswordButton));
            Wait.UntilElementInVisible(LockedOutPageResetPasswordButton);
        }
    }
}
