using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Traveler.Profile.Licensing;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.Licensing
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class LicenseTests : FmpBaseTest
    {
        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteLicenseDetails();
        }

        [TestMethod]
        [TestCategory("Smoke"),TestCategory("MobileReady")]
        public void License_VerifyThatLicenseDetailsAddedSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var licensingDetail = new LicenseDetailsPo(Driver);
            var addLicensePopup = new AddLicensingPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            addLicensePopup.WaitUntilFmpTextLoadingIndicatorInvisible();
            profileDetails.NavigateToPage();

            Log.Info("Step 4: Click on 'Add Licensing' button & verify 'Add License' popup is opened");
            profileDetails.ClickOnLicensingTab();
            licensingDetail.ClickOnAddLicensingButton();
            Assert.IsTrue(addLicensePopup.IsAddLicensePopupHeaderTextDisplayed(), "'Add License' Pop-up is not present");

            Log.Info("Step 5: Enter 'Add license' details");
            var addLicenseData = LicenseDataFactory.AddLicenseDetails();
            addLicensePopup.AddLicenseData(addLicenseData);
            addLicensePopup.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 6: Verify Added License details are displayed correctly on 'Licensing' page");
            var actualLicenseDetail = licensingDetail.GetLicenseData();
            Assert.AreEqual(addLicenseData.State, actualLicenseDetail.State, "State  does not match.");
            Assert.AreEqual(addLicenseData.Compact, actualLicenseDetail.Compact, "License isn't Compact");
            Assert.AreEqual(addLicenseData.ExpirationDate.ToString("MM/yyyy"), actualLicenseDetail.ExpirationDate.ToString("MM/yyyy"), "ExpirationDate doesn't match.");
            Assert.AreEqual(addLicenseData.LicenseNumber, actualLicenseDetail.LicenseNumber, "License number doesn't match.");

            Log.Info("Step 7: Click on 'Edit' button and verify Details are displayed correctly on 'Edit License' pop-up.");
            licensingDetail.ClickOnEditLicensingButton();
            const string expectedEditLicenseHeaderText = "Edit License";
            var actualEditLicenseHeaderText = addLicensePopup.GetEditLicensePopUpHeaderText();
            Assert.AreEqual(expectedEditLicenseHeaderText, actualEditLicenseHeaderText, "The 'Edit License' header text doesn't match");

            var actualLicenseData = addLicensePopup.GetLicensingData();
            Assert.AreEqual(addLicenseData.State, actualLicenseData.State, " State doesn't match");
            Assert.AreEqual(addLicenseData.Compact, actualLicenseData.Compact, " License isn't Compact");
            Assert.AreEqual(addLicenseData.ExpirationDate.ToString("MMMM/yyyy"), actualLicenseDetail.ExpirationDate.ToString("MMMM/yyyy"), "ExpirationDate doesn't match.");
            Assert.AreEqual(addLicenseData.LicenseNumber, actualLicenseData.LicenseNumber, " License number doesn't match");

            //Clean up
            try
            {
                addLicensePopup.ClickOnDeleteLicenseButton();
                addLicensePopup.WaitUntilFmpPageLoadingIndicatorInvisible();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void License_VerifyThatLicenseDetailUpdatedSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);
            var licensingDetail = new LicenseDetailsPo(Driver);
            var addLicensePopup = new AddLicensingPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            addLicensePopup.WaitUntilFmpTextLoadingIndicatorInvisible();
            profileDetails.NavigateToPage();

            Log.Info("Step 4: Click on 'Add Licensing' button ");
            profileDetailPage.ClickOnLicensingTab();
            licensingDetail.ClickOnAddLicensingButton();

            Log.Info("Step 5: Enter 'Add license' details");
            var addLicenseData = LicenseDataFactory.AddLicenseDetails();
            addLicensePopup.AddLicenseData(addLicenseData);
            addLicensePopup.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 6: Click on 'Edit' button and Verify Details are displayed correctly on license detail page.");
            licensingDetail.ClickOnEditLicensingButton();
            var updatedLicenseData = LicenseDataFactory.EditLicenseDetails();
            addLicensePopup.AddLicenseData(updatedLicenseData);
            addLicensePopup.WaitUntilFmpPageLoadingIndicatorInvisible();
            var actualUpdatedLicenseData = licensingDetail.GetLicenseData();
            Assert.AreEqual(updatedLicenseData.State, actualUpdatedLicenseData.State, "State  does not match.");
            Assert.AreEqual(updatedLicenseData.Compact, actualUpdatedLicenseData.Compact, "License is not Compact.");
            Assert.AreEqual(updatedLicenseData.ExpirationDate.ToString("MM/yyyy"), actualUpdatedLicenseData.ExpirationDate.ToString("MM/yyyy"), "ExpirationDate doesn't match.");
            Assert.AreEqual(updatedLicenseData.LicenseNumber, actualUpdatedLicenseData.LicenseNumber, "License number doesn't match.");

            Log.Info("Step 7: Click on 'Edit' button and verify Details are displayed on 'Edit License' pop-up correctly");
            licensingDetail.ClickOnEditLicensingButton();
            actualUpdatedLicenseData = addLicensePopup.GetLicensingData();
            Assert.AreEqual(updatedLicenseData.State, actualUpdatedLicenseData.State, "State  does not match.");
            Assert.AreEqual(updatedLicenseData.Compact, actualUpdatedLicenseData.Compact, "License is not Compact.");
            Assert.AreEqual(updatedLicenseData.ExpirationDate.ToString("MMMM/yyyy"), actualUpdatedLicenseData.ExpirationDate.ToString("MMMM/yyyy"), "Expiration Date  does not match.");
            Assert.AreEqual(updatedLicenseData.LicenseNumber, actualUpdatedLicenseData.LicenseNumber, "License number does not match.");

            //Clean up
            try
            {
                addLicensePopup.ClickOnDeleteLicenseButton();
                addLicensePopup.WaitUntilFmpPageLoadingIndicatorInvisible();
            }
            catch
            {
                //Do nothing
            }

        }

        [TestMethod]
        [TestCategory("Smoke"),TestCategory("MobileReady")]
        public void License_VerifyThatLicensingDetailDeletedSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);
            var licensingDetail = new LicenseDetailsPo(Driver);
            var addLicensePopup = new AddLicensingPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            addLicensePopup.WaitUntilFmpTextLoadingIndicatorInvisible();
            profileDetails.NavigateToPage();

            Log.Info("Step 4: Edit License details and Verify Licensing detail is present on 'Licensing detail' page");
            profileDetailPage.ClickOnLicensingTab();
            licensingDetail.ClickOnAddLicensingButton();
            var addLicenseData = LicenseDataFactory.AddLicenseDetails();
            addLicensePopup.AddLicenseData(addLicenseData);
            addLicensePopup.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(licensingDetail.IsLicensingPresent(addLicenseData.LicenseNumber), "License number is not present");

            Log.Info("Step 5: Delete License details and Verify that Licensing detail is not present on 'Licensing detail' page.");
            licensingDetail.ClickOnEditLicensingButton();
            addLicensePopup.WaitUntilFmpPageLoadingIndicatorInvisible();
            addLicensePopup.ClickOnDeleteLicenseButton();
            addLicensePopup.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsFalse(licensingDetail.IsLicensingPresent(addLicenseData.LicenseNumber), "License number is present");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void License_VerifyThatCloseIconAndCancelButtonWorkSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);
            var licensingDetail = new LicenseDetailsPo(Driver);
            var addLicensePopup = new AddLicensingPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            addLicensePopup.WaitUntilFmpTextLoadingIndicatorInvisible();
            profileDetails.NavigateToPage();

            Log.Info("Step 4: Click on 'Add Licensing' button");
            profileDetailPage.ClickOnLicensingTab();
            licensingDetail.ClickOnAddLicensingButton();

            Log.Info("Step 5: Click on 'Close Icon' button and Verify 'Add License' Pop - up is closed.");
            addLicensePopup.ClickOnCloseIcon();
            Assert.IsFalse(addLicensePopup.IsLicensePopUpPresent(), "'Add License' Pop-up is Open.");

            Log.Info("Step 6: Click on 'Add Licensing' button ");
            profileDetailPage.ClickOnLicensingTab();
            licensingDetail.ClickOnAddLicensingButton();

            Log.Info("Step 7: Click on 'Cancel Button' and Verify 'Add License' Pop - up is closed.");
            addLicensePopup.ClickOnCancelButton();
            Assert.IsFalse(addLicensePopup.IsLicensePopUpPresent(), "'Add License' Pop-up is Open.");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void License_VerifyValidationMessageIsDisplayedOnMandatoryFields()
        {
            var profileDetails = new ProfileDetailPagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);
            var addLicensePopup = new AddLicensingPo(Driver);
            var licensingDetail = new LicenseDetailsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            headerHomePagePo.ClickOnLogInButton();
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            addLicensePopup.WaitUntilFmpTextLoadingIndicatorInvisible();
            profileDetails.NavigateToPage();

            Log.Info("Step 3: Click to 'Licensing tab' & click on 'Add Licensing' button");
            profileDetailPage.ClickOnLicensingTab();
            licensingDetail.ClickOnAddLicensingButton();

            Log.Info("Step 4: Enter 'Add license' details");
            var addLicenseData = LicenseDataFactory.AddLicenseDetails();

            Log.Info("Step 5: Clear 'State' drop-down field and Click on 'Add license' button  and Verify Validation message is displayed for State field ");
            addLicensePopup.SelectState(addLicenseData.State);
            addLicensePopup.ClearStateDropDown();
            addLicensePopup.EnterExpirationDate(addLicenseData);
            addLicensePopup.EnterLicensingNumber(addLicenseData.LicenseNumber);
            addLicensePopup.ClickOnAddLicenseButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage,addLicensePopup.GetValidationMessageDisplayedForStateField(), "Validation message is not displayed for State field");

            Log.Info("Step 6: Clear 'Expiration Date' and Click on 'Add license' button  and Verify Validation message is displayed for Expiration date field ");
            addLicensePopup.SelectState(addLicenseData.State);
            addLicensePopup.ClearExpirationDate();
            addLicensePopup.ClickOnAddLicenseButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, addLicensePopup.GetValidationMessageDisplayedForExpirationDateField(), "Validation message is not displayed for Expiration date field");

            Log.Info("Step 7: Clear 'License' field and Click on 'Add license' button  and Verify Validation message is displayed for License number field ");
            addLicensePopup.EnterExpirationDate(addLicenseData);
            addLicensePopup.EnterLicensingNumber("");
            addLicensePopup.ClickOnAddLicenseButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage,addLicensePopup.GetValidationMessageDisplayedForLicenseNumberField(), "Validation message is not displayed for License number field");
        }
    }
}
