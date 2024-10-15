using OpenQA.Selenium;
using System;
using System.Globalization;
using System.Linq;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.Licensing.NativeApp
{
    internal class LicenseDetailsPo : FmpBasePo
    {
        public LicenseDetailsPo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By LicenseEditButton = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'EXP')]/parent::android.view.ViewGroup/preceding-sibling::android.view.ViewGroup");
        private readonly By DeleteLicenseButton = By.XPath("//android.widget.TextView[contains(@text,'delete license')]//parent::android.widget.Button");
        private readonly By DeleteButton = By.XPath("//*[@text='Delete']//parent::android.widget.Button");
        private readonly By LicenseDetailMissingText = By.XPath("//*[@text='LICENSING']//parent::android.view.ViewGroup/following-sibling::android.view.ViewGroup/android.view.ViewGroup//android.widget.TextView[contains(@text,'missing some information.')]");
        private readonly By LicenseStateLabel = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'EXP')]/parent::android.view.ViewGroup/parent::android.view.ViewGroup/android.widget.TextView[1]");
        private readonly By CompactCheckboxText = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'EXP')]/following-sibling::android.widget.TextView");
        private readonly By LicenseNumberLabel = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'EXP')]/parent::android.view.ViewGroup/parent::android.view.ViewGroup/android.widget.TextView[2]");
        private readonly By LicenseExpirationMonthAndYearLabel = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'EXP')]");
        private const string Licensing = "LICENSING";

        public void ClickOnDeleteLicense()
        {
            Wait.UntilElementClickable(DeleteLicenseButton).ClickOn();
            Wait.UntilElementClickable(DeleteButton).ClickOn();
            WaitUntilAppLoadingIndicatorInvisible();
        }
        public void ClickOnEditButton()
        {
            Wait.UntilElementClickable(LicenseEditButton).ClickOn();
        }

        public void DeleteLicense()
        {
            Driver.ScrollToElementByText(Licensing);
            if (Wait.IsElementPresent(LicenseDetailMissingText, 5)) return;
            var allElement = Wait.UntilAllElementsLocated(LicenseEditButton, 10).Count;
            for (var i = 0; i <= allElement; i++)
            {
                ClickOnEditButton();
                ClickOnDeleteLicense();
            }
        }

        public License GetLicenseData()
        {
            var state = Wait.UntilAllElementsLocated(LicenseStateLabel).Last(e => e.Displayed).GetText();
            var compact = Wait.IsElementPresent(CompactCheckboxText);
            var expirationDate = Wait.UntilAllElementsLocated(LicenseExpirationMonthAndYearLabel).Where(e => e.Displayed).Select(e => e.GetText().Replace("EXP ", "")).Last();
            var licenseNumber = Wait.UntilAllElementsLocated(LicenseNumberLabel).Last(e => e.Displayed).GetText().Replace("#", "");

            return new License
            {
                State = state,
                Compact = compact,
                ExpirationDate = DateTime.ParseExact(expirationDate, "MM/yyyy", CultureInfo.InvariantCulture),
                LicenseNumber = licenseNumber
            };
        }

        public bool IsLicensingDetailsPresent()
        {
            return  Wait.IsElementPresent(LicenseStateLabel, 5);
        }
    }
}
