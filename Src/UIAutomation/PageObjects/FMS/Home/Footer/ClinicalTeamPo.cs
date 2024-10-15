using OpenQA.Selenium;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Home.Footer
{
    internal class ClinicalTeamPo : FmsBasePo
    {
        public ClinicalTeamPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By ClinicalTeamLink = By.XPath("//div[contains(@class,'CalloutSectionContent')]//a[contains(@class,'CtaLink')]");

        public void ClickOnClinicalTeamLink()
        {
            Wait.UntilElementClickable(ClinicalTeamLink).ClickOn();
        }

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FmsUrl}traveler/clinical-team/");
            WaitUntilMpPageLoadingIndicatorInvisible();
        }
    }
}
