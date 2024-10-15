using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Agencies;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Home;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.PageObjects.YopMail;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.JobDetails
{
    [TestClass]
    [TestCategory("Jobs"), TestCategory("FMP")]
    public class JobsDetailsTests2 : FmpBaseTest
    {
        private readonly string OpenJobUrl = GetJobUrlByStatus(Env, "openJobs");
        private static readonly Login NewUserLogin = GetLoginUsersByType("LoginTests");

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatApplyForThisJobPopupCancelButtonWorksSuccessfully()
        {
            var homepage = new HomePagePo(Driver);
            var jobsDetails = new JobsDetailsPo(Driver);
            var quickApply = new QuickApplyFormPo(Driver);
            var searchPo = new PageObjects.FMP.Jobs.SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on first job card");
            searchPo.NavigateToPage();
            searchPo.ClickOnJobCard();
            homepage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 3: Click on 'Apply for this job' button");
            jobsDetails.ClickOnApplyForThisJobButton();

            Log.Info("Step 4: Click on 'Cancel' button & verify popup gets close");
            if (!quickApply.IsSendNowButtonDisplayed()) return;
            jobsDetails.ClickOnCancelButton();
            Assert.IsFalse(jobsDetails.IsCancelButtonDisplayed(), "The 'Apply for this job' popup is still open");

        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatEmailJobsButtonWorksSuccessfully()
        {
            var homepage = new HomePagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var jobsDetails = new JobsDetailsPo(Driver);
            var email = new EmailPo(Driver);
            var fmpHeader = new HeaderPo(Driver);
            var emailJobs = new EmailBodyPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();
            fmpHeader.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            homepage.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Job detail page, get job detail page url & get job details ");
            var jobsDetailUrl = FusionMarketPlaceUrl + "jobs/" + OpenJobUrl;
            Driver.NavigateTo(jobsDetailUrl);
            homepage.WaitUntilFmpPageLoadingIndicatorInvisible();
            var expectedJobTitle = jobsDetails.GetJobTitle();
            var jobLocation = jobsDetails.GetJobLocation();
            var jobDetailsFromJobsDetailsPage = jobsDetails.GetJobDetailsFromDetailPage();

            Log.Info("Step 5: Click on 'Email Job' button");
            jobsDetails.ClickOnEmailJobButton();

            Log.Info("Step 6: Enter 'Email job details', click on 'Send Email' button & verify 'Successfully shared job' popup gets open");
            const string includeMessage = "Hey, Please check out this new job";
            jobsDetails.EnterEmailJobDetailsPopUp(NewUserLogin.Email, includeMessage);
            jobsDetails.WaitUntilFmpPageLoadingIndicatorInvisible();

            const string expectedSharedThisJobDetailsHeaderText = "Woohoo!";
            var actualSharedThisJobDetailsHeaderText = jobsDetails.GetSharedThisJobDetailsHeaderText();
            Assert.AreEqual(expectedSharedThisJobDetailsHeaderText, actualSharedThisJobDetailsHeaderText, "The shared this job details header text is not matched");

            var jobName = jobsDetails.GetJobNameFromSuccessPopUp();
            var expectedSharedThisJobDetailsMessageText = "You have successfully shared job details for " + jobName + ".";
            var actualSharedThisJobDetailsMessageText = jobsDetails.GetSharedThisJobDetailsMessageText();
            Assert.AreEqual(expectedSharedThisJobDetailsMessageText, actualSharedThisJobDetailsMessageText, "The shared this job details message text doesn't match");

            Log.Info("Step 7: Open 'YopMail', Open your 'Job for you' subject name");
            new WaitHelpers(Driver).HardWait(10000); // Waiting for 30 seconds for registration email
            Driver.NavigateTo(new GlobalConstants().YopMailInbox);
            email.EnterEmailAddress(NewUserLogin.Email);

            var subjectName = $"Test has a job for you in {jobLocation}";
            emailJobs.OpenEmail(subjectName);

            Log.Info("Step 8: Verify Job Details from JobDetails page and from Email are equal");
            var jobDetailsFromEmailPage = emailJobs.GetJobDetailsFromEmailPage();
            Assert.AreEqual(jobDetailsFromJobsDetailsPage.Id.Split("Job ").Last(), jobDetailsFromEmailPage.Id, "The 'Job Id' doesn't match");
            Assert.AreEqual(jobDetailsFromJobsDetailsPage.Facility.Split("Facility: ").Last().ToLower().RemoveWhitespace(), jobDetailsFromEmailPage.Facility.Trim('.').ToLower().RemoveWhitespace(), "The 'Facility' doesn't match");
            Assert.AreEqual(jobDetailsFromJobsDetailsPage.FacilityType, jobDetailsFromEmailPage.FacilityType, "The 'Facility Type' doesn't match");
            Assert.AreEqual(jobDetailsFromJobsDetailsPage.NumberOfBeds, jobDetailsFromEmailPage.NumberOfBeds.Split(" beds").First(), "The 'Number of Beds' doesn't match");
            Assert.AreEqual(jobDetailsFromJobsDetailsPage.Shift, jobDetailsFromEmailPage.Shift, "The 'Shift' doesn't match");
            Assert.AreEqual(jobDetailsFromJobsDetailsPage.AssignmentLength, jobDetailsFromEmailPage.AssignmentLength.Split(" weeks").First(), "The 'Assignment Length' doesn't match");
            Assert.AreEqual(jobDetailsFromJobsDetailsPage.Quality, jobDetailsFromEmailPage.Quality, "The 'Job Quality' doesn't match");
            Assert.AreEqual(jobDetailsFromJobsDetailsPage.Type, jobDetailsFromEmailPage.Type, "The 'Type' doesn't match");

            Log.Info("Step 9: Click on 'View this Job on Marketplace' button/link & verify Job details page gets open");
            const string viewThisJobOnMarketplaceLinkOrButton = "View this Job on Marketplace";
            emailJobs.ClickOnButtonOrLink(viewThisJobOnMarketplaceLinkOrButton);
            jobsDetails.WaitUntilFmpPageLoadingIndicatorInvisible();
            var expectedJobDetailPageUrl = FusionMarketPlaceUrl + "jobs/" + OpenJobUrl;
            Assert.AreEqual(expectedJobDetailPageUrl, jobsDetailUrl, "The job details page url doesn't match");
            var actualJobTitle = jobsDetails.GetJobTitle();
            Assert.AreEqual(expectedJobTitle, actualJobTitle, $"The job title {actualJobTitle} doesn't match");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Email_Job_VerifyThatEmailJobPopUpCancelButtonWorksSuccessfully()
        {
            var homepage = new HomePagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var jobsDetails = new JobsDetailsPo(Driver);
            var fmpHeader = new HeaderPo(Driver);
            var searchPo = new PageObjects.FMP.Jobs.SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();
            fmpHeader.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            homepage.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on first job card");
            searchPo.NavigateToPage();
            searchPo.ClickOnJobCard();
            homepage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 5: Click on 'Email Job' button");
            jobsDetails.ClickOnEmailJobButton();

            Log.Info("Step 6: Click on Email Job popup 'Cancel' button & verify popup gets close");
            jobsDetails.ClickOnEmailJobCancelButton();
            Assert.IsFalse(jobsDetails.IsSendEmailButtonDisplayed(), "The 'Send Email' button is still displayed");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Email_Job_VerifyThatSuccessfullySharedJobPopUpCloseIconAndCloseButtonWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var jobsDetails = new JobsDetailsPo(Driver);
            var searchPo = new PageObjects.FMP.Jobs.SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on first job card");
            searchPo.NavigateToPage();
            searchPo.ClickOnJobCard();

            Log.Info("Step 5: Click on 'Email Job' button");
            jobsDetails.ClickOnEmailJobButton();

            Log.Info("Step 6: Enter 'Email job details', click on 'Send Email' button");
            const string includeMessage = "Hey, Please check out this new job";
            jobsDetails.EnterEmailJobDetailsPopUp(NewUserLogin.Email, includeMessage);
            jobsDetails.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 7: Click on popup 'Close' icon & verify 'Successfully shared job' popup gets close");
            jobsDetails.ClickOnSuccessfullySharedJobCloseIcon();
            Assert.IsFalse(jobsDetails.IsSuccessfullySharedJobPopUpHeaderTextDisplayed(), "The successfully shared job popup header text is still displayed");

            Log.Info("Step 8: Click on 'Email Job' button");
            jobsDetails.ClickOnEmailJobButton();

            Log.Info("Step 9: Enter 'Email job details', click on 'Send Email' button");
            const string includeMessageText = "Hey, Please check out this new job";
            jobsDetails.EnterEmailJobDetailsPopUp(NewUserLogin.Email, includeMessageText);
            jobsDetails.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 7: Click on popup 'Close' icon & verify 'Successfully shared job' popup gets close");
            jobsDetails.ClickOnSuccessfullySharedJobCloseButton();
            Assert.IsFalse(jobsDetails.IsSuccessfullySharedJobPopUpHeaderTextDisplayed(), "The successfully shared job popup header text is still displayed");

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatAgencyLinkOpensAgencyPageSuccessfully()
        {
            var jobsDetails = new JobsDetailsPo(Driver);
            var homePage = new HomePagePo(Driver);
            var agencies = new AgenciesPo(Driver);
            var searchPo = new PageObjects.FMP.Jobs.SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            searchPo.NavigateToPage();

            Log.Info("Step 2: Click on first job card & verify 'Agency' name");
            searchPo.WaitUntilFirstJobCardTitleGetDisplayed();
            var expectedAgencyName = searchPo.GetJobCardAgencyName();
            searchPo.ClickOnJobCard();
            homePage.WaitUntilFmpPageLoadingIndicatorInvisible();
            var actualAgencyName = jobsDetails.GetAgencyName();
            Assert.AreEqual(expectedAgencyName, actualAgencyName, "Agency name is not matched");

            Log.Info("Step 3: Get 'Agency' page url and verify 'Agency' url");
            jobsDetails.ClickOnAgencyLink();
            jobsDetails.WaitUntilFmpPageLoadingIndicatorInvisible();
            var agency = GetAgencyByName(expectedAgencyName);
            var expectedAgencyUrl = FusionMarketPlaceUrl + "agencies/" + agency.AliasName + "/";
            var actualAgencyUrl = Driver.GetCurrentUrl();
            Assert.AreEqual(expectedAgencyUrl, actualAgencyUrl, "'Agency' url is not matched");

            Log.Info("Step 4: Click on 'Agency' link & verify 'Agency' name");
            var actualAgencyTitle = agencies.GetAgencyTitleText();
            Assert.AreEqual(expectedAgencyName, actualAgencyTitle, "Agency name is not matched");
        }
    }
}