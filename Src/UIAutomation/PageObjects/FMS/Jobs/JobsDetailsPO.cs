using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Jobs
{
    internal class JobsDetailsPo : FmsBasePo
    {
        public JobsDetailsPo(IWebDriver driver) : base(driver)
        {
        }

        //Job Details
        private readonly By JobTitle = By.XPath("//div[contains(@class,'JobDetailsCardTitle')]//h1");
        private readonly By ApplyForThisJobButton = By.XPath("//div[contains(@class,'detailsjobsstyles__JobDetailsCardRightPanel')]/div//following-sibling::button");

        //Closed Job
        private readonly By ClosedJobMessage = By.XPath("//div[contains(@class,'indexstyles__JobFilledWarningWrapper')]/div/following-sibling::div");

        public string GetJobTitle()
        {
            return Wait.UntilElementVisible(JobTitle).GetText();
        }

        public bool IsApplyForThisJobButtonEnabled()
        {
            return Wait.IsElementEnabled(ApplyForThisJobButton);
        }

        public string GetFilledJobMessage()
        {
            return Wait.UntilElementVisible(ClosedJobMessage).GetText();
        }
    }
}
