using System;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.NativeApp
{
    internal class ProfileDetailPagePo : FmpBasePo
    {
        public ProfileDetailPagePo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By ProfileHeaderText = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'Welcome')]");
        private readonly By AddEducationOrCertificationButton = By.XPath("//*[@text='Add Education or Certification']//parent::android.widget.Button");
        private readonly By AddEducationOrCertificationPopupUpCancelIcon = By.XPath("*//android.widget.TextView[@text='Add Education']/parent::android.widget.Button/parent::android.view.ViewGroup/parent::android.view.ViewGroup/following-sibling::android.widget.Button[1]");
        private readonly By CategoryLabel = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'EXP')]/preceding-sibling::android.view.ViewGroup/parent::android.view.ViewGroup/android.widget.TextView[2]");
        private readonly By CertificationLabel = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'EXP')]/preceding-sibling::android.view.ViewGroup/parent::android.view.ViewGroup/android.widget.TextView[1]");
        private readonly By ExpirationMonthAndYearLabel = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'EXP')]");
        private const string EducationAndCertification = "Add Education or Certification";
        private readonly By ShareMyProfile = By.XPath("//android.widget.TextView[@text='Share My Profile']/parent::android.widget.Button | //android.widget.TextView[@text='Manage My Profile Sharing']/parent::android.widget.Button");

        public bool IsProfilePageOpen()
        {
            return Wait.IsElementPresent(ProfileHeaderText, 5);
        }

        public void ClickOnAddEducationOrCertificationButton()
        {
            Driver.ScrollToElementByText(EducationAndCertification);
            Wait.UntilElementClickable(AddEducationOrCertificationButton).ClickOn();
        }

        public bool IsAddEducationOrCertificationPopupIsDisplay()
        {
            return Wait.IsElementPresent(AddEducationOrCertificationPopupUpCancelIcon);
        }

        public Certification GetCertificationDetailsFromProfileDetailPage()
        {
            var certification = Wait.UntilAllElementsLocated(CertificationLabel).Where(e => e.Displayed).Select(e => e.GetText()).Last();
            var category = Wait.UntilAllElementsLocated(CategoryLabel).Where(e => e.Displayed).Select(e => e.GetText()).Last();
            var expirationDate = Wait.UntilAllElementsLocated(ExpirationMonthAndYearLabel).Where(e => e.Displayed).Select(e => e.GetText().Replace("EXP ", "")).Last();

            return new Certification
            {
                CertificationName = certification,
                Category = category,
                ExpirationDate = DateTime.ParseExact(expirationDate, "MM/yyyy", CultureInfo.InvariantCulture)
            };
        }

        public bool IsCertificationLabelPresent()
        {
            return Wait.IsElementPresent(ExpirationMonthAndYearLabel, 5);
        }

        public void ClickOnShareMyProfileButton()
        {
            Wait.UntilElementClickable(ShareMyProfile).ClickOn();
        }
    }
}
