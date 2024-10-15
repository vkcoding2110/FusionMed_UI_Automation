using System;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.EducationAndCertification.NativeApp
{
    internal class CertificationPo : FmpBasePo
    {
        public CertificationPo(IWebDriver driver) : base(driver)
        {

        }
        private readonly By CertificationTab = By.XPath("//android.widget.TextView[@text='Add Certification']/parent::android.widget.Button");
        private readonly By CategoryDropDown = By.XPath("//*[@text='Category *']/following-sibling::android.widget.TextView");
        private static By CategoryName(string category) => By.XPath($"//*[@class='android.widget.TextView'][@text='{category}']");
        private readonly By CertificationDropDown = By.XPath("//*[@text='Certification *']/following-sibling::android.widget.TextView");
        private static By CertificationName(string certification) => By.XPath($"//*[@class='android.widget.TextView'][@text='{certification}']");
        private readonly By ExpirationMonthDropDown = By.XPath("//*[@text='Month']/following-sibling::android.widget.TextView");
        private static By ExpirationMonthDropDownOption(string month) => By.XPath($"//*[@text='{month}']/parent::android.view.ViewGroup");
        private readonly By ExpirationYearDropDown = By.XPath("//*[@text='Year']/following-sibling::android.widget.TextView");
        private static By ExpirationYearDropDownOption(string year) => By.XPath($"//*[@text='{year}']/parent::android.view.ViewGroup");
        private readonly By AddCertificationButton = By.XPath("//*[@text='cancel']/parent::android.widget.Button/parent::android.view.ViewGroup/preceding-sibling::android.view.ViewGroup[1]//following-sibling::android.widget.Button");
        private readonly By DeleteCertificationButton = By.XPath("//android.widget.TextView[contains(@text,'delete certfication')]//parent::android.widget.Button");
        private readonly By DeleteButton = By.XPath("//*[@text='Delete']//parent::android.widget.Button");
        private readonly By CertificationEditButton = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'EXP')]/preceding-sibling::android.view.ViewGroup//following-sibling::android.widget.Button");
        private readonly By CertificationDetailMissingText = By.XPath("//*[@text='EDUCATION & CERTIFICATION']//parent::android.view.ViewGroup/following-sibling::android.view.ViewGroup/android.view.ViewGroup//android.widget.TextView[contains(@text,'missing some information.')]");
        private readonly By ClearDropDownValue = By.XPath("//android.widget.TextView[@text='cancel']//parent::android.widget.Button//parent::android.view.ViewGroup//following-sibling::android.widget.ScrollView//android.view.ViewGroup/android.view.ViewGroup[1]");
        private readonly By CategoryValidationMessage = By.XPath("//*[@text='Category *']/following-sibling::android.widget.TextView/parent::android.view.ViewGroup/following-sibling::android.widget.TextView[1]");
        private readonly By CertificationValidationMessage = By.XPath("//*[@text='Certification *']/following-sibling::android.widget.TextView/parent::android.view.ViewGroup/following-sibling::android.widget.TextView[1]");
        private readonly By AddEducationOrCertificationPopUpCancelIcon = By.XPath("*//android.widget.TextView[@text='Add Education']/parent::android.widget.Button/parent::android.view.ViewGroup/parent::android.view.ViewGroup/following-sibling::android.widget.Button[1]");
        private readonly By AddEducationOrCertificationPopUpCancelButton = By.XPath("*//android.widget.TextView[@text='Add Education']/parent::android.widget.Button/parent::android.view.ViewGroup/parent::android.view.ViewGroup/following-sibling::android.widget.Button[1]");
        private const string EducationAndCertification = "EDUCATION & CERTIFICATION";
        private const string Category = "ACLS (AHA)";

        public void ClickOnCertificationTab()
        {
            Wait.UntilElementClickable(CertificationTab).ClickOn();
        }

        public void EnterCertificationData(Certification certification)
        {
            ClickOnCategoryDropDown();
            Wait.UntilElementClickable(CategoryName(certification.Category)).ClickOn();
            ClickOnCertificationDropDown();
            Driver.ScrollToElementByText(certification.CertificationName);
            Wait.UntilElementClickable(CertificationName(certification.CertificationName)).ClickOn();
            SelectExpirationMonth(certification.ExpirationDate);
            SelectExpirationDateYear(certification.ExpirationDate);
        }

        public void ClickOnCategoryDropDown()
        {
            Wait.UntilElementClickable(CategoryDropDown).ClickOn();
        }

        public void ClickOnCertificationDropDown()
        {
            Wait.UntilElementClickable(CertificationDropDown).ClickOn();
        }

        public void SelectExpirationMonth(DateTime date)
        {
            var month = date.ToString("MMMM");
            Wait.UntilElementClickable(ExpirationMonthDropDown).ClickOn();
            Wait.UntilElementClickable(ExpirationMonthDropDownOption(month)).ClickOn();
        }

        public void SelectExpirationDateYear(DateTime date)
        {
            var year = date.ToString("yyyy");
            Wait.UntilElementClickable(ExpirationYearDropDown).ClickOn();
            Driver.ScrollToElementByText(year);
            Wait.UntilElementVisible(ExpirationYearDropDownOption(year), 5);
            Wait.UntilElementClickable(ExpirationYearDropDownOption(year)).ClickOn();
        }

        public void ClickOnAddCertification()
        {
            Wait.UntilElementClickable(AddCertificationButton).ClickOn();
        }

        public void ClickOnDeleteCertification()
        {
            Wait.UntilElementClickable(DeleteCertificationButton).ClickOn();
            Wait.UntilElementClickable(DeleteButton).ClickOn();
            WaitUntilAppLoadingIndicatorInvisible();
        }
        public void ClickOnEditButton()
        {
            Wait.UntilElementClickable(CertificationEditButton).ClickOn();
        }

        public void DeleteCertification()
        {
            Driver.ScrollToElementByText(EducationAndCertification);
            if (Wait.IsElementPresent(CertificationDetailMissingText, 5)) return;
            var allElement = Wait.UntilAllElementsLocated(CertificationEditButton, 10).Count;
            for (var i = 0; i <= allElement ; i++)
            {
                ClickOnEditButton();
                ClickOnDeleteCertification();
            }
        }

        public void ClearCategory()
        {
            ClickOnCategoryDropDown();
            Wait.UntilElementClickable(ClearDropDownValue).ClickOn();
        }

        public string GetCategoryValidationMessageText()
        {
            Wait.HardWait(1000);
            return Wait.UntilElementVisible(CategoryValidationMessage).GetText();
        }

        public void EnterCategory(string category)
        {
            ClickOnCategoryDropDown();
            Wait.UntilElementClickable(CategoryName(category)).ClickOn();
        }

        public void EnterCertification(string certification)
        {
            ClickOnCertificationDropDown();
            Driver.ScrollToElementByText(certification);
            Wait.UntilElementClickable(CertificationName(certification)).ClickOn();
        }

        public void ClearCertification()
        {
            ClickOnCertificationDropDown();
            Driver.ScrollToElementByText(Category);
            Wait.HardWait(1000);
            Wait.UntilElementClickable(ClearDropDownValue).ClickOn();
        }

        public string GetCertificationValidationMessageText()
        {
            return Wait.UntilElementVisible(CertificationValidationMessage).GetText();
        }

        public bool IsCertificationPopupDisplayed()
        {
            return Wait.IsElementPresent(AddCertificationButton, 5);
        }

        public void ClickOnAddEducationOrCertificationPopUpCancelIcon()
        {
            Wait.UntilElementClickable(AddEducationOrCertificationPopUpCancelIcon).ClickOn();
            Wait.UntilElementInVisible(AddEducationOrCertificationPopUpCancelIcon, 5);
        }

        public bool IsAddEducationOrCertificationPopupDisplayed()
        {
            return Wait.IsElementPresent(AddEducationOrCertificationPopUpCancelButton, 5);
        }

        public void ClickOnAddEducationOrCertificationPopUpCancelButton()
        {
            Wait.UntilElementClickable(AddEducationOrCertificationPopUpCancelButton).ClickOn();
            Wait.UntilElementInVisible(AddEducationOrCertificationPopUpCancelButton, 5);
        }
    }
}
