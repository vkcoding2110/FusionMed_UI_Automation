using System;
using System.Globalization;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.Components;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.Licensing
{
    internal class AddLicensingPo : FmpBasePo
    {
        public AddLicensingPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By State = By.CssSelector("select#licensing-state");
        private readonly By Compact = By.XPath("//span[text()='Compact']");
        private readonly By CompactInput = By.CssSelector("input#licensing-compact");
        private readonly By ExpirationDate = By.CssSelector("input#licensing-expiration-date");
        private readonly By LicenseNumber = By.CssSelector("input#licensing-number");
        private readonly By CloseIcon = By.CssSelector("div [class*='MuiDialog'] button[class*='CloseIconWrapper']");
        private readonly By AddLicenseButton = By.XPath("//span[text()='Add License']//parent::button");
        private readonly By SubmitButton = By.XPath("//span[text()='Submit']//parent::button");
        private readonly By CancelButton = By.XPath("//span[text()='cancel']//parent::button[contains(@class,'LicensingEditButton')]");

        //Delete License detail button
        private readonly By DeleteLicense = By.CssSelector("button[class*='DeleteButtonStyled'] span");
        private readonly By DeleteConfirmationButton = By.CssSelector("button[class*='DeleteConfirmationButton'] span");

        //Get pop up header text
        private readonly By AddLicensePopupHeaderText = By.XPath("//div[contains(@class,'MuiPaper-root MuiDialog-paper')]/h5[text()='Add License']");
        private readonly By EditLicensePopUpHeaderText = By.XPath("//h5[text()='Edit License']");

        // Validation message
        private readonly By ValidationMessageOfStateDropDown = By.XPath("//select[@id='licensing-state']/parent::div/following-sibling::p");
        private readonly By ValidationMessageOfExpirationDate = By.CssSelector("p#licensing-expiration-date-helper-text");
        private readonly By ValidationMessageOfLicenseNumber = By.CssSelector("p#licensing-number-helper-text");
        public bool IsAddLicensePopupHeaderTextDisplayed()
        {
            Wait.HardWait(2000); // Waiting as Element always present in DOM
            return Wait.UntilElementExists(AddLicensePopupHeaderText).GetCssValue("visibility").Equals("visible");
        }
        public string GetEditLicensePopUpHeaderText()
        {
            return Wait.UntilElementVisible(EditLicensePopUpHeaderText).GetText();
        }
        public bool IsLicensePopUpPresent()
        {
            Wait.HardWait(2000); // Waiting as Element always present in DOM
            return Wait.IsElementPresent(AddLicenseButton,5);
        }
        public string GetValidationMessageDisplayedForStateField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfStateDropDown).GetText();
        }
        public string GetValidationMessageDisplayedForExpirationDateField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfExpirationDate).GetText();
        }
        public string GetValidationMessageDisplayedForLicenseNumberField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfLicenseNumber).GetText();
        }
        public void AddLicenseData(License license)
        {
            var datePicker = new DatePickerPo(Driver);
            Wait.UntilElementClickable(State).SelectDropdownValueByText(license.State, Driver);
            if (license.Compact)
            {
                if (!Wait.UntilElementExists(CompactInput).IsElementSelected())
                {
                    Wait.UntilElementClickable(Compact).ClickOn();
                }
            }
            else
            {
                if (Wait.UntilElementExists(CompactInput).IsElementSelected())
                {
                    Wait.UntilElementClickable(Compact).ClickOn();
                }
            }
            datePicker.SelectMonthAndYear(license.ExpirationDate, ExpirationDate);
            Wait.UntilElementVisible(LicenseNumber).EnterText(license.LicenseNumber, true);
            if (Wait.IsElementPresent(AddLicenseButton, 2))
            {
                Wait.UntilElementClickable(AddLicenseButton).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(SubmitButton).ClickOn();
            }
        }
        public License GetLicensingData()
        {
            var state = Wait.UntilElementVisible(State).SelectDropdownGetSelectedValue();
            var compact = Wait.UntilElementExists(CompactInput).IsElementSelected();
            var expirationDate = Wait.UntilElementVisible(ExpirationDate).GetAttribute("value");
            var licenseNumber = Wait.UntilElementVisible(LicenseNumber).GetAttribute("value");

            return new License
            {
                State = state,
                Compact = compact,
                ExpirationDate = DateTime.ParseExact(expirationDate, "MMMM yyyy", CultureInfo.InvariantCulture),
                LicenseNumber = licenseNumber,
            };
        }
        public void ClickOnAddLicenseButton()
        {
            Wait.UntilElementClickable(AddLicenseButton).ClickOn();
        }
        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
            Wait.UntilElementInVisible(AddLicenseButton);
        }
        public void ClickOnCancelButton()
        {
            Wait.UntilElementVisible(CancelButton).ClickOn();
            Wait.UntilElementInVisible(AddLicenseButton);
        }
        public void ClickOnDeleteLicenseButton()
        {
            Wait.UntilElementClickable(DeleteLicense).ClickOn();
            Wait.UntilElementClickable(DeleteConfirmationButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public void SelectState(string state)
        {
            Wait.UntilElementClickable(State).SelectDropdownValueByText(state,Driver);
        }
        public void EnterExpirationDate(License license)
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.SelectMonthAndYear(license.ExpirationDate, ExpirationDate);
        }
        public void ClearStateDropDown()
        {
            Wait.UntilElementClickable(State).SelectDropdownValueByIndex(0);
        }
        public void ClearExpirationDate()
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.ClearDateSelection(ExpirationDate);
        }
        public void EnterLicensingNumber(string licensingNumber)
        {
            Wait.UntilElementVisible(LicenseNumber).EnterText(licensingNumber, true);
        }
    }
}
