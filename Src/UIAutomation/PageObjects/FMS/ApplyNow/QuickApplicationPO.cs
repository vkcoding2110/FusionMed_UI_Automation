using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using UIAutomation.DataObjects.FMS.ApplyNow;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.ApplyNow
{
    internal class QuickApplicationPo : FmsBasePo
    {
        public QuickApplicationPo(IWebDriver driver) : base(driver)
        {

        }

        private readonly By QuickAppFirstNameInput = By.CssSelector("input#input_1");
        private readonly By QuickAppLastNameInput = By.CssSelector("input#input_2");
        private readonly By QuickAppEmailInput = By.CssSelector("input#input_5");
        private readonly By QuickAppPhoneInput = By.CssSelector("input#input_6");
        private readonly By QuickAppSelectCategory = By.CssSelector("select#input_15");
        private readonly By QuickAppSpecialty = By.CssSelector("select#input_16");
        private readonly By QuickAppSignatureCanvas = By.CssSelector("canvas.sigCanvas");
        private readonly By QuickAppChooseFileInput = By.CssSelector("input#input_9");
        private readonly By QuickAppSubmitButton = By.CssSelector("button#button_66");
        private readonly By SomeoneReferredMeCheckbox = By.XPath("//div[contains(@class,'FormGroup')]/div[contains(@class,'CheckboxItemStyled')]/label/input/following-sibling::span");
        private readonly By QuickAppReferredByTextbox = By.CssSelector("input#input_8");
        private readonly By FirstNameFieldValidationMessage = By.XPath("//input[@id='input_1']//parent::div//following-sibling::div");
        private readonly By LastNameFieldValidationMessage = By.XPath("//input[@id='input_2']//parent::div//following-sibling::div");
        private readonly By EmailFieldValidationMessage = By.XPath("//input[@id='input_5']//parent::div//following-sibling::div");
        private readonly By MobilePhoneFieldValidationMessage = By.XPath("//input[@id='input_6']//parent::div//following-sibling::div");
        private readonly By CategoryFieldValidationMessage = By.XPath("//select[@id='input_15']//parent::div//following-sibling::div");
        private readonly By ReferredByFieldValidationMessage = By.XPath("//input[@id='input_8']//parent::div//following-sibling::div");

        public void AddDataInQuickApplication(QuickApplication applyNow)
        {
            EnterFirstName(applyNow.FirstName);
            EnterLastName(applyNow.LastName);
            EnterEmail(applyNow.Email);
            EnterPhoneNumber(applyNow.Phone);
            SelectCategory(applyNow.Category);
            SelectSpecialty(applyNow.Specialty);
            ClickOnSomeoneReferredMe(applyNow.SomeoneReferredMe);
            EnterReferredByText(applyNow.ReferredBy);
            EnterSignature();
            switch (BaseTest.PlatformName)
            {
                case PlatformName.Web:
                    Wait.UntilElementClickable(QuickAppChooseFileInput).SendKeys(applyNow.FilePath);
                    break;
                case PlatformName.Android:
                    Driver.JavaScriptClickOn(QuickAppChooseFileInput);
                    new MobileFileSelectionPo(Driver).SelectFileFromDevice(applyNow.FilePath);
                    break;
                case PlatformName.Ios:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public void ClickOnQuickAppSubmitButton()
        {
            Wait.UntilElementClickable(QuickAppSubmitButton).ClickOn();
        }

        public void EnterFirstName(string firstname)
        {
            Wait.UntilElementVisible(QuickAppFirstNameInput).EnterText(firstname, true);
        }

        public void EnterLastName(string lastname)
        {
            Wait.UntilElementVisible(QuickAppLastNameInput).EnterText(lastname, true);
        }

        public void EnterEmail(string email)
        {
            Wait.UntilElementVisible(QuickAppEmailInput).EnterText(email, true);
        }

        public void EnterPhoneNumber(string phoneNumber)
        {
            Wait.UntilElementVisible(QuickAppPhoneInput).EnterText(phoneNumber, true);
        }

        public void SelectCategory(string category)
        {
            Wait.UntilElementVisible(QuickAppSelectCategory).SelectDropdownValueByText(category, Driver);
        }

        public void SelectSpecialty(string category)
        {
            Wait.UntilElementVisible(QuickAppSpecialty).SelectDropdownValueByText(category, Driver);
        }

        public void ClearCategory()
        {
            Wait.UntilElementVisible(QuickAppSelectCategory).SelectDropdownValueByIndex(0);
        }

        public void EnterReferredByText(string referredBy)
        {
            Wait.UntilElementVisible(QuickAppReferredByTextbox).EnterText(referredBy, true);
        }
        public bool IsReferredByFieldDisplayed()
        {
            return Wait.IsElementPresent(QuickAppReferredByTextbox,5);
        }

        public void EnterSignature()
        {
            var actionBuilder = new Actions(Driver);
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(QuickAppSignatureCanvas));
            actionBuilder.MoveToElement(Wait.UntilElementVisible(QuickAppSignatureCanvas), 200, 125).Click().ClickAndHold(Wait.UntilElementVisible(QuickAppSignatureCanvas)).MoveByOffset(10, 10).MoveByOffset(10, 10).MoveByOffset(15, 15).MoveByOffset(20, -25).Release(Wait.UntilElementVisible(QuickAppSignatureCanvas)).Build().Perform();
        }

        public string GetValidationMessageDisplayedForFirstNameField()
        {
            return Wait.UntilElementVisible(FirstNameFieldValidationMessage).GetText();
        }
        public string GetValidationMessageDisplayedForLastNameField()
        {
            return Wait.UntilElementVisible(LastNameFieldValidationMessage).GetText();
        }
        public string GetValidationMessageDisplayedForEmailField()
        {
            return Wait.UntilElementVisible(EmailFieldValidationMessage).GetText();
        }
        public string GetValidationMessageDisplayedForMobilePhoneField()
        {
            return Wait.UntilElementVisible(MobilePhoneFieldValidationMessage).GetText();
        }
        public string GetValidationMessageDisplayedForCategoryField()
        {
            return Wait.UntilElementVisible(CategoryFieldValidationMessage).GetText();
        }
        public string GetValidationMessageDisplayedForReferredByField()
        {
            return Wait.UntilElementVisible(ReferredByFieldValidationMessage).GetText();
        }

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FmsUrl}apply/quick-application/");
            WaitUntilMpPageLoadingIndicatorInvisible();
        }
        public void ClickOnSomeoneReferredMe(bool select)
        {
            Wait.UntilElementExists(SomeoneReferredMeCheckbox).Check(select);
        }
        public bool IsReferredByCheckboxSelected()
        {
            return Wait.UntilElementExists(SomeoneReferredMeCheckbox).IsElementSelected();
        }
    }
}