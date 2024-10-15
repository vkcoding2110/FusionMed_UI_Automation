using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account.NativeApp;
using UIAutomation.PageObjects.FMP.Home.NativeApp;
using UIAutomation.PageObjects.FMP.NativeApp.More;
using UIAutomation.PageObjects.FMP.Traveler.Profile.EducationAndCertification.NativeApp;
using UIAutomation.PageObjects.FMP.Traveler.Profile.NativeApp;
using UIAutomation.SetUpTearDown.FMP.NativeApp;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.EducationAndCertification.NativeApp
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("NativeAppAndroid")]
    public class CertificationTests : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("CertificationTest");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteAllCertificationDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void Profile_Certification_VerifyAddCertificationWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var certification = new CertificationPo(Driver);
            var homePage = new HomePagePo(Driver);
            var profile = new ProfileDetailPagePo(Driver);
            var moreMenu = new MoreMenuPo(Driver);


            Log.Info("Step 1: Open FMP App & Login to the application");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on 'Profile' icon and verify Profile page open Successfully");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            Assert.IsTrue(profile.IsProfilePageOpen(), "Profile page is not opened!");

            Log.Info("Step 3: Click on 'Add Education Or Certification' button and verify 'AddEducation Or Certification' page open Successfully");
            profile.ClickOnAddEducationOrCertificationButton();
            Assert.IsTrue(profile.IsAddEducationOrCertificationPopupIsDisplay(), "'Add Education Or Certification' pop up is not opened!");

            Log.Info("Step 4: Click on 'Add certification' tab, Enter 'Add Certification' details and  Click on 'Add Certification' button");
            var addCertificationData = CertificationDataFactory.AddDataInCertification();
            certification.ClickOnCertificationTab();
            certification.EnterCertificationData(addCertificationData);
            certification.ClickOnAddCertification();

            Log.Info("Step 5: Verify 'Certification' details are displayed correctly on 'Profile Detail' page");
            var certificationData = profile.GetCertificationDetailsFromProfileDetailPage();
            Assert.AreEqual(addCertificationData.CertificationFullName, certificationData.CertificationName, "The 'Certification Name' value doesn't match");
            Assert.AreEqual(addCertificationData.Category, certificationData.Category, "The 'Category' value doesn't match");
            Assert.AreEqual(addCertificationData.ExpirationDate.ToString("MM/yyyy"), certificationData.ExpirationDate.ToString("MM/yyyy"), "The 'Graduated Date' value doesn't match");

            try
            {
                certification.DeleteCertification();
            }
            catch
            {
                //Nothing
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void Profile_Certification_VerifyThatDeleteCertificationButtonWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var certification = new CertificationPo(Driver);
            var homePage = new HomePagePo(Driver);
            var profile = new ProfileDetailPagePo(Driver);
            var moreMenu = new MoreMenuPo(Driver);

            Log.Info("Step 1: Open FMP App & Login to the application");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on 'Profile' icon and Click on 'Add Education Or Certification' button");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            profile.ClickOnAddEducationOrCertificationButton();

            Log.Info("Step 4: Enter 'Add Certification' details and  Click on 'Certification' button");
            var addCertificationData = CertificationDataFactory.AddDataInCertification();
            certification.ClickOnCertificationTab();
            certification.EnterCertificationData(addCertificationData);
            certification.ClickOnAddCertification();

            Log.Info("Step 5: Click on 'Edit' button, click on 'Delete' button and verify certification details are removed");
            certification.ClickOnEditButton();
            certification.ClickOnDeleteCertification();
            Assert.IsFalse(profile.IsCertificationLabelPresent(), "The Certification details are still displayed");
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void Profile_Certification_VerifyThatEditCertificationWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var certification = new CertificationPo(Driver);
            var homePage = new HomePagePo(Driver);
            var profile = new ProfileDetailPagePo(Driver);
            var moreMenu = new MoreMenuPo(Driver);

            Log.Info("Step 1: Open FMP App & Login to the application");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on 'Profile' icon and Click on 'Add Education Or Certification' button ");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            profile.ClickOnAddEducationOrCertificationButton();

            Log.Info("Step 4: Click on 'Add certification' tab, Enter 'Add Certification' details and  Click on 'Add Certification' button");
            var addCertificationData = CertificationDataFactory.AddDataInCertification();
            certification.ClickOnCertificationTab();
            certification.EnterCertificationData(addCertificationData);
            certification.ClickOnAddCertification();

            Log.Info("Step 6: Click on 'Edit' button and update 'Certification' details");
            certification.ClickOnEditButton();
            var editCertificationData = CertificationDataFactory.EditDataInCertification();
            certification.EnterCertificationData(editCertificationData);
            certification.ClickOnAddCertification();

            Log.Info("Step 5: Verify Details are displayed correctly on 'Profile Detail' page");
            var certificationData = profile.GetCertificationDetailsFromProfileDetailPage();
            Assert.AreEqual(editCertificationData.CertificationFullName, certificationData.CertificationName, "The 'Certification Name' value doesn't match");
            Assert.AreEqual(editCertificationData.Category, certificationData.Category, "The 'Category' value doesn't match");
            Assert.AreEqual(editCertificationData.ExpirationDate.ToString("MM/yyyy"), certificationData.ExpirationDate.ToString("MM/yyyy"), "The 'Graduated Date' value doesn't match");

            try
            {
                certification.DeleteCertification();
            }
            catch
            {
                //Nothing
            }
        }

        [TestMethod]
        public void Profile_Certification_VerifyThatAddCertificationValidationMessageIsDisplayedOnMandatoryFields()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var certification = new CertificationPo(Driver);
            var homePage = new HomePagePo(Driver);
            var profile = new ProfileDetailPagePo(Driver);
            var moreMenu = new MoreMenuPo(Driver);

            Log.Info("Step 1: Open FMP App & Login to the application");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on 'Profile' icon and Click on 'Add Education Or Certification' button ");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            profile.ClickOnAddEducationOrCertificationButton();

            Log.Info("Step 3: Click on 'Add certification' tab, Enter 'Add Certification' details and  Click on 'Add Certification' button");
            var addCertificationData = CertificationDataFactory.AddDataInCertification();
            certification.ClickOnCertificationTab();
            certification.EnterCertificationData(addCertificationData);
            certification.ClearCategory();
            certification.ClickOnAddCertification();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, certification.GetCategoryValidationMessageText(), "validation message is not displayed for Category");

            Log.Info("Step 4: Clear 'Certification' field and Click on 'Add Certification' button and Verify Validation message is displayed for Category field");
            certification.EnterCategory(addCertificationData.Category);
            certification.EnterCertification(addCertificationData.CertificationName);
            certification.ClearCertification();
            certification.ClickOnAddCertification();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, certification.GetCertificationValidationMessageText(), "validation message is not displayed for Certification");
        }

        [TestMethod]
        public void Profile_Certification_VerifyCertificationPopupOpenAndCloseWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var certification = new CertificationPo(Driver);
            var homePage = new HomePagePo(Driver);
            var profile = new ProfileDetailPagePo(Driver);
            var moreMenu = new MoreMenuPo(Driver);

            Log.Info("Step 1: Open FMP App & Login to the application");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on 'Profile' icon and Click on 'Add Education Or Certification' button ");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            profile.ClickOnAddEducationOrCertificationButton();

            Log.Info("Step 3: Click on 'CertificationTab' and verify 'Certification' pop up open Successfully");
            certification.ClickOnCertificationTab();
            Assert.IsTrue(certification.IsCertificationPopupDisplayed(), "'Certification' pop up is not opened!");

            Log.Info("Step 4: Click on 'cancel' icon and verify 'Certification' pop up close Successfully");
            certification.ClickOnAddEducationOrCertificationPopUpCancelIcon();
            Assert.IsFalse(certification.IsAddEducationOrCertificationPopupDisplayed(), "'Certification' pop up is opened!");

            Log.Info("Step 5: Click on 'cancel' button and verify 'Certification' pop up close Successfully");
            profile.ClickOnAddEducationOrCertificationButton();
            certification.ClickOnCertificationTab();
            certification.ClickOnAddEducationOrCertificationPopUpCancelButton();
            Assert.IsFalse(certification.IsAddEducationOrCertificationPopupDisplayed(), "'Certification' pop up is opened!");
        }
    }
}

