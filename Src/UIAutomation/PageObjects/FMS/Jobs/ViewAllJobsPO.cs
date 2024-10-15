using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Jobs
{
    internal class ViewAllJobsPo : FmsBasePo
    {
        public ViewAllJobsPo(IWebDriver driver) : base(driver)
        {

        }
        public static string LightSeaGreenForWindow = "rgba(34, 176, 191, 1)";
        public static string LightSeaGreenForMac = "rgb(34, 176, 191)";

        private readonly By JobCardJobTitle = By.XPath("//div[contains(@class,'jobsstyles__JobCardContainer')]//div[contains(@class,'jobsstyles__JobName')]");
        private readonly By JobCardJobLocation = By.XPath("//div[contains(@class,'jobsstyles__JobCardContainer')]//div[contains(@class,'jobsstyles__Location')]");
        private readonly By AppliedFilterText = By.XPath("//div[contains(@class,'FilterTagWrapper')]");
        private static By SearchResultWeeklyPayCard(string min, string max) => By.XPath($"//div[contains(@class,'searchmainstyles__SearchPageWrapper')]//div//span[text()='${min} - ${max}']");
        private static By ZipcodeAndDistanceCard(string zipCodeValue) => By.XPath($"//div[contains(@class,'searchmainstyles__SearchPageWrapper')]//div//span[text()='Within 25 miles of {zipCodeValue}']");
        //Sort & Filter
        private readonly By SortAndFilterButton = By.XPath("//span[@class='MuiButton-label' and contains(text(),'Filter')]");
        private readonly By SortAndFilterPopupCloseIcon = By.XPath("//div[text()='Filter']/preceding-sibling::div//span[contains(@class,'MuiIconButton-label')]");
        private static By SortByFilterCard(string sortBy) => By.XPath($"//div[contains(@class,' filterTagstyles__FilterTagWrapper')]//span[text()='{sortBy}']");
        private static By SortAndFilterMainOptions(string option) => By.XPath($"//div[contains(@class,'filterstyles__EntryTouch')]//div[text()='{option}']");
        private readonly By SortAndFilterPopupHeaderText = By.XPath("//div[contains(text(),'Category')]");
        private static By SelectFilterSubOptions(string option) => By.XPath($"//div[contains(@class,'RadioGroupInputStyled')]//label[text()='{option}']//span");
        private readonly By SortAndFilterBackButton = By.XPath("//button[contains(@class,'MuiIconButton-sizeSmall')]");
        private readonly By SortAndFilterSelectedCategoryOption = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Category']/following-sibling::div");
        private readonly By SortAndFilterSelectedShiftOption = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Shift']/following-sibling::div");
        private readonly By SortAndFilterSelectedSortByOption = By.XPath("//div[contains(@class,'EntryPlaceholderText')][text()='Sort By']/following-sibling::div");
        private static By SearchResultRegionCard(string regionCard) => By.XPath($"//div[contains(@class,'searchmainstyles__SearchPageWrapper')]//div//span[text()='{regionCard}']");
        private readonly By Zipcode = By.XPath("//input[@type='number' and @maxlength='5' and @class='MuiInputBase-input MuiFilledInput-input']");
        private static By ZipcodeFilterText(string zipCodeFilterText) => By.XPath($"//div[contains(@class,'filterstyles__DisplayTextView')]//div[text()='Within 25 miles of {zipCodeFilterText}']");
        //Estimated Weekly Pay Min & Max Salary
        private static By EstimatedWeeklyPay(string weeklyPay) => By.XPath($"//label[text()='{weeklyPay}']/parent::div//input");

        private static By SelectSpecialtyOptions(string option) => By.XPath($"//div[contains(@class,'CheckboxItemStyled')]//label[text()='{option}']");
        private readonly By SortAndFilterShowAllResultsButton = By.CssSelector("button[class*='ShowResultsButton']");
        private readonly By SortAndFilterResetAllButton = By.XPath("*//button[text()='Reset all']");
        private readonly By SortAndFilterSelectedOptionCount = By.XPath("//div[contains(@class,'filterstyles__FilteredText')]");
        private static By WeeklyPayFilterValue(string min, string max) => By.XPath($"//div[contains(@class,'DisplayTextView')]//div[text()='${min} - ${max}']");
        private static By RegionOptionValue() => By.XPath($"//div[contains(@class,'filterstyles__DisplayTextView')]//div[text()='Midwest']");
        private readonly By ShowAllResultsButton = By.XPath("//button[contains(@class,'sortFilterstyles__ShowResultsButton')]//span[contains(text(),'Results')]");
        private readonly By FirstCityNameInCityFilter = By.XPath("//div[contains(@class,'filterstyles__BrowseHeader')]//following-sibling::div/div[1]/label");

        public IList<string> GetSelectedJobCategoryList()
        {
            return Wait.UntilAllElementsLocated(AppliedFilterText).Select(e => e.GetText().Trim('x', '\r', '\n')).ToList();
        }

        //Sort & Filter
        public void ClickOnSortAndFilterButton()
        {
            Wait.UntilElementClickable(SortAndFilterButton).ClickOn();
            Wait.HardWait(2000);
        }

        public bool IsSortAndFilterPopupPresent()
        {
            return Wait.IsElementPresent(SortAndFilterPopupCloseIcon);
        }

        public void ClickOnSortAndFilterOption(string option)
        {
            if (BaseTest.PlatformName == PlatformName.Web)
            {
                Wait.UntilElementVisible(JobCardJobTitle);
            }
            Wait.UntilElementVisible(SortAndFilterMainOptions(option), 10);
            Driver.JavaScriptClickOn(Wait.UntilElementClickable(SortAndFilterMainOptions(option)));
        }

        public string GetSortAndFilterPopupHeaderText()
        {
            Wait.UntilElementVisible(SortAndFilterPopupHeaderText);
            return Wait.UntilElementVisible(SortAndFilterPopupHeaderText).GetText();
        }

        public void SelectFilterMenuSubOption(string option)
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectFilterSubOptions(option))).ClickOn();
            Wait.HardWait(1000);
        }

        public void ClickOnBackButton()
        {
            Driver.JavaScriptClickOn((SortAndFilterBackButton));
            Wait.WaitTillElementCountIsLessThan(SortAndFilterBackButton, 1);
        }

        public string GetSelectedCategoryOption()
        {
            return Wait.UntilElementVisible(SortAndFilterSelectedCategoryOption).GetText();
        }

        public string GetSelectedSortByOptionText()
        {
            return Wait.UntilElementVisible(SortAndFilterSelectedSortByOption).GetText();
        }

        public bool IsSortByCardFilterPresentOnSearchResultPage(string sortByFilter)
        {
            return Wait.IsElementPresent(SortByFilterCard(sortByFilter));
        }

        public string GetSelectedShiftOption()
        {
            return Wait.UntilElementVisible(SortAndFilterSelectedShiftOption).GetText();
        }

        public void EnterEstimatedWeeklyMinAndMaxPay(string minValue, string minSalary, string maxValue, string maxSalary)
        {
            Wait.UntilElementClickable(EstimatedWeeklyPay(minValue)).EnterText(minSalary, true);
            Wait.UntilElementClickable(EstimatedWeeklyPay(maxValue)).EnterText(maxSalary + Keys.Tab, true);
        }

        public bool IsSalaryCardPresentOnSearchResultPage(string min, string max)
        {
            return Wait.IsElementPresent(SearchResultWeeklyPayCard(min, max));
        }

        public bool IsRegionCardPresentOnSearchResultPage(string regionCard)
        {
            return Wait.IsElementPresent(SearchResultRegionCard(regionCard));
        }

        public void EnterZipCode(string zip)
        {
            Wait.UntilElementClickable(Zipcode).EnterText(zip, true);
        }

        public bool IsZipCodeCardFilterPresentOnSearchResultPage(string zipCodeValue)
        {
            return Wait.IsElementPresent(ZipcodeAndDistanceCard(zipCodeValue));
        }

        public bool IsZipCodeFilterTextPresentOnFilterTag(string zipCodeFilterText)
        {
            return Wait.IsElementPresent(ZipcodeFilterText(zipCodeFilterText));
        }

        public void ClickOnSpecialtyOption(string option)
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectSpecialtyOptions(option)));
            Driver.JavaScriptClickOn(SelectSpecialtyOptions(option));
            Wait.HardWait(3000);
        }
        public void SelectStateOrCity(string option)
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementClickable(SelectSpecialtyOptions(option)));
            Driver.JavaScriptClickOn(Wait.UntilElementClickable(SelectSpecialtyOptions(option)));
            Wait.WaitTillElementCountIsLessThan(SelectSpecialtyOptions(option), 1);
        }

        public void ClickOnShowResultsButton()
        {
            Wait.UntilElementClickable(SortAndFilterShowAllResultsButton).ClickOn();
        }

        public IList<string> GetAllJobCardTitle()
        {
            Wait.UntilElementVisible(JobCardJobTitle);
            return Wait.UntilAllElementsLocated(JobCardJobTitle).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }
        public IList<string> GetAllJobCardLocation()
        {
            Wait.UntilElementVisible(JobCardJobTitle);
            return Wait.UntilAllElementsLocated(JobCardJobLocation).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public void ClickOnResetAllButton()
        {
            Wait.UntilElementClickable(SortAndFilterResetAllButton).ClickOn();
            Wait.HardWait(5000);
        }

        public int GetSelectedFilterOptionCount()
        {
            return Wait.UntilAllElementsLocated(SortAndFilterSelectedOptionCount).Where(e => e.Displayed).ToList().Count;
        }

        public bool IsEstimatedWeeklyPayFilterValuePresent(string min, string max)
        {
            return Wait.IsElementPresent(WeeklyPayFilterValue(min, max));
        }

        public bool IsSelectedRegionIsPresentOnFilterTag(string regionOption)
        {
            return Wait.IsElementPresent(RegionOptionValue());
        }

        public void ClickOnShowAllResultsButton()
        {
            Wait.UntilElementClickable(ShowAllResultsButton).ClickOn();
            Wait.HardWait(4000);
        }

        public void WaitUntilFirstNameOfCityIsDisplayed()
        {
            Wait.UntilElementVisible(FirstCityNameInCityFilter, 10);
        }

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FmsUrl}search/");
            WaitUntilMpPageLoadingIndicatorInvisible();
        }
    }
}
