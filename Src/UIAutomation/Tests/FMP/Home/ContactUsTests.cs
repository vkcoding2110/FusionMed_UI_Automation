using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Home;
using UIAutomation.PageObjects.FMP.Footer;
using UIAutomation.PageObjects.FMP.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Home
{
    [TestClass]
    [TestCategory("FMP"), TestCategory("ContactUs")]
    public class ContactUsTests : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatContactUsFormSubmitSuccessfullyWithoutUserLogIn()
        {
            var footer = new FooterPo(Driver);
            var contactUs = new ContactUsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on Contact Us link & verify the contact us details");
            footer.ClickOnContactUsLink();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 3: Fill the details in 'Contact Us' form, click on 'Submit' button & verify thank you page gets open");
            var addContactUsData = ContactUsDataFactory.EnterDetailsInContactUsForm();
            contactUs.EnterContactUsData(addContactUsData);
            contactUs.ClickOnSubmitButton();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();

            const string expectedThankYouPageHeaderText = "We've got mail!";
            var actualThankYouPageHeaderText = contactUs.GetThankYouPageHeaderText();
            Assert.AreEqual(expectedThankYouPageHeaderText.ToLower(), actualThankYouPageHeaderText.ToLower(), "The 'Thank You' page header text doesn't match");

            const string expectedThankYouPageMessage = "Is it a love letter? A virtual fruit basket? In any case, we can't wait to chat! We'll contact you shortly.";
            var actualThankYouPageMessage = contactUs.GetThankYouPageMessageText();
            Assert.AreEqual(expectedThankYouPageMessage.ToLower().RemoveWhitespace(), actualThankYouPageMessage.ToLower().RemoveWhitespace(), "The 'Thank You' page message text doesn't match");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatValidationMessageIsDisplayedForMandatoryFieldsInContactUsForm()
        {
            var contactUs = new ContactUsPo(Driver);

            Log.Info("Step 1: Navigate to 'Contact Us' page");
            contactUs.NavigateToPage();
            contactUs.WaitUntilFormLoaded();

            Log.Info("Step 2: Clear name field and Verify that Validation message is displayed for name field");
            var addContactUsData = ContactUsDataFactory.EnterDetailsInContactUsForm();
            contactUs.EnterEmail(addContactUsData.Email);
            contactUs.EnterMessage(addContactUsData.Message);
            contactUs.ClickOnSubmitButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, contactUs.GetNameValidationMessage(), "validation message is not displayed for name");

            Log.Info("Step 3: Clear email field and Verify that Validation message is displayed for email field");
            contactUs.EnterName(addContactUsData.Name);
            contactUs.EnterEmail("");
            contactUs.ClickOnSubmitButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, contactUs.GetEmailValidationMessage(), "validation message is not displayed for email");

            Log.Info("Step 4: Clear message field and Verify that Validation message is displayed for message field");
            contactUs.EnterEmail(addContactUsData.Email);
            contactUs.EnterMessage("");
            contactUs.ClickOnSubmitButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage, contactUs.GetMessageValidationMessage(), "validation message is not displayed for message");
        }
    }
}
