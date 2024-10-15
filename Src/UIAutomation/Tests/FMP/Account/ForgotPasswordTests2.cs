using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.YopMail;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Account
{
    [TestClass]
    [TestCategory("ForgotPassword"), TestCategory("FMP")]
    public class ForgotPasswordTests2 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByType("ForgotPasswordTests");

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatBlankPasswordFieldValidationMessageIsDisplayed()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var forgotPasswordPage = new ForgetPasswordPo(Driver);
            var emailListingGrid = new EmailListingGridPo(Driver);
            var email = new EmailPo(Driver);
            var createNewPasswordPage = new CreateNewPasswordPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info(
                $"Step 3: Login to application with credentials - Email:{UserLogin.Email} & Click on 'Continue' button");
            fmpLogin.EnterEmailIdAndContinue(UserLogin);

            Log.Info("Step 4: Click on 'Forgot Password' link");
            fmpLogin.ClickOnForgetPasswordLink();

            Log.Info("Step 5: Click on 'Find Account' button");
            forgotPasswordPage.ClickOnFindAccountButton();

            Log.Info("Step 6: Open 'YopMail', Open your 'Reset Password', click on 'Reset Password' link");
            new WaitHelpers(Driver).HardWait(10000);
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(UserLogin.Email);
            emailListingGrid.OpenEmail("Reset Password");
            const string resetPassword = "Reset Password";
            emailListingGrid.ClickOnButtonOrLink(resetPassword);

            Log.Info(
                "Step 7: Do not enter password & confirm password, Click on 'Submit' button & verify error message is displayed");
            createNewPasswordPage.ClickOnCreateNewPasswordSubmitButton();
            new WaitHelpers(Driver).HardWait(3000);
            var url = $"https://accounts-{Env}.fusionmarketplace.com/Account/ResetPassword";
            Assert.IsTrue(Driver.IsUrlContains(url), "Url is not correct");
            const string expectedErrorMessageText = "The Password field is required.";
            var actualErrorMessageText = createNewPasswordPage.GetErrorMessage();
            Assert.AreEqual(expectedErrorMessageText, actualErrorMessageText, "The 'Error' Message doesn't match");

            Log.Info(
                "Step 8: Enter 'Password', Do not enter 'Confirm password', click on 'Submit' button & verify validation message is displayed");
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            var newPassword = "Test@" + randomNumber.Remove(5);
            createNewPasswordPage.EnterPassword(newPassword);
            createNewPasswordPage.ClickOnCreateNewPasswordSubmitButton();
            new WaitHelpers(Driver).HardWait(2000);
            const string expectedErrorMessage = "The password and confirmation password do not match.";
            var actualErrorMessage = createNewPasswordPage.GetErrorMessage();
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error message is not matched");

            Log.Info(
                "Step 9: Enter different values in 'Password' & 'Confirm Password' and verify validation message is displayed");
            createNewPasswordPage.EnterPassword(newPassword);
            var invalidConfirmPassword = "User@#" + randomNumber.Remove(6);
            createNewPasswordPage.EnterInvalidConfirmPassword(invalidConfirmPassword);
            const string expectedErrorMessageForInvalidPassword = "Please enter the same value again.";
            var actualErrorMessageForInvalidPassword = createNewPasswordPage.GetInvalidConfirmPasswordErrorMessage();
            Assert.AreEqual(expectedErrorMessageForInvalidPassword, actualErrorMessageForInvalidPassword,
                "The invalid confirm password error message is not displayed");
        }

        
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyRequestNewLinkWorkSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var forgotPasswordPage = new ForgetPasswordPo(Driver);
            var emailListingGrid = new EmailListingGridPo(Driver);
            var email = new EmailPo(Driver);
            var createNewPasswordPage = new CreateNewPasswordPo(Driver);
            var requestNewLink = new ResetNewLinkPo(Driver);
            var checkYourEmailPage = new CheckYourEmailPagePo(Driver);
            var confirmPage = new ConfirmPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            forgotPasswordPage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();
            forgotPasswordPage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info(
                $"Step 3: Login to application with credentials - Email:{UserLogin.Email} & Click on 'Continue' button");
            fmpLogin.EnterEmailIdAndContinue(UserLogin);

            Log.Info("Step 4: Click on 'Forgot Password' link");
            fmpLogin.ClickOnForgetPasswordLink();

            Log.Info("Step 5: Click on 'Find Account' button");
            forgotPasswordPage.ClickOnFindAccountButton();

            Log.Info("Step 6: Open 'YopMail', Open your 'Reset Password', click on 'Reset Password' link");
            new WaitHelpers(Driver).HardWait(10000);
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(UserLogin.Email);
            emailListingGrid.OpenEmail("Reset Password");
            const string resetPassword = "Reset Password";
            emailListingGrid.ClickOnButtonOrLink(resetPassword);

            Log.Info("Step 7: Fill the new password & click on 'Submit' button");
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            var newPassword = "Test@" + randomNumber.Remove(5);
            createNewPasswordPage.EnterNewPasswordAndConfirmPassword(newPassword);
            createNewPasswordPage.ClickOnCreateNewPasswordSubmitButton();

            Log.Info(
                "Step 8: Open 'YopMail', Open your 'Reset Password', click on 'Reset Password' link & verify 'Request New Link' page is opened");
            new WaitHelpers(Driver).HardWait(30000);
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(UserLogin.Email);
            emailListingGrid.OpenEmail("Reset Password");
            emailListingGrid.ClickOnButtonOrLink(resetPassword);

            const string expectedRequestNewLinkHeaderText = "Whoops!";
            var actualRequestNewLinkHeaderText = requestNewLink.GetRequestNewLinkHeaderText();
            Assert.AreEqual(expectedRequestNewLinkHeaderText, actualRequestNewLinkHeaderText,
                "The reset new link header text doesn't match");

            const string expectedRequestNewLinkMessageText =
                "This link has expired. Click the link below and we'll email a new one.";
            var actualRequestNewLinkMessageText = requestNewLink.GetRequestNewLinkMessageText();
            Assert.AreEqual(expectedRequestNewLinkMessageText, actualRequestNewLinkMessageText,
                "The reset new link message text doesn't match");

            Log.Info("Step 9: Click on 'Request New Link' button & verify 'Check Your Email' page gets open");
            requestNewLink.ClickOnRequestNewLinkButton();
            const string expectedCheckYourEmailHeaderText = "Check Your Email";
            var actualCheckYourEmailHeaderText = checkYourEmailPage.GetForgotPasswordCheckYourEmailHeaderText();
            Assert.AreEqual(expectedCheckYourEmailHeaderText, actualCheckYourEmailHeaderText,
                "The 'Check Your Email' header text doesn't match");

            const string expectedCheckYourEmailMessage =
                "Keep an eye out for an email with instructions for next steps.";
            var actualCheckYourEmailMessage = checkYourEmailPage.GetForgotPasswordCheckYourEmailMessage();
            Assert.AreEqual(expectedCheckYourEmailMessage, actualCheckYourEmailMessage,
                "The 'Check Your Email' message text doesn't match");

            Log.Info("Step 10:  Open 'YopMail', Open your 'Reset Password', click on 'Reset Password' link");
            new WaitHelpers(Driver).HardWait(10000);
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(UserLogin.Email);
            emailListingGrid.OpenEmail("Reset Password");
            emailListingGrid.ClickOnButtonOrLink(resetPassword);

            Log.Info(
                "Step 11: Fill the new password & click on 'Submit' button and verify confirmation page gets open");
            createNewPasswordPage.EnterNewPasswordAndConfirmPassword(newPassword);
            createNewPasswordPage.ClickOnCreateNewPasswordSubmitButton();
            var actualConformationTitle = confirmPage.GetNewConfirmationTitle();
            const string expectedConformationTitle = "Woohoo!";
            Assert.AreEqual(expectedConformationTitle, actualConformationTitle,
                " The confirmation title doesn't match");

        }
    }
}
