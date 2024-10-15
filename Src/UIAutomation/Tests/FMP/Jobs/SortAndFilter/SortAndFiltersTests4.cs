using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Jobs;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.SortAndFilter
{
    [TestClass]
    [TestCategory("Jobs"), TestCategory("FMP")]
    public class SortAndFiltersTests4 : FmpBaseTest
    {
        public static readonly DataObjects.FMP.Jobs.SortAndFilter SortAndFilterDetail = SortAndFilterDataFactory.SortAndFilterDetails();

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifySortByFilterStartDateWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var jobsDetails = new JobsDetailsPo(Driver);
            var sortByOption = SortAndFilterDetail.SortBy.Last();

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info($"Step 3: Click on 'Sort By' option & select '{sortByOption}' option & click on back button");
            const string filterOption = "Sort By";
            sortAndFilter.ClickOnSortAndFilterOption(filterOption);
            sortAndFilter.SelectFilterMenuSubOption(sortByOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info("Step 4: Verify Selected Sort by option is visible on filtered text");
            var actualSortByOption = sortAndFilter.GetSelectedSortByOptionText();
            Assert.AreEqual(sortByOption, actualSortByOption, "Sort By option is not matched");
            sortAndFilter.ClickOnShowAllResultsButton();

            Log.Info("Step 5: Verify Selected Sort by option is visible on search result page");
            const string sortByFilterTag = "Sort: Start Date";
            Assert.IsTrue(sortAndFilter.IsSortByCardFilterPresentOnSearchResultPage(sortByFilterTag), "Selected 'Sort: Start Date' is not present on search result page");

            Log.Info("Step 6: Verify Selected Sort by option is visible on job card");
            searchJobs.WaitUntilJobCardVisible();
            for (var i = 1; i <= 5; i++)
            {
                searchJobs.ClickOnNthJobCardCard(i);
                var actualStartDate = jobsDetails.GetStartsDate();
                const string expectedStartDate = "ASAP";
                Assert.AreEqual(expectedStartDate, actualStartDate, "Start Date does not match");
                jobsDetails.ClickOnBackToResultsButton();
                jobsDetails.WaitUntilFmpPageLoadingIndicatorInvisible();
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByJobQuantityWorksSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var jobsDetails = new JobsDetailsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button ");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info("Step 3 : Click on 'Job Quantity' option, and Enter minimum and maximum job quantity");
            const string jobQuantityFilterOption = "Job Quantity";
            sortAndFilter.ClickOnSortAndFilterOption(jobQuantityFilterOption);

            const string minText = "Min";
            const string maxText = "Max";
            var minQuantity = SortAndFilterDetail.MinJobQuantity;
            var maxQuantity = SortAndFilterDetail.MaxJobQuantity;
            sortAndFilter.EnterMinAndMaxJobQuantity(minText, minQuantity, maxText, maxQuantity);
            sortAndFilter.ClickOnShowAllResultsButton();
            Log.Info("Step 4: Verify that selected min & max job quantity is shown on search result page");
            var expectedJobQuantity = minQuantity + " - " + maxQuantity;
            var actualJobQuantityOnJobSearchPage = searchJobs.GetJobQuantity(minQuantity, maxQuantity);
            Assert.AreEqual("Jobs Available: " + expectedJobQuantity, actualJobQuantityOnJobSearchPage, $"{minText} - {maxText} Job quantity doesn't match on job search page");

            Log.Info("Step 5: Open jobs cards & verify the job quantity on job details page");
            var agencyList = searchJobs.GetAllJobCardAgencyName();
            for (var i = 1; i < agencyList.Count; i++)
            {
                if (!new WaitHelpers(Driver).IsElementPresent(SearchPo.JobCardMultipleAgencyName(i), 1))
                {
                    searchJobs.ClickOnNthJobCardCard(i);
                    var actualJobQuantityOnJobDetailsPage = jobsDetails.GetJobQuantityFromJobDetails();
                    Assert.IsTrue(actualJobQuantityOnJobDetailsPage.IsWithin(minQuantity, maxQuantity), $"Job quantity is less than {minQuantity}. Url = {Driver.GetCurrentUrl()}");
                    jobsDetails.ClickOnBackToResultsButton();
                    searchJobs.WaitUntilFmpPageLoadingIndicatorInvisible();
                }
            }

            Log.Info("Step 6: Click on 'Sort & Filter' button & verify the job quantity on filter tag");
            Driver.JavaScriptScroll("0", "10");
            searchJobs.ClickOnSortAndFilterButton();
            var actualJobQuantityValueOnFilter = sortAndFilter.GetJobQuantityValueFromFilter(jobQuantityFilterOption, minQuantity, maxQuantity);
            Assert.AreEqual(expectedJobQuantity, actualJobQuantityValueOnFilter, $"{minText} - {maxText} Job quantity is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFacilityTypeWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var facilityTypeOption = SortAndFilterDetail.FacilityType;

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info($"Step 3: Click on 'Facility Type' option , select '{facilityTypeOption}' & '{facilityTypeOption}' & click on back button");
            const string filterOption = "Facility Type";
            sortAndFilter.ClickOnSortAndFilterOption(filterOption);
            sortAndFilter.SelectSubOptionCheckboxFromFilter(facilityTypeOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info("Step 4: Verify selected Facility Type on filtered popup");
            var selectedFacilityType = sortAndFilter.GetSelectedFacilityType();
            var actualFacilityType = selectedFacilityType.Split(", ");
            CollectionAssert.AreEquivalent(facilityTypeOption.ToList(), actualFacilityType.ToList(), "Facility Type list is not matched");

            Log.Info("Step 5: Verify selected Facility Type on search result page");
            sortAndFilter.ClickOnShowAllResultsButton();
            searchJobs.WaitUntilJobCardVisible();
            var actualFacilityTypeOnSearchPage = searchJobs.GetSearchPageCriteria();
            string[] resultPageJobs = { "Jobs" };
            var expectedFacilityTypeOnSearchPage = resultPageJobs.Concat(facilityTypeOption).ToList();
            CollectionAssert.AreEquivalent(expectedFacilityTypeOnSearchPage.ToList(), actualFacilityTypeOnSearchPage.ToList(), "Search Page Criteria is not matched");
        }
        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByAgencyWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var agencyOption = SortAndFilterDetail.Agency;

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info($"Step 3: Click on 'Agency' option , select '{agencyOption}' & click on back button");
            const string filterOption = "Agency";
            sortAndFilter.ClickOnSortAndFilterOption(filterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(agencyOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info("Step 4: Verify that Selected Agency is shown on Filter tag & on search result page");
            var actualAgencyOption = sortAndFilter.GetSelectedAgencyOptionFromFilter();
            Assert.AreEqual(agencyOption, actualAgencyOption, $"The Selected Agency {agencyOption} option is not present on filter tag");
            sortAndFilter.ClickOnShowAllResultsButton();
            Assert.IsTrue(sortAndFilter.IsSelectedDepartmentPresentOnSearchResultPage(agencyOption), "The selected 'Agency' card filter is not present on search result page");

            Log.Info("Step 5: Verify that Selected Agency is shown on each job card");
            var agencyList = searchJobs.GetAllJobCardAgencyName();
            for (var i = 1; i < agencyList.Count; i++)
            {
                Assert.IsTrue(agencyList[i].Equals(agencyOption) || agencyList[i].StartsWith("Multiple Agencies"), $"Job card doesn't have {agencyOption} agency");
            }
        }
    }
}
