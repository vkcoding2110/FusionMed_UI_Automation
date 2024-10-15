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
    public class EmploymentTests2 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("EmploymentTests");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteEmploymentDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatAddEmploymentCloseIconAndCancelButtonWorksSuccessfully()
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

            Log.Info("Step 6: Click on 'Close' icon and verify popup gets close");
            employmentPopUp.ClickOnCloseIcon();
            Assert.IsFalse(employmentPopUp.IsAddEmploymentPopUpPresent(), "The 'Add Employment' popup is open");

            Log.Info("Step 7: Click on 'Add Position' button");
            employmentDetails.ClickOnAddPositionButton();

            Log.Info("Step 8: Click on 'Cancel' button and verify popup gets close");
            employmentPopUp.ClickOnCancelButton();
            Assert.IsFalse(employmentPopUp.IsAddEmploymentPopUpPresent(), "The 'Add Employment' popup does not close");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyValidationMessageIsDisplayedForMandatoryFieldsInAddEmploymentForm()
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
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();

            Log.Info("Step 6: Clear 'Facility' field and Click on 'Add employment' button & verify that 'Facility' validation message is displayed");
            employmentPopUp.EnterFacilityName("");
            employmentPopUp.SelectCategory(addEmploymentData.Category);
            employmentPopUp.SelectSpecialty(addEmploymentData.Specialty);
            employmentPopUp.SelectJobType(addEmploymentData.JobType);
            employmentPopUp.EnterHoursPerWeek(addEmploymentData.Hours);
            employmentPopUp.EnterJobDescription(addEmploymentData.JobDescription);
            employmentPopUp.SelectDeselectWorkHereCheckbox(false);
            employmentPopUp.SelectStartDate(addEmploymentData);
            employmentPopUp.SelectEndDate(addEmploymentData);
            employmentPopUp.ClickOnAddEmploymentButton();
            const string expectedFacilityValidationMessage = "Please select a valid facility.";
            Assert.AreEqual(expectedFacilityValidationMessage, employmentPopUp.GetFacilityValidationMessage(), "'Add employment' pop-up is not present");

            Log.Info("Step 7: Clear 'Category' field and Click on 'Add employment' button & verify that 'Category' validation message is displayed");
            employmentPopUp.EnterFacilityName(addEmploymentData.Facility);
            employmentPopUp.SelectFacilityOption(addEmploymentData.Facility);
            employmentPopUp.ClearCategory();
            employmentPopUp.ClickOnAddEmploymentButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, employmentPopUp.GetCategoryValidationMessage(), "'Add employment' pop-up is not present");

            Log.Info("Step 8: Clear 'Start Date' field and Click on 'Add employment' button & verify that 'Start Date' validation message is displayed");
            employmentPopUp.SelectCategory(addEmploymentData.Category);
            employmentPopUp.SelectSpecialty(addEmploymentData.Specialty);
            employmentPopUp.ClearStartDate();
            employmentPopUp.ClickOnAddEmploymentButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, employmentPopUp.GetStartDateValidationMessage(), " Validation message is not displayed for Start-date");

            Log.Info("Step 9: Clear 'End Date' field and Click on 'Add employment' button & verify that 'End Date' validation message is displayed");
            employmentPopUp.SelectStartDate(addEmploymentData);
            employmentPopUp.ClearEndDate();
            employmentPopUp.ClickOnAddEmploymentButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, employmentPopUp.GetEndDateValidationMessage(), "Validation message is not displayed for End-date");

            Log.Info("Step 10: Clear 'Job Type' field and Click on 'Add employment' button & verify that 'Job Type' validation message is displayed");
            employmentPopUp.SelectEndDate(addEmploymentData);
            employmentPopUp.ClearJobType();
            employmentPopUp.ClickOnAddEmploymentButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, employmentPopUp.GetJobTypeValidationMessage(), "'Add employment' pop-up is not present");

            Log.Info("Step 11: Clear 'Hours Per Week' field and Click on 'Add employment' button & verify that 'Hours Per Week' validation message is displayed");
            employmentPopUp.SelectJobType(addEmploymentData.JobType);
            employmentPopUp.EnterHoursPerWeek("");
            employmentPopUp.ClickOnAddEmploymentButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, employmentPopUp.GetHoursPerWeekValidationMessage(), "'Add employment' pop-up is not present");

            Log.Info("Step 12: Clear 'Job Description' field and Click on 'Add employment' button & verify that 'Job Description' validation message is displayed");
            employmentPopUp.EnterHoursPerWeek(addEmploymentData.Hours);
            employmentPopUp.EnterJobDescription("");
            employmentPopUp.ClickOnAddEmploymentButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, employmentPopUp.GetJobDescriptionValidationMessage(), "'Add employment' pop-up is not present");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyUserCanSelectOtherFacilityOptionManually()
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

            Log.Info("Step 5: Click on 'Add Position' button & fill employment form");
            employmentDetails.ClickOnAddPositionButton();
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetailsWithOtherFacility();
            employmentPopUp.EnterAddEmploymentData(addEmploymentData);
            employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 6: Verify Details are displayed correctly on 'Profile Detail' page");
            var actualUpdatedDataOnProfileDetailPage = employmentDetails.GetEmploymentDetailsFromProfileDetailPage();
            Assert.AreEqual(addEmploymentData.OtherFacility, actualUpdatedDataOnProfileDetailPage.OtherFacility, "The 'Facility' doesn't updated");
            Assert.AreEqual(addEmploymentData.Category, actualUpdatedDataOnProfileDetailPage.Category, "The 'Department' doesn't updated ");
            Assert.AreEqual(addEmploymentData.Specialty, actualUpdatedDataOnProfileDetailPage.Specialty, "The 'Specialty' doesn't updated");
            var actualJobSettingInput = actualUpdatedDataOnProfileDetailPage.JobSettingInput;
            var actualJobSettingInputList = string.Join(Environment.NewLine, actualJobSettingInput.ToArray()).Split(", ");
            CollectionAssert.AreEquivalent(addEmploymentData.JobSettingInput.ToList(), actualJobSettingInputList.ToList(), "Job setting list is not matched");
            Assert.AreEqual(addEmploymentData.Hours, actualUpdatedDataOnProfileDetailPage.Hours, "The 'Hours per week' doesn't updated");
            addEmploymentData.ChartingSystemInput.Add(addEmploymentData.OtherChartingSystems);
            var chartingSystemsInput = addEmploymentData.ChartingSystemInput;
            var actualChartingSystemInput = actualUpdatedDataOnProfileDetailPage.ChartingSystemInput;
            foreach (var a in actualChartingSystemInput)
            {
                foreach (var t in chartingSystemsInput)
                {
                    Assert.IsTrue(a.Contains(t), "The 'Charting Systems' value doesn't match");
                }
            }
            Assert.AreEqual(addEmploymentData.UnitAmount, actualUpdatedDataOnProfileDetailPage.UnitAmount, "The 'Unit Amount' doesn't match");
            Assert.AreEqual(addEmploymentData.UnitType, actualUpdatedDataOnProfileDetailPage.UnitType, "The 'Unit Type' does not match");
            Assert.AreEqual(addEmploymentData.JobDescription.RemoveWhitespace(), actualUpdatedDataOnProfileDetailPage.JobDescription.RemoveWhitespace(), "The 'Job Description' doesn't updated");
            Assert.AreEqual("1:" + addEmploymentData.PatientRatio, actualUpdatedDataOnProfileDetailPage.PatientRatio, "The 'Patient Ratio' value does not match");
            Assert.AreEqual(addEmploymentData.City, actualUpdatedDataOnProfileDetailPage.City, "City is not matched");
            Assert.AreEqual(addEmploymentData.State, actualUpdatedDataOnProfileDetailPage.State, "State is not matched");

            Log.Info("Step 7: Click on 'Edit' button and verify Details are displayed correctly");
            employmentPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();
            employmentDetails.ClickOnEditButton();
            var actualEmploymentData = employmentPopUp.GetEmploymentDetails(addEmploymentData);
            Assert.IsTrue(actualEmploymentData.FacilityOption.StartsWith(actualEmploymentData.FacilityOption), "The 'Facility' option doesn't match");
            CollectionAssert.AreEqual(addEmploymentData.JobSettingInput, actualEmploymentData.JobSettingInput, "Job Settings values doesn't match");
            //Assert.AreEqual(addEmploymentData.OtherFacility,actualEmploymentData.OtherFacility,"Other facility is not matched");
            //Assert.AreEqual(addEmploymentData.City,actualEmploymentData.City,"City is not matched");
            //Assert.AreEqual(addEmploymentData.State, actualEmploymentData.State, "State is not matched");
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
    }
}
