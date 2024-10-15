using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using UIAutomation.DataObjects.FMS.ApplyNow;
using UIAutomation.PageObjects.Components;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.ApplyNow
{
    internal class DrugConsentPo : FmsBasePo
    {
        public DrugConsentPo(IWebDriver driver) : base(driver)
        {

        }

        private readonly By DrugFirstNameInput = By.Id("input_1000");
        private readonly By DrugLastNameInput = By.Id("input_1001");
        private readonly By DrugPhoneInput = By.Id("input_11");
        private readonly By DrugEmailInput = By.Id("input_10");
        private readonly By DrugSelectCategory = By.Id("input_1002");
        private readonly By DrugSpecialty = By.Id("input_1003");
        private readonly By DrugDate = By.XPath("//label[contains(text(),'Date')]/preceding-sibling::input[contains(@class,'form-control input')]");
        private readonly By SubmitButton = By.Id("button_60");
        private readonly By Signature = By.ClassName("sigCanvas");
        private readonly By ClearSignature = By.CssSelector("div[class*='clear-sig-pad'] svg");
        
        public void WaitUntilPageLoaded()
        {
            Wait.WaitTillElementCountIsLessThan(SubmitButton, 0);
        }

        public void AddDataInDrugConsentForm(DrugConsentForm data)
        {
            new HomePagePo(Driver).CloseReviewPopupIfPresent();
            EnterFirstName(data.FirstName);
            EnterLastName(data.LastName);
            EnterPhoneNumber(data.Phone);
            EnterEmail(data.Email);
            SelectCategory(data.Category);
            SelectSpecialty(data.Speciality);
            EnterSignature();
            EnterDate(data);
        }

        public void DrugConsentForm_ClickOnSubmitButton()
        {
            Wait.UntilElementClickable(SubmitButton).ClickOn();
        }

        public void EnterFirstName(string firstname)
        {
            Wait.UntilElementVisible(DrugFirstNameInput).EnterText(firstname, true);
        }

        public void EnterLastName(string lastname)
        {
            Wait.UntilElementVisible(DrugLastNameInput).EnterText(lastname, true);
        }

        public void EnterEmail(string email)
        {
            Wait.UntilElementVisible(DrugEmailInput).EnterText(email, true);
        }

        public void EnterPhoneNumber(string phoneNumber)
        {
            Wait.UntilElementVisible(DrugPhoneInput).EnterText(phoneNumber, true);
        }

        public void SelectCategory(string category)
        {
            Wait.UntilElementVisible(DrugSelectCategory).SelectDropdownValueByText(category);
        }

        public void SelectSpecialty(string category)
        {
            Wait.UntilElementVisible(DrugSpecialty).SelectDropdownValueByText(category);
        }

        public void ClearCategory()
        {
            Wait.UntilElementVisible(DrugSelectCategory).SelectDropdownValueByIndex(0);
        }

        public void EnterSignature()
        {
            var actionBuilder = new Actions(Driver);
            actionBuilder.MoveToElement(Wait.UntilElementVisible(Signature), 200, 125).Click().ClickAndHold(Wait.UntilElementVisible(Signature)).MoveByOffset(10, 10).MoveByOffset(10, 10).MoveByOffset(15, 15).MoveByOffset(20, -25).Release(Wait.UntilElementVisible(Signature)).Build().Perform();
        }

        public void ClearSignaturePad()
        {
            Wait.UntilElementClickable(ClearSignature).ClickOn();
        }

        public void EnterDate(DrugConsentForm date)
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.SelectDate(date.Date, DrugDate);
        }


        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FmsUrl}forms/drug-consent-form/");
            WaitUntilMpPageLoadingIndicatorInvisible();
        }

    }
}
