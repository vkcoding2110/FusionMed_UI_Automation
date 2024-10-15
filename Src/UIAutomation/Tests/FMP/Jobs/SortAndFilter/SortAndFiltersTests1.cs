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
    public class SortAndFiltersTests1 : FmpBaseTest
    {
        public static readonly DataObjects.FMP.Jobs.SortAndFilter SortAndFilterData = SortAndFilterDataFactory.SortAndFilterDetails();
        private readonly string CategoryOption = SortAndFilterData.Category.First();
        private readonly string SpecialtyOption = SortAndFilterData.Specialty.First();

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifySortAndFilterPopupOpenedAndClosedWorkSuccessfully()
        {
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button & verify filter popup is opened");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.WaitUntilJobCardVisible();
            searchJobs.ClickOnSortAndFilterButton();
            Assert.IsTrue(sortAndFilter.IsSortAndFilterPopupPresent(), "Sort & Filter popup is not opened");

            Log.Info("Step 3: Click on 'close' icon on filter popup & verify filter popup is closed");
            sortAndFilter.ClickOnCloseIconOnSortAndFilterPopup();
            Assert.IsFalse(sortAndFilter.IsSortAndFilterPopupOpened(), "Sort & Filter popup is opened");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByCategoryWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info("Step 2: Click on 'Category' option & verify Category menu is opened");
            const string filterOption = "Category";
            sortAndFilter.ClickOnSortAndFilterOption(filterOption);
            var actualCategoryHeaderText = sortAndFilter.GetSortAndFilterPopupHeaderText();
            Assert.AreEqual(filterOption, actualCategoryHeaderText, "Category Header text is not matched");

            Log.Info($"Step 3: Click on 'Category' option & select '{CategoryOption}' option, click on back button & verify 'Sort & Filter' menu is opened & '{CategoryOption}' option is selected");
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(CategoryOption);
            sortAndFilter.ClickOnBackButton();
            Assert.IsTrue(sortAndFilter.IsSortAndFilterPopupPresent(), "Sort & Filter popup is not opened");

            var actualSelectedCategoryOption = sortAndFilter.GetSelectedCategoryOption();
            Assert.AreEqual(CategoryOption, actualSelectedCategoryOption, "Category option is not matched");

            Log.Info($"Step 4: Click on 'Show All Results' button & verify '{CategoryOption}' option added on job list");
            sortAndFilter.ClickOnShowAllResultsButton();
            var actualSelectedJobCategoryList = sortAndFilter.GetSelectedJobCategoryList();
            Assert.IsTrue(actualSelectedJobCategoryList.Contains(CategoryOption), $"Job category - {CategoryOption} is not present on applied filter");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifySortByFilterWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var sortByOption = SortAndFilterData.SortBy.First();

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

            Log.Info("Step 5: Verify Selected Sort by option is visible on filtered tag");
            const string sortByFilterTag = "Sort: Pay High";
            Assert.IsTrue(sortAndFilter.IsSortByCardFilterPresentOnSearchResultPage(sortByFilterTag), "Selected 'Estimated Weekly Pay' card is not present on search result page");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterBySpecialtyWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info($"Step 2: Click on 'Category' option & select '{CategoryOption}' option & click on back button");
            const string filterOption = "Category";
            sortAndFilter.ClickOnSortAndFilterOption(filterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(CategoryOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info($"Step 3: Click on 'Specialty' option, select '{SpecialtyOption}' & click on 'View All Results' button");
            const string specialtyFilterOption = "Specialty";
            sortAndFilter.ClickOnSortAndFilterOption(specialtyFilterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(SpecialtyOption);
            sortAndFilter.ClickOnShowAllResultsButton();

            Log.Info("Step 4: verify selected specialty is shown on each job card header");
            searchJobs.WaitUntilJobCardVisible();
            var jobTitleList = searchJobs.GetAllJobCardTitle();
            for (var i = 1; i < jobTitleList.Count; i++)
            {
                Assert.AreEqual(jobTitleList[i], SpecialtyOption, $"Job Title doesn't have {SpecialtyOption}");
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByStateAndCityWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var shiftOption = SortAndFilterData.Shift;

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info($"Step 3: Click on 'Category' option & select '{CategoryOption}' option & click on back button");
            const string filterOption = "Category";
            sortAndFilter.ClickOnSortAndFilterOption(filterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(CategoryOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info($"Step 4: Click on 'Specialty' option, select '{SpecialtyOption}' & click on back button");
            const string specialtyFilterOption = "Specialty";
            sortAndFilter.ClickOnSortAndFilterOption(specialtyFilterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(SpecialtyOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info($"Step 5: Click on 'Shift' option, select '{shiftOption}' & click on back button");
            const string shiftFilterOption = "Shift";
            sortAndFilter.ClickOnSortAndFilterOption(shiftFilterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(shiftOption);
            sortAndFilter.ClickOnShowAllResultsButton();

            var expectedCityOption = searchJobs.GetAllJobCardLocation();
            var stateName = expectedCityOption.First().Split(",").Last().RemoveWhitespace();
            var expectedStateOption = GlobalConstants.StateListWithAliasName[stateName];

            Log.Info($"Step 6: Click on 'State' option & select '{expectedStateOption}' & click on back button");
            searchJobs.ClickOnSortAndFilterButton();
            const string stateFilterOption = "State";
            sortAndFilter.ClickOnSortAndFilterOption(stateFilterOption);
            sortAndFilter.SelectStateOrCity(expectedStateOption);
            sortAndFilter.ClickOnBackButton();
            sortAndFilter.ClickOnShowAllResultsButton();

            Log.Info($"Step 7: Click on 'City' option & select '{expectedCityOption.First()}' & click on 'Show All Results' button");
            searchJobs.ClickOnSortAndFilterButton();
            const string cityFilterOption = "City";
            new WaitHelpers(Driver).HardWait(1000);
            sortAndFilter.ClickOnSortAndFilterOption(cityFilterOption);
            sortAndFilter.WaitUntilFirstNameOfCityIsDisplayed();
            sortAndFilter.SelectStateOrCity(expectedCityOption.First());
            sortAndFilter.ClickOnShowAllResultsButton();

            Log.Info("Step 8: verify selected specialty & city is matched with each job card");
            searchJobs.WaitUntilJobCardVisible();
            var titleList = searchJobs.GetAllJobCardTitle();
            for (var i = 1; i < titleList.Count; i++)
            {
                Assert.AreEqual(titleList[i], SpecialtyOption, $"Job Title doesn't have {SpecialtyOption}");
            }

            var locationList = searchJobs.GetAllJobCardLocation();
            for (var i = 1; i < locationList.Count; i++)
            {
                Assert.AreEqual(locationList[i], expectedCityOption.First(), "City name is not matched");
            }
        }

    }
}
