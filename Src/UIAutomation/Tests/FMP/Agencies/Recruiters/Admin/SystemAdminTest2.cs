using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using UIAutomation.DataFactory.FMP.Agencies.Recruiters.RateAndReview;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.DataObjects.FMP.Agencies.Recruiter.RateAndReview;
using UIAutomation.Enum.Recruiters;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter.Admin;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.BrowseAll.Agencies;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Agencies.Recruiters.Admin
{
    [TestClass]
    [TestCategory("RateAndReview"), TestCategory("FMP")]
    public class SystemAdminTest2 : FmpBaseTest
    {
        private const string SystemAdminRejectReviewName = "Automation SystemAdminRejectReview";
        private static readonly TravelerRateAndReview RateAndReviewData = RateAndReviewDataFactory.AddRateAndReviewDetailAsATraveler();
        private static readonly Login AdminLogin = GetLoginUsersByType("SystemAdminTests");
        private static readonly Login UserLogin = GetLoginUsersByType("AutomationSystemAdminRejectReview");
        private const string ReviewsTab = "Reviews";
        private const string AllTab = "All";
        private const string ExploreMenuAgenciesButton = "Agencies";
        private const string AgencyName = "Fusion Medical Staffing";
        private static readonly RateAndReviewBase RateAndReviewDetails = RateAndReviewDataFactory.AddRateAndReviewDetail();

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.AddReviewForRecruiter(FusionMarketPlaceLoginCredentials, RateAndReviewData, SystemAdminRejectReviewName);
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("SystemAdmin")]
        public void SystemAdmin_VerifyRejectReviewWorksSuccessfully()
        {
            var reviewsDashboard = new ReviewsDashboardPo(Driver);
            var recruiterDetail = new RecruiterDetailsPo(Driver);

            Log.Info($"Step 1: Login to application with credentials - Email:{AdminLogin.Email}, password:{AdminLogin.Password}");
            LoginToTheApp(AdminLogin);

            Log.Info("Step 2: Get Recruiter data From Grid and verify details");
            var actualRecruiterName = UserLogin.Name;
            var date = reviewsDashboard.GetRecruiterReviewsFromRow(1, 2);
            var actualDate = DateTime.ParseExact(date.RemoveWhitespace(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            Assert.AreEqual(DateTime.Now.ToString("MM/dd/yyyy"), actualDate.ToString("MM/dd/yyyy"), $"Review date is not matched. Expected: {DateTime.Now} & Actual: {actualDate}");
            Assert.AreEqual(UserLogin.Name, reviewsDashboard.GetRecruiterReviewsFromRow(1, 3), "Recruiter 'Name' is not matched");
            var overAllRating = reviewsDashboard.GetRecruiterReviewsFromRow(1, 7);
            Assert.AreEqual(RateAndReviewData.OverallRating, (int)Convert.ToDouble(overAllRating), "Over all Rating rating label text is not matched");

            Log.Info("Step 3: Click on Recruiter Expand Collapse Icon and verify details");
            reviewsDashboard.ClickOnRecruiterExpandCollapseIcon(1, 1);
            Assert.AreEqual(FusionMarketPlaceLoginCredentials.Name.Split(" ")[0], reviewsDashboard.GetReviewerNameText(), "Recruiter 'Name' is not matched");
            var expectedDescribeUserText = RoleType.Traveler + " | " + "Worked " + RateAndReviewDetails.NumberOfTravelJobs + " travel jobs with automation";
            var actualReviewUserDetailsText = reviewsDashboard.GetReviewUserDetailsText();
            Assert.AreEqual(expectedDescribeUserText.ToLowerInvariant(), actualReviewUserDetailsText.ToLowerInvariant(), "Selected recruiter details is not matched");
            var actualReviewDate = reviewsDashboard.GetReviewerDateText();
            Assert.AreEqual(DateTime.Now.ToString("MM/dd/yyyy"), actualReviewDate.ToString("MM/dd/yyyy").RemoveWhitespace(), "Review date is not matched");
            Assert.AreEqual(RateAndReviewData.ReviewMessage, reviewsDashboard.GetReviewsDescriptionText(), "Reviewer 'Reviews' is not matched");

            Log.Info("Step 4: Get rating data from recruiter dashboard & Verify it is matching with expected reating data");
            var actualAbilitiesData = reviewsDashboard.GetAbilitiesRating(1);
            Assert.AreEqual(RateAndReviewData.MeetYourNeedsRating, actualAbilitiesData.MeetYourNeedsRating, "Meet your needs rating label text is not matched");
            Assert.AreEqual(RateAndReviewData.CommunicationRating, actualAbilitiesData.CommunicationRating, "Communication rating label text is not matched");
            Assert.AreEqual(RateAndReviewData.KnowledgeRating, actualAbilitiesData.KnowledgeRating, "Knowledge rating label text is not matched");
            Assert.AreEqual(RateAndReviewData.ProfessionalRelationshipRating, actualAbilitiesData.ProfessionalRelationshipRating, "Professional relationship rating label text is not matched");
            Assert.AreEqual(RateAndReviewData.SupportRating, actualAbilitiesData.SupportRating, "Support rating label text is not matched");
            Assert.AreEqual(RateAndReviewData.EfficiencyRating, actualAbilitiesData.EfficiencyRating, "Efficiency rating label text is not matched");
            Assert.AreEqual(RateAndReviewData.OverallRating, actualAbilitiesData.OverallRating, "Over all Rating rating label text is not matched");

            Log.Info("Step 5: Click on 'Reject' Reviews button and verify 'Dispute This Review' popup is displayed");
            reviewsDashboard.WaitTillDisputePopupGetsOpen();
            reviewsDashboard.ClickOnRejectReviewsButton();
            var actualRejectReviewText = reviewsDashboard.GetDisputeReviewPopupContentText();
            var expectedRejectReviewText = "Are you sure you want to reject and unpublish " + SystemAdminRejectReviewName.Split(" ")[0] + "’s review?";
            Assert.AreEqual(expectedRejectReviewText, actualRejectReviewText, "'Reject Review' popup text is not matched");
            Assert.IsTrue(reviewsDashboard.IsRejectReviewPopupOpened(), "'Reject This Review' popup is not opened");

            Log.Info("Step 6: Verify 'Close' & 'X' icon works successfully on 'Reject Review' popup");
            reviewsDashboard.ClickOnCloseIcon();
            Assert.IsFalse(reviewsDashboard.IsRejectReviewPopupOpened(), "'Reject This Review' popup is opened");
            reviewsDashboard.ClickOnRejectReviewsButton();
            reviewsDashboard.ClickOnCancelButton();
            Assert.IsFalse(reviewsDashboard.IsRejectReviewPopupOpened(), "'Reject This Review' popup is opened");

            Log.Info("Step 7: Click on 'Reject Review' button & Verify 'Rejected due to a dispute while review was published..' text is displayed");
            reviewsDashboard.ClickOnRejectReviewsButton();
            reviewsDashboard.ClickOnRejectUnPublishReviewButton();
            const string expectedReviewRejectionText = "Rejected due to inappropriate content.";
            const string rejectedStatusText = "Rejected";
            var actualRejectReviewDate = reviewsDashboard.GetRejectReviewDateText();
            Assert.AreEqual(DateTime.Now.ToString("MM/dd/yyyy"), actualRejectReviewDate.ToString("MM/dd/yyyy"), "'Reject Review' date is not matched");
            var actualReviewRejectionText = reviewsDashboard.GetReviewRejectionText();
            Assert.AreEqual(expectedReviewRejectionText, actualReviewRejectionText, "'Rejection request' text is not matched");
            var actualStatusTextFromRow = reviewsDashboard.GetRatingRowText(1, 5);
            Assert.AreEqual(rejectedStatusText, actualStatusTextFromRow, "Status is not 'Disputed'");

            Log.Info("Step 8: Logout from 'System' admin and verify review is deleted from 'Recruiter' profile");
            NavigateToRecruiterDetailPage(SystemAdminRejectReviewName);
            Assert.IsFalse(recruiterDetail.IsReviewTextPresent(), "Review text is displayed");

            Log.Info("Step 9: Login as 'System admin', click on 'Approve Review' button and verify 'Approve This Review' pop up gets opened");
            LoginToTheApp(AdminLogin);
            reviewsDashboard.ClickOnRecruiterExpandCollapseIcon(1, 1);
            reviewsDashboard.ScrollToNthRowAndColumn(1, 1);
            reviewsDashboard.ClickOnApproveReviewsButton();
            var actualApproveReviewText = reviewsDashboard.GetDisputeReviewPopupContentText();
            var expectedApproveReviewText = "Are you sure you want to approve and publish " + SystemAdminRejectReviewName.Split(" ")[0] + "’s review to the site?";
            Assert.AreEqual(expectedApproveReviewText, actualApproveReviewText, "'Approve Review' popup text is not matched");
            Assert.IsTrue(reviewsDashboard.IsApproveReviewPopupOpened(), "'Approve This Review' popup is not opened");

            Log.Info("Step 10: Verify 'Close' & 'X' icon works successfully on 'Approve Review' popup");
            reviewsDashboard.ClickOnCloseIcon();
            Assert.IsFalse(reviewsDashboard.IsApproveReviewPopupOpened(), "'Approve This Review' popup is opened");
            reviewsDashboard.ClickOnApproveReviewsButton();
            reviewsDashboard.ClickOnCancelButton();
            Assert.IsFalse(reviewsDashboard.IsApproveReviewPopupOpened(), "'Approve This Review' popup is opened");

            Log.Info("Step 11: Click on 'Reject Review' button & Verify 'Rejected due to a dispute while review was published..' text is displayed");
            reviewsDashboard.ClickOnApproveReviewsButton();
            reviewsDashboard.ClickOnApproveReviewPopupButton();
            const string approvedStatusText = "Approved";
            var actualStatusFromRow = reviewsDashboard.GetRatingRowText(1, 5);
            Assert.AreEqual(approvedStatusText, actualStatusFromRow, "Status is not 'Disputed'");

            Log.Info("Step 12: Logout as 'System Admin' and verify review is displayed on 'Recruiter' profile");
            NavigateToRecruiterDetailPage(SystemAdminRejectReviewName);
            Assert.IsTrue(recruiterDetail.IsReviewTextPresent(), "Review text is not displayed");
        }

        private void NavigateToRecruiterDetailPage(string recruiterName)
        {
            var headerHomePagePo = new HeaderPo(Driver);
            var profilePage = new ProfileMenuPo(Driver);
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agency = new AgencyDetailPo(Driver);
            var recruitersPo = new RecruiterListingPo(Driver);

            headerHomePagePo.ClickOnProfileIcon();
            profilePage.ClickOnLogOutButton();
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();
            fmpHeader.ClickOnBrowseAllButton();
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
            exploreMenu.ClickOnAgencyMenuItem(AgencyName);
            agency.ClickOnViewAllRecruiterLink();
            recruitersPo.ClickOnRecruiterCard(recruiterName);
        }

        private void LoginToTheApp(Login systemAdmin)
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var adminDashboard = new AdminDashboardPo(Driver);
            var reviewsList = new RecruiterReviewsListPo(Driver);

            Driver.NavigateTo(FusionMarketPlaceUrl);
            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(systemAdmin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();
            adminDashboard.NavigateToPage();
            adminDashboard.ClickOnReviewsSubMenuTabs(ReviewsTab);
            reviewsList.ClickOnReviewsSubTabs(AllTab);
        }
    }
}
