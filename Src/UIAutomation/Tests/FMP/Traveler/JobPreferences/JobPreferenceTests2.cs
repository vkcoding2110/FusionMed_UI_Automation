using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.JobPreferences;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.PageObjects.FMP.Traveler.JobPreferences;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.JobPreferences
{

    [TestClass]
    [TestCategory("JobPreferences"), TestCategory("FMP")]
    public class JobPreferenceTests2 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("EditPreferenceTests");
        private const string FilterOption = "Preferences";

        // Commented code due to bug ,Defect - id 99,75

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyEditMyPreferenceWorkSuccessfullyFromSortAndFilter()
        {
            var searchJobs = new PageObjects.FMP.Jobs.SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var preferencePage = new JobPreferencePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileMenuPage = new ProfileMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            headerHomePagePo.ClickOnLogInButton();
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 3: Click on 'View All Jobs', Navigate to sort & filter and verify 'edit my preference' link is displayed & click on it");
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();
            sortAndFilter.ClickOnSortAndFilterOption(FilterOption);
            Assert.IsTrue(sortAndFilter.IsEditMyPreferenceLinkDisplayed(), "Edit my preference link is not displayed");

            Log.Info("Step 4: update preference data and Verify details are updated in sort and filter preference page");
            sortAndFilter.ClickOnEditPreferenceLink();
            var editPreferenceData = JobPreferenceDataFactory.EditJobPreferenceDetails();
            preferencePage.EnterPreferenceDetail(editPreferenceData);
            var actualPreferenceData = preferencePage.GetJobPreferenceDetail();
            CollectionAssert.AreEqual(editPreferenceData.Departments, actualPreferenceData.Departments, "Department is not matched");
            CollectionAssert.AreEqual(editPreferenceData.Specialties, actualPreferenceData.Specialties, "Specialty is not matched");
            CollectionAssert.AreEqual(editPreferenceData.States, actualPreferenceData.States, "State is not matched");
            CollectionAssert.AreEqual(editPreferenceData.Cities, actualPreferenceData.Cities, "City is not matched");
            CollectionAssert.AreEqual(editPreferenceData.ShiftType, actualPreferenceData.ShiftType, "Shift is not matched");
            CollectionAssert.AreEqual(editPreferenceData.JobType, actualPreferenceData.JobType, "Shift is not matched");
            Assert.AreEqual(editPreferenceData.StartDate.ToString("MM/dd/yyyy"), actualPreferenceData.StartDate.ToString("MM/dd/yyyy"), "Start date is not matched");
            Assert.AreEqual(editPreferenceData.StartNow, actualPreferenceData.StartNow, "Start now checkbox is not selected");

            Log.Info("Step 5: Verify updated preference details are displayed on filter text");
            sortAndFilter.ClickOnPreferenceBackButton();
            sortAndFilter.ClickOnSelectAllCheckbox(true);
            sortAndFilter.ClickOnBackButton();
            var actualPreferenceFilterValue = sortAndFilter.GetSelectedPreferenceValue();
            CollectionAssert.AreEqual(editPreferenceData.Departments, actualPreferenceFilterValue.Departments, "Selected Department filter text is not matched ");
            CollectionAssert.AreEqual(editPreferenceData.Specialties, actualPreferenceFilterValue.Specialties, "Selected Specialty filter text is not matched");
            CollectionAssert.AreEquivalent(editPreferenceData.States, actualPreferenceFilterValue.States, "Selected State filter text is not matched");
            //CollectionAssert.AreEqual(editPreferenceData.Cities,actualPreferenceFilterValue.Cities, "Selected City filter text is not matched");
            CollectionAssert.AreEqual(editPreferenceData.ShiftType, actualPreferenceFilterValue.ShiftType, "Selected Shift type filter text is not matched");
            CollectionAssert.AreEqual(editPreferenceData.JobType, actualPreferenceFilterValue.JobType, "Selected job type filter text is not matched");
            Assert.AreEqual(editPreferenceData.StartDate.ToString("MMMM d, yyyy"), actualPreferenceFilterValue.StartDate.ToString("MMMM d, yyyy"), "Selected start date filter text is not matched");
            Assert.AreEqual(editPreferenceData.MinSalary.RemoveWhitespace().Replace("$", ""), actualPreferenceFilterValue.MinSalary.RemoveWhitespace().Replace("$", ""), "Selected minimum salary filter text is not matched");
            Assert.AreEqual(editPreferenceData.MaxSalary.RemoveWhitespace().Replace("$", ""), actualPreferenceFilterValue.MaxSalary.RemoveWhitespace().Replace("$", ""), "Selected maximum salary filter text is not matched");

            Log.Info("Step 6: Verify selected preference is displayed on filter tags");
            sortAndFilter.ClickOnShowAllResultsButton();
            var selectedAllPreferenceData = sortAndFilter.GetSelectedJobCategoryList();
            foreach (var department in editPreferenceData.Departments)
            {
                Assert.IsTrue(selectedAllPreferenceData.Contains(department), $"{department} is not present in filter tag");
            }
            foreach (var state in editPreferenceData.States)
            {
                Assert.IsTrue(selectedAllPreferenceData.Contains(state), $"{state} State filter tag is not present");
            }

            foreach (var city in editPreferenceData.Cities)
            {
                Assert.IsTrue(selectedAllPreferenceData.Contains(city), $"{city} City filter tag is not present");
            }
            foreach (var specialty in editPreferenceData.Specialties)
            {
                Assert.IsTrue(selectedAllPreferenceData.Contains(specialty), $"{specialty} Specialty filter tag is not present");
            }
            foreach (var shift in editPreferenceData.ShiftType)
            {
                Assert.IsTrue(selectedAllPreferenceData.Contains(shift + " Shift"), $"{shift} Shift type filter tag is not present");
            }
            foreach (var job in editPreferenceData.JobType)
            {
                Assert.IsTrue(selectedAllPreferenceData.Contains(job), $"{job} Job type filter tag is not present");
            }

            var expectedMinAndMaxSalary = "$" + editPreferenceData.MinSalary + " - " + "$" + editPreferenceData.MaxSalary;
            Assert.IsTrue(selectedAllPreferenceData.Contains(expectedMinAndMaxSalary), "Selected Pay range filter tag is not present");
            var expectedEditedStartDate = "Starting: " + editPreferenceData.StartDate.ToString("MMMM d, yyyy");
            Assert.IsTrue(selectedAllPreferenceData.Contains(expectedEditedStartDate), "Start Date filter tag is not present");

            Log.Info("Step 7: Select Job type, State ,shift preferences and Verify all selected preference is displayed on filter text");
            Driver.RefreshPage();
            searchJobs.WaitUntilFmpPageLoadingIndicatorInvisible();
            searchJobs.ClickOnSortAndFilterButton();
            const string filterOption = "Department";
            sortAndFilter.ClickOnSortAndFilterOption(filterOption);
            const string expectedDepartmentOption = "Jobs";
            sortAndFilter.SelectDepartmentFilterMenuOption(expectedDepartmentOption);
            sortAndFilter.ClickOnBackButton();
            sortAndFilter.ClickOnSortAndFilterOption(FilterOption);
            sortAndFilter.ClickOnSelectAllCheckbox(false);
            sortAndFilter.SelectJobTypePreferenceCheckbox(true);
            sortAndFilter.SelectShiftPreferenceCheckbox(true);
            sortAndFilter.SelectStartDatePreferenceCheckbox(true);
            sortAndFilter.ClickOnBackButton();
            const string expectedPreferenceText = "My Preferences";
            var actualSelectedPreferenceText = sortAndFilter.GetPreferenceFilterText();
            var actualJobTypeFilterValue = sortAndFilter.GetJobTypeInputValue();
            //var actualStartDateFilterValue = sortAndFilter.GetSelectedStartDateText();
            var actualShiftTypeFilterValue = sortAndFilter.GetSelectedShiftOption();
            Assert.AreEqual(expectedPreferenceText, actualSelectedPreferenceText, "Preference test is not matched");
            CollectionAssert.AreEqual(editPreferenceData.JobType.ToList(), actualJobTypeFilterValue.ToList(), "Selected JobType filter text is not matched");
            // Assert.AreEqual(editPreferenceData.StartDate.ToString("MMMM dd, yyyy"), actualStartDateFilterValue, "Selected Start date filter text is not matched");
            CollectionAssert.AreEqual(editPreferenceData.ShiftType.ToList(), actualShiftTypeFilterValue.ToList(), "Selected shift filter text is not matched");

            Log.Info("Step 8: Verify selected preference is displayed on filter tag");
            sortAndFilter.ClickOnShowAllResultsButton();
            foreach (var shift in editPreferenceData.ShiftType)
            {
                Assert.IsTrue(selectedAllPreferenceData.Contains(shift + " Shift"), $"{shift} Shift type filter tag is not present");
            }
            foreach (var job in editPreferenceData.JobType)
            {
                Assert.IsTrue(selectedAllPreferenceData.Contains(job), $"{job} Job type filter tag is not present");
            }
            //var expectedEditedStartDateOnFilterTag = "Starting: " + editPreferenceData.StartDate.ToString("MMMM dd, yyyy");
            //Assert.IsTrue(selectedAllPreferenceData.Contains(expectedEditedStartDateOnFilterTag), "Start Date filter tag is not present");

            Log.Info("Step 9: De select select all Preferences and Verify all selected preferences data is not displayed");
            searchJobs.ClickOnSortAndFilterButton();
            sortAndFilter.ClickOnSortAndFilterOption(FilterOption);
            sortAndFilter.SelectJobTypePreferenceCheckbox(false);
            sortAndFilter.SelectShiftPreferenceCheckbox(false);
            sortAndFilter.SelectStartDatePreferenceCheckbox(false);
            sortAndFilter.ClickOnBackButton();
            const int expectedFilterSelectedOptionCount = 2;
            var actualFilterSelectedOptionCount = sortAndFilter.GetSelectedFilterOptionCount();
            Assert.AreEqual(expectedFilterSelectedOptionCount, actualFilterSelectedOptionCount, "Selected Filter Count is not matched");
            Assert.IsFalse(sortAndFilter.IsSelectedJobFilterTagsDisplayed(), "The selected job filter tags are still displayed");

            Log.Info("Step 10: Verify edited job preference details are correctly displayed in 'Job preference' page");
            sortAndFilter.ClickOnShowAllResultsButton();
            headerHomePagePo.ClickOnProfileIcon();
            profileMenuPage.ClickOnJobPreferenceMenuItem();
            var actualEditedPreferenceData = preferencePage.GetJobPreferenceDetail();
            CollectionAssert.AreEqual(editPreferenceData.Departments, actualEditedPreferenceData.Departments, "Department is not matched");
            CollectionAssert.AreEqual(editPreferenceData.Specialties, actualEditedPreferenceData.Specialties, "Specialty is not matched");
            CollectionAssert.AreEqual(editPreferenceData.States, actualEditedPreferenceData.States, "State is not matched");
            CollectionAssert.AreEqual(editPreferenceData.Cities, actualEditedPreferenceData.Cities, "City is not matched");
            CollectionAssert.AreEqual(editPreferenceData.ShiftType, actualEditedPreferenceData.ShiftType, "Shift is not matched");
            CollectionAssert.AreEqual(editPreferenceData.JobType, actualEditedPreferenceData.JobType, "Job type is not matched");
            //Assert.AreEqual(editPreferenceData.StartDate.ToString("MM/dd/yyyy"), actualEditedPreferenceData.StartDate.ToString("MM/dd/yyyy"), "Start date is not matched");
        }
    }
}
