using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.NativeApp.More
{
    internal class MoreMenuPo :FmpBasePo
    {
        public MoreMenuPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By LogoutOption = By.XPath("*//android.widget.TextView[@text='Log Out']");
        private readonly By YesButton = By.XPath("*//android.widget.Button[@text = 'Yes']");
        private readonly By LogoutSuccessText = By.XPath("*//android.view.View[@text ='Logout You are now logged out']");
        private readonly By ProfileOption = By.XPath("*//android.widget.TextView[@text='Profile']");
        private readonly By JobPreferencesOption = By.XPath("*//android.widget.TextView[@text='Job Preferences']");

        public void ClickOnLogoutOption()
        {
            Wait.UntilElementClickable(LogoutOption).ClickOn();
            if (Wait.IsElementPresent(YesButton,7))
            {
                Wait.UntilElementClickable(YesButton).ClickOn();
            }
            Wait.HardWait(5000);
        }

        public bool IsLoggedOutSuccessTextDisplayed()
        {
            return Wait.IsElementPresent(LogoutSuccessText, 5);
        }

        public void ClickOnProfileOption()
        {
            Wait.UntilElementClickable(ProfileOption).ClickOn();
            WaitUntilAppLoadingIndicatorInvisible();
        }

        public void ClickOnJobPreferencesOption()
        {
            Wait.UntilElementClickable(JobPreferencesOption).ClickOn();
            WaitUntilAppLoadingIndicatorInvisible();
        }
    }
}
