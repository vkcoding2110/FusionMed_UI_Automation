using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Traveler.Profile.Employment;
using UIAutomation.PageObjects.FMP.Traveler.Profile.References;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.References
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class ReferenceTests1 : FmpBaseTest
    {

        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("ReferenceTest");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteReferenceDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Reference_VerifyCloseAndCancelButtonWorkSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var referenceDetail = new ReferenceDetailPo(Driver);
            var referencePo = new ReferencePo(Driver);
            var employmentDetail = new EmploymentDetailsPo(Driver);
            var employmentPo = new EmploymentPo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Add employment detail if Employment history is not filled and Click on 'Add reference' button and Verify 'Add reference' pop-up displayed");
            profileDetail.ClickOnReferenceTab();
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            if (referenceDetail.IsAddEmploymentLinkDisplayed())
            {
                referenceDetail.ClickOnAddEmploymentHistoryLink();
                profileDetail.ClickOnEmploymentTab();
                employmentDetail.ClickOnAddPositionButton();
                employmentPo.EnterAddEmploymentData(addEmploymentData);
                employmentPo.WaitUntilFmpPageLoadingIndicatorInvisible();
                profileDetail.ClickOnReferenceTab();
            }
            referenceDetail.ClickOnAddReferenceButton();
            Assert.IsTrue(referencePo.IsAddReferencePopUpDisplayed(), "'Add reference' Pop-up is not displayed");

            Log.Info("Step 6: Click on 'Cancel' button and Verify 'Add reference' pop-up is closed");
            referencePo.ClickOnCancelButton();
            Assert.IsFalse(referencePo.IsAddReferencePopUpDisplayed(), "'Add reference' Pop-up is still open");

            Log.Info("Step 7: Click on 'Close' icon and Verify 'Add reference' pop-up is closed");
            referenceDetail.ClickOnAddReferenceButton();
            referencePo.ClickOnCloseIcon();
            Assert.IsFalse(referencePo.IsAddReferencePopUpDisplayed(), "'Add reference' Pop-up is still open");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void Reference_VerifyAddReferenceDetailWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var referenceDetail = new ReferenceDetailPo(Driver);
            var employmentDetail = new EmploymentDetailsPo(Driver);
            var referencePo = new ReferencePo(Driver);
            var employmentPo = new EmploymentPo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Add employment detail if Employment history is not filled");
            profileDetail.ClickOnReferenceTab();
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            if (referenceDetail.IsAddEmploymentLinkDisplayed())
            {
                referenceDetail.ClickOnAddEmploymentHistoryLink();
                employmentDetail.ClickOnAddPositionButton();
                employmentPo.EnterAddEmploymentData(addEmploymentData);
                employmentPo.WaitUntilFmpPageLoadingIndicatorInvisible();
                profileDetail.ClickOnReferenceTab();
            }
            referenceDetail.ClickOnAddReferenceButton();

            Log.Info("Step 6: Enter 'Add Reference' details & Verify Details are displayed correctly on 'Reference Detail' page");
            var referenceData = ReferencesDataFactory.AddReferenceDetail();
            referencePo.EnterReferenceData(referenceData);
            referencePo.WaitUntilFmpPageLoadingIndicatorInvisible();
            var actualReferenceData = referenceDetail.GetReferenceDetail();
            var actualEmploymentStartAndEndDate = referenceDetail.GetStartDateAndEndDate();
            Assert.AreEqual(referenceData.FirstName, actualReferenceData.FirstName, "First name does not match");
            Assert.AreEqual(referenceData.LastName, actualReferenceData.LastName, "Last name does not match");
            Assert.AreEqual(referenceData.Title, actualReferenceData.Title, "Title does not match");
            Assert.AreEqual(referenceData.Relationship, actualReferenceData.Relationship, "Relationship does not match");
            Assert.AreEqual(referenceData.Email, actualReferenceData.Email, "Email does not match");
            Assert.IsTrue(addEmploymentData.FacilityOption.StartsWith(actualReferenceData.WorkTogether), "The 'Facility' value does not match");
            Assert.AreEqual(addEmploymentData.StartDate.ToString("MM/yyyy"), actualEmploymentStartAndEndDate.StartDate.ToString("MM/yyyy"), "Start date does not match");

            Log.Info("Step 7: Click on 'Edit' button and Verify details are displayed correctly on 'Edit reference' pop-up");
            referenceDetail.ClickOnEditButton();
            var actualReferenceDetail = referencePo.GetReferenceData();
            Assert.AreEqual(referenceData.FirstName, actualReferenceDetail.FirstName, "First name is not matched");
            Assert.AreEqual(referenceData.LastName, actualReferenceDetail.LastName, "Last name is not matched");
            Assert.AreEqual(referenceData.Title, actualReferenceDetail.Title, "Title is not matched");
            Assert.AreEqual(referenceData.WorkTogether, actualReferenceDetail.WorkTogether, "Work together is not matched");
            Assert.AreEqual(referenceData.Relationship, actualReferenceDetail.Relationship, "Relationship is not matched");
            Assert.AreEqual(referenceData.PhoneNumber, actualReferenceDetail.PhoneNumber, "Phone number is not matched");
            Assert.AreEqual(referenceData.Email, actualReferenceDetail.Email, "Email is not matched");

            //Clean up
            try
            {
                referencePo.ClickOnDeleteReferenceButton();
                referencePo.WaitUntilFmpPageLoadingIndicatorInvisible();
                profileDetail.ClickOnEmploymentTab();
                employmentDetail.ClickOnEditButton();
                employmentPo.ClickOnDeleteEmploymentButton();
            }
            catch
            {
                //Do nothing
            }
        }
        [TestMethod]
        [TestCategory("Smoke")]
        public void Reference_VerifyUpdateReferenceDetailWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var referenceDetail = new ReferenceDetailPo(Driver);
            var employmentDetail = new EmploymentDetailsPo(Driver);
            var referencePo = new ReferencePo(Driver);
            var employmentPo = new EmploymentPo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Add employment detail if Employment history is not filled");
            profileDetail.ClickOnReferenceTab();
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            if (referenceDetail.IsAddEmploymentLinkDisplayed())
            {
                referenceDetail.ClickOnAddEmploymentHistoryLink();
                employmentDetail.ClickOnAddPositionButton();
                employmentPo.EnterAddEmploymentData(addEmploymentData);
                employmentPo.WaitUntilFmpPageLoadingIndicatorInvisible();
                profileDetail.ClickOnReferenceTab();
            }
            referenceDetail.ClickOnAddReferenceButton();

            Log.Info("Step 6: Enter 'Add Reference' details ");
            var referenceData = ReferencesDataFactory.AddReferenceDetail();
            referencePo.EnterReferenceData(referenceData);
            referencePo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 7: Update 'Employment' details ");
            profileDetail.ClickOnEmploymentTab();
            employmentDetail.ClickOnEditButton();
            var updateEmploymentData = EmploymentDataFactory.UpdateEmploymentFormDetails();
            employmentPo.EnterAddEmploymentData(updateEmploymentData);
            employmentPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 8: Click on 'Edit' button and Edit reference data, Verify edited reference detail is displayed correctly on Reference detail page");
            profileDetail.ClickOnReferenceTab();
            referenceDetail.ClickOnEditButton();
            var editReferenceData = ReferencesDataFactory.EditReferenceDetail();
            referencePo.EnterReferenceData(editReferenceData);
            var actualEditedReferenceData = referenceDetail.GetReferenceDetail();
            var actualEmploymentStartAndEndDate = referenceDetail.GetStartDateAndEndDate();
            Assert.AreEqual(editReferenceData.FirstName, actualEditedReferenceData.FirstName, "First name does not match");
            Assert.AreEqual(editReferenceData.LastName, actualEditedReferenceData.LastName, "Last name does not match");
            Assert.AreEqual(editReferenceData.Title, actualEditedReferenceData.Title, "Title does not match");
            Assert.AreEqual(editReferenceData.Relationship, actualEditedReferenceData.Relationship, "Relationship does not match");
            Assert.AreEqual(editReferenceData.Email, actualEditedReferenceData.Email, "Email does not match");
            Assert.AreEqual(updateEmploymentData.FacilityOption, actualEditedReferenceData.WorkTogether, "facility option does not match");
            Assert.AreEqual(updateEmploymentData.StartDate.ToString("MM/yyyy"), actualEmploymentStartAndEndDate.StartDate.ToString("MM/yyyy"), "Start date does not match");
            Assert.AreEqual(updateEmploymentData.EndDate.ToString("MM/yyyy"), actualEmploymentStartAndEndDate.EndDate.ToString("MM/yyyy"), "End date does not match");

            Log.Info("Step 9: Click on 'Edit' button and Verify edited reference detail is displayed correctly on Edit reference pop-up");
            referenceDetail.ClickOnEditButton();
            var actualReferenceDetail = referencePo.GetReferenceData();
            Assert.AreEqual(editReferenceData.FirstName, actualReferenceDetail.FirstName, "First name is not matched");
            Assert.AreEqual(editReferenceData.LastName, actualReferenceDetail.LastName, "Last name is not matched");
            Assert.AreEqual(editReferenceData.Title, actualReferenceDetail.Title, "Title is not matched");
            Assert.AreEqual(editReferenceData.WorkTogether, actualReferenceDetail.WorkTogether, "Work together is not matched");
            Assert.AreEqual(editReferenceData.Relationship, actualReferenceDetail.Relationship, "Relationship is not matched");
            Assert.AreEqual(editReferenceData.PhoneNumber, actualReferenceDetail.PhoneNumber, "Phone number is not matched");
            Assert.AreEqual(editReferenceData.Email, actualReferenceDetail.Email, "Email is not matched");

            //Clean up
            try
            {
                referencePo.ClickOnDeleteReferenceButton();
                profileDetail.ClickOnEmploymentTab();
                employmentDetail.ClickOnEditButton();
                employmentPo.ClickOnDeleteEmploymentButton();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void Reference_VerifyAddReferenceDetailDeletedSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var referenceDetail = new ReferenceDetailPo(Driver);
            var employmentDetail = new EmploymentDetailsPo(Driver);
            var referencePo = new ReferencePo(Driver);
            var employmentPo = new EmploymentPo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Add employment detail if Employment history is not filled");
            profileDetail.ClickOnReferenceTab();
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            if (referenceDetail.IsAddEmploymentLinkDisplayed())
            {
                referenceDetail.ClickOnAddEmploymentHistoryLink();
                employmentDetail.ClickOnAddPositionButton();
                employmentPo.EnterAddEmploymentData(addEmploymentData);
                employmentPo.WaitUntilFmpPageLoadingIndicatorInvisible();
                profileDetail.ClickOnReferenceTab();
            }
            referenceDetail.ClickOnAddReferenceButton();

            Log.Info("Step 6: Enter 'Add Reference' details & Verify Details are displayed");
            var referenceData = ReferencesDataFactory.AddReferenceDetail();
            referencePo.EnterReferenceData(referenceData);
            referencePo.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(referenceDetail.IsReferenceDetailDisplayed(), "Reference detail is not displayed");

            Log.Info("Step 7: Delete 'Add Reference' details & Verify Details are not displayed");
            referencePo.DeleteAllReferenceDetail();
            referenceDetail.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsFalse(referenceDetail.IsReferenceDetailDisplayed(), "Reference detail is displayed");
            
            //Clean up
            try
            {
                profileDetail.ClickOnEmploymentTab();
                employmentDetail.DeleteAllEmployment();
                employmentDetail.WaitUntilFmpPageLoadingIndicatorInvisible();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Reference_VerifyValidationMessageIsDisplayedOnMandatoryFields()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var referenceDetail = new ReferenceDetailPo(Driver);
            var employmentDetail = new EmploymentDetailsPo(Driver);
            var referencePo = new ReferencePo(Driver);
            var employmentPo = new EmploymentPo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Add employment detail if Employment history is not filled");
            profileDetail.ClickOnReferenceTab();
            var addEmploymentData = EmploymentDataFactory.AddEmploymentDetails();
            if (referenceDetail.IsAddEmploymentLinkDisplayed())
            {
                referenceDetail.ClickOnAddEmploymentHistoryLink();
                employmentDetail.ClickOnAddPositionButton();
                employmentPo.EnterAddEmploymentData(addEmploymentData);
                employmentPo.WaitUntilFmpPageLoadingIndicatorInvisible();
                profileDetail.ClickOnReferenceTab();
            }
            referenceDetail.ClickOnAddReferenceButton();

            Log.Info("Step 6: Clear 'First name' field and Verify Validation message is displayed for 'First name' field");
            var referenceData = ReferencesDataFactory.AddReferenceDetail();
            referencePo.EnterFirstName(referenceData.FirstName);
            referencePo.EnterFirstName("");
            referencePo.EnterLastName(referenceData.LastName);
            referencePo.EnterTitle(referenceData.Title);
            referencePo.SelectWorkTogetherPlaceDropDown(1);
            referencePo.SelectRelationshipDropDown(referenceData.Relationship);
            referencePo.ClickOnAddReferenceButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, referencePo.GetValidationMessageDisplayedForFirstNameField(), "Validation message is not displayed for 'First name' field");

            Log.Info("Step 7: Clear 'Last name' field and Verify Validation message is displayed for 'Last name' field");
            referencePo.EnterFirstName(referenceData.FirstName);
            referencePo.EnterLastName("");
            referencePo.ClickOnAddReferenceButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, referencePo.GetValidationMessageDisplayedForLastNameField(), "Validation message is not displayed for 'Last name' field");

            Log.Info("Step 8: Clear 'Title' field and Verify Validation message is displayed for 'Title' field");
            referencePo.EnterLastName(referenceData.LastName);
            referencePo.EnterTitle("");
            referencePo.ClickOnAddReferenceButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, referencePo.GetValidationMessageDisplayedForTitleField(), "Validation message is not displayed for 'Title' field");

            Log.Info("Step 9: Clear 'Work together' field and Verify Validation message is displayed for 'Work together' field");
            referencePo.EnterTitle(referenceData.Title);
            referencePo.ClearWorkTogetherDropDown();
            referencePo.ClickOnAddReferenceButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, referencePo.GetValidationMessageDisplayedForWorkTogetherField(), "Validation message is not displayed for 'Work together' field");

            Log.Info("Step 10: Clear 'Relationship' field and Verify Validation message is displayed for 'Relationship' field");
            referencePo.SelectWorkTogetherPlaceDropDown(1);
            referencePo.ClearRelationshipDropDown();
            referencePo.ClickOnAddReferenceButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, referencePo.GetValidationMessageDisplayedForRelationshipField(), "Validation message is not displayed for 'Relationship' field");

            Log.Info("Step 11: Do not enter 'Direct Phone number' field and Verify Validation message is displayed for 'Direct Phone number' field");
            referencePo.SelectRelationshipDropDown(referenceData.Relationship);
            referencePo.EnterPhoneNumber("");
            referencePo.ClickOnAddReferenceButton();
            Assert.AreEqual("*At least one contact field is required", referencePo.GetValidationMessageDisplayedForPhoneNumberField(), "Validation message is not displayed for 'Direct Phone number' field");

            Log.Info("Step 12: Do not enter 'Email' field and Verify Validation message is displayed for 'Email' field");
            referencePo.EnterEmail("");
            referencePo.ClickOnAddReferenceButton();
            Assert.AreEqual("*At least one contact field is required", referencePo.GetValidationMessageDisplayedForEmailField(), "Validation message is not displayed for 'Email' field");
        }
    }
}
