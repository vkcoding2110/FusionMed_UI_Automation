using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Account;
using UIAutomation.DataFactory.FMP.Jobs.JobDetails;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.DataObjects.FMP.Jobs.JobDetails;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Home;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.PageObjects.FMP.Traveler.JobApplications;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.YopMail;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.JobApplications
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class JobApplicationsTests : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("JobApplicationsTest");
        private readonly QuickApply QuickApplyData = QuickApplyDataFactory.AddQuickApplyInformation();

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatAppliedJobsDisplayedOnJobApplicationsPageSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var headerPo = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var quickApply = new QuickApplyFormPo(Driver);
            var thankYou = new ThankYouPagePo(Driver);
            var profileMenu = new ProfileMenuPo(Driver);
            var jobApplications = new JobApplicationsPo(Driver);
            var searchPo = new PageObjects.FMP.Jobs.SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            homePage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Log In' button");
            headerPo.ClickOnLogInButton();
            headerPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info($"Step 3: Click on 'Log In' button & Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on 'Quick Apply' button, Click on 'Send Now' button & click on 'Close' button");
            searchPo.NavigateToPage();
            searchPo.WaitUntilJobCardVisible();
            var expectedJobName = searchPo.GetJobCardJobTitle();
            searchPo.ClickOnFirstQuickApplyButton();
            homePage.WaitUntilFmpPageLoadingIndicatorInvisible();
            if (thankYou.IsJustMissedItPopupIsPresent()) return;
            if (thankYou.IsApplyAnywayButtonDisplayed())
            {
                thankYou.ClickOnApplyAnywayButton();
            }
            quickApply.SelectCategory(QuickApplyData.Category);
            quickApply.SelectPrimarySpecialty(QuickApplyData.PrimarySpecialty);
            quickApply.ClickOnShareProfileInformationRadioButton();
            quickApply.ClickOnSendNow();
            quickApply.WaitUntilFmpPageLoadingIndicatorInvisible();
            thankYou.ClickOnCloseButton();

            Log.Info("Step 5: Click on 'Profile' icon, Click on 'Job Applications' text & verify 'Job Application' page gets open");
            headerPo.ClickOnProfileIcon();
            profileMenu.ClickOnJobApplicationText();

            var expectedUrl = FusionMarketPlaceUrl + "job-applications/";
            Assert.AreEqual(expectedUrl, Driver.GetCurrentUrl(), $"{expectedUrl} url is not matched");

            const string expectedJobApplicationsHeaderText = "Job Applications";
            var actualJobApplicationsHeaderText = jobApplications.GetJobApplicationsHeaderText();
            Assert.AreEqual(expectedJobApplicationsHeaderText.ToLowerInvariant(), actualJobApplicationsHeaderText.ToLowerInvariant(), "Job Applications header text doesn't match");

            const string expectedJobApplicationsDescription = "You’re on your way to finding your next great job! Your recruiter should keep you regularly informed about your applications. In the meantime, keep your profile updated so you’re ready to go!";
            var actualJobApplicationsDescription = jobApplications.GetJobApplicationsDescription();
            Assert.AreEqual(expectedJobApplicationsDescription.RemoveWhitespace().ToLowerInvariant(), actualJobApplicationsDescription.RemoveWhitespace().ToLowerInvariant(), "Job Applications description doesn't match");

            Log.Info("Step 6: Verify applied job is displayed on 'Job Applications' page successfully");
            var actualJobName = jobApplications.GetAppliedJobName();
            Assert.AreEqual(expectedJobName, actualJobName, $"The applied job name doesn't match. Expected {expectedJobName} & Actual {actualJobName}");

            var expectedAppliedJobDate = "Applied on " + DateTime.UtcNow.ToString("M/d/yyyy").Replace("-", "/");
            var actualAppliedJobDate = jobApplications.GetAppliedJobDate();
            Assert.AreEqual(expectedAppliedJobDate, actualAppliedJobDate, $"The ExpectedDate : {expectedAppliedJobDate} & ActualDate : {actualAppliedJobDate} doesn't match");
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void VerifyThatBrowseAllJobsButtonWorksSuccessfullyOnJobsApplications()
        {
            var passwordPage = new PasswordPo(Driver);
            var emailList = new EmailListingGridPo(Driver);
            var email = new EmailPo(Driver);
            var confirmPage = new ConfirmPagePo(Driver);
            var searchJobs = new PageObjects.FMP.Jobs.SearchPo(Driver);
            var signUpPage = new AboutMePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var jobApplications = new JobApplicationsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log in' button & Click on 'Sign Up' link text");
            headerHomePagePo.ClickOnLogInButton();
            fmpLogin.ClickOnSignUpLink();

            Log.Info("Step 3: Sign up as a new user");
            var addAboutMeSignUpData = SignUpDataFactory.GetDataForSignUpForm();
            signUpPage.AddDataAboutMeInSignUpForm(addAboutMeSignUpData);
            passwordPage.FillFormAndSubmit(addAboutMeSignUpData);

            Log.Info("Step 4: Open 'YopMail', Open your 'Confirm Email', click on 'Confirm Email' button");
            new WaitHelpers(Driver).HardWait(10000); // Waiting for 30 seconds for registration email
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(addAboutMeSignUpData.Email);
            emailList.OpenEmail("Confirm Email");
            const string confirmEmail = "Confirm Email";
            emailList.ClickOnButtonOrLink(confirmEmail);
            confirmPage.ClickOnConfirmationLogInButton();

            Log.Info($"Step 5: Login to application with credentials - Email:{addAboutMeSignUpData.Email}, password:{addAboutMeSignUpData.Password}");
            fmpLogin.LoginToApplication(addAboutMeSignUpData);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 6: Navigate to 'Job Applications' page and  verify 'Job Applications' page gets opened");
            jobApplications.NavigateToPage();
            const string expectedJobApplicationsHeaderText = "Job Applications";
            var actualJobApplicationsHeaderText = jobApplications.GetJobApplicationsHeaderText();
            Assert.AreEqual(expectedJobApplicationsHeaderText.ToLowerInvariant(), actualJobApplicationsHeaderText.ToLowerInvariant(), "Job Applications header text doesn't match");

            const string expectedJobApplicationMessage = "Looks like you haven’t applied to any jobs yet! Once you’ve started applying, you’ll be able to keep track of all your applications here.";
            var actualJobApplicationsDescription = jobApplications.GetJobApplicationsDescription();
            Assert.AreEqual(expectedJobApplicationMessage.RemoveWhitespace(), actualJobApplicationsDescription.RemoveWhitespace(), "Job Application message is not matched");
            Assert.IsTrue(jobApplications.IsBrowseAllJobsButtonPresent(), "Browse All Jobs Button is not displayed");

            Log.Info("Step 7: Click on 'Browse All Jobs' button & verify jobs page gets opened");
            jobApplications.ClickOnBrowseAllJobsButton();
            const string expectedJobsHeaderText = "Jobs";
            var actualHeaderText = searchJobs.GetJobsPageFilterText();
            Assert.AreEqual(expectedJobsHeaderText, actualHeaderText, "Header text is not matched");

            var expectedUrl = FusionMarketPlaceUrl + "search/";
            Assert.IsTrue(Driver.IsUrlContains(expectedUrl), $"{expectedUrl} Jobs page url is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void VerifyThatJobArchivedRestoredSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var headerPo = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileMenu = new ProfileMenuPo(Driver);
            var jobApplications = new JobApplicationsPo(Driver);
            var archiveJobApplication = new ArchiveJobApplicationPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            homePage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Log In' button");
            headerPo.ClickOnLogInButton();
            headerPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info($"Step 3: Click on 'Log In' button & Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on 'Profile' icon, Click on 'Job Applications' text");
            headerPo.ClickOnProfileIcon();
            profileMenu.ClickOnJobApplicationText();

            Log.Info("Step 5: Click on 'Archive' button and Verify Archive job is displayed on 'Archived Job Applications' page successfully");
            archiveJobApplication.RestoreAllJobCards();
            var expectedJobName = jobApplications.GetAppliedJobName();
            var expectedAppliedJobDate = jobApplications.GetAppliedJobDate();
            var expectedJobAgencyName = jobApplications.GetAppliedJobAgency();
            var expectedJobLocation = jobApplications.GetAppliedJobLocation();
            jobApplications.ClickOnArchiveButton();
            jobApplications.ClickOnViewArchiveLink();
            const string expectedArchiveJobApplicationHeaderText = "Archived Job Applications";
            Assert.AreEqual(expectedArchiveJobApplicationHeaderText.ToLowerInvariant(), archiveJobApplication.GetArchivedJobApplicationHeaderTextHeaderText().ToLowerInvariant(), "Archive Job Applications header text doesn't match");

            var actualJobName = archiveJobApplication.GetAppliedJobName();
            var actualAppliedJobDate = archiveJobApplication.GetAppliedJobDate();
            var actualAgencyName = archiveJobApplication.GetAppliedJobAgency();
            var actualJobLocation = archiveJobApplication.GetAppliedJobLocation();
            Assert.AreEqual(expectedJobName, actualJobName, $"The Archive job name doesn't match. Expected {expectedJobName} & Actual {actualJobName}");
            Assert.AreEqual(expectedAppliedJobDate, actualAppliedJobDate, $"Archive job date doesn't match. ExpectedDate : {expectedAppliedJobDate} & ActualDate : {actualAppliedJobDate} doesn't match");
            Assert.AreEqual(expectedJobAgencyName, actualAgencyName, $"The Archive agency name doesn't match. Expected {expectedJobAgencyName} & Actual {actualAgencyName}");
            Assert.AreEqual(expectedJobLocation, actualJobLocation, $"The Archive job location doesn't match. Expected {expectedJobLocation} & Actual {actualJobLocation}");

            Log.Info("Step 6: Click on 'Restore' button and Verify Archive jobs 'Restore' successfully");
            archiveJobApplication.ClickOnRestoreButton();
            archiveJobApplication.ClickOnBackToJobApplicationsLink();
            var expectedJobsNameList = jobApplications.GetAppliedJobsNameList();
            Assert.IsTrue(expectedJobsNameList.Contains(actualJobName), $"{actualJobName} Archive job name is not matched");

            var expectedJobsDateList = jobApplications.GetAppliedJobsDateList();
            Assert.IsTrue(expectedJobsDateList.Contains(actualAppliedJobDate), $"{actualAppliedJobDate} Archive job date is not matched");

            var expectedJobLocationNameList = jobApplications.GetAppliedJobLocationList();
            Assert.IsTrue(expectedJobLocationNameList.Contains(actualJobLocation), $"{actualJobLocation} Archive job location is not matched");

            var expectedAgencyNameList = jobApplications.GetAppliedJobAgencyList();
            Assert.IsTrue(expectedAgencyNameList.Contains(actualAgencyName), $"{actualAgencyName} Archive job name is not matched");
        }
    }
}
