using System;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Components;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.EducationAndCertification
{
    internal class EducationPo : FmpBasePo
    {
        public EducationPo(IWebDriver driver) : base(driver)
        {
        }

        //Add Education Tab
        private readonly By AddEducationTabName = By.XPath("//button[contains(@class,'SectionTab')]/span[text()='Add Education']");

        //Add Education Details
        private readonly By InstitutionNameTextBox = By.CssSelector("input#education-institution");
        private readonly By FieldOfStudyTextBox = By.CssSelector("input#education-field-of-study");
        private readonly By CityTextBox = By.CssSelector("input#education-city");
        private readonly By StateOption = By.CssSelector("select#education-state");
        private readonly By DegreeOrDiplomaOption = By.CssSelector("select#education-degree");
        private readonly By GraduatedDate = By.CssSelector("input#education-graduation-date");
        private readonly By CurrentlyAttendingCheckbox = By.XPath("//form//label[contains(@class,'CheckboxStyled')]");

        //Add Education Button
        private readonly By AddEducationButton = By.XPath("//div[contains(@class,'ButtonContainer')]//button//span[@class='MuiButton-label'][text()='Add Education']");

        //Submit Button
        private readonly By SubmitButton = By.XPath("//div[contains(@class,'ButtonContainer')]//button//span[text()='Submit']");

        //Delete Education
        private readonly By DeleteEducationButton = By.XPath("//button//span[text()=' delete education']");
        private readonly By DeleteConfirmationButton = By.XPath("//button[contains(@class,'DeleteConfirmationButton')]//span[text()='Delete']");

        private readonly By CloseIcon = By.CssSelector("div [class*='MuiDialog'] button[class*='CloseIconWrapper']");

        private readonly By CancelButton = By.XPath("//div[contains(@class,'ButtonContainer')]//button//span[text()='cancel']");

        //Validation Message
        private readonly By InstitutionNameValidationMessage = By.XPath("//label[@id='education-institution-label']//following-sibling::p");
        private readonly By FieldOfStudyValidationMessage = By.XPath("//label[@id='education-field-of-study-label']//following-sibling::p");
        private readonly By CityValidationMessage = By.XPath("//p[@id='education-city-helper-text']");
        private readonly By StateValidationMessage = By.XPath("//label[text()='State']//following-sibling::p");
        private readonly By DegreeDiplomaValidationMessage = By.XPath("//label[text()='Degree / Diploma']//following-sibling::p");

        public bool IsEducationTabNameDisplayed()
        {
            return Wait.IsElementPresent(AddEducationTabName, 20);
        }

        public void EnterEducationData(Education education)
        {
            var datePicker = new DatePickerPo(Driver);
            EnterInstitutionName(education.InstitutionName);
            EnterFieldOfStudy(education.FieldOfStudy);
            EnterCityName(education.City);
            SelectState(education.State);
            SelectDegreeDiploma(education.DegreeDiploma);
            datePicker.SelectMonthAndYear(education.GraduatedDate, GraduatedDate);
        }

        public void ClickOnAddEducationButton()
        {
            Wait.UntilElementClickable(AddEducationButton).ClickOn();
        }

        public Education GetEducationDetails()
        {
            var institutionName = Wait.UntilElementVisible(InstitutionNameTextBox).GetAttribute("value");
            var fieldOfStudy = Wait.UntilElementVisible(FieldOfStudyTextBox).GetAttribute("value");
            var city = Wait.UntilElementVisible(CityTextBox).GetAttribute("value");
            var state = Wait.UntilElementVisible(StateOption).SelectDropdownGetSelectedValue();
            var degreeDiploma = Wait.UntilElementVisible(DegreeOrDiplomaOption).SelectDropdownGetSelectedValue();
            var expirationDate = Wait.UntilElementVisible(GraduatedDate).GetAttribute("value");

            return new Education
            {
                InstitutionName = institutionName,
                FieldOfStudy = fieldOfStudy,
                City = city,
                State = state,
                DegreeDiploma = degreeDiploma,
                GraduatedDate = DateTime.ParseExact(expirationDate, "MMMM yyyy", CultureInfo.InvariantCulture)
            };
        }

        public void ClickOnSubmitButton()
        {
            Wait.UntilElementClickable(SubmitButton).ClickOn();
            Wait.UntilElementInVisible(SubmitButton);
        }

        public void ClickOnDeleteEducationButton()
        {
            Wait.UntilElementClickable(DeleteEducationButton).ClickOn();
            Wait.HardWait(2000);
            Wait.UntilAllElementsLocated(DeleteConfirmationButton).First(e => e.Displayed).ClickOn();
            Wait.HardWait(2000);
            Wait.WaitTillElementCountIsLessThan(DeleteConfirmationButton, 1);
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
            Wait.UntilElementInVisible(CloseIcon);
        }

        public bool IsAddEducationPopUpPresent()
        {
            return Wait.IsElementPresent(CloseIcon,3);
        }

        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
            Wait.UntilElementInVisible(CancelButton);
        }

        public void EnterInstitutionName(string instituteName)
        {
            Wait.UntilElementVisible(InstitutionNameTextBox).EnterText(instituteName, true);
        }

        public void EnterFieldOfStudy(string fieldOfStudy)
        {
            Wait.UntilElementVisible(FieldOfStudyTextBox).EnterText(fieldOfStudy, true);
        }

        public void EnterCityName(string city)
        {
            Wait.UntilElementVisible(CityTextBox).EnterText(city, true);
        }

        public void SelectState(string state)
        {
            Wait.UntilElementClickable(StateOption).SelectDropdownValueByText(state, Driver);
        }

        public void ClearStateOption()
        {
            Wait.UntilElementClickable(StateOption).SelectDropdownValueByIndex(0);
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilElementClickable(CityTextBox).Click();
            }
        }

        public void SelectDegreeDiploma(string degreeDiploma)
        {
            Wait.UntilElementClickable(DegreeOrDiplomaOption).SelectDropdownValueByText(degreeDiploma, Driver);
        }

        public bool IsDeleteEducationButton()
        {
            return Wait.IsElementPresent(DeleteEducationButton, 3);
        }

        public void SelectCurrentlyAttendingCheckbox()
        {
            Wait.UntilElementClickable(CurrentlyAttendingCheckbox).ClickOn();
            Wait.HardWait(2000);
        }

        public bool IsDateGraduatedDateDisabled()
        {
            return Wait.IsElementEnabled(GraduatedDate,3);
        }

        public string GetInstitutionNameValidationMessage()
        {
            return Wait.UntilElementVisible(InstitutionNameValidationMessage).GetText();
        }

        public string GetFieldOfStudyValidationMessage()
        {
            return Wait.UntilElementVisible(FieldOfStudyValidationMessage).GetText();
        }

        public string GetCityValidationMessage()
        {
            return Wait.UntilElementVisible(CityValidationMessage).GetText();
        }

        public string GetStateValidationMessage()
        {
            return Wait.UntilElementVisible(StateValidationMessage).GetText();
        }

        public void ClearDegreeOrDiplomaOption()
        {
            Wait.UntilElementClickable(DegreeOrDiplomaOption).SelectDropdownValueByIndex(0);
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilElementClickable(CityTextBox).Click();
            }
        }

        public string GetDegreeDiplomaValidationMessage()
        {
            return Wait.UntilElementVisible(DegreeDiplomaValidationMessage).GetText();
        }
    }
}
