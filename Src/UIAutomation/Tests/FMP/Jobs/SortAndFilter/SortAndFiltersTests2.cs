using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Jobs;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.Home;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.SortAndFilter
{
    [TestClass]
    [TestCategory("Jobs"), TestCategory("FMP"), TestCategory("MobileReady")]
    public class SortAndFiltersTests2 : FmpBaseTest
    {
        public static readonly DataObjects.FMP.Jobs.SortAndFilter SortAndFilterData = SortAndFilterDataFactory.SortAndFilterDetails();
        private readonly string SortByOption = SortAndFilterData.SortBy.First();
        private readonly string CategoryOption = SortAndFilterData.Category.First();
        private readonly string SpecialtyOption = SortAndFilterData.Specialty.First();
        private readonly string ShiftOption = SortAndFilterData.Shift;
        private readonly string JobTypeOption = SortAndFilterData.JobType;
        private readonly List<string> FacilityTypeOption = SortAndFilterData.FacilityType;
        private readonly string RegionOption = SortAndFilterData.Region;

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void SortAndFilter_VerifyResetAllWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var categoryOption = SortAndFilterData.Category.Last();
            var specialtyOption = SortAndFilterData.Specialty.Last();

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button ");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.WaitUntilJobCardVisible();
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info($"Step 3: Click on 'Sort By' option & select '{SortByOption}' option & click on back button");
            const string sortByOption = "Sort By";
            sortAndFilter.ClickOnSortAndFilterOption(sortByOption);
            sortAndFilter.SelectFilterMenuSubOption(SortByOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info($"Step 4: Click on 'Category' option & select '{categoryOption}' option & click on back button");
            const string filterOption = "Category";
            sortAndFilter.ClickOnSortAndFilterOption(filterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(categoryOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info($"Step 5: Click on 'Specialty' option, select '{specialtyOption}' & click on back button");
            const string specialtyFilterOption = "Specialty";
            sortAndFilter.ClickOnSortAndFilterOption(specialtyFilterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(specialtyOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info($"Step 6: Click on 'Shift' option, select '{ShiftOption}' & click on back button");
            const string shiftFilterOption = "Shift";
            sortAndFilter.ClickOnSortAndFilterOption(shiftFilterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(ShiftOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info("Step 7: Click on 'Weekly estimated pay' option, and Enter minimum and maximum salary & click on back button");
            const string estimatedPay = "Estimated Weekly Pay";
            sortAndFilter.ClickOnSortAndFilterOption(estimatedPay);

            const string minValue = "Min";
            const string maxValue = "Max";
            sortAndFilter.EnterEstimatedWeeklyMinAndMaxPay(minValue, SortAndFilterData.MinSalary, maxValue, SortAndFilterData.MaxSalary);
            sortAndFilter.ClickOnBackButton();

            Log.Info($"Step 8: Click on 'Job Type' option , select '{JobTypeOption}' & click on back button");
            const string jobTypeOption = "Job Type";
            sortAndFilter.ClickOnSortAndFilterOption(jobTypeOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(JobTypeOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info($"Step 9: Click on 'Facility Type' option , select '{FacilityTypeOption}' & '{FacilityTypeOption}' & click on back button");
            const string facilityTypeOption = "Facility Type";
            sortAndFilter.ClickOnSortAndFilterOption(facilityTypeOption);
            sortAndFilter.SelectSubOptionCheckboxFromFilter(FacilityTypeOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info("Step 10: Click on 'Start Date' option, select date & click on back button");
            const string startDateOption = "Start Date";
            sortAndFilter.ClickOnSortAndFilterOption(startDateOption);
            sortAndFilter.EnterStartDate(SortAndFilterData.StartDate.ToString("MM/dd/yyyy"));
            sortAndFilter.ClickOnBackButton();

            Log.Info("Step 11: Click on 'Job Quantity' option, and Enter minimum and maximum job quantity & click on back button");
            const string jobQuantityFilterOption = "Job Quantity";
            sortAndFilter.ClickOnSortAndFilterOption(jobQuantityFilterOption);

            const string minText = "Min";
            const string maxText = "Max";
            var minQuantity = SortAndFilterData.MinJobQuantity;
            var maxQuantity = SortAndFilterData.MaxJobQuantity;
            sortAndFilter.EnterMinAndMaxJobQuantity(minText, minQuantity, maxText, maxQuantity);
            sortAndFilter.ClickOnBackButton();

            Log.Info($"Step 12: Click on 'Region' option, select '{RegionOption}' & click on back button");
            const string regionFilterOption = "Region";
            sortAndFilter.ClickOnSortAndFilterOption(regionFilterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(RegionOption);
            sortAndFilter.ClickOnBackButton();
            sortAndFilter.ClickOnShowAllResultsButton();

            var expectedCityOption = searchJobs.GetAllJobCardLocation();
            var stateName = expectedCityOption.First().Split(",").Last().RemoveWhitespace();
            var expectedStateOption = GlobalConstants.StateListWithAliasName[stateName];

            Log.Info($"Step 13: Click on 'State' option & select '{expectedStateOption}' & click on back button");
            searchJobs.ClickOnSortAndFilterButton();
            const string stateFilterOption = "State";
            sortAndFilter.ClickOnSortAndFilterOption(stateFilterOption);
            sortAndFilter.SelectStateOrCity(expectedStateOption);
            sortAndFilter.ClickOnBackButton();
            sortAndFilter.ClickOnShowAllResultsButton();

            Log.Info($"Step 14: Click on 'City' option, select '{expectedCityOption.First()}' option & click on back button");
            searchJobs.ClickOnSortAndFilterButton();
            const string cityFilterOption = "City";
            sortAndFilter.ClickOnSortAndFilterOption(cityFilterOption);
            sortAndFilter.WaitUntilFirstNameOfCityIsDisplayed();
            sortAndFilter.SelectStateOrCity(expectedCityOption.First());
            sortAndFilter.ClickOnBackButton();

            Log.Info("Step 15: Click on 'Reset all' button & verify selected filter count is matched");
            new WaitHelpers(Driver).HardWait(2000);
            sortAndFilter.ClickOnResetAllButton();
            const int expectedFilterSelectedOptionCount = 1;
            var actualFilterSelectedOptionCount = sortAndFilter.GetSelectedFilterOptionCount();
            Assert.AreEqual(expectedFilterSelectedOptionCount, actualFilterSelectedOptionCount, "Selected Filter Count is not matched");

            Log.Info("Step 16: Verify all filtered text is removed from header");
            Assert.IsFalse(sortAndFilter.IsSelectedJobFilterTagsDisplayed(), "The selected job filter tags are still displayed");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByShiftWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button ");
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

            Log.Info($"Step 5: Click on 'Shift' option, select '{ShiftOption}', click on back button & verify 'Shift' option is selected");
            const string shiftFilterOption = "Shift";
            sortAndFilter.ClickOnSortAndFilterOption(shiftFilterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(ShiftOption);
            sortAndFilter.ClickOnBackButton();
            var actualShiftOption = sortAndFilter.GetSelectedShiftOption();
            Assert.AreEqual(ShiftOption, actualShiftOption.FirstOrDefault(), "Shift option is not matched");

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByEstimatedWeeklyPayWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button ");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info("Step 3 : Click on 'Weekly estimated pay' option, and Enter minimum and maximum salary");
            const string estimatedPay = "Estimated Weekly Pay";
            sortAndFilter.ClickOnSortAndFilterOption(estimatedPay);

            const string minValue = "Min";
            const string maxValue = "Max";
            var minSalary = SortAndFilterData.MinSalary;
            var maxSalary = SortAndFilterData.MaxSalary;
            sortAndFilter.EnterEstimatedWeeklyMinAndMaxPay(minValue, minSalary, maxValue, maxSalary);
            sortAndFilter.ClickOnShowAllResultsButton();

            Log.Info("Step 4: Verify that selected Salary range is shown on Filter tag");
            searchJobs.WaitUntilJobCardVisible();
            searchJobs.ClickOnSortAndFilterButton();
            Assert.IsTrue(sortAndFilter.IsEstimatedWeeklyPayFilterValuePresent(minSalary, maxSalary), "Selected 'Estimated Weekly' Pay is not present on filter tag");
            sortAndFilter.ClickOnShowAllResultsButton();
            Assert.IsTrue(searchJobs.IsSalaryCardPresentOnSearchResultPage(minSalary, maxSalary), "Selected 'Estimated Weekly Pay' card is not present on search result page");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByRegionWorkSuccessfully()
        {
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button ");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info($"Step 3: Click on 'Region' option, select '{RegionOption}' & click on back button");
            const string regionFilterOption = "Region";
            sortAndFilter.ClickOnSortAndFilterOption(regionFilterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(RegionOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info("Step 4: Verify that Selected Region is shown on Filter tag & on search result page");
            var actualRegionOption = sortAndFilter.GetSelectedRegionOptionOnFilterTag();
            Assert.AreEqual(RegionOption, actualRegionOption, "The Selected 'Region' option is not present on filter tag");
            sortAndFilter.ClickOnShowAllResultsButton();
            Assert.IsTrue(sortAndFilter.IsRegionCardPresentOnSearchResultPage(RegionOption), "The selected 'Region' card filter is not present on search result page");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByZipCodeAndDistanceWorkSuccessfully()
        {
            var homepage = new HomePagePo(Driver);
            var searchJobs = new SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'View All Jobs', click on 'Sort & Filter' button ");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            homepage.WaitUntilFmpPageLoadingIndicatorInvisible();
            searchJobs.ClickOnSortAndFilterButton();

            Log.Info("Step 3: Click on 'Radius' option & enter City");
            const string radiusFilter = "Radius";
            var cityOption = SortAndFilterData.City;
            var distanceOption = SortAndFilterData.Distance;
            sortAndFilter.ClickOnSortAndFilterOption(radiusFilter);
            sortAndFilter.EnterCityName(cityOption);
            sortAndFilter.SelectCityOption(cityOption);
            sortAndFilter.SelectDistance(distanceOption);
            sortAndFilter.ClickOnBackButton();

            Log.Info("Step 4: Verify that Entered 'City' is shown on Filter tag and on search result page");
            Assert.IsTrue(sortAndFilter.IsCityAndRadiusFilterTextPresentOnFilterTag(distanceOption, cityOption), $" Selected distance {distanceOption} and City {cityOption} is not present on filter tag");
            sortAndFilter.ClickOnShowAllResultsButton();
            Assert.IsTrue(sortAndFilter.IsCityAndDistanceFilterPresentOnSearchResultPage(distanceOption, cityOption), $"Selected distance {distanceOption} and City {cityOption} card filter is not present in search result");
        }
    }
}