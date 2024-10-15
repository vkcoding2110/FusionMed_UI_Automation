using OpenQA.Selenium;
using UIAutomation.DataObjects.FMS.Home;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Components;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Home
{
    internal class RequestStaffPo : FmsBasePo
    {
        public RequestStaffPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By FacilityNameTextBox = By.CssSelector("input#input_1");
        private readonly By YourNameTextBox = By.CssSelector("input#input_2");
        private readonly By PhoneNumberTextBox = By.CssSelector("input#input_3");
        private readonly By EmailTextBox = By.CssSelector("input#input_4");
        private readonly By ProfessionalTypeDropdown = By.CssSelector("select#input_5");
        private readonly By SpecialtyDropdown = By.CssSelector("select#input_6");
        private readonly By JobTypeDropdown = By.CssSelector("select#input_7");
        private readonly By StartDateDateTimePicker = By.XPath("//label[contains(text(),'Start date')]/preceding-sibling::input[contains(@class,'form-control input')]");
        private readonly By MessageTextBox = By.CssSelector("textarea#input_9");
        private readonly By SubmitButton = By.CssSelector("button#button_53");
        private readonly By RequestStaffSuccessText = By.CssSelector("div[class*='privacyPolicystyles'] h1");
        private readonly By WorkPlaceSafetySolution = By.XPath("//div[contains(@class,'StaffingFeatureWrapper')]//div//a//h5[contains(text(),'Workplace Safety Solutions')]");

        //Device element
        private readonly By StartDateDateTimePickerDevice = By.XPath("//label[contains(text(),'Start date')]/preceding-sibling::input[contains(@class,'form-control input')]//parent::div");

        public void AddRequestStaffData(RequestStaff requestStaff)
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(FacilityNameTextBox));
            EnterFacilityName(requestStaff.FacilityName);
            EnterYourName(requestStaff.YourName);
            EnterPhoneNumber(requestStaff.PhoneNumber);
            EnterEmailId(requestStaff.Email);
            SelectProfessionalTypeDopDown(requestStaff.ProfessionalType);
            SelectSpecialtyDropDown(requestStaff.Specialty);
            SelectJobTypeDropDown(requestStaff.JobType);
            EnterStartDate(requestStaff);
            Wait.UntilElementVisible(MessageTextBox).EnterText(requestStaff.Message);

        }

        public void ClickOnSubmitButton()
        {
            Wait.UntilElementClickable(SubmitButton).ClickOn();
        }

        public string GetRequestStaffSuccessText()
        {
            return Wait.UntilElementVisible(RequestStaffSuccessText).GetText();
        }

        public void ClickOnWorkPlaceSafetySolution()
        {
            Wait.UntilElementClickable(WorkPlaceSafetySolution).ClickOn();
        }

        public void EnterFacilityName(string facility)
        {
            Wait.UntilElementVisible(FacilityNameTextBox).EnterText(facility);
        }

        public void EnterYourName(string name)
        {
            Wait.UntilElementVisible(YourNameTextBox).EnterText(name,true);
        }

        public void EnterPhoneNumber(string phoneNumber)
        {
            Wait.UntilElementVisible(PhoneNumberTextBox).EnterText(phoneNumber, true);
        }

        public void EnterEmailId(string email)
        {
            Wait.UntilElementVisible(EmailTextBox).EnterText(email,true);
        }

        public void SelectProfessionalTypeDopDown(string professionalType)
        {
            if (BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Safari))
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(PhoneNumberTextBox));
            }
            Wait.UntilElementClickable(ProfessionalTypeDropdown).SelectDropdownValueByText(professionalType);
        }

        public void ClearProfessionalTypeDropDown()
        {
            Wait.UntilElementClickable(ProfessionalTypeDropdown).SelectDropdownValueByIndex(0);
        }

        public void SelectSpecialtyDropDown(string specialty)
        {
            if (BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Safari))
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(PhoneNumberTextBox));
            }
            Wait.UntilElementClickable(SpecialtyDropdown).SelectDropdownValueByText(specialty);
        }

        public void SelectJobTypeDropDown(string jobType)
        {
            Wait.UntilElementClickable(JobTypeDropdown).SelectDropdownValueByText(jobType);
        }

        public void ClearJobTypeDropDown()
        {
            Wait.UntilElementClickable(JobTypeDropdown).SelectDropdownValueByIndex(0);
        }

        public void EnterStartDate(RequestStaff requestStaff)
        {
            var datePicker = new DatePickerPo(Driver);

            Wait.UntilElementClickable(BaseTest.PlatformName != PlatformName.Web ? StartDateDateTimePickerDevice : StartDateDateTimePicker).ClickOn();
            datePicker.SelectDate(requestStaff.StartDate, BaseTest.PlatformName != PlatformName.Web ? StartDateDateTimePickerDevice : StartDateDateTimePicker);
        }

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FmsUrl}client/");
            WaitUntilMpPageLoadingIndicatorInvisible();
        }
    }
}