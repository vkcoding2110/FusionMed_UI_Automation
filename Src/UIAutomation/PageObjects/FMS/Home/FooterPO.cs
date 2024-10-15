using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Home
{
    internal class FooterPo : FmsBasePo
    {
        public FooterPo(IWebDriver driver) : base(driver)
        {
        }

        //Fusion Medical Staff 
        private readonly By FusionMedicalStaff = By.XPath("//a[text()='Fusion Medical Staffing']");
        private readonly By Careers = By.XPath("//a[text()='Careers']");
        private readonly By ContactUs = By.PartialLinkText("Contact us");
        private readonly By StudentOutreach = By.XPath("//div[contains(@class,'FooterNavGroup')][1]//li/a[text()='Student Outreach']");
        private readonly By BeTheChange = By.PartialLinkText("Be the Change");
        private readonly By History = By.XPath("//a[text()='History']");
        private readonly By OnlineTimeCards = By.PartialLinkText("Online Timecards");
        private readonly By SkillsChecklists = By.PartialLinkText("Skills Checklist");
        private readonly By Covid19Response = By.PartialLinkText("COVID-19 Response");

        //Traveler
        private readonly By Traveler = By.XPath("//div[contains(@class,'FooterNavGroup')]//li//a[text()='Traveler']");
        private readonly By Nursing = By.XPath("//div[contains(@class,'FooterNavGroup')]//a[text()='Nursing']");
        private readonly By HomeHealth = By.XPath("//div[contains(@class,'FooterNavGroup')]//a[text()='Home Health']");
        private readonly By CathLab = By.XPath("//div[contains(@class,'FooterNavGroup')]//a[text()='Cath Lab']");
        private readonly By Therapy = By.XPath("//div[contains(@class,'FooterNavGroup')]//a[text()='Therapy']");
        private readonly By Laboratory = By.XPath("//div[contains(@class,'FooterNavGroup')]//a[text()='Laboratory']");
        private readonly By Cardiopulmonary = By.XPath("//div[contains(@class,'FooterNavGroup')]//a[text()='Cardiopulmonary']");
        private readonly By LongTermCare = By.XPath("//div[contains(@class,'FooterNavGroup')]//a[text()='Long Term Care']");
        private readonly By Radiology = By.XPath("//div[contains(@class,'FooterNavGroup')]//a[text()='Radiology']");
        private readonly By Benefits = By.XPath("//div[contains(@class,'FooterNavGroup')]//a[text()='Benefits']");
        private readonly By BenefitsFaq = By.XPath("//img[contains(@src,'benefits-faq.jpg')]/parent::a");
        private readonly By BenefitsGuideBook = By.XPath("//img[contains(@src,'benefits-guide-book.jpg')]/parent::a");
        private readonly By FusionMarketPlace = By.XPath("//div[contains(@class,'FooterNavGroup')]//a[text()='Fusion Marketplace']");

        //Healthcare Providers
        private readonly By HealthcareProviders = By.XPath("//a[text()='Healthcare Providers']");
        private readonly By RequestStaff = By.PartialLinkText("Request Staff");

        //Apply
        private readonly By Apply = By.PartialLinkText("Apply");
        private readonly By QuickApp = By.PartialLinkText("Quick App");
        private readonly By FullApp = By.PartialLinkText("Full App");
        private readonly By ReferralBonus = By.PartialLinkText("Referral Bonus");

        //Blog
        private readonly By Blog = By.XPath("//a[text()='Blog']");

        //Social Media Icons
        private readonly By FacebookIcon = By.XPath("//*[contains(@class,'fa-facebook-f')]//parent::a");
        private readonly By TwitterIcon = By.XPath("//*[contains(@class,'fa-twitter')]//parent::a");
        private readonly By InstagramIcon = By.XPath("//*[contains(@class,'fa-instagram')]//parent::a");
        private readonly By PinterestIcon = By.XPath("//*[contains(@class,'fa-pinterest')]//parent::a");
        private readonly By LinkedInIcon = By.XPath("//*[contains(@class,'fa-linkedin')]//parent::a");
        private readonly By SpotifyIcon = By.XPath("//*[contains(@class,'fa-spotify')]//parent::a");

        // Privacy policy,Terms of use, Policies
        private readonly By Policies = By.LinkText("Policies");
        private readonly By TermsOfUse = By.LinkText("Terms of Use");
        private readonly By PrivacyPolicy = By.LinkText("Privacy Policy");
        private readonly By JointCommission = By.XPath("//div[contains(@class,'FooterLegalContentRight')]/a");

        //NewsLetter
        private readonly By NewsLetterEmail = By.XPath("//div[contains(@class,'FooterContactWrappe')]//div[contains(@class,'InputGroupStyled')]/input[@type='email']");
        private readonly By NewsLetterArrow = By.XPath("//div[contains(@class,'FooterContactWrappe')]//div[contains(@class,'FormGroup')]/following-sibling::button");
        private readonly By NewsletterSuccessText = By.XPath("//div[contains(@class,'Container')]//div[contains(@class,'PrivacyPolicyWrapper')]");

        public void ClickOnFusionMedStaff()
        {
            Wait.UntilElementClickable(FusionMedicalStaff).ClickOn();
        }

        public void ClickOnCareersOption()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(Careers));
            }
            Wait.UntilElementClickable(Careers).ClickOn();
        }

        public string GetCareersLinkHref()
        {
            return Wait.UntilElementVisible(Careers).GetAttribute("href");
        }

        public void ClickOnContactUs()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(ContactUs));
            }
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(FusionMedicalStaff));
            Wait.UntilElementClickable(ContactUs).ClickOn();
        }

        public void ClickOnStudentOutreach()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(FusionMedicalStaff));
            }
            Driver.JavaScriptClickOn(Wait.UntilElementExists(StudentOutreach));
        }

        public void ClickOnBeTheChange()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(FusionMedicalStaff));
            }
            Wait.UntilElementClickable(BeTheChange).ClickOn();
            Wait.HardWait(2000);
        }

        public void ClickOnHistory()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(FusionMedicalStaff));
            }
            Wait.UntilElementClickable(History).ClickOn();
        }

        public string GetOnlineTimeCardsHref()
        {
            return Wait.UntilElementClickable(OnlineTimeCards).GetAttribute("href");
        }

        public void ClickOnSkillsChecklist()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(FusionMedicalStaff));
            Wait.UntilElementClickable(SkillsChecklists).ClickOn();
        }

        public void ClickOnCovid19Response()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(FusionMedicalStaff));
            }
            Wait.UntilElementClickable(Covid19Response).ClickOn();
        }

        //Traveler
        public void ClickOnTraveler()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(FusionMedicalStaff));
            }
            else 
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(Traveler));
            }
            Driver.JavaScriptClickOn(Traveler);
        }

        public void ClickOnOnNursing()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(FusionMedicalStaff));
            }
            Driver.JavaScriptClickOn(Wait.UntilElementExists(Nursing));
        }

        public void ClickOnOnHomeHealth()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(Traveler));
            }
            Driver.JavaScriptClickOn(Wait.UntilElementExists(HomeHealth));
        }

        public void ClickOnOnCathLab()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(Traveler));
            }
            Driver.JavaScriptClickOn(Wait.UntilElementExists(CathLab));
            Wait.HardWait(2000);
        }

        public void ClickOnOnTherapy()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(Traveler));
            }
            Wait.UntilElementClickable(Therapy).ClickOn();
        }

        public void ClickOnOnLaboratory()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(Traveler));
            }
            Wait.UntilElementClickable(Laboratory).ClickOn();
        }

        public void ClickOnOnCardiopulmonary()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(Traveler));
            }
            Wait.UntilElementClickable(Cardiopulmonary).ClickOn();
        }

        public void ClickOnOnLongTermCare()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(Traveler));
            }
            Wait.UntilElementClickable(LongTermCare).ClickOn();
        }

        public void ClickOnOnRadiology()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(Traveler));
            }
            Wait.UntilElementClickable(Radiology).ClickOn();
        }

        public void ClickOnOnBenefits()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(Traveler));
            }
            Wait.UntilElementClickable(Benefits).ClickOn();
        }

        public string GetFusionMarketPlaceHref()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(FusionMarketPlace));
            return Wait.UntilElementVisible(FusionMarketPlace).GetAttribute("href");
        }

        public string GetTravelerBenefitsFaqHref()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(BenefitsFaq));
            return Wait.UntilElementVisible(BenefitsFaq).GetAttribute("href");
        }

        public string GetTravelerBenefitsGuideBookHref()
        {
            return Wait.UntilElementClickable(BenefitsGuideBook).GetAttribute("href");
        }
        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FmsUrl}traveler/benefits/");
            WaitUntilMpPageLoadingIndicatorInvisible();
        }

        //Healthcare Providers
        public void ClickOnHealthcareProviders()
        {
            Wait.UntilElementClickable(HealthcareProviders).ClickOn();
        }

        public void ClickOnRequestStaff()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(HealthcareProviders));
            }
            Wait.UntilElementClickable(RequestStaff).ClickOn();
        }

        //Apply
        public void ClickOnApply()
        {
            Wait.UntilElementClickable(Apply).ClickOn();
        }

        public void ClickOnOnQuickApp()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(Apply));
            }
            Driver.JavaScriptClickOn(Wait.UntilElementExists(QuickApp));
        }

        public void ClickOnOnFullApp()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(Apply));
            }
            Driver.JavaScriptClickOn(Wait.UntilElementExists(FullApp));
        }

        public void ClickOnOnReferralBonus()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(Apply));
            }
            Driver.JavaScriptClickOn(Wait.UntilElementExists(ReferralBonus));
        }

        //Blog
        public string GetBlogHref()
        {
            return Wait.UntilElementClickable(Blog).GetAttribute("href");
        }

        //Social Media Icons
        public string GetFacebookIconHref()
        {
            return Wait.UntilElementClickable(FacebookIcon).GetAttribute("href");
        }

        public string GetTwitterIconHref()
        {
            return Wait.UntilElementClickable(TwitterIcon).GetAttribute("href");
        }

        public string GetInstagramIconHref()
        {
            return Wait.UntilElementClickable(InstagramIcon).GetAttribute("href");
        }

        public string GetPinterestIconHref()
        {
            return Wait.UntilElementClickable(PinterestIcon).GetAttribute("href");
        }

        public string GetLinkedInIconHref()
        {
            return Wait.UntilElementClickable(LinkedInIcon).GetAttribute("href");
        }

        public string GetSpotifyIconHref()
        {
            return Wait.UntilElementClickable(SpotifyIcon).GetAttribute("href");
        }

        // Privacy policy,Terms of use, Policies
        public void ClickOnPoliciesLink()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(Policies));
            Wait.UntilElementClickable(Policies).ClickOn();
            WaitUntilMpPageLoadingIndicatorInvisible();
        }

        public void ClickOnPrivacyPoliciesLink()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(PrivacyPolicy));
            Wait.UntilElementClickable(PrivacyPolicy).ClickOn();
            WaitUntilMpPageLoadingIndicatorInvisible();
        }

        public void ClickOnTermsOfUseLink()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(TermsOfUse));
            Wait.UntilElementClickable(TermsOfUse).ClickOn();
            WaitUntilMpPageLoadingIndicatorInvisible();
        }

        public string GetJointCommissionApprovalHref()
        {
            return Wait.UntilElementVisible(JointCommission).GetAttribute("href");
        }

        public void EnterNewsLetterEmail(string email)
        {
            Wait.UntilElementVisible(NewsLetterEmail).EnterText(email, true);
        }

        public void ClickOnNewsLetterArrow()
        {
            Wait.UntilElementClickable(NewsLetterArrow).ClickOn();
            WaitUntilMpSubmitFormLoadingIndicatorInvisible();
        }

        public string GetNewsletterRepliedSuccessText()
        {
            return Wait.UntilElementVisible(NewsletterSuccessText).GetText().Replace("\r", " ").Replace("\n", "");
        }

        public bool IsNewsletterRepliedSuccessTextDisplayed()
        {
            return Wait.IsElementPresent(NewsletterSuccessText, 3);
        }
    }
}
