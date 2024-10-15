using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.Common;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account.NativeApp;
using UIAutomation.PageObjects.FMP.Home.NativeApp;
using UIAutomation.PageObjects.FMP.NativeApp.More;
using UIAutomation.PageObjects.FMP.Traveler.Profile.NativeApp;
using UIAutomation.PageObjects.FMP.Traveler.Profile.ProfileSharing.NativeApp;
using UIAutomation.SetUpTearDown.FMP.NativeApp;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.ProfileSharing.NativeApp
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("NativeAppAndroid")]
    public class ProfileSharingTests1 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("ManageMyProfileSharingTests");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteProfileSharingDetails(UserLogin);
        }

        [TestMethod]
        public void Profile_VerifyMyProfileSharingPopupOpenAndCloseIconAndCancelButtonWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var homePage = new HomePagePo(Driver);
            var profile = new ProfileDetailPagePo(Driver);
            var moreMenu = new MoreMenuPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);

            Log.Info("Step 1: Open FMP App & Login to the application");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on 'Profile' icon and verify Profile page open Successfully");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            Assert.IsTrue(profile.IsProfilePageOpen(), "Profile page is not opened!");

            Log.Info("Step 3: Click on 'Manage My Profile Sharing' button , click on 'Add Recipient via Email' button  and verify 'Share My Profile' pop up open successfully");
            profile.ClickOnShareMyProfileButton();
            profileSharing.ClickOAddRecipientViaEmailButton();
            const string expectedShareMyProfileText = "Share My Profile";
            Assert.AreEqual(expectedShareMyProfileText, profileSharing.GetShareMyProfileHeaderText(), "'Share My Profile' pop up is not displayed");

            Log.Info("Step 4: Click on Share My Profile 'close' icon and verify popup gets close");
            profileSharing.ClickOnShareMyProfileCloseIcon();
            Assert.IsFalse(profileSharing.IsShareMyProfileDisplayed(), "'Share My Profile' pop-up is  displayed");

            Log.Info("Step 5: Click on Share My Profile 'Cancel' button and verify popup gets close");
            profile.ClickOnShareMyProfileButton();
            profileSharing.ClickOAddRecipientViaEmailButton();
            profileSharing.ClickOnShareMyProfileCancelButton();
            Assert.IsTrue(profileSharing.IsMyProfileSharingPopUpDisplayed(), "'Share My Profile' pop-up is not displayed");

            Log.Info("Step 6: Click on Share My Profile 'back' button and verify 'My Profile Sharing' popup gets open");
            profileSharing.ClickOAddRecipientViaEmailButton();
            profileSharing.ClickOnShareMyProfileBackButton();
            const string expectedMyProfileSharingPopUpHeaderText = "My Profile Sharing";
            Assert.AreEqual(expectedMyProfileSharingPopUpHeaderText, profileSharing.GetMyProfileSharingPopUpHeaderText(), "'Profile Sharing Pop Up' Header Text is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void Profile_VerifyThatProfileSharingDeleteDetailsWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var homePage = new HomePagePo(Driver);
            var profile = new ProfileDetailPagePo(Driver);
            var moreMenu = new MoreMenuPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);

            Log.Info("Step 1: Open FMP App & Login to the application");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on 'Profile' icon and Click on 'Share My Profile' button");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            profile.ClickOnShareMyProfileButton();

            Log.Info("Step 2: Add details in 'Share My Profile' form");
            profileSharing.ClickOAddRecipientViaEmailButton();
            var recipientsEmail = UserDataFactory.AddUserInformation();
            profileSharing.AddShareMyProfileDetails(recipientsEmail.Email);

            Log.Info("Step 3: Click on 'Stop Sharing Link' and Click on 'Stop Sharing Button' & verify 'Profile Sharing' details are removed");
            profileSharing.DeleteAllProfileSharingEmails();
            Assert.IsFalse(profileSharing.IsStopSharingLinkPresent(), "The 'Profile Sharing' details are still displayed");
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void Profile_VerifyShareMyProfileDetailsWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var homePage = new HomePagePo(Driver);
            var profile = new ProfileDetailPagePo(Driver);
            var moreMenu = new MoreMenuPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);

            Log.Info("Step 1: Open FMP App & Login to the application");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on 'Profile' icon and Click on 'Share My Profile' button");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            profile.ClickOnShareMyProfileButton();

            Log.Info("Step 3: Add details in 'Share My Profile' form, click on 'Share My Profile' button and verify details added successfully");
            profileSharing.ClickOAddRecipientViaEmailButton();
            var recipientsEmail = UserDataFactory.AddUserInformation();
            profileSharing.AddShareMyProfileDetails(recipientsEmail.Email);
            var actualRecipientsEmail = profileSharing.GetAllowedEmailsText();
            Assert.AreEqual(recipientsEmail.Email, actualRecipientsEmail, "Share Recipients Email is not matched");

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

        [TestMethod]
        public void Profile_VerifyStopProfileSharingPopupOpenAndCloseSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var homePage = new HomePagePo(Driver);
            var profile = new ProfileDetailPagePo(Driver);
            var moreMenu = new MoreMenuPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);

            Log.Info("Step 1: Open FMP App & Login to the application");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on 'Profile' icon and Click on 'Share My Profile' button");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            profile.ClickOnShareMyProfileButton();

            Log.Info("Step 3: Add details in 'Share My Profile' form, click on 'Share My Profile' button and verify details added successfully");
            profileSharing.ClickOAddRecipientViaEmailButton();
            var recipientsEmail = UserDataFactory.AddUserInformation();
            profileSharing.AddShareMyProfileDetails(recipientsEmail.Email);
            profileSharing.ClickOnStopSharingLink();
            profileSharing.ClickStopSharingCancelButton();
            Assert.IsFalse(profileSharing.IsStopSharingPopUpDisplayed(), "The 'Stop Sharing' pop up is displayed");

            Log.Info("Step 4: Click on 'Stop Sharing popup 'back' button and verify 'My Profile Sharing' popup gets open");
            profileSharing.ClickOnStopSharingLink();
            profileSharing.ClickOnStopSharingBackButton();
            const string expectedMyProfileSharingPopUpHeaderText = "My Profile Sharing";
            Assert.AreEqual(expectedMyProfileSharingPopUpHeaderText, profileSharing.GetMyProfileSharingPopUpHeaderText(), "'My Profile Sharing' pop up is not displayed");

            Log.Info("Step 5: Click on 'Stop Sharing popup 'close' button and verify popup gets close");
            profileSharing.ClickOnStopSharingLink();
            profileSharing.ClickOnStopSharingCloseIcon();
            Assert.IsFalse(profileSharing.IsStopSharingPopUpDisplayed(), "The 'Stop Sharing' pop up is displayed");

            Log.Info("Step 6: Click on 'Stop sharing Link' button and verify 'Stop Sharing' popup gets open");
            profile.ClickOnShareMyProfileButton();
            profileSharing.ClickOnStopSharingLink();
            Assert.IsTrue(profileSharing.IsStopSharingPopUpDisplayed(), "The 'Stop Sharing' pop up is not  displayed");

            Log.Info("Step 7: Click on 'Stop sharing' button and verify 'My Profile Sharing' popup gets open");
            profileSharing.ClickOnStopSharingButton();
            Assert.AreEqual(expectedMyProfileSharingPopUpHeaderText, profileSharing.GetMyProfileSharingPopUpHeaderText(), "'My Profile Sharing' pop up is not displayed");
        }

        [TestMethod]
        public void Profile_VerifyValidationMessageForRecipientsEmailDisplayedSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var homePage = new HomePagePo(Driver);
            var profile = new ProfileDetailPagePo(Driver);
            var moreMenu = new MoreMenuPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);

            Log.Info("Step 1: Open FMP App & Login to the application");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on 'Profile' icon and Click on 'Share My Profile' button");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            profile.ClickOnShareMyProfileButton();

            Log.Info("Step 3: Do not Add details in 'Email',Click on 'Checkbox' of 'Share My Profile' form, click on 'Share My Profile' button & verify validation message for blank 'email'");
            profileSharing.ClickOAddRecipientViaEmailButton();
            profileSharing.EnterRecipientsEmail("");
            profileSharing.SelectShareMyFullProfileCheckbox();
            profileSharing.ClickOnShareMyFullProfileButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, profileSharing.GetRequireRecipientsEmailValidationMessageText(), "Validation message is not displayed for 'Email's field");

            Log.Info("Step 4: Add details in 'Share My Profile' form, click on 'Share My Profile' button");
            var recipientsEmail = UserDataFactory.AddUserInformation();
            profileSharing.EnterRecipientsEmail(recipientsEmail.Email);
            profileSharing.ClickOnShareMyFullProfileButton();

            Log.Info("Step 5: Add same details in 'Share My Profile' form, click on 'Share My Profile' button,Verify validation message for 'Recipients' email");
            profileSharing.ClickOAddRecipientViaEmailButton();
            profileSharing.EnterRecipientsEmail(recipientsEmail.Email);
            profileSharing.SelectShareMyFullProfileCheckbox();
            profileSharing.ClickOnShareMyFullProfileButton();
            const string expectedValidationMessageText = "Looks like you’re already sharing with this recipient! Enter another email address.";
            Assert.AreEqual(expectedValidationMessageText, profileSharing.GetRecipientsEmailValidationMessageText(), "Recipients email validation message is not displayed");

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
    }
}
