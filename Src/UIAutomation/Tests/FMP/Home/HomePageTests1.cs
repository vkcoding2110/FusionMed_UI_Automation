using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMP.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Home
{
    [TestClass]
    [TestCategory("Home"), TestCategory("FMP")]
    public class HomePageTests1 : FmpBaseTest
    {
        private readonly string ExpectedFusionMarketPlaceUrl = FusionMarketPlaceUrl.Insert(8, "accounts-");

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyBlogsWorkSuccessfully()
        {
            var homePage = new HomePagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on First Blog and verify title, header & text");
            const int blogFirst = 1;
            var expectedFirstBlogTitleAndHeaderText = homePage.GetBlogTitle(blogFirst);
            homePage.ClickOnNthBlogCard(blogFirst);
            var actualFirstBlogHeaderText = homePage.GetBlogHeaderText();
            var actualFirstBlogTitle = Driver.GetPageTitle();
            Assert.AreEqual(expectedFirstBlogTitleAndHeaderText, actualFirstBlogHeaderText,
                "First Blog header text is not matched");
            Assert.AreEqual(expectedFirstBlogTitleAndHeaderText.RemoveWhitespace(), actualFirstBlogTitle.RemoveWhitespace(),
                "First Blog title is not matched");

            Log.Info("Step 3: Click on second Blog and verify title & header text");
            Driver.Back();
            homePage.WaitUntilFmpPageLoadingIndicatorInvisible();
            const int blogSecond = 2;
            var expectedSecondBlogTitleAndHeaderText = homePage.GetBlogTitle(blogSecond);
            homePage.ClickOnNthBlogCard(blogSecond);
            var actualSecondBlogHeaderText = homePage.GetBlogHeaderText();
            var actualSecondBlogTitle = Driver.GetPageTitle();
            Assert.AreEqual(expectedSecondBlogTitleAndHeaderText, actualSecondBlogHeaderText, "Second Blog header text is not matched");
            Assert.AreEqual(expectedSecondBlogTitleAndHeaderText.RemoveWhitespace(), actualSecondBlogTitle.RemoveWhitespace(), "second Blog title is not matched");

            Log.Info("Step 4: Click on third Blog and verify title & header text");
            Driver.Back();
            homePage.WaitUntilFmpPageLoadingIndicatorInvisible();
            const int blogThird = 3;
            var expectedThirdBlogTitleAndHeaderText = homePage.GetBlogTitle(blogThird);
            homePage.ClickOnNthBlogCard(blogThird);
            var actualThirdBlogHeaderText = homePage.GetBlogHeaderText();
            var actualThirdBlogTitle = Driver.GetPageTitle();
            Assert.AreEqual(expectedThirdBlogTitleAndHeaderText, actualThirdBlogHeaderText, "Third Blog header text is not matched");
            Assert.AreEqual(expectedThirdBlogTitleAndHeaderText.RemoveWhitespace(), actualThirdBlogTitle.RemoveWhitespace(), "Third Blog title is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyBenefitsGroupOptionsCardsWorkSuccessfully()
        {
            const string travelerText = "Traveler";
            const string agencyText = "Agency";
            const string recruiterText = "Recruiter";

            var homePage = new HomePagePo(Driver);
            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            homePage.WaitUntilFirstJobCardTitleGetDisplayed();

            Log.Info("Step 2: Verify 'Traveler' Cards");
            var travelerDetails = GetCardListByUserType(travelerText);
            Assert.IsTrue(homePage.IsViewJobsButtonDisplayed(), "View jobs button is not displayed");
            for (var i = 0; i < travelerDetails.Count; i++)
            {
                var expectedCardTitle = travelerDetails[i].CardTitle;
                var expectedCardDetails = travelerDetails[i].CardDescription;
                if (PlatformName != PlatformName.Web)
                {
                    new WaitHelpers(Driver).HardWait(7000);
                }
                var cardTitle = homePage.GetCardTitleText(i);
                var cardDetails = homePage.GetCardDescriptionText(i);
                Assert.AreEqual(expectedCardTitle, cardTitle, "Card 'Title' not matched");
                Assert.AreEqual(expectedCardDetails, cardDetails, "Card 'Details' not matched");
            }
            var expectedSearchUrl = FusionMarketPlaceUrl + "search/";
            homePage.ClickOnViewJobsButton();
            homePage.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(Driver.IsUrlMatched(expectedSearchUrl), "Search page url is not matched");

            Log.Info("Step 3: Click on 'Agency' button and  Verify 'Agency' data");
            Driver.Back();
            if (PlatformName != PlatformName.Web)
            {
                homePage.ClickOnJobApplicationNextButtonDevice();
                var actualBenefitsGroupOptionsText = homePage.GetBenefitsGroupOptionsDevice();
                Assert.AreEqual(agencyText, actualBenefitsGroupOptionsText, "Benefits Group Options text is not matched");
            }
            else
            {
                homePage.ClickOnBenefitsGroupsOptionsTitleButton(agencyText);
            }
            Assert.IsTrue(homePage.IsPartnerWithUsButtonDisplayed(), "Partner with us button is not present");
            var agencyDetails = GetCardListByUserType(agencyText);
            for (var i = 0; i < agencyDetails.Count; i++)
            {
                var expectedCardTitle = agencyDetails[i].CardTitle;
                var expectedCardDetails = agencyDetails[i].CardDescription;
                if (PlatformName != PlatformName.Web)
                {
                    new WaitHelpers(Driver).HardWait(8000);
                }
                var cardTitle = homePage.GetCardTitleText(i);
                var cardDetails = homePage.GetCardDescriptionText(i);
                Assert.AreEqual(expectedCardTitle, cardTitle, "Card 'Title' not matched");
                Assert.AreEqual(expectedCardDetails, cardDetails, "Card 'Details' not matched");
            }
            var expectedAgencyUrl = FusionMarketPlaceUrl + "agencies/";
            homePage.ClickOnPartnerWithUsButton();
            homePage.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(Driver.IsUrlMatched(expectedAgencyUrl), "Search page url is not matched");

            Log.Info("Step 4: Click on 'Recruiter' button and  Verify 'Recruiter' data");
            Driver.Back();
            if (PlatformName != PlatformName.Web)
            {
                homePage.ClickOnJobApplicationNextButtonDevice();
                homePage.ClickOnJobApplicationNextButtonDevice();
                var actualBenefitsGroupOptionsText = homePage.GetBenefitsGroupOptionsDevice();
                Assert.AreEqual(recruiterText, actualBenefitsGroupOptionsText, "Benefits Group Options text is not matched");
            }
            else
            {
                homePage.ClickOnBenefitsGroupsOptionsTitleButton(recruiterText);
            }
            var recruiterDetails = GetCardListByUserType(recruiterText);
            for (var i = 0; i < recruiterDetails.Count; i++)
            {
                var expectedCardTitle = recruiterDetails[i].CardTitle;
                var expectedCardDetails = recruiterDetails[i].CardDescription;
                if (PlatformName != PlatformName.Web)
                {
                    new WaitHelpers(Driver).HardWait(8000);
                }
                var cardTitle = homePage.GetCardTitleText(i);
                var cardDetails = homePage.GetCardDescriptionText(i);
                Assert.AreEqual(expectedCardTitle, cardTitle, "Card 'Title' not matched");
                Assert.AreEqual(expectedCardDetails, cardDetails, "Card 'Details' not matched");
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HappierTraveler_VerifyThatCreateATravelerProfileButtonWorksSuccessfully()
        {
            var homePage = new HomePagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Create a Traveler profile' button and verify 'Sign Up' page open Successfully");
            homePage.ClickCreateATravelerProfileButton();
            var actualUrl = Driver.GetCurrentUrl();
            Assert.IsTrue(actualUrl.StartsWith(ExpectedFusionMarketPlaceUrl), "Card 'URL' not matched");
        }
    }
}
