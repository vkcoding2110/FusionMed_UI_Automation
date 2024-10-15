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
    public class RecruiterDashboardTests1 : FmpBaseTest
    {
        private const int RowIndex = 1;
        private const string ExpectedInnerWrapperPopUpHeaderText = "Are you sure?";
        public string AccountNavText = "Admin Dashboard";
        private readonly Recruiter AddRecruitersDetail = RecruiterDataFactory.AddRecruitersDetails();
        private static readonly Login RecruiterEmail = GetLoginUsersByType("AutomationRecruiter2Star");

        [DataTestMethod]
        [DataRow(FmpConstants.AgencyAdmin)]
        [DataRow(FmpConstants.SystemAdmin)]
        [TestCategory("Smoke"), TestCategory("SystemAdmin"), TestCategory("AgencyAdmin")]
        public void RecruiterDashboard_VerifyThatAddUpdateAndDeleteRecruiterWorksSuccessfully(string userType)
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileMenu = new ProfileMenuPo(Driver);
            var recruiterTab = new RecruitersListPo(Driver);
            var inviteRecruiterPo = new InviteRecruiterPo(Driver);
            var userAdmin = GetLoginUsersByType(userType);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{userAdmin.Email}, password:{userAdmin.Password}");
            fmpLogin.LoginToApplication(userAdmin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on Agency admin profile badge");
            fmpHeader.ClickOnLogInBadge();
            switch (userAdmin.Type)
            {
                case FmpConstants.AgencyAdmin:
                    Log.Info("Step 5: Click on 'Admin' navigation item button & verify 'Recruiters' tab gets opened");
                    profileMenu.ClickOnAccountNavMenuItem(AccountNavText);
                    const string expectedRecruiterHeaderText = "Recruiters";
                    var actualRecruiterHeaderText = recruiterTab.GetRecruiterHeaderText();
                    Assert.AreEqual(expectedRecruiterHeaderText, actualRecruiterHeaderText, "Header text is not matched");
                    break;

                case FmpConstants.SystemAdmin:
                    Log.Info("Step 5: Click on 'Admin' navigation item button & verify 'Recruiters' tab gets opened");
                    profileMenu.ClickOnAccountNavMenuItem(AccountNavText);
                    const string expectedHeaderText = "Users";
                    var actualHeaderText = recruiterTab.GetRecruiterHeaderText();
                    Assert.AreEqual(expectedHeaderText, actualHeaderText, "Header text is not matched");
                    break;
            }

            Log.Info("Step 6: Click on 'Invite Recruiter' button, Verify 'Invite recruiter' popup is open");
            recruiterTab.ClickOnInviteRecruiterButton();
            var expectedInviteRecruiterHeaderText = "Invite Recruiter";
            var actualInviteRecruiterHeaderText = inviteRecruiterPo.GetInviteRecruiterPopUpHeaderText();
            Assert.AreEqual(expectedInviteRecruiterHeaderText, actualInviteRecruiterHeaderText, "Invite recruiter popup header text is not matched");

            Log.Info("Step 7: Add Recruiter details and verify recruiter is created");
            inviteRecruiterPo.EnterRecruitersDetails(AddRecruitersDetail);
            var recruiterEmail = AddRecruitersDetail.Email;
            recruiterTab.SearchRecruitersByFilter("Email", recruiterEmail);
            var actualRecruiterDetails = recruiterTab.GetRecruiterDetails(RowIndex);
            Assert.AreEqual(AddRecruitersDetail.FirstName + AddRecruitersDetail.LastName, (actualRecruiterDetails.FirstName + actualRecruiterDetails.LastName).RemoveWhitespace(), "Recruiter 'Name' is not matched");
            Assert.AreEqual(AddRecruitersDetail.Email, actualRecruiterDetails.Email, "Recruiter 'Email' is not matched");
            CollectionAssert.AreEqual(AddRecruitersDetail.Department, actualRecruiterDetails.Department, "Department is not matched");
            Assert.IsTrue(recruiterTab.IsRecruiterProfileImagePresent(), "Recruiter image is not present");

            Log.Info("Step 8: Click on 'Actions' button, click on 'Edit' button,Verify 'Update Recruiter' popup gets opened");
            recruiterTab.ClickOnActionsButton(RowIndex);
            recruiterTab.ClickOnEditButton();
            var updatedInviteRecruiterHeaderText = "Update Recruiter";
            var actualUpdatedInviteRecruiterHeaderText = inviteRecruiterPo.GetInviteRecruiterPopUpHeaderText();
            Assert.AreEqual(updatedInviteRecruiterHeaderText, actualUpdatedInviteRecruiterHeaderText, "Update recruiter popup header text is not matched");

            Log.Info("Step 9: Click On 'Keep editing' button, Edit recruiter details & verify recruiter details updated successfully");
            var editRecruitersDetail = RecruiterDataFactory.EditRecruitersDetails();
            inviteRecruiterPo.EnterRecruitersDetails(editRecruitersDetail);
            var actualEditedRecruiterDetails = recruiterTab.GetRecruiterDetails(RowIndex);
            Assert.AreEqual(editRecruitersDetail.FirstName + editRecruitersDetail.LastName, (actualEditedRecruiterDetails.FirstName + actualEditedRecruiterDetails.LastName).RemoveWhitespace(), "Recruiter 'Name' is not edited");
            CollectionAssert.AreEqual(editRecruitersDetail.Department.OrderBy(n => n).ToList(), actualEditedRecruiterDetails.Department.OrderBy(n => n).ToList(), "Department is not edited");

            Log.Info("Step 10: Click on 'Actions' button, click on 'Delete' button & verify 'Delete Recruiter' popup gets opened");
            recruiterTab.ClickOnActionsButton(RowIndex);
            recruiterTab.ClickOnDeleteButton();
            Assert.AreEqual(ExpectedInnerWrapperPopUpHeaderText, recruiterTab.GetInnerWrapperPopUpHeaderText(), "Cancel edit popup header text is not matched");
            const string expectedDeleteRecruiterPopUpMessage = "Are you sure you want to permanently delete this recruiter?";
            Assert.AreEqual(expectedDeleteRecruiterPopUpMessage, recruiterTab.GetInnerWrapperPopUpMessage(), "Cancel edit popup message is not matched");

            Log.Info("Step 11: Click on delete recruiter 'Cancel' button & verify popup gets closed");
            recruiterTab.ClickOnInnerWrapperCancelButton();
            Assert.IsFalse(recruiterTab.IsInnerWrapperPopUpHeaderTextDisplayed(), "The Cancel edit popup is still open");

            Log.Info("Step 12: Click on 'Actions' button, click on 'Delete' button, click on 'Delete Recruiter' button & verify recruiter gets removed");
            recruiterTab.ClickOnDeleteRecruiterButton(RowIndex);
            recruiterTab.ClickOnClearFiltersButton();
            var recruiterEditedEmail = AddRecruitersDetail.Email;
            recruiterTab.SearchRecruitersByFilter("Email", recruiterEditedEmail);
            Assert.IsFalse(recruiterTab.IsRecruiterDisplayed(RowIndex, 3), "Recruiter is not deleted");
        }

        [DataTestMethod]
        [DataRow(FmpConstants.AgencyAdmin)]
        [DataRow(FmpConstants.SystemAdmin)]
        [TestCategory("SystemAdmin"), TestCategory("AgencyAdmin")]
        public void RecruiterDashboard_VerifyValidationMessageIsDisplayedOnMandatoryFields(string userType)
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileMenu = new ProfileMenuPo(Driver);
            var recruiterTab = new RecruitersListPo(Driver);
            var inviteRecruiterPo = new InviteRecruiterPo(Driver);
            var userAdmin = GetLoginUsersByType(userType);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with credentials - Email:{userAdmin.Email}, password:{userAdmin.Password}");
            fmpHeader.ClickOnLogInButton();
            fmpLogin.LoginToApplication(userAdmin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();
            fmpHeader.ClickOnLogInBadge();

            Log.Info("Step 3: Click on 'Invite Recruiter' button, Click on 'Cancel' button & verify 'Invite recruiter' pop up gets closed");
            profileMenu.ClickOnAccountNavMenuItem(AccountNavText);
            recruiterTab.ClickOnInviteRecruiterButton();
            inviteRecruiterPo.ClickOnCancelButton();
            Assert.IsFalse(inviteRecruiterPo.IsInviteRecruiterPopUpDisplayed(), "The 'Invite Recruiter' row is still displayed");

            Log.Info("Step 4: Click on 'Invite Recruiter' button, Click on 'Close' icon & verify 'Invite recruiter' pop up gets closed");
            recruiterTab.ClickOnInviteRecruiterButton();
            inviteRecruiterPo.ClickOnCloseIcon();
            Assert.IsFalse(inviteRecruiterPo.IsInviteRecruiterPopUpDisplayed(), "The 'Invite Recruiter' row is still displayed");

            Log.Info("Step 5: Click on 'Invite Recruiter' button, keep 'First name' field blank , Click on 'Send Invite' button and Verify validation message is displayed for 'First name' field");
            recruiterTab.ClickOnInviteRecruiterButton();
            inviteRecruiterPo.EnterLastName(AddRecruitersDetail.LastName);
            inviteRecruiterPo.EnterEmail(AddRecruitersDetail.Email);
            inviteRecruiterPo.ClickOnSendInviteButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, inviteRecruiterPo.GetValidationMessageOfFirstNameField(), "Validation message is not displayed for 'First name' field");

            Log.Info("Step 6: Clear 'Last name' field, Click on 'Send Invite' button and Verify validation message is displayed for 'Last name' field");
            inviteRecruiterPo.EnterFirstName(AddRecruitersDetail.FirstName);
            inviteRecruiterPo.EnterLastName("");
            inviteRecruiterPo.ClickOnSendInviteButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, inviteRecruiterPo.GetValidationMessageOfLastNameField(), "Validation message is not displayed for 'Last name' field");

            Log.Info("Step 7: Clear 'Email' field, Click on 'Send Invite' button and Verify validation message is displayed for 'Email' field");
            inviteRecruiterPo.EnterLastName(AddRecruitersDetail.LastName);
            inviteRecruiterPo.EnterEmail("");
            inviteRecruiterPo.ClickOnSendInviteButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, inviteRecruiterPo.GetValidationMessageOfEmailField(), "Validation message is not displayed for 'Email' field");
        }

        [DataTestMethod]
        [DataRow(FmpConstants.AgencyAdmin)]
        [DataRow(FmpConstants.SystemAdmin)]
        [TestCategory("Smoke"), TestCategory("SystemAdmin"), TestCategory("AgencyAdmin")]
        public void RecruiterDashboard_VerifyThatRecruitersFilterWorksSuccessfully(string userType)
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

            Log.Info("Step 4: Click on filter icon, Search recruiter by 'Name' & verify recruiters gets filtered");
            recruiterTab.SearchRecruitersByFilter("Name", AddRecruitersDetail.FirstName+" "+AddRecruitersDetail.LastName);
            var firstName = recruiterTab.GetRecruitersByNthColumn(2);
            foreach (var name in firstName)
            {
                Assert.IsTrue(name.Contains(AddRecruitersDetail.FirstName + " " + AddRecruitersDetail.LastName.First()), "Recruiter name is not matched");
            }

            Log.Info("Step 6: Click on filter icon, Search recruiter by 'Email' & verify recruiters gets filtered");
            recruiterTab.ClickOnClearFiltersButton();
            recruiterTab.SearchRecruitersByFilter("Email", RecruiterEmail.Email);
            var email = recruiterTab.GetRecruitersByNthColumn(3);
            foreach (var name in email)
            {
                Assert.IsTrue(name.Contains(RecruiterEmail.Email), "Recruiter email is not matched");
            }

            Log.Info("Step 7: Click on 'Department' filter, search department & Verify recruiter department is displayed in list");
            recruiterTab.ClickOnClearFiltersButton();
            recruiterTab.SearchRecruitersByDepartmentFilter("Department", AddRecruitersDetail.Department.First());
            var dept = recruiterTab.GetDepartmentColumnList(5);
            foreach (var values in dept.Select(name => name.Split(',').Select(s => s.Trim()).ToList()))
            {
                Assert.IsTrue(values.Contains(AddRecruitersDetail.Department.First()), "First name is not matched");
            }

            Log.Info("Step 8: Click on 'Clear Filter' button & verify filter button gets disabled");
            recruiterTab.ClickOnClearFiltersButton();
            Assert.IsFalse(recruiterTab.IsClearFilterButtonEnabled(), "Clear filter button is still enabled");
        }

        [DataTestMethod]
        [DataRow(FmpConstants.AgencyAdmin)]
        [DataRow(FmpConstants.SystemAdmin)]
        [TestCategory("SystemAdmin"), TestCategory("AgencyAdmin")]
        public void RecruiterDashboard_VerifyThatViewFullScreenAndPaginationWorksSuccessfully(string userType)
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

            Log.Info("Step 4: Click on 'View Full Screen' button & verify 'Minimize' button is displayed");
            const string viewFullScreenButton = "View Full Screen";
            const string minimizeButton = "Minimize";
            recruiterTab.ClickOnButton(viewFullScreenButton);
            Assert.IsTrue(recruiterTab.IsButtonNameDisplayed(minimizeButton), "Minimize button is not displayed");

            Log.Info("Step 5: Click on 'Minimize' button & verify 'View Full Screen' button is displayed");
            recruiterTab.ClickOnButton(minimizeButton);
            Assert.IsTrue(recruiterTab.IsButtonNameDisplayed(viewFullScreenButton), "Minimize button is not displayed");

            Log.Info("Step 6: Click on 'Last page icon' & Verify last page icon, next page icon is disabled");
            recruiterTab.ClickOnLastPageIcon();
            Assert.IsFalse(recruiterTab.IsLastPageIconDisabled(), "Last page icon is enabled");
            Assert.IsFalse(recruiterTab.IsNextPageIconDisabled(), "Next page icon is enabled");

            Log.Info("Step 7: Click on 'First page icon' & Verify first page icon, previous page icon is disabled");
            recruiterTab.ClickOnFirstPageIcon();
            Assert.IsFalse(recruiterTab.IsFirstPageIconDisabled(), "First page icon is enabled");
            Assert.IsFalse(recruiterTab.IsPreviousPageIconDisabled(), "Previous page icon is enabled");

            Log.Info("Step 8: Select row per page drop down , Verify selected row per page and total count is matched");
            const string rowPerPage = "25";
            recruiterTab.SelectRowPerPageDropDown(rowPerPage);
            var actualAgencyAdminEmail = recruiterTab.GetColumnData(3).Count;
            Assert.AreEqual(int.Parse(rowPerPage), actualAgencyAdminEmail, "Row per page is not matched");

        }

        [DataTestMethod]
        [DataRow(FmpConstants.AgencyAdmin)]
        [DataRow(FmpConstants.SystemAdmin)]
        [TestCategory("Smoke"), TestCategory("SystemAdmin"), TestCategory("AgencyAdmin")]
        public void RecruiterDashboard_VerifyThatDownloadAllReviewLinksWorksSuccessfully(string userType)
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var recruiterTab = new RecruitersListPo(Driver);
            var fileUtil = new FileUtil();
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

            Log.Info("Step 4: Delete existing file from folder, click on 'Download All Review Links' button and verify excel file is downloaded successfully");
            const string filename = "Recruiter Ratings And Reviews Links.csv";
            var downloadPath = fileUtil.GetDownloadPath();
            fileUtil.DeleteFileInFolder(downloadPath, filename, ".csv");
            const string downloadReviewLinkButton = "Download All Review Links";
            recruiterTab.ClickOnButton(downloadReviewLinkButton);
            Assert.IsTrue(fileUtil.DoesFileExistInFolder(downloadPath, filename, ".csv", 15), $"File - {filename} not found!");

            Log.Info("Step 5: Get details from file and verify selected agency data is present in file");
            var columnName = CsvUtil.GetCsvData(downloadPath, "/" + filename);
            var csvFileData = columnName.AsEnumerable().Select(s => s.Field<string>("Review Url").RemoveWhitespace()).ToArray();
            const string agencyName = "fusion-medical-staffing";
            const string url = "^https://www.fusionmarketplace.com/agencies/" + agencyName + "/([a-zA-Z0-9]+(-[a-zA-Z0-9]+)+)/rate-review$";
            var regEx = new Regex(url);

            foreach (var csvData in csvFileData)
            {
                Assert.IsTrue(regEx.IsMatch(csvData), "Agency data is not present");
            }
        }
    }
}
