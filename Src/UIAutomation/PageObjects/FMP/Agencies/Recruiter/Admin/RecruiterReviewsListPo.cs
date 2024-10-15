using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Components;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter.Admin
{
    internal class RecruiterReviewsListPo : FmpBasePo
    {
        public RecruiterReviewsListPo(IWebDriver driver) : base(driver)
        {
        }

        //Recruiter reviews tab
        private static By ReviewsSubTabs(string item) => By.XPath($"//div[contains(@class,'SubTabsContainer')]//button//span[text()='{item}']");
        private readonly By RecruiterReviewsHeaderText = By.XPath("//div[contains(@class,'ReviewsTabsContainer')]//button/span[text()='Recruiter Reviews']");
        private static By RecruiterReviewsByNthColumn(int columnIndex) => By.XPath($"//div[@class='tbody']//div[@role='row']/div[@role='cell'][{columnIndex}]");

        //Filter
        private static By FilterToggleIcon(string filterText) => By.XPath($"//div[contains(@class,'thead')]//div[text()='{filterText}']//following-sibling::div/button[contains(@class,'IconButtonStyled')]");
        private readonly By StartDateButton = By.XPath("//span[text()='to']/preceding-sibling::div//button");
        private readonly By EndDateButton = By.XPath("//span[text()='to']/following-sibling::div//button");
        private readonly By ClearFiltersButton = By.XPath("//div[contains(@class,'ActionRowWrapper')]//span[text()='Clear Filters']/parent::button");
        private readonly By TextContainsTextBox = By.XPath("//input[@placeholder='Text Contains']");
        private readonly By ReviewerRole = By.XPath("//select[contains(@class,'MuiNativeSelect-select')]");
        private static By ReviewerRoleOption(string text) => By.XPath($"//select[contains(@class,'MuiNativeSelect-select')]//*[text()='{text}']");
        private readonly By MinRating = By.XPath("//p[text()='to']/preceding-sibling::div/div/input");
        private readonly By MaxRating = By.XPath("//p[text()='to']/following-sibling::div/div/input");

        //SortBy
        private static By SortByButton(string text) => By.XPath($"//div[contains(@role,'columnheader')]//div[text()='{text}']//preceding-sibling::div/button");


        public void ClickOnReviewsSubTabs(string item)
        {
            Wait.UntilElementClickable(ReviewsSubTabs(item)).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public bool IsRecruiterReviewsPageOpened()
        {
            return Wait.IsElementPresent(RecruiterReviewsHeaderText, 5);
        }

        //Filter
        public void ClickOnRecruiterReviewsFilterText(string filterText)
        {
            Wait.UntilElementClickable(FilterToggleIcon(filterText)).ClickOn();
            Wait.UntilElementVisible(FilterToggleIcon(filterText));
        }

        public void EnterStartAndEndDate(DateTime startDate, DateTime endDate)
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.SelectDate(startDate, StartDateButton, CalenderPicker.RangeCalenderMonthView);
            datePicker.SelectDate(endDate, EndDateButton, CalenderPicker.RangeCalenderMonthView);
        }

        public IList<T> GetRecruiterReviewsByNthColumn<T>(int columnIndex)
        {
            var listType = typeof(T);
            var elements = Wait.UntilAllElementsLocated(RecruiterReviewsByNthColumn(columnIndex));

            if (listType == typeof(DateTime))
            {
                return (IList<T>)elements.Select(e => DateTime.ParseExact(e.GetText(), "MM/dd/yyyy", CultureInfo.InvariantCulture)).ToList();
            }

            return (IList<T>)elements.Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public void ClickOnClearFiltersButton()
        {
            Wait.UntilElementClickable(ClearFiltersButton).ClickOn();
        }

        public void SearchRecruiterReviewsByFilter(string recruiters, string label)
        {
            Wait.UntilElementVisible(TextContainsTextBox).EnterText(recruiters);
            Wait.UntilElementExists(FilterToggleIcon(label)).ClickOn();
        }

        public void SearchRecruiterReviewsByStatusFilter(string filterText, string recruiters)
        {
            Wait.UntilElementClickable(FilterToggleIcon(filterText)).ClickOn();
            Wait.UntilElementClickable(ReviewerRole).ClickOn();
            Wait.UntilElementClickable(ReviewerRoleOption(recruiters)).ClickOn();
        }

        public void EnterMinAndMaxRating(int min, int max, string label)
        {
            Wait.UntilElementVisible(MinRating).EnterText(min.ToString(), true);
            Wait.UntilElementVisible(MaxRating).EnterText(max.ToString(), true);
            Wait.UntilElementClickable(FilterToggleIcon(label)).ClickOn();
        }

        public void ClickOnSortByButton(string text)
        {
            Wait.UntilElementClickable(SortByButton(text)).ClickOn();
        }
    }
}
