using System;
using System.Globalization;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Careers
{
    internal class OpportunitiesDetailsPagePo : FmsBasePo
    {
        public OpportunitiesDetailsPagePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By JobCategoryText = By.XPath("//strong[text()='Job Category']/following-sibling::span");
        private readonly By JobTitle = By.XPath("//span[@data-automation = 'opportunity-title']");
        private readonly By RequisitionNumberText = By.XPath("//strong[text()='Requisition Number']/following-sibling::span");
        private readonly By ScheduleText = By.XPath("//span[@id='JobFullTime']");
        private readonly By AddressText = By.XPath("//address[@data-automation = 'physical-location']");
        private readonly By JobPostedDateText = By.XPath("//span[@data-automation='job-posted-date']");
        private readonly By SearchResultsButton = By.XPath("//a[@data-automation = 'search-results-button']");

        public string GetJobCategory()
        {
            return Wait.UntilElementVisible(JobCategoryText).GetText();
        }

        public string GetJobTitle()
        {
            Wait.UntilElementVisible(SearchResultsButton, 5);
            return Wait.UntilElementVisible(JobTitle).GetText();
        }

        public string GetRequisitionNumber()
        {
            return Wait.UntilElementVisible(RequisitionNumberText).GetText();
        }

        public string GetSchedule()
        {
            return Wait.UntilElementVisible(ScheduleText).GetText();
        }

        public string GetAddress()
        {
            return Wait.UntilElementVisible(AddressText).GetText().Replace("Locations","");
        }

        public void ClickOnBackToCareerPage()
        {
            Wait.UntilElementClickable(SearchResultsButton).ClickOn();
        }

        public DateTime GetPostedDateText()
        {
            return DateTime.ParseExact(Wait.UntilElementVisible(JobPostedDateText).GetText(), "MMMM d, yyyy", CultureInfo.InvariantCulture);
        }
    }
}
