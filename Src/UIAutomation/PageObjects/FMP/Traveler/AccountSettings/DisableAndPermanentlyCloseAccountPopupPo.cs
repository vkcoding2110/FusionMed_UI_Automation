using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.AccountSettings;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.AccountSettings
{
    internal class DisableAndPermanentlyCloseAccountPopupPo : FmpBasePo
    {
        public DisableAndPermanentlyCloseAccountPopupPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By BackArrowIcon = By.CssSelector("div[class*='FormNavigation'] svg[class*='BackArrow']");
        private readonly By CloseIcon = By.CssSelector("div[class*='CloseContainer'] svg[class*='CloseX']");
        private readonly By HeaderText = By.XPath("//div[contains(@class,'DeleteAccountWrapper')]//form/h3");
        private readonly By OtherReasonTextBox = By.CssSelector("input#other-info");
        private readonly By CommentTextArea = By.CssSelector("textarea#input_1");
        private readonly By CloseTextBox = By.CssSelector("input#confirm");
        private readonly By CloseTextBoxValidationMessageText = By.CssSelector("p#confirm-helper-text");
        private readonly By DisableAccountButton = By.XPath("//span[contains(text(),'Disable Account')]/parent::button");
        private readonly By PermanentlyCloseAccountButton = By.XPath("//span[contains(text(),'Permanently Close Account')]/parent::button");
        private readonly By CloseAccountButton = By.XPath("//form[contains(@class,'FormStyled')]//button//span[contains(text(),'Close Account')]");
        private readonly By CancelButton = By.XPath("//span[contains(text(),'cancel')]/parent::button");
        private static By ReasonCheckBox(string item) => By.XPath($"//div[contains(@class,'CheckBoxGroupStyled')]//span[text()='{item}']/preceding-sibling::span");


        public void EnterCloseAccountDetails(CloseAccount closeAccount)
        {
            var closingAccountReason = closeAccount.Reasons;
            foreach (var listItem in closingAccountReason)
            {
                Wait.UntilElementClickable(ReasonCheckBox(listItem)).ClickOn();
            }
            Wait.UntilElementClickable(OtherReasonTextBox).EnterText(closeAccount.OtherReason, true);
            Wait.UntilElementClickable(CommentTextArea).EnterText(closeAccount.Comment, true);
            Wait.UntilElementClickable(CloseTextBox).EnterText(closeAccount.AccountText, true);
        }

        public void ClickOnBackArrowIcon()
        {
            Wait.UntilElementClickable(BackArrowIcon).ClickOn();
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
        }

        public string GetPopUpHeaderText()
        {
            return Wait.UntilElementVisible(HeaderText).GetText();
        }
        public bool IsPopupHeaderTextPresent()
        {
            return Wait.IsElementPresent(HeaderText, 3);
        }

        public void EnterTextIntoCloseTextBox(string text)
        {
            Wait.UntilElementClickable(CloseTextBox).EnterText(text, true);
        }

        public string GetValidationMessageForCloseTextBox()
        {
            return Wait.UntilElementVisible(CloseTextBoxValidationMessageText).GetText();
        }

        public void ClickOnDisableAccountButton()
        {
            Wait.UntilElementClickable(DisableAccountButton).ClickOn();
        }
        public bool IsDisableAccountButtonPresent()
        {
            return Wait.IsElementPresent(DisableAccountButton, 3);
        }
        public void ClickOnPermanentlyCloseAccountButton()
        {
            Wait.UntilElementClickable(PermanentlyCloseAccountButton).ClickOn();
        }
        public bool IsPermanentlyCloseAccountButtonPresent()
        {
            return Wait.IsElementPresent(PermanentlyCloseAccountButton, 3);
        }

        public void ClickOnCloseAccountButton()
        {
            Wait.UntilElementClickable(CloseAccountButton).ClickOn();
        }
        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
        }
    }
}
