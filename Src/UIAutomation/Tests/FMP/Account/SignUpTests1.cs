using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.YopMail;
using UIAutomation.Utilities;


namespace UIAutomation.Tests.FMP.Account
{
    [TestClass]
    [TestCategory("SignUp"), TestCategory("FMP")]
    public class SignUpTests1 : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatSignUpWorksSuccessfully()
        {
            var passwordPage = new PasswordPo(Driver);
            var checkYouEmailPage = new CheckYourEmailPagePo(Driver);
            var emailList = new EmailListingGridPo(Driver);
            var email = new EmailPo(Driver);
            var confirmPage = new ConfirmPagePo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);
            var signUpPage = new AboutMePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Click on 'Sign Up' link text");
            fmpLogin.ClickOnSignUpLink();

            Log.Info(
                "Step 4: Fill data for 'ABOUT ME' in 'Sign Up' form & click on 'Continue' button and verify user is navigated to the 'Password' data fields");
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            signUpPage.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);
            Assert.IsTrue(passwordPage.IsFilledProgressBarDisplayed(), "Filled Progressbar is not displayed");

            Log.Info(
                "Step 5: Fill the valid password & click on 'Submit' button and verify 'Check Your Email' page is opened");
            passwordPage.FillFormAndSubmit(addAboutMeSignUpData);
            var actualCheckYourEmailMessage = checkYouEmailPage.GetCheckYourEmailMessageText();
            Assert.AreEqual(FmpConstants.CheckYourEmailMessage, actualCheckYourEmailMessage,
                "The Check Your Email Message doesn't match");

            Log.Info("Step 6: Open 'YopMail', Open your 'Confirm Email', click on 'Confirm Email' button & verify Confirmation page gets open");
            new WaitHelpers(Driver).HardWait(10000); // Waiting for 30 seconds for registration email
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(addAboutMeSignUpData.Email);

            emailList.OpenEmail("Confirm Email");
            var actualSenderEmail = emailList.GetSenderEmailText();
            Assert.AreEqual(GlobalConstants.SenderEmailText, actualSenderEmail, "The Sender email doesn't match");

            const string confirmEmail = "Confirm Email";
            emailList.ClickOnButtonOrLink(confirmEmail);
            var actualConformationTitle = confirmPage.GetConfirmationTitle();
            const string expectedConformationTitle = "Woohoo!";
            Assert.AreEqual(expectedConformationTitle, actualConformationTitle, " The confirmation title doesn't match");

            const string expectedConfirmationText = "You're all set up and ready to go!";
            var actualConfirmationText = confirmPage.GetConfirmationText();
            Assert.AreEqual(expectedConfirmationText, actualConfirmationText, "The confirmation text doesn't match");

            Log.Info("Step 7: Click on 'Edit My Profile' button ");
            confirmPage.ClickOnConfirmationLogInButton();

            Log.Info(
                "Step 8: Enter 'Login' credentials & verify profile page gets open and Welcome message is displayed.");
            fmpLogin.LoginToApplication(addAboutMeSignUpData);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();
            new WaitHelpers(Driver).HardWait(2000);
            var expectedProfilePageUrl = FusionMarketPlaceUrl + "profile/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedProfilePageUrl, 180), "Profile page url does not match");
            var actualMessage = profileDetailPage.GetWelcomeMessageText();
            var expectedMessage = "Welcome, " + addAboutMeSignUpData.FirstName + "!";
            Assert.AreEqual(expectedMessage, actualMessage, "Welcome message doesn't match");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyMandatoryFieldsValidationOnAboutMePage()
        {

            var headerHomePagePo = new HeaderPo(Driver);
            var signUpPage = new AboutMePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Click on 'Sign Up' link text");
            fmpLogin.ClickOnSignUpLink();

            Log.Info(
                "Step 4: Do not enter data in 'About Me' fields, Click on 'Continue' button & verify validation message is displayed");
            signUpPage.ClickOnSignUpContinueButton();
            const string expectedAboutMeValidationMessage = "This field is required.";
            var actualAboutMeValidationMessage = signUpPage.GetValidationMessage();
            Assert.AreEqual(expectedAboutMeValidationMessage, actualAboutMeValidationMessage,
                "The validation message doesn't match");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyMandatoryFieldsValidationForPasswordFieldOnPasswordPage()
        {
            var headerHomePagePo = new HeaderPo(Driver);
            var signUpPage = new AboutMePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var passwordPage = new PasswordPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Click on 'Sign Up' link text");
            fmpLogin.ClickOnSignUpLink();

            Log.Info("Step 4: Fill data for 'ABOUT ME' in 'Sign Up' form & click on 'Continue' button");
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            signUpPage.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);

            Log.Info(
                "Step 5: Do not enter 'Password', click on 'Submit' button & verify validation message is displayed");
            passwordPage.ClickOnSubmitButton();
            const string expectedPasswordValidationMessage = "This field is required.";
            var actualPasswordValidationMessage = passwordPage.GetPasswordValidationMessage();
            Assert.AreEqual(expectedPasswordValidationMessage, actualPasswordValidationMessage,
                "The validation message doesn't match");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyMandatoryFieldsValidationForConfirmPasswordFieldOnPasswordPage()
        {
            var headerHomePagePo = new HeaderPo(Driver);
            var signUpPage = new AboutMePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var passwordPage = new PasswordPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Click on 'Sign Up' link text");
            fmpLogin.ClickOnSignUpLink();

            Log.Info("Step 4: Fill data for 'ABOUT ME' in 'Sign Up' form & click on 'Continue' button");
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            signUpPage.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);

            Log.Info(
                "Step 5: Enter 'Password', click on 'Submit' button & verify validation message is displayed for confirm password field");
            passwordPage.EnterPassword(addAboutMeSignUpData);
            passwordPage.ClickOnSubmitButton();
            const string expectedConfirmPasswordValidationMessage = "This field is required.";
            var actualConfirmPasswordValidationMessage = passwordPage.GetConfirmPasswordValidationMessage();
            Assert.AreEqual(expectedConfirmPasswordValidationMessage, actualConfirmPasswordValidationMessage,
                "The 'Confirm Password' validation message doesn't match");

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyValidationMessageWhenPasswordConfirmPasswordNotMatchOnPasswordPage()
        {
            var headerHomePagePo = new HeaderPo(Driver);
            var signUpPage = new AboutMePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var passwordPage = new PasswordPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Click on 'Sign Up' link text");
            fmpLogin.ClickOnSignUpLink();

            Log.Info("Step 4: Fill data for 'ABOUT ME' in 'Sign Up' form & click on 'Continue' button");
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            signUpPage.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);

            Log.Info(
                "Step 5: Enter valid 'Password', enter invalid 'Confirm Password', click on 'Submit' button & verify validation message is displayed");
            passwordPage.EnterPassword(addAboutMeSignUpData);
            addAboutMeSignUpData.Password = "another";
            passwordPage.EnterConfirmPassword(addAboutMeSignUpData);
            const string expectedValidationMessage = "Please enter the same value again.";
            var actualValidationMessage = passwordPage.GetValidationMessageConfirmPassword();
            Assert.AreEqual(expectedValidationMessage, actualValidationMessage,
                "The confirm password validation message does not match");
        }
    }
}
