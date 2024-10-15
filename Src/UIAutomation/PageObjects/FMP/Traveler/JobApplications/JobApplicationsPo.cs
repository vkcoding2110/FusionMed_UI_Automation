using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.JobApplications
{
    internal class JobApplicationsPo : FmpBasePo
    {
        public JobApplicationsPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By JobApplicationsHeaderText = By.XPath("//div[contains(@class,'ApplicationContainer')]//div//h2[contains(@class,'Header')]");
        private readonly By JobApplicationsDescription = By.XPath("//div[contains(@class,'ApplicationContainer')]//div//p[contains(@class,'Description')]"); 
        private readonly By AppliedJobName = By.XPath("//div[contains(@class,'ApplicationContainer')]//div[contains(@class,'JobCardContainer')][1]//a//div[contains(@class,'JobName')]");
        private readonly By AppliedJobDate = By.XPath("//div[contains(@class,'ApplicationContainer')]//div[contains(@class,'JobCardContainer')][1]//div[contains(@class,'AgingReport')]");
        private readonly By BrowseAllJobsButton = By.XPath("//a[contains(@class,'ButtonStyled')]/span[text()='Browse All Jobs']");
        private readonly By ArchiveButton = By.XPath("//div[contains(@class,'JobCardFooter')]/button[contains(@class,'ButtonStyled')]/span[text()='Archive']");
        private readonly By ViewArchiveLink = By.XPath("//div[contains(@class,'ApplicationCategoryContainer')]/a[text()='View Archive']");
        private readonly By AppliedJobNameList = By.XPath("//div[contains(@class,'ApplicationContainer')]//div[contains(@class,'JobCardContainer')]//a//div[contains(@class,'JobName')]");
        private readonly By AppliedJobDateList = By.XPath("//div[contains(@class,'ApplicationContainer')]//div[contains(@class,'JobCardContainer')]//div[contains(@class,'AgingReport')]");
        private readonly By AppliedJobAgencyName = By.XPath("//div[contains(@class,'ApplicationContainer')]//div[contains(@class,'JobCardFooter')]//div[contains(@class,'AgencyName')]");
        private readonly By AppliedJobLocation = By.XPath("//div[contains(@class,'ApplicationContainer')]//div[contains(@class,'JobCardContainer')][1]//a//div[contains(@class,'Location')]");
        private readonly By AppliedJobAgencyList = By.XPath("//div[contains(@class,'ApplicationContainer')]//div[contains(@class,'JobCardFooter')]//div[contains(@class,'Agency')]");
        private readonly By AppliedJobLocationList = By.XPath("//div[contains(@class,'ApplicationContainer')]//div[contains(@class,'JobCardContainer')]//a//div[contains(@class,'Location')]");

        public string GetJobApplicationsHeaderText()
        {
            return Wait.UntilElementVisible(JobApplicationsHeaderText).GetText();
        }

        public string GetJobApplicationsDescription()
        {
            return Wait.UntilElementVisible(JobApplicationsDescription).GetText();
        }

        public string GetAppliedJobName()
        {
            Wait.WaitUntilTextRefreshed(AppliedJobName);
            return Wait.UntilElementVisible(AppliedJobName).GetText();
        }

        public string GetAppliedJobDate()
        {
            return Wait.UntilElementVisible(AppliedJobDate).GetText();
        }

        public string GetAppliedJobLocation()
        {
            return Wait.UntilElementVisible(AppliedJobLocation).GetText();
        }


        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FusionMarketPlaceUrl}job-applications/");
            WaitUntilFmpTextLoadingIndicatorInvisible();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public bool IsBrowseAllJobsButtonPresent()
        {
            return Wait.IsElementPresent(BrowseAllJobsButton, 5);
        }

        public void ClickOnBrowseAllJobsButton()
        {
            Wait.WaitIncaseElementClickable(BrowseAllJobsButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnArchiveButton()
        {
            Wait.WaitIncaseElementClickable(ArchiveButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnViewArchiveLink()
        {
            Wait.WaitIncaseElementClickable(ViewArchiveLink).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public IList<string> GetAppliedJobsNameList()
        {
            Wait.UntilElementVisible(AppliedJobNameList, 5);
            return Wait.UntilAllElementsLocated(AppliedJobNameList).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }

        public IList<string> GetAppliedJobsDateList()
        {
            return Wait.UntilAllElementsLocated(AppliedJobDateList).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }

        public IList<string> GetAppliedJobAgencyList()
        {
            return Wait.UntilAllElementsLocated(AppliedJobAgencyList).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }

        public IList<string> GetAppliedJobLocationList()
        {
            return Wait.UntilAllElementsLocated(AppliedJobLocationList).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }

        public string GetAppliedJobAgency()
        {
            Wait.WaitUntilTextRefreshed(AppliedJobName);
            return Wait.UntilElementVisible(AppliedJobAgencyName).GetText();
        }
    }
}
