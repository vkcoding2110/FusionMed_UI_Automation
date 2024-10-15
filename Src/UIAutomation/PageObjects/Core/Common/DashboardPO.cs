using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Core.Common
{
    internal class DashboardPo : CoreBasePo
    {
        public DashboardPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By GridIFrame = By.CssSelector("iFrame[class='iframe fullheight']");

        //Specifications | Auditor

        private readonly By PendingButton = By.CssSelector("button[data-status='pending'][data-name='AuditorDashboard']");
        private readonly By DeniedButton = By.CssSelector("button[data-status='denied'][data-name='AuditorDashboard']");
        private readonly By NaButton = By.CssSelector("button[data-status='NA'][data-name='AuditorDashboard']");

        //Compliance Specialist | Specialist
        private readonly By AssignmentsAllCompleteButton = By.Id("AssignCompleteCount");
        private readonly By AssignmentsAllIncompleteButton = By.Id("AssignIncompleteCount");
        private readonly By AssignmentsFutureCompleteButton = By.Id("AssignFutureCompleteCount");
        private readonly By AssignmentsIncompleteButton = By.Id("AssignFutureIncompleteCount");
        private readonly By SpecificationsExpiringButton = By.CssSelector("button[data-status='expiring'][data-name='Dashboard']");
        private readonly By SpecificationsExpiredButton = By.CssSelector("button[data-status='expired'][data-name='Dashboard']");
        private readonly By SpecificationsExpirationButton = By.CssSelector("button[data-status='expiration'][data-name='Dashboard']");
        private readonly By SpecificationsIncompleteButton = By.CssSelector("button[data-status='incomplete'][data-name='Dashboard']");
        private readonly By SpecificationsDeniedButton = By.CssSelector("button[data-status='denied'][data-name='Dashboard']");
        private readonly By SpecificProcessingIndicator = By.Id("specificationdetails_processing");


        public void SwitchToIFrame()
        {
            Wait.UntilElementInVisible(SpecificProcessingIndicator, 20);
            Driver.SwitchToDefaultIframe();
            Driver.SwitchToIframe(Wait.UntilElementExists(GridIFrame));
        }

        //Specifications | Auditor

        public void ClickOnPendingButton() 
        {
            Wait.UntilElementClickable(PendingButton).ClickOn();
        }

        public void ClickOnDeniedButton()
        {
            Wait.UntilElementClickable(DeniedButton).ClickOn();
            Wait.HardWait(3000);
        }

        public void ClickOnNAButton()
        {
            Wait.UntilElementClickable(NaButton).ClickOn();
        }

        //Compliance Specialist | Specialist
       
        public void ClickOnAssignmentsAllCompleteButton()
        {
            Wait.UntilElementClickable(AssignmentsAllCompleteButton).ClickOn();
        }
        public void ClickOnAssignmentsAllIncompleteButton()
        {
            Wait.UntilElementClickable(AssignmentsAllIncompleteButton).ClickOn();
        }
        public void ClickOnAssignmentsFutureCompleteButton()
        {
            Wait.UntilElementClickable(AssignmentsFutureCompleteButton).ClickOn();
        }
        public void ClickOnAssignmentsFutureIncompleteButton()
        {
            Wait.UntilElementClickable(AssignmentsIncompleteButton).ClickOn();
        }

        public void ClickOnSpecificationsExpiringButton()
        {
            Wait.UntilElementClickable(SpecificationsExpiringButton).ClickOn();
        }
        public void ClickOnSpecificationsExpiredButton()
        {
            Wait.UntilElementClickable(SpecificationsExpiredButton).ClickOn();
        }
        public void ClickOnSpecificationsExpirationButton()
        {
            Wait.UntilElementClickable(SpecificationsExpirationButton).ClickOn();
        }
        public void ClickOnSpecificationsIncompleteButton()
        {
            Wait.UntilElementClickable(SpecificationsIncompleteButton).ClickOn();
        }
        public void ClickOnSpecificationsDeniedButton()
        {
            Wait.UntilElementClickable(SpecificationsDeniedButton).ClickOn();
        }
    }
}
