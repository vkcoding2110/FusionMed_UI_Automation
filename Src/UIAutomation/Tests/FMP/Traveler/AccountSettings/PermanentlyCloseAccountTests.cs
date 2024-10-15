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
    public class PermanentlyCloseAccountTests : FmpBaseTest
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
        public void VerifyThatTravelerAccountCanBePermanentlyClosedSuccessfully()
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

            Log.Info("Step 4: Click on 'Permanently Close my Account' radio button and verify 'Permanently Close Account' pop up gets open successfully");
            accountSettings.ClickOnCloseAccountButton();
            closeAccount.ClickOnPermanentlyCloseAccountRadioButton();
            closeAccount.ClickOnContinueButton();
            const string expectedPermanentlyCloseAccountHeaderText = "Permanently Close Account";
            Assert.AreEqual(expectedPermanentlyCloseAccountHeaderText, disableAndPermanentlyCloseAccount.GetPopUpHeaderText(), "Permanently close account pop up is not displayed");
            Assert.IsTrue(disableAndPermanentlyCloseAccount.IsPermanentlyCloseAccountButtonPresent(), "'Permanently Close Account' pop-up is not displayed");

            Log.Info("Step 5: Click on permanently close account 'Back' button and verify popup gets close");
            disableAndPermanentlyCloseAccount.ClickOnBackArrowIcon();
            Assert.IsFalse(disableAndPermanentlyCloseAccount.IsPermanentlyCloseAccountButtonPresent(), "'Permanently Close Account' pop-up is displayed");

            Log.Info("Step 6: Click on permanently close account 'Close' button and verify popup gets close");
            closeAccount.ClickOnContinueButton();
            disableAndPermanentlyCloseAccount.ClickOnCloseIcon();
            Assert.IsFalse(disableAndPermanentlyCloseAccount.IsPopupHeaderTextPresent(), "'Permanently Close Account' pop-up is  displayed");

            Log.Info("Step 7: Click on permanently close account 'Cancel' button and verify popup gets close");
            accountSettings.ClickOnCloseAccountButton();
            closeAccount.ClickOnPermanentlyCloseAccountRadioButton();
            closeAccount.ClickOnContinueButton();
            disableAndPermanentlyCloseAccount.ClickOnCancelButton();
            Assert.IsFalse(disableAndPermanentlyCloseAccount.IsPopupHeaderTextPresent(), "'Permanently Close Account' pop-up is  displayed");

            Log.Info("Step 8: Do not add details in 'Permanently Close Account' popup text box and Click on 'Permanently Close Account' button & verify validation message is displayed");
            accountSettings.ClickOnCloseAccountButton();
            closeAccount.ClickOnPermanentlyCloseAccountRadioButton();
            closeAccount.ClickOnContinueButton();
            disableAndPermanentlyCloseAccount.EnterTextIntoCloseTextBox("");
            disableAndPermanentlyCloseAccount.ClickOnPermanentlyCloseAccountButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, disableAndPermanentlyCloseAccount.GetValidationMessageForCloseTextBox(), "Validation message is not displayed for 'Permanently close account close to confirm text box' field");

            Log.Info("Step 9: Enter 'close' in 'Permanently Close Account' popup text box and Click on 'Permanently Close Account' button & verify validation message is displayed");
            disableAndPermanentlyCloseAccount.ClickOnBackArrowIcon();
            closeAccount.ClickOnContinueButton();
            disableAndPermanentlyCloseAccount.EnterTextIntoCloseTextBox("close");
            disableAndPermanentlyCloseAccount.ClickOnPermanentlyCloseAccountButton();
            const string expectedDisableAccountCloseToConfirmTextBoxValidationMessage = "Please type the word CLOSE to confirm this action.";
            Assert.AreEqual(expectedDisableAccountCloseToConfirmTextBoxValidationMessage, disableAndPermanentlyCloseAccount.GetValidationMessageForCloseTextBox(), "Validation message is not displayed for 'Disable account close to confirm text box' field");

            Log.Info("Step 10: Enter 'CLOSE' text into 'Permanently Close Account Close to confirm' text box & verify 'Are You Sure' pop up gets open successfully");
            disableAndPermanentlyCloseAccount.ClickOnBackArrowIcon();
            closeAccount.ClickOnContinueButton();
            disableAndPermanentlyCloseAccount.EnterTextIntoCloseTextBox("CLOSE");
            disableAndPermanentlyCloseAccount.ClickOnPermanentlyCloseAccountButton();
            const string expectedAreYouSureHeaderText = "Are You Sure?";
            Assert.AreEqual(expectedAreYouSureHeaderText, disableAndPermanentlyCloseAccount.GetPopUpHeaderText(), "'Are you sure' pop up is not displayed");

            Log.Info("Step 11: Click on 'Are You Sure?' popup 'Back' button and verify popup gets close");
            disableAndPermanentlyCloseAccount.ClickOnBackArrowIcon();
            Assert.IsTrue(disableAndPermanentlyCloseAccount.IsPermanentlyCloseAccountButtonPresent(), "'Are You Sure?' pop-up is displayed");

            Log.Info("Step 12: Click on 'Are You Sure?' popup 'Close' button and verify popup gets close");
            disableAndPermanentlyCloseAccount.ClickOnPermanentlyCloseAccountButton();
            disableAndPermanentlyCloseAccount.ClickOnCloseIcon();
            Assert.IsFalse(disableAndPermanentlyCloseAccount.IsPopupHeaderTextPresent(), "'Are You Sure?' pop-up is  displayed");

            Log.Info("Step 13: Click on'Are You Sure?' popup 'Cancel' button and verify popup gets close");
            accountSettings.ClickOnCloseAccountButton();
            closeAccount.ClickOnPermanentlyCloseAccountRadioButton();
            closeAccount.ClickOnContinueButton();
            disableAndPermanentlyCloseAccount.EnterTextIntoCloseTextBox("CLOSE");
            disableAndPermanentlyCloseAccount.ClickOnPermanentlyCloseAccountButton();
            disableAndPermanentlyCloseAccount.ClickOnCancelButton();
            Assert.IsFalse(disableAndPermanentlyCloseAccount.IsPopupHeaderTextPresent(), "'Are You Sure?' pop-up is  displayed");

            Log.Info("Step 14: Click on 'Close Account' button of 'Are You Sure?' pop up and verify your account has been disabled or not ");
            var disableAndPermanentlyCloseAccountData = CloseAccountDataFactory.DisableAndPermanentlyCloseAccountDetails();
            accountSettings.ClickOnCloseAccountButton();
            closeAccount.ClickOnPermanentlyCloseAccountRadioButton();
            closeAccount.ClickOnContinueButton();
            disableAndPermanentlyCloseAccount.EnterCloseAccountDetails(disableAndPermanentlyCloseAccountData);
            disableAndPermanentlyCloseAccount.ClickOnPermanentlyCloseAccountButton();
            disableAndPermanentlyCloseAccount.ClickOnCloseAccountButton();
            accountSettings.WaitUntilFmpPageLoadingIndicatorInvisible();
            const string expectedAccountDisabledHeaderText = "Your account has been permanently closed.";
            Assert.AreEqual(expectedAccountDisabledHeaderText.RemoveWhitespace(), accountSettings.GetAccountDisabledAndClosedHeaderText().RemoveWhitespace(), "Account permanently closed page header text is not matched.");
            const string expectedAccountDisabledInformationText = "We’re sorry to see you go!";
            Assert.AreEqual(expectedAccountDisabledInformationText.RemoveWhitespace(), accountSettings.GetAccountDisabledAndClosedInformationText().RemoveWhitespace(), "Account permanently closed page information text is not matched.");
        }
    }
}
