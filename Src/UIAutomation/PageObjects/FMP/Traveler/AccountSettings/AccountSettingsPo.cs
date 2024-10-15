using OpenQA.Selenium;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.AccountSettings
{
    internal class AccountSettingsPo : FmpBasePo
    {
        public AccountSettingsPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By HeaderText = By.XPath("//div[contains(@class,'HeaderBanner')]//h1");
        private readonly By CloseAccountMessage = By.XPath("//div[contains(@class,'SectionWrapper')]//p[2]");
        private readonly By CloseAccountButton = By.XPath("//div[contains(@class,'SectionWrapper')]//button/span[text()='Close Account']");

        //Account Disabled Or Permanently close 
        private readonly By AccountDisabledAndClosedHeaderText = By.XPath("//div[contains(@class,'AccountClosedText')]//h4");
        private readonly By AccountDisabledAndClosedInformationText = By.XPath("//div[contains(@class,'AccountClosedText')]//p");

        public string GetAccountSettingsHeaderText()
        {
            return Wait.UntilElementVisible(HeaderText).GetText();
        }

        public string GetCloseAccountMessage()
        {
            return Wait.UntilElementVisible(CloseAccountMessage).GetText();
        }

        public void ClickOnCloseAccountButton()
        {
            Wait.UntilElementClickable(CloseAccountButton).ClickOn();
        }

        //Account Disabled Or Permanently close
        public string GetAccountDisabledAndClosedHeaderText()
        {
            return Wait.UntilElementVisible(AccountDisabledAndClosedHeaderText).GetText();
        }

        public string GetAccountDisabledAndClosedInformationText()
        {
            return Wait.UntilElementVisible(AccountDisabledAndClosedInformationText).GetText();
        }

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FusionMarketPlaceUrl}account-settings/");
            WaitUntilFmpTextLoadingIndicatorInvisible();
        }
    }
}

