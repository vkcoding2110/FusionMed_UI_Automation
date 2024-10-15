using OpenQA.Selenium;
using UIAutomation.DataObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Home.Footer
{
    internal class ClinicalContactUsPo : FmsBasePo
    {
        public ClinicalContactUsPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By FirstNameTextBox = By.CssSelector("input#input_1");
        private readonly By LastNameTextBox = By.CssSelector("input#input_2");
        private readonly By EmailTextBox = By.CssSelector("input#input_4");
        private readonly By PhoneTextBox = By.CssSelector("input#input_3");
        private readonly By MessageTextArea = By.CssSelector("textarea#input_5");
        private readonly By SubmitButton = By.CssSelector("button#button_125");

        //Thank You page
        private readonly By ThankYouPageHeaderText = By.CssSelector("div[class*='privacyPolicystyles__HeaderBackground'] h1");
        private readonly By ThankYouPageMessage = By.CssSelector("div[class*='privacyPolicystyles__HeaderBackground'] h2");

        public void EnterClinicalContactUsData(ContactUsForm contactUs)
        {
            EnterFirstName(contactUs.Name);
            EnterLastName(contactUs.LastName);
            EnterEmail(contactUs.Email);
            EnterMessage(contactUs.Message);
            Wait.UntilElementVisible(PhoneTextBox).EnterText(contactUs.Phone);
            ClickOnSubmitButton();
        }

        public void EnterFirstName(string name)
        {
            Wait.UntilElementVisible(FirstNameTextBox).EnterText(name, true);
        }

        public void EnterLastName(string name)
        {
            Wait.UntilElementVisible(LastNameTextBox).EnterText(name, true);
        }

        public void EnterEmail(string email)
        {
            Wait.UntilElementVisible(EmailTextBox).EnterText(email, true);
        }

        public void EnterMessage(string message)
        {
            Wait.UntilElementVisible(MessageTextArea).EnterText(message, true);
        }

        public void ClickOnSubmitButton()
        {
            Wait.UntilElementClickable(SubmitButton).ClickOn();
            WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            WaitUntilMpPageLoadingIndicatorInvisible();
        }

        public string GetThankYouPageHeaderText()
        {
            return Wait.UntilElementVisible(ThankYouPageHeaderText).GetText();
        }

        public string GetThankYouPageMessageText()
        {
            return Wait.UntilElementVisible(ThankYouPageMessage).GetText();
        }
    }
}
