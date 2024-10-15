using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Jobs
{
    internal class ThankYouPagePo : FmpBasePo
    {
        public ThankYouPagePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By GoToLoginAndCountMeInButton = By.XPath("//button[contains(@class,'ConfirmButton')]");
        private readonly By ThanksMessage = By.XPath("//div//h3[contains(@class,'ThankYou')]");
        private readonly By GoToProfileButton = By.XPath("//a[contains(@class,'ConfirmButton')]");
        private readonly By CloseButton = By.XPath("//div[contains(@class,'QuickApplySuccessWrapper')]//p[text()='close']");
        private readonly By JustMissedIt = By.XPath("//div[contains(@class,'ApplicationCapWrapper')]//h3[text()='Just missed it :(']");
        private readonly By ApplyAnywayButton = By.XPath("//button[contains(@class,'ApplyAnyway')]");

        public void ClickOnGoToLoginAndCountMeInButton()
        {
            Wait.UntilElementClickable(GoToLoginAndCountMeInButton).ClickOn();
        }
        public void ClickOnGoToProfileButton()
        {
            Wait.UntilElementClickable(GoToProfileButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public string GetThanksMessage()
        {
            return Wait.UntilElementVisible(ThanksMessage).GetText().RemoveEndOfTheLineCharacter();
        }

        public bool IsApplyAnywayButtonDisplayed()
        {
            return Wait.IsElementPresent(ApplyAnywayButton, 3);
        }
        public void ClickOnApplyAnywayButton()
        {
            Wait.UntilElementClickable(ApplyAnywayButton).ClickOn();
        }

        public void ClickOnCloseButton()
        {
            Wait.UntilElementClickable(CloseButton).ClickOn();
        }
        public bool IsJustMissedItPopupIsPresent()
        {
            return Wait.IsElementPresent(JustMissedIt, 3);
        }
    }
}
