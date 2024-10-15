using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Account;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.YopMail;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class EditAboutMeTests1 : FmpBaseTest
    {

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void Profile_AboutMe_VerifyUserCanUpdateDetailsSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var signUpPage = new AboutMePo(Driver);
            var passwordPage = new PasswordPo(Driver);
            var emailListingGrid = new EmailListingGridPo(Driver);
            var email = new EmailPo(Driver);
            var confirmPage = new ConfirmPagePo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);
            var editProfile = new EditAboutMePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Signing up as a new user and login as new user");
            headerHomePagePo.ClickOnLogInButton();
            fmpLogin.ClickOnSignUpLink();
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            signUpPage.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);
            passwordPage.FillFormAndSubmit(addAboutMeSignUpData);
            new WaitHelpers(Driver).HardWait(15000); // Waiting for 15 seconds for registration email
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(addAboutMeSignUpData.Email);
            emailListingGrid.OpenEmail("Confirm Email");
            const string confirmEmail = "Confirm Email";
            emailListingGrid.ClickOnButtonOrLink(confirmEmail);
            confirmPage.ClickOnConfirmationLogInButton();
            fmpLogin.LoginToApplication(addAboutMeSignUpData);
            editProfile.WaitUntilFmpTextLoadingIndicatorInvisible();
            var expectedProfilePageUrl = FusionMarketPlaceUrl + "profile/";
            new WaitHelpers(Driver).WaitUntilUrlMatched(expectedProfilePageUrl, 15);

            Log.Info("Step 3: Click on 'Edit About Me' button and editing information");
            profileDetail.ClickOnEditAboutMeButton();
            const string expectedHeaderText = "Edit About Me";
            var actualHeaderText = editProfile.GetEditAboutMePageHeaderText();
            Assert.AreEqual(expectedHeaderText, actualHeaderText, "Page Title not matched");

            var editDetail = AboutMeDataFactory.EditProfileDetails();
            editProfile.EditData(editDetail);
            editProfile.ClickOnSubmitButton();
            editProfile.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Verify that Edited detail are shown on Profile detail page");
            var actualEditDetails = profileDetail.GetProfileAboutMeData();
            var expectedName = editDetail.FirstName + " " + editDetail.LastName;
            var expectedMailingAddress = editDetail.MailingAddress + editDetail.City + "," + editDetail.License.StateAlias + editDetail.ZipCode;
            Assert.AreEqual(expectedName, profileDetail.GetProfileName(), "Name is incorrect");
            Assert.AreEqual(editDetail.PhoneNumber, actualEditDetails.PhoneNumber, "Phone number is not matched");
            var expectedBirthDate = editDetail.DateOfBirth.ToString("MM dd yyyy");
            var actualBirthDate = actualEditDetails.DateOfBirth.ToString("MM dd yyyy");
            Assert.AreEqual(expectedBirthDate, actualBirthDate, "Birthdate is not matched");
            Assert.AreEqual(editDetail.Category, actualEditDetails.Category, "Category is not matched");
            Assert.AreEqual(editDetail.PrimarySpecialty, actualEditDetails.PrimarySpecialty, "Specialty is not matched");
            Assert.AreEqual(editDetail.OtherSpecialty, actualEditDetails.OtherSpecialty, "Specialty is not matched");
            Assert.AreEqual(editDetail.AboutMe, actualEditDetails.AboutMe, "Biography is not matched");
            Assert.AreEqual(expectedMailingAddress.RemoveWhitespace(), actualEditDetails.MailingAddress.RemoveWhitespace(), "Mailing Address is not matched");

            profileDetail.ClickOnEditAboutMeButton();
            var actualEditData = editProfile.GetEditAboutMeData();
            Assert.AreEqual(editDetail.DateOfBirth.ToString("MM dd yyyy"), actualEditData.DateOfBirth.ToString("MM dd yyyy"), "Date of birth is not matched");
            Assert.IsTrue(editDetail.SocialSecurityNumber.EndsWith(actualEditData.SocialSecurityNumber), $"The social security number is not matched. Expected : {editDetail.SocialSecurityNumber}, Actual : {actualEditData.SocialSecurityNumber}");
            Assert.AreEqual(editDetail.Category, actualEditData.Category, "Category is not matched");
            Assert.AreEqual(editDetail.PrimarySpecialty, actualEditData.PrimarySpecialty, "Primary Specialty is not matched");
            Assert.AreEqual(editDetail.OtherSpecialty, actualEditData.OtherSpecialty, "Other Specialty is not matched");
            Assert.AreEqual(editDetail.HealthcareExperience, actualEditData.HealthcareExperience, "Healthcare Experience is not selected");
            //Assert.AreEqual(editDetail.AboutMe, actualEditData.AboutMe, "About me text is not matched");
            Assert.AreEqual(editDetail.License.State, actualEditData.License.State, "State is not matched");

            if (PlatformName == PlatformName.Web || PlatformName == PlatformName.Ios)
            {
                Assert.AreEqual(editDetail.FirstName, actualEditData.FirstName, "First name is not matched");
                Assert.AreEqual(editDetail.LastName, actualEditData.LastName, "Last name is not matched");
                Assert.AreEqual(editDetail.PhoneNumber, actualEditData.PhoneNumber, "Phone number is not matched");
                Assert.AreEqual(editDetail.YearsOfExperience, actualEditData.YearsOfExperience, "Years of experience is not matched");
                Assert.AreEqual(editDetail.MailingAddress, actualEditData.MailingAddress, "About me text is not matched");
                Assert.AreEqual(editDetail.City, actualEditData.City, "City is not matched");
                Assert.AreEqual(editDetail.ZipCode, actualEditData.ZipCode, "Zip Code is not matched");
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Profile_AboutMe_VerifyCloseIconAndCancelButtonWorksSuccessfully()
        {
            var headerHomePagePo = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);
            var editProfile = new EditAboutMePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            editProfile.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetail.NavigateToPage();

            Log.Info("Step 5: Click on 'Edit About Me' button");
            profileDetail.ClickOnEditAboutMeButton();

            Log.Info("Step 6: Click on 'Close' button");
            editProfile.ClickOnCloseButton();

            Log.Info("Step 7: Verify Edit about me page is Closed.");
            Assert.IsFalse(editProfile.IsEditAboutMePopUpOpened(), "Edit About me Page is not open");

            Log.Info("Step 8: Click on 'Edit About Me' button");
            profileDetail.ClickOnEditAboutMeButton();

            Log.Info("Step 9: Click on 'Cancel' button & Verify Edit about me page is closed.");
            editProfile.ClickOnCancelButton();
            Assert.IsFalse(editProfile.IsEditAboutMePopUpOpened(), "Edit About me Page is not open");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void Profile_AboutMe_VerifyValidationMessageIsDisplayedOnMandatoryFields()
        {
            var headerHomePagePo = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);
            var editProfile = new EditAboutMePo(Driver);

            const string mandatoryFieldValidationMessage = FmpConstants.MandatoryFieldValidationMessage;

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            editProfile.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetail.NavigateToPage();

            Log.Info("Step 5: Click on 'Edit About Me' button");
            profileDetail.ClickOnEditAboutMeButton();

            Log.Info("Step 6: Clear 'First Name' field, Click on 'Submit' button & Verify Validation message is displayed for 'First name' field");
            var editDetail = AboutMeDataFactory.EditProfileDetails();
            editProfile.EnterFirstName("");
            editProfile.ClickOnSubmitButton();
            Assert.AreEqual(mandatoryFieldValidationMessage, editProfile.GetValidationMessageDisplayedForFirstNameField(), "Validation message is not displayed for 'First name' field");

            Log.Info("Step 7: Clear 'Last Name' field, Click on 'Submit' button & Verify Validation message is displayed for 'Last name' field");
            editProfile.EnterFirstName(editDetail.FirstName);
            editProfile.EnterLastName("");
            editProfile.ClickOnSubmitButton();
            Assert.AreEqual(mandatoryFieldValidationMessage, editProfile.GetValidationMessageDisplayedForLastNameField(), "Validation message is not displayed for 'Last name' field");

            Log.Info("Step 8: Verify that 'Email' field is disabled");
            Assert.IsFalse(editProfile.IsEmailFieldDisabled(), "Email field is enabled");

            Log.Info("Step 9: Clear 'mailing address' and Verify Validation message is displayed for 'Mailing address' field");
            editProfile.EnterLastName(editDetail.LastName);
            editProfile.EnterMailingAddress("");
            editProfile.ClickOnSubmitButton();
            Assert.AreEqual(mandatoryFieldValidationMessage, editProfile.GetValidationMessageDisplayedForMailingAddressField(), "Validation message is not displayed for 'Mailing address' field");

            Log.Info("Step 10: Clear 'City' and Verify Validation message is displayed for 'City' field");
            editProfile.EnterMailingAddress(editDetail.MailingAddress);
            editProfile.EnterCityName("");
            editProfile.ClickOnSubmitButton();
            Assert.AreEqual(mandatoryFieldValidationMessage, editProfile.GetValidationMessageDisplayedForCityField(), "Validation message is not displayed for 'City' field");

            Log.Info("Step 11: Clear 'State' and Verify Validation message is displayed for 'State' field");
            editProfile.EnterCityName(editDetail.City);
            editProfile.ClearState();
            editProfile.ClickOnSubmitButton();
            Assert.AreEqual(mandatoryFieldValidationMessage, editProfile.GetValidationMessageDisplayedForStateField(), "Validation message is not displayed for 'State' field");

            Log.Info("Step 12: Clear 'Zipcode' and Verify Validation message is displayed for 'Zipcode' field");
            editProfile.SelectState(editDetail.License.State);
            editProfile.EnterZipCode("");
            editProfile.ClickOnSubmitButton();
            Assert.AreEqual(mandatoryFieldValidationMessage, editProfile.GetValidationMessageDisplayedForZipCodeField(), "Validation message is not displayed for 'Zipcode' field");

            Log.Info("Step 13: Clear 'Phone number' and Verify Validation message is displayed for 'Phone number' field");
            editProfile.EnterZipCode(editDetail.ZipCode);
            editProfile.EnterPhoneNumber("");
            editProfile.ClickOnSubmitButton();
            Assert.AreEqual(mandatoryFieldValidationMessage, editProfile.GetValidationMessageDisplayedForPhoneNumberField(), "Validation message is not displayed for 'Phone number' field");

            Log.Info("Step 14: Clear 'Date of birth' and Verify Validation message is displayed for 'Date of birth' field");
            editProfile.EnterPhoneNumber(editDetail.PhoneNumber);
            editProfile.EnterDateOfBirth("");
            editProfile.ClickOnSubmitButton();
            Assert.AreEqual(FmpConstants.MandatoryDateFieldValidationMessage, editProfile.GetValidationMessageDisplayedForDateOfBirthField(), "Validation message is not displayed for 'Date of birth' field");

            Log.Info("Step 15: Clear 'Category' drop-down and Verify Validation message is displayed for 'Category' field");
            editProfile.EnterDateOfBirth(editDetail.DateOfBirth.ToString("MM/dd/yyyy"));
            editProfile.ClearCategoryDropDown();
            editProfile.ClickOnSubmitButton();
            Assert.AreEqual(mandatoryFieldValidationMessage, editProfile.GetValidationMessageDisplayedForCategoryField(), "Validation message is not displayed for 'Category' field");

            Log.Info("Step 16: Clear 'Primary specialty' field, and Verify Validation message is displayed for 'Primary specialty' field");
            editProfile.SelectCategory(editDetail.Category);
            editProfile.ClearPrimarySpecialtyDropDown();
            editProfile.ClickOnSubmitButton();
            Assert.AreEqual(mandatoryFieldValidationMessage, editProfile.GetValidationMessageDisplayedForPrimarySpecialtyField(), "Validation message is not displayed for 'Primary specialty' field");

            Log.Info("Step 17: Clear 'Years of Experience' field, and Verify Validation message is displayed for 'Years of Experience' field");
            editProfile.SelectPrimarySpecialty(editDetail.PrimarySpecialty);
            editProfile.EnterYearsOfExperience("");
            editProfile.ClickOnSubmitButton();
            Assert.AreEqual(mandatoryFieldValidationMessage, editProfile.GetValidationMessageDisplayedForYearsOfExperienceField(), "Validation message is not displayed for 'Years of Experience' field");
        }
    }
}
