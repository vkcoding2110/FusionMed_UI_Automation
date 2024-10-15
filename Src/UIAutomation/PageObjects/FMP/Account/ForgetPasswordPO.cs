using OpenQA.Selenium;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Account
{
    internal class ForgetPasswordPo : FmpBasePo
    {
        public ForgetPasswordPo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By ForgetPasswordPageHeader = By.XPath("//div[@class='forgot-password-container']//form//h5");
        private readonly By ForgotPasswordText = By.CssSelector("div[class='forgot-password-container'] h6");

        //Find Account Button
        private readonly By FindAccountButton = By.XPath("//button[text()='Find Account']");

        //Close Icon
        private readonly By CloseIcon = By.CssSelector("button[class*='close-icon-wrapper']");

        //Cancel Button
        private readonly By CancelButton = By.XPath("//div[@class='button-container']//button[text()='cancel']");

        private readonly By EmailTextBox = By.CssSelector("div[class*='form-label-group'] input#Email");

        public string GetForgetPasswordPageHeader()
        {
            return Wait.UntilElementVisible(ForgetPasswordPageHeader).GetText();
        }
        public string GetForgotPasswordText()
        {
            return Wait.UntilElementVisible(ForgotPasswordText).GetText();
        }

        public void ClickOnFindAccountButton()
        {
            Wait.UntilElementVisible(FindAccountButton, 30);
            Wait.UntilElementClickable(FindAccountButton).ClickOn();
        }

        public void ClickOnForgotPasswordCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
        }

        public void ClickOnForgotPasswordCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
            Wait.WaitTillElementCountIsLessThan(CancelButton, 1);
        }

        public void EnterEmailOnForgotPasswordPage(Login login)
        {
            Wait.UntilElementVisible(EmailTextBox);
            Wait.UntilElementClickable(EmailTextBox).EnterText(login.Email);
        }

    }
}

