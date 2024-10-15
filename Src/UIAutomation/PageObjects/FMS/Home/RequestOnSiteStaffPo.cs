using System;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMS.Home;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Components;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Home
{
    internal class RequestOnSiteStaffPo : FmsBasePo
    {
        public RequestOnSiteStaffPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By FacilityNameTextBox = By.CssSelector("input#input_2");
        private readonly By YourNameTextBox = By.CssSelector("input#input_3");
        private readonly By PhoneNumberTextBox = By.CssSelector("input#input_4");
        private readonly By EmailTextBox = By.CssSelector("input#input_5");
        private readonly By SolutionTypeDropDown = By.CssSelector("select#input_6");
        private readonly By JobTypeDropDown = By.CssSelector("select#input_7");
        private readonly By StartDateDateTimePicker = By.XPath("//label[contains(text(),'Start date')]/preceding-sibling::input[contains(@class,'form-control input')]//parent::div");
        private readonly By MessageTextArea = By.CssSelector("textarea#input_9");
        private readonly By SubmitButton = By.CssSelector("button#button_114");
        private readonly By RequestOnSiteStaffSuccessText = By.CssSelector("div[class*='privacyPolicystyles'] h1");

        public void ClickOnSubmitButton()
        {
            Wait.UntilElementVisible(SubmitButton).ClickOn();
        }

        public void EnterRequestOnSiteStaffDetails(RequestStaff requestStaff)
        {
            EnterFacilityName(requestStaff.FacilityName);
            EnterYourName(requestStaff.YourName);
            EnterPhoneNumber(requestStaff.PhoneNumber);
            EnterEmailAddress(requestStaff.Email);
            SelectSolutionTypeDropDown(requestStaff.SolutionType);
            SelectJobTypeDropDown(requestStaff.JobType);
            EnterStartDate(requestStaff.StartDate);
            EnterMessage(requestStaff.Message);
        }

        public void EnterFacilityName(string facility)
        {
            Wait.UntilElementVisible(FacilityNameTextBox,20).EnterText(facility, true);
        }

        public void EnterYourName(string name)
        {
            Wait.UntilElementVisible(YourNameTextBox).EnterText(name, true);
        }

        public void EnterPhoneNumber(string phoneNumber)
        {
            Wait.UntilElementVisible(PhoneNumberTextBox).EnterText(phoneNumber, true);
        }

        public void EnterEmailAddress(string email)
        {
            Wait.UntilElementVisible(EmailTextBox).EnterText(email, true);
        }

        public void SelectSolutionTypeDropDown(string solutionType)
        {
            Wait.UntilElementClickable(SolutionTypeDropDown).SelectDropdownValueByText(solutionType);
        }

        public void ClearSolutionTypeDropDown()
        {
            Wait.UntilElementClickable(SolutionTypeDropDown).SelectDropdownValueByIndex(0);
        }

        public void SelectJobTypeDropDown(string jobType)
        {
            if (BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Safari))
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SolutionTypeDropDown));
            }
            Wait.UntilElementClickable(JobTypeDropDown).SelectDropdownValueByText(jobType);
        }

        public void ClearJobTypeDropDown()
        {
            Wait.UntilElementClickable(JobTypeDropDown).SelectDropdownValueByIndex(0);
        }

        public void EnterStartDate(DateTime startDate)
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.SelectDate(startDate, StartDateDateTimePicker);
        }

        public void EnterMessage(string message)
        {
            Wait.UntilElementVisible(MessageTextArea).EnterText(message, true);
        }

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FmsUrl}workplace-safety-solutions/");
            WaitUntilMpPageLoadingIndicatorInvisible();
        }

        public string GetRequestOnSiteStaffSuccessText()
        {
            return Wait.UntilElementVisible(RequestOnSiteStaffSuccessText).GetText();
        }
    }
}