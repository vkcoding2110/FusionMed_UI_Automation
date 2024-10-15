using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Account;
using UIAutomation.DataFactory.FMP.Traveler.AccountSettings;
using UIAutomation.DataObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.AccountSettings;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.AccountSettings
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class AccountDisableTests : FmpBaseTest
    {
        private static SignUp _signUp = new();

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            _signUp = SignUpDataFactory.GetDataForSignUpForm();
            setup.CreateUser(_signUp);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void VerifyThatTravelerAccountCanBeDisabledSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var headerPo = new HeaderPo(Driver);
            var accountSettings = new AccountSettingsPo(Driver);
            var closeAccount = new CloseAccountPopupPo(Driver);
            var disableAndPermanentlyCloseAccount = new DisableAndPermanentlyCloseAccountPopupPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info($"Step 2: Login to application with credentials - Email:{_signUp.Email}, password:{_signUp.Password}");
            headerPo.ClickOnLogInButton();
            fmpLogin.LoginToApplication(_signUp);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 3: Click on the 'Profile' icon, Click on 'Account Settings' text & verify 'Account Settings' page gets open");
            accountSettings.NavigateToPage();
            var expectedUrl = FusionMarketPlaceUrl + "account-settings/";
            Assert.AreEqual(expectedUrl, Driver.GetCurrentUrl(), $"{expectedUrl} url is not matched");

            const string expectedAccountSettingsHeaderText = "Account Settings";
            var actualAccountSettingsHeaderText = accountSettings.GetAccountSettingsHeaderText();
            Assert.AreEqual(expectedAccountSettingsHeaderText.ToLowerInvariant(), actualAccountSettingsHeaderText.ToLowerInvariant(), "Account Settings header text is not matched");

            const string expectedCloseAccountDescription = "You can disable your account and reactivate within 90 days if you change your mind.";
            var actualCloseAccountDescriptionText = accountSettings.GetCloseAccountMessage();
            Assert.AreEqual(expectedCloseAccountDescription.ToLowerInvariant(), actualCloseAccountDescriptionText.ToLowerInvariant(), "Close Account description message is not matched.");

            Log.Info("Step 4: Click on 'Close Account' button and verify 'Close Account' pop up gets open successfully");
            accountSettings.ClickOnCloseAccountButton();
            const string expectedCloseAccountText = "Close Account";
            Assert.AreEqual(expectedCloseAccountText, closeAccount.GetCloseAccountHeaderText(), "Close account pop up is not displayed");
            Assert.IsTrue(closeAccount.IsCloseAccountPopupIsPresent(), "'Close Account' pop-up is not opened");

            Log.Info("Step 5: Click on 'close' icon on 'Close Account' popup and verify popup gets close");
            closeAccount.ClickOnCloseIcon();
            Assert.IsFalse(closeAccount.IsCloseAccountPopupIsPresent(), "'Close Account' pop-up is  displayed");

            Log.Info("Step 6: Click on 'Cancel' button on 'Close Account' popup and verify popup gets close");
            accountSettings.ClickOnCloseAccountButton();
            closeAccount.ClickOnCancelButton();
            Assert.IsFalse(closeAccount.IsCloseAccountPopupIsPresent(), "'Disable Account' pop-up is  displayed");

            Log.Info("Step 7: Click on 'Continue' button on 'Close Account' popup and verify Disable Account popup gets open successfully");
            accountSettings.ClickOnCloseAccountButton();
            closeAccount.ClickOnContinueButton();
            const string expectedDisableAccountText = "Disable Account";
            Assert.AreEqual(expectedDisableAccountText, disableAndPermanentlyCloseAccount.GetPopUpHeaderText(), "Disable account pop up is not displayed");
            Assert.IsTrue(disableAndPermanentlyCloseAccount.IsDisableAccountButtonPresent(), "Disable account pop up is not opened");

            Log.Info("Step 8: Click on disable Account 'Back' button and verify popup gets close");
            disableAndPermanentlyCloseAccount.ClickOnBackArrowIcon();
            Assert.IsFalse(disableAndPermanentlyCloseAccount.IsDisableAccountButtonPresent(), "'Disable Account' pop-up is  displayed");

            Log.Info("Step 9: Click on disable Account 'Close' button and verify popup gets close");
            closeAccount.ClickOnContinueButton();
            disableAndPermanentlyCloseAccount.ClickOnCloseIcon();
            Assert.IsFalse(disableAndPermanentlyCloseAccount.IsPopupHeaderTextPresent(), "'Disable Account' pop-up is  displayed");

            Log.Info("Step 10: Click on disable Account 'Cancel' button and verify popup gets close");
            accountSettings.ClickOnCloseAccountButton();
            closeAccount.ClickOnContinueButton();
            disableAndPermanentlyCloseAccount.ClickOnCancelButton();
            Assert.IsFalse(disableAndPermanentlyCloseAccount.IsPopupHeaderTextPresent(), "'Disable Account' pop-up is  displayed");

            Log.Info("Step 11: Do not add details in 'Disable Account' popup textbox and click on 'Disable Account' button & verify validation message is displayed");
            accountSettings.ClickOnCloseAccountButton();
            closeAccount.ClickOnContinueButton();
            disableAndPermanentlyCloseAccount.EnterTextIntoCloseTextBox("");
            disableAndPermanentlyCloseAccount.ClickOnDisableAccountButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, disableAndPermanentlyCloseAccount.GetValidationMessageForCloseTextBox(), "Validation message is not displayed for 'Disable account close to confirm text box' field");

            Log.Info("Step 12: Enter 'close' in 'Disable Account' popup text box and click on 'Disable Account' button & verify validation message is displayed");
            disableAndPermanentlyCloseAccount.ClickOnBackArrowIcon();
            closeAccount.ClickOnContinueButton();
            disableAndPermanentlyCloseAccount.EnterTextIntoCloseTextBox("close");
            disableAndPermanentlyCloseAccount.ClickOnDisableAccountButton();
            const string expectedDisableAccountCloseToConfirmTextBoxValidationMessage = "Please type the word CLOSE to confirm this action.";
            Assert.AreEqual(expectedDisableAccountCloseToConfirmTextBoxValidationMessage, disableAndPermanentlyCloseAccount.GetValidationMessageForCloseTextBox(), "Validation message is not displayed for 'Disable account close to confirm text box' field");

            Log.Info("Step 13: Enter 'CLOSE' text into 'Disable account close to confirm' text box & verify your account has been disabled or not");
            var disableAndPermanentlyCloseAccountData = CloseAccountDataFactory.DisableAndPermanentlyCloseAccountDetails();
            disableAndPermanentlyCloseAccount.ClickOnBackArrowIcon();
            closeAccount.ClickOnContinueButton();
            disableAndPermanentlyCloseAccount.EnterCloseAccountDetails(disableAndPermanentlyCloseAccountData);
            disableAndPermanentlyCloseAccount.ClickOnDisableAccountButton();
            accountSettings.WaitUntilFmpPageLoadingIndicatorInvisible();
            const string expectedAccountDisabledHeaderText = "Your account has been disabled.";
            Assert.AreEqual(expectedAccountDisabledHeaderText, accountSettings.GetAccountDisabledAndClosedHeaderText(), "Account disabled page header text is not matched.");
            const string expectedAccountDisabledInformationText = "We’re sorry to see you go!  You can reactivate your account by logging in within 90 days. After 90 days, your account will be permanently closed and you will not be able to retrieve it.";
            Assert.AreEqual(expectedAccountDisabledInformationText.RemoveWhitespace(), accountSettings.GetAccountDisabledAndClosedInformationText().RemoveWhitespace(), "Account disabled page information text is not matched.");
        }
    }
}
