using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.JobPreferences;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.DataObjects.FMP.Traveler.JobPreferences;
using UIAutomation.PageObjects.FMP.Account.NativeApp;
using UIAutomation.PageObjects.FMP.Home.NativeApp;
using UIAutomation.PageObjects.FMP.NativeApp.More;
using UIAutomation.PageObjects.FMP.Traveler.JobPreferences.NativeApp;

namespace UIAutomation.Tests.FMP.Traveler.JobPreferences.NativeApp
{

    [TestClass]
    [TestCategory("JobPreferences"), TestCategory("NativeAppAndroid")]
    public class JobPreferenceTests2 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("EditPreferenceTests");
        private static readonly JobPreference PreferenceDetail = JobPreferenceDataFactory.AddPreferenceDetails();

        [TestMethod]
        public void VerifyEditMyJobPreferenceWorkSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var homePage = new HomePagePo(Driver);
            var moreMenu = new MoreMenuPo(Driver);
            var jobPreference = new JobPreferencePo(Driver);

            Log.Info("Step 1: Open FMP App & Login to the application");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on 'Profile' icon, Click on 'Job Preferences' option");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnJobPreferencesOption();
            Assert.IsTrue(jobPreference.IsPreferencePageHeaderTextPresent(), "'Job Preference' page is not opened");

            Log.Info("Step 3: Add detail in preference page & click on 'Save Preferences' button");
            jobPreference.EnterPreferenceDetail(PreferenceDetail);
            Assert.IsTrue(jobPreference.IsSavePreferencesSuccessMessageTextPresent(), "Save 'Job Preference' pop up is not opened");

            Log.Info("Step 4: update preference data and Verify details");
            var editPreferenceData = JobPreferenceDataFactory.EditJobPreferenceDetails();
            jobPreference.EnterPreferenceDetail(editPreferenceData);
            var actualPreferenceData = jobPreference.GetJobPreferenceDetail();
            CollectionAssert.AreEqual(editPreferenceData.Departments, actualPreferenceData.Departments, "Department is not matched");
            CollectionAssert.AreEqual(editPreferenceData.Specialties, actualPreferenceData.Specialties, "Specialty is not matched");
            CollectionAssert.AreEqual(editPreferenceData.States, actualPreferenceData.States, "State is not matched");
            CollectionAssert.AreEqual(editPreferenceData.Cities, actualPreferenceData.Cities, "City is not matched");
            CollectionAssert.AreEqual(editPreferenceData.ShiftType, actualPreferenceData.ShiftType, "Shift is not matched");
            CollectionAssert.AreEqual(editPreferenceData.JobType, actualPreferenceData.JobType, "Shift is not matched");
            Assert.AreEqual(editPreferenceData.StartDate.ToString("MM/dd/yyyy"), actualPreferenceData.StartDate.ToString("MM/dd/yyyy"), "Start date is not matched");
            Assert.AreEqual(editPreferenceData.StartNow, actualPreferenceData.StartNow, "Start now checkbox is not selected");
        }
    }
}
