using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Jobs.JobDetails;
using UIAutomation.PageObjects.FMP.Home;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.JobDetails
{
    [TestClass]
    [TestCategory("Jobs"), TestCategory("FMP")]
    public class JobsDetailsTests1 : FmpBaseTest
    {
        private readonly string ClosedJobId = GetJobUrlByStatus(Env, "closeJobs");

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyJobCardBackToResultsWorkSuccessfully()
        {
            var jobsDetails = new JobsDetailsPo(Driver);
            var homepage = new HomePagePo(Driver);
            var searchPo = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            searchPo.NavigateToPage();

            Log.Info("Step 2: Click on first job card & verify same job is opened with full details");
            searchPo.WaitUntilFirstJobCardTitleGetDisplayed();
            var expectedJobTitle = searchPo.GetJobCardJobTitle();
            searchPo.ClickOnJobCard();
            homepage.WaitUntilFmpPageLoadingIndicatorInvisible();
            var actualJobTitle = jobsDetails.GetJobTitle();
            Assert.AreEqual(expectedJobTitle, actualJobTitle, "Job title is not matched");

            Log.Info("Step 3: Click on 'Back to results' button & verify Jobs page gets opened");
            jobsDetails.ClickOnBackToResultsButton();
            jobsDetails.WaitUntilFmpPageLoadingIndicatorInvisible();
            const string expectedTitle = "Search | Fusion Marketplace";
            var actualTitle = Driver.Title;
            Assert.AreEqual(expectedTitle, actualTitle, "Title is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyApplyForThisJobWorkSuccessfully()
        {
            var homepage = new HomePagePo(Driver);
            var jobsDetails = new JobsDetailsPo(Driver);
            var quickApply = new QuickApplyFormPo(Driver);
            var thankYou = new ThankYouPagePo(Driver);
            var searchPo = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            searchPo.NavigateToPage();

            Log.Info("Step 2: Click on first job card, click on 'Apply for this job' & fill details in form & click on 'Send Now' button");
            homepage.WaitUntilFmpPageLoadingIndicatorInvisible();
            var expectedJobTitle = searchPo.GetJobCardJobTitle();
            searchPo.ClickOnJobCard();
            homepage.WaitUntilFmpPageLoadingIndicatorInvisible();
            jobsDetails.ClickOnApplyForThisJobButton();
            if (!quickApply.IsSendNowButtonDisplayed()) return;
            var userData = QuickApplyDataFactory.AddQuickApplyInformation();
            quickApply.AddQuickApplyFormData(userData);
            quickApply.ClickOnSendNow();
            quickApply.WaitUntilFmpPageLoadingIndicatorInvisible();
            Log.Info("Step 3 : Verify Thanks message is matched ");
            var expectedThanksMessage = "Thanks for applying to" + expectedJobTitle;
            var actualThanksMessage = thankYou.GetThanksMessage();
            Assert.IsTrue(actualThanksMessage.RemoveWhitespace().StartsWith(expectedThanksMessage.RemoveWhitespace()), "Recruiter's Profile Photo is not displayed");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatSocialMediaLinksWorksSuccessfully()
        {
            var jobsDetails = new JobsDetailsPo(Driver);
            var homepage = new HomePagePo(Driver);
            var searchPo = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on first job card");
            searchPo.NavigateToPage();
            searchPo.ClickOnJobCard();
            homepage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 3: Click on 'Facebook' icon & verify 'Facebook' page gets open");
            var actualFacebookHref = jobsDetails.GetFacebookIconHref();
            Assert.IsTrue(actualFacebookHref.Contains("https://www.facebook.com"), "The 'Facebook' url is not matched");

            Log.Info("Step 4: Click on 'Twitter' icon & verify 'Twitter' page gets open");
            var actualTwitterHref = jobsDetails.GetTwitterIconHref();
            Assert.IsTrue(actualTwitterHref.Contains("https://twitter.com"), "The 'Twitter' url is not matched");

            Log.Info("Step 5: Click on 'Twitter' icon & verify 'Twitter' page gets open");
            var actualLinkedInHref = jobsDetails.GetLinkedInIconHref();
            Assert.IsTrue(actualLinkedInHref.Contains("https://www.linkedin.com"), "The 'LinkedIn' url is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatWhyCantYouTellMeButtonShouldOpenPopupSuccessfully()
        {
            var homepage = new HomePagePo(Driver);
            var jobsDetails = new JobsDetailsPo(Driver);
            var searchPo = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on first job card");
            searchPo.NavigateToPage();
            searchPo.ClickOnJobCard();
            homepage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 3: Click on 'Why can't you tell me?' button & verify popup gets opened");
            jobsDetails.ClickOnTellMeButton();
            const string expectedDisclosureText = "We are contractually obligated to not disclose certain facility information digitally.";
            var actualDisclosureText = jobsDetails.GetDisclosureText();
            Assert.AreEqual(expectedDisclosureText, actualDisclosureText, "The disclosure text doesn't match");
            jobsDetails.ClickOnDisclosureOkayButton();
            Assert.IsFalse(jobsDetails.IsDisclosureTextDisplayed(), "The disclosure popup is still opened");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatDisabledApplyForThisJobButtonForClosedJobs()
        {
            var jobsDetails = new JobsDetailsPo(Driver);
            var homepage = new HomePagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Navigate to Job detail page & verify closed job message and 'Apply for this jobs' button gets disabled");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + ClosedJobId;
            Driver.NavigateTo(jobsDetailUrl);
            homepage.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsFalse(jobsDetails.IsApplyForThisJobButtonEnabled(), "The 'Apply for this job' button is not disabled");

            const string expectedJobTitleFilledText = "(Filled)";
            var actualJobTitleFilledText = jobsDetails.GetJobTitle();
            Assert.IsTrue(actualJobTitleFilledText.EndsWith(expectedJobTitleFilledText), $"The filled text is not matched  Expected : {expectedJobTitleFilledText}, Actual : {actualJobTitleFilledText}");

            var expectedClosedJobMessage = $"Uh Oh!\r\nLooks like this job has been filled.\r\nClick here to browse all our other awesome {actualJobTitleFilledText}jobs!";
            var actualClosedJobMessage = jobsDetails.GetFilledJobMessage();
            Assert.AreEqual(expectedClosedJobMessage.Replace("(" + "Filled)", "").RemoveWhitespace(), actualClosedJobMessage.RemoveWhitespace(), "The closed job message is not matched");
        }
    }
}
