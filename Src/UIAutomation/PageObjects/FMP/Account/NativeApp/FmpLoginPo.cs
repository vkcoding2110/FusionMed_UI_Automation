using OpenQA.Selenium;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Account.NativeApp
{
    internal class FmpLoginPo : FmpBasePo
    {
        public FmpLoginPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By UsernameTextBox = By.XPath("//*[@resource-id='Username']");
        private readonly By PasswordTextBox = By.XPath("//*[@resource-id='Password']");
        private readonly By ContinueButton = By.XPath("//*[@text='Continue']");
        private readonly By SubmitButton = By.XPath("//*[@text='Submit']");

        //Profile
        private readonly By ProfileText = By.XPath(" *//android.widget.TextView[@text='HOME']");

        public void LoginToApplication(Login login)
        {
            if (!Wait.IsElementPresent(ProfileText,5))
            {
                Wait.UntilElementClickable(UsernameTextBox).EnterText(login.Email);
                Wait.UntilElementClickable(ContinueButton).Click();
                Wait.UntilElementClickable(PasswordTextBox).EnterText(login.Password);
                Wait.UntilElementClickable(SubmitButton).Click();
                WaitUntilAppLoadingIndicatorInvisible();
            }
            new AreaOfExpertisePo(Driver).FillAreaOfExpertiseForm();
        }

        public bool IsProfileTextDisplayed()
        {
            Wait.HardWait(5000);
            return Wait.IsElementPresent(ProfileText, 10);
        }
    }
}
