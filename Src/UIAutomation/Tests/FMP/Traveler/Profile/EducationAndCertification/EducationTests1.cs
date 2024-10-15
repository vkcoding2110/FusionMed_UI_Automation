using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Traveler.Profile.EducationAndCertification;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.EducationAndCertification
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class EducationTests1 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("EducationTest");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteAllEducationAndCertificationsDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void Profile_Education_VerifyThatAddEducationWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var educationPopUp = new EducationPo(Driver);
            var certificationPopUp = new CertificationPo(Driver);
            var educationDetails = new EducationAndCertificationDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Education & Certification' section tab button, verify 'Education & Certification' tab gets open, Click on 'Add Education or Certification' button & verify 'Add Education and Add Certification' popup gets open");
            Assert.IsTrue(educationDetails.IsAddEducationOrCertificationButtonDisplayed(), "The 'Add Education or Certification' button is not displayed");
            educationDetails.ClickOnEducationAndCertificationSectionTabButton();

            Assert.IsTrue(educationPopUp.IsEducationTabNameDisplayed(), "The 'Add Education' tab name doesn't display");
            Assert.IsTrue(certificationPopUp.IsCertificationTabNameDisplayed(), "The 'Add Certification' tab name doesn't display");

            Log.Info("Step 6: Enter 'Add Education' details & click on 'Add Education' button");
            var addEducationData = EducationDataFactory.AddDataInEducationForm();
            educationPopUp.EnterEducationData(addEducationData);
            educationPopUp.ClickOnAddEducationButton();
            educationPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 7: Verify Details are displayed correctly on 'Profile Detail' page");
            var actualDataOnDetailPage = educationDetails.GetEducationDetailsFromProfileDetailPage();
            Assert.AreEqual(addEducationData.InstitutionName, actualDataOnDetailPage.InstitutionName, "The 'Institution Name' value doesn't match");
            Assert.AreEqual(addEducationData.GraduatedDate.ToString("MM/yyyy"), actualDataOnDetailPage.GraduatedDate.ToString("MM/yyyy"), "The 'Graduated Date' value doesn't match");
            Assert.AreEqual(addEducationData.FieldOfStudy, actualDataOnDetailPage.FieldOfStudy, "The 'Field of Study' value doesn't match");
            Assert.AreEqual(addEducationData.DegreeDiploma, actualDataOnDetailPage.DegreeDiploma, "The 'Degree or Diploma' value doesn't match");

            Log.Info("Step 8: Click on 'Edit' button and verify Details are displayed correctly");
            educationDetails.ClickOnEditButton();
            var actualEducationData = educationPopUp.GetEducationDetails();
            Assert.AreEqual(addEducationData.State, actualEducationData.State, "The 'State' value doesn't match");
            Assert.AreEqual(addEducationData.DegreeDiploma, actualEducationData.DegreeDiploma, "The 'Degree or Diploma' value doesn't match");
            Assert.AreEqual(addEducationData.GraduatedDate.ToString("MMMM/yyyy"), actualEducationData.GraduatedDate.ToString("MMMM/yyyy"), "The 'Graduated Date' value doesn't match");

            if (PlatformName == PlatformName.Web || PlatformName == PlatformName.Ios)
            {
                Assert.AreEqual(addEducationData.InstitutionName, actualEducationData.InstitutionName, "The 'Institution Name' value doesn't match");
                Assert.AreEqual(addEducationData.City, actualEducationData.City, "The 'City' value doesn't match");
                Assert.AreEqual(addEducationData.FieldOfStudy, actualEducationData.FieldOfStudy, "The 'Field of Study' value doesn't match");
            }

            try
            {
                educationPopUp.ClickOnDeleteEducationButton();
            }
            catch
            {
                //Nothing
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void Profile_Education_VerifyThatEditEducationWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var educationPopUp = new EducationPo(Driver);
            var educationDetails = new EducationAndCertificationDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Education & Certification' section tab button & Click on 'Add Education or Certification' button");
            educationDetails.ClickOnEducationAndCertificationSectionTabButton();

            Log.Info("Step 6: Enter 'Add Education' details & click on 'Add Education' button");
            var addEducationData = EducationDataFactory.AddDataInEducationForm();
            educationPopUp.EnterEducationData(addEducationData);
            educationPopUp.ClickOnAddEducationButton();
            educationPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 7: Click on 'Edit' button & update 'Education' details");
            educationDetails.ClickOnEditButton();
            var editEducationData = EducationDataFactory.EditDataInEducationForm();
            educationPopUp.EnterEducationData(editEducationData);
            educationPopUp.ClickOnSubmitButton();
            educationPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 8: Verify updated Details are displayed correctly on 'Profile Detail' page");
            var actualUpdatedDataOnDetailPage = educationDetails.GetEducationDetailsFromProfileDetailPage();
            Assert.AreEqual(editEducationData.InstitutionName, actualUpdatedDataOnDetailPage.InstitutionName, "The 'Institution Name' value doesn't match");
            Assert.AreEqual(editEducationData.GraduatedDate.ToString("MM/yyyy"), actualUpdatedDataOnDetailPage.GraduatedDate.ToString("MM/yyyy"), "The 'Graduated Date' value doesn't match");
            Assert.AreEqual(editEducationData.FieldOfStudy, actualUpdatedDataOnDetailPage.FieldOfStudy, "The 'Field of Study' value doesn't match");
            Assert.AreEqual(editEducationData.DegreeDiploma, actualUpdatedDataOnDetailPage.DegreeDiploma, "The 'Degree or Diploma' value doesn't match");

            Log.Info("Step 9: Click on 'Edit' button and verify Details are displayed correctly");
            educationDetails.ClickOnEditButton();
            var actualUpdatedEducationData = educationPopUp.GetEducationDetails();
            Assert.AreEqual(editEducationData.InstitutionName, actualUpdatedEducationData.InstitutionName, "The 'Institution Name' value doesn't match");
            Assert.AreEqual(editEducationData.City, actualUpdatedEducationData.City, "The 'City' value doesn't match");
            Assert.AreEqual(editEducationData.State, actualUpdatedEducationData.State, "The 'State' value doesn't match");
            Assert.AreEqual(editEducationData.FieldOfStudy, actualUpdatedEducationData.FieldOfStudy, "The 'Field of Study' value doesn't match");
            Assert.AreEqual(editEducationData.DegreeDiploma, actualUpdatedEducationData.DegreeDiploma, "The 'Degree or Diploma' value doesn't match");
            Assert.AreEqual(editEducationData.GraduatedDate.ToString("MMMM/yyyy"), actualUpdatedEducationData.GraduatedDate.ToString("MMMM/yyyy"), "The 'Graduated Date' value doesn't match");
            try
            {
                educationPopUp.ClickOnDeleteEducationButton();
            }
            catch
            {
                //Nothing
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Profile_Education_VerifyAddEducationValidationMessageIsDisplayedOnMandatoryFields()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var educationPopUp = new EducationPo(Driver);
            var educationDetails = new EducationAndCertificationDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Education & Certification' section tab button & Click on 'Add Education or Certification' button");
            educationDetails.ClickOnEducationAndCertificationSectionTabButton();

            Log.Info("Step 6: Do not enter detail in 'Institution Name' field & click on 'Add Education' button & verify 'Institution Name' validation message is displayed");
            var enterDetail = EducationDataFactory.AddDataInEducationForm();
            educationPopUp.EnterFieldOfStudy(enterDetail.FieldOfStudy);
            educationPopUp.EnterCityName(enterDetail.City);
            educationPopUp.SelectState(enterDetail.State);
            educationPopUp.SelectDegreeDiploma(enterDetail.DegreeDiploma);
            educationPopUp.ClickOnAddEducationButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, educationPopUp.GetInstitutionNameValidationMessage(), "The 'Education' popup is not open");

            Log.Info("Step 7: Do not enter detail in 'Field of Study' field & click on 'Add Education' button & verify 'Field of Study' validation message is displayed");
            educationPopUp.EnterInstitutionName(enterDetail.InstitutionName);
            educationPopUp.EnterFieldOfStudy("");
            educationPopUp.EnterCityName(enterDetail.City);
            educationPopUp.SelectState(enterDetail.State);
            educationPopUp.SelectDegreeDiploma(enterDetail.DegreeDiploma);
            educationPopUp.ClickOnAddEducationButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, educationPopUp.GetFieldOfStudyValidationMessage(), "The 'Education' popup is not open");

            Log.Info("Step 8: Do not enter detail in 'City' field & click on 'Add Education' button & verify 'City' validation message is displayed");
            educationPopUp.EnterInstitutionName(enterDetail.InstitutionName);
            educationPopUp.EnterFieldOfStudy(enterDetail.FieldOfStudy);
            educationPopUp.EnterCityName("");
            educationPopUp.SelectState(enterDetail.State);
            educationPopUp.SelectDegreeDiploma(enterDetail.DegreeDiploma);
            educationPopUp.ClickOnAddEducationButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, educationPopUp.GetCityValidationMessage(), "The 'Education' popup is not open");

            Log.Info("Step 9: Do not enter detail in 'State' field & click on 'Add Education' button & verify 'State' validation message is displayed");
            educationPopUp.EnterInstitutionName(enterDetail.InstitutionName);
            educationPopUp.EnterFieldOfStudy(enterDetail.FieldOfStudy);
            educationPopUp.EnterCityName(enterDetail.City);
            educationPopUp.ClearStateOption();
            educationPopUp.SelectDegreeDiploma(enterDetail.DegreeDiploma);
            educationPopUp.ClickOnAddEducationButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, educationPopUp.GetStateValidationMessage(), "The 'Education' popup is not open");

            Log.Info("Step 10: Do not select option in 'Degree/Diploma' dropdown & click on 'Add Education' button & verify 'Degree/Diploma' validation message is displayed");
            educationPopUp.EnterInstitutionName(enterDetail.InstitutionName);
            educationPopUp.EnterFieldOfStudy(enterDetail.FieldOfStudy);
            educationPopUp.EnterCityName(enterDetail.City);
            educationPopUp.SelectState(enterDetail.State);
            educationPopUp.ClearDegreeOrDiplomaOption();
            educationPopUp.ClickOnAddEducationButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, educationPopUp.GetDegreeDiplomaValidationMessage(), "The 'Education' popup is not open");

            Log.Info("Step 11: Verify that education details are added");
            educationPopUp.SelectDegreeDiploma(enterDetail.DegreeDiploma);
            educationPopUp.ClickOnAddEducationButton();
            educationPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(educationDetails.IsEducationHeaderTextDisplayed(enterDetail.InstitutionName), "The 'Education' details are still not displayed");

            try
            {
                educationDetails.ClickOnEditButton();
                educationPopUp.ClickOnDeleteEducationButton();
            }
            catch
            {
                //Nothing
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void Profile_Education_VerifyThatDeleteEducationButtonWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var educationPopUp = new EducationPo(Driver);
            var educationDetails = new EducationAndCertificationDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Education & Certification' section tab button & Click on 'Add Education or Certification' button");
            educationDetails.ClickOnEducationAndCertificationSectionTabButton();

            Log.Info("Step 6: Enter 'Add Education' details & click on 'Add Education' button");
            var addEducationData = EducationDataFactory.AddDataInEducationForm();
            educationPopUp.EnterEducationData(addEducationData);
            educationPopUp.SelectCurrentlyAttendingCheckbox();
            educationPopUp.ClickOnAddEducationButton();
            educationPopUp.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 7: Click on 'Edit' button");
            educationDetails.ClickOnEditButton();

            Log.Info("Step 8: Click on 'delete education' button and verify education details are removed");
            educationPopUp.ClickOnDeleteEducationButton();
            Assert.IsFalse(educationDetails.IsEducationHeaderTextDisplayed(addEducationData.InstitutionName), "The 'Education' details are stiil displayed");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Profile_Education_VerifyThatAddEducationCloseIconAndCancelButtonWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var educationPopUp = new EducationPo(Driver);
            var educationDetails = new EducationAndCertificationDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Education & Certification' section tab button & Click on 'Add Education or Certification' button");
            educationDetails.ClickOnEducationAndCertificationSectionTabButton();

            Log.Info("Step 6: Click on 'Close' icon & verify 'Add Education' popup is closed");
            educationPopUp.ClickOnCloseIcon();
            Assert.IsFalse(educationPopUp.IsAddEducationPopUpPresent(), "The education popup is not closed");

            Log.Info("Step 7: Click on 'Add Education or Certification' button");
            educationDetails.ClickOnAddEducationOrCertificationButton();

            Log.Info("Step 8: Click on 'Cancel' button & verify 'Add Education' popup is closed");
            educationPopUp.ClickOnCancelButton();
            Assert.IsFalse(educationPopUp.IsAddEducationPopUpPresent(), "The education popup is not closed");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Profile_Education_VerifyThatGraduatedDateMonthAndYearDropDownIsEnabledOrDisabledAfterSelectingTheCurrentlyAttendingCheckbox()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var educationPopUp = new EducationPo(Driver);
            var educationDetails = new EducationAndCertificationDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Education & Certification' section tab button & Click on 'Add Education or Certification' button");
            educationDetails.ClickOnEducationAndCertificationSectionTabButton();

            Log.Info("Step 6: Select 'I am currently attending' checkbox and verify the date graduatedDate gets disabled");
            educationPopUp.SelectCurrentlyAttendingCheckbox();
            Assert.IsFalse(educationPopUp.IsDateGraduatedDateDisabled(), "The Graduated date is not disabled");

        }
    }
}
