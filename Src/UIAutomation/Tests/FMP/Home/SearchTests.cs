﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Home
{
    [TestClass]
    [TestCategory("Search"), TestCategory("FMP")]
    public class SearchTests : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyJobSearchFunctionalityWorkSuccessfully()
        {
            var searchJobs = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Enter job name in search bar and hit enter ");
            const string searchJobName = "CNA";
            searchJobs.EnterJobsToSearchBoxAndHitEnter(searchJobName);

            Log.Info("Step 3: Select 'View all CNA jobs' option from the dropdown and verify 'Search Jobs' page gets open");
            searchJobs.ClickOnJobsFromSearchResult();
            searchJobs.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(searchJobs.IsSelectedJobCardFilterPresent(searchJobName), "The selected 'Job Card' filter is not present on search result page");

            Log.Info($"Step 4: Verify that all the results are returned having name = {searchJobName}");
            var jobList = searchJobs.GetJobTitle();
            foreach (var jobName in jobList)
            {
                Assert.IsTrue(jobName.Contains(searchJobName), "Job Title is not matched");
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatSearchBarCancelButtonWorkSuccessfully()
        {
            var searchJobs = new SearchPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Search Bar' input box & verify search result popup gets open");
            searchJobs.ClickOnSearchBarInputBox();
            Assert.IsTrue(searchJobs.IsSearchResultPopupPresent(), "The 'Search Result' popup is not present");

            Log.Info("Step 3: Click on 'Cancel' button & verify search result popup gets close");
            searchJobs.ClickOnSearchBarCancelButton();
            Assert.IsFalse(searchJobs.IsSearchResultPopupPresent(), "The 'Search Result' popup is present");
        }
    }
}
