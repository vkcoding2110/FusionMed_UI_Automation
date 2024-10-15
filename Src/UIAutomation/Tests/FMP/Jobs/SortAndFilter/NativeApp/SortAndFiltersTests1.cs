using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Account.NativeApp;
using UIAutomation.PageObjects.FMP.Home.NativeApp;
using UIAutomation.PageObjects.FMP.Jobs.NativeApp;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.SortAndFilter.NativeApp
{
    [TestClass]
    [TestCategory("Jobs"), TestCategory("NativeAppAndroid")]
    public class SortAndFiltersTests1 : FmpBaseTest
    {
        private const string DepartmentText = "Department";
        private const string SubCategoryText = "Jobs";
        private const string CategoryText = "Category";
        private const string SpecialtyText = "Specialty";

        [TestMethod]
        public void VerifySortAndFilterPopupOpenedAndClosedWorkSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);

            Log.Info("Step 2: Click on 'Sort & Filter' button & verify filter popup is opened");
            homePage.ClickOnSortAndFilterButton();
            Assert.IsTrue(sortAndFilter.IsSortAndFilterPopupPresent(), "Sort & Filter popup is not opened");

            Log.Info("Step 3: Click on 'close' icon on filter popup & verify filter popup is closed");
            sortAndFilter.ClickOnSortAndFilterCloseIcon();
            Assert.IsFalse(sortAndFilter.IsSortAndFilterPopupPresent(), "Sort & Filter popup is opened");
        }

        [TestMethod]
        public void SortAndFilter_VerifyFilterByCategoryAndSpecialtyWorkSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var jobDetails = new JobsDetailsPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);

            Log.Info("Step 2: Click on 'Sort & Filter' button & verify filter popup is opened");
            homePage.ClickOnSortAndFilterButton();
            Log.Info("Step 3: Click on 'Reset All' button, Click on 'Department' option & select 'Jobs' option");
            sortAndFilter.ClickOnResetAllButton();
            sortAndFilter.ClickOnSortAndFilterCategoryDropDown(DepartmentText);
            sortAndFilter.ClickOnDepartmentSubCategoryRadioButton(SubCategoryText);

            var data = EmploymentDataFactory.AddEmploymentDetails();
            Log.Info($"Step 4: Click on '{CategoryText}' option, select '{data.Category}'");
            sortAndFilter.ClickOnSortAndFilterCategoryDropDown(CategoryText);
            sortAndFilter.ClickOnCategoryCheckbox(data.Category);
            sortAndFilter.ClickOnSortAndFilterCategoryDropDown(CategoryText);

            Log.Info("Step 5: Click on 'Specialty' option, select 'Cath Lab RN'");
            Driver.ScrollToElementByText(SpecialtyText);
            sortAndFilter.ClickOnSortAndFilterCategoryDropDown(SpecialtyText);
            Driver.ScrollToElementByText(data.Specialty);
            sortAndFilter.ClickOnCategoryCheckbox(data.Specialty);

            Log.Info("Step 6: Click on 'Show All Results', Verify Selected Sort by option is visible on filtered tag");
            sortAndFilter.ClickOnShowAllResultsButton();
            var filteredTitleList = sortAndFilter.GetFilterTagTextList();
            Assert.IsTrue(filteredTitleList.Contains(data.Category) , "Category doesn't match");
            Assert.IsTrue( filteredTitleList.Contains(data.Specialty) , "Specialty doesn't match");
            Assert.IsTrue( filteredTitleList.Contains(SubCategoryText), "Sub Category doesn't match");

            Log.Info("Step 7: Click on first job card and verify 'Job' title");
            sortAndFilter.ClickOnFirstJobCard();
            var actualJobCardTitle = jobDetails.GetJobCardTitle();
            Assert.AreEqual(data.Specialty, actualJobCardTitle,"Job card title is not matched");
        }
    }
}
