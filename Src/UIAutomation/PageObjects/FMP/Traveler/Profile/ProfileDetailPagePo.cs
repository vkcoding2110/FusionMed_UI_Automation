using System;
using System.Globalization;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile
{
    internal class ProfileDetailPagePo : FmpBasePo
    {
        public ProfileDetailPagePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By WelcomeMessage = By.CssSelector("h5[class*='WelcomeHeaderText']");
        private readonly By ProfileName = By.XPath("//h4[contains(@class,'HeaderNameText')]");
        private readonly By EditAboutMeButton = By.XPath("//span[text()='Edit About Me']//parent::button");

        // Employment tab
        private readonly By EmploymentTabButton = By.XPath("//button[contains(@class,'SectionTab')]/span[text()='Employment']");

        // Need help pop-up
        private readonly By NeedHelpLink = By.XPath("//a[text()='Send us a detailed message']");

        // Reference tab
        private readonly By ReferencesTab = By.XPath("//button[contains(@class,'SectionTab')]//span[text()='References']");

        // Licensing tab
        private readonly By LicensingTab = By.XPath("//span[text()='Licensing']//parent::button");
        
        //Edit Profile
        private readonly By PhoneNumberLabel = By.XPath("//label[text()='Phone']//following-sibling::label");
        private readonly By BirthDateLabel = By.XPath("//label[text()='Date of Birth']//following-sibling::label");
        private readonly By CategoryLabel = By.XPath("//label[text()='Category']//following-sibling::label");
        private readonly By SpecialtyLabel = By.XPath("//label[text()='Primary Specialty']//following-sibling::label");
        private readonly By OtherSpecialty = By.XPath("//label[text()='Other Specialties']//following-sibling::label");
        private readonly By BiographyLabel = By.XPath("//label[text()='Biography']//following-sibling::label");
        private readonly By MailingAddressLabel = By.XPath("//label[text()='Mailing Address']/following-sibling::label[contains(@class,'AboutMeText')]");

        //Download resume
        private readonly By DownloadMyResumeButton = By.CssSelector("div[class*='AboutMeResumeContainer'] button[class*='AboutMeResumeButton']");

        //Download My Resume Popup elements
        private readonly By OpenMyResumeButton = By.XPath("//span[text()='Open My Resume']/parent::button[@type='submit']");
        private readonly By SelectAllLabel = By.CssSelector("button[class*='FormSectionSelectAll'] span");
        private readonly By OpenMyResumeButtonLoadingIndicator = By.XPath("//button[contains(@class,'EditButton')]//div[contains(@class,'BeatLoaderWrapper')]");

        //Device Elements
        private readonly By ProfileNameDevice = By.XPath("//div[1]/label[contains(@class,'AboutMeText')]");
        private readonly By AddLicensingButtonDevice = By.XPath("//button[contains(@class,'LicensingEditButton')]/span[text()='Add Licensing']");
        private readonly By DownloadResumeButton = By.XPath("//button/span[text()='Download Resume']");

        //ShareProfile
        private readonly By ManageMyProfileSharingButton = By.XPath("//button[contains(@class,'ManageProfileShareButton')]");

        public DataObjects.FMP.Traveler.Profile.Profile GetProfileAboutMeData()
        {
            var editPhoneNumber = Wait.UntilElementVisible(PhoneNumberLabel).GetText();
            var editBirthdate = Wait.UntilElementVisible(BirthDateLabel).GetText();
            var editCategory = Wait.UntilElementVisible(CategoryLabel).GetText();
            var editSpecialty = Wait.UntilElementVisible(SpecialtyLabel).GetText();
            var editOtherSpecialty = Wait.UntilElementVisible(OtherSpecialty).GetText();
            var editBiography = Wait.UntilElementVisible(BiographyLabel).GetText();
            var mailingAddress = Wait.UntilElementVisible(MailingAddressLabel).GetText().Replace("\n", "").Replace("\r", "");

            return new DataObjects.FMP.Traveler.Profile.Profile
            {
                PhoneNumber = editPhoneNumber,
                DateOfBirth = DateTime.ParseExact(editBirthdate, "M/d/yyyy", CultureInfo.InvariantCulture),
                Category = editCategory,
                PrimarySpecialty = editSpecialty,
                OtherSpecialty = editOtherSpecialty,
                AboutMe = editBiography,
                MailingAddress = mailingAddress
            };
        }
        public string GetWelcomeMessageText()
        {
            return Wait.UntilElementVisible(WelcomeMessage, 10).GetText();
        }
        public void ClickOnEmploymentTab()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Web))
            {
                Wait.UntilElementClickable(EmploymentTabButton).ClickOn();
            }
        }
        public string GetProfileName()
        {
            return BaseTest.PlatformName != PlatformName.Web ? Wait.UntilElementVisible(ProfileNameDevice).GetText() : Wait.UntilElementVisible(ProfileName).GetText();
        }
        public void ClickOnNeedHelpLink()
        {
            Driver.JavaScriptClickOn(Wait.UntilElementExists(NeedHelpLink));
            new NeedHelpPo(Driver).WaitUntilPopupGetsOpen();
        }
        public void ClickOnReferenceTab()
        {
            if (BaseTest.PlatformName == PlatformName.Web)
            {
                Wait.UntilElementClickable(ReferencesTab).ClickOn();
            }
        }
        public void ClickOnLicensingTab()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                if (BaseTest.PlatformName.Equals(PlatformName.Android))
                {
                    Driver.MoveToElement(Wait.UntilElementExists(AddLicensingButtonDevice));
                }
                else
                {
                    Driver.JavaScriptScrollToElement(Wait.UntilElementExists(AddLicensingButtonDevice));
                }
                Wait.UntilElementClickable(AddLicensingButtonDevice).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(LicensingTab).ClickOn();
                Wait.HardWait(1000); //To avoid test flakiness
            }
        }

        public void ClickOnDownloadMyResumeButton()
        {
            Wait.UntilElementClickable(DownloadMyResumeButton).ClickOn();
            ClickOnSelectAllLabel();
            ClickOnOPenMyResume();
            if (BaseTest.PlatformName == PlatformName.Android)
            {
                new MobileFileSelectionPo(Driver).ClickOnDownloadButton();
            }
        }
        public void ClickOnEditAboutMeButton()
        {
            Wait.UntilElementVisible(EditAboutMeButton);
            Wait.UntilElementClickable(EditAboutMeButton).ClickOn();
            new EditAboutMePo(Driver).WaitTillEditAboutMePopupHeaderGetsDisplay();
        }

        //Download My Resume Popup elements
        public void ClickOnSelectAllLabel()
        {
            Wait.UntilElementClickable(SelectAllLabel).ClickOn();
        }
        public void ClickOnOPenMyResume()
        {
            Driver.JavaScriptClickOn(OpenMyResumeButton);
            Wait.UntilElementInVisible(OpenMyResumeButtonLoadingIndicator,20);
        }

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FusionMarketPlaceUrl}profile/");
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnManageMyProfileSharingButton()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(DownloadResumeButton));
            }

            Wait.UntilElementVisible(ManageMyProfileSharingButton);
            Wait.UntilElementClickable(ManageMyProfileSharingButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
    }
}
