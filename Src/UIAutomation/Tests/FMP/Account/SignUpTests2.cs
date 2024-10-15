using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.YopMail;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Account
{

    [TestClass]
    [TestCategory("SignUp"), TestCategory("FMP")]
    public class SignUpTests2 : FmpBaseTest
    {

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyPasswordAndConfirmPasswordShowsCheerTextMessageOnPasswordPage()
        {
            var signUpPage = new AboutMePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
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

            Log.Info("step 5: fill the password & verify Cheer text message is displayed");
            passwordPage.EnterPassword(addAboutMeSignUpData);
            Assert.IsTrue(passwordPage.IsPasswordCheerTextDisplayed(), "The 'Password' cheer text isn't displayed");

            Log.Info("step 6: fill the  confirm password & verify Cheer text message is displayed");
            passwordPage.EnterConfirmPassword(addAboutMeSignUpData);
            Assert.IsTrue(passwordPage.IsConfirmPasswordCheerTextDisplayed(), "The 'Confirm Password' cheer text isn't displayed");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyBackButtonOnPasswordPageRedirectsToTheAboutMePage()
        {
     
            var signUpPage = new AboutMePo(Driver);
            var passwordPage = new PasswordPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Click on 'Sign Up' link text");
            fmpLogin.ClickOnSignUpLink();

            Log.Info("Step 4: Fill data for 'ABOUT ME' in 'Sign Up' form & click on 'Continue' button and verify user is navigated to the 'Password' data fields");
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            signUpPage.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);
            Assert.IsTrue(passwordPage.IsFilledProgressBarDisplayed(), "Filled Progressbar is not displayed");

            Log.Info("Step 5: Click on 'Back' button & verify 'About Me' page gets open");
            passwordPage.ClickOnBackButton();
            Assert.IsTrue(signUpPage.IsUnFilledProgressBarDisplayed(), "The filled progress bar is displayed");

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyAboutMeCloseIconAndCancelButtonWorksSuccessfully()
        {
            var signUpPage = new AboutMePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Click on 'Sign Up' link text");
            fmpLogin.ClickOnSignUpLink();

            Log.Info("Step 4: Click on 'Close' Icon & verify 'Login' page gets open ");
            signUpPage.ClickOnCloseIcon();
            const string expectedLogInPageHeaderText = "LOGIN";
            var actualLogInPageHeaderText = fmpLogin.GetLoginPageHeader();
            Assert.AreEqual(expectedLogInPageHeaderText.ToLowerInvariant(), actualLogInPageHeaderText.ToLowerInvariant(), "The 'Login' page header text doesn't match");

            Log.Info("Step 5: Click on 'Sign Up' link text");
            fmpLogin.ClickOnSignUpLink();

            Log.Info("Step 6: Click on 'Cancel' button & verify 'Login' page gets open ");
            signUpPage.ClickOnCancelButton();
            const string expectedHeaderTextOfLoginPage = "LOGIN";
            var actualHeaderTextOfLoginPage = fmpLogin.GetLoginPageHeader();
            Assert.AreEqual(expectedHeaderTextOfLoginPage.ToLowerInvariant(), actualHeaderTextOfLoginPage.ToLowerInvariant(), "The 'Login' page header text doesn't match");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyPasswordPageCancelButtonWorksSuccessfully()
        {

            var signUpPage = new AboutMePo(Driver);
            var passwordPage = new PasswordPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Click on 'Sign Up' link text and verify sign up page gets open");
            fmpLogin.ClickOnSignUpLink();

            Log.Info("Step 4: Fill data for 'ABOUT ME' in 'Sign Up' form & click on 'Continue' button");
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            signUpPage.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);

            Log.Info("Step 5: Click on 'Cancel' button & verify 'About Me' page gets open");
            passwordPage.ClickOnCancelButton();
            Assert.IsTrue(signUpPage.IsUnFilledProgressBarDisplayed(), "The filled progress bar is displayed");

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyPasswordPageCloseIconWorksSuccessfully()
        {

            var signUpPage = new AboutMePo(Driver);
            var passwordPage = new PasswordPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Click on 'Sign Up' link text and verify sign up page gets open");
            fmpLogin.ClickOnSignUpLink();

            Log.Info("Step 4: Fill data for 'ABOUT ME' in 'Sign Up' form & click on 'Continue' button");
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            signUpPage.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);

            Log.Info("Step 5: Click on 'Close' Icon & verify 'Login' page gets open");
            passwordPage.ClickOnCloseIcon();
            const string expectedLogInPageHeaderText = "LOGIN";
            var actualLogInPageHeaderText = fmpLogin.GetLoginPageHeader();
            Assert.AreEqual(expectedLogInPageHeaderText.ToLowerInvariant(), actualLogInPageHeaderText.ToLowerInvariant(), "The 'Login' page header text doesn't match");
        }
    }
}
