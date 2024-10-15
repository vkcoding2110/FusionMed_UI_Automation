using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Traveler.Profile.Employment;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.Employment
{
    [TestClass]
    [TestCategory("TimeBetweenJobs"), TestCategory("FMP")]
    public class TimeBetweenJobsTests2 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("TimeBetweenJobsTests2");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteEmploymentDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatMultipleAddTimeBetweenJobsWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var employmentPopUp = new EmploymentPo(Driver);
            var employmentDetails = new EmploymentDetailsPo(Driver);
            var addTimePopUp = new TimeBetweenJobsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info(
                $"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Add Position' button");
            employmentDetails.ClickOnAddPositionButton();

            Log.Info("Step 6: Add employment details with job gap & verify");
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            employmentPopUp.EnterAddEmploymentData(addEmploymentData);
            employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();
            employmentDetails.ClickOnEditButton();
            employmentPopUp.DeselectCheckBoxAndEnterEndDate(addEmploymentData);

            Log.Info(
                "Step 7: Click on 'Add Time Between Jobs' button, Enter 'Add Time Between Jobs' details in form & verify add time link is displayed again");
            employmentDetails.ClickOnAddTimeBetweenJobsButton();
            var timeOffData = TimeBetweenJobsDataFactory.AddTimeBetweenJobsDetails();
            timeOffData.First().TimeOff = false;
            addTimePopUp.EnterTimeBetweenJobsData(timeOffData.First());
            Assert.IsTrue(employmentDetails.IsAddTimeBetweenJobsSectionDisplayed(),
                "Add time between jobs section is not displayed");

            Log.Info("Step 8: Click on 'Add Time Between Jobs' button, Enter 'Add Time Between Jobs' details in form");
            employmentDetails.ClickOnAddTimeBetweenJobsButton();
            timeOffData.Last().TimeOff = false;
            addTimePopUp.EnterTimeBetweenJobsData(timeOffData.Last());

            Log.Info("Step 9: Verify that multiple time between jobs details are displayed on employment detail page");
            var timeOffRowCount = 1;
            const string expectedTimeOffHeaderText = "Time Between Jobs";
            for (var i = timeOffData.Count - 1; i >= 0; i--)
            {
                var actualTimeOffHeaderText = employmentDetails.GetTimeOffDetailsHeaderText(timeOffRowCount);
                Assert.AreEqual(expectedTimeOffHeaderText, actualTimeOffHeaderText,
                    "Time Off details header label doesn't match");

                var actualTimeOffDetails = employmentDetails.GetTimeOffBetweenJobsDetails(timeOffRowCount);
                Assert.AreEqual(timeOffData[i].State, actualTimeOffDetails.State, "State is not matched");
                Assert.AreEqual(timeOffData[i].City, actualTimeOffDetails.City, "City is not matched");
                Assert.AreEqual(timeOffData[i].StartDate.ToString("MMMM/yyyy"),
                    actualTimeOffDetails.StartDate.ToString("MMMM/yyyy"), "Start date is not matched");
                Assert.AreEqual(timeOffData[i].EndDate.ToString("MMMM/yyyy"),
                    actualTimeOffDetails.EndDate.ToString("MMMM/yyyy"), "End date is not matched");
                timeOffRowCount++;
            }

            //Clean up
            try
            {
                employmentDetails.DeleteAllEmployment();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatTakingTimeOffCheckboxSelectDeSelectWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var employmentPopUp = new EmploymentPo(Driver);
            var employmentDetails = new EmploymentDetailsPo(Driver);
            var addTimePopUp = new TimeBetweenJobsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info(
                $"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Add Position' button");
            employmentDetails.ClickOnAddPositionButton();

            Log.Info("Step 6: Add employment details with job gap & verify");
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            employmentPopUp.EnterAddEmploymentData(addEmploymentData);
            employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();
            employmentDetails.ClickOnEditButton();
            employmentPopUp.DeselectCheckBoxAndEnterEndDate(addEmploymentData);

            Log.Info("Step 7: Click on 'Add Time Between Jobs' button");
            employmentDetails.ClickOnAddTimeBetweenJobsButton();

            Log.Info("Step 8: Select 'Time Off' checkbox & verify End Date is enabled");
            var timeOffData = TimeBetweenJobsDataFactory.AddTimeBetweenJobsDetails().First();
            addTimePopUp.SelectDeselectTimeOffCheckBox(timeOffData.TimeOff);
            Assert.IsFalse(addTimePopUp.IsEndDateInputPresent(), "End date value is still present");

            Log.Info("Step 8: DeSelect 'Time Off' checkbox & verify End Date is enabled");
            timeOffData.TimeOff = false;
            addTimePopUp.SelectDeselectTimeOffCheckBox(timeOffData.TimeOff);
            Assert.IsTrue(addTimePopUp.IsEndDateInputPresent(), "End date value is not present");

            //Clean up
            try
            {
                addTimePopUp.ClickOnCloseIcon();
                employmentDetails.DeleteAllEmployment();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatNonMedicalFieldCheckboxSelectDeSelectWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var employmentPopUp = new EmploymentPo(Driver);
            var employmentDetails = new EmploymentDetailsPo(Driver);
            var addTimePopUp = new TimeBetweenJobsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info(
                $"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Add Position' button");
            employmentDetails.ClickOnAddPositionButton();

            Log.Info("Step 6: Add employment details with job gap & verify");
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            employmentPopUp.EnterAddEmploymentData(addEmploymentData);
            employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();
            employmentDetails.ClickOnEditButton();
            employmentPopUp.DeselectCheckBoxAndEnterEndDate(addEmploymentData);

            Log.Info("Step 7: Click on 'Add Time Between Jobs' button");
            employmentDetails.ClickOnAddTimeBetweenJobsButton();

            Log.Info("Step 8: Select 'Time Off' checkbox & verify End Date is enabled");
            var timeOffData = TimeBetweenJobsDataFactory.AddTimeBetweenJobsDetails().First();
            addTimePopUp.EnterTimeBetweenJobsData(timeOffData);
            const int rowCount = 1;
            Assert.IsTrue(employmentDetails.IsNonMedicalFieldLabelDisplayed(rowCount), "Non medical label is not present");

            Log.Info("Step 8: DeSelect 'Time Off' checkbox & verify End Date is enabled");
            employmentDetails.ClickOnEditTimeBetweenJobsButton();
            timeOffData.TimeOff = false;
            addTimePopUp.SelectDeselectNonMedicalFieldCheckBox(timeOffData.NonMedicalField);
            addTimePopUp.ClickOnSubmitButton();
            Assert.IsFalse(employmentDetails.IsNonMedicalFieldLabelDisplayed(rowCount), "Non medical label is still present");

            //Clean up
            try
            {
                employmentDetails.DeleteAllEmployment();
            }
            catch
            {
                //Do nothing
            }
        }
    }
}
