using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter.Admin;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile.ProfileSharing;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Agencies.Recruiters.Admin
{
    [TestClass]
    [TestCategory("RateAndReview"), TestCategory("FMP")]
    public class RecruiterSharedProfileTests : FmpBaseTest
    {
        private const int RowIndex = 1;
        private static readonly Login RecruiterLogin = GetLoginUsersByType("AutomationRecruiterFiveReviews");
        private const string SharedProfileTab = "Shared Profiles";
        private static readonly List<Document> UploadDocument = DocumentsDataFactory.UploadDocumentFile();
        private static readonly IList<string> UsersList = new List<string>
        {
            "RecruiterProfileSharingTests",
            "JobApplicationsTest"
        };
        private static readonly IList<Login> TravelerUsers = UsersList.Select(GetLoginUsersByType).ToList();

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.ShareProfileWithTraveler(TravelerUsers, RecruiterLogin.Email, UploadDocument);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void VerifySharedProfileWithRecruiterWorkSuccessfully()
        {
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var adminDashboard = new AdminDashboardPo(Driver);
            var recruiterTab = new RecruitersListPo(Driver);
            var recipientSharedProfilePage = new RecipientSharedProfilePo(Driver);
            var recruiterSharedProfilePo = new RecruiterSharedProfilePo(Driver);
            var fileUtil = new FileUtil();

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 6: Login to application with Recruiter credentials - Email:{RecruiterLogin.Email}, password:{RecruiterLogin.Password}");
            headerHomePagePo.ClickOnLogInButton();
            marketPlaceLogin.LoginToApplication(RecruiterLogin);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 7: Navigate to 'Recruiters' list page ,Click on 'Shared profile' tab and Verify Shared document with recruiter is matched");
            adminDashboard.NavigateToPage();
            adminDashboard.ClickOnReviewsSubMenuTabs(SharedProfileTab);
            var actualRecruiterDetails = recruiterTab.GetRecruiterDetails(RowIndex);
            Assert.AreEqual(TravelerUsers.First().Email, actualRecruiterDetails.Email, "Recruiter 'Email' is not matched");
            Assert.AreEqual(TravelerUsers.First().Name.RemoveWhitespace(), actualRecruiterDetails.FirstName.RemoveWhitespace(), "Recruiter 'Email' is not matched");

            //Commented due to current date issue
            //var actualReviewDate = recruiterTab.GetRecruiterDateText(RowIndex);
            //Assert.AreEqual(DateTime.Now.ToString("MM/dd/yyyy"), actualReviewDate.ToString("MM/dd/yyyy").RemoveWhitespace(), "Date shared is not matched");

            Log.Info("Step 8: Delete existing file from folder, click on 'Download My Resume' button and verify Resume is downloaded successfully");
            recruiterTab.ClickOnActionsButton(RowIndex);
            var downloadPath = fileUtil.GetDownloadPath();
            var filename = TravelerUsers.First().Name + "_Resume";
            fileUtil.DeleteFileInFolder(downloadPath, filename, ".pdf");
            recruiterSharedProfilePo.ClickOnDownloadResumeButton();
            Assert.IsTrue(PlatformName != PlatformName.Web ? new MobileFileSelectionPo(Driver).IsFilePresentOnDevice(filename) : fileUtil.DoesFileExistInFolder(downloadPath, filename, ".pdf", 15), $"File - {filename} not found!");

            Log.Info("Step 9: Click on 'View document' button and Verify shared profile detail is displayed");
            recruiterTab.ClickOnActionsButton(RowIndex);
            recruiterSharedProfilePo.ClickOnViewDocumentButton();
            const int documentRowNumber = 1;
            var actualUploadedDocumentDetails = recipientSharedProfilePage.GetDocumentDetailsFromDetailPage(documentRowNumber);
            Assert.AreEqual(UploadDocument.First().FileName.ToLowerInvariant(), actualUploadedDocumentDetails.First().FileName.ToLowerInvariant(), "The 'Document Name' is not matched");
            Assert.AreEqual(UploadDocument.First().DocumentType.ToLowerInvariant(), actualUploadedDocumentDetails.First().DocumentType.ToLowerInvariant(), "The 'Document Type' is not matched");
            Assert.AreEqual(UploadDocument.First().DocumentUploadedDate.ToString("MM/dd/yyyy"), actualUploadedDocumentDetails.First().DocumentUploadedDate.ToString("MM/dd/yyyy"), "The document uploaded 'Date' is not matched");
            Assert.AreEqual(UploadDocument.First().DocumentTypeCode.ToLowerInvariant(), actualUploadedDocumentDetails.First().DocumentTypeCode.ToLowerInvariant(), "The document uploaded 'Date' is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void VerifySearchFunctionalityWorksSuccessfully()
        {
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var adminDashboard = new AdminDashboardPo(Driver);
            var recruiterSharedProfilePo = new RecruiterSharedProfilePo(Driver);
            var recruiterTab = new RecruitersListPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with Recruiter credentials - Email:{RecruiterLogin.Email}, password:{RecruiterLogin.Password}");
            headerHomePagePo.ClickOnLogInButton();
            marketPlaceLogin.LoginToApplication(RecruiterLogin);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 3: Navigate to 'Recruiters' list page, Click on 'Shared profile' tab");
            adminDashboard.NavigateToPage();
            adminDashboard.ClickOnReviewsSubMenuTabs(SharedProfileTab);

            Log.Info("Step 4: Search User by Email and verify Searched email address and name are matched");
            recruiterSharedProfilePo.EnterDataToSearchTextBox(TravelerUsers.FirstOrDefault()?.Email);
            var actualRecruiterDetails = recruiterTab.GetRecruiterDetails(RowIndex);
            Assert.AreEqual(TravelerUsers.First().Email, actualRecruiterDetails.Email, "Recruiter 'Email' is not matched");
            Assert.AreEqual(TravelerUsers.First().Name.RemoveWhitespace(), actualRecruiterDetails.FirstName.RemoveWhitespace(), "Recruiter 'Email' is not matched");

            Log.Info("Step 5: Search User by User Name and verify Searched email address and name are matched");
            recruiterSharedProfilePo.EnterDataToSearchTextBox(TravelerUsers[0].Name);
            var actualRecruiterDetails1 = recruiterTab.GetRecruiterDetails(RowIndex);
            Assert.AreEqual(TravelerUsers[0].Email, actualRecruiterDetails1.Email, "Recruiter 'Email' is not matched");
            Assert.AreEqual(TravelerUsers[0].Name.RemoveWhitespace(), actualRecruiterDetails1.FirstName.RemoveWhitespace(), "Recruiter 'Email' is not matched");
        }
    }
}
