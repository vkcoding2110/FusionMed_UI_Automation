using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Home;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile
{

    internal class NeedHelpPo : FmpBasePo
    {
        public NeedHelpPo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By NeedHelpPopupHeaderText = By.XPath("//div[contains(@class,'HelpMeContent')]/div");
        private readonly By EmailTextBox = By.XPath("//input[@name='email']");
        private readonly By MessageTextArea = By.XPath("//div[contains(@class,'TextAreaStyled')]//textarea");
        private readonly By CloseIcon = By.XPath("//div[@role='dialog']/div[1]/button");
        private readonly By CancelButton = By.XPath("//button[contains(@class,'HelpMeButton')]//span[text()='cancel']");
        private readonly By SubmitButton = By.XPath("//button[contains(@class,'HelpMeButton')]//span[text()='Submit']");
        private readonly By SuccessPopupOkayButton = By.CssSelector("button[class*='OkayButton']");
        private readonly By SuccessPopupText = By.CssSelector("span[class*='QuickApplyText']");

        //Validation Message 
        private readonly By EmailValidationMessage = By.XPath("//input[@name='email']/parent::div/following-sibling::p");
        private readonly By MessageValidationMessage = By.XPath("//textarea[@name='comments']/parent::div/following-sibling::p");

        //Device elements
        private readonly By CloseIconDevice = By.XPath("//div[contains(@class,'HelpMeWrapper')]/button");
        private readonly By MessageTextAreaDevice = By.XPath("//XCUIElementTypeOther[@name='Details about your issue...']/following-sibling:: XCUIElementTypeOther/following-sibling:: XCUIElementTypeTextView");
        private readonly By EmailTextBoxDevice = By.XPath("//XCUIElementTypeOther[@name='Email']/following-sibling:: XCUIElementTypeOther/following-sibling:: XCUIElementTypeTextField");

        public void EnterHelpDetails(ContactUsForm helpDetail)
        {
            EnterMessage(helpDetail.Message);
        }
        public void ClickOnSubmitButton()
        {
            Wait.UntilElementClickable(SubmitButton).ClickOn();
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
            Wait.UntilElementInVisible(CloseIcon, 5);
        }

        public void WaitUntilPopupGetsOpen()
        {
            Wait.WaitUntilTextRefreshed(NeedHelpPopupHeaderText);
        }

        public bool IsNeedHelpPopUpDisplayed()
        {
            return Wait.IsElementPresent(BaseTest.PlatformName != PlatformName.Web ? CloseIconDevice : CloseIcon, 5);
        }

        public void ClickOnCancelButton()
        {
            Driver.JavaScriptClickOn(Wait.UntilElementExists(CancelButton));
            Wait.UntilElementInVisible(CancelButton, 5);
        }
        public bool IsInformationSubmittedPopupPresent()
        {
            return Wait.IsElementPresent(SuccessPopupOkayButton, 5);
        }
        public string GetNeedHelpPopUpSubmitSuccessText()
        {
            return Wait.UntilElementVisible(SuccessPopupText).GetText();
        }
        public void ClickOnOkayButtonOfSuccessPopup()
        {
            Wait.UntilElementClickable(SuccessPopupOkayButton).ClickOn();
            Wait.UntilElementInVisible(SuccessPopupOkayButton);
        }
        public void EnterEmail(string email)
        {
            if (BaseTest.PlatformName == PlatformName.Ios)
            {
                Driver.NativeAppClearText(EmailTextBoxDevice);
                Driver.NativeAppEnterText(EmailTextBoxDevice, email);
            }
            else
            {
                Wait.UntilElementVisible(EmailTextBox).EnterText(email, true);
            }

        }
        public void EnterMessage(string message)
        {
            if (BaseTest.PlatformName == PlatformName.Ios)
            {
                Driver.NativeAppClearText(MessageTextAreaDevice);
                Driver.NativeAppEnterText(MessageTextAreaDevice, message);
            }
            else
            {
                Wait.UntilElementVisible(MessageTextArea).EnterText(message, true);
            }
        }

        public void ScrollToCloseIcon()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(CloseIconDevice));
            }
        }

        public string GetEmailValidationMessage()
        {
            return Wait.UntilElementVisible(EmailValidationMessage).GetText();
        }

        public string GetMessageValidationMessage()
        {
            return Wait.UntilElementVisible(MessageValidationMessage).GetText();
        }
    }

}
