using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMS.Jobs;
using UIAutomation.DataObjects.Common.Jobs;
using UIAutomation.PageObjects.FMS.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.Jobs
{
    [TestClass]
    [TestCategory("Jobs"), TestCategory("FMS")]
    public class SortAndFiltersTests1 : BaseTest
    {
        public static readonly SortAndFilterBase SortAndFilterDetail = SortAndFilterDataFactory.SortAndFilterDetails();
        private readonly string CategoryOption = SortAndFilterDetail.Category.First();
        private readonly string SpecialtyOption = SortAndFilterDetail.Specialty.First();

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByCategoryWorkSuccessfully()
        {
            var viewAllJobs = new ViewAllJobsPo(Driver);

            Log.Info("Step 1: Navigate to 'Jobs' page & click on 'Sort & Filter' button");
            viewAllJobs.NavigateToPage();
            viewAllJobs.ClickOnSortAndFilterButton();

            Log.Info("Step 2: Click on 'Category' option & verify Category menu is opened");
            const string filterOption = "Category";
            viewAllJobs.ClickOnSortAndFilterOption(filterOption);
            var actualCategoryHeaderText = viewAllJobs.GetSortAndFilterPopupHeaderText();
            Assert.AreEqual(filterOption, actualCategoryHeaderText, "Category Header text is not matched");

            Log.Info($"Step 3: Click on 'Category' option & select '{CategoryOption}' option, click on back button & verify 'Sort & Filter' menu is opened & '{CategoryOption}' option is selected");
            viewAllJobs.SelectFilterMenuSubOption(CategoryOption);
            viewAllJobs.ClickOnBackButton();
            Assert.IsTrue(viewAllJobs.IsSortAndFilterPopupPresent(), "Sort & Filter popup is not opened");

            var actualSelectedCategoryOption = viewAllJobs.GetSelectedCategoryOption();
            Assert.AreEqual(CategoryOption, actualSelectedCategoryOption, "Category option is not matched");

            Log.Info($"Step 4: Click on 'Show All Results' button & verify '{CategoryOption}' option added on job list");
            viewAllJobs.ClickOnShowResultsButton();
            var actualSelectedJobCategoryList = viewAllJobs.GetSelectedJobCategoryList();
            Assert.IsTrue(actualSelectedJobCategoryList.Contains(CategoryOption), $"Job category - {CategoryOption} is not present on applied filter");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifySortByFilterWorkSuccessfully()
        {
            var viewAllJobs = new ViewAllJobsPo(Driver);

            Log.Info("Step 1: Navigate to 'Jobs' page & click on 'Sort & Filter' button");
            viewAllJobs.NavigateToPage();
            viewAllJobs.ClickOnSortAndFilterButton();
            var sortByOption = SortAndFilterDetail.SortBy.First();

            Log.Info($"Step 2: Click on 'Sort By' option & select '{sortByOption}' option & click on back button");
            const string filterOption = "Sort By";
            viewAllJobs.ClickOnSortAndFilterOption(filterOption);
            viewAllJobs.SelectFilterMenuSubOption(sortByOption);
            viewAllJobs.ClickOnBackButton();

            Log.Info("Step 3: Verify Selected Sort by option is visible on filtered text");
            var actualSortByOption = viewAllJobs.GetSelectedSortByOptionText();
            Assert.AreEqual(sortByOption, actualSortByOption, "Sort By option is not matched");
            viewAllJobs.ClickOnShowAllResultsButton();

            Log.Info("Step 4: Verify Selected Sort by option is visible on filtered tag");
            const string sortByFilterTag = "Sort: Pay High";
            Assert.IsTrue(viewAllJobs.IsSortByCardFilterPresentOnSearchResultPage(sortByFilterTag), "Selected 'Estimated Weekly Pay' card is not present on search result page");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterBySpecialtyWorkSuccessfully()
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

            Log.Info($"Step 3: Click on 'Specialty' option, select '{SpecialtyOption}' & click on 'View All Results' button");
            const string specialtyFilterOption = "Specialty";
            viewAllJobs.ClickOnSortAndFilterOption(specialtyFilterOption);
            viewAllJobs.ClickOnSpecialtyOption(SpecialtyOption);
            viewAllJobs.ClickOnShowResultsButton();

            Log.Info("Step 4: verify selected specialty is shown on each job card header");
            var jobTitleList = viewAllJobs.GetAllJobCardTitle();
            for (var i = 1; i < jobTitleList.Count; i++)
            {
                Assert.AreEqual(jobTitleList[i], SpecialtyOption, $"Job Title doesn't have {SpecialtyOption}");
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyFilterByStateAndCityWorkSuccessfully()
        {
            var viewAllJobs = new ViewAllJobsPo(Driver);
            var shiftOption = SortAndFilterDetail.Shift;

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

            Log.Info($"Step 4: Click on 'Shift' option, select '{shiftOption}' & click on back button");
            const string shiftFilterOption = "Shift";
            viewAllJobs.ClickOnSortAndFilterOption(shiftFilterOption);
            viewAllJobs.ClickOnSpecialtyOption(shiftOption);
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
            viewAllJobs.ClickOnShowResultsButton();

            Log.Info("Step 7: verify selected specialty & city is matched with each job card");
            var titleList = viewAllJobs.GetAllJobCardTitle();
            for (var i = 1; i < titleList.Count; i++)
            {
                Assert.AreEqual(titleList[i], SpecialtyOption, $"Job Title doesn't have {SpecialtyOption}");
            }

            var locationList = viewAllJobs.GetAllJobCardLocation();
            for (var i = 1; i < locationList.Count; i++)
            {
                Assert.AreEqual(locationList[i], expectedCityOption.First(), "City name is not matched");
            }
        }
    }
}