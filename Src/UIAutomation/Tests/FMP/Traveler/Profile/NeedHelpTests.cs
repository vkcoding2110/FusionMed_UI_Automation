using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Home;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class NeedHelpTests : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("Smoke"), TestCategory("FMP"), TestCategory("MobileReady")]
     
        public void Profile_VerifyNeedHelpFormSubmitSuccessfully()
        {
            var profilePage = new ProfileMenuPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var needHelpPopUp = new NeedHelpPo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info(
                $"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            headerHomePagePo.ClickOnProfileIcon();
            profilePage.ClickOnProfileMenuItem();
            profilePage.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 5: Click on Need help Link, Verify 'Need help' pop-up is displayed");
            profileDetail.ClickOnNeedHelpLink();
            Assert.IsTrue(needHelpPopUp.IsNeedHelpPopUpDisplayed(), "'Need help' pop-up is not displayed");

            Log.Info("Step 6: Add details in 'Need help' form");
            var needHelpData = ContactUsDataFactory.EnterDetailsInContactUsForm();
            needHelpPopUp.EnterHelpDetails(needHelpData);
            needHelpPopUp.ClickOnSubmitButton();
            profileDetail.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 7: Verify 'information submitted' popup is shown & text is matched");
            Assert.IsTrue(needHelpPopUp.IsInformationSubmittedPopupPresent(),
                "'Information submitted' popup is not shown");
            const string expectedText = "Your information has been successfully submitted! We'll be in contact soon!";
            var actualText = needHelpPopUp.GetNeedHelpPopUpSubmitSuccessText();
            Assert.AreEqual(expectedText, actualText, "Submitted form Successful text is not matched");

            Log.Info("Step 8: Click on 'Okay' button & verify 'Information submitted' popup is closed");
            needHelpPopUp.ClickOnOkayButtonOfSuccessPopup();
            Assert.IsFalse(needHelpPopUp.IsInformationSubmittedPopupPresent(),
                "'Information submitted' popup is shown");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Profile_NeedHelp_VerifyValidationMessageIsDisplayedForMandatoryFields()
        {
            var profileDetails = new ProfileDetailPagePo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);
            var needHelpPopUp = new NeedHelpPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileDetail.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on Need help Link");
            profileDetail.ClickOnNeedHelpLink();

            Log.Info("Step 6: Clear Email field,Click on 'Submit' button and verify 'Need help' pop-up remained open");
            var needHelpData = ContactUsDataFactory.EnterDetailsInContactUsForm();
            needHelpPopUp.EnterEmail("");
            needHelpPopUp.EnterMessage(needHelpData.Message);
            needHelpPopUp.ClickOnSubmitButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, needHelpPopUp.GetEmailValidationMessage(), "validation message is not displayed for 'Email' text box");

            Log.Info("Step 7: Clear Message field,Click on 'Submit' button and verify 'Need help' pop-up remained open");
            needHelpPopUp.EnterEmail(needHelpData.Email);
            needHelpPopUp.EnterMessage("");
            needHelpPopUp.ClickOnSubmitButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, needHelpPopUp.GetMessageValidationMessage(), "validation message is not displayed for 'Message' text box");

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void Profile_NeedHelp_VerifyFormClosedSuccessfully()
        {
            var profileDetails = new ProfileDetailPagePo(Driver);
            var marketPlaceLogin = new FmpLoginPo(Driver);
            var profileDetail = new ProfileDetailPagePo(Driver);
            var needHelpPopUp = new NeedHelpPo(Driver);
            var headerHomePagePo = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info(
                $"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            marketPlaceLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            profileDetail.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on Need help Link");
            profileDetail.ClickOnNeedHelpLink();

            Log.Info("Step 6: Click on Close Icon and Verify 'Need help' pop-up is closed");
            needHelpPopUp.ClickOnCloseIcon();
            Assert.IsFalse(needHelpPopUp.IsNeedHelpPopUpDisplayed(), "'Need help' pop-up is not displayed");

            Log.Info("Step 7: Click on Need help Link");
            profileDetail.ClickOnNeedHelpLink();

            Log.Info("Step 8: Click on Cancel button and Verify 'Need help' pop-up is closed");
            needHelpPopUp.ClickOnCancelButton();
            Assert.IsFalse(needHelpPopUp.IsNeedHelpPopUpDisplayed(), "'Need help' pop-up is not closed");
        }
    }
}