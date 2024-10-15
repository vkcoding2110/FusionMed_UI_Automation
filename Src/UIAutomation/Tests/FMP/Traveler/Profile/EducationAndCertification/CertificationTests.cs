using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
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
    public class CertificationTests : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("CertificationTest");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteAllEducationAndCertificationsDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void Profile_Certification_VerifyThatAddCertificationWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var educationAndCertificationDetailPage = new EducationAndCertificationDetailPo(Driver);
            var certificationTab = new CertificationPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Education & Certification' section tab button & Click on 'Add Education or Certification' button");
            educationAndCertificationDetailPage.ClickOnEducationAndCertificationSectionTabButton();

            Log.Info("Step 6: Click on 'Add Certification' tab");
            certificationTab.ClickOnAddCertificationTab();

            Log.Info("Step 7: Enter 'Add Certification' details & click on 'Add Certification' button");
            var addCertificationData = CertificationDataFactory.AddDataInCertification();
            certificationTab.EnterCertificationData(addCertificationData);
            certificationTab.ClickOnAddCertificationButton();
            certificationTab.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 8: Verify Details are displayed correctly on 'Profile Detail' page");
            educationAndCertificationDetailPage.ScrollToEducationAndCertificationTab();
            var actualDataOnDetailPage = educationAndCertificationDetailPage.GetCertificationDetailsFromDetailsPage();
            Assert.AreEqual(addCertificationData.Category, actualDataOnDetailPage.Category, "The 'Category' option doesn't match");
            Assert.AreEqual(addCertificationData.CertificationFullName, actualDataOnDetailPage.CertificationFullName, "The 'Certification' option doesn't match");
            Assert.AreEqual(addCertificationData.ExpirationDate.ToString("MM/yyyy"), actualDataOnDetailPage.ExpirationDate.ToString("MM/yyyy"), "The 'Expiration Date' option doesn't match");

            Log.Info("Step 9: Click on 'Edit' button & verify Details are displayed correctly");
            educationAndCertificationDetailPage.ClickOnEditButton();
            var actualCertificationData = certificationTab.GetCertificationDetails();
            Assert.AreEqual(addCertificationData.Category, actualCertificationData.Category, "The 'Category' option doesn't match");
            Assert.AreEqual(addCertificationData.CertificationName, actualCertificationData.CertificationName, "The 'CertificationName' value doesn't match");
            Assert.AreEqual(addCertificationData.ExpirationDate.ToString("MMMM/yyyy"), actualCertificationData.ExpirationDate.ToString("MMMM/yyyy"), "The 'Expiration Date' option doesn't match");

            try
            {
                certificationTab.ClickOnDeleteCertificationButton();
            }
            catch
            {
                //Nothing
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void Profile_Certification_VerifyThatEditCertificationWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var educationAndCertificationDetailPage = new EducationAndCertificationDetailPo(Driver);
            var certificationTab = new CertificationPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            certificationTab.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Education & Certification' section tab button & Click on 'Add Education or Certification' button");
            educationAndCertificationDetailPage.ClickOnEducationAndCertificationSectionTabButton();

            Log.Info("Step 6: Click on 'Add Certification' tab");
            certificationTab.ClickOnAddCertificationTab();

            Log.Info("Step 7: Enter 'Add Certification' details & click on 'Add Certification' button");
            var addCertificationData = CertificationDataFactory.AddDataInCertification();
            certificationTab.EnterCertificationData(addCertificationData);
            certificationTab.ClickOnAddCertificationButton();
            certificationTab.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 8: Click on 'Edit' button & update 'certification' details & click on 'Submit' button");
            educationAndCertificationDetailPage.ScrollToEducationAndCertificationTab();
            educationAndCertificationDetailPage.ClickOnEditButton();
            var editCertificationData = CertificationDataFactory.EditDataInCertification();
            certificationTab.EnterCertificationData(editCertificationData);
            certificationTab.ClickOnSubmitButton();
            certificationTab.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 9: Verify updated Details are displayed correctly on 'Profile Detail' page");
            educationAndCertificationDetailPage.ScrollToEducationAndCertificationTab();
            var actualUpdatedDataOnDetailPage = educationAndCertificationDetailPage.GetCertificationDetailsFromDetailsPage();
            Assert.AreEqual(editCertificationData.Category, actualUpdatedDataOnDetailPage.Category, "The 'Category' option doesn't match");
            Assert.AreEqual(editCertificationData.CertificationFullName, actualUpdatedDataOnDetailPage.CertificationFullName, "The 'Category' option doesn't match");
            Assert.AreEqual(editCertificationData.ExpirationDate.ToString("MM/yyyy"), actualUpdatedDataOnDetailPage.ExpirationDate.ToString("MM/yyyy"), "The 'Expiration Date' option doesn't match");

            Log.Info("Step 10: Click on 'Edit' button & verify Details are displayed correctly");
            educationAndCertificationDetailPage.ClickOnEditButton();
            var actualUpdatedCertificationData = certificationTab.GetCertificationDetails();
            Assert.AreEqual(editCertificationData.Category, actualUpdatedCertificationData.Category, "The 'Category' option doesn't match");
            Assert.AreEqual(editCertificationData.CertificationName, actualUpdatedCertificationData.CertificationName, "The 'CertificationName' value doesn't match");
            Assert.AreEqual(editCertificationData.ExpirationDate.ToString("MMMM/yyyy"), actualUpdatedCertificationData.ExpirationDate.ToString("MMMM/yyyy"), "The 'Expiration Date' option doesn't match");

            try
            {
                certificationTab.ClickOnDeleteCertificationButton();
            }
            catch
            {
                //Nothing
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Profile_Certification_VerifyThatAddCertificationValidationMessageIsDisplayedOnMandatoryFields()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var educationAndCertificationDetailPage = new EducationAndCertificationDetailPo(Driver);
            var certificationTab = new CertificationPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            certificationTab.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Education & Certification' section tab button & Click on 'Add Education or Certification' button");
            educationAndCertificationDetailPage.ClickOnEducationAndCertificationSectionTabButton();

            Log.Info("Step 6: Click on 'Add CertificationName' tab");
            certificationTab.ClickOnAddCertificationTab();

            Log.Info("Step 7: Do not select option from 'Category' dropdown & click on 'Add Certification' button & verify 'Category' Validation message is displayed");
            var enterData = CertificationDataFactory.AddDataInCertification();
            certificationTab.ClickOnAddCertificationButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, certificationTab.GetCategoryValidationMessage(), "Validation message is not displayed Category field");

            Log.Info("Step 8: Do not select option from 'Certification' dropdown & click on 'Add Certification' button & verify 'Certification' Validation message is displayed");
            certificationTab.SelectCategoryOption(enterData.Category);
            certificationTab.ClickOnAddCertificationButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, certificationTab.GetCertificationValidationMessage(), "Validation message is not displayed for Certification field");

        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void Profile_Certification_VerifyThatDeleteCertificationButtonWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var educationAndCertificationDetailPage = new EducationAndCertificationDetailPo(Driver);
            var certificationTab = new CertificationPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            certificationTab.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Education & Certification' section tab button & Click on 'Add Education or Certification' button");
            educationAndCertificationDetailPage.ClickOnEducationAndCertificationSectionTabButton();

            Log.Info("Step 6: Click on 'Add CertificationName' tab");
            certificationTab.ClickOnAddCertificationTab();

            Log.Info("Step 7: Enter 'Add Certification' details & click on 'Add Certification' button");
            var addCertificationData = CertificationDataFactory.AddDataInCertification();
            certificationTab.EnterCertificationData(addCertificationData);
            certificationTab.ClickOnAddCertificationButton();
            certificationTab.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 8: Click on 'Edit' button");
            educationAndCertificationDetailPage.ClickOnEditButton();

            Log.Info("Step 9: Click on 'delete certification' button and verify education details are removed");
            certificationTab.ClickOnDeleteCertificationButton();
            Assert.IsFalse(educationAndCertificationDetailPage.IsEducationHeaderTextDisplayed(addCertificationData.CertificationName), "The Certification details are still displayed");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Profile_Certification_VerifyThatAddCertificationCloseIconAndCancelButtonWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var educationAndCertificationDetailPage = new EducationAndCertificationDetailPo(Driver);
            var certificationTab = new CertificationPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            certificationTab.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'Education & Certification' section tab button & Click on 'Add Education or Certification' button");
            educationAndCertificationDetailPage.ClickOnEducationAndCertificationSectionTabButton();

            Log.Info("Step 6: Click on 'Add CertificationName' tab");
            certificationTab.ClickOnAddCertificationTab();

            Log.Info("Step 7: Click on 'Close' icon & verify 'Add Certification' popup is closed");
            certificationTab.ClickOnCertificationCloseIcon();
            Assert.IsFalse(certificationTab.IsCertificationPopupPresent(), "The certification popup is not closed");

            Log.Info("Step 8: Click on 'Add Certification' tab");
            educationAndCertificationDetailPage.ClickOnAddEducationOrCertificationButton();
            certificationTab.ClickOnAddCertificationTab();

            Log.Info("Step 9: Click on 'Cancel' button & verify 'Add Certification' popup is closed");
            certificationTab.ClickOnCertificationCancelButton();
            Assert.IsFalse(certificationTab.IsCertificationPopupPresent(), "The certification popup is not closed");

        }
    }
}
