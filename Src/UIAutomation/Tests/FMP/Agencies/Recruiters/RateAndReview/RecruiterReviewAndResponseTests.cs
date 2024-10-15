using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace UIAutomation.Tests.FMP.Agencies.Recruiters.RateAndReview
{
    [TestClass]
    [TestCategory("RateAndReview"), TestCategory("FMP")]
    public class RecruiterReviewAndResponseTests : FmpBaseTest
    {
        private const string ResponseCrudRecruiterName = "Automation RecruiterRespond";
        private static readonly TravelerRateAndReview RateAndReviewData = RateAndReviewDataFactory.AddRateAndReviewDetailAsATraveler();
        private static readonly Login FiveReviewRecruiter = GetLoginUsersByType("RecruiterForResponseCRUD");
        private const string ExploreMenuAgenciesButton = "Agencies";
        private const string AgencyName = "Fusion Medical Staffing";
        private const string ReviewsTab = "Reviews";
        private static readonly RateAndReviewBase RateAndReviewDetails = RateAndReviewDataFactory.AddRateAndReviewDetail();

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.AddReviewForRecruiter(FusionMarketPlaceLoginCredentials, RateAndReviewData, ResponseCrudRecruiterName);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void AgencyAdmin_VerifyAddUpdateDeleteReviewResponseWorksSuccessfully()
        {
            var adminDashboardPo = new AdminDashboardPo(Driver);
            var userAdmin = GetLoginUsersByType(FmpConstants.AgencyAdmin);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with recruiter credentials - Email:{userAdmin.Email}, password:{userAdmin.Password}");
            LoginToTheSite(userAdmin);

            Log.Info("Step 3: Verify add, update & delete recruiter review response works successfully");
            adminDashboardPo.ClickOnReviewsSubMenuTabs(ReviewsTab);
            AddUpdateDeleteReviewResponse();
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void RecruiterAdmin_VerifyAddUpdateDeleteReviewResponseWorksSuccessfully()
        {
            var userAdmin = GetLoginUsersByType(FmpConstants.RecruiterForResponseCrud);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with recruiter credentials - Email:{userAdmin.Email}, password:{userAdmin.Password}");
            LoginToTheSite(userAdmin);

            Log.Info("Step 3: Verify add, update & delete recruiter review response works successfully");
            AddUpdateDeleteReviewResponse();
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void AgencyAdmin_VerifyDisputeReviewWorksSuccessfully()
        {
            var adminDashboardPo = new AdminDashboardPo(Driver);
            var userAdmin = GetLoginUsersByType(FmpConstants.AgencyAdmin);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with recruiter credentials - Email:{userAdmin.Email}, password:{userAdmin.Password}");
            LoginToTheSite(userAdmin);

            Log.Info("Step 3: Verify 'Dispute Review' works successfully");
            adminDashboardPo.ClickOnReviewsSubMenuTabs(ReviewsTab);
            DisputeReview(userAdmin);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void RecruiterAdmin_VerifyDisputeReviewWorksSuccessfully()
        {
            var userAdmin = GetLoginUsersByType(FmpConstants.RecruiterForResponseCrud);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with recruiter credentials - Email:{userAdmin.Email}, password:{userAdmin.Password}");
            LoginToTheSite(userAdmin);

            Log.Info("Step 3: Verify 'Dispute Review' works successfully");
            DisputeReview(userAdmin);
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("RecruiterDashboard")]
        public void RecruiterAdmin_VerifyThatReviewDisplayedOnRecruiterDashboardSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var reviewsDashboard = new ReviewsDashboardPo(Driver);
            var adminDashboard = new AdminDashboardPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Login as 'Recruiter' and Navigate to dashboard page");
            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(FiveReviewRecruiter);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            adminDashboard.NavigateToPage();

            Log.Info("Step 3: Get Recruiter data From Grid and verify details: " + DateTime.Now);
            var actualRecruiterName = FiveReviewRecruiter.Name;
            var date = reviewsDashboard.GetReviewerDataFromRow(1, 2);
            var actualDate = DateTime.ParseExact(date.RemoveWhitespace(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            Assert.AreEqual(DateTime.Now.ToString("MM/dd/yyyy"), actualDate.ToString("MM/dd/yyyy"), $"Review date is not matched: Expected: {DateTime.Now} & Actual: {actualDate}" + DateTime.Now);
            Assert.AreEqual(FusionMarketPlaceLoginCredentials.Name.Split(" ")[0], reviewsDashboard.GetReviewerDataFromRow(1, 3), "Recruiter 'Name' is not matched");
            var overAllRating = reviewsDashboard.GetReviewerDataFromRow(1, 7);
            Assert.AreEqual(RateAndReviewData.OverallRating, (int)Convert.ToDouble(overAllRating), "Over all Rating rating label text is not matched");
            Assert.AreEqual(RoleType.Traveler.ToString(), reviewsDashboard.GetReviewerDataFromRow(1, 4), "Recruiter 'Role' is not matched");

            Log.Info("Step 4: Click on Recruiter Expand Collapse Icon and verify details");
            reviewsDashboard.ClickOnRecruiterExpandCollapseIcon(1, 1);
            Assert.AreEqual(FusionMarketPlaceLoginCredentials.Name.Split(" ")[0], reviewsDashboard.GetReviewerNameText(), "Recruiter 'Name' is not matched");
            var expectedDescribeUserText = RoleType.Traveler + " | " + "Worked " + RateAndReviewDetails.NumberOfTravelJobs + " travel jobs with automation";
            var actualReviewUserDetailsText = reviewsDashboard.GetReviewUserDetailsText();
            Assert.AreEqual(expectedDescribeUserText.ToLowerInvariant(), actualReviewUserDetailsText.ToLowerInvariant(), "Selected recruiter details is not matched");
            var actualReviewDate = reviewsDashboard.GetReviewerDateText();
            Assert.AreEqual(DateTime.Now.ToString("MM/dd/yyyy"), actualReviewDate.ToString("MM/dd/yyyy").RemoveWhitespace(), "Review date is not matched");
            Assert.AreEqual(RateAndReviewData.ReviewMessage, reviewsDashboard.GetReviewsDescriptionText(), "Reviewer 'Reviews' is not matched");

            Log.Info("Step 5: Get rating data from recruiter dashboard & Verify it is matching with expected reating data");
            var actualAbilitiesData = reviewsDashboard.GetAbilitiesRating(1);
            Assert.AreEqual(RateAndReviewData.MeetYourNeedsRating, actualAbilitiesData.MeetYourNeedsRating, "Meet your needs rating label text is not matched");
            Assert.AreEqual(RateAndReviewData.CommunicationRating, actualAbilitiesData.CommunicationRating, "Communication rating label text is not matched");
            Assert.AreEqual(RateAndReviewData.KnowledgeRating, actualAbilitiesData.KnowledgeRating, "Knowledge rating label text is not matched");
            Assert.AreEqual(RateAndReviewData.ProfessionalRelationshipRating, actualAbilitiesData.ProfessionalRelationshipRating, "Professional relationship rating label text is not matched");
            Assert.AreEqual(RateAndReviewData.SupportRating, actualAbilitiesData.SupportRating, "Support rating label text is not matched");
            Assert.AreEqual(RateAndReviewData.EfficiencyRating, actualAbilitiesData.EfficiencyRating, "Efficiency rating label text is not matched");
            Assert.AreEqual(RateAndReviewData.OverallRating, actualAbilitiesData.OverallRating, "Over all Rating rating label text is not matched");
        }

        private void LoginToTheSite(Login login)
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var adminDashboardPo = new AdminDashboardPo(Driver);

            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(login);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            adminDashboardPo.NavigateToPage();
        }

        private void LoginToTravelerAndSearchRecruiter(Login travelerLogin, string recruiterName)
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agency = new AgencyDetailPo(Driver);
            var recruitersPo = new RecruiterListingPo(Driver);
            var profilePage = new ProfileMenuPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var recruiterDetail = new RecruiterDetailsPo(Driver);

            fmpHeader.ClickOnLogInBadge();
            profilePage.ClickOnLogOutButton();
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();
            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(travelerLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            fmpHeader.ClickOnBrowseAllButton();
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
            exploreMenu.ClickOnAgencyMenuItem(AgencyName);
            agency.ClickOnViewAllRecruiterLink();
            recruitersPo.ClickOnRecruiterCard(recruiterName);
            recruiterDetail.WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        private void LoggedIntoTheRecruiter(Login recruiterLogin)
        {
            var headerHomePagePo = new HeaderPo(Driver);
            var profilePage = new ProfileMenuPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var adminDashboardPo = new AdminDashboardPo(Driver);
            var reviewsDashboard = new ReviewsDashboardPo(Driver);

            headerHomePagePo.ClickOnProfileIcon();
            profilePage.ClickOnLogOutButton();
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();
            headerHomePagePo.ClickOnLogInButton();
            fmpLogin.LoginToApplication(recruiterLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            adminDashboardPo.NavigateToPage();
            reviewsDashboard.ClickOnExpandCollapseIcon(1, 1);
        }

        private void AddUpdateDeleteReviewResponse()
        {
            var recruiterDetail = new RecruiterDetailsPo(Driver);
            var reviewsDashboard = new ReviewsDashboardPo(Driver);

            Log.Info("Step 4: Click on 'ExpandCollapse' icon, Click on 'Write a Response' button & Verify 'Respond to this Review' popup is displayed");
            reviewsDashboard.ClickOnExpandCollapseIcon(1, 1);
            reviewsDashboard.ClickOnWriteAResponseButton();
            reviewsDashboard.WaitTillResponsePopupGetsOpen();
            Assert.IsTrue(reviewsDashboard.IsRespondReviewPopupOpened(), "'Respond Review' popup is not opened");
            const string expectedRespondReviewHeaderText = "Respond to this Review";
            var actualRespondReviewHeaderText = reviewsDashboard.GetRespondReviewHeaderText();
            Assert.AreEqual(expectedRespondReviewHeaderText, actualRespondReviewHeaderText, "Not Matched");

            Log.Info("Step 5: Verify close, cancel button works successfully & verify validation message is displayed for mandatory field");
            reviewsDashboard.ClickOnCloseIcon();
            Assert.IsFalse(reviewsDashboard.IsRespondReviewPopupOpened(), "'Respond Review' popup is not closed");
            reviewsDashboard.ClickOnWriteAResponseButton();
            reviewsDashboard.ClickOnCancelButton();
            Assert.IsFalse(reviewsDashboard.IsRespondReviewPopupOpened(), "'Respond Review' popup is not closed");
            reviewsDashboard.ClickOnWriteAResponseButton();
            reviewsDashboard.ClickOnPublishResponseButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, reviewsDashboard.GetResponseRequiredFieldValidationMessage(), "'Response required' validation message is not displayed");

            Log.Info("Step 6: Add respond and verify response display in 'recruiter dashboard'");
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            var responseText = "Thank you test respond" + randomNumber;
            reviewsDashboard.EnterResponseText(responseText);
            reviewsDashboard.ClickOnPublishResponseButton();
            reviewsDashboard.WaitUntilFmpPageLoadingIndicatorInvisible();
            var actualResponseTextOnRecruiterDashboard = reviewsDashboard.GetResponseText();
            Assert.AreEqual(responseText, actualResponseTextOnRecruiterDashboard, "Response text is not matched");

            Log.Info("Step 7: Login to the traveler & verify response display in 'recruiter detail' page");
            LoginToTravelerAndSearchRecruiter(FusionMarketPlaceLoginCredentials, ResponseCrudRecruiterName);
            var actualResponseText = recruiterDetail.GetResponseTextFromFirstReview();
            Assert.IsTrue(actualResponseText.Contains(responseText), $"Response text is not matched Actual: {actualResponseText} Expected: {responseText}");

            Log.Info("Step 8: Login to application as a recruiter");
            LoggedIntoTheRecruiter(FiveReviewRecruiter);

            Log.Info("Step 9: Edit response & verify response updated successfully");
            reviewsDashboard.ClickOnEditResponseButton();
            reviewsDashboard.WaitTillResponsePopupGetsOpen();
            Assert.IsTrue(reviewsDashboard.IsRespondReviewPopupOpened(), "'Respond Review' popup is not opened");
            const string expectedEditRespondHeaderText = "Edit Response";
            var actualEditRespondRespondHeaderText = reviewsDashboard.GetRespondReviewHeaderText();
            Assert.AreEqual(expectedEditRespondHeaderText, actualEditRespondRespondHeaderText, "'Edit Response' Header text is not matched");
            var updatedResponseText = "Thank you test respond updated" + randomNumber;
            reviewsDashboard.EnterResponseText(updatedResponseText);
            reviewsDashboard.ClickOnPublishResponseButton();
            reviewsDashboard.WaitUntilFmpPageLoadingIndicatorInvisible();
            var actualUpdatedResponseTextOnRecruiterDashboard = reviewsDashboard.GetResponseText();
            Assert.AreEqual(updatedResponseText, actualUpdatedResponseTextOnRecruiterDashboard, "Response text is not matched");

            Log.Info("Step 10: Log out from traveler & Verify updated response display on recruiter detail page");
            LoginToTravelerAndSearchRecruiter(FusionMarketPlaceLoginCredentials, ResponseCrudRecruiterName);
            var actualUpdatedResponseText = recruiterDetail.GetResponseTextFromFirstReview();
            Assert.IsTrue(actualUpdatedResponseText.Contains(updatedResponseText), "Response text is not matched");

            Log.Info("Step 11: Login to application as a recruiter");
            LoggedIntoTheRecruiter(FiveReviewRecruiter);

            Log.Info("Step 12: Verify 'cancel', 'Close' button works successfully and delete the response text & verify 'response' text id deleted from 'recruiter dashboard'");
            reviewsDashboard.ClickOnDeleteResponseButton();
            reviewsDashboard.WaitTillResponsePopupGetsOpen();
            Assert.IsTrue(reviewsDashboard.IsRespondReviewPopupOpened(), "'Delete Response' popup is not opened");
            reviewsDashboard.ClickOnCloseIcon();
            Assert.IsFalse(reviewsDashboard.IsRespondReviewPopupOpened(), "'Delete Response' popup is not closed");
            reviewsDashboard.ClickOnDeleteResponseButton();
            reviewsDashboard.ClickOnCancelButton();
            Assert.IsFalse(reviewsDashboard.IsRespondReviewPopupOpened(), "'Respond Review' popup is not closed");
            reviewsDashboard.ClickOnDeleteResponseButton();
            reviewsDashboard.ClickOnPublishResponseButton();
            reviewsDashboard.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsFalse(reviewsDashboard.IsResponseTextPresent(), "Response is not deleted");

            Log.Info("Step 13: Login to traveler & verify 'response' text is deleted from 'recruiter details' page");
            LoginToTravelerAndSearchRecruiter(FusionMarketPlaceLoginCredentials, ResponseCrudRecruiterName);
            var actualUpdatedResponseTextAfterDeletedResponse = recruiterDetail.GetResponseTextFromFirstReview();
            Assert.IsFalse(actualUpdatedResponseTextAfterDeletedResponse.Contains(responseText), "Response is not deleted from recruiter detail page");
            Assert.IsFalse(actualUpdatedResponseTextAfterDeletedResponse.Contains(updatedResponseText), "Response is not deleted from recruiter detail page");
        }

        private void DisputeReview(Login userType)
        {
            var reviewsDashboard = new ReviewsDashboardPo(Driver);

            Log.Info("Step 4: Click on 'ExpandCollapse' icon, Click on 'Dispute Review' button & verify 'Dispute This Review' popup is displayed");
            reviewsDashboard.ClickOnExpandCollapseIcon(1, 1);
            reviewsDashboard.ClickOnDisputeReviewButton();
            reviewsDashboard.WaitTillDisputePopupGetsOpen();
            var actualDisputeReviewText = reviewsDashboard.GetDisputeReviewPopupContentText();
            var expectedDisputeReviewText = "Should this review be removed from your profile?";
            if (userType.Type == FmpConstants.AgencyAdmin)
            {
                expectedDisputeReviewText = "Should this review be removed from " + ResponseCrudRecruiterName.Split(" ")[0] + "’s profile?";
            }
            Assert.AreEqual(expectedDisputeReviewText.RemoveWhitespace(), actualDisputeReviewText.RemoveWhitespace(), "'Dispute Review' popup text is not matched");
            Assert.IsTrue(reviewsDashboard.IsDisputeReviewPopupOpened(), "'Dispute This Review' popup is not opened");

            Log.Info("Step 5: Verify 'Close' & 'X' icon works successfully on 'Dispute Review' popup");
            reviewsDashboard.ClickOnCloseIcon();
            Assert.IsFalse(reviewsDashboard.IsDisputeReviewPopupOpened(), "'Dispute This Review' popup is opened");
            reviewsDashboard.ClickOnDisputeReviewButton();
            reviewsDashboard.ClickOnCancelButton();
            Assert.IsFalse(reviewsDashboard.IsDisputeReviewPopupOpened(), "'Dispute This Review' popup is opened");

            Log.Info("Step 6: Click on 'Request Rejection' button & verify rejection request had been set");
            reviewsDashboard.ClickOnDisputeReviewButton();
            const string disputeReason = "This is a Fake Review";
            reviewsDashboard.EnterDisputeReviewReason(disputeReason);
            reviewsDashboard.ClickOnRequestRejectionButton();
            const string expectedReviewRejectionText = "A rejection request has been submitted for this published review.";
            const string disputeStatusText = "Disputed";
            var actualReviewRejectionText = reviewsDashboard.GetReviewRejectionText();
            Assert.AreEqual(expectedReviewRejectionText, actualReviewRejectionText, "'Rejection request' text is not matched");
            var actualStatusTextFromRow = reviewsDashboard.GetRatingRowText(1, 5);
            Assert.AreEqual(disputeStatusText, actualStatusTextFromRow, "Status is not 'Disputed'");
            if (userType.Type == FmpConstants.RecruiterForResponseCrud)
            {
                var actualDisputeReason = reviewsDashboard.GetDisputeReasonText();
                Assert.AreEqual("Reason:" + disputeReason.RemoveWhitespace(), actualDisputeReason.RemoveWhitespace(), "The dispute reason is not matched");
            }

            Log.Info("Step 7: Click on 'Cancel Dispute' button & verify 'Dispute cancel' confirmation popup opened");
            reviewsDashboard.ClickOnCancelDisputeButton();
            Assert.IsTrue(reviewsDashboard.IsCancelDisputePopupOpened(), "Dispute cancel' confirmation popup is not opened");
            var actualCancelReviewPopupContentText = reviewsDashboard.GetCancelReviewPopupContentText();
            var expectedCancelReviewPopupContentText = "Are You Sure ?By canceling this dispute, this review will remain published on" + ResponseCrudRecruiterName.Split(" ")[0] + "’s profile and the Marketplace team will not evaluate it.";
            if (userType.Type != FmpConstants.AgencyAdmin)
            {
                expectedCancelReviewPopupContentText = "Are You Sure? By canceling this dispute, the review Test submitted will remain published on your profile and the Marketplace team will not evaluate it.";
            }
            Assert.AreEqual(expectedCancelReviewPopupContentText.RemoveWhitespace(), actualCancelReviewPopupContentText.RemoveWhitespace(), "'Cancel Review' popup content text is not matched");

            Log.Info("Step 8: Verify 'Keep dispute' button, 'X'close icon & 'Cancel Dispute' button works successfully");
            reviewsDashboard.ClickOnCloseIcon();
            Assert.IsFalse(reviewsDashboard.IsCancelDisputePopupOpened(), "Dispute cancel' confirmation popup is opened");
            reviewsDashboard.ClickOnCancelDisputeButton();
            reviewsDashboard.ClickOnKeepDisputeButton();
            Assert.IsFalse(reviewsDashboard.IsCancelDisputePopupOpened(), "Dispute cancel' confirmation popup is opened");
            Assert.IsTrue(reviewsDashboard.IsCancelDisputeButtonDisplayed(), "'Cancel Dispute' button is not Displayed");
            reviewsDashboard.ClickOnCancelDisputeButton();
            reviewsDashboard.ClickOnCancelDisputePopupButton();
            Assert.IsTrue(reviewsDashboard.IsDisputeButtonDisplayed(), "'Dispute Review' button is not displayed");
            const string approvedStatusText = "Approved";
            var actualStatusTextAfterCancelDispute = reviewsDashboard.GetRatingRowText(1, 5);
            Assert.AreEqual(approvedStatusText, actualStatusTextAfterCancelDispute, "Status is not 'Approved'");
        }
    }
}
