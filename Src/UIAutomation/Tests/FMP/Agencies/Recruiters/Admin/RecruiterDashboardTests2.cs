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
    public class RecruiterDashboardTests2 : FmpBaseTest
    {
        [DataTestMethod]
        [DataRow(FmpConstants.AgencyAdmin)]
        [DataRow(FmpConstants.SystemAdmin)]
        [TestCategory("Smoke"),TestCategory("SystemAdmin"), TestCategory("AgencyAdmin")]
        public void RecruiterDashboard_VerifyThatRecruitersSortByWorksSuccessfully(string userType)
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var recruiterTab = new RecruitersListPo(Driver);
            var userAdmin = GetLoginUsersByType(userType);
            var adminDashboard = new AdminDashboardPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Click on 'Log In' button, Login to application with credentials - Email:{userAdmin.Email}, password:{userAdmin.Password}");
            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(userAdmin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 3: Navigate to 'Recruiters' list page");
            adminDashboard.NavigateToPage(); 

            Log.Info("Step 4: Click on 'Name' 'SortBy' icon & verify 'FirstName' column gets sorted in ascending order");
            var expectedDefaultFirstName = recruiterTab.GetRecruitersByNthColumn(2);
            recruiterTab.ClickOnSortByButton("Name");
            var expectedFirstNameSortedInAscending = recruiterTab.GetRecruitersByNthColumn(2);
            var actualFirstNameSortedInAscending = expectedFirstNameSortedInAscending.OrderBy(n => n).ToList();
            CollectionAssert.AreEqual(expectedFirstNameSortedInAscending.ToList(), actualFirstNameSortedInAscending.ToList(), "Name list is not sorted in ascending order");

            Log.Info("Step 5: Click again on 'Name' 'SortBy' icon & verify 'FirstName' column gets sorted in descending order");
            recruiterTab.ClickOnSortByButton("Name");
            var expectedFirstNameSortedInDescending = recruiterTab.GetRecruitersByNthColumn(2);
            var actualFirstNameSortedInDescending = expectedFirstNameSortedInDescending.OrderByDescending(n => n).ToList();
            CollectionAssert.AreEqual(expectedFirstNameSortedInDescending.ToList(), actualFirstNameSortedInDescending.ToList(), "Name list is not sorted in descending order");

            Log.Info("Step 6: Click again on 'Name' 'SortBy' icon & verify 'FirstName' column gets sorted in default order");
            recruiterTab.ClickOnSortByButton("Name");
            var actualDefaultFirstName = recruiterTab.GetRecruitersByNthColumn(2);
            CollectionAssert.AreEqual(expectedDefaultFirstName.ToList(), actualDefaultFirstName.ToList(), "Name list is not sorted in default order");

            Log.Info("Step 10: Click on 'Email' 'SortBy' icon & verify 'Email' column gets sorted in ascending order");
            var expectedDefaultEmail = recruiterTab.GetRecruitersByNthColumn(3);
            recruiterTab.ClickOnSortByButton("Email");
            var expectedEmailSortedInAscending = recruiterTab.GetRecruitersByNthColumn(3);
            var actualEmailSortedInAscending = expectedEmailSortedInAscending.OrderBy(n => n).ToList();
            CollectionAssert.AreEqual(expectedEmailSortedInAscending.ToList(), actualEmailSortedInAscending.ToList(), "Email list is not sorted in ascending order");

            Log.Info("Step 11: Click again on 'Email' 'SortBy' icon & verify 'Email' column gets sorted in descending order");
            recruiterTab.ClickOnSortByButton("Email");
            var expectedEmailSortedInDescending = recruiterTab.GetRecruitersByNthColumn(3);
            var actualEmailSortedInDescending = expectedEmailSortedInDescending.OrderByDescending(n => n).ToList();
            CollectionAssert.AreEqual(expectedEmailSortedInDescending.ToList(), actualEmailSortedInDescending.ToList(), "Email list is not sorted in descending order");

            Log.Info("Step 12: Click again on 'Email' 'SortBy' icon & verify 'Email' column gets sorted in default order");
            recruiterTab.ClickOnSortByButton("Email");
            var actualDefaultEmail = recruiterTab.GetRecruitersByNthColumn(3);
            CollectionAssert.AreEqual(expectedDefaultEmail.ToList(), actualDefaultEmail.ToList(), "Email list is not sorted in default order");
        }
    }
}
