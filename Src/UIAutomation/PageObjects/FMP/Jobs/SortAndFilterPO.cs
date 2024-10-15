using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.JobPreferences;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Jobs
{
    internal class SortAndFilterPo : FmpBasePo
    {
        public SortAndFilterPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By SortAndFilterHeaderText = By.XPath("//div[contains(@class,'SlideHeader')]//h6");

        private static By SortAndFilterMenuOptions(string option) => By.XPath($"//div[contains(@class,'EntryTouch')]//div[text()='{option}']");
        private static By SelectFilterSubOptions(string option) => By.XPath($"//div[contains(@class,'RadioGroupInputStyled')]//label//span[text()='{option}']");
        private static readonly By SortFilterSubOptions = By.XPath("//div[contains(@class,'CheckGroupInputStyled')]//span[contains(@class,'MuiFormControlLabel')]");
        private static By GetSubOptionText(int count) => By.CssSelector($"div[class*='CheckGroupInputStyled']:nth-of-type({count}) span[class*='MuiFormControlLabel']");
        private static By SelectSortAndFilterSubMenuOptions(string option) => By.XPath($"//div[contains(@class,'CheckboxItemStyled')]//span[text()='{option}']");
        private readonly By SortAndFilterSelectedCategoryOption = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Category']/following-sibling::div");
        private readonly By AppliedFilterText = By.XPath("//div[contains(@class,'FilterBarTags')]//span[text()='Jobs']//parent::div//following-sibling::div");
        private readonly By SortAndFilterSelectedSortByOption = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Sort By']/following-sibling::div");
        private static By SortByFilterCard(string sortBy) => By.XPath($"//div[contains(@class,'FilterTagWrapper')]//span[text()='{sortBy}']");
        private readonly By FirstCityNameInCityFilter = By.XPath("//div[contains(@class,'CheckGroupInputStyled')]/label");
        private readonly By SortAndFilterSelectedOptionCount = By.XPath("//div[contains(@class,'DisplayTextView')]/div/following-sibling::div");
        private readonly By SortAndFilterSelectedShiftOption = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Shift']/following-sibling::div");
        private static By EstimatedWeeklyPay(string weeklyPay) => By.XPath($"//label[text()='{weeklyPay}']/parent::div//input");
        private static By WeeklyPayFilterValue(string min, string max) => By.XPath($"//div[text()='${min} - ${max}']");
        private static By RegionOptionValue => By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Region']/following-sibling::div");
        private static By SearchResultRegionCard(string regionCard) => By.XPath($"//div[contains(@class,'FilterBarTags')]//div//span[text()='{regionCard}']");
        private readonly By CityTextBox = By.XPath("//div[contains(@class,'autosuggest__container')] /input");
        private static By CityOption(string cityOption) => By.XPath($"//ul[contains(@class,'autosuggest__suggestions')]/li/div[contains(text(),'{cityOption}')]");
        private static By CityFilterText(string distance, string cityFilterText) => By.XPath($"//div[contains(@class,'DisplayTextView')]//div[text()='Within {distance} of {cityFilterText}']");
        private static By CityAndDistanceCard(string distance, string cityAndDistanceValue) => By.XPath($"//div[contains(@class,' MuiChip-clickable MuiChip-deletable')]//span[text()='Within {distance} of {cityAndDistanceValue}']");
        private readonly By DistanceDropDown = By.CssSelector("select#RadiusDistance");
        private readonly By SortAndFilterStartAsap = By.XPath("//div[contains(@class,'StartDateWrapper')]//span/span[contains(@class,'MuiIconButton-label')]");
        private readonly By SortAndFilterSelectDate = By.XPath("//div[contains(@class,'MuiPickersBasePicker-container')]");
        private readonly By SortAndFilterStartDateInput = By.XPath("//div[contains(@class,'MuiFilledInput')]/input");
        private readonly By SortAndFilterStartTextOnFilterTag = By.XPath("//div[contains(@class,'DisplayTextView')]/div[text()='Start Date']/following-sibling::div");
        private readonly By SortAndFilterSelectDateValidationMessage = By.XPath("//h6[text()='Start Date']/ancestor::div[contains(@class,'SlideHeader')]/following-sibling::div//p");
        private readonly By JobTypeInput = By.XPath("//div[text()='Job Type']//following-sibling::div");

        private readonly By SortAndFilterBackButton = By.XPath("//div[contains(@class,'SlideHeader')]//button[contains(@class,'MuiIconButton')]");
        private readonly By SortAndFilterPopupCloseIcon = By.XPath("//h6[text()='Filter']/parent::div//preceding-sibling::div/button");
        private readonly By ShowAllResultsButton = By.XPath("//span[contains(text(),'Results')]//parent::button");
        private readonly By SortAndFilterResetAllButton = By.XPath("*//button[text()='Reset all']");

        private readonly By SelectedCategory = By.XPath("//div[contains(@class,'DisplayTextView')]/div[text()='Category']/following-sibling::div");
        private readonly By SelectedSpecialty = By.XPath("//div[contains(@class,'DisplayTextView')]/div[text()='Specialty']/following-sibling::div");

        private static By SubFilterOptionCheckbox(string option) => By.XPath($"//div[contains(@class,'CheckboxItemStyled')]//span[text()='{option}']");
        private readonly By SelectedFacilityType = By.XPath("//div[contains(@class,'DisplayTextView')]/div[text()='Facility Type']/following-sibling::div");

        private readonly By SelectedAgencyFilterText = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Agency']/following-sibling::div");

        private static By SelectedDepartmentFilterBarTag(string departmentName) => By.XPath($"//div[contains(@class,'FilterBarTags')]//div//span[text()='{departmentName}']");

        private readonly By SelectedDepartmentFilterText = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Department']/following-sibling::div");
        private static By SelectDepartmentOptions(string option) => By.XPath($"//div[contains(@class,'RadioGroupInputStyled')]//span[text()='{option}']//parent::label/span/span");
        private readonly By JobsWithMultipleAgencyCheckbox = By.XPath("//span[contains(text(),'multiple agency')]");
        private readonly By JobsWithMultipleAgencyCheckboxInput = By.XPath("//span[contains(text(),'multiple agency')]//parent::label//span[contains(@class,'Mui-checked')]");

        //Job Quantity Filter
        private static By MinMaxJobQuantity(string quantity) => By.XPath($"//label[text()='{quantity}']/parent::div//input");
        private static By JobQuantityFilterValue(string quantityText, int minValue, int maxValue) => By.XPath($"//div[text()='{quantityText}']//following-sibling::div[text()='{minValue} - {maxValue}']");
        private static By SortAndFilterMenuHeaderOption(string option) => By.XPath($"//div[contains(@class,'SlideWrapper')][2]//h6[text()='{option}']");

        // Job Preferences
        private readonly By SelectedStartDateFilterText = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Start Date']/following-sibling::div");
        private readonly By SelectedPayRangeFilterText = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Estimated Weekly Pay']/following-sibling::div");
        private readonly By SelectedSpecialtyFilterText = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Specialty']/following-sibling::div");
        private readonly By SelectedPreferenceFilterText = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Preferences']/following-sibling::div");
        private readonly By SelectedStateFilterText = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='State']/following-sibling::div");
        private readonly By ShitTypePreferenceCheckbox = By.XPath("//input[contains(@name,'shift')]");
        private readonly By StartDatePreferenceCheckbox = By.XPath("//input[contains(@name,'startDate')]");
        private readonly By JobTypePreferenceCheckbox = By.XPath("//input[contains(@name,'jobType')]");
        private readonly By SelectAllCheckboxInput = By.XPath("//span[text()='Select All']//parent::label//span[contains(@class,'Mui-checked')]");
        private readonly By SelectAllCheckbox = By.XPath("//span[text()='Select All']//parent::label//input");
        private readonly By SelectedDepartmentText = By.XPath("//input[contains(@name,'category')]//parent::span//parent::span//parent::label//span[contains(@class,'FormControlLabel')]");
        private readonly By SelectedSpecialtyText = By.XPath("//input[contains(@name,'specialty')]//parent::span//parent::span//parent::label//span[contains(@class,'FormControlLabel')]");
        private readonly By SelectedJobTypeText = By.XPath("//input[contains(@name,'jobType')]//parent::span//parent::span//parent::label//span[contains(@class,'FormControlLabel')]");
        private readonly By SelectedShiftTypeText = By.XPath("//input[contains(@name,'shift')]//parent::span//parent::span//parent::label//span[contains(@class,'FormControlLabel')]");
        private readonly By SelectedStateText = By.XPath("//input[contains(@name,'state')]//parent::span//parent::span//parent::label//span[contains(@class,'FormControlLabel')]");
        private readonly By SelectedCityText = By.XPath("//input[contains(@name,'city')]//parent::span//parent::span//parent::label//span[contains(@class,'FormControlLabel')]");
        private readonly By SelectedStartDateText = By.XPath("//input[contains(@name,'startDate')]//parent::span//parent::span//parent::label//span[contains(@class,'FormControlLabel')]");
        private readonly By SelectedPayRangeValue = By.XPath("//input[contains(@name,'estimatedWeeklyPay')]//parent::span//parent::span//parent::label//span[contains(@class,'FormControlLabel')]");
        private readonly By EditMyPreferenceLink = By.XPath("//span[text()='Edit my preferences']");


        public bool IsEditMyPreferenceLinkDisplayed()
        {
            if (BaseTest.PlatformName == PlatformName.Android)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectedSpecialtyText));
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectedShiftTypeText));
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectedStateText));
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectedPayRangeValue));
            }
            else
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectedStateText));
            }
            return Wait.IsElementPresent(EditMyPreferenceLink, 3);
        }

        public void ClickOnEditPreferenceLink()
        {
            Driver.JavaScriptClickOn(EditMyPreferenceLink);
        }
        public void ClickOnSelectAllCheckbox(bool select)
        {
            var isCheckboxSelected = Wait.IsElementPresent(SelectAllCheckboxInput, 5);
            if (select)
            {
                if (!isCheckboxSelected)
                {
                    Wait.UntilElementExists(SelectAllCheckbox).ClickOn();
                }
            }
            else if (isCheckboxSelected.Equals(true))
            {
                Wait.UntilElementExists(SelectAllCheckbox).ClickOn();
            }
        }
        public void ClickOnJobsWithMultipleAgencyCheckbox(bool select)
        {
            var isCheckboxSelected = Wait.IsElementPresent(JobsWithMultipleAgencyCheckboxInput, 5);
            if ((!@select || isCheckboxSelected) && (@select || !isCheckboxSelected)) return;
            if (BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Safari) && Driver!= null)
            {
                Driver.JavaScriptClickOn(JobsWithMultipleAgencyCheckbox);
            }
            else
            {
                Wait.UntilElementClickable(JobsWithMultipleAgencyCheckbox).Click();
            }

        }

        public bool IsSelectAllCheckboxChecked()
        {
            return Wait.IsElementPresent(SelectAllCheckboxInput,4);
        }
        public JobPreference GetMyPreferenceDetail()
        {
            var department = Wait.UntilAllElementsLocated(SelectedDepartmentText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var specialty = Wait.UntilAllElementsLocated(SelectedSpecialtyText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var jobType = Wait.UntilAllElementsLocated(SelectedJobTypeText).Where(e => e.Displayed).Select(s => s.GetText()).ToList(); 
            var shiftType = Wait.UntilAllElementsLocated(SelectedShiftTypeText).Where(e => e.Displayed).Select(s => s.GetText()).ToList(); 
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectedShiftTypeText));
            var state = Wait.UntilAllElementsLocated(SelectedStateText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var city = Wait.UntilAllElementsLocated(SelectedCityText).Where(e => e.Displayed).Select(s => s.GetText()).ToList();
            var startNow = Wait.IsElementPresent(SelectedStartDateText, 3);
            var maxAndMinSalary = Wait.UntilAllElementsLocated(SelectedPayRangeValue).Last(e => e.Displayed)
                .GetText().Split(" ");
            return new JobPreference
            {
                Departments = department,
                Specialties = specialty,
                States = state,
                Cities = city,
                JobType = jobType,
                ShiftType = shiftType,
                MaxSalary = maxAndMinSalary.Last(),
                MinSalary = maxAndMinSalary.First(),
                StartNow = startNow
            };
        }


        public JobPreference GetSelectedPreferenceValue()
        {
            var category = GetSelectedCategoryOption();
            var categoryList = new CSharpHelpers().StringToList(category, ',');
            var specialty = Wait.UntilElementVisible(SelectedSpecialtyFilterText).GetText();
            var specialtyList = new CSharpHelpers().StringToList(specialty, ',');
            var state = Wait.UntilElementVisible(SelectedStateFilterText).GetText();
            var stateList = new CSharpHelpers().StringToList(state, ',');
            var shift = Wait.UntilElementVisible(SortAndFilterSelectedShiftOption).GetText();
            var shiftList = new CSharpHelpers().StringToList(shift, ',');
            var jobType = Wait.UntilElementVisible(JobTypeInput).GetText();
            var jobList = new CSharpHelpers().StringToList(jobType, ',');
            var minSalaryAndMaxSalary = Wait.UntilAllElementsLocated(SelectedPayRangeFilterText).Last(e => e.Displayed)
                .GetText().Split(" ");
            var startDate = Wait.UntilElementVisible(SelectedStartDateFilterText).GetText();
            return new JobPreference
            {
                Departments = categoryList.ToList(),
                Specialties = specialtyList.ToList(),
                States = stateList.ToList(),
                ShiftType = shiftList.ToList(),
                JobType = jobList.ToList(),
                MaxSalary = minSalaryAndMaxSalary.Last(),
                MinSalary = minSalaryAndMaxSalary.First(),
                StartDate = DateTime.ParseExact(startDate, "MMMM d, yyyy", CultureInfo.InvariantCulture)
            };
        }

        public string GetSelectedStartDateText()
        {
            return Wait.UntilElementVisible(SelectedStartDateFilterText).GetText();
        }

        public string GetPreferenceFilterText()
        {
            return Wait.UntilElementVisible(SelectedPreferenceFilterText).GetText();
        }
        public void SelectShiftPreferenceCheckbox(bool select)
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectedSpecialtyText));
            Wait.UntilElementExists(ShitTypePreferenceCheckbox).Check(select,Driver);
        }
        public void SelectStartDatePreferenceCheckbox(bool select)
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectedSpecialtyText));
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectedStateText));
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectedCityText));
            Wait.UntilElementExists(StartDatePreferenceCheckbox).Check(select,Driver);
        }
        public void SelectJobTypePreferenceCheckbox(bool select)
        {
            Wait.UntilElementExists(JobTypePreferenceCheckbox).Check(select,Driver);
        }
        public void ClickOnBackButton()
        {
            Driver.JavaScriptClickOn(Wait.UntilAllElementsLocated(SortAndFilterBackButton).First(e => e.Displayed));
            Wait.HardWait(2000);
            if (BaseTest.PlatformName.Equals(PlatformName.Web)) return;
            if (!Wait.IsElementPresent(SortAndFilterHeaderText,5))
            {
                new SearchPo(Driver).ClickOnSortAndFilterButton();
            }
        }
        public void ClickOnPreferenceBackButton()
        {
            Driver.JavaScriptClickOn(Wait.UntilAllElementsLocated(SortAndFilterBackButton).First(e => e.Displayed));
        }
        public void ClickOnSortAndFilterOption(string option)
        {
            Wait.UntilElementVisible(SortAndFilterMenuOptions(option), 20);
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilAllElementsLocated(SortAndFilterMenuOptions(option)).Where(e => e.IsDisplayed()).FirstOrDefault().ClickOn();
            }
            else
            {
                Driver.JavaScriptClickOn(Wait.UntilElementClickable(SortAndFilterMenuOptions(option)));
            }
            Wait.UntilElementVisible(SortAndFilterMenuHeaderOption(option), 10);
        }

        public string GetSortAndFilterPopupHeaderText()
        {
            return Wait.UntilAllElementsLocated(SortAndFilterHeaderText).First(e => e.Displayed).GetText();
        }

        public void SelectFilterMenuSubOption(string option)
        {
            Wait.UntilElementVisible(SelectFilterSubOptions(option));
            Driver.JavaScriptScrollToElement(Wait.UntilElementClickable(SelectFilterSubOptions(option))).ClickOn();
        }

        public void ClickOnCloseIconOnSortAndFilterPopup()
        {
            Wait.UntilElementClickable(SortAndFilterPopupCloseIcon).ClickOn();
            Wait.UntilElementInVisible(SortAndFilterPopupCloseIcon);
        }

        public bool IsSortAndFilterPopupPresent()
        {
            Wait.UntilElementVisible(SortAndFilterPopupCloseIcon);
            return Wait.IsElementPresent(SortAndFilterPopupCloseIcon);
        }

        public bool IsSortAndFilterPopupOpened()
        {
            return Wait.IsElementPresent(SortAndFilterPopupCloseIcon);
        }

        public string GetSelectedCategoryOption()
        {
            return Wait.UntilElementVisible(SortAndFilterSelectedCategoryOption).GetText();
        }

        public IList<string> GetSelectedJobCategoryList()
        {
            Wait.UntilElementVisible(AppliedFilterText);
            return Wait.UntilAllElementsLocated(AppliedFilterText).Select(e => e.GetText().Trim('x', '\r', '\n')).ToList();
        }

        public bool IsSelectedJobFilterTagsDisplayed()
        {
            return Wait.IsElementPresent(AppliedFilterText,5);
        }

        public string GetSelectedSortByOptionText()
        {
            return Wait.UntilElementVisible(SortAndFilterSelectedSortByOption).GetText();
        }

        public void ClickOnShowAllResultsButton()
        {
            Driver.JavaScriptClickOn(ShowAllResultsButton);
            Wait.UntilElementInVisible(ShowAllResultsButton, 5);
        }

        public bool IsSortByCardFilterPresentOnSearchResultPage(string sortByFilter)
        {
            return Wait.IsElementPresent(SortByFilterCard(sortByFilter));
        }

        public void ClickOnSortAndFilterSubMenuOption(string option)
        {
            Wait.UntilElementVisible(SelectSortAndFilterSubMenuOptions(option), 10);
            Driver.JavaScriptClickOn(Wait.UntilElementClickable(SelectSortAndFilterSubMenuOptions(option)));
            Wait.HardWait(2000);
            Wait.WaitTillElementCountIsLessThan(SelectSortAndFilterSubMenuOptions(option), 1);
        }
        public void SelectStateOrCity(string option)
        {
            var counter = 1;
            do
            {
                if (Wait.IsElementPresent(SelectSortAndFilterSubMenuOptions(option),1))
                {
                    Driver.JavaScriptScrollToElement(Wait.UntilElementClickable(SelectSortAndFilterSubMenuOptions(option)));
                    Driver.JavaScriptClickOn(Wait.UntilElementClickable(SelectSortAndFilterSubMenuOptions(option)));
                    Wait.WaitTillElementCountIsLessThan(SelectSortAndFilterSubMenuOptions(option), 1);
                    break;
                }
                var optionCount = Wait.UntilAllElementsLocated(SortFilterSubOptions).Count;
                var optionText = Wait.UntilElementExists(GetSubOptionText(optionCount)).GetText();
                Driver.JavaScriptScrollToElement(Wait.UntilElementClickable(SelectSortAndFilterSubMenuOptions(optionText)));

                if (counter.Equals(150)) break;
                counter++;
            } while (true);
        }

        public void WaitUntilFirstNameOfCityIsDisplayed()
        {
            Wait.UntilElementVisible(FirstCityNameInCityFilter, 30);
        }

        public void ClickOnResetAllButton()
        {
            Wait.UntilElementClickable(SortAndFilterResetAllButton).ClickOn();
        }

        public int GetSelectedFilterOptionCount()
        {
            return Wait.UntilAllElementsLocated(SortAndFilterSelectedOptionCount).Where(e => e.Displayed).ToList().Count;
        }

        public IList<string> GetSelectedShiftOption()
        {
            return Wait.UntilAllElementsLocated(SortAndFilterSelectedShiftOption).Select(e => e.GetText()).ToList();
        }

        public void EnterEstimatedWeeklyMinAndMaxPay(string minValue, string minSalary, string maxValue, string maxSalary)
        {
            Wait.UntilElementClickable(EstimatedWeeklyPay(minValue)).EnterText(minSalary, true);
            Wait.UntilElementClickable(EstimatedWeeklyPay(maxValue)).EnterText(maxSalary + Keys.Tab, true);
        }

        public bool IsEstimatedWeeklyPayFilterValuePresent(string min, string max)
        {
            Wait.UntilElementVisible(WeeklyPayFilterValue(min, max));
            return Wait.IsElementPresent(WeeklyPayFilterValue(min, max));
        }

        public string GetSelectedRegionOptionOnFilterTag()
        {
            return Wait.UntilElementVisible(RegionOptionValue).GetText();
        }

        public bool IsRegionCardPresentOnSearchResultPage(string regionCard)
        {
            return Wait.IsElementPresent(SearchResultRegionCard(regionCard));
        }

        public void EnterCityName(string city)
        {
            Wait.UntilElementVisible(CityTextBox).EnterText(city, true);
        }
        public void SelectCityOption(string cityOption)
        {
            Driver.JavaScriptClickOn(Wait.UntilElementExists(CityOption(cityOption)));
            Wait.UntilElementInVisible(CityOption(cityOption));
        }

        public void SelectDistance(string distance)
        {
            Wait.UntilElementVisible(DistanceDropDown).SelectDropdownValueByText(distance, Driver);
        }

        public bool IsCityAndRadiusFilterTextPresentOnFilterTag(string distance, string cityFilterText)
        {
            return Wait.IsElementPresent(CityFilterText(distance, cityFilterText));
        }

        public bool IsCityAndDistanceFilterPresentOnSearchResultPage(string distance, string cityAndDistanceValue)
        {
            return Wait.IsElementPresent(CityAndDistanceCard(distance, cityAndDistanceValue));
        }

        public void ClickOnSortAndFilterStartDateButton()
        {
            Wait.UntilElementClickable(SortAndFilterStartDateInput).ClickOn();
        }

        public void ClickOnSortAndFilterStartASAPCheckbox()
        {
            Wait.UntilElementClickable(SortAndFilterStartAsap).ClickOn();
        }

        public bool IsStartDateInputEnabled()
        {
            return Wait.IsElementEnabled(SortAndFilterSelectDate);
        }

        public void EnterStartDate(string date)
        {
            Wait.UntilElementVisible(SortAndFilterStartDateInput).EnterText(date, true);
        }

        public string GetStartDateValidationMessage()
        {
            return Wait.UntilElementVisible(SortAndFilterSelectDateValidationMessage).GetText();
        }

        public bool IsStartDateTextPresentOnFilterTag()
        {
            return Wait.IsElementPresent(SortAndFilterStartTextOnFilterTag);
        }

        public IList<string> GetJobTypeInputValue()
        {
            return Wait.UntilAllElementsLocated(JobTypeInput).Select(e => e.GetText()).ToList();
        }

        public string GetSelectedCategories()
        {
            return Wait.UntilElementVisible(SelectedCategory).GetText();
        }

        public string GetSelectedSpecialties()
        {
            return Wait.UntilElementVisible(SelectedSpecialty).GetText();
        }

        public void SelectSubOptionCheckboxFromFilter(IList<string> categoryMenus)
        {
            foreach (var item in categoryMenus)
            {
                if (BaseTest.PlatformName.Equals(PlatformName.Web))
                {
                    Wait.UntilAllElementsLocated(SubFilterOptionCheckbox(item));
                    Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SubFilterOptionCheckbox(item)));
                }
                Wait.UntilElementVisible(SubFilterOptionCheckbox(item));
                Driver.JavaScriptClickOn(Wait.UntilElementExists(SubFilterOptionCheckbox(item)));
            }
        }

        public string GetSelectedFacilityType()
        {
            return Wait.UntilElementVisible(SelectedFacilityType).GetText();
        }

        public void EnterMinAndMaxJobQuantity(string minText, int minQuantity, string maxText, int maxQuantity)
        {
            Wait.UntilElementClickable(MinMaxJobQuantity(minText)).EnterText(minQuantity.ToString(), true);
            Wait.UntilElementClickable(MinMaxJobQuantity(maxText)).EnterText(maxQuantity + Keys.Tab, true);
        }

        public string GetJobQuantityValueFromFilter(string quantityText, int minValue, int maxValue)
        {
            return Wait.UntilElementVisible(JobQuantityFilterValue(quantityText, minValue, maxValue)).GetText();
        }
        public string GetSelectedAgencyOptionFromFilter()
        {
            return Wait.UntilElementVisible(SelectedAgencyFilterText).GetText();
        }
        public bool IsSelectedDepartmentPresentOnSearchResultPage(string departmentName)
        {
            return Wait.IsElementPresent(SelectedDepartmentFilterBarTag(departmentName),3);
        }

        public string GetSelectedDepartmentOptionFromFilter()
        {
            return Wait.UntilElementVisible(SelectedDepartmentFilterText).GetText();
        }
        public void SelectDepartmentFilterMenuOption(string option)
        {
            Wait.UntilElementVisible(SelectDepartmentOptions(option));
            Driver.JavaScriptScrollToElement(Wait.UntilElementClickable(SelectDepartmentOptions(option))).ClickOn();
        }
    }
}
