using System;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter.Admin;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Agencies.Recruiters.Admin
{
    [TestClass]
    [TestCategory("RateAndReview"), TestCategory("FMP")]
    public class RecruiterReviewsTest1 : FmpBaseTest
    {
        private const string DateText = "Date";
        private const string ReviewsTab = "Reviews";
        private const string RecruiterText = "Recruiter";
        private const string RecruiterName = "TestGuestUser";
        private const string AgencyText = "Agency";
        private const string AgencyName = "Fusion Medical Staffing";
        private const string SubTabName = "All";
        private const string StatusFilter = "Status";
        private const string StatusTypeText = "Auto Approved";
        private const string PublicFilterText = "Public";
        private const string PublicFilterOption = "Yes";
        private const string RatingFilterOption = "Rating";
        private const string ReviewerText = "Reviewer";
        private const string ReviewerName = "Test";

        [DataTestMethod]
        [DataRow(FmpConstants.AgencyAdmin)]
        [DataRow(FmpConstants.SystemAdmin)]
        [TestCategory("Smoke"),TestCategory("SystemAdmin"), TestCategory("AgencyAdmin")]
        public void ReviewsDashboard_VerifyThatRecruiterReviewsSearchByFilterWorksSuccessfully(string userType)
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var reviewsList = new RecruiterReviewsListPo(Driver);
            var adminDashboard = new AdminDashboardPo(Driver);
            var userAdmin = GetLoginUsersByType(userType);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{userAdmin.Email}, password:{userAdmin.Password}");
            fmpLogin.LoginToApplication(userAdmin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to 'Review' list page");
            adminDashboard.NavigateToPage();

            Log.Info("Step 5: Click on 'Review' sub menu , click on 'All' tabs and verify 'Recruiter Reviews' page open Successfully");
            adminDashboard.ClickOnReviewsSubMenuTabs(ReviewsTab);
            if (userAdmin.Type == FmpConstants.SystemAdmin)
            {
                reviewsList.ClickOnReviewsSubTabs(SubTabName);
            }
            Assert.IsTrue(reviewsList.IsRecruiterReviewsPageOpened(), "'Recruiter Reviews' page is not opened");

            Log.Info("Step 5: Click On 'Date' filter text, enter 'Start' and 'End' date & verify sorted date");
            reviewsList.ClickOnRecruiterReviewsFilterText(DateText);
            var startDate = DateTime.Now.AddMonths(-6);
            var endDate = DateTime.Now;
            reviewsList.EnterStartAndEndDate(startDate, endDate);
            var dateList = reviewsList.GetRecruiterReviewsByNthColumn<string>(2);
            for (var i = 1; i < dateList.Count; i++)
            {
                var date = DateTime.ParseExact(dateList[i], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                Assert.IsTrue(date >= startDate && date <= endDate, "Recruiter 'Start' and 'End' date doesn't matched");
            }

            Log.Info("Step 6: Click On 'Recruiter' filter text, enter 'Recruiter' name & verify 'Recruiter' sorted data");
            reviewsList.ClickOnClearFiltersButton();
            reviewsList.ClickOnRecruiterReviewsFilterText(RecruiterText);
            reviewsList.SearchRecruiterReviewsByFilter(RecruiterName, RecruiterText);
            var recruiterList = reviewsList.GetRecruiterReviewsByNthColumn<string>(3);
            foreach (var name in recruiterList)
            {
                Assert.IsTrue(name.ToLowerInvariant().Contains(RecruiterName.ToLowerInvariant()), "'Recruiter' name is not matched");
            }

            Log.Info("Step 7: Click On 'Agency' filter text, enter 'Agency' name & verify 'Agency' sorted data");
            reviewsList.ClickOnClearFiltersButton();
            reviewsList.ClickOnRecruiterReviewsFilterText(AgencyText);
            reviewsList.SearchRecruiterReviewsByFilter(AgencyName, AgencyText);
            var agencyTextList = reviewsList.GetRecruiterReviewsByNthColumn<string>(4);
            foreach (var name in agencyTextList)
            {
                Assert.IsTrue(name.ToLowerInvariant().Contains(AgencyName.ToLowerInvariant()), "'Agency' name is not matched");
            }

            Log.Info("Step 8: Click On 'Recruiter  Status' filter text, enter 'Recruiter  Status' name & verify 'Recruiter  Status' data");
            reviewsList.ClickOnClearFiltersButton();
            reviewsList.SearchRecruiterReviewsByStatusFilter(StatusFilter, StatusTypeText);
            var statusList = reviewsList.GetRecruiterReviewsByNthColumn<string>(5);
            foreach (var name in statusList)
            {
                Assert.IsTrue(name.Contains(StatusTypeText), "'Status' is not matched");
            }

            Log.Info("Step 9: Click On 'Public Status' filter text, enter 'Public Status' name & verify 'Public Status' data");
            reviewsList.ClickOnClearFiltersButton();
            reviewsList.SearchRecruiterReviewsByStatusFilter(PublicFilterText, PublicFilterOption);
            var publicStatusList = reviewsList.GetRecruiterReviewsByNthColumn<string>(6);
            foreach (var name in publicStatusList)
            {
                Assert.IsTrue(name.Contains(PublicFilterOption), "Public 'Status' is not matched");
            }

            Log.Info("Step 10: Click On 'Rating' filter text, enter 'Min' and 'Max' Rating & verify sorted date");
            reviewsList.ClickOnRecruiterReviewsFilterText(RatingFilterOption);
            const int minRating = 2;
            const int maxRating = 4;
            reviewsList.EnterMinAndMaxRating(minRating, maxRating, RatingFilterOption);
            var ratingList = reviewsList.GetRecruiterReviewsByNthColumn<string>(7);
            var myStringList = ratingList.Select(double.Parse).ToList();
            foreach (var t in myStringList)
            {
                Assert.IsTrue(t is >= minRating and <= maxRating, "'Min' and 'Max' Rating doesn't matched");
            }

            Log.Info("Step 11: Click On 'Reviewer' filter text, enter 'Reviewer' name & verify 'Reviewer' sorted data");
            reviewsList.ClickOnClearFiltersButton();
            reviewsList.ClickOnRecruiterReviewsFilterText(ReviewerText);
            reviewsList.SearchRecruiterReviewsByFilter(ReviewerName, ReviewerText);
            var reviewerList = reviewsList.GetRecruiterReviewsByNthColumn<string>(8);
            foreach (var name in reviewerList)
            {
                Assert.IsTrue(name.ToLowerInvariant().Contains(ReviewerName.ToLowerInvariant()), "'Reviewer' name is not matched");
            }
        }

        [DataTestMethod]
        [DataRow(FmpConstants.AgencyAdmin)]
        [DataRow(FmpConstants.SystemAdmin)]
        [TestCategory("Smoke"),TestCategory("SystemAdmin"), TestCategory("AgencyAdmin")]
        public void ReviewsDashboard_VerifyThatRecruiterReviewsSortByWorksSuccessfully(string userType)
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var reviewsList = new RecruiterReviewsListPo(Driver);
            var adminDashboard = new AdminDashboardPo(Driver);
            var userAdmin = GetLoginUsersByType(userType);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{userAdmin.Email}, password:{userAdmin.Password}");
            fmpLogin.LoginToApplication(userAdmin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to 'Review' list page");
            adminDashboard.NavigateToPage();

            Log.Info("Step 5: Click on 'Review' sub menu , click on 'All' tabs");
            adminDashboard.ClickOnReviewsSubMenuTabs(ReviewsTab);
            if (userAdmin.Type == FmpConstants.SystemAdmin)
            {
                reviewsList.ClickOnReviewsSubTabs(SubTabName);
            }

            Log.Info("Step 6: Click on 'Date' 'SortBy' icon & verify 'Date' column gets sorted in ascending order");
            var expectedDefaultDate = reviewsList.GetRecruiterReviewsByNthColumn<DateTime>(2);
            reviewsList.ClickOnSortByButton(DateText);
            var expectedDateSortedInAscending = reviewsList.GetRecruiterReviewsByNthColumn<DateTime>(2);
            var actualDateSortedInAscending = expectedDateSortedInAscending.OrderBy(n => n).ToList();
            CollectionAssert.AreEqual(expectedDateSortedInAscending.ToList(), actualDateSortedInAscending.ToList(), "Date list is not sorted in ascending order");

            Log.Info("Step 7: Click again on 'Date' 'SortBy' icon & verify 'Date' column gets sorted in descending order");
            reviewsList.ClickOnSortByButton(DateText);
            var expectedDateSortedInDescending = reviewsList.GetRecruiterReviewsByNthColumn<DateTime>(2);
            var actualDateSortedInDescending = expectedDateSortedInDescending.OrderByDescending(n => n).ToList();
            CollectionAssert.AreEqual(expectedDateSortedInDescending.ToList(), actualDateSortedInDescending.ToList(), "Date list is not sorted in descending order");

            Log.Info("Step 8: Click again on 'Date' 'SortBy' icon & verify 'Date' column gets sorted in default order");
            reviewsList.ClickOnSortByButton(DateText);
            var actualDefaultDate = reviewsList.GetRecruiterReviewsByNthColumn<DateTime>(2);
            CollectionAssert.AreEqual(expectedDefaultDate.ToList(), actualDefaultDate.ToList(), "Date list is not sorted in default order");
        }
    }
}
