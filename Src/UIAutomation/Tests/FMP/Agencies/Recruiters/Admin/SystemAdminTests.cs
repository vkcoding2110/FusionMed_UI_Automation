using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Agencies.Recruiters.Admin;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.DataObjects.FMP.Agencies.Recruiter.Admin;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter.Admin;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Agencies.Recruiters.Admin
{
    [TestClass]
    [TestCategory("RateAndReview"), TestCategory("FMP")]
    public class SystemAdminTests : FmpBaseTest
    {
        private const int RowIndex = 1;
        private const string ExpectedInnerWrapperPopUpHeaderText = "Are you sure?";
        public string AccountNavText = "Admin Dashboard";
        private readonly AgencyAdmin AgencyAdminDetail = AgencyAdminDataFactory.AddAgencyAdminDetails();
        private static readonly Login UserLogin = GetLoginUsersByType("SystemAdminTests");
        public const string AgencyName = "Atlas MedStaff";

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("SystemAdmin")]
        public void SystemAdmin_AgencyAdmin_VerifyThatAddUpdateAndDeleteWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileMenu = new ProfileMenuPo(Driver);
            var agencyAdminTab = new AgencyAdminListPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on Agency admin profile badge, Click on 'Admin' navigation item button ");
            fmpHeader.ClickOnLogInBadge();
            profileMenu.ClickOnAccountNavMenuItem(AccountNavText);

            Log.Info("Step 5:Click on agency admin tab ,Click on 'Invite Agency admin', Do not enter any details in 'Invite Agency admin' row, click on 'Check' icon & verify validation message gets displayed");
            agencyAdminTab.ClickOnAgencyAdminTab();
            agencyAdminTab.ClickOnInviteAgencyAdminButton();
            agencyAdminTab.ClickOnInviteAgencyAdminCheckIcon();
            const string expectedValidationText = "First Name is required. Last Name is required. Email is required.";
            var actualValidationText = agencyAdminTab.GetInviteAgencyAdminValidationText();
            Assert.AreEqual(expectedValidationText, actualValidationText, "The validation text is not matched");

            Log.Info("Step 6: Click on 'Invite Agency admin' button, Click on 'Close' icon & verify new row gets removed");
            agencyAdminTab.ClickOnInviteAgencyAdminValidationCloseIcon();
            agencyAdminTab.ClickOnInviteAgencyAdminButton();
            agencyAdminTab.ClickOnInviteAgencyAdminCloseIcon();
            Assert.IsFalse(agencyAdminTab.IsInviteAgencyAdminCloseIconDisplayed(), "The 'Invite Agency admin' row is still displayed");

            Log.Info("Step 7: Click on 'Invite Agency admin' button, add 'Agency admin' detail, Click on 'Check' icon & verify 'Agency admin' Added successfully");
            agencyAdminTab.ClickOnInviteAgencyAdminButton();
            agencyAdminTab.EnterAgencyAdminDetails(AgencyAdminDetail);
            var actualAgencyAdminDetail = agencyAdminTab.GetAgencyAdminDetails(RowIndex);
            Assert.AreEqual(AgencyAdminDetail.FirstName, actualAgencyAdminDetail.FirstName, "Agency admin 'First Name' is not matched");
            Assert.AreEqual(AgencyAdminDetail.LastName, actualAgencyAdminDetail.LastName, "Agency admin 'Last Name' is not matched");
            Assert.AreEqual(AgencyAdminDetail.Email, actualAgencyAdminDetail.Email, "Agency admin 'Email' is not matched");

            Log.Info("Step 8: Click on 'Actions' button, click on 'Edit' button, Click on 'Close' icon & verify 'Are you sure?' popup gets opened");
            agencyAdminTab.ClickOnActionsButton(RowIndex);
            agencyAdminTab.ClickOnEditButton();
            agencyAdminTab.ClickOnInviteAgencyAdminCloseIcon();
            Assert.AreEqual(ExpectedInnerWrapperPopUpHeaderText, agencyAdminTab.GetInnerWrapperPopUpHeaderText(), "Cancel edit popup header text is not matched");
            const string expectedCancelEditPopUpMessage = "By clicking the button below, any changes you've made to this item will be discarded.";
            Assert.AreEqual(expectedCancelEditPopUpMessage.RemoveWhitespace(), agencyAdminTab.GetInnerWrapperPopUpMessage().RemoveWhitespace(), "Cancel edit popup message is not matched");

            Log.Info("Step 9: Click on 'Discard Changes' button , Verify Cancel edit popup gets closed and Agency admin data is present");
            agencyAdminTab.ClickOnDiscardChangesButton();
            Assert.IsFalse(agencyAdminTab.IsInnerWrapperPopUpHeaderTextDisplayed(), "The Cancel edit popup is still open");
            Assert.AreEqual(AgencyAdminDetail.FirstName, actualAgencyAdminDetail.FirstName, "Agency admin 'First name' is not matched");
            Assert.AreEqual(AgencyAdminDetail.LastName, actualAgencyAdminDetail.LastName, "Agency admin 'Last Name' is not edited");

            Log.Info("Step 10: Click On Action button, Click on Edit button ,Edit agency admin details & verify Agency admin details updated successfully");
            agencyAdminTab.ClickOnActionsButton(RowIndex);
            agencyAdminTab.ClickOnEditButton();
            var editAgencyDetail = AgencyAdminDataFactory.EditAgencyAdminDetails();
            agencyAdminTab.EnterAgencyAdminDetails(editAgencyDetail);
            var actualEditedAgencyDetails = agencyAdminTab.GetAgencyAdminDetails(RowIndex);
            Assert.AreEqual(editAgencyDetail.FirstName, actualEditedAgencyDetails.FirstName, "Agency admin 'First Name' is not edited");
            Assert.AreEqual(editAgencyDetail.LastName, actualEditedAgencyDetails.LastName, "Agency admin 'Last Name' is not edited");

            Log.Info("Step 11: Click on 'Actions' button, click on 'Delete' button & verify 'Delete Agency admin' popup gets opened");
            agencyAdminTab.ClickOnActionsButton(RowIndex);
            agencyAdminTab.ClickOnDeleteButton();
            Assert.AreEqual(ExpectedInnerWrapperPopUpHeaderText, agencyAdminTab.GetInnerWrapperPopUpHeaderText(), "Cancel edit popup header text is not matched");
            const string expectedDeleteRecruiterPopUpMessage = "Are you sure you want to permanently delete this agency admin?";
            Assert.AreEqual(expectedDeleteRecruiterPopUpMessage, agencyAdminTab.GetInnerWrapperPopUpMessage(), "Cancel edit popup message is not matched");

            Log.Info("Step 12: Click on delete agency admin 'Cancel' button & Verify Cancel edit popup gets closed and Agency admin data is present");
            agencyAdminTab.ClickOnInnerWrapperCancelButton();
            Assert.IsFalse(agencyAdminTab.IsInnerWrapperPopUpHeaderTextDisplayed(), "The Cancel edit popup is still open");
            actualEditedAgencyDetails = agencyAdminTab.GetAgencyAdminDetails(RowIndex);
            Assert.AreEqual(editAgencyDetail.FirstName, actualEditedAgencyDetails.FirstName, "Agency admin 'First name' is not matched");
            Assert.AreEqual(editAgencyDetail.LastName, actualEditedAgencyDetails.LastName, "Agency admin 'Last Name' is not edited");

            Log.Info("Step 13: Click on 'Delete' button, click on 'Delete Agency admin' button & verify Agency admin gets removed");
            agencyAdminTab.ClickOnDeleteAgencyAdminButton(RowIndex);
            const string rowDropDown = "10";
            agencyAdminTab.SelectRowPerPageDropDown(rowDropDown);
            var actualAgencyAdminEmail = agencyAdminTab.GetColumnData(4);
            foreach (var name in actualAgencyAdminEmail)
            {
                Assert.IsFalse(name.Contains(editAgencyDetail.Email), "Agency admin email is not present");
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("SystemAdmin")]
        public void SystemAdmin_AgencyAdmin__VerifyThatViewFullScreenAndSortByWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var adminDashboard = new AdminDashboardPo(Driver);
            var agencyAdminTab = new AgencyAdminListPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Click on 'Log In' button, Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 3: Navigate to 'Recruiters' list page and Click on 'Agency admins' tab");
            adminDashboard.NavigateToPage();
            agencyAdminTab.ClickOnAgencyAdminTab();

            Log.Info("Step 4: Click on 'View Full Screen' button & verify 'Minimize' button is displayed");
            const string viewFullScreenButton = "View Full Screen";
            const string minimizeButton = "Minimize";
            agencyAdminTab.ClickOnMaximizeMinimizeButton(viewFullScreenButton);
            Assert.IsTrue(agencyAdminTab.IsButtonNameDisplayed(minimizeButton), "Minimize button is not displayed");

            Log.Info("Step 5: Click on 'Minimize' button & verify 'View Full Screen' button is displayed");
            agencyAdminTab.ClickOnMaximizeMinimizeButton(minimizeButton);
            Assert.IsTrue(agencyAdminTab.IsButtonNameDisplayed(viewFullScreenButton), "Minimize button is not displayed");

            Log.Info("Step 6: Click on 'FirstName' 'SortBy' icon & verify 'FirstName' column gets sorted in ascending order");
            var expectedDefaultFirstNameList = agencyAdminTab.GetAgencyAdminByNthColumn(2);
            agencyAdminTab.ClickOnSortByButton("First Name");
            var actualFirstNameSortedInAscendingList = agencyAdminTab.GetAgencyAdminByNthColumn(2);
            var expectedFirstNameSortedInAscendingList = actualFirstNameSortedInAscendingList.OrderBy(n => n).ToList(); agencyAdminTab.GetAgencyAdminByNthColumn(2); agencyAdminTab.GetAgencyAdminByNthColumn(2);
            CollectionAssert.AreEqual(expectedFirstNameSortedInAscendingList.ToList(), actualFirstNameSortedInAscendingList.ToList(), "First Name list is not sorted in ascending order");

            Log.Info("Step 7: Click again on 'FirstName' 'SortBy' icon & verify 'FirstName' column gets sorted in descending order");
            agencyAdminTab.ClickOnSortByButton("First Name");
            var actualFirstNameSortedInDescendingList = agencyAdminTab.GetAgencyAdminByNthColumn(2);
            var expectedFirstNameSortedInDescendingList = actualFirstNameSortedInDescendingList.OrderByDescending(n => n).ToList();
            CollectionAssert.AreEqual(expectedFirstNameSortedInDescendingList.ToList(), actualFirstNameSortedInDescendingList.ToList(), "First Name list is not sorted in descending order");

            Log.Info("Step 8: Click again on 'FirstName' 'SortBy' icon & verify 'FirstName' column gets sorted in default order");
            agencyAdminTab.ClickOnSortByButton("First Name");
            var actualDefaultFirstNameList = agencyAdminTab.GetAgencyAdminByNthColumn(2);
            CollectionAssert.AreEqual(expectedDefaultFirstNameList.ToList(), actualDefaultFirstNameList.ToList(), "First Name list is not sorted in default order");

            Log.Info("Step 9: Click on 'Last Name' 'SortBy' icon & verify 'Last Name' column gets sorted in ascending order");
            var expectedDefaultLastNameList = agencyAdminTab.GetAgencyAdminByNthColumn(3);
            agencyAdminTab.ClickOnSortByButton("Last Name");
            var actualLastNameSortedInAscendingList = agencyAdminTab.GetAgencyAdminByNthColumn(3);
            var expectedLastNameSortedInAscendingList = actualLastNameSortedInAscendingList.OrderBy(n => n).ToList();
            CollectionAssert.AreEqual(expectedLastNameSortedInAscendingList.ToList(), actualLastNameSortedInAscendingList.ToList(), "Last Name list is not sorted in ascending order");

            Log.Info("Step 10: Click again on 'Last Name' 'SortBy' icon & verify 'Last Name' column gets sorted in descending order");
            agencyAdminTab.ClickOnSortByButton("Last Name");
            var actualLastNameSortedInDescendingList = agencyAdminTab.GetAgencyAdminByNthColumn(3);
            var expectedLastNameSortedInDescendingList = actualLastNameSortedInDescendingList.OrderByDescending(n => n).ToList();
            CollectionAssert.AreEqual(expectedLastNameSortedInDescendingList.ToList(), actualLastNameSortedInDescendingList.ToList(), "Last Name list is not sorted in descending order");

            Log.Info("Step 11: Click again on 'Last Name' 'SortBy' icon & verify 'Last Name' column gets sorted in default order");
            agencyAdminTab.ClickOnSortByButton("Last Name");
            var actualDefaultLastNameList = agencyAdminTab.GetAgencyAdminByNthColumn(3);
            CollectionAssert.AreEqual(expectedDefaultLastNameList.ToList(), actualDefaultLastNameList.ToList(), "Last Name list is not sorted in default order");

            Log.Info("Step 12: Click on 'Email' 'SortBy' icon & verify 'Email' column gets sorted in ascending order");
            var expectedDefaultEmailList = agencyAdminTab.GetAgencyAdminByNthColumn(4);
            agencyAdminTab.ClickOnSortByButton("Email");
            var actualEmailSortedInAscendingList = agencyAdminTab.GetAgencyAdminByNthColumn(4);
            var expectedEmailSortedInAscendingList = actualEmailSortedInAscendingList.OrderBy(n => n).ToList();
            CollectionAssert.AreEqual(expectedEmailSortedInAscendingList.ToList(), actualEmailSortedInAscendingList.ToList(), "Email list is not sorted in ascending order");

            Log.Info("Step 13: Click again on 'Email' 'SortBy' icon & verify 'Email' column gets sorted in descending order");
            agencyAdminTab.ClickOnSortByButton("Email");
            var actualEmailSortedInDescendingList = agencyAdminTab.GetAgencyAdminByNthColumn(4);
            var expectedEmailSortedInDescendingList = actualEmailSortedInDescendingList.OrderByDescending(n => n).ToList();
            CollectionAssert.AreEqual(expectedEmailSortedInDescendingList.ToList(), actualEmailSortedInDescendingList.ToList(), "Email list is not sorted in descending order");

            Log.Info("Step 14: Click again on 'Email' 'SortBy' icon & verify 'Email' column gets sorted in default order");
            agencyAdminTab.ClickOnSortByButton("Email");
            var actualDefaultEmailList = agencyAdminTab.GetAgencyAdminByNthColumn(4);
            CollectionAssert.AreEqual(expectedDefaultEmailList.ToList(), actualDefaultEmailList.ToList(), "Email list is not sorted in default order");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("SystemAdmin")]
        public void SystemAdmin_VerifyThatAgencyDropDownWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var adminDashboard = new AdminDashboardPo(Driver);
            var fileUtil = new FileUtil();

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Click on 'Log In' button, Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 3: Navigate to 'Recruiters' list page , Click on 'Agency' dropdown and verify dropdown value");
            adminDashboard.NavigateToPage();
            adminDashboard.SelectDropDownValue(AgencyName);
            Assert.AreEqual(AgencyName, adminDashboard.GetSelectedDropDownValue(), "'Agency' dropdown value is not matched");

            Log.Info("Step 4: Delete existing file from folder, click on 'Download All Review Links' button and verify excel file is downloaded successfully");
            const string filename = "Recruiter Ratings And Reviews Links.csv";
            var downloadPath = fileUtil.GetDownloadPath();
            fileUtil.DeleteFileInFolder(downloadPath, filename, ".csv");
            const string downloadReviewLinkButton = "Download All Review Links";
            adminDashboard.ClickOnButton(downloadReviewLinkButton);
            Assert.IsTrue(fileUtil.DoesFileExistInFolder(downloadPath, filename, ".csv", 15), $"File - {filename} not found!");

            Log.Info("Step 5: Get details from file and verify selected agency data is present in file");
            var columnName = CsvUtil.GetCsvData(downloadPath, "/" + filename);
            var csvFileData = columnName.AsEnumerable().Select(s => s.Field<string>("Review Url")).ToArray();
            var agencyName = AgencyName.ToLowerInvariant().Replace(" ", "-");
            var url = "^https://www.fusionmarketplace.com/agencies/" + agencyName + "/([a-zA-Z0-9]+(-[a-zA-Z0-9]+)+)/rate-review$";
            var regEx = new Regex(url);

            foreach (var csvData in csvFileData)
            {
                Assert.IsTrue(regEx.IsMatch(csvData), "Agency data is not present");
            }
        }
    }
}
