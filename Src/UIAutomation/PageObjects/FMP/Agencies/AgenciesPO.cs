using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Agencies;
using UIAutomation.Tests;
using UIAutomation.Enum;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies
{

    internal class AgenciesPo : FmpBasePo
    {
        public AgenciesPo(IWebDriver driver) : base(driver)
        {
        }

        //Partner With Us 
        private readonly By AgenciesHeaderText = By.CssSelector("div[class*='HeaderMessage'] h1");
        private readonly By PartnerNameTextBox = By.XPath("//div[contains(@class,'MuiFilledInput')]/input[@name='name']");
        private readonly By PartnerTitleTextBox = By.XPath("//div[contains(@class,'MuiFilledInput')]/input[@name='title']");
        private readonly By PartnerEmailTextBox = By.XPath("//div[contains(@class,'MuiFilledInput')]/input[@name='email']");
        private readonly By PartnerPhoneNumberTextBox = By.XPath("//div[contains(@class,'MuiFilledInput')]/input[@name='phoneNumber']");
        private readonly By PartnerAgencyNameTextBox = By.XPath("//div[contains(@class,'MuiFilledInput')]/input[@name='agencyName']");
        private readonly By PartnerNumberOfRecruitersTextBox = By.XPath("//div[contains(@class,'MuiFilledInput')]/input[@name='numberOfRecruiters']");
        private readonly By PartnerSubmitButton = By.XPath("//button[contains(@class,'ButtonStyled')]/span[text()='Request Partner Information']");
        private readonly By PartnerWithUsSuccessMessage = By.XPath("//div[contains(@class,'MessageContainer')]/div/following-sibling::h5");
        private readonly By NameValidationMessage = By.XPath("//div[contains(@class,'PartnerWithUsFormContainer')]//div[1]/div/following-sibling::p");
        private readonly By TitleValidationMessage = By.XPath("//div[contains(@class,'PartnerWithUsFormContainer')]//div[2]/div/following-sibling::p");
        private readonly By EmailValidationMessage = By.XPath("//div[contains(@class,'PartnerWithUsFormContainer')]//div[3]/div/following-sibling::p");
        private readonly By PhoneNumberValidationMessage = By.XPath("//div[contains(@class,'PartnerWithUsFormContainer')]//div[4]/div/following-sibling::p");
        private readonly By AgencyNameValidationMessage = By.XPath("//div[contains(@class,'PartnerWithUsFormContainer')]//div[5]/div/following-sibling::p");

        private readonly By LetsTalkButton = By.XPath("//div[contains(@class,'HeaderMessage')]/a/span[contains(text(),'Talk')]");
        private readonly By AgencyTitle = By.XPath("//div[contains(@class,'AgencyHeaderDetailsWrapper')]/h1");

        public string GetAgencyTitleText()
        {
            return Wait.UntilElementVisible(AgencyTitle).GetText();
        }

        public string GetAgenciesPageHeaderText()
        {
            return Wait.UntilElementVisible(AgenciesHeaderText).GetText();
        }

        public void ClickOnLetsTalkButton()
        {
            if (BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Safari))
            {
                Wait.UntilElementVisible(LetsTalkButton);
                Driver.JavaScriptClickOn(Wait.UntilElementExists(LetsTalkButton));
            }
            else
            {
                Wait.UntilElementClickable(LetsTalkButton);
                Wait.UntilElementVisible(LetsTalkButton).ClickOn();
            }
        }

        public string GetNameValidationMessageText()
        {
            return Wait.UntilElementVisible(NameValidationMessage).GetText();
        }

        public string GetTitleValidationMessageText()
        {
            return Wait.UntilElementVisible(TitleValidationMessage).GetText();
        }

        public string GetEmailValidationMessageText()
        {
            return Wait.UntilElementVisible(EmailValidationMessage).GetText();
        }

        public string GetPhoneNumberValidationMessageText()
        {
            return Wait.UntilElementVisible(PhoneNumberValidationMessage).GetText();
        }

        public string GetAgencyNameValidationMessageText()
        {
            return Wait.UntilElementVisible(AgencyNameValidationMessage).GetText();
        }

        public void AddDataInPartnerWithUsForm(PartnerWithUs partnerWithUs)
        {
            EnterName(partnerWithUs.Name);
            EnterTitle(partnerWithUs.Title);
            EnterWorkEmail(partnerWithUs.WorkEmail);
            EnterPhoneNumber(partnerWithUs.PhoneNumber);
            EnterAgencyName(partnerWithUs.AgencyName);
            EnterNumberOfRecruiters(partnerWithUs.NumberOfRecruiters);
            ClickOnRequestPartnerInformationButton();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnRequestPartnerInformationButton()
        {
            Wait.UntilElementClickable(PartnerSubmitButton).ClickOn();
        }

        public string GetSuccessMessageText()
        {
            return Wait.UntilElementVisible(PartnerWithUsSuccessMessage).GetText();
        }

        public void EnterName(string name)
        {
            Wait.UntilElementVisible(PartnerNameTextBox).EnterText(name, true);
        }

        public void EnterTitle(string title)
        {
            Wait.UntilElementVisible(PartnerTitleTextBox).EnterText(title, true);
        }

        public void EnterWorkEmail(string email)
        {
            Wait.UntilElementVisible(PartnerEmailTextBox).EnterText(email, true);
        }

        public void EnterPhoneNumber(string phoneNumber)
        {
            Wait.UntilElementVisible(PartnerPhoneNumberTextBox).EnterText(phoneNumber, true);
        }

        public void EnterAgencyName(string agencyName)
        {
            Wait.UntilElementVisible(PartnerAgencyNameTextBox).EnterText(agencyName, true);
        }

        public void EnterNumberOfRecruiters(string numberOfRecruiter)
        {
            Wait.UntilElementVisible(PartnerNumberOfRecruitersTextBox).EnterText(numberOfRecruiter, true);
        }
    }
}
