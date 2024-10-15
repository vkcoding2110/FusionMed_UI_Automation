using OpenQA.Selenium;
using System.Linq;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Core.Common
{
    internal class LeftNavPo : CoreBasePo
    {
        public LeftNavPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By LeftNavMenuDiv = By.TagName("body");
        private readonly By ProcessingIndicator = By.XPath("//div[text()='Processing...']");

        private readonly By DashboardMenu = By.PartialLinkText("Dashboard");
        private readonly By FacilityMenu = By.PartialLinkText("Facility");
        private readonly By CandidateMenu = By.PartialLinkText("Candidate");
        private readonly By AssignmentMenu = By.XPath("//a[@data-name='Assignment'][text()='Assignment']");
        private readonly By AdministrationMenu = By.PartialLinkText("Administration");
        private readonly By EntitiesMenu = By.PartialLinkText("Package Items");
        private readonly By SpecificationsMenu = By.PartialLinkText("Specifications");
        private readonly By SubcategoriesMenu = By.PartialLinkText("Subcategories");
        private readonly By PackagesMenu = By.PartialLinkText("Packages");
        private readonly By RuleSearchMenu = By.PartialLinkText("Rule Search");

        private readonly By HolidayTimeMenu = By.CssSelector("a.nav-link[data-name='Holiday Time']");
        private readonly By AccountReceivableMenu = By.CssSelector("a.nav-link[data-name='Account Receivable']");
        private readonly By DashboardIFrame = By.XPath("//iframe[@class='iframe fullheight']");
        private readonly By DashboardHeaderText = By.XPath("//div[@id='menuMy']//span[@class='lead']");


        public void SwitchToDashboardIFrame()
        {
            Driver.SwitchToIframe(Wait.UntilAllElementsLocated(DashboardIFrame).First(e => e.Displayed));
            WaitUntilCoreProcessingTextInvisible();
            WaitTillDashboardHeaderGetsDisplay();
            Driver.SwitchToDefaultIframe();
        }

        public void WaitTillDashboardHeaderGetsDisplay()
        {
            Wait.WaitUntilTextRefreshed(DashboardHeaderText);
        }


        public void WaitUntilProcessingIndicatorInvisible()
        {
            Wait.UntilElementInVisible(ProcessingIndicator, 10);
        }

        public bool IsLeftNavExpanded()
        {
            SwitchToDashboardIFrame();
            return Wait.UntilElementExists(LeftNavMenuDiv).GetAttribute("class").Equals("nav-md");
        }

        public bool IsLeftNavCollapsed()
        {
            return Wait.UntilElementExists(LeftNavMenuDiv).GetAttribute("class").Equals("nav-sm");
        }

        public void ClickOnDashboardMenu()
        {
            Wait.UntilElementClickable(DashboardMenu).ClickOn();
        }

        public void ClickOnFacilityMenu()
        {
            SwitchToDashboardIFrame();
            Wait.UntilElementClickable(FacilityMenu).ClickOn();
        }

        public void ClickOnCandidateMenu()
        {
            SwitchToDashboardIFrame();
            Wait.UntilElementClickable(CandidateMenu).ClickOn();
        }

        public void ClickOnAssignmentMenu()
        {
            SwitchToDashboardIFrame();    
            Wait.UntilElementClickable(AssignmentMenu).ClickOn();
        }


        public void ClickOnAdministrationMenu()
        {
            Wait.UntilElementVisible(AdministrationMenu);
            Wait.UntilElementClickable(AdministrationMenu).ClickOn();
        }

        public void ClickOnEntitiesMenu()
        {
            Wait.UntilElementVisible(EntitiesMenu);
            Wait.UntilElementClickable(EntitiesMenu).ClickOn();
        }

        public void ClickOnSpecificationsMenu()
        {
            Wait.UntilElementVisible(SpecificationsMenu);
            Wait.UntilElementClickable(SpecificationsMenu).ClickOn();
        }

        public void ClickOnSubcategoriesMenu()
        {
            Wait.UntilElementVisible(SubcategoriesMenu);
            Wait.UntilElementClickable(SubcategoriesMenu).ClickOn();
        }

        public void ClickOnPackagesMenu()
        {
            Wait.UntilElementVisible(PackagesMenu);
            Wait.UntilElementClickable(PackagesMenu).ClickOn();
        }

        public void ClickOnRuleSearchMenu()
        {
            Wait.UntilElementVisible(RuleSearchMenu);
            Wait.UntilElementClickable(RuleSearchMenu).ClickOn();
        }

        public void ClickOnHolidayTimeMenu()
        {
            Wait.UntilElementVisible(HolidayTimeMenu);
            Wait.UntilElementClickable(HolidayTimeMenu).ClickOn();
        }

        public void ClickOnAccountReceivableMenu()
        {
            Wait.UntilElementVisible(AccountReceivableMenu);
            Wait.UntilElementClickable(AccountReceivableMenu).ClickOn();
        }
    }
}
