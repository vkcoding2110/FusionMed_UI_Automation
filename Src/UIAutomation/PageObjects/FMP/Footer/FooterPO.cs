using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Footer
{
    internal class FooterPo : FmpBasePo
    {
        public FooterPo(IWebDriver driver) : base(driver)
        {
        }

        //FusionMarketplaceLink
        private readonly By FusionMarketplaceLink = By.XPath("//a[text()='Fusion Marketplace']");
        private readonly By AboutUsLink = By.XPath("//div/a[text()='About Us']");
        private readonly By ContactUsLink = By.XPath("//div/a[text()='Contact Us']");

        //Travelers
        private readonly By TravelersLink = By.XPath("//a[text()='Travelers']");
        private readonly By LoginLink = By.XPath("//div/a[text()='Login']");
        private readonly By JobsLink = By.XPath("//div/a[text()='Jobs']");
        private readonly By StateVaccinationMapLink = By.XPath("//div/a[text()='State Vaccination Map']");
        private readonly By BlogLink = By.XPath("//a[text()='Blog']");

        //Social Media Icons
        private readonly By FacebookIcon = By.XPath("//div[contains(@class,'SocialIcons')]//a[@title='Facebook']");
        private readonly By InstagramIcon = By.XPath("//div[contains(@class,'SocialIcons')]//a[@title='Instagram']");
        private readonly By LinkedInIcon = By.XPath("//div[contains(@class,'SocialIcons')]//a[@title='LinkedIn']");

        //TermsAndAgreement,PrivacyPolicy
        private readonly By TravelerTermsAndAgreementLink = By.XPath("//div[contains(@class,'FooterLegalContentLeft')]/a[text()=' Traveler Terms & Agreement']");
        private readonly By TravelerPrivacyPolicyLink = By.XPath("//div[contains(@class,'FooterLegalContentLeft')]/a[text()=' Traveler Privacy Policy']");
        private readonly By PartnerTermsAgreementLink = By.XPath("//div[contains(@class,'FooterLegalContentLeft')]/a[text()='Partner Terms & Agreement']");
        private readonly By PartnerPrivacyPolicy = By.XPath("//div[contains(@class,'FooterLegalContentLeft')]/a[text()=' Partner Privacy Policy']");
        private readonly By EmploymentPolicies = By.XPath("//div[contains(@class,'FooterLegalContentLeft')]/a[text()=' Employment Policies']");

        //FusionMedicalStaffing
        private readonly By FusionMedicalStaffingLogo = By.XPath("//div/a[contains(@class,'mrktLogoSVG')]");

        //Healthcare Employers
        private readonly By HealthcareAgencies = By.XPath("//a[text()='Healthcare Agencies']");
        private readonly By FusionMedicalStaffingLink = By.XPath("//div[contains(@class,'FooterNavContent')]//a[text()='Fusion Medical Staffing']");
        private readonly By BecomeAPartnerLink = By.XPath("//a[text()='Become a Partner']");
        private readonly By AxisMedicalStaffingLink = By.XPath("//div[contains(@class,'FooterNavContent')]//a[text()='Axis Medical Staffing']");
        private readonly By GetMedStaffingLink = By.XPath("//div[contains(@class,'FooterNavContent')]//a[text()='GetMed Staffing, Inc']");
        private readonly By AequorHealthCareAgencyLink = By.XPath("//div[contains(@class,'FooterNavContent')]//a[text()='Aequor Healthcare']");
        private readonly By LeadHealthcareAgencyLink = By.XPath("//div[contains(@class,'FooterNavContent')]//a[text()='Lead Healthstaff']");

        public void ClickOnFusionMarketplaceLink()
        {
            Driver.JavaScriptClickOn(FusionMarketplaceLink);
        }

        public void ClickOnAboutUsLink()
        {
            Driver.JavaScriptClickOn(AboutUsLink);
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnContactUsLink()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(FusionMarketplaceLink));
            Driver.JavaScriptClickOn(ContactUsLink);
        }

        public void ClickOnTravelersLink()
        {
            Driver.JavaScriptClickOn(TravelersLink);
        }

        public void ClickOnLoginLink()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(FusionMarketplaceLink));
            Driver.JavaScriptClickOn(LoginLink);
        }

        public void ClickOnJobsLink()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(FusionMarketplaceLink));
            Driver.JavaScriptClickOn(JobsLink);
        }

        public void ClickOnStateVaccinationMapLink()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(FusionMarketplaceLink));
            Driver.JavaScriptClickOn(StateVaccinationMapLink);
        }

        public string GetStateVaccinationMapHref()
        {
            return Wait.UntilElementVisible(StateVaccinationMapLink).GetAttribute("href");
        }

        public void ClickOnBlogLink()
        {
            Driver.JavaScriptClickOn(BlogLink);
        }

        public string GetBlogHref()
        {
            return Wait.UntilElementClickable(BlogLink).GetAttribute("href");
        }

        public string GetFacebookIconHref()
        {
            return Wait.UntilElementClickable(FacebookIcon).GetAttribute("href");
        }

        public string GetInstagramIconHref()
        {
            return Wait.UntilElementClickable(InstagramIcon).GetAttribute("href");
        }

        public string GetLinkedInIconHref()
        {
            return Wait.UntilElementClickable(LinkedInIcon).GetAttribute("href");
        }

        public void ClickOnTravelerTermsAndAgreementLink()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(FacebookIcon));
            Driver.JavaScriptClickOn(TravelerTermsAndAgreementLink);
        }

        public void ClickOnTravelerPrivacyPolicyLink()
        {
            Driver.JavaScriptClickOn(Wait.UntilElementExists(TravelerPrivacyPolicyLink));
        }

        public void ClickOnPartnerTermsAgreementLink()
        {
            Driver.JavaScriptClickOn(PartnerTermsAgreementLink);
        }

        public void ClickOnPartnerPrivacyPolicyLink()
        {
            Driver.JavaScriptClickOn(PartnerPrivacyPolicy);
        }

        public void ClickOneEmploymentPoliciesLink()
        {
            Driver.JavaScriptClickOn(EmploymentPolicies);
        }

        public void ClickFusionMedicalStaffingLogo()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(TravelerPrivacyPolicyLink));
            Driver.JavaScriptClickOn(FusionMedicalStaffingLogo);
        }

        //Healthcare Agencies
        public void ClickOnHealthcareAgencies()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(HealthcareAgencies));
            Driver.JavaScriptClickOn(HealthcareAgencies);
        }
        public void ClickOnFusionMedicalStaffingLink()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(FusionMedicalStaffingLink));
            }
            Wait.UntilElementClickable(FusionMedicalStaffingLink).ClickOn();
            Driver.JavaScriptClickOn(FusionMedicalStaffingLink);
        }

        public void ClickOnBecomePartnerLink()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(BecomeAPartnerLink));
            }
            Wait.UntilElementClickable(BecomeAPartnerLink).ClickOn();
            Driver.JavaScriptClickOn(BecomeAPartnerLink);
        }
        public void ClickOnAxisMedicalStaffingLink()
        {
            Driver.JavaScriptClickOn(AxisMedicalStaffingLink);
        }

        public void ClickOnGetMedStaffingLink()
        {
            Driver.JavaScriptClickOn(GetMedStaffingLink);
        }
        
        public void ClickOnAequorHealthcareAgencyLink()
        {
            Driver.JavaScriptClickOn(AequorHealthCareAgencyLink);
        }

        public void ClickOnLeadHealthcareAgencyLink()
        {
            Driver.JavaScriptClickOn(LeadHealthcareAgencyLink);
        }
    }
}
