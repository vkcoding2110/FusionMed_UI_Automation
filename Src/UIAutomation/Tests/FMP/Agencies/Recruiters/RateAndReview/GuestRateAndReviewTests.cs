using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Account;
using UIAutomation.DataFactory.FMP.Agencies.Recruiters.RateAndReview;
using UIAutomation.DataObjects.FMP.Account;
using UIAutomation.Enum.Recruiters;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter.RateAndReview;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.BrowseAll.Agencies;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Agencies.Recruiters.RateAndReview
{
    [TestClass]
    [TestCategory("RateAndReview"), TestCategory("FMP")]
    public class GuestRateAndReviewTests : FmpBaseTest
    {
        private const string ExploreMenuAgenciesButton = "Agencies";
        private const string AgencyName = "Fusion Medical Staffing";
        private const string RecruiterName = "Automation TestGuestUser";
        private static SignUp _signUp = new();
        private const string ValidationMessage = "* Please select an option.";

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            _signUp = SignUpDataFactory.GetDataForSignUpForm();
            setup.CreateUser(_signUp, UserType.Guest);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void VerifyReviewFormSubmittedSuccessfullyForGuestUser()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agency = new AgencyDetailPo(Driver);
            var recruitersPo = new RecruiterListingPo(Driver);
            var recruiterDetail = new RecruiterDetailsPo(Driver);
            var rateAndReviewPo = new RateAndReviewPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var previewPo = new StepSuccessPo(Driver);
            var scaleAndReviewPo = new StepReviewPo(Driver);
            var starRatingPo = new StepRatingPo(Driver);
            var aboutMePo = new StepAboutMePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 3: Click on 'Agencies' menu item & Click on 'Fusion Medical Staffing' agency from the list");
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
            exploreMenu.ClickOnAgencyMenuItem(AgencyName);

            Log.Info("Step 4: Click on 'View all recruiter' link , click on 'Guest recruiter' and verify recruiter page url ");
            agency.ClickOnViewAllRecruiterLink();
            recruitersPo.ClickOnRecruiterCard(RecruiterName);
            var actualRecruiterName = recruiterDetail.GetRecruiterName();
            var actualRecruiterAgencyName = recruiterDetail.GetRecruiterAgencyName();
            recruiterDetail.ClickOnReviewRecruiterButton();
            var actualUrl = Driver.GetCurrentUrl().Replace("-", " ").Replace("-", "");
            var expectedUrl = FusionMarketPlaceUrl + "agencies/" + actualRecruiterAgencyName + "/" + actualRecruiterName + "/rate review/";
            Assert.AreEqual(expectedUrl.ToLowerInvariant(), actualUrl.ToLowerInvariant(), "Url is not matched");

            Log.Info($"Step 5: Login to application with credentials - Email:{_signUp.Email}, password:{_signUp.Password}");
            rateAndReviewPo.ClickOnLoginButton();
            rateAndReviewPo.WaitUntilFmpPageLoadingIndicatorInvisible();
            fmpLogin.LoginToApplication(_signUp);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 6: Verify user type and recruiter type radio button is enable and not selected");
            var rateAndReviewData = RateAndReviewDataFactory.AddRateAndReviewDetail();
            Assert.IsTrue(aboutMePo.IsUserTypeRadioButtonEnabled(), "User type Radio Button is Not enabled");
            Assert.IsTrue(aboutMePo.IsRecruiterTypeRadioButtonEnabled(), "Interacted with recruiter Radio Button is not enabled");
            Assert.IsFalse(aboutMePo.IsUserTypeRadioButtonSelected(), "User type Radio Button is not selected");
            Assert.IsFalse(aboutMePo.IsRecruiterTypeRadioButtonSelected(), "Interacted with recruiter Radio Button is not selected");

            Log.Info("Step 7: Select user type & recruiter type radio button ,Verify User type & Recruiter interaction type radio button is selected");
            aboutMePo.SelectUserTypeRadioButton(rateAndReviewData);
            aboutMePo.SelectRecruiterTypeRadioButton(rateAndReviewData);
            Assert.IsTrue(aboutMePo.IsUserTypeRadioButtonSelected(), "User type Radio Button is not selected");
            Assert.IsTrue(aboutMePo.IsRecruiterTypeRadioButtonSelected(), "Recruiter interaction type Radio Button is not selected");

            Log.Info("Step 8: Click on next button and verify about me progress bar is filled");
            aboutMePo.ClickOnNextButton();
            Assert.IsTrue(aboutMePo.IsAboutMeProgressBarFilled(), "About me progress bar not filled");

            Log.Info("Step 9: Give overall rating for recruiter and verify rating label is displayed");
            starRatingPo.GiveOverAllRating(rateAndReviewData.OverallRating);
            var actualRatingLabelText = starRatingPo.GetOverAllRatingLabelText();
            var expectedStateOption = FmpConstants.OverAllRatingAndMessage[rateAndReviewData.OverallRating];
            Assert.AreEqual(expectedStateOption, actualRatingLabelText, "Over all rating label text is not matched");

            Log.Info("Step 10: Click on next button and verify star rating progress bar is filled");
            aboutMePo.ClickOnNextButton();
            Assert.IsTrue(starRatingPo.IsStarRatingProgressBarFilled(), "Star rating progress bar not filled");

            Log.Info("Step 11: Give recommend scale for recruiter and write review for recruiter");
            scaleAndReviewPo.ScrollRateAndReviewScale(rateAndReviewData);
            scaleAndReviewPo.AddReviewForRecruiter(rateAndReviewData.ReviewMessage);

            Log.Info("Step 12: Click on next button and verify Scale and Review progress bar is filled ");
            aboutMePo.ClickOnNextButton();
            rateAndReviewPo.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(scaleAndReviewPo.IsScaleAndReviewProgressBarFilled(), "Scale and Review progress bar not filled");

            Log.Info("Step 13: Verify all the selected recruiter's review detail and Click on 'Submit Review' button");
            var reviewDate = DateTime.Now;
            var actualRecruiterDetail = previewPo.GetSelectedDetailOfRecruiter();
            var expectedDescribeUserSelectedText = RoleType.Client + " | " + "worked with automation 4-9 times";
            var actualGuestUserName = previewPo.GetUserName();
            var actualOverAllRating = previewPo.GetOverAllRatingStar();
            var actualMonthAndYear = previewPo.GetReviewDate();
            Assert.AreEqual(expectedDescribeUserSelectedText.ToLowerInvariant(), actualRecruiterDetail.ToLowerInvariant(), "Selected recruiter detail is not matched");
            Assert.AreEqual(_signUp.FirstName.ToLowerInvariant(), actualGuestUserName, "Guest user name is not matched");
            Assert.AreEqual(rateAndReviewData.OverallRating.ToString(), actualOverAllRating, "Over all rating is not matched");
            Assert.AreEqual(reviewDate.ToString("MMMM yyyy").RemoveWhitespace(), actualMonthAndYear, "Review date is not matched");

            Log.Info("Step 14: Click on 'Submit Review' button and verify Preview progress bar is filled ");
            previewPo.ClickOnSubmitReviewButton();
            rateAndReviewPo.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(previewPo.IsPreviewProgressBarFilled(), "Scale and Review progress bar not filled");

            Log.Info("Step 15: Click on 'View Profile' button and verify all the details is correct");
            previewPo.ClickOnViewProfileButton();
            rateAndReviewPo.WaitUntilFmpTextLoadingIndicatorInvisible();
            var actualSelectedUserDetail = recruiterDetail.GetSelectedUserTypeText();
            var expectedSelectedUserDetail = RoleType.Client + " | " + "worked with automation 4-9 times";
            var expectedGuestUserNameData = recruiterDetail.GetUserRoleTypeName();
            var expectedReviewDate = recruiterDetail.GetReviewDate();
            Assert.AreEqual(expectedSelectedUserDetail.ToLowerInvariant() + " " + reviewDate.ToString("MMMM yyyy").ToLowerInvariant(), actualSelectedUserDetail.ToLowerInvariant(), "Selected recruiter details is not matched");
            Assert.AreEqual(expectedGuestUserNameData, _signUp.FirstName.ToLowerInvariant(), "Guest user name is not matched");
            Assert.AreEqual(expectedReviewDate, reviewDate.ToString("MMMM yyyy").RemoveWhitespace(), "Review date is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void VerifyValidationMessagesIsDisplayedForMandatoryFieldsAndLogOutWorkSuccessfully()
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

            Log.Info($"Step 2: Login to application with credentials - Email:{_signUp.Email}, password:{_signUp.Password}");
            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(_signUp);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 3: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 4: Click on 'Agencies' menu item & Click on 'Fusion Medical Staffing' agency from the list");
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
            exploreMenu.ClickOnAgencyMenuItem(AgencyName);

            Log.Info("Step 5: Click on 'View all recruiter' link , click on 'Guest recruiter' and verify recruiter page url ");
            agency.ClickOnViewAllRecruiterLink();
            recruitersPo.ClickOnRecruiterCard(RecruiterName);
            recruiterDetail.ClickOnReviewRecruiterButton();

            Log.Info("Step 6: Click on 'Next' button without filling user role data and verify validation message is displayed");
            var rateAndReviewData = RateAndReviewDataFactory.AddRateAndReviewDetail();
            aboutMePo.ClickOnNextButton();
            Assert.AreEqual(ValidationMessage.RemoveWhitespace(), aboutMePo.GetValidationMessageOfUserRoleTypeField().RemoveWhitespace(), "Validation message is not displayed for 'User role type ' field");
            Assert.AreEqual(ValidationMessage.RemoveWhitespace(), aboutMePo.GetValidationMessageOfInteractionWithRecruiterField().RemoveWhitespace(), "Validation message is not displayed for 'Interaction with recruiter' field");

            Log.Info("Step 7: Fill the User role detail, Click on 'Next' button");
            aboutMePo.SelectUserTypeRadioButton(rateAndReviewData);
            aboutMePo.SelectRecruiterTypeRadioButton(rateAndReviewData);
            aboutMePo.ClickOnNextButton();

            Log.Info("Step 8: Click on 'Next' button without giving overall rating and verify validation message is displayed");
            aboutMePo.ClickOnNextButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, starRatingPo.GetValidationMessageOfOverAllRatingField(),"Validation message is not displayed for 'Overall rating' field");

            Log.Info("Step 9: Give 1 star rating and verify 1 star rating message is displayed");
            var rateAndReviewStar = 1;
            starRatingPo.GiveOverAllRating(rateAndReviewStar);
            var actualOneStarRatingLabelText = starRatingPo.GetOverAllRatingLabelText();
            var expectedOneStarOption = FmpConstants.OverAllRatingAndMessage[rateAndReviewStar];
            Assert.AreEqual(expectedOneStarOption, actualOneStarRatingLabelText, "1 star rating message is not displayed");

            Log.Info("Step 10: Give 2 star rating and verify 2 star rating message is displayed");
            rateAndReviewStar = 2;
            starRatingPo.GiveOverAllRating(rateAndReviewStar);
            var actualTwoStarRatingLabelText = starRatingPo.GetOverAllRatingLabelText();
            var expectedTwoStarOption = FmpConstants.OverAllRatingAndMessage[rateAndReviewStar];
            Assert.AreEqual(expectedTwoStarOption, actualTwoStarRatingLabelText, "2 star rating message is not displayed");

            Log.Info("Step 11: Give 3 star rating and verify 3 star rating message is displayed");
            rateAndReviewStar = 3;
            starRatingPo.GiveOverAllRating(rateAndReviewStar);
            var actualThreeStarRatingLabelText = starRatingPo.GetOverAllRatingLabelText();
            var expectedThreeStarOption = FmpConstants.OverAllRatingAndMessage[rateAndReviewStar];
            Assert.AreEqual(expectedThreeStarOption, actualThreeStarRatingLabelText, "3 star rating message is not displayed");

            Log.Info("Step 12: Give 4 star rating and verify 4 star rating message is displayed");
            rateAndReviewStar = 4;
            starRatingPo.GiveOverAllRating(rateAndReviewStar);
            var actualFourStarRatingLabelText = starRatingPo.GetOverAllRatingLabelText();
            var expectedFourStarOption = FmpConstants.OverAllRatingAndMessage[rateAndReviewStar];
            Assert.AreEqual(expectedFourStarOption, actualFourStarRatingLabelText, "4 star rating message is not displayed");

            Log.Info("Step 13: Give 5 star rating and verify 5 star rating message is displayed");
            rateAndReviewStar = 5;
            starRatingPo.GiveOverAllRating(rateAndReviewStar);
            var actualFiveStarRatingLabelText = starRatingPo.GetOverAllRatingLabelText();
            var expectedFiveStarOption = FmpConstants.OverAllRatingAndMessage[rateAndReviewStar];
            Assert.AreEqual(expectedFiveStarOption, actualFiveStarRatingLabelText, "5 star rating message is not displayed");

            Log.Info("Step 14: Add review message,Click on 'Next' button without giving review scale and verify validation message is displayed for review scale");
            aboutMePo.ClickOnNextButton();
            scaleAndReviewPo.AddReviewForRecruiter(rateAndReviewData.ReviewMessage);
            aboutMePo.ClickOnNextButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage,scaleAndReviewPo.GetValidationMessageOfReviewScale(),"Validation message is not displayed for 'Review scale'");

            Log.Info("Step 15: Add 'Review scale' and clear 'Review message' field and verify validation message is displayed for 'Review message' ");
            scaleAndReviewPo.AddReviewForRecruiter("");
            scaleAndReviewPo.ScrollRateAndReviewScale(rateAndReviewData);
            aboutMePo.ClickOnNextButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage,scaleAndReviewPo.GetValidationMessageOfReviewMessageTextarea(),"Validation message is not displayed for 'Review message'");

            Log.Info("Step 16: Add Inappropriate message in 'Review message' field and Verify validation message is displayed for inappropriate content");
            scaleAndReviewPo.AddReviewForRecruiter(FmpConstants.InappropriateMessage);
            aboutMePo.ClickOnNextButton();
            aboutMePo.WaitUntilFmpPageLoadingIndicatorInvisible();
            const string expectedInappropriateContentMessage = "Uh oh! Unfortunately, we detected inappropriate content in your review. The word 'shit' is unacceptable for submission. Please go back and remove any inappropriate language from your review.";
            Assert.AreEqual(expectedInappropriateContentMessage.RemoveWhitespace(), successPo.GetInappropriateContentMessage().RemoveWhitespace(), "Validation message is not matched for Inappropriate content");
        }
    }
}
