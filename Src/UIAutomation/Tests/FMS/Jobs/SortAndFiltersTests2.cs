using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMS.Jobs;
using UIAutomation.PageObjects.FMS.Jobs;
using UIAutomation.DataObjects.FMS.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.Jobs
{
    [TestClass]
    [TestCategory("Jobs"), TestCategory("FMS")]
    public class SortAndFiltersTests2 : BaseTest
    {
        public static readonly SortAndFilter SortAndFilterDetail = SortAndFilterDataFactory.SortAndFilterDetails();
        private readonly string CategoryOption = SortAndFilterDetail.Category.First();
        private readonly string SpecialtyOption = SortAndFilterDetail.Specialty.First();
        private readonly string ShiftOption = SortAndFilterDetail.Shift;

        [TestMethod]
        [TestCategory("Smoke"),TestCategory("MobileReady")]
        public void SortAndFilter_VerifyResetAllWorkSuccessfully()
        {
            var viewAllJobs = new ViewAllJobsPo(Driver);

            Log.Info("Step 1: Navigate to 'Jobs' page & click on 'Sort & Filter' button");
            viewAllJobs.NavigateToPage();
            viewAllJobs.ClickOnSortAndFilterButton();

            Log.Info($"Step 2: Click on 'Category' option & select '{CategoryOption}' option & click on back button");
            const string filterOption = "Category";
            viewAllJobs.ClickOnSortAndFilterOption(filterOption);
            viewAllJobs.SelectFilterMenuSubOption(CategoryOption);
            viewAllJobs.ClickOnBackButton();

            Log.Info($"Step 3: Click on 'Specialty' option, select '{SpecialtyOption}' & click on back button");
            const string specialtyFilterOption = "Specialty";
            viewAllJobs.ClickOnSortAndFilterOption(specialtyFilterOption);
            viewAllJobs.ClickOnSpecialtyOption(SpecialtyOption);
            viewAllJobs.ClickOnBackButton();

            Log.Info($"Step 4: Click on 'Shift' option, select '{ShiftOption}' & click on back button");
            const string shiftFilterOption = "Shift";
            viewAllJobs.ClickOnSortAndFilterOption(shiftFilterOption);
            viewAllJobs.ClickOnSpecialtyOption(ShiftOption);
            viewAllJobs.ClickOnShowAllResultsButton();

            var expectedCityOption = viewAllJobs.GetAllJobCardLocation();
            var stateName = expectedCityOption.First().Split(",").Last().RemoveWhitespace();
            var expectedStateOption = GlobalConstants.StateListWithAliasName[stateName];

            Log.Info($"Step 5: Click on 'State' option & select '{expectedStateOption}' & click on back button");
            viewAllJobs.ClickOnSortAndFilterButton();
            const string stateFilterOption = "State";
            viewAllJobs.ClickOnSortAndFilterOption(stateFilterOption);
            viewAllJobs.SelectStateOrCity(expectedStateOption);
            viewAllJobs.ClickOnBackButton();
            viewAllJobs.ClickOnShowAllResultsButton();

            Log.Info($"Step 6: Click on 'City' option & select '{expectedCityOption.First()}' & click on 'Show All Results' button");
            viewAllJobs.ClickOnSortAndFilterButton();
            const string cityFilterOption = "City";
            viewAllJobs.ClickOnSortAndFilterOption(cityFilterOption);
            viewAllJobs.WaitUntilFirstNameOfCityIsDisplayed();
            viewAllJobs.SelectStateOrCity(expectedCityOption.First());
            viewAllJobs.ClickOnBackButton();

            Log.Info("Step 7: verify selected filter count is matched");
            viewAllJobs.ClickOnResetAllButton();
            const int expectedFilterSelectedOptionCount = 2;
            var actualFilterSelectedOptionCount = viewAllJobs.GetSelectedFilterOptionCount();
            Assert.AreEqual(expectedFilterSelectedOptionCount, actualFilterSelectedOptionCount, "Selected Filter Count is not matched");

            Log.Info("Step 8: Click on 'Reset All' button & verify all filtered text is removed from header");
            IList<string> expectedFilteredTextList = new[] { "Jobs" };
            var actualFilteredTextList = viewAllJobs.GetSelectedJobCategoryList();
            CollectionAssert.AreEqual(expectedFilteredTextList.ToList(), actualFilteredTextList.ToList(), "Filtered text list is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByShiftWorkSuccessfully()
        {
            var viewAllJobs = new ViewAllJobsPo(Driver);

            Log.Info("Step 1: Navigate to 'Jobs' page & click on 'Sort & Filter' button");
            viewAllJobs.NavigateToPage();
            viewAllJobs.ClickOnSortAndFilterButton();

            Log.Info($"Step 2: Click on 'Category' option & select '{CategoryOption}' option & click on back button");
            const string filterOption = "Category";
            viewAllJobs.ClickOnSortAndFilterOption(filterOption);
            viewAllJobs.SelectFilterMenuSubOption(CategoryOption);
            viewAllJobs.ClickOnBackButton();

            Log.Info($"Step 3: Click on 'Specialty' option, select '{SpecialtyOption}' & click on back button");
            const string specialtyFilterOption = "Specialty";
            viewAllJobs.ClickOnSortAndFilterOption(specialtyFilterOption);
            viewAllJobs.ClickOnSpecialtyOption(SpecialtyOption);
            viewAllJobs.ClickOnBackButton();

            Log.Info($"Step 4: Click on 'Shift' option, select '{ShiftOption}' & click on back button");
            const string shiftFilterOption = "Shift";
            viewAllJobs.ClickOnSortAndFilterOption(shiftFilterOption);
            viewAllJobs.ClickOnSpecialtyOption(ShiftOption);
            viewAllJobs.ClickOnBackButton();
            var actualShiftOption = viewAllJobs.GetSelectedShiftOption();
            Assert.AreEqual(ShiftOption, actualShiftOption, "Shift option is not matched");

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByEstimatedWeeklyPayWorkSuccessfully()
        {
            var viewAllJobs = new ViewAllJobsPo(Driver);

            Log.Info("Step 1: Navigate to 'Jobs' page & click on 'Sort & Filter' button");
            viewAllJobs.NavigateToPage();
            viewAllJobs.ClickOnSortAndFilterButton();

            Log.Info("Step 2 : Click on 'Weekly estimated pay' option, and Enter minimum and maximum salary");
            const string estimatedPay = "Estimated Weekly Pay";
            viewAllJobs.ClickOnSortAndFilterOption(estimatedPay);

            var minSalary = SortAndFilterDetail.MinSalary;
            var maxSalary = SortAndFilterDetail.MaxSalary;
            const string minValue = "Min";
            const string maxValue = "Max";
            viewAllJobs.EnterEstimatedWeeklyMinAndMaxPay(minValue, minSalary, maxValue, maxSalary);
            viewAllJobs.ClickOnBackButton();

            Log.Info("Step 3: Verify that selected Salary range is shown on Filter tag");
            Assert.IsTrue(viewAllJobs.IsEstimatedWeeklyPayFilterValuePresent(minSalary, maxSalary), "Selected 'Estimated Weekly' Pay is not present on filter tag");
            viewAllJobs.ClickOnShowAllResultsButton();
            Assert.IsTrue(viewAllJobs.IsSalaryCardPresentOnSearchResultPage(minSalary, maxSalary), "Selected 'Estimated Weekly Pay' card is not present on search result page");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByRegionWorkSuccessfully()
        {
            var viewAllJobs = new ViewAllJobsPo(Driver);
            var regionOption = SortAndFilterDetail.Region;

            Log.Info("Step 1: Navigate to 'Jobs' page & click on 'Sort & Filter' button");
            viewAllJobs.NavigateToPage();
            viewAllJobs.ClickOnSortAndFilterButton();

            Log.Info("Step 2: Click on 'Region' option, select 'Midwest' & click on back button");
            const string regionFilterOption = "Region";
            viewAllJobs.ClickOnSortAndFilterOption(regionFilterOption);
            viewAllJobs.ClickOnSpecialtyOption(regionOption);
            viewAllJobs.ClickOnBackButton();

            Log.Info("Step 3: Verify that Selected Region is shown on Filter tag & on search result page");
            Assert.IsTrue(viewAllJobs.IsSelectedRegionIsPresentOnFilterTag(regionOption), "The Selected 'Region' option is not present on filter tag");
            viewAllJobs.ClickOnShowAllResultsButton();
            Assert.IsTrue(viewAllJobs.IsRegionCardPresentOnSearchResultPage(regionOption), "The selected 'Region' card filter is not present on search result page");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByZipCodeAndDistanceWorkSuccessfully()
        {
            var viewAllJobs = new ViewAllJobsPo(Driver);
            var zipCode = SortAndFilterDetail.ZipCode;

            Log.Info("Step 1: Navigate to 'Jobs' page & click on 'Sort & Filter' button");
            viewAllJobs.NavigateToPage();
            viewAllJobs.ClickOnSortAndFilterButton();

            Log.Info("Step 2: Click on 'ZipCode / Radius' option & enter zipcode and distance");
            const string zipCodeOption = "Zip Code / Radius";
            viewAllJobs.ClickOnSortAndFilterOption(zipCodeOption);
            viewAllJobs.EnterZipCode(zipCode);
            viewAllJobs.WaitUntilMpPageLoadingIndicatorInvisible();
            viewAllJobs.ClickOnBackButton();

            Log.Info("Step 3: Verify that Entered 'ZipCode' is shown on Filter tag and on search result page");
            Assert.IsTrue(viewAllJobs.IsZipCodeFilterTextPresentOnFilterTag(zipCode), " Selected 'Zip code' is not present on filter tag ");
            viewAllJobs.ClickOnShowAllResultsButton();
            Assert.IsTrue(viewAllJobs.IsZipCodeCardFilterPresentOnSearchResultPage(zipCode), "Selected 'Zip Code' card filter is not present in search result");
        }

    }
}