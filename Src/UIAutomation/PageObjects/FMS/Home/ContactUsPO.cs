using OpenQA.Selenium;
using UIAutomation.DataObjects.FMS.Home;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Home
{
    internal class ContactUsPo : FmsBasePo
    {
        public ContactUsPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By NameTextBox = By.CssSelector("input#input_1");
        private readonly By EmailTextBox = By.CssSelector("input#input_6");
        private readonly By PhoneTextBox = By.CssSelector("input#input_3");
        private readonly By MessageTextArea = By.CssSelector("textarea#input_4");
        private readonly By SubmitButton = By.CssSelector("button#button_49");
        
        //Thank You page
        private readonly By ThankYouPageHeaderText = By.CssSelector("div[class*='privacyPolicystyles__HeaderBackground'] h1");
        private readonly By ThankYouPageMessage = By.CssSelector("div[class*='privacyPolicystyles__HeaderBackground'] h2");

        public void EnterContactUsData(ContactUsForm contactUs)
        {
            EnterName(contactUs.Name);
            EnterEmail(contactUs.Email);
            EnterMessage(contactUs.Message);
            Wait.UntilElementVisible(PhoneTextBox).EnterText(contactUs.Phone);
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
            return Wait.UntilElementVisible(ThankYouPageMessage).GetText();
        }

        public void WaitUntilFormLoaded()
        {
            Wait.UntilElementVisible(NameTextBox, 10);
        }

        public void EnterName(string name)
        {
            Wait.UntilElementVisible(NameTextBox).EnterText(name, true);
        }

        public void EnterEmail(string email)
        {
            Wait.UntilElementVisible(EmailTextBox).EnterText(email, true);
        }

        public void EnterMessage(string message)
        {
            Wait.UntilElementVisible(MessageTextArea).EnterText(message, true);
        }

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FmsUrl}about-us/contact-us/");
            WaitUntilMpPageLoadingIndicatorInvisible();
        }
    }
}


