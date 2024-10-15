using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.JobDetails
{
    [TestClass]
    [TestCategory("Jobs"), TestCategory("FMP")]
    public class JobsDetailsTests3 : FmpBaseTest
    {
        private readonly string MultipleJobId = GetJobUrlByStatus(Env, "multipleJobs");

        [TestMethod]
        public void VerifyJobWithMultipleAgencyDisplayedCorrectly()
        {
            var jobsDetails = new JobsDetailsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            jobsDetails.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Navigate to Multiple agency with pay offer card and Verify that Numbers of offers are displayed.");
            var jobsDetailUrl = FusionMarketPlaceUrl + "jobs/" + MultipleJobId;
            Driver.NavigateTo(jobsDetailUrl);
            jobsDetails.WaitUntilFmpPageLoadingIndicatorInvisible();

            var expectedTotalNumberOfAgencyCard = jobsDetails.GetAgencyCardCount().ToString();
            var actualTotalNumberOfAgencyCard = jobsDetails.GetOffersCount();
            Assert.AreEqual(expectedTotalNumberOfAgencyCard, actualTotalNumberOfAgencyCard, "Agency card count is not matched");

            Log.Info("Step:3 Verify that job pay amount and agency name is matched");
            if (jobsDetails.IsAgencyCardDisableTextPresent(1)) return;
            var expectedJobPayAmount = jobsDetails.GetPayAmountFromAgencyCard(1);
            var actualJobPayAmount = jobsDetails.GetJobPayAmount();
            Assert.AreEqual("$" + expectedJobPayAmount + " weekly gross", actualJobPayAmount, "Fusion agency Pay Amount is not matched");

            var expectedAgencyName = jobsDetails.GetAgencyNameFromAgencyCard(1);
            var actualAgencyName = jobsDetails.GetAgencyName();
            Assert.AreEqual(expectedAgencyName, actualAgencyName, "Agency Name is not matched");

            Log.Info("Step 4: Navigate to Second agency offer card and Verify that Selected offer,job pay amount and agency is matched");
            jobsDetails.ClickOnAgencyOfferCard(2);
            Assert.IsTrue(jobsDetails.IsSelectedAgencyOfferCardTextPresent(2), "Selected Offer number is not Present");

            if (jobsDetails.IsAgencyCardDisableTextPresent(2)) return;
            var expectedSecondOfferCardJobPayAmount = jobsDetails.GetPayAmountFromAgencyCard(2);
            var actualSecondOfferJobPayAmount = jobsDetails.GetJobPayAmount();
            Assert.AreEqual("$" + expectedSecondOfferCardJobPayAmount + " weekly gross", actualSecondOfferJobPayAmount, "Partner agency Pay Amount is not matched");

            var expectedSecondOfferCardAgencyName = jobsDetails.GetAgencyNameFromAgencyCard(2);
            var actualSecondOfferAgencyName = jobsDetails.GetAgencyName();
            Assert.AreEqual(expectedSecondOfferCardAgencyName, actualSecondOfferAgencyName, "Agency Name is not matched");

            Log.Info("Step 5: Navigate back to fusion agency card and verify that selected offer is displayed");
            jobsDetails.ClickOnPreviousAgencyCardButton();
            Assert.IsTrue(jobsDetails.IsSelectedAgencyOfferCardTextPresent(1), "Selected offer is not present");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyViewAllJobsOpenedSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs' button icon & verify Apply Page is opened");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            const string expectedHeaderText = "Jobs";
            var actualHeaderText = searchJobs.GetViewAllJobsPageHeaderText();
            Assert.AreEqual(expectedHeaderText, actualHeaderText, "Header text is not matched");

            var expectedUrl = FusionMarketPlaceUrl + "search/";
            Assert.IsTrue(Driver.IsUrlContains(expectedUrl), $"{expectedUrl} Jobs page url is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatJobsPagePaginationBackAndNextButtonWorkSuccessfully()
        {
            var searchJobs = new SearchPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs' link");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);

            var pageIndex = 1;
            Log.Info($"Step 3: Verify 'Back' button is disable and the default pagination is {pageIndex}");
            Assert.IsFalse(searchJobs.IsPaginationBackButtonEnabled(), "The 'Back' button is not disable");
            Assert.IsTrue(searchJobs.IsPaginationNthButtonSelected(pageIndex), $"The {pageIndex} button is Not selected");

            pageIndex = 2;
            Log.Info($"Step 4: Click on 'Next' button and verify the pagination is selected to {pageIndex}");
            searchJobs.ClickOnPaginationNextButton();
            Assert.IsTrue(searchJobs.IsPaginationNthButtonSelected(pageIndex), $"The {pageIndex} button is Not selected");
            Assert.IsFalse(searchJobs.IsPaginationNthButtonSelected(pageIndex - 1), $"The {pageIndex - 1} button is still selected");

            pageIndex = 3;
            Log.Info($"Step 5: Click on 'Nth' button and verify the pagination is selected to {pageIndex}");
            searchJobs.ClickOnPaginationNthButton(pageIndex);
            Assert.IsTrue(searchJobs.IsPaginationNthButtonSelected(pageIndex), $"The {pageIndex} button is Not selected");

            Log.Info($"Step 6: Click on 'Back' button and verify the pagination is selected back to {pageIndex - 1}");
            searchJobs.ClickOnPaginationBackButton();
            Assert.IsTrue(searchJobs.IsPaginationNthButtonSelected(pageIndex - 1), $"The {pageIndex - 1} button is Not selected");
            Assert.IsFalse(searchJobs.IsPaginationNthButtonSelected(pageIndex), $"The {pageIndex} button is still selected");

            Log.Info($"Step 7: Click on 'Next' button and verify the pagination is selected to last button");
            searchJobs.ClickOnPaginationLastButton();
            Assert.IsTrue(searchJobs.IsPaginationLastButtonSelected(), "The last button is Not selected");
            Assert.IsFalse(searchJobs.IsPaginationNextButtonEnabled(), "The 'Next' button is not disable");
        }
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyJobCardDetailsAreMatchedWithQuickApplyPopUpHeader()
        {
            var quickApplyPage = new QuickApplyFormPo(Driver);
            var searchPo = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            searchPo.NavigateToPage();
            searchPo.WaitUntilFirstJobCardTitleGetDisplayed();

            Log.Info("Step 2: Get job details from the card");
            var jobTitle = searchPo.GetJobCardJobTitle();
            var jobLocation = searchPo.GetJobLocation();
            var jobPayAmounts = searchPo.GetJobPayAmount();
            var jobHours = searchPo.GetJobHours();

            Log.Info("Step 3: Click on 'Quick apply' button and verify job detail is matched with quick apply pop-up header detail");
            searchPo.ClickOnFirstQuickApplyButton();
            searchPo.WaitUntilFmpPageLoadingIndicatorInvisible();
            if (!quickApplyPage.IsSendNowButtonDisplayed()) return;
            var actualJobTitle = quickApplyPage.GetJobName();
            var actualJobLocation = quickApplyPage.GetJobLocation();
            var actualJobPayAmount = quickApplyPage.GetJobPayAmount();
            var actualJobHours = quickApplyPage.GetJobHours();
            Assert.AreEqual(jobPayAmounts, actualJobPayAmount, "Job pay amount is not matched");
            Assert.AreEqual(jobHours, actualJobHours, "Job hours is not matched");
            Assert.AreEqual(jobTitle, actualJobTitle, "Job name is not matched");
            Assert.AreEqual(jobLocation, actualJobLocation, "Job location is not matched");
        }
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatJobsRecentlyAddedSliderPreviousAndNextButtonWorkSuccessfully()
        {
            var search = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            search.NavigateToPage();
            search.WaitUntilFirstJobCardTitleGetDisplayed();

            Log.Info("Step 2: Verify first Job card is present or not");
            const int firstIndexValue = 0;
            Assert.IsTrue(search.IsNthJobCardPresent(firstIndexValue), "The first job card is not present");

            Log.Info("Step 3: Click on 'Next' button & verify first Job card is present or not");
            search.ClickOnJobsRecentlyAddedNextButton();
            Assert.IsFalse(search.IsNthJobCardPresent(firstIndexValue), "The first job card is present");

            Log.Info("Step 4: Click on 'Previous' button & verify first Job card is present or not");
            search.ClickOnJobsRecentlyAddedPreviousButton();
            Assert.IsTrue(search.IsNthJobCardPresent(firstIndexValue), "The first job card is not present");

        }
    }
}
