using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Components;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.Employment
{
    internal class EmploymentPo : FmpBasePo
    {
        public EmploymentPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By AddEmploymentHeaderText = By.CssSelector("div h5[class*='EditHeaderText']");

        //AddEmployment Details
        private readonly By FacilityTextBox = By.CssSelector("input[class*='autosuggest-input']");
        private readonly By FacilityOptionList = By.CssSelector("div#react-autowhatever-employment-facility ul.react-autosuggest__suggestions-list li");
        private static By FacilityOption(string facilityOption) => By.XPath($"//ul[contains(@class,'autosuggest__suggestions')]/li/div[contains(text(),'{facilityOption}')]");
        private readonly By FacilityLoadingIndicator = By.CssSelector("span[class*='LoadingIndicator']");
        private readonly By FacilityInputBoxOption = By.XPath("//div[contains(@class,'FacilityAutosuggestStyled')]//input");
        private readonly By FacilityTextBoxCancelIcon = By.CssSelector("button[class*='ActionIconButton']");
        private readonly By OtherFacilityOption = By.CssSelector("input#other-facility-name");
        private readonly By CityTextBox = By.CssSelector("input#city");
        private readonly By StateDropDown = By.CssSelector("select#state");
        private readonly By CategoryOption = By.CssSelector("select#employment-department");
        private readonly By SpecialtyOption = By.CssSelector("select#employment-specialty");
        private readonly By JobSetting = By.CssSelector("div#employment-job-setting-select");
        private readonly By JobSettingCloseButton = By.XPath("//label[text()='Job Setting(s)']//following-sibling::div//div[contains(@class,'InputRender')]//button");
        private static By JobSettingCheckBox(string jobSettings) => By.XPath($"//ul[@aria-labelledby='employment-job-setting-label']//li[text()='{jobSettings}']");
        private static By JobSettingOptionInput(string jobSettings) => By.XPath($"//div[contains(@class,'InputRender')]//span[text()='{jobSettings}']");

        private readonly By JobTypeOption = By.CssSelector("select#employment-job-type");
        private readonly By SupervisorEmploymentYesRadioButton = By.XPath("//label[contains(text(),'did you have charge')]//following-sibling::div//span[text()='Yes']");
        private readonly By SupervisorEmploymentNoRadioButton = By.XPath("//label[contains(text(),'did you have charge')]//following-sibling::div//span[text()='No']");
        private readonly By HoursPerWeekTextBox = By.CssSelector("input#employment-hours-per-week");
        private readonly By UnitAmountTextBox = By.CssSelector("input#employment-unit-amount");
        private readonly By UnitTypeOption = By.CssSelector("select#employment-unit-type");
        private readonly By ChartingSystem = By.CssSelector("div#employment-charting-systems-select");
        private static By ChartingSystemCheckBox(string chartingSystems) => By.XPath($"//li[text()='{chartingSystems}']");
        private static By ChartingSystemOptionInput(string chartingSystems) => By.XPath($"//div[contains(@class,'InputRender')]//span[text()='{chartingSystems}']");
        private readonly By ChartingSystemCloseButton = By.XPath("//label[text()='Charting System(s)']//following-sibling::div//div[contains(@class,'InputRender')]//button");
        private readonly By OtherChartingSystemsTextBox = By.CssSelector("input#employment-other-charting-systems");
        private readonly By PatientRatioTextBox = By.CssSelector("input#employment-patient-ratio");
        private readonly By JobDescriptionTextArea = By.CssSelector("textarea#employment-description");
        private readonly By JobDescriptionPlaceHolderText = By.XPath("//div[contains(@class,'MuiFilledInput-multiline')]//textarea");
        private readonly By WorkHereInput = By.XPath("//span[text()='I currently work here']/preceding-sibling::span");
        private readonly By WorkHereSelectedCheckbox = By.XPath("//span[contains(@class,'MuiCheckbox-colorPrimary')][contains(@class,'checked')]/following-sibling::span");
        private readonly By StartDate = By.CssSelector("input#employment-start-date");
        private readonly By EndDate = By.CssSelector("input#employment-end-date");

        private readonly By AddEmploymentButton = By.XPath("//div[contains(@class,'ButtonContainer-sc')]//button[contains(@class,'EmploymentEditButton-')]/span[text()='Add Employment']");
        private readonly By SubmitButton = By.XPath("//button[contains(@class,'EditButton')]/span[text()='Submit']");

        private readonly By CloseIcon = By.CssSelector("div [class*='MuiDialog'] button[class*='CloseIconWrapper']");
        private readonly By CancelButton = By.XPath("//div[contains(@class,'ButtonContainer')]//button//span[text()='cancel']");

        //Employment Details HeaderText present
        private readonly By EditEmploymentPopUpHeaderText = By.XPath("//div//h5[text()='Edit Employment']");

        //Delete Employment button
        private readonly By JobDescriptionTextAreaNativeApp = By.XPath("//XCUIElementTypeOther[@name='Job Description']");
        private readonly By DeleteEmployment = By.XPath("//button[contains(@class,'DeleteButtonStyled')]/span[contains(text(),'delete')]");
        private readonly By DeleteConfirmationButton = By.CssSelector("button[class*='DeleteConfirmationButton'] span");

        private readonly By DeleteRecordReferenceMessage = By.XPath("//div[contains(@class,'DeleteBodyText')]/div");
        private readonly By AttachedReferenceRecord = By.XPath("//div[contains(@class,'ReferenceItem')]");
        private readonly By FacilityValidationMessage = By.CssSelector("div[class*='FacilityAutosuggestStyled'] p");
        private readonly By CategoryValidationMessage = By.XPath("//div[contains(@class,'SelectGroupStyled')]//label[text()='Category']//following-sibling::p");
        private readonly By StartDateValidationMessage = By.XPath("//div[contains(@class,'DatePickerWrapper')]//label[text()='Start Date *']//following-sibling::p");
        private readonly By EndDateValidationMessage = By.XPath("//div[contains(@class,'DatePickerWrapper')]//label[text()='End Date *']//following-sibling::p");
        private readonly By JobTypeValidationMessage = By.XPath("//div[contains(@class,'SelectGroupStyled')]//label[text()='Job Type']//following-sibling::p");
        private readonly By HoursPerWeekValidationMessage = By.XPath("//div[contains(@class,'InputStyled')]//label[text()='Hours per week']//following-sibling::p");
        private readonly By JobDescValidationMessage = By.XPath("//div[contains(@class,'TextFieldStyled')]//label[text()='Job Description']//following-sibling::p");

        //Safari elements
        private static By JobSettingCheckBoxSafari(string jobSettings) => By.XPath($"//li[text()='{jobSettings}']//span//input");
        private static By ChartingSystemCheckBoxSafari(string chartingSystems) => By.XPath($"//li[text()='{chartingSystems}']//span//input");

        public string GetAddEmploymentPopupHeaderText()
        {
            return Wait.UntilElementVisible(AddEmploymentHeaderText,20).GetText();
        }
        public void WaitTillEmploymentPopupHeaderGetsDisplay()
        {
            Wait.WaitUntilTextRefreshed(AddEmploymentHeaderText);
        }

        public void EnterAddEmploymentData(DataObjects.FMP.Traveler.Profile.Employment addEmployment)
        {
            WaitTillEmploymentPopupHeaderGetsDisplay();
            var datePicker = new DatePickerPo(Driver);
            if (Wait.IsElementPresent(FacilityTextBoxCancelIcon, 20))
            {
                Wait.UntilElementClickable(FacilityTextBoxCancelIcon).Click();
                Wait.UntilElementInVisible(FacilityTextBoxCancelIcon);
            }
            EnterFacilityName(addEmployment.Facility);
            SelectFacilityOption(addEmployment.Facility);
            if (Wait.IsElementPresent(OtherFacilityOption,4))
            {
                EnterOtherFacilityOption(addEmployment.OtherFacility);
                EnterCity(addEmployment.City);
                SelectState(addEmployment.State);
            }
            SelectCategory(addEmployment.Category);
            SelectSpecialty(addEmployment.Specialty);
            SelectJobType(addEmployment.JobType);
            if (addEmployment.SupervisorEmployment)
            {
                Wait.UntilElementClickable(SupervisorEmploymentYesRadioButton).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(SupervisorEmploymentNoRadioButton).ClickOn();
            }
            EnterHoursPerWeek(addEmployment.Hours);
            Wait.UntilElementVisible(UnitAmountTextBox).EnterText(addEmployment.UnitAmount, true);
            Wait.UntilElementClickable(UnitTypeOption).SelectDropdownValueByText(addEmployment.UnitType, Driver);

            Wait.UntilElementVisible(OtherChartingSystemsTextBox).EnterText(addEmployment.OtherChartingSystems, true);
            Wait.UntilElementVisible(PatientRatioTextBox).EnterText(addEmployment.PatientRatio, true);
            EnterJobDescription(addEmployment.JobDescription);
            //Select Job Setting

            if (Wait.IsElementPresent(JobSettingCloseButton, 5))
            {
                Wait.UntilElementClickable(JobSettingCloseButton).Click();
            }
            Wait.UntilElementClickable(JobSetting).Click();
            var isIos = BaseTest.PlatformName.Equals(PlatformName.Ios);
            foreach (var jobSettings in addEmployment.JobSettingInput.Where(jobSettings => !Wait.UntilElementExists(isIos ? JobSettingCheckBoxSafari(jobSettings) : JobSettingCheckBox(jobSettings)).IsElementSelected()))
            {
                var element = isIos ? JobSettingCheckBoxSafari(jobSettings) : JobSettingCheckBox(jobSettings);
                Wait.UntilElementVisible(element, 10);
                Wait.UntilElementClickable(element).Click();
            }

            if (BaseTest.PlatformName.Equals(PlatformName.Ios))
            {
                Driver.JavaScriptClickOn(Wait.UntilElementExists(SpecialtyOption));
            }
            else
            {
                Wait.UntilElementExists(JobSettingCheckBox(addEmployment.JobSettingInput.First())).SendKeys(Keys.Escape);
                Wait.UntilElementInVisible(JobSettingCheckBox(addEmployment.JobSettingInput.First()));
            }

            //Charting System

            if (Wait.IsElementPresent(ChartingSystemCloseButton, 5))
            {
                Wait.UntilElementClickable(ChartingSystemCloseButton).Click();
            }
            Wait.UntilElementClickable(ChartingSystem).Click();
            var isIosElement = BaseTest.PlatformName.Equals(PlatformName.Ios);
            foreach (var chartingSystems in addEmployment.ChartingSystemInput.Where(chartingSystems => !Wait.UntilElementExists(isIosElement ? ChartingSystemCheckBoxSafari(chartingSystems) : ChartingSystemCheckBox(chartingSystems)).IsElementSelected()))
            {
                var element = isIosElement ? ChartingSystemCheckBoxSafari(chartingSystems) : ChartingSystemCheckBox(chartingSystems);
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(element));
                Wait.UntilElementClickable(element).Click();
            }
            if (BaseTest.PlatformName.Equals(PlatformName.Ios))
            {
                Driver.JavaScriptClickOn(Wait.UntilElementExists(HoursPerWeekTextBox));
            }
            else
            {
                Wait.UntilElementExists(ChartingSystemCheckBox(addEmployment.ChartingSystemInput.First())).SendKeys(Keys.Escape);
                Wait.UntilElementInVisible(ChartingSystemCheckBox(addEmployment.ChartingSystemInput.First()));
            }

            Wait.UntilElementClickable(WorkHereInput).ClickOn();
            SelectDeselectWorkHereCheckbox(addEmployment.WorkHere);
            datePicker.SelectMonthAndYear(addEmployment.StartDate, StartDate);

            if (!addEmployment.WorkHere)
            {
                datePicker.SelectMonthAndYear(addEmployment.EndDate, EndDate);
            }

            if (Wait.IsElementPresent(SubmitButton, 5))
            {
                Wait.UntilElementClickable(SubmitButton).ClickOn();
                Wait.UntilElementInVisible(SubmitButton);
            }
            else
            {
                ClickOnAddEmploymentButton();
                Wait.UntilElementInVisible(AddEmploymentButton);
            }
        }

        public string GetJobDescriptionPlaceHolderText()
        {
            return Wait.UntilElementVisible(JobDescriptionPlaceHolderText).GetAttribute("placeholder");
        }

        public string GetDeleteRecordReferenceMessage()
        {
            return Wait.UntilElementVisible(DeleteRecordReferenceMessage).GetText();
        }
        public bool IsDeleteRecordReferenceMessageDisplayed()
        {
            return Wait.IsElementPresent(DeleteRecordReferenceMessage, 8);
        }
        public string GetReferenceRecordDetail()
        {
            return Wait.UntilElementVisible(AttachedReferenceRecord).GetText();
        }
        public DataObjects.FMP.Traveler.Profile.Employment GetEmploymentDetails(DataObjects.FMP.Traveler.Profile.Employment addEmployment)
        {
            var employment = new DataObjects.FMP.Traveler.Profile.Employment();
            employment.FacilityOption = Wait.UntilElementVisible(FacilityInputBoxOption).GetAttribute("value");
            employment.Category = Wait.UntilElementVisible(CategoryOption).SelectDropdownGetSelectedValue();
            employment.Specialty = Wait.UntilElementVisible(SpecialtyOption).SelectDropdownGetSelectedValue();
            employment.WorkHere = Wait.IsElementPresent(WorkHereSelectedCheckbox,5);
            var startDate = Wait.UntilElementVisible(StartDate).GetAttribute("value");
            employment.StartDate = DateTime.ParseExact(startDate, "MMMM yyyy", CultureInfo.InvariantCulture);

            if (!addEmployment.WorkHere)
            {
                var endDate = Wait.UntilElementVisible(EndDate).GetAttribute("value");
                employment.EndDate = DateTime.ParseExact(endDate, "MMMM yyyy", CultureInfo.InvariantCulture);
            }
            var data = addEmployment.JobSettingInput.Where(jobSettings => Wait.IsElementPresent(JobSettingOptionInput(jobSettings),5)).ToList();
            employment.JobSettingInput = data;
            employment.JobType = Wait.UntilElementVisible(JobTypeOption).SelectDropdownGetSelectedValue();
            employment.Hours = Wait.UntilElementVisible(HoursPerWeekTextBox).GetAttribute("value");
            employment.UnitAmount = Wait.UntilElementVisible(UnitAmountTextBox).GetAttribute("value");
            employment.UnitType = Wait.UntilElementVisible(UnitTypeOption).SelectDropdownGetSelectedValue();
            var chartingInput = addEmployment.ChartingSystemInput.Where(chartingSystems => Wait.IsElementPresent(ChartingSystemOptionInput(chartingSystems),5)).ToList();
            employment.ChartingSystemInput = chartingInput;
            employment.OtherChartingSystems = Wait.UntilElementVisible(OtherChartingSystemsTextBox).GetAttribute("value");
            employment.PatientRatio = Wait.UntilElementVisible(PatientRatioTextBox).GetAttribute("value");
            return employment;
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
            Wait.HardWait(2000);
        }

        public bool IsAddEmploymentPopUpPresent()
        {
            var element = Wait.WaitIncaseElementExists(CloseIcon);
            if (element == null) return false;
            Driver.JavaScriptScrollToElement(element);
            return Wait.IsElementPresent(CloseIcon, 5);
        }

        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
            Wait.HardWait(2000);
        }

        public void EnterFacilityDetailsUsingAutoSuggestion(DataObjects.FMP.Traveler.Profile.Employment addEmployment)
        {
            Wait.UntilElementVisible(FacilityTextBox).EnterText(addEmployment.Facility);
            Wait.UntilElementInVisible(FacilityLoadingIndicator);
            Wait.UntilAllElementsLocated(FacilityOptionList).First(e => e.Displayed).Click();
        }

        public List<string> GetFacilityAutoSuggestionList(DataObjects.FMP.Traveler.Profile.Employment addEmployment)
        {
            Wait.UntilElementVisible(FacilityTextBox).EnterText(addEmployment.Facility);
            Wait.UntilElementInVisible(FacilityLoadingIndicator);
            return Wait.UntilAllElementsLocated(FacilityOptionList).Where(e => e.Displayed).Select(s => s.GetText().Trim()).ToList();
        }

        public string GetFacilityAutoSuggestionOptionSelected()
        {
            Wait.UntilElementVisible(FacilityInputBoxOption);
            return Wait.UntilElementVisible(FacilityInputBoxOption).GetAttribute("value");
        }

        public void ClickOnFacilityTextBoxCancelIcon()
        {
            Wait.UntilElementClickable(FacilityTextBoxCancelIcon).ClickOn();
        }

        public bool IsFacilityTextBoxEmpty()
        {
            return Wait.IsElementPresent(FacilityInputBoxOption,5);
        }

        public string GetEditEmploymentPopUpHeaderText()
        {
            return Wait.UntilElementVisible(EditEmploymentPopUpHeaderText, 20).GetText();
        }

        //Delete & Cancel Button Edit Employment Popup
        public void ClickOnDeleteEmploymentButton()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(DeleteEmployment));
            Wait.UntilElementClickable(DeleteEmployment).ClickOn();
            Wait.UntilElementClickable(DeleteConfirmationButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnAddEmploymentButton()
        {
            Wait.UntilElementClickable(AddEmploymentButton).ClickOn();
        }

        public void EnterFacilityName(string facilityName)
        {
            Wait.UntilElementVisible(FacilityTextBox).EnterText(facilityName, true);
            Wait.UntilElementInVisible(FacilityLoadingIndicator, 30);
        }

        public void SelectFacilityOption(string facilityOption)
        {
            Wait.UntilElementVisible(FacilityOption(facilityOption),20).ClickOn();
        }

        public void EnterOtherFacilityOption(string otherFacility)
        {
            Wait.UntilElementVisible(OtherFacilityOption).EnterText(otherFacility,true);
        }

        public void EnterCity(string city)
        {
            Wait.UntilElementVisible(CityTextBox).EnterText(city, true);
        }

        public void SelectState(string state)
        {
            Wait.UntilElementClickable(StateDropDown).SelectDropdownValueByText(state,Driver);
        }

        public void SelectCategory(string category)
        {
            Wait.UntilElementClickable(CategoryOption).SelectDropdownValueByText(category, Driver);
        }

        public void ClearCategory()
        {
            Wait.UntilElementClickable(CategoryOption).SelectDropdownValueByIndex(0);
        }
        public void SelectSpecialty(string specialty)
        {
            Wait.UntilElementClickable(SpecialtyOption).SelectDropdownValueByText(specialty, Driver);
        }

        public void SelectJobType(string jobType)
        {
            Wait.UntilElementClickable(JobTypeOption).SelectDropdownValueByText(jobType, Driver);
        }

        public void ClearJobType()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementClickable(JobTypeOption));
            Wait.UntilElementClickable(JobTypeOption).SelectDropdownValueByIndex(0);
        }

        public void EnterHoursPerWeek(string hoursPerWeek)
        {
            Wait.UntilElementVisible(HoursPerWeekTextBox).EnterText(hoursPerWeek, true);
        }

        public void EnterJobDescription(string jobDescription)
        {
            if (BaseTest.PlatformName == PlatformName.Ios)
            {
                Driver.NativeAppEnterText(JobDescriptionTextAreaNativeApp, jobDescription);
            }
            else
            {
                Wait.UntilElementClickable(JobDescriptionTextArea).ClickOn();
                Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(StartDate));
                Wait.UntilElementVisible(JobDescriptionTextArea).EnterText(jobDescription, true);
            }
        }

        public void SelectDeselectWorkHereCheckbox(bool workHere)
        {
            if (workHere)
            {
                if (!Wait.IsElementPresent(WorkHereSelectedCheckbox,5))
                {
                    Wait.UntilElementClickable(WorkHereInput).ClickOn();
                }
            }
            else
            {
                if (Wait.IsElementPresent(WorkHereSelectedCheckbox,5))
                {
                    Wait.UntilElementClickable(WorkHereInput).ClickOn();
                }
            }
        }

        public void SelectStartDate(DataObjects.FMP.Traveler.Profile.Employment startDate)
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.SelectMonthAndYear(startDate.StartDate, StartDate);
        }

        public void SelectEndDate(DataObjects.FMP.Traveler.Profile.Employment endDate)
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.SelectMonthAndYear(endDate.EndDate, EndDate);
        }

        public void ClearStartDate()
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.ClearDateSelection(StartDate);
        }
        public void ClearEndDate()
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.ClearDateSelection(EndDate);
        }

        public string GetFacilityValidationMessage()
        {
            return Wait.UntilElementVisible(FacilityValidationMessage).GetText();
        }

        public string GetCategoryValidationMessage()
        {
            return Wait.UntilElementVisible(CategoryValidationMessage).GetText();
        }

        public string GetStartDateValidationMessage()
        {
            return Wait.UntilElementVisible(StartDateValidationMessage).GetText();
        }

        public string GetEndDateValidationMessage()
        {
            return Wait.UntilElementVisible(EndDateValidationMessage).GetText();
        }

        public string GetJobTypeValidationMessage()
        {
            return Wait.UntilElementVisible(JobTypeValidationMessage).GetText();
        }

        public string GetHoursPerWeekValidationMessage()
        {
            return Wait.UntilElementVisible(HoursPerWeekValidationMessage).GetText();
        }

        public string GetJobDescriptionValidationMessage()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(OtherChartingSystemsTextBox));
            return Wait.UntilElementVisible(JobDescValidationMessage).GetText();
        }

        public void DeselectCheckBoxAndEnterEndDate(DataObjects.FMP.Traveler.Profile.Employment employment)
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(StartDate));
            }
            SelectDeselectWorkHereCheckbox(false);
            SelectEndDate(employment);
            Wait.UntilElementClickable(SubmitButton).ClickOn();
            Wait.UntilElementInVisible(SubmitButton);
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
    }
}

