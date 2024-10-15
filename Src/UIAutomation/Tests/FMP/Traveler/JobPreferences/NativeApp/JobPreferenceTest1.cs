using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.JobPreferences;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account.NativeApp;
using UIAutomation.PageObjects.FMP.Home.NativeApp;
using UIAutomation.PageObjects.FMP.NativeApp.More;
using UIAutomation.PageObjects.FMP.Traveler.JobPreferences.NativeApp;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.JobPreferences.NativeApp
{
    [TestClass]
    [TestCategory("JobPreferences"), TestCategory("NativeAppAndroid")]
    public class JobPreferenceTest : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("PreferenceAppTests");
        private const string UpToThreeJobTypes = "Up to 3 Job Types";
        private const string StateText = "Colorado";
        private const string CityText = "Adona, AR";
        private const string ShiftType = "Night";
        private const string JobType = "Travel";

        [TestMethod]
        public void VerifyJobPreferenceDropDownsGetsDisabledAfterMaxSelectionSuccessfully()
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

            Log.Info("Step 3: Select 5 states from the list and Verify other states from the list are disabled");
            var editPreferenceData = JobPreferenceDataFactory.AddJobPreferenceDetailToValidate();
            jobPreference.SelectStates(editPreferenceData.States);
            Assert.IsFalse(jobPreference.IsCheckboxEnabled(StateText), "State check box list is not disabled");
            jobPreference.ClickOnConfirmButton();

            Log.Info("Step 4: Select 10 cities from the list and Verify other cities from the list are disabled ");
            jobPreference.SelectCities(editPreferenceData.Cities);
            Assert.IsFalse(jobPreference.IsCheckboxEnabled(CityText), "City check box list is not disabled");
            jobPreference.ClickOnConfirmButton();

            Log.Info("Step 5: Select 3 shifts from the list and Verify other shifts from the list are disabled ");
            Driver.ScrollToElementByText(UpToThreeJobTypes);
            jobPreference.SelectShiftType(editPreferenceData.ShiftType);
            Assert.IsFalse(jobPreference.IsCheckboxEnabled(ShiftType), "Shift type check box list is not disabled");
            jobPreference.ClickOnConfirmButton();

            Log.Info("Step 6: Select 3 jobs from the list and Verify other jobs from the list are disabled ");
            Driver.ScrollToElementByText(UpToThreeJobTypes);
            jobPreference.SelectJobType(editPreferenceData.JobType);
            Assert.IsFalse(jobPreference.IsCheckboxEnabled(JobType), "Job type check box list is not disabled");
        }
    }
}
