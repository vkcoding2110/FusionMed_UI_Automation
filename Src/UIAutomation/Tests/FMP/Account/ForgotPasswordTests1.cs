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
    public class ForgotPasswordTests1 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByType("ForgotPasswordTests");

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatForgotPasswordWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var forgotPasswordPage = new ForgetPasswordPo(Driver);
            var emailListGrid = new EmailListingGridPo(Driver);
            var email = new EmailPo(Driver);
            var createNewPasswordPage = new CreateNewPasswordPo(Driver);
            var confirmPage = new ConfirmPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email} & Click on 'Continue' button");
            fmpLogin.EnterEmailIdAndContinue(UserLogin);

            Log.Info("Step 4: Click on 'Forgot Password' link");
            fmpLogin.ClickOnForgetPasswordLink();

            Log.Info("Step 5: Click on 'Find Account' button");
            forgotPasswordPage.ClickOnFindAccountButton();

            Log.Info("Step 6: Open 'YopMail', Open your 'Reset Password', click on 'Reset Password' link");
            new WaitHelpers(Driver).HardWait(10000);
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(UserLogin.Email);

            emailListGrid.OpenEmail("Reset Password");
            var actualSenderEmail = emailListGrid.GetSenderEmailText();
            Assert.AreEqual(GlobalConstants.SenderEmailText, actualSenderEmail, "The Sender email doesn't match");

            const string resetPassword = "Reset Password";
            var resetPasswordHref = emailListGrid.GetHRefOfButton(resetPassword);
            Driver.NavigateTo(resetPasswordHref);

            Log.Info("Step 7: Click on 'Close' icon & verify 'Login' page gets open");
            confirmPage.ClickOnCreateNewPasswordConfirmationCloseIcon();
            const string expectedLoginPageTitle = "login";
            var actualLoginPageTitle = fmpLogin.GetLoginPageHeader();
            Assert.AreEqual(expectedLoginPageTitle, actualLoginPageTitle.ToLower(),
                "The 'Login' page header text is not matched");
            Driver.NavigateTo(resetPasswordHref);

            Log.Info("Step 8: Verify 'Create New Password' page is opened");
            const string expectedCreateNewPasswordHeaderText = "Create New Password";
            var actualCreateNewPasswordHeaderText = createNewPasswordPage.GetCreateNewPasswordHeaderText();
            Assert.AreEqual(expectedCreateNewPasswordHeaderText, actualCreateNewPasswordHeaderText,
                "The 'Create New Password' header text is not matched");
           
            Log.Info("Step 9: Fill the new confirm password & verify Cheer text message is displayed");
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            var newPassword = "Test@" + randomNumber.Remove(5);
            createNewPasswordPage.EnterNewPasswordAndConfirmPassword(newPassword);
            Assert.IsTrue(createNewPasswordPage.IsConfirmPasswordCheerTextDisplayed(),"The 'Confirm Password' cheer text isn't displayed");

            Log.Info("Step 11: Click on 'Submit' button and verify 'created a brand spankin' new password!' message is displayed");
            createNewPasswordPage.ClickOnCreateNewPasswordSubmitButton();
            var actualConformationTitle = confirmPage.GetNewConfirmationTitle();
            var expectedConformationTitle = "Woohoo!";
            Assert.AreEqual(expectedConformationTitle, actualConformationTitle,
                " The confirmation title doesn't match");

            const string expectedConfirmationText = "You have successfully created a brand spankin' new password!";
            var actualConfirmationText = confirmPage.GetNewConfirmationText();
            Assert.AreEqual(expectedConfirmationText, actualConfirmationText, "The confirmation text doesn't match");

            Log.Info("Step 12: Click on 'Login' button");
            confirmPage.ClickOnLogInButton();

            Log.Info("Step 13: Login to application with 'Email' & new 'Password' & verify ");
            UserLogin.Password = newPassword;
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            var actualProfileName = headerHomePagePo.GetUserName().RemoveWhitespace();
            Assert.AreEqual(UserLogin.Name.RemoveWhitespace(), actualProfileName, "The profile name is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatClickingOnForgotPasswordLinkWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var forgotPasswordPage = new ForgetPasswordPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info(
                $"Step 3: Login to application with credentials - Email:{UserLogin.Email} & Click on 'Continue' button");
            fmpLogin.EnterEmailIdAndContinue(UserLogin);

            Log.Info("Step 4: Click on 'Forgot Password' link & verify 'Forgot Password' page gets open");
            fmpLogin.ClickOnForgetPasswordLink();
            const string expectedForgotPasswordHeader = "Forgot Password";
            var actualForgotPasswordHeader = forgotPasswordPage.GetForgetPasswordPageHeader();
            Assert.AreEqual(expectedForgotPasswordHeader, actualForgotPasswordHeader,
                "The 'Forgot Password' header is not matched");

            const string expectedForgotPasswordText = "Please enter your email address associated with your account.";
            var actualForgotPasswordText = forgotPasswordPage.GetForgotPasswordText();
            Assert.AreEqual(expectedForgotPasswordText, actualForgotPasswordText,
                "The 'Forgot Password' text is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatFindAccountButtonWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var forgotPasswordPage = new ForgetPasswordPo(Driver);
            var checkYourEmailPage = new CheckYourEmailPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info(
                $"Step 3: Login to application with credentials - Email:{UserLogin.Email} & Click on 'Continue' button");
            fmpLogin.EnterEmailIdAndContinue(UserLogin);

            Log.Info("Step 4: Click on 'Forgot Password' link");
            fmpLogin.ClickOnForgetPasswordLink();

            Log.Info("Step 5: Click on 'Find Account' button & verify 'Check Your Email' page gets open");
            forgotPasswordPage.ClickOnFindAccountButton();
            const string expectedCheckYourEmailHeaderText = "Check Your Email";
            var actualCheckYourEmailHeaderText = checkYourEmailPage.GetForgotPasswordCheckYourEmailHeaderText();
            Assert.AreEqual(expectedCheckYourEmailHeaderText, actualCheckYourEmailHeaderText,
                "The 'Check Your Email' header text doesn't match");

            const string expectedCheckYourEmailMessage =
                "Keep an eye out for an email with instructions for next steps.";
            var actualCheckYourEmailMessage = checkYourEmailPage.GetForgotPasswordCheckYourEmailMessage();
            Assert.AreEqual(expectedCheckYourEmailMessage, actualCheckYourEmailMessage,
                "The 'Check Your Email' message text doesn't match");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatForgotPasswordCancelButtonAndCloseIconWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var forgotPasswordPage = new ForgetPasswordPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info(
                $"Step 3: Login to application with credentials - Email:{UserLogin.Email} & Click on 'Continue' button");
            fmpLogin.EnterEmailIdAndContinue(UserLogin);

            Log.Info("Step 4: Click on 'Forgot Password' link");
            fmpLogin.ClickOnForgetPasswordLink();

            Log.Info("Step 5: Click on forgot password page 'Cancel' button & verify 'Login' page gets open");
            forgotPasswordPage.ClickOnForgotPasswordCancelButton();
            const string expectedLoginPageTitle = "login";
            var actualLoginPageTitle = fmpLogin.GetLoginPageHeader().ToLower();
            Assert.AreEqual(expectedLoginPageTitle, actualLoginPageTitle,
                "The 'Login' page header text is not matched");

            Log.Info(
                $"Step 6: Login to application with credentials - Email:{UserLogin.Email} & Click on 'Continue' button");
            fmpLogin.EnterEmailIdAndContinue(UserLogin);

            Log.Info("Step 7: Click on 'Forgot Password' link");
            fmpLogin.ClickOnForgetPasswordLink();

            Log.Info("Step 8: Click on forgot password page 'Close' icon & verify 'Login' page gets open");
            forgotPasswordPage.ClickOnForgotPasswordCloseIcon();
            const string expectedTitleOfLoginPage = "login";
            var actualTitleOfLoginPage = fmpLogin.GetLoginPageHeader().ToLower();
            Assert.AreEqual(expectedTitleOfLoginPage, actualTitleOfLoginPage,
                "The 'Login' page header text is not matched");

        }
    }
}
