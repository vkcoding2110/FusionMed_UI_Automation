using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Agencies;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.BrowseAll.Agencies
{
    internal class AgencyDetailPo : FmpBasePo
    {
        public AgencyDetailPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By LocationDetail = By.XPath("//div[contains(@class,'AgencyHeaderLocationWrapper')]");
        private readonly By PaidTimeOffDetail = By.XPath("//h5[text()='PTO']//following-sibling::h6");
        private readonly By Plan401Detail = By.XPath("//h5[text()='401k retirement plan']//following-sibling::h6");
        private readonly By ReferralBonusDetail = By.XPath("//h5[text()='Referral bonus']//following-sibling::h6");
        private readonly By InsuranceDetail = By.XPath("//h5[text()='Insurance']//parent::div//ul | //h5[text()='Insurance']//parent::div//h6");
        private readonly By OtherReimbursementsDetail = By.XPath("//h5[text()='Reimbursements']//parent::div/h6 | //h5[text()='Reimbursements']//parent::div/ul");
        private readonly By AgencyCompanyDetail = By.XPath("//div[contains(@class,'MainTabPanelComponent')][2]");
        private readonly By ContactNumber = By.XPath("//div[contains(@class,'ContactContentWrapper')]/div[2]/a/span");
        private readonly By WebsiteUrl = By.XPath("//a[contains(@class,'ContactButton') and @aria-label='Visit agency website']");
        private readonly By AgenciesTitle = By.XPath("//div[contains(@class,'AgencyHeaderDetailsWrapper')]//h1");
        private readonly By AgencyDetailTab = By.XPath("//button[contains(@class,'MuiTab-textColorInherit')]/span[text()='Agency Details']");
        private readonly By BenefitsTab = By.XPath("//button[contains(@class,'MuiTab-textColorInherit')]/span[text()='Benefits']");
        private readonly By QuickApplyButton = By.XPath("//span[text()='Quick Apply']//parent::a");
        private readonly By ViewAllJobsLink = By.XPath("//h2[contains(text(),'Jobs')]//parent::div//following-sibling::div/a");

        // Recruiter data
        private readonly By RecruiterCards = By.XPath("//div[contains(@class,'RecruiterCardStyled')]");
        private readonly By ViewAllRecruiterLink = By.XPath("//a[text()='View all recruiters']");

        //Device element
        private readonly By AgencyDetailTabDevice = By.XPath("//div[contains(@class,'MainTabPanelComponent')][2] | //span[contains(text(),'Agency Details')]//parent::div/following-sibling::div");
        private readonly By InsuranceDetailDevice = By.XPath("//h5[text()='Insurance']//following-sibling::ul | //h5[text()='Insurance']//following-sibling::h6 ");
        private readonly By OtherReimbursementsDetailDevice = By.XPath("//h5[text()='Reimbursements']//following-sibling::ul | //h5[text()='Reimbursements']//following-sibling::h6");
        private readonly By AgencyCompanyDetailDevice = By.XPath("//span[contains(text(),'Agency Details')]//parent::div//parent::div//parent::div//div[contains(@class,'StyledAccordionDetails')]");
        private readonly By ContactNumberDevice = By.XPath("//div[contains(@class,'ContactAndHoursContainer')]/div[3]/a/span");

        public Agency GetAgencyDetails()
        {
            var title = Wait.UntilElementVisible(AgenciesTitle).GetText();
            var location = Wait.UntilElementVisible(LocationDetail).GetText();
            string paidTimeOff = null;
            if (Wait.IsElementPresent(PaidTimeOffDetail,5))
            {
                paidTimeOff = Wait.UntilElementVisible(PaidTimeOffDetail).GetText();
            }
            var url = Wait.UntilElementVisible(WebsiteUrl).GetAttribute("href");
            var contactNumber = Wait.UntilElementVisible(BaseTest.PlatformName != PlatformName.Web ? ContactNumberDevice : ContactNumber).GetText();

            string plan401 = null;
            if (Wait.IsElementPresent(Plan401Detail, 5))
            {
                plan401 = Wait.UntilElementVisible(Plan401Detail).GetText();
            }
            string referralBonus = null;
            if (Wait.IsElementPresent(ReferralBonusDetail, 10))
            {
                referralBonus = Wait.UntilElementVisible(ReferralBonusDetail).GetText();
            }
            string insurance = null;
            if (Wait.IsElementPresent(InsuranceDetail, 10))
            {
                insurance = Wait.UntilElementVisible(BaseTest.PlatformName != PlatformName.Web ? InsuranceDetailDevice : InsuranceDetail).GetText();
            }
            string otherReimbursements = null;
            var otherReimbursementsDetailLocator = OtherReimbursementsDetail;
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                otherReimbursementsDetailLocator = OtherReimbursementsDetailDevice;
            }
            if (Wait.IsElementPresent(otherReimbursementsDetailLocator, 10))
            {
                 otherReimbursements = Wait.UntilElementVisible(otherReimbursementsDetailLocator).GetText();
            }
            ClickOnAgencyDetailsTab();
            var companyDetail = Wait.UntilElementVisible(BaseTest.PlatformName != PlatformName.Web ? AgencyCompanyDetailDevice : AgencyCompanyDetail).GetText();
            ClickOnBenefitsTab();

            return new Agency
            {
                Name = title,
                Location = location,
                PaidTimeOff = paidTimeOff,
                Url = url,
                PhoneNumber = contactNumber,
                Plan401 = plan401,
                ReferralBonus = referralBonus,
                Insurance = insurance,
                OtherReimbursements = otherReimbursements,
                Details = companyDetail
            };
        }

        public void ClickOnAgencyDetailsTab()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                if (BaseTest.PlatformName.Equals(PlatformName.Android))
                {
                    Driver.MoveToElement(Wait.UntilElementExists(AgencyDetailTabDevice));
                }
                Wait.UntilElementClickable(AgencyDetailTabDevice).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(AgencyDetailTab).ClickOn();
            }
        }

        public void ClickOnBenefitsTab()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Web))
            {
                Wait.UntilElementClickable(BenefitsTab).ClickOn();
                Wait.HardWait(1000);
            }
        }
        public void ClickOnAgencyQuickApplyButton()
        {
            Wait.UntilAllElementsLocated(QuickApplyButton).Last(e => e.Displayed).ClickOn();
        }

        public bool IsQuickApplyButtonDisplayed()
        {
            return Wait.IsElementPresent(QuickApplyButton,5);
        }
        public bool IsRecruiterCardDisplayed()
        {
            return Wait.IsElementPresent(RecruiterCards, 8);
        }
        public void ClickOnViewAllRecruiterLink()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(ViewAllJobsLink));
            Wait.UntilElementVisible(ViewAllRecruiterLink).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
    }
}
