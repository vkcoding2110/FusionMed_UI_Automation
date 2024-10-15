using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account.NativeApp;
using UIAutomation.PageObjects.FMP.Home.NativeApp;
using UIAutomation.PageObjects.FMP.NativeApp.More;
using UIAutomation.PageObjects.FMP.Traveler.Profile.Licensing.NativeApp;
using UIAutomation.SetUpTearDown.FMP.NativeApp;

namespace UIAutomation.Tests.FMP.Traveler.Profile.Licensing.NativeApp
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("NativeAppAndroid")]
    public class LicenseTests : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByType("LicenseTest");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteLicenseDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void License_VerifyThatLicenseDetailsAddedSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var moreMenu = new MoreMenuPo(Driver);
            var addLicensePopup = new AddLicensingPo(Driver);
            var licenseDetails = new LicenseDetailsPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on profile 'More' menu & click on 'Profile' icon");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();

            Log.Info("Step 3: Enter 'Add license' details");
            var addLicenseData = LicenseDataFactory.AddLicenseDetails();
            addLicensePopup.ClickOnAddLicensingButton();
            addLicensePopup.AddLicenseData(addLicenseData);
            addLicensePopup.ClickOnAddLicenseButton();

            Log.Info("Step 4: Verify Added License details are displayed correctly on 'Licensing' page");
            var actualLicenseData = licenseDetails.GetLicenseData();
            Assert.AreEqual(addLicenseData.State, actualLicenseData.State, " State doesn't match");
            Assert.AreEqual(addLicenseData.Compact, actualLicenseData.Compact, "License compact info doesn't match");
            Assert.AreEqual(addLicenseData.ExpirationDate.ToString("MMMM/yyyy"), actualLicenseData.ExpirationDate.ToString("MMMM/yyyy"), "ExpirationDate doesn't match.");
            Assert.AreEqual(addLicenseData.LicenseNumber, actualLicenseData.LicenseNumber, " License number doesn't match");

            Log.Info("Step 5: Click on 'Edit' button and verify details are displayed correctly on 'Edit License' pop-up.");
            licenseDetails.ClickOnEditButton();
            const string expectedEditLicenseHeaderText = "Edit License";
            var actualEditLicenseHeaderText = addLicensePopup.GetEditLicensePopUpHeaderText();
            Assert.AreEqual(expectedEditLicenseHeaderText, actualEditLicenseHeaderText, "The 'Edit License' header text doesn't match");

            var actualLicenseDataFromPopUp = addLicensePopup.GetLicenseData();
            Assert.AreEqual(addLicenseData.State, actualLicenseDataFromPopUp.State, "State  does not match.");
            Assert.AreEqual(addLicenseData.Compact, actualLicenseDataFromPopUp.Compact, "License compact info doesn't match");
            Assert.AreEqual(addLicenseData.ExpirationDate.ToString("MMMM"), actualLicenseDataFromPopUp.ExpirationMonth, "Expiration Month doesn't match.");
            Assert.AreEqual(addLicenseData.ExpirationDate.ToString("yyyy"), actualLicenseDataFromPopUp.ExpirationYear, "Expiration Year doesn't match.");

            try
            {
                licenseDetails.ClickOnDeleteLicense();
            }
            catch
            {
                //Nothing
            }
        }

        [TestMethod]
        public void License_VerifyThatLicensingDetailDeletedSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var moreMenu = new MoreMenuPo(Driver);
            var addLicensePopup = new AddLicensingPo(Driver);
            var licenseDetails = new LicenseDetailsPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on profile 'More' menu & click on 'Profile' icon");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();

            Log.Info("Step 3: Enter 'Add license' details. Verify that Licensing details is present on 'Licensing detail' page");
            var addLicenseData = LicenseDataFactory.AddLicenseDetails();
            addLicensePopup.ClickOnAddLicensingButton();
            addLicensePopup.AddLicenseData(addLicenseData);
            addLicensePopup.ClickOnAddLicenseButton();
            Assert.IsTrue(licenseDetails.IsLicensingDetailsPresent(), "License is not present");

            Log.Info("Step 4: Delete License details and Verify that Licensing details is not present on 'Licensing detail' page");
            licenseDetails.ClickOnEditButton();
            licenseDetails.ClickOnDeleteLicense();
            Assert.IsFalse(licenseDetails.IsLicensingDetailsPresent(), "License is present");
        }

        [TestMethod]
        public void License_VerifyThatLicenseDetailUpdatedSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var moreMenu = new MoreMenuPo(Driver);
            var addLicensePopup = new AddLicensingPo(Driver);
            var licenseDetails = new LicenseDetailsPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on profile 'More' menu & click on 'Profile' icon");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();

            Log.Info("Step 3: Enter 'Add license' details");
            var addLicenseData = LicenseDataFactory.AddLicenseDetails();
            addLicensePopup.ClickOnAddLicensingButton();
            addLicensePopup.AddLicenseData(addLicenseData);
            addLicensePopup.ClickOnAddLicenseButton();

            Log.Info("Step 4: Click on 'Edit' button and update 'License' details");
            licenseDetails.ClickOnEditButton();
            var updatedLicenseData = LicenseDataFactory.EditLicenseDetails();
            addLicensePopup.AddLicenseData(updatedLicenseData);
            addLicensePopup.ClickOnAddLicenseButton();

            Log.Info("Step 5: Verify Added License details are displayed correctly on 'License' page");
            var actualLicenseData = licenseDetails.GetLicenseData();
            Assert.AreEqual(updatedLicenseData.State, actualLicenseData.State, " State doesn't match");
            Assert.AreEqual(updatedLicenseData.Compact, actualLicenseData.Compact, "License compact info doesn't match");
            Assert.AreEqual(updatedLicenseData.ExpirationDate.ToString("MMMM/yyyy"), actualLicenseData.ExpirationDate.ToString("MMMM/yyyy"), "Expiration Date doesn't match.");
            Assert.AreEqual(updatedLicenseData.LicenseNumber, actualLicenseData.LicenseNumber, " License number doesn't match");

            try
            {
                licenseDetails.DeleteLicense();
            }
            catch
            {
                //Nothing
            }
        }

        [TestMethod]
        public void License_VerifyThatCloseIconAndCancelButtonWorkSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var moreMenu = new MoreMenuPo(Driver);
            var addLicensePopup = new AddLicensingPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on profile 'More' menu & click on 'Profile' icon");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();

            Log.Info("Step 3: Click on 'Add Licensing' button");
            addLicensePopup.ClickOnAddLicensingButton();
            Assert.IsTrue(addLicensePopup.IsAddLicenseCloseIconDisplayed(), "'Add Licensing' pop up is not opened!");

            Log.Info("Step 4: Click on 'Close' icon and Verify 'Add Licensing' gets open");
            addLicensePopup.ClickOnLicenseCloseIcon();
            Assert.IsFalse(addLicensePopup.IsLicensePopUpPresent(), "Add Licensing  pop up is opened!");

            Log.Info("Step 6: Click on 'Add Licensing' button");
            addLicensePopup.ClickOnAddLicensingButton();

            Log.Info("Step 7: Click on 'Cancel' Button and Verify 'Add License' Pop - up is closed.");
            addLicensePopup.ClickOnLicenseCancelButton();
            Assert.IsFalse(addLicensePopup.IsLicensePopUpPresent(), "'Add License' Pop-up is Open.");
        }
    }
}
