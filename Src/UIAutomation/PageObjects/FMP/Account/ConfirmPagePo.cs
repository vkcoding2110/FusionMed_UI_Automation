using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Account
{
    internal class ConfirmPagePo : FmpBasePo
    {
        public ConfirmPagePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By ConfirmationTitle = By.CssSelector("div[class='confirmation-information'] h5");
        private readonly By ConfirmationText = By.CssSelector("div[class='confirmation-information'] h6");

        private readonly By ConfirmationLoginButton =
            By.XPath("//div[@class='button-container']//button[text()='Log In']");

        //Create New Password Confirmation
        private readonly By NewConfirmationTitle = By.CssSelector("div[class='reset-password-container'] h4");
        private readonly By NewConfirmationText = By.CssSelector("div[class='reset-password-container'] h6");

        private readonly By LoginButton =
            By.CssSelector("div[class='reset-password-container'] div.button-container button");

        //Create New Password Close Icon
        private readonly By CreateNewPasswordCloseIcon =
            By.CssSelector("div[class*='reset-password-container'] button[class*=close-icon]");

        public string GetConfirmationTitle()
        {
            return Wait.UntilElementVisible(ConfirmationTitle).GetText();
        }

        public string GetConfirmationText()
        {
            return Wait.UntilElementVisible(ConfirmationText).GetText();
        }

        public void ClickOnConfirmationLogInButton()
        {
            Wait.UntilElementVisible(ConfirmationLoginButton);
            Wait.UntilElementClickable(ConfirmationLoginButton).ClickOn();
            Wait.WaitTillElementCountIsLessThan(ConfirmationLoginButton, 1);
        }

        public string GetNewConfirmationTitle()
        {
            return Wait.UntilElementVisible(NewConfirmationTitle).GetText();
        }

        public string GetNewConfirmationText()
        {
            return Wait.UntilElementVisible(NewConfirmationText).GetText();
        }

        public void ClickOnLogInButton()
        {
            Wait.UntilElementClickable(LoginButton).ClickOn();
            Wait.WaitTillElementCountIsLessThan(LoginButton, 1);
        }

        public void ClickOnCreateNewPasswordConfirmationCloseIcon()
        {
            Wait.UntilElementClickable(CreateNewPasswordCloseIcon).ClickOn();
        }
    }
}
