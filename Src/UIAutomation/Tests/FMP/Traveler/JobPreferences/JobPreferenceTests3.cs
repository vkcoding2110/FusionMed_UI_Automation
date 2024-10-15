using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.JobPreferences;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.JobPreferences;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.JobPreferences
{
    [TestClass]
    [TestCategory("JobPreferences"), TestCategory("FMP")]
    public class JobPreferenceTests3 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("JobPreferenceTests");

        [TestMethod]
        [TestCategory("Smoke"),TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyJobPreferenceDropDownsGetsDisabledAfterMaxSelectionSuccessfully()
        {
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

            Log.Info("Step 3: Click on profile icon & click on 'Job preference' link");
            headerHomePagePo.ClickOnProfileIcon();
            profileMenuPage.ClickOnJobPreferenceMenuItem();
            var editPreferenceData = JobPreferenceDataFactory.AddJobPreferenceDetailToValidate();

            Log.Info("Step 4: Select 5 states from the list and Verify other states from the list is disabled");
            preferencePage.SelectStates(editPreferenceData.States);
            Assert.IsTrue(preferencePage.IsDisabledStateCheckboxPresent(),"State check box list is not disabled");
            preferencePage.CloseStateCheckboxDropDown(editPreferenceData.States);

            Log.Info("Step 5: Select 10 cities from the list and Verify other cities from the list is disabled ");
            preferencePage.SelectCities(editPreferenceData.Cities);
            Assert.IsTrue(preferencePage.IsDisabledCityCheckboxPresent(), "City check box list is not disabled");
            preferencePage.CloseCityCheckboxDropDown(editPreferenceData.Cities);

            Log.Info("Step 6: Select 3 shifts from the list and Verify other shifts from the list is disabled ");
            preferencePage.SelectShiftType(editPreferenceData.ShiftType);
            Assert.IsTrue(preferencePage.IsDisabledShiftCheckboxPresent(), "Shift type check box list is not disabled");
            preferencePage.CloseShiftTypeCheckboxDropDown(editPreferenceData.ShiftType);

            Log.Info("Step 7: Select 3 jobs from the list and Verify other jobs from the list is disabled ");
            preferencePage.SelectJobType(editPreferenceData.JobType);
            Assert.IsTrue(preferencePage.IsDisabledJobTypeCheckboxPresent(), "Job type check box list is not disabled");
            preferencePage.CloseJobTypeCheckboxDropDown(editPreferenceData.JobType);
        }
    }
}
