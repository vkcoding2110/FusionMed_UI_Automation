using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.BrowseAll.Agencies;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Agencies.Recruiters
{
    [TestClass]
    [TestCategory("Recruiter"), TestCategory("FMP")]
    public class RecruiterProfileTests : FmpBaseTest
    {
        private const string ExploreMenuAgenciesButton = "Agencies";
        private const string AgencyName = "Fusion Medical Staffing";
        private const string RecruiterName = "Automation Recruiter1Star";
        private const string MultipleDepartmentRecruiter = "Automation Recruiter2Star";
        private const string ExpectedRateAndReviewHeaderText = "RATE & REVIEW";

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatRecruitersCardsIsDisplayedInAgencyDetailPage()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agency = new AgencyDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 3: Click on 'Agencies' menu item & Click on 'Fusion medical staffing' agency from the list and Verify Recruiter card displayed");
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
            var agencyName = "Fusion Medical Staffing";
            exploreMenu.ClickOnAgencyMenuItem(agencyName);
            Assert.IsTrue(agency.IsRecruiterCardDisplayed(), "Recruiter cards is not displayed");
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void VerifyRecruiterWithSingleDepartmentDetailsAreCorrect()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agency = new AgencyDetailPo(Driver);
            var recruitersPo = new RecruiterListingPo(Driver);
            var recruiterDetail = new RecruiterDetailsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 3: Click on 'Agencies' menu item & Click on 'Fusion medical staffing' agency from the list and Verify each recruiter card have same agency");
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
            exploreMenu.ClickOnAgencyMenuItem(AgencyName);
            agency.ClickOnViewAllRecruiterLink();
            var expectedAgencyName = recruitersPo.GetRecruiterFilterTagName();
            Assert.AreEqual(expectedAgencyName, AgencyName, "Recruiter agency name is not matched");
            var recruiterList = recruitersPo.GetAllAgencyNamesFromRecruiterCards();
            for (var i = 1; i < recruiterList.Count; i++)
            {
                Assert.AreEqual(recruiterList[i], AgencyName, $"Recruiter agency name doesn't have {AgencyName}");
            }
            Log.Info("Step 4: Click on Recruiter card and Verify Recruiter details are matched & Email button is enabled");
            recruitersPo.ClickOnRecruiterCard(RecruiterName);
            var expectedRecruiterDetails = GetRecruitersByName(RecruiterName);
            var expectedRecruiterAgencyDetails = GetAgencyByName(AgencyName);
            var recruiterDetails = recruiterDetail.GetRecruiterDetail();
            var expectedMetaTitle = recruiterDetails.RecruiterName + " | " + recruiterDetails.RecruiterAgency.Name + " Recruiter";
            var actualMetaTitle = Driver.GetPageTitle();
            var expectedRecruiterProfileImageUrl = FusionMarketPlaceUrl.Replace("https://", "https://api-");
            Assert.AreEqual(expectedMetaTitle.ToLowerInvariant(), actualMetaTitle.ToLowerInvariant(), "Meta title is not matched");
            Assert.AreEqual(expectedRecruiterDetails.RecruiterName.ToLowerInvariant(), recruiterDetails.RecruiterName, "Recruiter's name is not matched");
            Assert.AreEqual(expectedRecruiterDetails.Specialty.ToString(), recruiterDetails.Specialty.ToString(), "Recruiter's specialty is not matched");
            Assert.AreEqual(expectedRecruiterDetails.AboutMe.RemoveWhitespace().ToLowerInvariant(), recruiterDetails.AboutMe.RemoveWhitespace().ToLowerInvariant(), "Recruiter's About Me is not matched");
            Assert.IsTrue(recruiterDetail.GetRecruiterProfilePhotoUrl().StartsWith(expectedRecruiterProfileImageUrl), "Recruiter's Profile Photo is not displayed");

            Log.Info("Step 5: Click on 'Review This Recruiter' Button and verify 'Rate & Review' page gets open");
            recruiterDetail.ClickOnReviewThisRecruiterButton();
            var actualRateAndReviewHeaderText = recruiterDetail.GetRateAndReviewHeaderText();
            Assert.AreEqual(ExpectedRateAndReviewHeaderText.ToLowerInvariant(), actualRateAndReviewHeaderText.ToLowerInvariant(), "'Rate And Review' page is not opened");

            Log.Info("Step 6: Get 'View benefits and agency details' link and verify 'Agency' detail page gets open");
            Driver.Back();
            recruiterDetail.WaitUntilFmpPageLoadingIndicatorInvisible();
            var expectedAgencyUrl = FusionMarketPlaceUrl + "agencies/" + expectedRecruiterAgencyDetails.AliasName + "/";
            Assert.AreEqual(expectedAgencyUrl, recruiterDetail.GetLearnMoreAboutAgencyHref(), "'Agency' page is not opened");

            Log.Info("Step 7: Get 'Browse Jobs' link and verify 'Jobs' page gets open");
            recruiterDetail.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.AreEqual(FusionMarketPlaceUrl + "search/", recruiterDetail.GetBrowseJobsHref(), "Jobs page is not opened");

            Log.Info("Step 8: Click on 'Review Recruiter' link and verify 'Rate & Review' page gets open");
            recruiterDetail.ClickOnReviewRecruiterLink();
            Assert.AreEqual(ExpectedRateAndReviewHeaderText, actualRateAndReviewHeaderText, "'Rate And Review' page is not opened");

            Log.Info("Step 9: Verify that star rating is not show overall under department");
            Assert.IsFalse(recruiterDetail.IsStarRatingOfReviewSectionDisplayed(), "Review Section Of 'Star Rating' is displayed");

            Log.Info("Step 10: Click on 'Review' link under department and verify it's navigate to user's review section of profile'");
            Driver.Back();
            recruiterDetail.WaitUntilFmpPageLoadingIndicatorInvisible();
            recruiterDetail.ClickOnHeaderRecruiterReview();
            Assert.IsTrue(recruiterDetail.IsReviewSectionOfProfileDisplayed(), "'Review' Section Of Profile is not displayed");
            Assert.IsTrue(recruiterDetail.IsReviewTextDisplayed(), "Review text Section not displayed");
        }

        [TestMethod]
        public void VerifyRecruiterWithMultipleDepartmentDetailsAreCorrect()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agency = new AgencyDetailPo(Driver);
            var recruitersPo = new RecruiterListingPo(Driver);
            var recruiterDetail = new RecruiterDetailsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 3: Click on 'Agencies' menu item , Click on 'Fusion medical staffing' agency from the list and click on 'Recruiter' link");
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
            exploreMenu.ClickOnAgencyMenuItem(AgencyName);
            agency.ClickOnViewAllRecruiterLink();

            Log.Info("Step 4: Click on Recruiter card and Verify Recruiter Department");
            recruitersPo.ClickOnRecruiterCard(MultipleDepartmentRecruiter);
            var expectedRecruiterDetails = GetRecruitersByName(MultipleDepartmentRecruiter);
            var actualRecruiterDetails = recruiterDetail.GetRecruiterDetail();
            Assert.AreEqual(expectedRecruiterDetails.Specialty.ToString(), actualRecruiterDetails.Specialty.ToString(), "Recruiter's Department is not matched");

            Log.Info("Step 5: Verify that star rating is not show overall under department");
            Assert.IsFalse(recruiterDetail.IsStarRatingOfReviewSectionDisplayed(), "Review Section Of 'Star Rating' is displayed");

            Log.Info("Step 6: Click on 'Review' link under department and verify it's navigate to user's review section of profile'");
            recruiterDetail.ClickOnHeaderRecruiterReview();
            Assert.IsTrue(recruiterDetail.IsReviewSectionOfProfileDisplayed(), "'Review' Section Of Profile is not displayed");
            Assert.IsTrue(recruiterDetail.IsReviewTextDisplayed(), "Review text Section not displayed");
        }
    }
}
