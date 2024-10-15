using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Account
{
    internal class CheckYourEmailPagePo : FmpBasePo
    {
        public CheckYourEmailPagePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By SignUpCheckYourEmailMessage = By.CssSelector("div[class='login-container'] form h6");

        //Forgot Password Confirmation
        private readonly By ForgotPasswordCheckYouEmailHeaderText =
            By.XPath("//div[@class='forgot-password-container']//h4");

        private readonly By ForgotPasswordCheckYourEmailMessageText =
            By.XPath("//div[@class='forgot-password-container']//h6");

        public string GetCheckYourEmailMessageText()
        {
            return Wait.UntilElementVisible(SignUpCheckYourEmailMessage).GetText();
        }

        public string GetForgotPasswordCheckYourEmailHeaderText()
        {
            return Wait.UntilElementVisible(ForgotPasswordCheckYouEmailHeaderText).GetText();
        }

        public string GetForgotPasswordCheckYourEmailMessage()
        {
            return Wait.UntilElementVisible(ForgotPasswordCheckYourEmailMessageText).GetText();
        }
    }
}
