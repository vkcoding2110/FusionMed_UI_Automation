using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Account
{
    [TestClass]
    [TestCategory("Login"), TestCategory("FMP")]
    public class LoginTests1 : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyLogInPageOpenedSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 2: Verify that Login page is Opened SuccessFully");
            const string expectedTitle = "LOGIN";
            var actualTitle = fmpLogin.GetLoginPageHeader();
            Assert.AreEqual(expectedTitle.ToLowerInvariant(), actualTitle.ToLowerInvariant(), "Login page title is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        [TestCategory("Smoke")]
        public void VerifyUserIsLoggedInSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var homePage=new HomePagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            homePage.WaitUntilFmpTextLoadingIndicatorInvisible();

            var actualUserName = headerHomePagePo.GetUserName().RemoveWhitespace();
            Assert.AreEqual(FusionMarketPlaceLoginCredentials.Name.RemoveWhitespace(), actualUserName, "Username is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyCloseButtonWorkSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Click on 'Close' button");
            fmpLogin.ClickOnCloseButton();
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4:Verify that home page opened");
            var expectedUrl = FusionMarketPlaceUrl;
            Assert.IsTrue(Driver.IsUrlMatched(expectedUrl), $"{expectedUrl} URL is not matched");

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifySignUpPageOpenSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var signUpPage = new SignUpPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Click on Sign up link");
            fmpLogin.ClickOnSignUpLink();
            const string expectedTitle = "sign up";
            var actualTitle = signUpPage.GetSignUpPageHeader().ToLower();
            Assert.AreEqual(expectedTitle, actualTitle, "Sign up Title is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyForgetPasswordPageOpenSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var forgotPasswordPage = new ForgetPasswordPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info("Step 3: Enter email address in 'Email' field");
            fmpLogin.EnterEmailIdAndContinue(FusionMarketPlaceLoginCredentials);

            Log.Info("Step 4: Click on 'Forget password' link");
            fmpLogin.ClickOnForgetPasswordLink();

            Log.Info("Step 5: Verify that forget password page open successfully");
            const string expectedTitle = "Forgot Password";
            var actualTitle = forgotPasswordPage.GetForgetPasswordPageHeader();
            Assert.AreEqual(expectedTitle, actualTitle, "Forget password page header not matched");
        }
    }
}

