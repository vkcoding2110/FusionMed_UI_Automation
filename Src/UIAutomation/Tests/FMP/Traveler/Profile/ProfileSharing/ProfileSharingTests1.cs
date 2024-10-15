using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.Common;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Traveler.Profile.ProfileSharing;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.ProfileSharing
{

    [TestClass]
    [TestCategory("ProfileSharing"), TestCategory("FMP")]
    public class ProfileSharingTests1 : FmpBaseTest
    {
        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteProfileSharingDetails();
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]

        public void Profile_VerifyShareMyProfileDetailsWorksSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            headerHomePagePo.ClickOnProfileIcon();
            profilePage.ClickOnProfileMenuItem();
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 5: Click on 'Manage My Profile Sharing' button, click on 'Add Recipient Via Email' button and verify 'Share My Profile' button is disable");
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            profileSharing.ClickOAddRecipientViaEmailButton();
            Assert.IsFalse(profileSharing.IsShareMyProfileButtonEnabled(), "The 'Share My Profile' button is not disabled");

            Log.Info("Step 6: Add details in 'Share My Profile' form, click on 'Share My Profile' button and verify details added successfully");
            var recipientsEmail = UserDataFactory.AddUserInformation();
            profileSharing.AddShareMyProfileDetails(recipientsEmail.Email);
            var actualRecipientsEmail = profileSharing.GetAllowedEmailsText();
            Assert.AreEqual(recipientsEmail.Email, actualRecipientsEmail, "Share Recipients Email is not matched");

            Log.Info("Step 7: Verify that added 'Email' is still present after refreshing the page");
            profileSharing.ClickOnMyProfileSharingCloseIcon();
            Driver.RefreshPage();
            profileDetailPage.WaitUntilFmpTextLoadingIndicatorInvisible();
            profileDetailPage.WaitUntilFmpPageLoadingIndicatorInvisible();
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            Assert.AreEqual(recipientsEmail.Email, actualRecipientsEmail, "Share Recipients Email is not matched");

            Log.Info("Step 8: Click on 'Stop sharing' link and verify 'Email' is removed from 'Allowed Emails'");
            profileSharing.DeleteAllProfileSharingEmails();
            Assert.IsFalse(profileSharing.IsRecipientsEmailDisplayed(), "The 'Recipients Email'  is  displayed");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Profile_VerifyMyProfileSharingPopupOpenAndCloseIconAndCancelButtonWorksSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            headerHomePagePo.ClickOnProfileIcon();
            profilePage.ClickOnProfileMenuItem();
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 5: Click on 'Manage My Profile Sharing' button , verify 'My Profile Sharing' pop up gets open successfully");
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            const string expectedMyProfileSharingPopUpHeaderText = "My Profile Sharing";
            Assert.AreEqual(expectedMyProfileSharingPopUpHeaderText, profileSharing.GetMyProfileSharingPopUpHeaderText(), "'My Profile Sharing pop up is not displayed");

            Log.Info("Step 6: Click on 'My Profile Sharing' popup 'close' icon and verify popup gets close");
            profileSharing.ClickOnMyProfileSharingCloseIcon();
            Assert.IsFalse(profileSharing.IsMyProfileSharingPopUpDisplayed(), "'My Profile Sharing' pop-up is  displayed");

            Log.Info("Step 6: Click on 'My Profile Sharing' popup 'Cancel' button and verify popup gets close");
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            profileSharing.ClickOnMyProfileSharingCancelButton();
            Assert.IsFalse(profileSharing.IsMyProfileSharingPopUpDisplayed(), "'My Profile Sharing' pop-up is  displayed");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Profile_VerifyShareMyProfilePopupOpenAndCloseSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);


            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            headerHomePagePo.ClickOnProfileIcon();
            profilePage.ClickOnProfileMenuItem();
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 5: Click on 'Manage My Profile Sharing' button , click on 'Add Recipient via Email' button  and verify 'Share My Profile' pop up open successfully");
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            profileSharing.ClickOAddRecipientViaEmailButton();
            const string expectedShareMyProfileText = "Share My Profile";
            Assert.AreEqual(expectedShareMyProfileText, profileSharing.GetShareMyProfileHeaderText(), "'Share My Profile' pop up is not displayed");

            Log.Info("Step 6: Click on Share My Profile 'close' icon and verify popup gets close");
            profileSharing.ClickOnShareMyProfileCloseIcon();
            Assert.IsFalse(profileSharing.IsShareMyProfileDisplayed(), "'Share My Profile' pop-up is  displayed");

            Log.Info("Step 7: Click on Share My Profile 'Cancel' button and verify popup gets close");
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            profileSharing.ClickOAddRecipientViaEmailButton();
            profileSharing.ClickOnShareMyProfileCancelButton();
            Assert.IsTrue(profileSharing.IsMyProfileSharingPopUpDisplayed(), "'Share My Profile' pop-up is  displayed");

            Log.Info("Step 8: Click on Share My Profile 'back' button and verify 'My Profile Sharing' popup gets open");
            profileSharing.ClickOAddRecipientViaEmailButton();
            profileSharing.ClickOnShareMyProfileBackButton();
            const string expectedMyProfileSharingPopUpHeaderText = "My Profile Sharing";
            Assert.AreEqual(expectedMyProfileSharingPopUpHeaderText, profileSharing.GetMyProfileSharingPopUpHeaderText(), "'My Profile Sharing' pop up is not displayed");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Profile_VerifyStopProfileSharingPopupOpenAndCloseSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            headerHomePagePo.ClickOnProfileIcon();
            profilePage.ClickOnProfileMenuItem();
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 5:Add details in 'Share My Profile' form, click on 'Share My Profile' button , click on 'cancel' button, Verify 'Stop Sharing' popup gets close");
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            profileSharing.ClickOAddRecipientViaEmailButton();
            var recipientsEmail = UserDataFactory.AddUserInformation();
            profileSharing.EnterRecipientsEmail(recipientsEmail.Email);
            profileSharing.SelectShareMyFullProfileCheckbox();
            profileSharing.ClickOnShareMyFullProfileButton();
            profileSharing.ClickOnStopSharingLink();
            profileSharing.ClickStopSharingCancelButton();
            Assert.IsFalse(profileSharing.IsStopSharingPopUpDisplayed(), "The 'Stop Sharing' pop up is displayed");

            Log.Info("Step 6: Click on 'Stop Sharing popup 'back' button and verify 'My Profile Sharing' popup gets open");
            profileSharing.ClickOnStopSharingLink();
            profileSharing.ClickOnStopSharingBackButton();
            const string expectedMyProfileSharingPopUpHeaderText = "My Profile Sharing";
            Assert.AreEqual(expectedMyProfileSharingPopUpHeaderText, profileSharing.GetMyProfileSharingPopUpHeaderText(), "'My Profile Sharing' pop up is not displayed");

            Log.Info("Step 7: Click on 'Stop Sharing popup 'close' button and verify popup gets close");
            profileSharing.ClickOnStopSharingLink();
            profileSharing.ClickOnStopSharingCloseIcon();
            Assert.IsFalse(profileSharing.IsStopSharingPopUpDisplayed(), "The 'Stop Sharing' pop up is displayed");

            Log.Info("Step 8: Click on 'Stop sharing Link' button and verify 'Stop Sharing' popup gets open");
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            profileSharing.ClickOnStopSharingLink();
            Assert.IsTrue(profileSharing.IsStopSharingPopUpDisplayed(), "The 'Stop Sharing' pop up is not  displayed");

            Log.Info("Step 9: Click on 'Stop sharing' button and verify 'My Profile Sharing' popup gets open");
            profileSharing.ClickOnStopSharingButton();
            Assert.AreEqual(expectedMyProfileSharingPopUpHeaderText, profileSharing.GetMyProfileSharingPopUpHeaderText(), "'My Profile Sharing' pop up is not displayed");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Profile_VerifyValidationMessageForRecipientsEmailDisplayedSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);


            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            headerHomePagePo.ClickOnProfileIcon();
            profilePage.ClickOnProfileMenuItem();
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 5: Do not Add details in 'Email',Click on 'Checkbox' of'Share My Profile' form, click on 'Share My Profile' button & verify validation message for blank 'email'");
            profileDetailPage.ClickOnManageMyProfileSharingButton();
            profileSharing.ClickOAddRecipientViaEmailButton();
            profileSharing.EnterRecipientsEmail("");
            profileSharing.SelectShareMyFullProfileCheckbox();
            profileSharing.ClickOnShareMyFullProfileButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, profileSharing.GetRequireRecipientsEmailValidationMessageText(), "Validation message is not displayed for 'EmailS' field");

            Log.Info("Step 6: Add details in 'Share My Profile' form, click on 'Share My Profile' button");
            var recipientsEmail = UserDataFactory.AddUserInformation();
            profileSharing.EnterRecipientsEmail(recipientsEmail.Email);

            profileSharing.ClickOnShareMyFullProfileButton();

            Log.Info("Step 7: Add same details in 'Share My Profile' form, click on 'Share My Profile' button,Verify validation message for enter same 'RecipientsEmail' email");
            profileSharing.ClickOAddRecipientViaEmailButton();
            profileSharing.EnterRecipientsEmail(recipientsEmail.Email);
            profileSharing.SelectShareMyFullProfileCheckbox();
            profileSharing.ClickOnShareMyFullProfileButton();
            const string expectedValidationMessageText = "Looks like you’re already sharing with this recipient! Enter another email address.";
            Assert.AreEqual(expectedValidationMessageText, profileSharing.GetRecipientsEmailValidationMessageText(), "Recipients Email Validation Message is not displayed");

            //Clean up
            try
            {
                profileSharing.ClickOnShareMyProfileBackButton();
                profileSharing.DeleteAllProfileSharingEmails();
            }
            catch
            {
                //Do nothing
            }

        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void Profile_VerifyAddMultipleShareMyProfileDetailsWorksSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var profileDetailPage = new ProfileDetailPagePo(Driver);
            var emailData = new List<string>
            { "test" + new CSharpHelpers().GenerateRandomNumber() + "@yopmail.com",
              "test" + new CSharpHelpers().GenerateRandomNumber() + "@yopmail.com",
               "test" + new CSharpHelpers().GenerateRandomNumber() + "@yopmail.com"
            };

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileSharing.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            headerHomePagePo.ClickOnProfileIcon();
            profilePage.ClickOnProfileMenuItem();
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 5: Click on 'Manage My Profile Sharing' button");
            profileDetailPage.ClickOnManageMyProfileSharingButton();

            Log.Info("Step 6: Click on 'Add Recipient Via Email' button, Add details in 'Share My Profile' form, click on 'Share My Profile' button and verify details added successfully");
            profileSharing.DeleteAllProfileSharingEmails();
            foreach (var email in emailData)
            {
                profileSharing.ClickOAddRecipientViaEmailButton();
                profileSharing.AddShareMyProfileDetails(email); 
            }
            var actualAddRecipientViaEmail = profileSharing.GetAllowedEmailsList();
            CollectionAssert.AreEqual(emailData.ToList(), actualAddRecipientViaEmail.ToList(), "Allowed emails list is not matched");


            //Clean up
            try
            {
                profileSharing.DeleteAllProfileSharingEmails();
            }
            catch
            {
                //Do nothing
            }
        }
    }
}

