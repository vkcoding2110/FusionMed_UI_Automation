using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Agencies;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Header
{
    [TestClass]
    [TestCategory("Header"), TestCategory("FMP")]
    public class HeaderTests1 : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyBrowseAllMenuOpenedSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button & verify 'Explore' popup gets open");
            fmpHeader.ClickOnBrowseAllButton();
            Assert.IsTrue(exploreMenu.IsExploreMenuOpened(), "Explore menu is not opened");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatTravelersLinkWorkSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var travelers = new TravelersBrandingPO(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Travelers' link & verify 'Travelers' page gets open");
            fmpHeader.ClickOnTravelersLink();
            fmpHeader.WaitUntilFmpPageLoadingIndicatorInvisible();
            const string expectedTravelersPageHeaderText = "HAPPIER TRAVELERS";
            var actualTravelersPageHeaderText = travelers.GetTravelersPageHeaderText();
            Assert.AreEqual(expectedTravelersPageHeaderText.ToLowerInvariant(), actualTravelersPageHeaderText.ToLowerInvariant(), "Travelers page header text is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatBlogLinkWorkSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Get 'Blog' page Href");
            const string expectedBlogHref = "https://blog.fusionmarketplace.com/";
            var actualBlogPageHref = fmpHeader.GetBlogHref();
            Assert.AreEqual(expectedBlogHref, actualBlogPageHref, "Blog page url is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyLogInButtonWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var fmpHeader = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Click on 'Log In' button & Verify that Login page is opened SuccessFully");
            fmpHeader.ClickOnLogInButton();
            const string expectedTitle = "LOGIN";
            var actualTitle = fmpLogin.GetLoginPageHeader();
            Assert.AreEqual(expectedTitle.ToLowerInvariant(), actualTitle.ToLowerInvariant(), "Login page title is not matched");
        }

        [TestMethod]
        public void VerifyJoinUsButtonWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var signUpPage = new SignUpPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Click on 'Join Us' button & Verify that Login page is opened SuccessFully");
            fmpHeader.ClickOnJoinUsButton();
            const string expectedTitle = "sign up";
            var actualTitle = signUpPage.GetSignUpPageHeader().ToLower();
            Assert.AreEqual(expectedTitle, actualTitle, "Sign up Title is not matched"); Assert.AreEqual(expectedTitle.ToLowerInvariant(), actualTitle.ToLowerInvariant(), "Login page title is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatAgenciesLinkWorkSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var agencies = new AgenciesPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Agencies' link & verify 'Agencies' page gets open");
            fmpHeader.ClickOnAgenciesLink();
            fmpHeader.WaitUntilFmpPageLoadingIndicatorInvisible();
            const string expectedTravelersPageHeaderText = "PARTNER WITH FUSION MARKETPLACE";
            var actualTravelersPageHeaderText = agencies.GetAgenciesPageHeaderText();
            Assert.AreEqual(expectedTravelersPageHeaderText.ToLowerInvariant(), actualTravelersPageHeaderText.ToLowerInvariant(), "Agencies page header text is not matched");
        }
    }
}
