using System;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Careers
{
    internal class OpportunitiesPo : FmsBasePo
    {
        public OpportunitiesPo(IWebDriver driver) : base(driver)
        {
        }

        private static By JobCategoryText(int index) => By.XPath($"//div[{index}][@data-automation = 'opportunity']//span[@data-automation = 'job-category']");
        private static By JobCardTitle(int index) => By.XPath($"//div[{index}][@data-automation = 'opportunity']/div/div/h3/a[@data-automation = 'job-title']");
        private static By RequisitionNumberText(int index) => By.XPath($"//div[{index}][@data-automation = 'opportunity']//strong[text()='Requisition Number']/following-sibling::span");
        private static By ScheduleText(int index) => By.XPath($"//div[{index}][@data-automation = 'opportunity']//strong[text()='Schedule']/following-sibling::span");
        private static By AddressText(int index) => By.XPath($"//div[{index}][@data-automation = 'opportunity']//address[@data-automation = 'physical-location']");
        private static By JobPostedDateText(int index) => By.XPath($"//div[{index}][@data-automation = 'opportunity']//small[@data-automation='opportunity-posted-date']");
        private readonly By CareersPageTitle = By.XPath("//h1[contains(@class,'job-board-title')]");
        private readonly By SearchInputBox = By.XPath("//input[@id='SearchInput']");
        private readonly By FirstJobCardTitle = By.XPath("//div[1][@data-automation = 'opportunity']/div/div/h3/a[@data-automation = 'job-title']");
        private readonly By CompanyLocationDropDown = By.XPath("//span[@id='Dropdown0']");
        private readonly By LocationSearchInput = By.XPath("//input[@id='SearchInput0']");
        private readonly By JobTitleText = By.XPath("//div/div/h3/a[@data-automation = 'job-title']");
        private readonly By JobCategoryDropDown = By.XPath("//span[@id='Dropdown1']");
        private readonly By ScheduleDropDown = By.XPath("//button[@class='btn-link dropdown-label']//span[text()='Schedule']");
        private readonly By ResetAllButton = By.XPath("//button[@data-automation='reset-button']");
        private readonly By CategorySearchInput = By.XPath("//input[@id='SearchInput1']");
        private static By DropDownOption(string option) => By.XPath($"//div[@role='list']//div[@class='option-label']/span[text()='{option}']");
        private readonly By LoadingIndicator = By.XPath("//div[@data-automation='activity-indicator']");

        private static By SelectSchedule(string schedule) => By.XPath($"//div[@role='checkbox']/i/following-sibling::div/span[1][text()='{schedule}']");
        
        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FmsUrl}about-us/careers/");
            WaitUntilMpPageLoadingIndicatorInvisible();
        }
        public void ClickOnNthJobCard(int card)
        {
            Wait.UntilElementClickable(JobCardTitle(card)).ClickOn();
            WaitUntilMpPageLoadingIndicatorInvisible();
        }

        public string GetJobTitle(int jobTitle)
        {
            return Wait.UntilElementVisible(JobCardTitle(jobTitle)).GetText();
        }

        public string GetJobCategory(int jobType)
        {
            return Wait.UntilElementVisible(JobCategoryText(jobType)).GetText();
        }

        public string GetCareersText()
        {
            return Wait.UntilElementVisible(CareersPageTitle).GetText();
        }

        public string GetRequisitionNumber(int number)
        {
            return Wait.UntilElementVisible(RequisitionNumberText(number)).GetText();
        }

        public string GetSchedule(int schedule)
        {
            return Wait.UntilElementVisible(ScheduleText(schedule)).GetText();
        }

        public string GetAddress(int address)
        {
            return Wait.UntilElementVisible(AddressText(address)).GetText();
        }

        public DateTime GetJobPostedDateText(int date)
        {
            string dateTime = Wait.UntilElementVisible(JobPostedDateText(date)).GetText();
            if (dateTime == "Today")
            {
                return DateTime.Today;
            }
            else
            {
                return DateTime.ParseExact(Wait.UntilElementVisible(JobPostedDateText(date)).GetText(), "MMM d, yyyy", CultureInfo.InvariantCulture);
            }
        }

        public void EnterJobsToSearchBoxAndHitEnter(string jobs)
        {
            Wait.UntilElementVisible(SearchInputBox).EnterText(jobs);
            Wait.HardWait(3000);
        }

        public string GetFirstJobCardTitle()
        {
            return Wait.UntilElementVisible(FirstJobCardTitle).GetText();
        }

        public void SelectCompanyLocationDropDown(string location)
        {
            Wait.UntilElementClickable(CompanyLocationDropDown).ClickOn();
            Wait.UntilElementVisible(LocationSearchInput).EnterText(location);
            ClickOnDropDownOption(location);
        }

        public void ClickOnDropDownOption(string option)
        {
            Wait.UntilElementClickable(DropDownOption(option)).ClickOn();
            Wait.UntilElementInVisible(LoadingIndicator);
        }

        public void SelectScheduleDropDown(string schedule)
        {
            Wait.UntilElementClickable(ScheduleDropDown).ClickOn();
            Wait.UntilElementClickable(SelectSchedule(schedule)).ClickOn();
            Wait.UntilElementInVisible(LoadingIndicator);
        }

        public int GetAllJobCardCount()
        {
            return Wait.UntilAllElementsLocated(JobTitleText).Where(e => e.Displayed).ToList().Count;
        }

        public void ClickOnResetAllButton()
        {
            Wait.UntilElementClickable(ResetAllButton).ClickOn();
        }

        public void SelectJobCategory(string category)
        {
            Wait.UntilElementClickable(JobCategoryDropDown).ClickOn();
            Wait.UntilElementVisible(CategorySearchInput).EnterText(category);
            ClickOnDropDownOption(category);
        }
    }
}
