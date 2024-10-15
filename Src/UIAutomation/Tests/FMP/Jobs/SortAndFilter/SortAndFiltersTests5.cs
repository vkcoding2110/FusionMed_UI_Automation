using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Jobs;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.SortAndFilter
{

    [TestClass]
    [TestCategory("Jobs"), TestCategory("FMP"), TestCategory("MobileReady")]
    public class SortAndFiltersTests5 : FmpBaseTest
    {
        private readonly List<string> AgencyNames = GetAgencyByNames().Select(x => x.Name).ToList();
        public static readonly DataObjects.FMP.Jobs.SortAndFilter SortAndFilterDetail = SortAndFilterDataFactory.SortAndFilterDetails();

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyAgencyTypeWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var departmentOption = SortAndFilterDetail.Department;
            var agencyOption = SortAndFilterDetail.Agency;

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button ");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info($"Step 3: Click on 'Department' option, select '{departmentOption}' and Verify Selected Department is shown on Filter tag & on search result page");
            const string filterOption = "Department";
            sortAndFilter.ClickOnSortAndFilterOption(filterOption);
            sortAndFilter.SelectDepartmentFilterMenuOption(departmentOption);
            sortAndFilter.ClickOnBackButton();
            var actualDepartmentOption = sortAndFilter.GetSelectedDepartmentOptionFromFilter();
            Assert.AreEqual(departmentOption, actualDepartmentOption, $"The Selected Department {departmentOption} option is not present on filter tag");
            Assert.IsTrue(sortAndFilter.IsSelectedDepartmentPresentOnSearchResultPage(departmentOption), "The selected department filter is not present on search result page");

            Log.Info("Step 4: Verify that all agency card is displayed");
            sortAndFilter.ClickOnShowAllResultsButton();
            var actualAgencyList = searchJobs.GetAllAgencyNameFromAgencyCard();
            actualAgencyList = actualAgencyList.Except(FmpConstants.AgencyList).ToList();
            CollectionAssert.AreEquivalent(AgencyNames.ToList(), actualAgencyList.ToList(), "Agency list is not matched");

            Log.Info("Step 5: Verify that Selected Agency is shown on agency card");
            searchJobs.ClickOnSortAndFilterButton();
            const string agencyFilterOption = "Agency";
            sortAndFilter.ClickOnSortAndFilterOption(agencyFilterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(agencyOption);
            sortAndFilter.ClickOnShowAllResultsButton();
            var selectedAgencyList = searchJobs.GetAllAgencyNameFromAgencyCard();
            Assert.AreEqual(agencyOption, selectedAgencyList.First(), $"Selected agency {agencyOption} is not displayed on agency card");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyJobsWithMultipleAgencyPayPackageWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button ");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info("Step 3: Check jobs with multiple agency check box option, Click on 'Show all results' button and Verify each job card has multiple agency with selected filter tag");
            sortAndFilter.ClickOnJobsWithMultipleAgencyCheckbox(true);
            sortAndFilter.ClickOnShowAllResultsButton();
            searchJobs.WaitUntilJobCardVisible();
            const string expectedAgencyOption = "Multiple Agencies";
            const string expectedAgencyFilterTag = "Multiple Pay Packages Only";
            var agencyList = searchJobs.GetAllJobCardAgencyName();
            for (var i = 1; i < agencyList.Count; i++)
            {
                Assert.IsTrue(agencyList[i].StartsWith("Multiple Agencies"), $"Job card doesn't have {expectedAgencyOption} agency");
            }
            Assert.IsTrue(sortAndFilter.IsSelectedDepartmentPresentOnSearchResultPage(expectedAgencyFilterTag), "The selected 'Agency' card filter is not present on search result page");

            Log.Info("Step 4: Click on 'Sort & Filter' button, Uncheck the job with multiple agency checkbox and verify all type of job card is displayed ");
            searchJobs.ClickOnSortAndFilterButton();
            sortAndFilter.ClickOnJobsWithMultipleAgencyCheckbox(false);
            Assert.IsFalse(sortAndFilter.IsSelectedDepartmentPresentOnSearchResultPage(expectedAgencyFilterTag), "The selected 'Agency' card filter is not present on search result page");
        }
    }
}
