using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Account;
using UIAutomation.DataFactory.FMP.Traveler.JobPreferences;
using UIAutomation.DataObjects.FMP.Account;
using UIAutomation.DataObjects.FMP.Traveler.JobPreferences;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.PageObjects.FMP.Traveler.JobPreferences;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.JobPreferences
{

    [TestClass]
    [TestCategory("JobPreferences"), TestCategory("FMP")]
    public class JobPreferenceTests1 : FmpBaseTest
    {
        private static SignUp _signUp = new();
        private static readonly JobPreference PreferenceDetail = JobPreferenceDataFactory.AddPreferenceDetails();
        private const string FilterOption = "Preferences";

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            _signUp = SignUpDataFactory.GetDataForSignUpForm();
            setup.CreateUser(_signUp);
        }


        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyJobPreferenceDetailIsAddedSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var preferencePage = new JobPreferencePo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var searchJobs = new PageObjects.FMP.Jobs.SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{_signUp.Email}, password:{_signUp.Password}");
            fmpLogin.LoginToApplication(_signUp);
            headerHomePagePo.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Click on profile icon & click on 'Job preference' link");
            headerHomePagePo.ClickOnProfileIcon();
            profilePage.ClickOnJobPreferenceMenuItem();
            const string expectedHeaderText = "Job Preferences";
            var actualHeaderText = preferencePage.GetPreferencePageHeaderText();
            Assert.AreEqual(expectedHeaderText.ToLowerInvariant(), actualHeaderText.ToLowerInvariant(), "Job Preference page header text is not matched");

            Log.Info("Step 5: Add detail in preference page & click on 'Save Preferences' button");
            preferencePage.EnterPreferenceDetail(PreferenceDetail);
            Assert.IsTrue(preferencePage.IsStartNowCheckboxChecked(),"Ready to start now checkbox is not checked");
            Assert.IsFalse(preferencePage.IsStartDateFieldEnabled(),"Start date field is enable");

            Log.Info("Step 6: Verify Added job preference details are correct");
            var actualPreferenceData = preferencePage.GetJobPreferenceDetail();
            CollectionAssert.AreEqual(PreferenceDetail.Departments, actualPreferenceData.Departments, "Department is not matched");
            CollectionAssert.AreEqual(PreferenceDetail.Specialties, actualPreferenceData.Specialties, "Specialty is not matched");
            CollectionAssert.AreEqual(PreferenceDetail.States, actualPreferenceData.States, "State is not matched");
            CollectionAssert.AreEqual(PreferenceDetail.Cities,actualPreferenceData.Cities,"City is not matched");
            CollectionAssert.AreEqual(PreferenceDetail.ShiftType, actualPreferenceData.ShiftType, "Shift is not matched");
            CollectionAssert.AreEqual(PreferenceDetail.JobType, actualPreferenceData.JobType, "Job type is not matched");
            Assert.AreEqual(PreferenceDetail.StartNow, actualPreferenceData.StartNow, "Start now checkbox is not selected ");

            Log.Info("Step 7: Click on 'View All Jobs', click on 'Sort & Filter' button & Select 'Preference' filter");
            Driver.Back();
            exploreMenu.ClickOnViewAllJobsLinkText(FmpConstants.JobsText);
            searchJobs.ClickOnSortAndFilterButton();
            sortAndFilter.ClickOnSortAndFilterOption(FilterOption);

            Log.Info("Step 8: Click on 'Select All' checkbox and Verify all preference is selected");
            sortAndFilter.ClickOnSelectAllCheckbox(true);
            Assert.IsTrue(sortAndFilter.IsSelectAllCheckboxChecked(), "Select all check box not selected");

            Log.Info("Step 9: Uncheck 'Select All' checkbox and Verify all preference is not selected ");
            sortAndFilter.ClickOnSelectAllCheckbox(false);
            Assert.IsFalse(sortAndFilter.IsSelectAllCheckboxChecked(), "Select all check box is selected");

            Log.Info("Step 10: Verify Added preference is displayed in sort and filter by preference");
            var actualPreferenceSortAndFilterData = sortAndFilter.GetMyPreferenceDetail();
            CollectionAssert.AreEqual(PreferenceDetail.Departments, actualPreferenceSortAndFilterData.Departments, "Department is not matched");
            CollectionAssert.AreEqual(PreferenceDetail.Specialties, actualPreferenceSortAndFilterData.Specialties, "Specialty is not matched");
            CollectionAssert.AreEqual(PreferenceDetail.JobType, actualPreferenceSortAndFilterData.JobType, "Job type is not matched");
            CollectionAssert.AreEqual(PreferenceDetail.ShiftType, actualPreferenceSortAndFilterData.ShiftType, "Shift is not matched");
            CollectionAssert.AreEqual(PreferenceDetail.States, actualPreferenceSortAndFilterData.States, "State is not matched");
            CollectionAssert.AreEqual(PreferenceDetail.Cities,actualPreferenceSortAndFilterData.Cities,"Cities is not matched");
            Assert.AreEqual(PreferenceDetail.StartNow, actualPreferenceSortAndFilterData.StartNow, "Start now  is not matched");
            Assert.AreEqual(PreferenceDetail.MaxSalary.RemoveWhitespace().Replace("$", ""), actualPreferenceSortAndFilterData.MaxSalary.RemoveWhitespace().Replace("$", ""), "Max salary is not matched");
            Assert.AreEqual(PreferenceDetail.MinSalary.RemoveWhitespace().Replace("$", ""), actualPreferenceSortAndFilterData.MinSalary.RemoveWhitespace().Replace("$", ""), "Min salary is not matched");
        }
    }
}
