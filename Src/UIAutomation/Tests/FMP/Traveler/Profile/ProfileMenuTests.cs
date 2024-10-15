using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class ProfileMenuTests : FmpBaseTest
    {

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyUserProfileMenuIsOpenedSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profilePage.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on user profile");
            headerHomePagePo.ClickOnProfileIcon();

            Log.Info("Step 5: Verify user profile menu is opened");
            var expectedUsername = FusionMarketPlaceLoginCredentials.Name.Split(" ").First();
            var actualUsername = profilePage.GetUserFirstNameFromProfileMenu();
            Assert.AreEqual(expectedUsername, actualUsername, "Username is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyProfileDetailPageIsOpenedSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profilePage.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on user profile");
            headerHomePagePo.ClickOnProfileIcon();

            Log.Info("Step 5: Click on 'profile' button");
            profilePage.ClickOnProfileMenuItem();

            Log.Info("Step 6: Verify Profile detail page is open");
            var expectedUsername = FusionMarketPlaceLoginCredentials.Name;
            var actualUsername = profileDetail.GetProfileName();
            Assert.AreEqual(expectedUsername, actualUsername, "Username is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyUserProfileMenuCloseButtonWorkSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profilePage.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on user profile");
            headerHomePagePo.ClickOnProfileIcon();

            Log.Info("Step 5: Click on 'Close' button");
            profilePage.ClickOnCloseButton();

            Log.Info("Step 6: Verify Profile menu close successfully");
            Assert.IsFalse(profilePage.IsLogoutButtonPresent(), "Profile Menu isn't closed");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyRecentlyViewJobsShownOnProfileMenu()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var jobDetail = new JobsDetailsPo(Driver);
            var searchPo = new PageObjects.FMP.Jobs.SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            headerHomePagePo.ClickOnLogInButton();
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profilePage.WaitUntilFmpTextLoadingIndicatorInvisible();
             
            Log.Info("Step 3: Click on First job card and Verify that Recently viewed job is displayed  on menu");
            searchPo.NavigateToPage();
            searchPo.WaitUntilFirstJobCardTitleGetDisplayed();
            searchPo.ClickOnJobCard();
            var expectedRecentlyViewedJob = jobDetail.GetJobTitle() + " - " + jobDetail.GetJobLocation();
            headerHomePagePo.ClickOnProfileIcon();
            profilePage.ClickOnRecentlyViewItem();
            var actualRecentlyViewedJob = profilePage.GetRecentlyViewedJobText().ToLower();
            Assert.IsTrue(expectedRecentlyViewedJob.ToLower().Equals(actualRecentlyViewedJob) || expectedRecentlyViewedJob.ToLower().EndsWith(" (Filled)"));

            Log.Info("Step 4: Click on 'Clear all' button and Verify that recent viewed job gets cleared.");
            profilePage.ClickOnClearAllRecentlyViewedJob();
            Assert.IsFalse(profilePage.IsRecentlyViewedJobTextIsPresent(), "Recently viewed jobs is still present.");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyUserProfileMenuLogOutButtonWorkSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on user profile");
            headerHomePagePo.ClickOnProfileIcon();

            Log.Info("Step 5: Click on 'Log out' button");
            profilePage.ClickOnLogOutButton();
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 6: Verify User is Logged out successfully");
            Assert.IsFalse(profilePage.IsLogoutButtonPresent(), "User isn't logged out");
        }
    }
}
