using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.JobPreferences;
using UIAutomation.PageObjects.Components;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.JobPreferences.NativeApp
{
    internal class JobPreferencePo : FmpBasePo
    {
        public JobPreferencePo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By PreferencePageHeaderText = By.XPath("*//android.widget.TextView[@text='JOB PREFERENCES']");
        private readonly By ClearStateCloseButton = By.XPath("//android.widget.TextView[@text='Up to 5 States']/parent::android.view.ViewGroup/android.widget.Button");
        private readonly By StateDropDown = By.XPath("//android.widget.TextView[@text='Up to 5 States']/parent::android.view.ViewGroup/android.widget.TextView[2]");
        private static By JobPreferencePopUpCheckbox(string item) => By.XPath($"*//android.widget.TextView[@text='{item}']/preceding-sibling::android.widget.CheckBox");
        private readonly By ClearCityCloseButton = By.XPath("//android.widget.TextView[@text='Up to 10 Cities']/parent::android.view.ViewGroup/android.widget.Button");
        private readonly By CitiesDropDown = By.XPath("//android.widget.TextView[@text='Up to 10 Cities']/parent::android.view.ViewGroup/android.widget.TextView[2]");
        private readonly By ClearShiftTypeCloseButton = By.XPath("//android.widget.TextView[@text='Up to 3 Shift Types']/parent::android.view.ViewGroup/android.widget.Button");
        private readonly By ShiftTypeDropDown = By.XPath("//android.widget.TextView[@text='Up to 3 Shift Types']/parent::android.view.ViewGroup/android.widget.TextView[2]");
        private readonly By ClearJobTypeCloseButton = By.XPath("//android.widget.TextView[@text='Up to 3 Job Types']/parent::android.view.ViewGroup/android.widget.Button");
        private readonly By JobTypeDropDown = By.XPath("//android.widget.TextView[@text='Up to 3 Job Types']/parent::android.view.ViewGroup/android.widget.TextView[2]");
        private readonly By ConfirmButton = By.XPath("*//android.widget.TextView[@text='Confirm']");
        private static By Checkbox(string item) => By.XPath($"//android.widget.TextView[@text='{item}']/preceding-sibling::android.widget.CheckBox");
        private readonly By ClearDepartmentsCloseButton = By.XPath("//android.widget.TextView[@text='Departments']/parent::android.view.ViewGroup/android.widget.Button");
        private readonly By ClearSpecialtiesCloseButton = By.XPath("//android.widget.TextView[@text='Specialties']/parent::android.view.ViewGroup/android.widget.Button");
        private readonly By DepartmentsDropDown = By.XPath("//android.widget.TextView[@text='Departments']/parent::android.view.ViewGroup/android.widget.TextView[2]");
        private readonly By SpecialtiesDropDown = By.XPath("//android.widget.TextView[@text='Specialties']/parent::android.view.ViewGroup/android.widget.TextView[2]");
        private readonly By MinSalaryTextBox = By.XPath("//android.widget.TextView[@text='Min']/parent::android.view.ViewGroup/preceding-sibling::android.widget.EditText");
        private readonly By MaxSalaryTextBox = By.XPath("//android.widget.TextView[@text='Max']/parent::android.view.ViewGroup/preceding-sibling::android.widget.EditText");
        private readonly By StartTextBox = By.XPath("//android.widget.TextView[@text='Start Date']/following-sibling::android.widget.TextView[2]");
        private readonly By AmReadyToStartNowCheckbox = By.XPath("//android.widget.TextView[@text='I am ready to start now!']/preceding-sibling::android.widget.CheckBox");
        private readonly By SaveJobPreferencesButton = By.XPath("//android.widget.TextView[@text='Save Job Preferences']/parent::android.widget.Button");
        private readonly By SavePreferencesSuccessMessage = By.XPath("//android.widget.TextView[@text='Your preferences have been saved!']");
        private readonly By StartDateTextBox = By.XPath("//android.widget.TextView[@text='Start Date']/parent::android.view.ViewGroup");
        private readonly By StartTextBoxText = By.XPath("//android.widget.TextView[@text='Start Date']/following-sibling::android.widget.TextView[1]");

        //GetJobPreference Data
        private readonly By SelectedDepartmentText = By.XPath("//android.widget.TextView[@text='Departments']/following-sibling::android.view.ViewGroup/android.widget.Button[1]/android.widget.TextView");
        private readonly By SelectedSpecialtyText = By.XPath("//android.widget.TextView[@text='Specialties']/following-sibling::android.view.ViewGroup/android.widget.Button[1]/android.widget.TextView");
        private readonly By SelectedStateText = By.XPath("//android.widget.TextView[@text='Up to 5 States']/following-sibling::android.view.ViewGroup/android.widget.Button[1]/android.widget.TextView");
        private readonly By SelectedCityText = By.XPath("//android.widget.TextView[@text='Up to 10 Cities']/following-sibling::android.view.ViewGroup/android.widget.Button[1]/android.widget.TextView");
        private readonly By SelectedShiftTypeText = By.XPath("//android.widget.TextView[@text='Up to 3 Shift Types']/following-sibling::android.view.ViewGroup/android.widget.Button[1]/android.widget.TextView");
        private readonly By SelectedJobTypeText = By.XPath("//android.widget.TextView[@text='Up to 3 Job Types']/following-sibling::android.view.ViewGroup/android.widget.Button[1]/android.widget.TextView");

        private const string UpToThreeJobTypesText = "Up to 3 Job Types";
        private const string DepartmentsText = "Departments";
        private const string UpToThreeShiftTypesText = "Up to 3 Shift Types";
        private const string SaveJobPreferencesText = "Save Job Preferences";
        private const string CityDropDownText = "Up to 10 Cities";

        public bool IsPreferencePageHeaderTextPresent()
        {
            return Wait.IsElementPresent(PreferencePageHeaderText);
        }

        public void SelectStates(IList<string> stateList)
        {
            if (Wait.IsElementPresent(ClearStateCloseButton, 3))
            {
                Wait.UntilElementClickable(ClearStateCloseButton).ClickOn();
            } 
            
            Wait.UntilElementClickable(StateDropDown).ClickOn();
            foreach (var state in stateList)
            {
                Driver.ScrollToElementByText(state);
                Wait.UntilElementVisible(JobPreferencePopUpCheckbox(state)).ClickOn();
            }
        }

        public bool IsCheckboxEnabled(string item)
        {
            Wait.HardWait(1000);
            return Convert.ToBoolean(Wait.UntilElementVisible(Checkbox(item)).GetAttribute("enabled"));
        }

        public void SelectCities(IList<string> cityList)
        {
            if (Wait.IsElementPresent(ClearCityCloseButton, 3))
            {
                Wait.UntilElementClickable(ClearCityCloseButton).ClickOn();
            }
            Driver.ScrollToElementByText(CityDropDownText);
            Wait.UntilElementClickable(CitiesDropDown).ClickOn();
            foreach (var city in cityList)
            {
                Driver.ScrollToElementByText(city);
                Wait.UntilElementVisible(JobPreferencePopUpCheckbox(city)).ClickOn();
            }
        }

        public void SelectShiftType(IList<string> shiftList)
        {
            if (Wait.IsElementPresent(ClearShiftTypeCloseButton, 3))
            {
                Wait.UntilElementClickable(ClearShiftTypeCloseButton).ClickOn();
            } 
            Wait.UntilElementClickable(ShiftTypeDropDown).ClickOn();
            foreach (var shift in shiftList)
            {
                Driver.ScrollToElementByText(shift);
                Wait.UntilElementVisible(JobPreferencePopUpCheckbox(shift)).ClickOn();
            }
        }

        public void SelectJobType(IList<string> jobList)
        {
            if (Wait.IsElementPresent(ClearJobTypeCloseButton, 3))
            {
                Wait.UntilElementClickable(ClearJobTypeCloseButton).ClickOn();
            }

            Wait.UntilElementClickable(JobTypeDropDown).ClickOn();
            foreach (var job in jobList)
            {
                Driver.ScrollToElementByText(job);
                Wait.UntilElementVisible(JobPreferencePopUpCheckbox(job)).ClickOn();
            }
        }

        public void ClickOnConfirmButton()
        {
            Wait.UntilElementClickable(ConfirmButton).ClickOn();
        }

        public void EnterPreferenceDetail(JobPreference preference)
        {
            Wait.HardWait(2000);
            Driver.ScrollToElementByText(DepartmentsText);
            if (Wait.IsElementPresent(ClearDepartmentsCloseButton, 3))
            {
                Wait.UntilElementVisible(ClearDepartmentsCloseButton).Click();
            }
            Wait.UntilElementClickable(DepartmentsDropDown).ClickOn();
            foreach (var department in preference.Departments)
            {
                Wait.HardWait(1000);
                Driver.ScrollToElementByText(department);
                Wait.UntilElementVisible(JobPreferencePopUpCheckbox(department)).ClickOn();
            }
            ClickOnConfirmButton();
            if (Wait.IsElementPresent(ClearSpecialtiesCloseButton, 3))
            {
                Wait.UntilElementClickable(ClearSpecialtiesCloseButton).Click();
            }
            Wait.UntilElementClickable(SpecialtiesDropDown).ClickOn();
            foreach (var specialties in preference.Specialties)
            {
                Driver.ScrollToElementByText(specialties);
                Wait.UntilElementVisible(JobPreferencePopUpCheckbox(specialties)).ClickOn();
            }
            ClickOnConfirmButton();
            SelectStates(preference.States);
            ClickOnConfirmButton();
            SelectCities(preference.Cities);
            ClickOnConfirmButton();
            Driver.ScrollToElementByText(UpToThreeJobTypesText);
            SelectShiftType(preference.ShiftType);
            ClickOnConfirmButton();
            Driver.ScrollToElementByText(UpToThreeShiftTypesText);
            SelectJobType(preference.JobType);
            ClickOnConfirmButton();
            EnterMinSalary(preference.MinSalary);
            EnterMaxSalary(preference.MaxSalary);
            Wait.HardWait(1000);
            var isStartNowCheckbox = Convert.ToBoolean(Wait.UntilElementVisible(AmReadyToStartNowCheckbox).GetAttribute("checked"));

            if ((!preference.StartNow && isStartNowCheckbox) || preference.StartNow && !isStartNowCheckbox)
            {
                Wait.UntilElementExists(AmReadyToStartNowCheckbox).ClickOn();
            }

            if ((!preference.StartNow && isStartNowCheckbox))
            {
                EnterStartDate(preference.StartDate);
            }

            ClickOnSaveJobPreferencesButton();
        }
        public void EnterMinSalary(string minSalary)
        {
            Wait.UntilElementVisible(MinSalaryTextBox).EnterText(minSalary);
        }
        public void EnterMaxSalary(string maxSalary)
        {
            Wait.UntilElementVisible(MaxSalaryTextBox).EnterText(maxSalary);
        }
        public void EnterStartDate(DateTime startDate)
        {
            new DatePickerPo(Driver).SelectDate(startDate, StartTextBox);
        }
        public void ClickOnSaveJobPreferencesButton()
        {
            Driver.ScrollToElementByText(SaveJobPreferencesText);
            Wait.UntilElementClickable(SaveJobPreferencesButton).ClickOn();
        }
        public bool IsSavePreferencesSuccessMessageTextPresent()
        {
            return Wait.IsElementPresent(SavePreferencesSuccessMessage);
        }
        public bool IsStartDateFieldEnabled()
        {
            Wait.HardWait(1000);
            return Convert.ToBoolean(Wait.UntilElementVisible(StartDateTextBox).GetAttribute("clickable"));

        }
        public JobPreference GetJobPreferenceDetail()
        {
            Wait.HardWait(2000);
            Driver.ScrollToElementByText(DepartmentsText);
            var department = Wait.UntilAllElementsLocated(SelectedDepartmentText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var specialty = Wait.UntilAllElementsLocated(SelectedSpecialtyText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var state = Wait.UntilAllElementsLocated(SelectedStateText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            Driver.ScrollToElementByText(UpToThreeJobTypesText);
            var city = Wait.UntilAllElementsLocated(SelectedCityText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var jobType = Wait.UntilAllElementsLocated(SelectedJobTypeText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var shiftType = Wait.UntilAllElementsLocated(SelectedShiftTypeText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var startDate = DateTime.UtcNow.ToString("MM/dd/yyyy");
            if (IsStartDateFieldEnabled())
            {
                startDate = Wait.UntilElementVisible(StartTextBoxText).GetText();
            }
            Wait.HardWait(1000);
            var startNow = Convert.ToBoolean(Wait.UntilElementVisible(AmReadyToStartNowCheckbox).GetAttribute("clickable"));

            return new JobPreference
            {
                Departments = department,
                Specialties = specialty,
                States = state,
                Cities = city,
                JobType = jobType,
                ShiftType = shiftType,
                StartDate = DateTime.ParseExact(startDate, "M/d/yyyy", CultureInfo.InvariantCulture),
                StartNow = startNow
            };
        }
    }
}
