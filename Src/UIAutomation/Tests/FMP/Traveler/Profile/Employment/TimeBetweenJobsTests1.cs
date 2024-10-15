using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.Enum;
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
    public class TimeBetweenJobsTests1 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("TimeBetweenJobsTests");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteEmploymentDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatAddTimeBetweenJobsWorksSuccessfully()
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

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Add Position' button");
            employmentDetails.ClickOnAddPositionButton();

            Log.Info("Step 6: Add employment details with job gap & verify & verify 'Add Time Between Jobs' link displayed");
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            employmentPopUp.EnterAddEmploymentData(addEmploymentData);
            employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();
            employmentDetails.ClickOnEditButton();
            employmentPopUp.DeselectCheckBoxAndEnterEndDate(addEmploymentData);
            const string expectedAddTimeBetweenJobsText = "Hey! Looks like you’ve got a 24 month gap between jobs. Add another position or some time away from work to fill in your job history. Add Time Between Jobs";
            var actualAddTimeBetweenJobsText = employmentDetails.GetAddTimeBetweenJobsText();
            Assert.AreEqual(expectedAddTimeBetweenJobsText.RemoveWhitespace(), actualAddTimeBetweenJobsText.RemoveWhitespace(), "Add Time Between jobs text is not displayed");

            Log.Info("Step 7: Click on 'Add Time Between Jobs' button & verify popup gets open");
            employmentDetails.ClickOnAddTimeBetweenJobsButton();
            Assert.IsTrue(addTimePopUp.IsAddTimeBetweenJobsPopUpOpened(), "Add time between jobs popup is not present");

            Log.Info("Step 8: Enter 'Add Time Between Jobs' form details & verify details gets added on detail page successfully");
            var timeOffData = TimeBetweenJobsDataFactory.AddTimeBetweenJobsDetails();
            timeOffData.First().TimeOff = false;
            addTimePopUp.EnterTimeBetweenJobsData(timeOffData.First());

            const int timeOffRowCount = 1;
            const string expectedTimeOffHeaderText = "Time Between Jobs";
            var actualTimeOffHeaderText = employmentDetails.GetTimeOffDetailsHeaderText(timeOffRowCount);
            Assert.AreEqual(expectedTimeOffHeaderText, actualTimeOffHeaderText, "Time Off details header label doesn't match");

            var actualTimeOffDetailsFromDetailPage = employmentDetails.GetTimeOffBetweenJobsDetails(timeOffRowCount);
            Assert.AreEqual(timeOffData.First().State, actualTimeOffDetailsFromDetailPage.State, "State is not matched");
            Assert.AreEqual(timeOffData.First().City, actualTimeOffDetailsFromDetailPage.City, "City is not matched");
            Assert.AreEqual(timeOffData.First().StartDate.ToString("MMMM/yyyy"), actualTimeOffDetailsFromDetailPage.StartDate.ToString("MMMM/yyyy"), "Start date is not matched");
            Assert.AreEqual(timeOffData.First().EndDate.ToString("MMMM/yyyy"), actualTimeOffDetailsFromDetailPage.EndDate.ToString("MMMM/yyyy"), "End date is not matched");

            Log.Info("Step 9: Click on 'Edit' button & Verify that details gets added on 'Time between jobs' popup successfully");
            employmentDetails.ClickOnEditTimeBetweenJobsButton();
            var actualTimeOffDetailsFromPopUp = addTimePopUp.GetTimeBetweenJobsDetailsFromPopUp();
            Assert.AreEqual(timeOffData.First().StartDate.ToString("MMMM/yyyy"), actualTimeOffDetailsFromPopUp.First().StartDate.ToString("MMMM/yyyy"), "Start date is not matched");
            Assert.AreEqual(timeOffData.First().EndDate.ToString("MMMM/yyyy"), actualTimeOffDetailsFromPopUp.First().EndDate.ToString("MMMM/yyyy"), "End date is not matched");
            Assert.AreEqual(timeOffData.First().State, actualTimeOffDetailsFromPopUp.First().State, "State is not matched");
            if (PlatformName == PlatformName.Web || PlatformName == PlatformName.Ios)
            {
                Assert.AreEqual(timeOffData.First().City, actualTimeOffDetailsFromPopUp.First().City, "City is not matched");
            }

            //Clean up
            try
            {
                addTimePopUp.ClickOnDeleteTimeBetweenJobsButton();
                employmentDetails.DeleteAllEmployment();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatEditTimeBetweenJobsWorksSuccessfully()
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

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
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

            Log.Info("Step 8: Enter 'Add Time Between Jobs' details in form");
            var timeOffData = TimeBetweenJobsDataFactory.AddTimeBetweenJobsDetails();
            timeOffData.First().TimeOff = false;
            addTimePopUp.EnterTimeBetweenJobsData(timeOffData.First());

            Log.Info("Step 9: Click on 'Edit' button, update 'Add Time Between Jobs' details in form  & verify details gets added on detail page successfully");
            employmentDetails.ClickOnEditTimeBetweenJobsButton();
            var editTimeOffData = TimeBetweenJobsDataFactory.UpdateTimeBetweenJobsDetails();
            editTimeOffData.TimeOff = false;
            addTimePopUp.EnterTimeBetweenJobsData(editTimeOffData);

            const int timeOffRowCount = 1;
            const string expectedTimeOffHeaderText = "Time Between Jobs";
            var actualTimeOffHeaderText = employmentDetails.GetTimeOffDetailsHeaderText(timeOffRowCount);
            Assert.AreEqual(expectedTimeOffHeaderText, actualTimeOffHeaderText, "Time Off details header label doesn't match");

            var actualTimeOffDetailsFromDetailPage = employmentDetails.GetTimeOffBetweenJobsDetails(timeOffRowCount);
            Assert.AreEqual(editTimeOffData.State, actualTimeOffDetailsFromDetailPage.State, "State is not matched");
            Assert.AreEqual(editTimeOffData.City, actualTimeOffDetailsFromDetailPage.City, "City is not matched");
            Assert.AreEqual(editTimeOffData.StartDate.ToString("MMMM/yyyy"), actualTimeOffDetailsFromDetailPage.StartDate.ToString("MMMM/yyyy"), "Start date is not matched");
            Assert.AreEqual(editTimeOffData.EndDate.ToString("MMMM/yyyy"), actualTimeOffDetailsFromDetailPage.EndDate.ToString("MMMM/yyyy"), "End date is not matched");

            Log.Info("Step 10: Click on 'Edit' button & Verify that details gets added on 'Time between jobs' popup successfully");
            employmentDetails.ClickOnEditTimeBetweenJobsButton();
            var actualTimeOffDetailsFromPopUp = addTimePopUp.GetTimeBetweenJobsDetailsFromPopUp();
            Assert.AreEqual(editTimeOffData.StartDate.ToString("MMMM/yyyy"), actualTimeOffDetailsFromPopUp.First().StartDate.ToString("MMMM/yyyy"), "Start date is not matched");
            Assert.AreEqual(editTimeOffData.EndDate.ToString("MMMM/yyyy"), actualTimeOffDetailsFromPopUp.First().EndDate.ToString("MMMM/yyyy"), "End date is not matched");
            Assert.AreEqual(editTimeOffData.City, actualTimeOffDetailsFromPopUp.First().City, "City is not matched");
            Assert.AreEqual(editTimeOffData.State, actualTimeOffDetailsFromPopUp.First().State, "State is not matched");

            //Clean up
            try
            {
                addTimePopUp.ClickOnDeleteTimeBetweenJobsButton();
                employmentDetails.DeleteAllEmployment();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatDeleteTimeBetweenJobsWorksSuccessfully()
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

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
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

            Log.Info("Step 8: Enter 'Add Time Between Jobs' details in form");
            var timeOffData = TimeBetweenJobsDataFactory.AddTimeBetweenJobsDetails();
            addTimePopUp.EnterTimeBetweenJobsData(timeOffData.First());

            Log.Info("Step 9: Click on 'Edit' button, Click on 'delete time between jobs' button & verify time between jobs gets deleted successfully");
            employmentDetails.ClickOnEditTimeBetweenJobsButton();
            addTimePopUp.ClickOnDeleteTimeBetweenJobsButton();
            const int timeOffRowCount = 1;
            var  expectedTimeOffHeaderText = employmentDetails.GetStateName(timeOffRowCount);
            Assert.AreNotEqual(timeOffData.First().State, expectedTimeOffHeaderText, "Time off details are still displayed");

            //Clean up
            try
            {
                addTimePopUp.ClickOnDeleteTimeBetweenJobsButton();
                employmentDetails.DeleteAllEmployment();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatTimeBetweenJobsPopUpCloseIconAndCancelButtonWorksSuccessfully()
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

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
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

            Log.Info("Step 8: Click on 'Close' icon & verify popup gets close");
            addTimePopUp.ClickOnCloseIcon();
            Assert.IsFalse(addTimePopUp.IsTimeOffPopUpPresent(), "The 'Add Employment' popup is open");

            Log.Info("Step 9: Click on 'Add Time Between Jobs' button");
            employmentDetails.ClickOnAddTimeBetweenJobsButton();

            Log.Info("Step 10: Click on 'Close' icon & verify popup gets close");
            addTimePopUp.ClickOnCancelButton();
            Assert.IsFalse(addTimePopUp.IsTimeOffPopUpPresent(), "The 'Add Employment' popup is open");

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
        public void VerifyThatValidationMessageIsDisplayedForMandatoryFieldsInTimeBetweenJobsForm()
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

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
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
            var timeOffData = TimeBetweenJobsDataFactory.AddTimeBetweenJobsDetails().First();

            Log.Info("Step 8: Clear 'Start Date' field and Click on 'Add Time Between Jobs' button & verify that 'Start Date' validation message is displayed");
            addTimePopUp.ClearStartDate();
            addTimePopUp.SelectEndDate(timeOffData.EndDate);
            addTimePopUp.EnterTimeAwayCity(timeOffData.City);
            addTimePopUp.SelectTimeAwayState(timeOffData.State);
            addTimePopUp.ClickOnAddTimeBetweenJobsButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, addTimePopUp.GetStartDateValidationMessage(), " Validation message is not displayed for Start-date");

            Log.Info("Step 9: Clear 'End Date' field and Click on 'Add Time Between Jobs' button & verify that 'End Date' validation message is displayed");
            addTimePopUp.SelectStartDate(timeOffData.StartDate);
            addTimePopUp.ClearEndDate();
            addTimePopUp.EnterTimeAwayCity(timeOffData.City);
            addTimePopUp.SelectTimeAwayState(timeOffData.State);
            addTimePopUp.ClickOnAddTimeBetweenJobsButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, addTimePopUp.GetEndDateValidationMessage(), "Validation message is not displayed for End-date");

            Log.Info("Step 10: Clear 'City' field and Click on 'Add Time Between Jobs' button & verify that 'City' validation message is displayed");
            addTimePopUp.SelectStartDate(timeOffData.StartDate);
            addTimePopUp.SelectEndDate(timeOffData.EndDate);
            addTimePopUp.EnterTimeAwayCity("");
            addTimePopUp.SelectTimeAwayState(timeOffData.State);
            addTimePopUp.ClickOnAddTimeBetweenJobsButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, addTimePopUp.GetTimeAwayCityValidationMessage(), "'Add employment' pop-up is not present");

            Log.Info("Step 11: Clear 'City' field and Click on 'Add Time Between Jobs' button & verify that 'City' validation message is displayed");
            addTimePopUp.SelectStartDate(timeOffData.StartDate);
            addTimePopUp.SelectEndDate(timeOffData.EndDate);
            addTimePopUp.EnterTimeAwayCity(timeOffData.City);
            addTimePopUp.ClearTimeAwayState();
            addTimePopUp.ClickOnAddTimeBetweenJobsButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, addTimePopUp.GetTimeAwayStateValidationMessage(), "'Add employment' pop-up is not present");

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
