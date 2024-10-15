using OpenQA.Selenium;
using System;
using System.Linq;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.Licensing.NativeApp
{
    internal class AddLicensingPo : FmpBasePo
    {
        public AddLicensingPo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By AddLicensingButton = By.XPath("//*[@text='Add Licensing']//parent::android.widget.Button");
        private readonly By LicenseStateDropDown = By.XPath("//*[@text='State *']/following-sibling::android.widget.TextView");
        private readonly By EditLicensePopUpHeaderText = By.XPath("//*[@text='State *']/parent::android.view.ViewGroup/parent::android.view.ViewGroup/android.widget.TextView[1]");
        private static By StateLicenseStateListItem(string state) => By.XPath($"//*[@class='android.widget.TextView'][@text='{state}']");
        private readonly By CompactCheckBox = By.XPath("//*[@class='android.widget.TextView'][@text='Compact']/preceding-sibling::android.widget.CheckBox");
        private readonly By LicenseExpirationDateMonthDropDown = By.XPath("//*[@class='android.widget.TextView'][@text='Month *']/following-sibling::android.widget.TextView");
        private static By LicenseExpirationMonthDropDownOption(string month) => By.XPath($"//*[@text='{month}']/parent::android.view.ViewGroup");
        private readonly By LicenseExpirationYearDropDown = By.XPath("//*[@class='android.widget.TextView'][@text='Year *']/following-sibling::android.widget.TextView");
        private static By LicenseExpirationYearDropDownOption(string year) => By.XPath($"//*[@text='{year}']/parent::android.view.ViewGroup");
        private readonly By LicenseNumberInput = By.XPath("//*[@class='android.widget.TextView'][@text='License Number *']/parent::android.view.ViewGroup//preceding-sibling::android.widget.EditText");
        private readonly By AddLicenseButton = By.XPath("//*[@text='cancel']/parent::android.widget.Button/parent::android.view.ViewGroup/preceding-sibling::android.view.ViewGroup[1]//following-sibling::android.widget.Button");
        private readonly By AddLicenseCloseIcon = By.XPath("//*[@class='android.widget.TextView'][@text='Add License']/following-sibling::android.widget.Button[1]");
        private readonly By LicenseCancelButton = By.XPath("//android.widget.TextView[@text='cancel']/parent::android.widget.Button");

        //GetLicenseDataFromPopUp
        private readonly By LicenseStateText = By.XPath("//*[@text='State *']/following-sibling::android.widget.TextView[1]");
        private readonly By LicenseExpirationDateMonthDropDownText = By.XPath("//*[@class='android.widget.TextView'][@text='Month *']/following-sibling::android.widget.TextView[1]");
        private readonly By LicenseExpirationYearDropDownText = By.XPath("//*[@class='android.widget.TextView'][@text='Year *']/following-sibling::android.widget.TextView[1]");
        private readonly By LicenseNumberInputText = By.XPath("//*[@class='android.widget.TextView'][@text='License Number *']/parent::android.view.ViewGroup//preceding-sibling::android.widget.EditText");
        
        private const string Licensing = "Add Licensing";
        
        public void AddLicenseData(License license)
        {
            ClickOnStateDropDown();
            SelectState(license.State);
            var isCompactCheckbox = Convert.ToBoolean(Wait.UntilElementVisible(CompactCheckBox).GetAttribute("checked"));
            if ((!license.Compact && isCompactCheckbox) || license.Compact && !isCompactCheckbox)
            {
                Wait.UntilElementClickable(CompactCheckBox).ClickOn();
            }
            if (license.Compact && isCompactCheckbox)
            {
                Wait.UntilElementClickable(CompactCheckBox).ClickOn();
            }
            SelectExpirationMonth(license.ExpirationDate);
            SelectExpirationDateYear(license.ExpirationDate);
            EnterLicenseNumber(license.LicenseNumber);
        }

        public void ClickOnAddLicensingButton()
        {
            Driver.ScrollToElementByText(Licensing);
            Wait.UntilElementClickable(AddLicensingButton).ClickOn();
        }

        public void ClickOnAddLicenseButton()
        {
            Wait.UntilElementClickable(AddLicenseButton).ClickOn();
        }

        public void ClickOnStateDropDown()
        {
            Wait.UntilElementClickable(LicenseStateDropDown).ClickOn();
        }

        public void SelectState(string state)
        {
            Wait.UntilElementClickable(StateLicenseStateListItem(state)).ClickOn();
        }

        public void SelectExpirationMonth(DateTime date)
        {
            var month = date.ToString("MMMM");
            Wait.UntilElementClickable(LicenseExpirationDateMonthDropDown).ClickOn();
            Wait.UntilElementInVisible(LicenseExpirationDateMonthDropDown,3);
            Wait.UntilElementClickable(LicenseExpirationMonthDropDownOption(month)).ClickOn();
        }

        public void SelectExpirationDateYear(DateTime date)
        {
            var year = date.ToString("yyyy");
            Wait.UntilElementClickable(LicenseExpirationYearDropDown).ClickOn();
            Driver.ScrollToElementByText(year);
            Wait.UntilElementVisible(LicenseExpirationYearDropDownOption(year), 5);
            Wait.UntilElementClickable(LicenseExpirationYearDropDownOption(year)).ClickOn();
        }
        public void EnterLicenseNumber(string license)
        {
            Wait.UntilElementVisible(LicenseNumberInput).EnterText(license);
        }

        public string GetEditLicensePopUpHeaderText()
        {
            return Wait.UntilElementVisible(EditLicensePopUpHeaderText).GetText();
        }

        public License GetLicenseData()
        {
            var state = Wait.UntilAllElementsLocated(LicenseStateText).Last(e => e.Displayed).GetText();
            var compact = Convert.ToBoolean(Wait.UntilElementVisible(CompactCheckBox).GetAttribute("checked"));
            var expirationDate = Wait.UntilElementVisible(LicenseExpirationDateMonthDropDownText).GetText();
            var expirationYear = Wait.UntilElementVisible(LicenseExpirationYearDropDownText).GetText();
            var licenseNumber = Wait.UntilElementVisible(LicenseNumberInputText).GetText();

            return new License
            {
                State = state,
                Compact = compact,
                ExpirationMonth = expirationDate,
                ExpirationYear = expirationYear,
                LicenseNumber = licenseNumber
            };
        }

        public bool IsAddLicenseCloseIconDisplayed()
        {
            return Wait.IsElementPresent(AddLicenseCloseIcon, 5);
        }

        public void ClickOnLicenseCloseIcon()
        {
            Wait.UntilElementClickable(AddLicenseCloseIcon).ClickOn();
            Wait.UntilElementInVisible(AddLicenseCloseIcon,3);
        }

        public bool IsLicensePopUpPresent()
        {
            return Wait.IsElementPresent(AddLicenseCloseIcon, 5);
        }

        public void ClickOnLicenseCancelButton()
        {
            Wait.UntilElementClickable(LicenseCancelButton).ClickOn();
            Wait.UntilElementInVisible(LicenseCancelButton, 3);
        }
    }
}
