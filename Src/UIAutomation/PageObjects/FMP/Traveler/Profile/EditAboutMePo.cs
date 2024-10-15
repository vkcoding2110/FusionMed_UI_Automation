using System;
using System.Globalization;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile
{
    internal class EditAboutMePo : FmpBasePo
    {
        public EditAboutMePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By FirstNameTextBox = By.CssSelector("input#about-me-first-name");
        private readonly By LastNameTextBox = By.CssSelector("input#about-me-last-name");
        private readonly By MailingAddress = By.CssSelector("input#about-me-mailing-address");
        private readonly By CityTextBox = By.CssSelector("input#about-me-city");
        private readonly By StateDropDown = By.CssSelector("select#about-me-state");
        private readonly By ZipTextBox = By.CssSelector("input#about-me-zipcode");
        private readonly By EmailTextBox = By.CssSelector("input#about-me-email");
        private readonly By PhoneNumberTextBox = By.CssSelector("input#about-me-mobile-number");
        private readonly By DateOfBirthCalender = By.XPath("//input[@id='about-me-dob']");
        private readonly By SecurityNumberPassword = By.CssSelector("input#about-me-ssn");
        private readonly By SecurityNumberEyeIcon = By.XPath("//button[contains(@class,'MuiIconButton-edgeEnd')]");
        private readonly By CategoryDropDown = By.CssSelector("select#about-me-department");
        private readonly By PrimarySpecialtyDropDown = By.CssSelector("select#about-me-specialty");
        private readonly By OtherSpecialtyDropDown = By.CssSelector("div#about-me-other-specialties-select");
        private static By SelectOtherSpecialty(string item) => By.XPath($"//li[text()='{item}']");
        private readonly By OtherSpecialtyInput = By.CssSelector("div[class*='InputRender'] div");
        private readonly By YearsOfExperienceTextBox = By.CssSelector("input#about-me-years-experience");
        private readonly By ExperienceYesRadioButton = By.XPath("//span[text()='Yes']/preceding-sibling::span[contains(@class,'MuiRadio-root')]//input[@name='travelHealthCareExperience']");
        private readonly By ExperienceNoRadioButton = By.XPath("//span[text()='No']/preceding-sibling::span[contains(@class,'MuiRadio-root')]//input[@name='travelHealthCareExperience']");
        private readonly By BiographyTextArea = By.CssSelector("textarea#about-me-biography");
        private readonly By SubmitButton = By.CssSelector("button[class*='AboutMeEditButton'][type='submit']");
        private readonly By CloseIcon = By.CssSelector("div [class*='MuiDialog'] button[class*='CloseIconWrapper']");
        private readonly By CancelButton = By.XPath("//button[contains(@class,'AboutMeEditButton')]//child::span[text()='cancel']");
        //assert
        private readonly By EditAboutMePageHeaderText = By.CssSelector("h5[class*='EditHeaderText']");
        private readonly By ValidationMessageOfFirstNameField = By.CssSelector("p#about-me-first-name-helper-text");
        private readonly By ValidationMessageOfLastNameField = By.CssSelector("p#about-me-last-name-helper-text");
        private readonly By ValidationMessageOfMailingAddressField = By.CssSelector("p#about-me-mailing-address-helper-text");
        private readonly By ValidationMessageOfCityField = By.CssSelector("p#about-me-city-helper-text");
        private readonly By ValidationMessageOfStateField = By.XPath("//select[@id='about-me-state']/parent::div/following-sibling::p");
        private readonly By ValidationMessageOfZipCodeField = By.CssSelector("p#about-me-zipcode-helper-text");
        private readonly By ValidationMessageOfPhoneNumberField = By.CssSelector("p#about-me-mobile-number-helper-text");
        private readonly By ValidationMessageOfDateOfBirthField = By.CssSelector("p#about-me-dob-helper-text");
        private readonly By ValidationMessageOfCategoryField = By.XPath("//select[@id='about-me-department']/parent::div/following-sibling::p");
        private readonly By ValidationMessageOfPrimarySpecialtyField = By.XPath("//select[@id='about-me-specialty']/parent::div/following-sibling::p");
        private readonly By ValidationMessageOfBiographyField = By.CssSelector("p#about-me-biography-helper-text");
        private readonly By ValidationMessageOfYearsOfExperience = By.CssSelector("p#about-me-years-experience-helper-text");

        //Safari element
        private static By SelectOtherSpecialtySafari(string item) => By.XPath($"//ul[@aria-labelledby='about-me-other-specialties-label']//li[text()='{item}']//span//input");

        //Device elements
        private readonly By BiographyTextAreaDevice = By.XPath("//XCUIElementTypeStaticText[@name='A little bit about me...']");

        public string GetEditAboutMePageHeaderText()
        {
           return Wait.UntilElementVisible(EditAboutMePageHeaderText).GetText();
        }

        public void WaitTillEditAboutMePopupHeaderGetsDisplay()
        {
            Wait.WaitUntilTextRefreshed(EditAboutMePageHeaderText);
        }

        public void EditData(DataObjects.FMP.Traveler.Profile.Profile editProfile)
        {
            EnterFirstName(editProfile.FirstName);
            EnterLastName(editProfile.LastName);
            EnterMailingAddress(editProfile.MailingAddress);
            EnterCityName(editProfile.City);
            SelectState(editProfile.License.State);
            EnterZipCode(editProfile.ZipCode);
            EnterPhoneNumber(editProfile.PhoneNumber);
            Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(SecurityNumberPassword));
            Wait.UntilElementVisible(DateOfBirthCalender).EnterText(editProfile.DateOfBirth.ToString("MM/dd/yyyy"), true);
            Wait.UntilElementVisible(SecurityNumberPassword).EnterText(editProfile.SocialSecurityNumber, true);
            SelectCategory(editProfile.Category);
            EnterBiography(editProfile.AboutMe);
            SelectPrimarySpecialty(editProfile.PrimarySpecialty);
            Wait.UntilElementClickable(OtherSpecialtyDropDown).ClickOn();
            var isIos = BaseTest.PlatformName.Equals(PlatformName.Ios);
            Wait.UntilElementClickable(isIos ? SelectOtherSpecialtySafari(editProfile.OtherSpecialty) : SelectOtherSpecialty(editProfile.OtherSpecialty)).ClickOn();
            if (isIos)
            {
                Wait.UntilElementClickable(SecurityNumberPassword).Click();
            }
            else
            {
                Wait.UntilElementClickable(SelectOtherSpecialty(editProfile.OtherSpecialty)).SendKeys(Keys.Escape);
                Wait.UntilElementInVisible(SelectOtherSpecialty(editProfile.OtherSpecialty));
            }
            Wait.UntilElementVisible(YearsOfExperienceTextBox).EnterText(editProfile.YearsOfExperience, true);
            if (editProfile.HealthcareExperience)
            {
                Wait.UntilElementExists(ExperienceYesRadioButton).Check();
            }
            else
            {
                Wait.UntilElementExists(ExperienceNoRadioButton).Check();
            }
        }
        public DataObjects.FMP.Traveler.Profile.Profile GetEditAboutMeData()
        {
            var firstName = Wait.UntilElementVisible(FirstNameTextBox).GetAttribute("value");
            var lastName = Wait.UntilElementVisible(LastNameTextBox).GetAttribute("value");
            var phoneNumber = Wait.UntilElementVisible(PhoneNumberTextBox).GetAttribute("value");
            Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(SecurityNumberEyeIcon));
            var birthdate = Wait.UntilElementVisible(DateOfBirthCalender).GetAttribute("value");
            Wait.UntilElementClickable(SecurityNumberEyeIcon).ClickOn();
            var securityNumber = Wait.UntilElementVisible(SecurityNumberPassword).GetAttribute("value");
            var category = Wait.UntilElementVisible(CategoryDropDown).SelectDropdownGetSelectedValue();
            var specialty = Wait.UntilElementVisible(PrimarySpecialtyDropDown).SelectDropdownGetSelectedValue();
            var otherSpecialty = Wait.UntilElementVisible(OtherSpecialtyInput).GetText();
            var yearsOfExperience = Wait.UntilElementVisible(YearsOfExperienceTextBox).GetAttribute("value");
            var healthcareExperience = Wait.UntilElementExists(ExperienceYesRadioButton).IsElementSelected();
            //var biography = Wait.UntilElementVisible(BiographyTextArea).GetText();
            var mailingAddress = Wait.UntilElementVisible(MailingAddress).GetAttribute("value");
            var city = Wait.UntilElementVisible(CityTextBox).GetAttribute("value");
            var state = Wait.UntilElementVisible(StateDropDown).SelectDropdownGetSelectedValue();
            var zip = Wait.UntilElementVisible(ZipTextBox).GetAttribute("value");

            return new DataObjects.FMP.Traveler.Profile.Profile
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                DateOfBirth = DateTime.ParseExact(birthdate, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                SocialSecurityNumber = securityNumber,
                Category = category,
                PrimarySpecialty = specialty,
                OtherSpecialty = otherSpecialty,
                YearsOfExperience = yearsOfExperience,
                HealthcareExperience = healthcareExperience,
                //AboutMe = biography,
                MailingAddress = mailingAddress,
                City = city,
                ZipCode = zip,
                License = new License
                {
                    State = state
                }
            };
        }

        public void ClickOnSubmitButton()
        {
            Wait.UntilElementClickable(SubmitButton).ClickOn();
        }
        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
            Wait.UntilElementInVisible(CancelButton,2);
        }
        public void ClickOnCloseButton()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
            Wait.UntilElementInVisible(CloseIcon, 2);
        }

        public bool IsEditAboutMePopUpOpened()
        {
            return Wait.IsElementPresent(EditAboutMePageHeaderText, 3);
        }
        public void EnterFirstName(string firstName)
        {
            Wait.UntilElementVisible(FirstNameTextBox).EnterText(firstName, true);
        }
        public void EnterLastName(string lastName)
        {
            Wait.UntilElementVisible(LastNameTextBox).EnterText(lastName, true);
        }
        public void EnterMailingAddress(string mailingAddress)
        {
            Wait.UntilElementVisible(MailingAddress).EnterText(mailingAddress, true);
        }
        public void EnterCityName(string city)
        {
            Wait.UntilElementVisible(CityTextBox).EnterText(city, true);
        }
        public void SelectState(string state)
        {
            Wait.UntilElementVisible(StateDropDown).SelectDropdownValueByText(state, Driver);
        }
        public void ClearState()
        {
            Wait.UntilElementVisible(StateDropDown).SelectDropdownValueByIndex(0);
        }
        public void EnterZipCode(string zipCode)
        {
            Wait.UntilElementVisible(ZipTextBox).EnterText(zipCode, true);
        }
        public void EnterPhoneNumber(string phoneNumber)
        {
            Wait.UntilElementVisible(PhoneNumberTextBox).EnterText(phoneNumber, true);
        }
        public bool IsEmailFieldDisabled()
        {
            return Wait.IsElementEnabled(EmailTextBox);
        }
        public void EnterDateOfBirth(string dateOfBirth)
        {
            Wait.UntilElementVisible(DateOfBirthCalender).EnterText(dateOfBirth, true);
        }
        public void ClearCategoryDropDown()
        {
            Wait.UntilElementClickable(CategoryDropDown).SelectDropdownValueByIndex(0);
        }
        public void SelectCategory(string category)
        {
            Wait.UntilElementClickable(CategoryDropDown).SelectDropdownValueByText(category, Driver);
        }
        public void ClearPrimarySpecialtyDropDown()
        {
            Wait.UntilElementClickable(PrimarySpecialtyDropDown).SelectDropdownValueByIndex(0);
        }
        public void SelectPrimarySpecialty(string specialty)
        {
            Wait.UntilElementClickable(PrimarySpecialtyDropDown).SelectDropdownValueByText(specialty, Driver);
        }
        public void EnterBiography(string biography)
        {
            if (BaseTest.PlatformName == PlatformName.Ios)
            {
                Driver.NativeAppEnterText(BiographyTextAreaDevice, biography);
            }
            else
            {
                Wait.UntilElementVisible(BiographyTextArea).EnterText("");
                Wait.UntilElementVisible(BiographyTextArea).EnterText(biography, true);
            }
        }
        public void EnterYearsOfExperience(string yearsOfExperience)
        {
            Wait.UntilElementVisible(YearsOfExperienceTextBox).EnterText(yearsOfExperience, true);
        }
        public string GetValidationMessageDisplayedForFirstNameField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfFirstNameField,10).GetText();
        }
        public string GetValidationMessageDisplayedForLastNameField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfLastNameField,10).GetText();
        }
        public string GetValidationMessageDisplayedForMailingAddressField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfMailingAddressField,10).GetText();
        }
        public string GetValidationMessageDisplayedForCityField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfCityField,10).GetText();
        }
        public string GetValidationMessageDisplayedForStateField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfStateField,10).GetText();
        }
        public string GetValidationMessageDisplayedForZipCodeField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfZipCodeField,10).GetText();
        }
        public string GetValidationMessageDisplayedForPhoneNumberField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfPhoneNumberField,10).GetText();
        }
        public string GetValidationMessageDisplayedForCategoryField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfCategoryField,10).GetText();
        }
        public string GetValidationMessageDisplayedForPrimarySpecialtyField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfPrimarySpecialtyField,10).GetText();
        }
        public string GetValidationMessageDisplayedForBiographyField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfBiographyField, 10).GetText();
        }
        public string GetValidationMessageDisplayedForYearsOfExperienceField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfYearsOfExperience, 10).GetText();
        }
        public string GetValidationMessageDisplayedForDateOfBirthField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfDateOfBirthField, 10).GetText();
        }
    }
}
