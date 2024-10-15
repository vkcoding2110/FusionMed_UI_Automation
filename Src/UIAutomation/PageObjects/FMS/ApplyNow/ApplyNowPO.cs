using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.ApplyNow
{
    internal class ApplyNowPo : FmsBasePo
    {
        public ApplyNowPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By FullApplication = By.CssSelector("ul[class*='applystyles__QuickTabs'] li:nth-of-type(2)");
        private readonly By DrugConsentFormText = By.PartialLinkText("Drug Consent Form");
        private readonly By SkillsChecklistFormText = By.PartialLinkText("Skills Checklist");

        public void ClickOnFullApplication()
        {
            Wait.UntilElementClickable(FullApplication).ClickOn();
        }

        public void ClickOnDrugConsentForm()
        {
            Wait.UntilElementClickable(DrugConsentFormText).ClickOn();
        }

        public void ClickOnSkillsChecklistForm()
        {
            Wait.UntilElementClickable(SkillsChecklistFormText).ClickOn();
        }
    }
}
