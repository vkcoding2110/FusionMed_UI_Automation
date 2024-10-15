using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter.RateAndReview
{
    internal class RateAndReviewPo : FmpBasePo
    {
        public RateAndReviewPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By SignUpHereLink = By.XPath("//span[contains(@class,'LoginAction')][text()='Sign up here']");
        private readonly By LoginButton = By.XPath("//div[contains(@class,'ReviewPageContent')]//span[text()='Log In']");
        private readonly By BackButton = By.XPath("//span[text()='Back']//parent::button");

        public void ClickOnLoginButton()
        {
            Wait.UntilElementClickable(LoginButton).ClickOn();
        }
        public void ClickOnSignUpHereLink()
        {
            Wait.UntilElementClickable(SignUpHereLink).ClickOn();
        }

        public void ClickOnBackButton()
        {
            Wait.UntilElementClickable(BackButton).ClickOn();
        }
    }
}
