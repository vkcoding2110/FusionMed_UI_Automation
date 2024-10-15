using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Agencies.Recruiters.RateAndReview;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.Enum.Recruiters;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter.RateAndReview;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.BrowseAll.Agencies;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Agencies.Recruiters.RateAndReview
{
    [TestClass]
    [TestCategory("RateAndReview"), TestCategory("FMP")]
    public class TravelerRateAndReviewTests : FmpBaseTest
    {
        private const string ExploreMenuAgenciesButton = "Agencies";
        private const string AgencyName = "Fusion Medical Staffing";
        private const string TravelerRecruiterName = "Automation UserTraveler";
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("RateAndReviewTests");

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("RecruiterDashboard")]

        public void VerifyReviewFormSubmittedSuccessfullyAsATraveler()
        {
            var scaleAndReviewPo = new StepReviewPo(Driver);
            var starRatingPo = new StepRatingPo(Driver);
            var aboutMePo = new StepAboutMePo(Driver);
            var successPo = new StepSuccessPo(Driver);
            var recruiterDetail = new RecruiterDetailsPo(Driver);
            var stepAboutMePo = new StepAboutMePo(Driver);

            Log.Info($"Step 1: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            LogInAsAUser(UserLogin);

            Log.Info("Step 2: Click on 'Review Recruiter's Button, click on 'Logout' button and verify 'Home' page gets open");
            recruiterDetail.ClickOnReviewRecruiterButton();
            stepAboutMePo.ClickOnLogOutButton();
            Assert.AreEqual(Driver.GetCurrentUrl(), FusionMarketPlaceUrl, "Url is not matched");

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            LogInAsAUser(UserLogin);
            var actualRecruiterName = recruiterDetail.GetRecruiterName();
            var actualRecruiterAgencyName = recruiterDetail.GetRecruiterAgencyName();
            recruiterDetail.ClickOnReviewRecruiterButton();
            var actualUrl = Driver.GetCurrentUrl().Replace("-", " ").Replace("-", "");
            var expectedUrl = FusionMarketPlaceUrl + "agencies/" + actualRecruiterAgencyName + "/" + actualRecruiterName + "/rate review/";
            Assert.AreEqual(expectedUrl.ToLowerInvariant(), actualUrl.ToLowerInvariant(), "Url is not matched");

            Log.Info("Step 4: Select 'Number of Travel Jobs' dropdown, Click on next button & verify about me progress bar is filled");
            var rateAndReviewData = RateAndReviewDataFactory.AddRateAndReviewDetailAsATraveler();
            aboutMePo.SelectTravelJobsDropdown(rateAndReviewData.NumberOfTravelJobs);
            aboutMePo.ClickOnNextButton();
            Assert.IsTrue(aboutMePo.IsAboutMeProgressBarFilled(), "About me progress bar not filled");

            Log.Info("Step 5: Give overall and abilities rating for recruiter and verify rating label is displayed");
            var rating = FmpConstants.OverAllRatingAndMessage;
            starRatingPo.GiveOverAllRating(rateAndReviewData.OverallRating);
            starRatingPo.GiveAbilitiesRating(rateAndReviewData);
            var expectedOverAllRating = rating[rateAndReviewData.OverallRating];
            var expectedMeetYourNeedsRating = rateAndReviewData.MeetYourNeedsRating;
            var expectedSupportRating = rateAndReviewData.SupportRating;
            var expectedProfessionalRelationshipRating = rateAndReviewData.ProfessionalRelationshipRating;
            var expectedKnowledgeRating = rateAndReviewData.KnowledgeRating;
            var expectedCommunicationRating = rateAndReviewData.CommunicationRating;
            var expectedEfficiencyRating = rateAndReviewData.EfficiencyRating;
            var actualOverAllRatingLabelText = starRatingPo.GetOverAllRatingLabelText();
            var actualAbilitiesData = starRatingPo.GetAbilitiesRating();
            Assert.AreEqual(expectedOverAllRating, actualOverAllRatingLabelText, "Over all rating label text is not matched");
            Assert.AreEqual(expectedMeetYourNeedsRating, actualAbilitiesData.MeetYourNeedsRating, "Meet your needs rating label text is not matched");
            Assert.AreEqual(expectedCommunicationRating, actualAbilitiesData.CommunicationRating, "Communication rating label text is not matched");
            Assert.AreEqual(expectedKnowledgeRating, actualAbilitiesData.KnowledgeRating, "Knowledge rating label text is not matched");
            Assert.AreEqual(expectedProfessionalRelationshipRating, actualAbilitiesData.ProfessionalRelationshipRating, "Professional relationship rating label text is not matched");
            Assert.AreEqual(expectedSupportRating, actualAbilitiesData.SupportRating, "Support rating label text is not matched");
            Assert.AreEqual(expectedEfficiencyRating, actualAbilitiesData.EfficiencyRating, "Efficiency rating label text is not matched");

            Log.Info("Step 6: Click on next button and verify star rating progress bar is filled");
            aboutMePo.ClickOnNextButton();
            Assert.IsTrue(starRatingPo.IsStarRatingProgressBarFilled(), "Star rating progress bar not filled");

            Log.Info("Step 7: Give recommend scale for recruiter and write review for recruiter");
            scaleAndReviewPo.ScrollRateAndReviewScale(rateAndReviewData);
            scaleAndReviewPo.AddReviewForRecruiter(rateAndReviewData.ReviewMessage);

            Log.Info("Step 8: Click on next button, verify Scale and Review progress bar is filled and Preview your review message is displayed");
            aboutMePo.ClickOnNextButton();
            aboutMePo.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(scaleAndReviewPo.IsScaleAndReviewProgressBarFilled(), "Scale and Review progress bar not filled");
            Assert.IsTrue(successPo.IsPreviewYourReviewMessageDisplayed(), "Preview your review message is not displayed");

            Log.Info("Step 9: Verify all the selected recruiter's review detail and Click on 'Submit Review' button");
            var reviewDate = DateTime.Now;
            var actualRecruiterDetail = successPo.GetSelectedDetailOfRecruiter();
            var expectedDescribeUserSelectedText = RoleType.Traveler + " | " + $"Worked {rateAndReviewData.NumberOfTravelJobs} travel jobs with Automation"; 
            var actualUserName = successPo.GetUserName();
            var actualOverAllRating = successPo.GetOverAllRatingStar();
            var actualMonthAndYear = successPo.GetReviewDate();
            var expectedUserName = UserLogin.Name.ToLowerInvariant();
            var expectedReviewMessage = successPo.GetReviewMessage();
            Assert.AreEqual(expectedDescribeUserSelectedText.ToLowerInvariant(), actualRecruiterDetail.ToLowerInvariant(), "Selected recruiter detail is not matched");
            Assert.IsTrue(expectedUserName.Contains(actualUserName), "Traveler user name is not matched");
            Assert.AreEqual(rateAndReviewData.OverallRating.ToString(), actualOverAllRating, "Over all rating is not matched");
            Assert.AreEqual(reviewDate.ToString("MMMM yyyy").RemoveWhitespace(), actualMonthAndYear, "Review date is not matched");
            Assert.AreEqual(expectedReviewMessage, rateAndReviewData.ReviewMessage, "Review message is not matched");

            Log.Info("Step 10: Click on 'Submit Review' button ,Verify Preview progress bar is filled and Thank you message is displayed");
            successPo.ClickOnSubmitReviewButton();
            successPo.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(successPo.IsPreviewProgressBarFilled(), "Preview progress bar not filled");
            Assert.IsTrue(successPo.IsThankYouMessageDisplayed(), "Thank you message is not displayed");

            Log.Info("Step 11: Click on 'View Profile' button and verify all the details on recruiter detail page is correct");
            successPo.ClickOnViewProfileButton();
            successPo.WaitUntilFmpTextLoadingIndicatorInvisible();
            var actualSelectedUserDetail = recruiterDetail.GetSelectedUserTypeText();
            var expectedUserRoleTypeName = recruiterDetail.GetUserRoleTypeName();
            var expectedReviewDate = recruiterDetail.GetReviewDate();
            var expectedReviewMessageOfRecruiter = recruiterDetail.GetReviewMessage();
            Assert.AreEqual(expectedDescribeUserSelectedText.ToLowerInvariant() + " " + reviewDate.ToString("MMMM yyyy").ToLowerInvariant(), actualSelectedUserDetail.ToLowerInvariant(), "Selected recruiter details is not matched");
            Assert.IsTrue(expectedUserName.Contains(expectedUserRoleTypeName), "Traveler user name is not present");
            Assert.AreEqual(expectedReviewDate, reviewDate.ToString("MMMM yyyy").RemoveWhitespace(), "Review date is not matched");
            Assert.AreEqual(expectedReviewMessageOfRecruiter, rateAndReviewData.ReviewMessage, "Review message is not matched");
        }

        private void LogInAsAUser(Login user)
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agency = new AgencyDetailPo(Driver);
            var recruitersPo = new RecruiterListingPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);

            Driver.NavigateTo(FusionMarketPlaceUrl);
            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(user);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            fmpHeader.ClickOnBrowseAllButton();
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
            exploreMenu.ClickOnAgencyMenuItem(AgencyName);
            agency.ClickOnViewAllRecruiterLink();
            recruitersPo.ClickOnRecruiterCard(TravelerRecruiterName);
        }

        [TestMethod]
        public void VerifyTravelerCanUpdateRateAndReviewSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agency = new AgencyDetailPo(Driver);
            var recruitersPo = new RecruiterListingPo(Driver);
            var recruiterDetail = new RecruiterDetailsPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var scaleAndReviewPo = new StepReviewPo(Driver);
            var starRatingPo = new StepRatingPo(Driver);
            var aboutMePo = new StepAboutMePo(Driver);
            var successPo = new StepSuccessPo(Driver);
            var rateAndReviewPo = new RateAndReviewPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 3: Click on 'Browse All' button and  Click on 'Agencies' menu item , Click on 'Fusion Medical Staffing' agency from the list ");
            fmpHeader.ClickOnBrowseAllButton();
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
            exploreMenu.ClickOnAgencyMenuItem(AgencyName);

            Log.Info("Step 4: Click on 'View all recruiter' link , click on 'Automation UserTraveler' and verify recruiter page url ");
            agency.ClickOnViewAllRecruiterLink();
            recruitersPo.ClickOnRecruiterCard(TravelerRecruiterName);
            recruiterDetail.ClickOnReviewRecruiterButton();
            recruiterDetail.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 5: Add Review details for Recruiter as a traveler");
            var rateAndReviewData = RateAndReviewDataFactory.AddRateAndReviewDetailAsATraveler();
            aboutMePo.SelectTravelJobsDropdown(rateAndReviewData.NumberOfTravelJobs);
            aboutMePo.ClickOnNextButton();
            starRatingPo.GiveOverAllRating(rateAndReviewData.OverallRating);
            starRatingPo.GiveAbilitiesRating(rateAndReviewData);
            aboutMePo.ClickOnNextButton();
            scaleAndReviewPo.ScrollRateAndReviewScale(rateAndReviewData);
            scaleAndReviewPo.AddReviewForRecruiter(rateAndReviewData.ReviewMessage);
            aboutMePo.ClickOnNextButton();
            aboutMePo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 6: Verify all the selected recruiter's review detail");
            var reviewDate = DateTime.Now;
            var actualRecruiterDetail = successPo.GetSelectedDetailOfRecruiter();
            var expectedDescribeUserSelectedText = RoleType.Traveler + " | " + $"Worked {rateAndReviewData.NumberOfTravelJobs} travel jobs with Automation";
            var actualUserName = successPo.GetUserName();
            var actualOverAllRating = successPo.GetOverAllRatingStar();
            var actualMonthAndYear = successPo.GetReviewDate();
            var expectedUserRoleName = UserLogin.Name.ToLowerInvariant();
            var expectedReviewMessage = successPo.GetReviewMessage();
            Assert.AreEqual(expectedDescribeUserSelectedText.ToLowerInvariant(), actualRecruiterDetail.ToLowerInvariant(), "Selected recruiter detail is not matched");
            Assert.IsTrue(expectedUserRoleName.Contains(actualUserName), "Traveler user name is not matched");
            Assert.AreEqual(rateAndReviewData.OverallRating.ToString(), actualOverAllRating, "Over all rating is not matched");
            Assert.AreEqual(reviewDate.ToString("MMMM yyyy").RemoveWhitespace(), actualMonthAndYear, "Review date is not matched");
            Assert.AreEqual(expectedReviewMessage, rateAndReviewData.ReviewMessage, "Review message is not matched");

            Log.Info("Step 7: Click on 'Edit Review' button , Verify Scale and review progress bar is unfilled");
            successPo.ClickOnEditReviewButton();
            Assert.IsFalse(scaleAndReviewPo.IsScaleAndReviewProgressBarFilled(), "Scale and Review progress bar is filled");

            Log.Info("Step 8: Click on 'Back' button and Edit the review details");
            for (var i = 0; i < 2; i++) 
                rateAndReviewPo.ClickOnBackButton();
            rateAndReviewPo.WaitUntilFmpPageLoadingIndicatorInvisible();
            var updatedRateAndReviewData = RateAndReviewDataFactory.EditRateAndReviewDetailAsATraveler();
            aboutMePo.SelectTravelJobsDropdown(updatedRateAndReviewData.NumberOfTravelJobs);
            aboutMePo.ClickOnNextButton();

            Log.Info("Step 9: Edit overall and abilities rating for recruiter");
            starRatingPo.GiveOverAllRating(updatedRateAndReviewData.OverallRating);
            starRatingPo.GiveAbilitiesRating(updatedRateAndReviewData);

            Log.Info("Step 10: Click on 'Next' button and Edit recommend scale, Review message");
            aboutMePo.ClickOnNextButton();
            scaleAndReviewPo.ScrollRateAndReviewScale(updatedRateAndReviewData);
            scaleAndReviewPo.AddReviewForRecruiter(updatedRateAndReviewData.ReviewMessage);
            aboutMePo.ClickOnNextButton();
            aboutMePo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 11: Verify recruiter's review detail are updated");
            var updatedReviewDate = DateTime.Now;
            var updatedRecruiterDetail = successPo.GetSelectedDetailOfRecruiter();
            var updatedUserDetail = RoleType.Traveler + " | " + $"Worked {updatedRateAndReviewData.NumberOfTravelJobs} travel jobs with Automation";
            var updatedUserName = successPo.GetUserName();
            var updatedOverAllRating = successPo.GetOverAllRatingStar();
            var updatedMonthAndYear = successPo.GetReviewDate();
            var updatedUserRoleName = UserLogin.Name.ToLowerInvariant();
            var updatedReviewMessage = successPo.GetReviewMessage();
            Assert.AreEqual(updatedUserDetail.ToLowerInvariant(), updatedRecruiterDetail.ToLowerInvariant(), "Selected recruiter detail is not matched");
            Assert.IsTrue(updatedUserRoleName.Contains(updatedUserName), "Traveler user name is not matched");
            Assert.AreEqual(updatedRateAndReviewData.OverallRating.ToString(), updatedOverAllRating, "Over all rating is not matched");
            Assert.AreEqual(updatedReviewDate.ToString("MMMM yyyy").RemoveWhitespace(), updatedMonthAndYear, "Review date is not matched");
            Assert.AreEqual(updatedRateAndReviewData.ReviewMessage, updatedReviewMessage, "Review message is not matched");
        }
    }
}
