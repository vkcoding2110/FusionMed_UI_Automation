using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler
{
    [TestClass]
    [TestCategory("Travelers"), TestCategory("FMP")]
    public class TravelersTests : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatAboutUsPageButtonsAndLinksWorksSuccessfully()
        {

            var fmpHeader = new HeaderPo(Driver);
            var travelers = new TravelersBrandingPO(Driver);
            var signUpPage = new SignUpPo(Driver);
            var searchJobs = new PageObjects.FMP.Jobs.SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Travelers' link");
            fmpHeader.ClickOnTravelersLink();

            Log.Info("Step 3: Click on 'Get Started' button and verify 'Sign Up' page gets open");
            travelers.ClickOnGetStartedButton();
            fmpHeader.WaitUntilFmpPageLoadingIndicatorInvisible();
            const string expectedTitle = "sign up";
            var actualTitle = signUpPage.GetSignUpPageHeader().ToLower();
            Assert.AreEqual(expectedTitle, actualTitle, "Sign up Title is not matched");

            Log.Info("Step 4: Click on 'Start Your Job Search' and verify 'Jobs' page gets open");
            Driver.Back();
            travelers.ClickOnStartYourJobSearchButton();
            fmpHeader.WaitUntilFmpPageLoadingIndicatorInvisible();
            searchJobs.WaitUntilJobCardVisible();
            var expectedJobsUrl = FusionMarketPlaceUrl + "search/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedJobsUrl), $"{expectedJobsUrl} Jobs page url is not matched");

            Log.Info("Step 5: Click on 'Let's Do It!' Button and verify 'Sign Up' page gets open");
            Driver.Back();
            travelers.ClickOnLetsDoItButton();
            fmpHeader.WaitUntilFmpPageLoadingIndicatorInvisible();
            const string expectedSignUpTitle = "sign up";
            var actualSignUpTitle = signUpPage.GetSignUpPageHeader().ToLower();
            Assert.AreEqual(expectedSignUpTitle, actualSignUpTitle, "Sign up Title is not matched");
        }
    }
}
