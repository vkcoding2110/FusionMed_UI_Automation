using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.JobPreferences;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.JobPreferences
{
    internal class JobPreferencePo : FmpBasePo
    {
        public JobPreferencePo(IWebDriver driver) : base(driver)
        {
        }
        // Commented code due to bug ,Defect - id 77,79,80
        private readonly By PreferencePageHeaderText = By.XPath("//h1[contains(@class,'HeaderStyled')]");
        private readonly By DepartmentDropDown = By.CssSelector("div#departmentList-select");
        private readonly By SpecialtiesDropDown = By.CssSelector("div#specialtyList-select");
        private readonly By StateDropDown = By.CssSelector("div#stateList-select");
        private readonly By CitiesDropDown = By.CssSelector("div#citiesList-select");
        private readonly By ShiftTypeDropDown = By.CssSelector("div#shiftTypeList-select");
        private readonly By JobTypeDropDown = By.CssSelector("div#jobTypeList-select");
        private readonly By MinSalaryTextbox = By.CssSelector("input#payRangeFloor");
        private readonly By MaxSalaryTextbox = By.CssSelector("input#payRangeCeiling");
        private readonly By StartDateTextbox = By.CssSelector("input#startDateBegin");
        private readonly By SelectedDepartmentText = By.XPath("//div[@id='departmentList-select']//span[contains(@class,'Chip-label')]");
        private readonly By SelectedSpecialtyText = By.XPath("//div[@id='specialtyList-select']//span[contains(@class,'Chip-label')]");
        private readonly By SelectedStateText = By.XPath("//div[@id='stateList-select']//span[contains(@class,'Chip-label')]");
        private readonly By SelectedCityText = By.XPath("//div[@id='citiesList-select']//span[contains(@class,'Chip-label')]");
        private readonly By SelectedShiftTypeText = By.XPath("//div[@id='shiftTypeList-select']//span[contains(@class,'Chip-label')]");
        private readonly By SelectedJobTypeText = By.XPath("//div[@id='jobTypeList-select']//span[contains(@class,'Chip-label')]");
        private readonly By SaveJobPreferenceButton = By.XPath("//button//span[text()='Save Job Preferences']");
        private readonly By StartNowCheckbox = By.XPath("//input[@name='isAsapStart']");
        private readonly By StartNowCheckboxInput = By.XPath("//span[contains(@class,'Mui-checked')]");
        private static By DepartmentCheckbox(string item) => By.XPath($"//ul[@aria-labelledby='departmentList-label']//li[text()='{item}']");
        private static By SpecialtiesCheckbox(string item) => By.XPath($"//ul[@aria-labelledby='specialtyList-label']//li[text()='{item}']");
        private static By StatesCheckbox(string item) => By.XPath($"//ul[@aria-labelledby='stateList-label']//li[text()='{item}']");
        private static By CitiesCheckbox(string item) => By.XPath($"//ul[@aria-labelledby='citiesList-label']//li[text()='{item}']");
        private static By ShiftTypeCheckbox(string item) => By.XPath($"//ul[@aria-labelledby='shiftTypeList-label']//li[text()='{item}']");
        private static By JobTypeCheckbox(string item) => By.XPath($"//ul[@aria-labelledby='jobTypeList-label']//li[text()='{item}']");

        private readonly By ClearDepartmentCloseButton = By.XPath("//div[@id='departmentList-select']//button");
        private readonly By ClearSpecialtyCloseButton = By.XPath("//div[@id='specialtyList-select']//button");
        private readonly By ClearJobTypeCloseButton = By.XPath("//div[@id='jobTypeList-select']//button");
        private readonly By ClearShiftTypeCloseButton = By.XPath("//div[@id='shiftTypeList-select']//button");
        private readonly By ClearStateCloseButton = By.XPath("//div[@id='stateList-select']//button");
        private readonly By ClearCityCloseButton = By.XPath("//div[@id='citiesList-select']//button");
        private readonly By DisabledStateCheckbox = By.XPath("//div[@id='menu-stateList']//li[contains(@class,'Mui-disabled')]");
        private readonly By DisabledCityCheckbox = By.XPath("//div[@id='menu-citiesList']//li[contains(@class,'Mui-disabled')]");
        private readonly By DisabledShiftCheckbox = By.XPath("//div[@id='menu-shiftTypeList']//li[contains(@class,'Mui-disabled')]");
        private readonly By DisabledJobTypeCheckbox = By.XPath("//div[@id='menu-jobTypeList']//li[contains(@class,'Mui-disabled')]");

        // Device element
        private readonly By PreferencePageHeaderTextDevice = By.XPath("//h1[contains(@class,'MobileHeader')]");

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FusionMarketPlaceUrl}job-preferences/");
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public string GetPreferencePageHeaderText()
        {
            return BaseTest.PlatformName != PlatformName.Web
                ? Wait.UntilElementVisible(PreferencePageHeaderTextDevice).GetText()
                : Wait.UntilElementVisible(PreferencePageHeaderText).GetText();
        }

        public void EnterPreferenceDetail(JobPreference preference)
        {
            Wait.HardWait(2000);
            if (Wait.IsElementPresent(ClearDepartmentCloseButton, 3))
            {
                Wait.UntilElementVisible(ClearDepartmentCloseButton).Click();
            }
            Wait.UntilElementClickable(DepartmentDropDown).ClickOn();
            foreach (var department in preference.Departments)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(DepartmentCheckbox(department)));
                Wait.UntilElementClickable(DepartmentCheckbox(department)).Click();
            }

            Wait.UntilElementClickable(DepartmentCheckbox(preference.Departments.First())).PressEscKey();
            Wait.UntilElementInVisible(DepartmentCheckbox(preference.Departments.First()));
            if (Wait.IsElementPresent(ClearSpecialtyCloseButton, 3))
            {
                Wait.UntilElementClickable(ClearSpecialtyCloseButton).Click();
            }
            Wait.UntilElementClickable(SpecialtiesDropDown).ClickOn();
            foreach (var specialties in preference.Specialties)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(SpecialtiesCheckbox(specialties)));
                Wait.UntilElementClickable(SpecialtiesCheckbox(specialties)).Click();
            }
            Wait.UntilElementClickable(SpecialtiesCheckbox(preference.Specialties.First())).PressEscKey();
            Wait.UntilElementInVisible(SpecialtiesCheckbox(preference.Specialties.First()));

            SelectStates(preference.States);
            CloseStateCheckboxDropDown(preference.States);
            SelectCities(preference.Cities);
            CloseCityCheckboxDropDown(preference.Cities);
            SelectShiftType(preference.ShiftType);
            CloseShiftTypeCheckboxDropDown(preference.ShiftType);
            SelectJobType(preference.JobType);
            CloseJobTypeCheckboxDropDown(preference.JobType);
            EnterMinSalary(preference.MinSalary);
            EnterMaxSalary(preference.MaxSalary);
            var isStartNowCheckbox = Wait.UntilElementExists(StartNowCheckbox).IsElementSelected();
            if (preference.StartNow)
            {
                if (!isStartNowCheckbox)
                {
                    Wait.UntilElementExists(StartNowCheckbox).Check(preference.StartNow);
                }
            }
            else
            {
                if (!isStartNowCheckbox)
                {
                    Wait.UntilElementExists(StartNowCheckbox).Check(preference.StartNow);
                }
                EnterStartDate(preference.StartDate.ToString("MM/dd/yyyy"));
            }
            Wait.UntilElementClickable(SaveJobPreferenceButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public JobPreference GetJobPreferenceDetail()
        {
            Wait.HardWait(4000);
            var department = Wait.UntilAllElementsLocated(SelectedDepartmentText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var specialty = Wait.UntilAllElementsLocated(SelectedSpecialtyText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var jobType = Wait.UntilAllElementsLocated(SelectedJobTypeText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var shiftType = Wait.UntilAllElementsLocated(SelectedShiftTypeText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var state = Wait.UntilAllElementsLocated(SelectedStateText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var city = Wait.UntilAllElementsLocated(SelectedCityText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var startDate = DateTime.UtcNow.ToString("MM/dd/yyyy");
            if (IsStartDateFieldEnabled())
            {
                startDate = Wait.UntilElementVisible(StartDateTextbox).GetAttribute("value");
            }
            var startNow = Wait.IsElementPresent(StartNowCheckboxInput,5);

            return new JobPreference
            {
                Departments = department,
                Specialties = specialty,
                States = state,
                Cities = city,
                JobType = jobType,
                ShiftType = shiftType,
                StartDate = DateTime.ParseExact(startDate, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                StartNow = startNow
            };
        }

        public void EnterMinSalary(string minSalary)
        {
            Wait.UntilElementVisible(MinSalaryTextbox).EnterText(minSalary, true);
        }
        public void EnterMaxSalary(string maxSalary)
        {
            Wait.UntilElementVisible(MaxSalaryTextbox).EnterText(maxSalary, true);
        }
        public void EnterStartDate(string startDate)
        {
            Wait.UntilElementVisible(StartDateTextbox).EnterText(startDate, true);
        }
        public bool IsStartNowCheckboxChecked()
        {
            return Wait.UntilElementExists(StartNowCheckbox).IsElementSelected();
        }
        public bool IsStartDateFieldEnabled()
        {
            return Wait.IsElementEnabled(StartDateTextbox);
        }

        public bool IsDisabledStateCheckboxPresent()
        {
            return Wait.IsElementPresent(DisabledStateCheckbox, 3);
        }

        public bool IsDisabledCityCheckboxPresent()
        {
            return Wait.IsElementPresent(DisabledCityCheckbox, 3);
        }
        public bool IsDisabledShiftCheckboxPresent()
        {
            return Wait.IsElementPresent(DisabledShiftCheckbox,3);
        }

        public bool IsDisabledJobTypeCheckboxPresent()
        {
            return Wait.IsElementPresent(DisabledJobTypeCheckbox, 3);
        }

        public void SelectStates(IList<string> stateList)
        {
            if (Wait.IsElementPresent(ClearStateCloseButton, 3))
            {
                Wait.UntilElementClickable(ClearStateCloseButton).Click();
            }
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                if (Wait.IsElementPresent(PreferencePageHeaderTextDevice))
                {
                    Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(PreferencePageHeaderTextDevice));
                }
            }
            Wait.UntilElementClickable(StateDropDown).ClickOn();
            foreach (var state in stateList)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(StatesCheckbox(state)));
                Wait.UntilElementVisible(StatesCheckbox(state)).Click();
            }
        }

        public void CloseStateCheckboxDropDown(IList<string> stateList)
        {
            Wait.UntilElementClickable(StatesCheckbox(stateList.First())).PressEscKey();
            Wait.UntilElementInVisible(StatesCheckbox(stateList.First()));
        }

        public void SelectCities(IList<string> cityList)
        {
            if (Wait.IsElementPresent(ClearCityCloseButton, 3))
            {
                Wait.UntilElementClickable(ClearCityCloseButton).Click();
            }

            Wait.UntilElementClickable(CitiesDropDown).ClickOn();
            foreach (var city in cityList)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(CitiesCheckbox(city)));
                Wait.UntilElementVisible(CitiesCheckbox(city)).Click();
            }
        }

        public void CloseCityCheckboxDropDown(IList<string> cityList)
        {
            Wait.UntilElementClickable(CitiesCheckbox(cityList.First())).PressEscKey();
            Wait.UntilElementInVisible(CitiesCheckbox(cityList.First()));
        }

        public void SelectShiftType(IList<string> shiftList)
        {
            if (Wait.IsElementPresent(ClearShiftTypeCloseButton, 3))
            {
                Wait.UntilElementClickable(ClearShiftTypeCloseButton).Click();
            }

            Wait.UntilElementClickable(ShiftTypeDropDown).ClickOn();
            foreach (var shift in shiftList)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(ShiftTypeCheckbox(shift)));
                Wait.UntilElementVisible(ShiftTypeCheckbox(shift)).Click();
            }
        }

        public void CloseShiftTypeCheckboxDropDown(IList<string> shiftList)
        {
            Wait.UntilElementClickable(ShiftTypeCheckbox(shiftList.First())).PressEscKey();
            Wait.UntilElementInVisible(ShiftTypeCheckbox(shiftList.First()));
        }

        public void SelectJobType(IList<string> jobList)
        {
            if (Wait.IsElementPresent(ClearJobTypeCloseButton, 3))
            {
                Wait.UntilElementClickable(ClearJobTypeCloseButton).Click();
            }

            Wait.UntilElementClickable(JobTypeDropDown).ClickOn();
            foreach (var job in jobList)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(JobTypeCheckbox(job)));
                Wait.UntilElementVisible(JobTypeCheckbox(job)).Click();
            }
        }

        public void CloseJobTypeCheckboxDropDown(IList<string> jobList)
        {
            Wait.UntilElementClickable(JobTypeCheckbox(jobList.First())).PressEscKey();
            Wait.UntilElementInVisible(JobTypeCheckbox(jobList.First()));
        }
    }
}