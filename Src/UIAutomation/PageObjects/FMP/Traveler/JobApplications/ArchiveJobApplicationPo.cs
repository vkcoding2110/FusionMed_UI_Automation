using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.JobApplications
{
    internal class ArchiveJobApplicationPo : FmpBasePo
    {
        public ArchiveJobApplicationPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By AppliedJobName = By.XPath("//div[contains(@class,'ApplicationContainer')]//div[contains(@class,'JobCardContainer')][1]//a//div[contains(@class,'JobName')]");
        private readonly By AppliedJobDate = By.XPath("//div[contains(@class,'ApplicationContainer')]//div[contains(@class,'JobCardContainer')][1]//div[contains(@class,'AgingReport')]");
        private readonly By ArchivedJobApplicationHeaderText = By.XPath("//div[contains(@class,'ApplicationContainer')]/div/h2");
        private readonly By RestoreButton = By.XPath("//div[contains(@class,'JobCardFooter')]/button[contains(@class,'ButtonStyled')]/span[text()='Restore']");
        private readonly By BackToJobApplicationsLink = By.XPath("//div[contains(@class,'ApplicationContainer')]//a[text()='Back to Job Applications'] | //div[contains(@class,'ApplicationContainer')]//a/span[text()='Back to Job Applications']");
        private readonly By AppliedJobAgency = By.XPath("//div[contains(@class,'ApplicationContainer')]//div[contains(@class,'JobCardFooter')]//div[contains(@class,'Agency')]");
        private readonly By AppliedJobLocation = By.XPath("//div[contains(@class,'ApplicationContainer')]//div[contains(@class,'JobCardContainer')][1]//a//div[contains(@class,'Location')]");
       
        public string GetAppliedJobName()
        {
            Wait.WaitUntilTextRefreshed(AppliedJobName);
            return Wait.UntilElementVisible(AppliedJobName).GetText();
        }

        public string GetAppliedJobDate()
        {
            return Wait.UntilElementVisible(AppliedJobDate).GetText();
        }

        public string GetArchivedJobApplicationHeaderTextHeaderText()
        {
            return Wait.UntilElementVisible(ArchivedJobApplicationHeaderText).GetText();
        }

        public void ClickOnRestoreButton()
        {
            Wait.WaitIncaseElementClickable(RestoreButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnBackToJobApplicationsLink()
        {
            Wait.WaitIncaseElementClickable(BackToJobApplicationsLink).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public string GetAppliedJobAgency()
        {
            Wait.WaitUntilTextRefreshed(AppliedJobName);
            return Wait.UntilElementVisible(AppliedJobAgency).GetText();
        }

        public string GetAppliedJobLocation()
        {
            return Wait.UntilElementVisible(AppliedJobLocation).GetText();
        }

        public void RestoreAllJobCards()
        {
            new JobApplicationsPo(Driver).ClickOnViewArchiveLink();
            if (Wait.IsElementPresent(RestoreButton, 5))
            {
                var allElement = Wait.UntilAllElementsLocated(RestoreButton, 10).Count;
                for (var i = 0; i < allElement; i++)
                {
                    ClickOnRestoreButton();
                }
            }
            ClickOnBackToJobApplicationsLink();
        }
    }
}
