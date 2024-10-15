using System;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.Components;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.EducationAndCertification
{
    internal class CertificationPo : FmpBasePo
    {
        public CertificationPo(IWebDriver driver) : base(driver)
        {
        }

        //Add Certification Tab
        private readonly By CertificationTabName = By.XPath("//button[contains(@class,'SectionTab')]/span[text()='Add Certification']");

        //Delete Certification Button
        private readonly By DeleteCertificationButton = By.CssSelector("button[class*='DeleteButtonStyled']");
        private readonly By DeleteConfirmationButton = By.XPath("//button[contains(@class,'DeleteConfirmationButton')]//span[text()='Delete']");


        //Certification Details
        private readonly By CategoryDropDown = By.CssSelector("select#certification-department");
        private readonly By CertificationDropDown = By.CssSelector("select#certification-certificate");
        private readonly By ExpirationDate = By.CssSelector("input#certification-expiration-date");

        //Add Certification Button 
        private readonly By AddCertificationButton = By.XPath("//span[text()='Add Certification'][@class='MuiButton-label']/parent::button");
        //Submit Button
        private readonly By SubmitButton = By.XPath("//button[contains(@class,'EducationCertificationEditButton')] /span[text()='Submit']");

        //Close Icon
        private readonly By CloseIcon = By.CssSelector("div [class*='MuiDialog'] button[class*='CloseIconWrapper']");

        //Cancel Button
        private readonly By CancelButton = By.XPath("//span[text()='cancel']//parent::button");

        //Validation Message
        private readonly By CategoryValidationMessage = By.XPath("//label[text()='Category']//following-sibling::p");
        private readonly By CertificationValidationMessage = By.XPath("//label[text()='Certification']//following-sibling::p");

        //Device
        private readonly By DeviceCertificationTabHeaderText = By.XPath("//button[contains(@class,'SectionTab')][1]/span[1]");

        public bool IsCertificationTabNameDisplayed()
        {
            return Wait.IsElementPresent(CertificationTabName);
        }
        public void ClickOnAddCertificationTab()
        {
            Wait.UntilElementClickable(CertificationTabName).ClickOn();
        }
        public void WaitTillAddEducationHeaderTextDisplay()
        {
            Wait.WaitUntilTextRefreshed(DeviceCertificationTabHeaderText);
        }
        public void EnterCertificationData(Certification certification)
        {
            var datePicker = new DatePickerPo(Driver);
            SelectCategoryOption(certification.Category);
            Wait.UntilElementVisible(CertificationDropDown).SelectDropdownValueByText(certification.CertificationName, Driver);
            datePicker.SelectMonthAndYear(certification.ExpirationDate, ExpirationDate);
        }

        public void ClickOnAddCertificationButton()
        {
            Wait.UntilElementClickable(AddCertificationButton).ClickOn();
        }

        public Certification GetCertificationDetails()
        {
            var category = Wait.UntilElementVisible(CategoryDropDown).SelectDropdownGetSelectedValue();
            var certification = Wait.UntilElementVisible(CertificationDropDown).SelectDropdownGetSelectedValue();
            var expirationDate = Wait.UntilElementVisible(ExpirationDate).GetAttribute("value");

            return new Certification
            {
                Category = category,
                CertificationName = certification,
                ExpirationDate = DateTime.ParseExact(expirationDate, "MMMM yyyy", CultureInfo.InvariantCulture)
            };
        }

        public void ClickOnDeleteCertificationButton()
        {
            Wait.UntilElementClickable(DeleteCertificationButton).ClickOn();
            Wait.HardWait(2000);
            Wait.UntilAllElementsLocated(DeleteConfirmationButton).First(e => e.Displayed).ClickOn();
            Wait.HardWait(2000);
            Wait.WaitTillElementCountIsLessThan(DeleteConfirmationButton, 1);
        }

        public void ClickOnSubmitButton()
        {
            Wait.UntilAllElementsLocated(SubmitButton).First(e => e.Displayed).ClickOn();
        }
        public void SelectCategoryOption(string category)
        {
            Wait.UntilElementClickable(CategoryDropDown).SelectDropdownValueByText(category, Driver);
        }

        public void ClickOnCertificationCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
            Wait.UntilElementInVisible(CloseIcon);
        }

        public bool IsCertificationPopupPresent()
        {
            return Wait.IsElementPresent(CloseIcon,8);
        }
        public void ClickOnCertificationCancelButton()
        {
            Wait.UntilAllElementsLocated(CancelButton).First(e => e.Displayed).ClickOn();
            Wait.UntilElementInVisible(CancelButton);
        }

        public string GetCategoryValidationMessage()
        {
            return Wait.UntilElementVisible(CategoryValidationMessage).GetText();
        }

        public string GetCertificationValidationMessage()
        {
            return Wait.UntilElementVisible(CertificationValidationMessage).GetText();
        }
    }
}
