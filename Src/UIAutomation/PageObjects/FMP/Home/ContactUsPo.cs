using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Home;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Home
{
    internal  class ContactUsPo : FmpBasePo
    {
        public ContactUsPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By NameTextBox = By.XPath("//div[contains(@class,'ContactUsForm')]//input[@name='name']");
        private readonly By EmailTextBox = By.XPath("//div[contains(@class,'ContactUsForm')]//input[@name='email']");
        private readonly By PhoneTextBox = By.XPath("//div[contains(@class,'ContactUsForm')]//input[@name='phone']");
        private readonly By MessageTextArea = By.XPath("//div[contains(@class,'ContactUsForm')]//textarea[@name='message']");
        private readonly By SubmitButton = By.XPath("//button[contains(@class,'ButtonStyled')]/span[text()='Submit']");
        private readonly By FusionMarketPlaceText = By.XPath("//div[contains(@class,'AddressInformation')]/p[1]");
        private readonly By InfoEmailHref = By.XPath("//div[contains(@class,'AddressInformation')]/p[6]/a");

        //Validation Message 
        private readonly By NameValidationMessage = By.XPath("//input[@name='name']/parent::div/following-sibling::p");
        private readonly By EmailValidationMessage = By.XPath("//input[@name='email']/parent::div/following-sibling::p");
        private readonly By MessageValidationMessage = By.XPath("//textarea[@name='message']/parent::div/following-sibling::p");

        //Thank You page
        private readonly By ThankYouPageHeaderText = By.XPath("//div[contains(@class,'MessageContainer')]/h5 ");
        private readonly By ThankYouPageMessage = By.XPath("//div[contains(@class,'MessageContainer')]/h5/following-sibling::p");

        //Device Element
        private readonly By MessageTextAreaDevice = By.XPath("//XCUIElementTypeOther[@name='Message or questions']/following-sibling::XCUIElementTypeOther/following-sibling::XCUIElementTypeTextView");
        private readonly By EmailTextBoxDevice = By.XPath("//XCUIElementTypeOther[@name='Email'][1]/following-sibling::XCUIElementTypeOther[1]/following-sibling::XCUIElementTypeTextField[1]");

        public void EnterContactUsData(ContactUsForm contactUs)
        {
            EnterName(contactUs.Name);
            EnterEmail(contactUs.Email);
            Wait.UntilElementVisible(PhoneTextBox).EnterText(contactUs.Phone);
            EnterMessage(contactUs.Message);
        }

        public void EnterName(string name)
        {
            Wait.UntilElementVisible(NameTextBox).EnterText(name, true);
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

        public void ClickOnSubmitButton()
        {
            Wait.UntilElementClickable(SubmitButton).ClickOn();
        }

        public string GetThankYouPageHeaderText()
        {
            return Wait.UntilElementVisible(ThankYouPageHeaderText).GetText();
        }

        public string GetThankYouPageMessageText()
        {
            return Wait.UntilElementVisible(ThankYouPageMessage).GetText().Replace("\n", "").Replace("\t", "");
        }

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FusionMarketPlaceUrl}contact/");
            WaitUntilFmpTextLoadingIndicatorInvisible();
        }

        public void WaitUntilFormLoaded()
        {
            Wait.UntilElementVisible(NameTextBox, 5);
        }

        public string GetNameValidationMessage()
        {
            return Wait.UntilElementVisible(NameValidationMessage).GetText();
        }

        public string GetEmailValidationMessage()
        {
            return Wait.UntilElementVisible(EmailValidationMessage).GetText();
        }

        public string GetMessageValidationMessage()
        {
            return Wait.UntilElementVisible(MessageValidationMessage).GetText();
        }

        public string GetFusionMarketPlaceText()
        {
            return Wait.UntilElementVisible(FusionMarketPlaceText).GetText();
        }

        public string GetInfoEmailHref()
        {
            return Wait.UntilElementVisible(InfoEmailHref).GetAttribute("href");
        }
    }
}
