using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.AccountSettings
{
    internal class CloseAccountPopupPo : FmpBasePo
    {
        public CloseAccountPopupPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By HeaderText = By.XPath("//div[contains(@class,'DeleteAccountWrapper')]//h3");
        private readonly By CloseIcon = By.CssSelector("div[class*='CloseContainer'] svg[class*='CloseX']");
        private readonly By DisableAccountRadioButton = By.XPath("//h4[text()='Disable my account for now ']/parent::span/preceding-sibling::span");
        private readonly By PermanentlyCloseAccountRadioButton = By.XPath("//h4[text()='Permanently close my account ']/parent::span/preceding-sibling::span");
        private readonly By ContinueButton = By.XPath("//span[text()='Continue']/parent::button");
        private readonly By CancelButton = By.XPath("//span[text()='cancel']/parent::button");

        public string GetCloseAccountHeaderText()
        {
            return Wait.UntilElementVisible(HeaderText).GetText();
        }

        public bool IsCloseAccountPopupIsPresent()
        {
            return Wait.IsElementPresent(DisableAccountRadioButton, 3);
        }

        public void ClickOnPermanentlyCloseAccountRadioButton()
        {
            Wait.UntilElementClickable(PermanentlyCloseAccountRadioButton).ClickOn();
        }

        public void ClickOnContinueButton()
        {
            Wait.UntilElementClickable(ContinueButton).ClickOn();
        }

        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
        }

    }
}