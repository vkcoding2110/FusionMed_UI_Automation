using System;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter.Admin;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Agencies.Recruiters.Admin
{
    [TestClass]
    [TestCategory("RateAndReview"), TestCategory("FMP")]
    public class RecruiterReviewsTest2 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByType("AutomationRecruiterFiveReviews");
        private const string ExpectedReviewsHeaderText = "My Overall Ratings";
        private const string DateText = "Date";
        private const string ReviewerText = "Reviewer";
        private const string ReviewerName = "Test";
        private const string ReviewerRole = "Reviewer Role";
        private const string ReviewerRoleName = "Traveler";
        private const string StatusFilter = "Status";
        private const string StatusTypeText = "Auto Approved";
        private const string PublicFilterText = "Public";
        private const string PublicFilterOption = "Yes";
        private const string RatingFilterOption = "Rating";

        [DataTestMethod]
        [DataRow(FmpConstants.RecruiterNoReviews)]
        [DataRow(FmpConstants.Recruiter2Star)]
        [DataRow(FmpConstants.RecruiterFiveReviews)]
        [TestCategory("RecruiterDashboard")]
        public void VerifyAbilityToViewOverallAndCategoricalRatings(string userType)
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var reviewsDashboard = new ReviewsDashboardPo(Driver);
            var adminDashboard = new AdminDashboardPo(Driver);

            var recruiterReviews = GetLoginUsersByTypeAndPlatform(userType);
            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{recruiterReviews.Email}, password:{recruiterReviews.Password}");
            fmpLogin.LoginToApplication(recruiterReviews);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to 'Review' list page and verify 'Reviews' page open Successfully");
            adminDashboard.NavigateToPage();
            const string expectedHeaderText = "Reviews";
            Assert.AreEqual(expectedHeaderText, reviewsDashboard.GetReviewsHeaderText(), "'Reviews' page is not opened");

            switch (recruiterReviews.Type)
            {
                case FmpConstants.RecruiterNoReviews:
                    Log.Info("Step 5: Verify text of 'No Reviews' Recruiter dashboard ");
                    const string expectedNoReviewsRecruiterText = "You don't have any reviews just yet";
                    Assert.AreEqual(expectedNoReviewsRecruiterText, reviewsDashboard.GetNoReviewRecruiterText(), "'NoReview' Recruiter text is not matched");
                    break;

                case FmpConstants.Recruiter2Star:
                    Log.Info("Step 5: Verify '2 Reviews' Recruiter details");
                    Assert.AreEqual(ExpectedReviewsHeaderText, reviewsDashboard.GetMyOverallRatingsHeaderText(), "'Reviews' header text is not match");
                    Assert.IsTrue(reviewsDashboard.IsHowLikelyReviewersTextPresent(), "'How Likely Reviewers Are To Recommend You' is not present on reviews dashboard");

                    Log.Info("Step 6: Verify Reviews Count");
                    var actualReviewCount = reviewsDashboard.GetReviewCountText();
                    Assert.AreEqual(reviewsDashboard.GetReviewerCount().ToString(), actualReviewCount.RemoveWhitespace(), "'Reviews' count doesn't matched");

                    var actualGetGetStarAreaText = reviewsDashboard.GetGetStarAreaText();
                    var firstRowRating = reviewsDashboard.GetRatingRowText(1, 7);
                    var secondRowRating = reviewsDashboard.GetRatingRowText(2, 7);
                    var totalRatings = Convert.ToDouble(firstRowRating) + Convert.ToDouble(secondRowRating);
                    var expectedTotalRatings = totalRatings / 2;
                    Assert.AreEqual(expectedTotalRatings, Convert.ToDouble(actualGetGetStarAreaText), "'Reviews' count doesn't matched");
                    break;

                case FmpConstants.RecruiterFiveReviews:
                    Log.Info("Step 5:  Verify '5 Reviews' Recruiter details");
                    var reviewCount = reviewsDashboard.GetReviewCountText();
                    Assert.IsTrue(Convert.ToInt32(reviewCount) >= 5, "Reviews count doesn't matched");
                    Assert.AreEqual(ExpectedReviewsHeaderText, reviewsDashboard.GetMyOverallRatingsHeaderText(), "'Reviews' header text is not match");
                    Assert.AreEqual(FmpConstants.MyOverallRatingsDescriptionText, reviewsDashboard.GetMyOverallRatingsDescriptionText(), "My Overall Ratings Description is not match");
                    Assert.IsTrue(reviewsDashboard.IsAbilityToMeetYourNeedsTextPresent(), "'Ability To Meet Your Needs' text is not present");
                    Assert.IsTrue(reviewsDashboard.IsPersonalityFitTextPresent(), "'Personality' text is not present");
                    Assert.IsTrue(reviewsDashboard.IsCommunicationTextPresent(), "'Communication' text is not present");
                    Assert.IsTrue(reviewsDashboard.IsThoughtfulnessTextPresent(), "'Thoughtfulness' text is not present");
                    Assert.IsTrue(reviewsDashboard.IsKnowledgeTextPresent(), "'Knowledge' text is not present");
                    Assert.IsTrue(reviewsDashboard.IsEfficiencyTextPresent(), "'Efficiency' text is not present");
                    Assert.IsTrue(reviewsDashboard.IsHowLikelyReviewersTextPresent(), "'How Likely Reviewers...' text is not present");
                    break;
            }
        }


        [TestMethod]
        [TestCategory("Smoke"),TestCategory("RecruiterDashboard")]
        public void VerifyThatReviewerDetailsSearchByFilterWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var reviewsDashboard = new ReviewsDashboardPo(Driver);
            var adminDashboard = new AdminDashboardPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to 'Review' list page");
            adminDashboard.NavigateToPage();

            Log.Info("Step 5: Click On 'Date' filter text, enter 'Start' and 'End' date & verify sorted date");
            reviewsDashboard.ClickOnRecruiterFilterText(DateText);
            var startDate = DateTime.Now.AddMonths(-6);
            var endDate = DateTime.Now;
            reviewsDashboard.EnterStartAndEndDate(startDate, endDate);
            var dateList = reviewsDashboard.GetRecruitersByNthColumn(2);
            for (var i = 1; i < dateList.Count; i++)
            {
                var date = DateTime.ParseExact(dateList[i], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                Assert.IsTrue(date >= startDate && date < endDate, "Recruiter 'Start' and 'End' date does't matched");
            }

            Log.Info("Step 6: Click On 'Reviewer' filter text, enter 'Reviewer' name & verify 'Reviewer' sorted data");
            reviewsDashboard.ClickOnClearFiltersButton();
            reviewsDashboard.ClickOnRecruiterFilterText(ReviewerText);
            reviewsDashboard.SearchRecruitersByFilter(ReviewerName, ReviewerText);
            var reviewerList = reviewsDashboard.GetRecruitersByNthColumn(3);
            foreach (var name in reviewerList)
            {
                Assert.IsTrue(name.ToLowerInvariant().Contains(ReviewerName.ToLowerInvariant()), "'Reviewer' name is not matched");
            }

            Log.Info("Step 7: Click On 'Reviewer Role' filter text, enter 'Reviewer' role & verify 'Reviewer Role' data");
            reviewsDashboard.ClickOnClearFiltersButton();
            reviewsDashboard.SearchReviewerRoleFilter(ReviewerRole, ReviewerRoleName);
            var reviewerRoleList = reviewsDashboard.GetRecruitersByNthColumn(4);
            foreach (var name in reviewerRoleList)
            {
                Assert.IsTrue(name.Contains(ReviewerRoleName), "'Reviewer' role is not matched");
            }

            Log.Info("Step 8: Click On 'Reviewer Status' filter text, enter 'Reviewer Status' name & verify 'Reviewer Status' data");
            reviewsDashboard.ClickOnClearFiltersButton();
            reviewsDashboard.SearchReviewerRoleFilter(StatusFilter, StatusTypeText);
            var statusList = reviewsDashboard.GetRecruitersByNthColumn(5);
            foreach (var name in statusList)
            {
                Assert.IsTrue(name.Contains(StatusTypeText), "'Status' is not matched");
            }

            Log.Info("Step 9: Click On 'Public Status' filter text, enter 'Public Status' name & verify 'Public Status' data");
            reviewsDashboard.ClickOnClearFiltersButton();
            reviewsDashboard.SearchReviewerRoleFilter(PublicFilterText, PublicFilterOption);
            var publicStatusList = reviewsDashboard.GetRecruitersByNthColumn(6);
            foreach (var name in publicStatusList)
            {
                Assert.IsTrue(name.Contains(PublicFilterOption), "Public 'Status' is not matched");
            }

            Log.Info("Step 10: Click On 'Rating' filter text, enter 'Min' and 'Max' Rating & verify sorted date");
            reviewsDashboard.ClickOnClearFiltersButton();
            reviewsDashboard.ClickOnRecruiterFilterText(RatingFilterOption);
            const int minRating = 2;
            const int maxRating = 4;
            reviewsDashboard.EnterMinAndMaxRating(minRating, maxRating, RatingFilterOption);
            var ratingList = reviewsDashboard.GetRecruitersByNthColumn(7);
            var myStringList = ratingList.Select(double.Parse).ToList();
            foreach (var t in myStringList)
            {
                Assert.IsTrue(t is >= minRating and <= maxRating, "'Min' and 'Max' Rating doesn't matched");
            }

            Log.Info("Step 11: Click on 'Clear Filter' button & verify filter button gets disabled");
            reviewsDashboard.ClickOnClearFiltersButton();
            Assert.IsFalse(reviewsDashboard.IsClearFilterButtonEnabled(), "Clear filter button is still enabled");
        }
    }
}
