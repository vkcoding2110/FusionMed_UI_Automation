using System;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Jobs.JobDetails;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Jobs
{

    internal class QuickApplyFormPo : FmpBasePo
    {
        public QuickApplyFormPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By JobHours = By.XPath("//div[contains(@class,'JobDetailsContent')]//div[contains(@class,'JobHours')]");
        private readonly By JobName = By.XPath("//div[contains(@class,'JobDetailsContent')]//div[contains(@class,'JobName')]");
        private readonly By JobLocation = By.XPath("//div[contains(@class,'JobDetailsContent')]//div[contains(@class,'Location')]");
        private readonly By JobPayAmount = By.XPath("//div[contains(@class,'JobDetailsContent')]//div[contains(@class,'JobPay')]");
        private readonly By JobNoPayPackage = By.XPath("//div[contains(@class,'JobDetailsContent')]//div[contains(@class,'NoPay')]");
        private readonly By CollectInfoPopup = By.CssSelector("div[class*='CallToActionWrapper']");
        private readonly By CancelOnInfoPopup = By.XPath("//span[text()='cancel']");
        private readonly By FirstNameInput = By.CssSelector("input#cta_firstname");
        private readonly By LastNameInput = By.CssSelector("input#cta_lastname");
        private readonly By EmailInput = By.CssSelector("input#cta_email");
        private readonly By PhoneNumberInput = By.CssSelector("input#cta_phone");
        private readonly By StateDropDown = By.CssSelector("div#cta_state_license-select");
        private static By StateListItem(string state) => By.XPath($"//ul[contains(@class,'MuiMenu-list')]//li[text()='{state}']");
        private readonly By ReferredByInput = By.CssSelector("input#cta_referred_by");
        private readonly By UploadResumeInput = By.XPath("//span[text()='Upload Your Resume']//parent::button//following-sibling::input");
        private readonly By SendNowButton = By.XPath("//span[text()='Send now']");
        private readonly By ShareProfileInformationRadioButton = By.XPath("//div[contains(@class,'RadioGroupStyled')]//label[1]//span/input");
        private readonly By ShareProfileNoThanksRadioButton = By.XPath("//div[contains(@class,'RadioGroupStyled')]//label[2]//span/input");

        //Agency Quick Apply
        private readonly By CategoryDropDown = By.CssSelector("select#cta_my_department");
        private readonly By PrimarySpecialtyDropDown = By.CssSelector("select#cta_my_specialty");
        private readonly By QuickApplyAgencyName = By.XPath("//span[contains(@class,'HeaderText')]");
        private readonly By SomeoneReferredMeCheckbox = By.XPath("//label[contains(@class,'FormControlLabelStyled')]//input[@name='referred']");

        private readonly By FirstNameFieldValidationMessage = By.CssSelector("p#cta_firstname-helper-text");
        private readonly By LastNameFieldValidationMessage = By.CssSelector("p#cta_lastname-helper-text");
        private readonly By EmailFieldValidationMessage = By.CssSelector("p#cta_email-helper-text");
        private readonly By MobilePhoneFieldValidationMessage = By.CssSelector("p#cta_phone-helper-text");
        private readonly By CategoryFieldValidationMessage = By.XPath("//select[@id='about-me-department']/parent::div/following-sibling::p");
        private readonly By PrimarySpecialtyFieldValidationMessage = By.XPath("//select[@id='about-me-specialty']/parent::div/following-sibling::p");
        private readonly By HearAboutUsFieldValidationMessage = By.XPath("//label[@id='cta_referred_by-label']/following-sibling::p");

        public bool IsCollectInfoPopupOpened()
        {
            return Wait.IsElementPresent(CollectInfoPopup, 5);
        }

        public void ClickOnCancelOfInfoPopup()
        {
            Wait.UntilElementExists(CancelOnInfoPopup);
            Driver.JavaScriptClickOn(CancelOnInfoPopup);
        }

        public string GetAgencyNameFromPopUp()
        {
            return Wait.UntilElementVisible(QuickApplyAgencyName).GetText();
        }
        public void WaitUntilCollectInfoPopupClosed()
        {
            Wait.UntilElementInVisible(CollectInfoPopup);
        }

        public void AddQuickApplyFormData(QuickApply quickApplyInformation)
        {
            Driver.JavaScriptScroll("0", "10");
            EnterFirstName(quickApplyInformation.FirstName);
            EnterLastName(quickApplyInformation.LastName);
            EnterEmail(quickApplyInformation.Email);
            EnterMobilePhoneNumber(quickApplyInformation.PhoneNumber);
            if (Wait.IsElementPresent(CategoryDropDown, 5))
            {
                SelectCategory(quickApplyInformation.Category);
                SelectPrimarySpecialty(quickApplyInformation.PrimarySpecialty);
            }
            Wait.UntilElementClickable(StateDropDown).ClickOn();
            Wait.UntilElementClickable(StateListItem(quickApplyInformation.State)).ClickOn();
            if (BaseTest.PlatformName == PlatformName.Web || BaseTest.PlatformName == PlatformName.Android)
            {
                Wait.UntilElementClickable(StateListItem(quickApplyInformation.State)).SendKeys(Keys.Escape);
            }
            Wait.HardWait(2000);
            ClickOnSomeoneReferredMe(quickApplyInformation.SomeoneReferredMe);
            EnterReferredByText(quickApplyInformation.ReferredBy);
            try
            {
                Wait.UntilElementExists(UploadResumeInput).SendKeys(quickApplyInformation.ResumeFilePath);
            }
            catch (Exception)
            {
                //Do Nothing
            }
        }

        public QuickApply GetQuickApplyData()
        {
            var firstName = Wait.UntilElementVisible(FirstNameInput).GetAttribute("value");
            var lastName = Wait.UntilElementVisible(LastNameInput).GetAttribute("value");
            var email = Wait.UntilElementVisible(EmailInput).GetAttribute("value");
            var phoneNumber = Wait.UntilElementVisible(PhoneNumberInput).GetAttribute("value");
            string category = null;
            string primarySpecialty = null;
            if (Wait.IsElementPresent(CategoryDropDown, 5))
            {
                category = Wait.UntilElementVisible(CategoryDropDown).SelectDropdownGetSelectedValue();
                primarySpecialty = Wait.UntilElementVisible(PrimarySpecialtyDropDown).SelectDropdownGetSelectedValue();
            }

            return new QuickApply
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Category = category,
                PrimarySpecialty = primarySpecialty
            };
        }

        public void ClickOnSendNow()
        {
            Wait.UntilElementClickable(SendNowButton).ClickOn();
        }
        public bool IsSendNowButtonDisplayed()
        {
            return Wait.IsElementPresent(SendNowButton, 5);
        }
        public void ClickOnShareProfileInformationRadioButton()
        {
            Wait.UntilElementExists(ShareProfileInformationRadioButton).ClickOn();
        }
        public void ClickOnShareProfileNoThanksRadioButton()
        {
            Wait.UntilElementExists(ShareProfileNoThanksRadioButton).ClickOn();
        }
        public bool IsShareProfileRadioButtonSelected()
        {
            return Wait.UntilElementExists(ShareProfileInformationRadioButton).IsElementSelected();
        }
        public bool IsShareProfileRadioButtonEnabled()
        {
            return Wait.IsElementEnabled(ShareProfileInformationRadioButton, 5);
        }
        public void EnterFirstName(string firstname)
        {
            Wait.UntilElementVisible(FirstNameInput).EnterText(firstname, true);
        }
        public void EnterLastName(string lastname)
        {
            Wait.UntilElementVisible(LastNameInput).EnterText(lastname, true);
        }
        public void EnterEmail(string email)
        {
            Wait.UntilElementVisible(EmailInput).EnterText(email, true);
        }
        public void EnterMobilePhoneNumber(string mobilePhone)
        {
            Wait.UntilElementVisible(PhoneNumberInput).EnterText(mobilePhone, true);
        }
        public void SelectCategory(string category)
        {
            Wait.UntilElementClickable(CategoryDropDown).SelectDropdownValueByText(category, Driver);
        }
        public void SelectPrimarySpecialty(string specialty)
        {
            Wait.UntilElementClickable(PrimarySpecialtyDropDown).SelectDropdownValueByText(specialty, Driver);
        }
        public void EnterReferredByText(string referredBy)
        {
            Wait.UntilElementVisible(ReferredByInput).EnterText(referredBy, true);
        }
        public void ClearCategoryDropDown()
        {
            Wait.UntilElementClickable(CategoryDropDown).SelectDropdownValueByIndex(0);
        }
        public void ClearSpecialtyDropDown()
        {
            Wait.UntilElementClickable(PrimarySpecialtyDropDown).SelectDropdownValueByIndex(0);
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
        public string GetValidationMessageDisplayedForPrimarySpecialtyField()
        {
            return Wait.UntilElementVisible(PrimarySpecialtyFieldValidationMessage).GetText();
        }
        public string GetValidationMessageDisplayedForHearAboutUsField()
        {
            return Wait.UntilElementVisible(HearAboutUsFieldValidationMessage).GetText();
        }
        public void ClickOnSomeoneReferredMe(bool select)
        {
            Wait.UntilElementExists(SomeoneReferredMeCheckbox).Check(select);
        }
        public bool IsReferredByCheckboxSelected()
        {
            return Wait.UntilElementExists(SomeoneReferredMeCheckbox).IsElementSelected();
        }
        public string GetJobName()
        {
            return Wait.UntilAllElementsLocated(JobName).First(x => x.IsDisplayed()).GetText();
        }
        public string GetJobLocation()
        {
            return Wait.UntilAllElementsLocated(JobLocation).First(x => x.IsDisplayed()).GetText();
        }
        public string GetJobHours()
        {
            return IsJobHoursPresent() ? Wait.UntilAllElementsLocated(JobHours).First(x => x.IsDisplayed()).GetText().RemoveEndOfTheLineCharacter().RemoveWhitespace() : null;
        }
        public string GetJobPayAmount()
        {
            return !IsJobPayAmountPresent() ? Wait.UntilAllElementsLocated(JobPayAmount).First(x => x.IsDisplayed()).GetText().RemoveEndOfTheLineCharacter().RemoveWhitespace() : null;
        }
        public bool IsJobPayAmountPresent()
        {
            return Wait.IsElementPresent(JobNoPayPackage, 5);
        }

        public bool IsJobHoursPresent()
        {
            return Wait.IsElementPresent(JobHours, 5);
        }
    }
}
