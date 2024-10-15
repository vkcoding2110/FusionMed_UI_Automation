using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.YopMail;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Account
{
    [TestClass]
    [TestCategory("Login"), TestCategory("FMP")]
    public class LoginTests2 : FmpBaseTest
    {

        private static readonly Login NewUserLogin = GetLoginUsersByType("LoginTests");

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyBackAndCancelButtonWorkSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Enter email address in 'Email' field");
            fmpLogin.EnterEmailIdAndContinue(FusionMarketPlaceLoginCredentials);

            Log.Info("Step 4: Click on Back button");
            fmpLogin.ClickOnBackButton();
            Assert.IsTrue(fmpLogin.IsUserNameInputBoxPresent(), "User Name input box is not present");

            Log.Info("Step 5: Click on 'cancel' button.");
            fmpLogin.ClickOnCancelButton();
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 6: Verify that after clicking on 'cancel' button user navigates to home page");
            var expectedUrl = FusionMarketPlaceUrl;
            var actualUrl = Driver.GetCurrentUrl();
            Assert.AreEqual(expectedUrl, actualUrl, "URL is not matched");

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyValidationMessageDisplayedForBlankEmailField()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Click on 'Continue' button without entering any data in 'Email' field");
            fmpLogin.ClickOnContinueButton();

            Log.Info("Step 4: Verify that Validation message is displayed if user leaves email field blank");
            const string expectedValidationMessage = "This field is required.";
            var actualValidationMessage = fmpLogin.GetValidationMessageForEmailField();
            Assert.AreEqual(expectedValidationMessage, actualValidationMessage, "Validation message is not displayed for email field");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyValidationMessageDisplayedForInvalidEmail()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Enter Invalid data in 'Email' field");
            var credentials = new Login() { Email = "EmailId_Test" + new CSharpHelpers().GenerateRandomNumber().ToString() };
            fmpLogin.EnterEmail(credentials.Email);

            Log.Info("Step 4: Click on 'Continue' button ");
            fmpLogin.ClickOnContinueButton();

            Log.Info("Step 5: Verify that Validation message is displayed for Invalid email id");
            const string expectedValidationMessage = "Please enter a valid email address.";
            var actualValidationMessage = fmpLogin.GetValidationMessageForEmailField();
            Assert.AreEqual(expectedValidationMessage, actualValidationMessage, "Invalid validation message is displayed for email field");

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyValidationMessageDisplayedForBlankPasswordField()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
        
            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Enter Valid Email id in 'Email' field.");
            fmpLogin.EnterEmailIdAndContinue(FusionMarketPlaceLoginCredentials);

            Log.Info("Step 4: Click on 'Submit' button without entering any data in 'Password' field");
            fmpLogin.ClickOnSubmitButton();

            Log.Info("Step 5: Verify that Validation message is displayed for blank password field");
            const string expectedValidationMessage = "This field is required.";
            var actualValidationMessage = fmpLogin.GetValidationMessageForPasswordField();
            Assert.AreEqual(expectedValidationMessage, actualValidationMessage, "Invalid validation message is displayed for password field");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyValidationMessageDisplayedForInvalidPassword()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Enter Valid Email id in 'Email' field");
            fmpLogin.EnterEmailIdAndContinue(FusionMarketPlaceLoginCredentials);

            Log.Info("Step 4: Enter InValid Password in 'Password' field");
            fmpLogin.EnterInValidPassword();

            Log.Info("Step 5: Click on 'Submit' button");
            fmpLogin.ClickOnSubmitButton();

            Log.Info("Step 6: Verify that Validation message is displayed for Invalid Password");
            Assert.IsTrue(fmpLogin.IsValidationMessageDisplayedForInValidData(), "Invalid validation message is displayed for password field");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatTravelerIsLockedOutOfTheirProfileWhileEnteringWrongPasswordMultipleTimes()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var lockedOutPage = new AccountLockedOutPo(Driver);
            var forgotPasswordPage = new ForgetPasswordPo(Driver);
            var email = new EmailPo(Driver);
            var emailListGrid = new EmailListingGridPo(Driver);
            var createNewPasswordPage = new CreateNewPasswordPo(Driver);
            var confirmPage = new ConfirmPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Enter Valid Email id in 'Email' field.");
            fmpLogin.EnterEmailIdAndContinue(NewUserLogin);

            Log.Info("Step 4: Enter invalid password multiple times and verify account locked out page gets open");
            fmpLogin.EnterIncorrectPasswordMultipleTimes();

            const string expectedLockedOutHeaderText = "Whoops!";
            var actualLockedOutHeaderText = lockedOutPage.GetLockedOutPageHeaderText();
            Assert.AreEqual(expectedLockedOutHeaderText, actualLockedOutHeaderText, "The locked out page header text is not matched");

            const string expectedLockedOutMessageText = "Log in attempts have exceeded the limit and the account has been locked out. Try again later or reset your password.";
            var actualLockedOutMessageText = lockedOutPage.GetLockedOutPageMessageText();
            Assert.AreEqual(expectedLockedOutMessageText, actualLockedOutMessageText, "The locked out page message text is not matched");

            Log.Info("Step 5: Click 'Reset Password' button & verify 'Forgot Password' page gets open");
            lockedOutPage.ClickOnLockedOutPageResetPasswordButton();
            var forgotPasswordPageUrl = $"https://accounts-{Env}.fusionmarketplace.com/Account/ForgotPassword";
            Assert.IsTrue(Driver.IsUrlContains(forgotPasswordPageUrl), "The forgot password page is not opened ");
            const string expectedForgotPasswordHeader = "Forgot Password";
            var actualForgotPasswordHeader = forgotPasswordPage.GetForgetPasswordPageHeader();
            Assert.AreEqual(expectedForgotPasswordHeader, actualForgotPasswordHeader, "The 'Forgot Password' header is not matched");

            Log.Info("Step 6: Enter 'Email', click on 'Find Account' button, Open 'YopMail', click on 'Confirm Email' button");
            forgotPasswordPage.EnterEmailOnForgotPasswordPage(NewUserLogin);
            forgotPasswordPage.ClickOnFindAccountButton();
            new WaitHelpers(Driver).HardWait(10000);
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(NewUserLogin.Email);
            emailListGrid.OpenEmail("Reset Password");
            const string resetPassword = "Reset Password";
            emailListGrid.ClickOnButtonOrLink(resetPassword);

            Log.Info("Step 7: Fill the new password & click on 'Submit' button and verify 'created a brand spankin' new password!' message is displayed");
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            var newPassword = "Test@" + randomNumber.Remove(5);
            createNewPasswordPage.EnterNewPasswordAndConfirmPassword(newPassword);
            createNewPasswordPage.ClickOnCreateNewPasswordSubmitButton();
            var actualConformationTitle = confirmPage.GetNewConfirmationTitle();
            const string expectedConformationTitle = "Woohoo!";
            Assert.AreEqual(expectedConformationTitle, actualConformationTitle, " The confirmation title doesn't match");

            const string expectedConfirmationText = "You have successfully created a brand spankin' new password!";
            var actualConfirmationText = confirmPage.GetNewConfirmationText();
            Assert.AreEqual(expectedConfirmationText, actualConfirmationText, "The confirmation text doesn't match");

            Log.Info("Step 8: Click on 'Login' button");
            confirmPage.ClickOnLogInButton();

            Log.Info("Step 9: Login to application with 'Email' & new 'Password' & verify ");
            NewUserLogin.Password = newPassword;
            fmpLogin.LoginToApplication(NewUserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            var actualProfileName = headerHomePagePo.GetUserName().RemoveWhitespace();
            Assert.AreEqual(NewUserLogin.Name.RemoveWhitespace(), actualProfileName, "The profile name is not matched");
        }
    }
}