using System;
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
    [TestCategory("Employment"), TestCategory("FMP")]
    public class EmploymentTests1 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("EmploymentTests");
        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteEmploymentDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatAddEmploymentWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var employmentPopUp = new EmploymentPo(Driver);
            var employmentDetails = new EmploymentDetailsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Add Position' button & verify 'Add Employment' popup gets open ");
            employmentDetails.ClickOnAddPositionButton();
            const string expectedAddEmploymentPopUpHeaderText = "Add Employment";
            var actualAddEmploymentPopUpHeaderText = employmentPopUp.GetAddEmploymentPopupHeaderText();
            Assert.AreEqual(expectedAddEmploymentPopUpHeaderText, actualAddEmploymentPopUpHeaderText, "The 'Add Employment' popup header text doesn't match");

            Log.Info("Step 6: Verify that 'Job Description' placeholder text is displayed successfully");
            const string expectedJobDescriptionPlaceHolderText = "Departments/Units/Settings covered, Types of Cases, Patient Population, Floating, Avg. Number of Procedures per day/month, Scrub/Monitor/Circulate, etc.";
            var actualJobDescriptionPlaceHolderText = employmentPopUp.GetJobDescriptionPlaceHolderText();
            Assert.AreEqual(expectedJobDescriptionPlaceHolderText, actualJobDescriptionPlaceHolderText, "The 'Job Description' placeholder text doesn't match");

            Log.Info("Step 7: Enter 'Add employment' details ");
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            employmentPopUp.EnterAddEmploymentData(addEmploymentData);
            employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 8: Enter 'Add employment' details & verify Details are displayed correctly on 'Profile Detail' page");
            var actualDataOnDetailPage = employmentDetails.GetEmploymentDetailsFromProfileDetailPage();
            Assert.IsTrue(addEmploymentData.FacilityOption.StartsWith(actualDataOnDetailPage.FacilityOption), "The 'Facility' value does not match");
            Assert.IsTrue(actualDataOnDetailPage.SupervisorEmployment, "The 'Supervisor' is not present");
            Assert.AreEqual(addEmploymentData.Category, actualDataOnDetailPage.Category, "The 'Category' value does not match");
            Assert.AreEqual(addEmploymentData.Specialty, actualDataOnDetailPage.Specialty, "The 'Specialty' value does not match");
            var actualJobSettingInput = actualDataOnDetailPage.JobSettingInput;
            var actualJobSettingInputList = string.Join(Environment.NewLine, actualJobSettingInput.ToArray()).Split(", ");
            CollectionAssert.AreEquivalent(addEmploymentData.JobSettingInput.ToList(), actualJobSettingInputList.ToList(), "Job setting list is not matched");
            Assert.AreEqual(addEmploymentData.Hours, actualDataOnDetailPage.Hours, "The 'Hours per week' value does not match");
            addEmploymentData.ChartingSystemInput.Add(addEmploymentData.OtherChartingSystems);
            var expectedChartingSystemsInput = addEmploymentData.ChartingSystemInput;
            var actualChartingSystemInput = actualDataOnDetailPage.ChartingSystemInput;
            foreach (var a in actualChartingSystemInput)
            {
                foreach (var t in expectedChartingSystemsInput)
                {
                    Assert.IsTrue(a.Contains(t), "The 'Charting Systems' value doesn't match");
                }
            }
            Assert.AreEqual(addEmploymentData.UnitAmount, actualDataOnDetailPage.UnitAmount, "The 'Unit Amount' doesn't match");
            Assert.AreEqual(addEmploymentData.UnitType, actualDataOnDetailPage.UnitType, "The 'Unit Type' does not match");
            Assert.AreEqual("1:" + addEmploymentData.PatientRatio, actualDataOnDetailPage.PatientRatio, "The 'Patient Ratio' value does not match");

            Log.Info("Step 9: Click on 'Edit' button and verify Details are displayed correctly");
            employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();
            employmentDetails.ClickOnEditButton();
            const string expectedEditEmploymentHeaderText = "Edit Employment";
            var actualEditEmploymentHeaderText = employmentPopUp.GetEditEmploymentPopUpHeaderText();
            Assert.AreEqual(expectedEditEmploymentHeaderText, actualEditEmploymentHeaderText, "The 'Edit Employment' header text doesn't match");

            var actualEmploymentData = employmentPopUp.GetEmploymentDetails(addEmploymentData);
            Assert.IsTrue(actualEmploymentData.FacilityOption.StartsWith(actualEmploymentData.FacilityOption), "The 'Facility' option doesn't match");
            CollectionAssert.AreEqual(addEmploymentData.JobSettingInput, actualEmploymentData.JobSettingInput, "Job Settings values doesn't match");
            Assert.AreEqual(addEmploymentData.Category, actualEmploymentData.Category, "The 'Category' option doesn't match");
            Assert.AreEqual(addEmploymentData.Specialty, actualEmploymentData.Specialty, "The 'Specialty' option doesn't match");
            Assert.AreEqual(addEmploymentData.StartDate.ToString("MMMM/yyyy"), actualEmploymentData.StartDate.ToString("MMMM/yyyy"), "The 'Start Date Month' doesn't match");
            if (!actualEmploymentData.WorkHere)
            {
                Assert.AreEqual(addEmploymentData.EndDate.ToString("MMMM/yyyy"), actualEmploymentData.EndDate.ToString("MMMM/yyyy"), "The 'End Date Month' doesn't match");
            }
            Assert.AreEqual(addEmploymentData.JobType, actualEmploymentData.JobType, "The 'Job Type' option doesn't match");
            Assert.AreEqual(addEmploymentData.UnitType, actualEmploymentData.UnitType, "The 'Unit Type' does not match");
            addEmploymentData.ChartingSystemInput.Remove(addEmploymentData.OtherChartingSystems);
            CollectionAssert.AreEqual(addEmploymentData.ChartingSystemInput, actualEmploymentData.ChartingSystemInput, "The 'Charting Systems' options doesn't match");
            if (PlatformName == PlatformName.Web || PlatformName == PlatformName.Ios)
            {
                Assert.AreEqual(addEmploymentData.Hours, actualEmploymentData.Hours, "The 'Hours per week' doesn't match");
                Assert.AreEqual(addEmploymentData.UnitAmount, actualEmploymentData.UnitAmount, "The 'Unit Amount' doesn't match");
                Assert.AreEqual(addEmploymentData.OtherChartingSystems, actualEmploymentData.OtherChartingSystems, "The 'Other Charting Systems' value doesn't match");
                Assert.AreEqual(addEmploymentData.PatientRatio, actualEmploymentData.PatientRatio, "The 'Patient Ratio' value doesn't match");
            }

            //Clean up
            try
            {
                employmentPopUp.ClickOnDeleteEmploymentButton();
                employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        [DoNotParallelize]
        public void VerifyThatUpdateEmploymentDetailsWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var employmentPopUp = new EmploymentPo(Driver);
            var employmentDetails = new EmploymentDetailsPo(Driver);

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

            Log.Info("Step 6: Enter 'Add employment' details & click on 'Edit' button");
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            employmentPopUp.EnterAddEmploymentData(addEmploymentData);
            employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();
            employmentDetails.ClickOnEditButton();

            Log.Info("Step 7: Update 'Employment' details & verify details gets updated");
            var updateEmploymentData = EmploymentDataFactory.UpdateEmploymentFormDetails();
            employmentPopUp.EnterAddEmploymentData(updateEmploymentData);
            employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();
            var actualUpdatedDataOnProfileDetailPage = employmentDetails.GetEmploymentDetailsFromProfileDetailPage();
            Assert.AreEqual(updateEmploymentData.FacilityOption, actualUpdatedDataOnProfileDetailPage.FacilityOption, "The 'Facility' doesn't updated");
            Assert.AreEqual(updateEmploymentData.Category, actualUpdatedDataOnProfileDetailPage.Category, "The 'Department' doesn't updated ");
            Assert.AreEqual(updateEmploymentData.Specialty, actualUpdatedDataOnProfileDetailPage.Specialty, "The 'Specialty' doesn't updated");
            var actualJobSettingInput = actualUpdatedDataOnProfileDetailPage.JobSettingInput;
            var actualJobSettingInputList = string.Join(Environment.NewLine, actualJobSettingInput.ToArray()).Split(", ");
            CollectionAssert.AreEquivalent(updateEmploymentData.JobSettingInput.ToList(), actualJobSettingInputList.ToList(), "Job setting list is not matched");
            Assert.AreEqual(updateEmploymentData.Hours, actualUpdatedDataOnProfileDetailPage.Hours, "The 'Hours per week' doesn't updated");
            updateEmploymentData.ChartingSystemInput.Add(updateEmploymentData.OtherChartingSystems);
            var expectedChartingSystemsInput = updateEmploymentData.ChartingSystemInput;
            var actualChartingSystemInput = actualUpdatedDataOnProfileDetailPage.ChartingSystemInput;
            foreach (var a in actualChartingSystemInput)
            {
                foreach (var t in expectedChartingSystemsInput)
                {
                    Assert.IsTrue(a.Contains(t), "The 'Charting Systems' value doesn't match");
                }
            }
            Assert.AreEqual(updateEmploymentData.UnitAmount, actualUpdatedDataOnProfileDetailPage.UnitAmount, "The 'Unit Amount' doesn't match");
            Assert.AreEqual(updateEmploymentData.UnitType, actualUpdatedDataOnProfileDetailPage.UnitType, "The 'Unit Type' does not match");
            Assert.IsTrue(actualUpdatedDataOnProfileDetailPage.JobDescription.Contains(updateEmploymentData.JobDescription), "The 'Job Description' doesn't updated");
            Assert.AreEqual("1:" + updateEmploymentData.PatientRatio, actualUpdatedDataOnProfileDetailPage.PatientRatio, "The 'Patient Ratio' value does not match");

            Log.Info("Step 8: Click on 'Edit' button and verify Details are displayed correctly");
            employmentDetails.ClickOnEditButton();
            var actualEmploymentData = employmentPopUp.GetEmploymentDetails(updateEmploymentData);
            Assert.IsTrue(actualEmploymentData.FacilityOption.StartsWith(updateEmploymentData.FacilityOption), "The 'Facility' option doesn't match");
            CollectionAssert.AreEquivalent(updateEmploymentData.JobSettingInput, actualEmploymentData.JobSettingInput, "Job Settings values doesn't match");
            Assert.AreEqual(updateEmploymentData.Category, actualEmploymentData.Category, "The 'Category' option doesn't match");
            Assert.AreEqual(updateEmploymentData.Specialty, actualEmploymentData.Specialty, "The 'Specialty' option doesn't match");
            Assert.AreEqual(updateEmploymentData.StartDate.ToString("MMMM/yyyy"), actualEmploymentData.StartDate.ToString("MMMM/yyyy"), "The 'Start Date Month' doesn't match");
            if (!actualEmploymentData.WorkHere)
            {
                Assert.AreEqual(updateEmploymentData.EndDate.ToString("MMMM/yyyy"), actualEmploymentData.EndDate.ToString("MMMM/yyyy"), "The 'End Date' doesn't match");
            }
            Assert.AreEqual(updateEmploymentData.JobType, actualEmploymentData.JobType, "The 'Job Type' option doesn't match");
            Assert.AreEqual(updateEmploymentData.Hours, actualEmploymentData.Hours, "The 'Hours per week' doesn't match");
            Assert.AreEqual(updateEmploymentData.UnitAmount, actualEmploymentData.UnitAmount, "The 'Unit Amount' doesn't match");
            Assert.AreEqual(updateEmploymentData.UnitType, actualEmploymentData.UnitType, "The 'Unit Type' does not match");
            updateEmploymentData.ChartingSystemInput.Remove(updateEmploymentData.OtherChartingSystems);
            CollectionAssert.AreEqual(updateEmploymentData.ChartingSystemInput, actualEmploymentData.ChartingSystemInput, "The 'Charting Systems' options doesn't match");
            Assert.AreEqual(updateEmploymentData.OtherChartingSystems, actualEmploymentData.OtherChartingSystems, "The 'Other Charting Systems' value doesn't match");
            Assert.AreEqual(updateEmploymentData.PatientRatio, actualEmploymentData.PatientRatio, "The 'Patient Ratio' value doesn't match");

            //Clean up
            try
            {
                employmentPopUp.ClickOnDeleteEmploymentButton();
                employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatDeleteEmploymentButtonWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var employmentPopUp = new EmploymentPo(Driver);
            var employmentDetails = new EmploymentDetailsPo(Driver);

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

            Log.Info("Step 6: Enter 'Add employment' details & click on 'Edit' button");
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            employmentPopUp.EnterAddEmploymentData(addEmploymentData);
            employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();
            employmentDetails.ClickOnEditButton();

            Log.Info("Step 7: Click on 'delete employment' button & verify employment details are removed");
            employmentPopUp.ClickOnDeleteEmploymentButton();
            Assert.IsFalse(employmentDetails.IsEmploymentDetailsHeaderTextDisplayed(addEmploymentData.FacilityOption), "The 'Employment' details are present");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyAddEmploymentFacilityTextBoxCloseIconWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var employmentPopUp = new EmploymentPo(Driver);
            var employmentDetails = new EmploymentDetailsPo(Driver);

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

            Log.Info("Step 6: Select 'Facility' auto suggestion option & verify suggestion option is selected");
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            employmentPopUp.EnterFacilityName(addEmploymentData.Facility);
            employmentPopUp.SelectFacilityOption(addEmploymentData.Facility);
            var actualFacilitySuggestionOption = employmentPopUp.GetFacilityAutoSuggestionOptionSelected();
            Assert.AreEqual(addEmploymentData.FacilityOption, actualFacilitySuggestionOption, "The selected auto suggestion option does not match with expected suggestion option");

            Log.Info("Step 7: Click on facility text box 'Cancel' icon & verify Facility TextBox is empty");
            employmentPopUp.ClickOnFacilityTextBoxCancelIcon();
            Assert.IsTrue(employmentPopUp.IsFacilityTextBoxEmpty(), "The 'Facility' text box is not empty");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyAddEmploymentFacilityAutoSuggestionWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var employmentPopUp = new EmploymentPo(Driver);
            var employmentDetails = new EmploymentDetailsPo(Driver);

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

            Log.Info("Step 6: Select 'Facility' auto suggestion option & verify suggestion option is selected");
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();

            var facilityList = employmentPopUp.GetFacilityAutoSuggestionList(addEmploymentData);
            facilityList.Remove("Other - My facility isn't listed");
            foreach (var item in facilityList)
            {
                Assert.IsTrue(item.Contains(addEmploymentData.Facility), $"The selected auto suggestion option does not match with expected suggestion option. Expected : {addEmploymentData.Facility}, Actual : {item} ");
            }
        }
    }
}
