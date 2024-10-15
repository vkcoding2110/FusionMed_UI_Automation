using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Account.NativeApp;
using UIAutomation.PageObjects.FMP.Home.NativeApp;
using UIAutomation.PageObjects.FMP.NativeApp.More;

namespace UIAutomation.Tests.FMP.Account.NativeApp
{
    [TestClass]
    [TestCategory("Login"), TestCategory("NativeAppAndroid")]
    public class LoginTests : FmpBaseTest
    {
        [TestMethod]
        public void VerifyLogInWorksSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);

            Log.Info("Step 2: Verify Profile button is present");
            Assert.IsTrue(fmpLogin.IsProfileTextDisplayed(), "Home page is not opened!");
        }

        [TestMethod]
        public void VerifyLogoutWorksSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var moreMenu = new MoreMenuPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);

            Log.Info("Step 2: Tap on 'More' menu & select 'Logout' option");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnLogoutOption();

            Log.Info("Step 3: Verify Logged out successfully message is display");
            Assert.IsTrue(moreMenu.IsLoggedOutSuccessTextDisplayed(), "'Logged out successfully' message is not display");
        }
    }
}
