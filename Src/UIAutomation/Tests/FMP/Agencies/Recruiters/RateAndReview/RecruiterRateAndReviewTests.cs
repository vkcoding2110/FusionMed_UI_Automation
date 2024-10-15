using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Agencies.Recruiters.RateAndReview;
using UIAutomation.DataObjects.Common.Account;
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
    public class RecruiterRateAndReviewTests : FmpBaseTest
    {
        private const string ExploreMenuAgenciesButton = "Agencies";
        private const string AgencyName = "Fusion Medical Staffing";
        private const string ReviewRecruiterName = "Automation NewRecruiter";
        private const string RecruiterName= "Automation RecruiterUser";
        private static readonly Login UserLogin = GetLoginUsersByType("RecruiterReviewTests");

        [TestMethod]
        [TestCategory("Smoke")]
        public void VerifyReviewFormSubmittedSuccessfullyAsARecruiter()
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

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 3: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 4: Click on 'Agencies' menu item & Click on 'Fusion Medical Staffing' agency from the list");
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
            exploreMenu.ClickOnAgencyMenuItem(AgencyName);

            Log.Info("Step 5: Click on 'View all recruiter' link , click on 'Automation RecruiterUser' and Verify Review recruiter button is disabled");
            agency.ClickOnViewAllRecruiterLink();
            recruitersPo.ClickOnRecruiterCard(RecruiterName);
            Assert.IsTrue(recruiterDetail.IsReviewRecruiterButtonDisabled(), "Review recruiter button is enabled");

            Log.Info("Step 6: Navigate back and  Click on 'Automation NewRecruiter' and Verify Recruiter url is matched");
            Driver.Back();
            recruitersPo.WaitUntilFmpPageLoadingIndicatorInvisible();
            recruitersPo.ClickOnRecruiterCard(ReviewRecruiterName);
            var actualRecruiterName = recruiterDetail.GetRecruiterName();
            var actualRecruiterAgencyName = recruiterDetail.GetRecruiterAgencyName();
            recruiterDetail.ClickOnReviewRecruiterButton();
            var actualUrl = Driver.GetCurrentUrl().Replace("-", " ").Replace("-", "");
            var expectedUrl = FusionMarketPlaceUrl + "agencies/" + actualRecruiterAgencyName + "/" + actualRecruiterName + "/rate review/";
            Assert.AreEqual(expectedUrl.ToLowerInvariant(), actualUrl.ToLowerInvariant(), "Url is not matched");

            Log.Info("Step 7: Verify 'Traveler', 'Client' radio button is disabled and 'Other' radio button is enable");
            var rateAndReviewData = RateAndReviewDataFactory.AddRateAndReviewDetailAsAOtherUser();
            Assert.IsFalse(aboutMePo.IsUserTypeRadioButtonEnabled(),"User type field is enable");
            Assert.IsFalse(aboutMePo.IsTravelerUserTypeFieldEnabled(),"Traveler user type field is enable");
            Assert.IsTrue(aboutMePo.IsOtherUserTypeFieldEnabled(), "User type Radio Button is disable");

            Log.Info("Step 8: Select Recruiter interaction type radio button and Click on next button, verify about me progress bar is filled");
            aboutMePo.SelectUserTypeRadioButton(rateAndReviewData);
            aboutMePo.SelectRecruiterTypeRadioButton(rateAndReviewData);
            aboutMePo.ClickOnNextButton();
            Assert.IsTrue(aboutMePo.IsAboutMeProgressBarFilled(), "About me progress bar not filled");

            Log.Info("Step 9: Give overall and abilities rating for recruiter and verify rating label is displayed");
            starRatingPo.GiveOverAllRating(rateAndReviewData.OverallRating);

            Log.Info("Step 10: Click on next button and verify star rating progress bar is filled");
            aboutMePo.ClickOnNextButton();
            Assert.IsTrue(starRatingPo.IsStarRatingProgressBarFilled(), "Star rating progress bar not filled");

            Log.Info("Step 11: Give recommend scale for recruiter and write review for recruiter");
            scaleAndReviewPo.ScrollRateAndReviewScale(rateAndReviewData);
            scaleAndReviewPo.AddReviewForRecruiter(rateAndReviewData.ReviewMessage);

            Log.Info("Step 12: Click on next button, verify Scale and Review progress bar is filled and Preview your review message is displayed");
            aboutMePo.ClickOnNextButton();
            aboutMePo.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(scaleAndReviewPo.IsScaleAndReviewProgressBarFilled(), "Scale and Review progress bar not filled");
            Assert.IsTrue(successPo.IsPreviewYourReviewMessageDisplayed(), "Preview your review message is not displayed");

            Log.Info("Step 13: Verify all the selected recruiter's review detail and Click on 'Submit Review' button");
            var reviewDate = DateTime.Now;
            var actualRecruiterDetail = successPo.GetSelectedDetailOfRecruiter();
            var expectedDescribeUserSelectedText = "Worked with " + actualRecruiterName.Split(" ")[0] + " " + FmpConstants.SelectedRecruiterType[rateAndReviewData.InteractionWithRecruiter];
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

            Log.Info("Step 14: Click on 'Submit Review' button ,Verify Preview progress bar is filled and Thank you message is displayed");
            successPo.ClickOnSubmitReviewButton();
            successPo.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(successPo.IsPreviewProgressBarFilled(), "Preview progress bar not filled");
            Assert.IsTrue(successPo.IsThankYouMessageDisplayed(), "Thank you message is not displayed");

            Log.Info("Step 15: Click on 'View Profile' button and verify all the details on recruiter detail page is correct");
            successPo.ClickOnViewProfileButton();
            successPo.WaitUntilFmpTextLoadingIndicatorInvisible();
            var expectedSelectedUserDetail = recruiterDetail.GetSelectedUserTypeText();
            var actualSelectedUserDetail = "Worked with " + actualRecruiterName.Split(" ")[0] + " " + FmpConstants.SelectedRecruiterType[rateAndReviewData.InteractionWithRecruiter] + " " + reviewDate.ToString("MMMM yyyy");
            var expectedUserRoleTypeName = recruiterDetail.GetUserRoleTypeName();
            var expectedReviewDate = recruiterDetail.GetReviewDate();
            var expectedReviewMessageOfRecruiter = recruiterDetail.GetReviewMessage();
            Assert.AreEqual(expectedSelectedUserDetail.ToLowerInvariant(), actualSelectedUserDetail.ToLowerInvariant(), "Selected recruiter details is not matched");
            Assert.IsTrue(expectedUserName.Contains(expectedUserRoleTypeName), "Traveler user name is not present");
            Assert.AreEqual(expectedReviewDate, reviewDate.ToString("MMMM yyyy").RemoveWhitespace(), "Review date is not matched");
            Assert.AreEqual(expectedReviewMessageOfRecruiter, rateAndReviewData.ReviewMessage, "Review message is not matched");
        }
    }
}
