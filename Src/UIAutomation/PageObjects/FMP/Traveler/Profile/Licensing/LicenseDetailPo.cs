using System;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.Licensing
{
    internal class LicenseDetailsPo : FmpBasePo
    {
        public LicenseDetailsPo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By AddLicensingButton = By.XPath("//span[text()='Add Licensing']//parent::button");
        private readonly By EditLicensingButton = By.XPath("//button[contains(@class,'ContentEditButton')]");
        private readonly By StateDropDown = By.XPath("//label[contains(@class,'ItemHeaderTitleText')]");
        private readonly By CompactCheckBox = By.XPath("//label[contains(@class,'ItemHeaderDescriptionSubText')][text()='Compact']");
        private readonly By LicenseNumberTextBox = By.XPath("//label[contains(@class,'ItemHeaderDescriptionText')]");
        private static By SpecificLicenseNumber(string licenseNumber) => By.XPath($"//div[contains(@class,'ItemHeaderColumn')]/label[text()='{licenseNumber}']");
        private readonly By ExpirationDate = By.XPath("//label[contains(@class,'ExpirationText')]");

        //Device element
        private readonly By CompactCheckBoxDevice = By.XPath(" //label[contains(@class,'ItemHeaderDescriptionSubText')]/div/div/following-sibling::div[text()='Compact']");

        public License GetLicenseData()
        {
            var state = Wait.UntilAllElementsLocated(StateDropDown).Last(e => e.Displayed).GetText();
            var compact = Wait.IsElementPresent(BaseTest.PlatformName != PlatformName.Web ? CompactCheckBoxDevice : CompactCheckBox);
            var expirationDate = Wait.UntilAllElementsLocated(ExpirationDate).Last(e => e.Displayed).GetText();
            var licenseNumber = Wait.UntilAllElementsLocated(LicenseNumberTextBox).Last(e => e.Displayed).GetText().Replace("#", "");

            return new License
            {
                State = state,
                Compact = compact,
                ExpirationDate = DateTime.ParseExact(expirationDate, "MM/yyyy", CultureInfo.InvariantCulture),
                LicenseNumber = licenseNumber
            };
        }
        public void ClickOnAddLicensingButton()
        {
            if (BaseTest.PlatformName == PlatformName.Web)
            {
                Wait.UntilAllElementsLocated(AddLicensingButton).Last(x => x.IsDisplayed()).ClickOn();
            }
        }
        public void ClickOnEditLicensingButton()
        {
            Wait.UntilAllElementsLocated(EditLicensingButton).Last(x => x.IsDisplayed()).ClickOn();
        }
        public void DeleteAllLicenseDetails()
        {
            if (BaseTest.PlatformName == PlatformName.Web)
            {
                new ProfileDetailPagePo(Driver).ClickOnLicensingTab();
            }
            var allElement = Wait.UntilAllElementsLocated(EditLicensingButton).Where(e => e.Displayed).ToList().Count;
            for (var i = 0; i < allElement; i++)
            {
                Wait.UntilAllElementsLocated(EditLicensingButton).Last(e => e.Displayed).ClickOn();
                new AddLicensingPo(Driver).ClickOnDeleteLicenseButton();
            }
        }

        public bool IsLicensingPresent(string licensingNumber)
        {
            return Wait.IsElementPresent(SpecificLicenseNumber("#" + licensingNumber),8);
        }
    }
}
