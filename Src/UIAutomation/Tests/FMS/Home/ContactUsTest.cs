using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMS.Home;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.Home
{
    [TestClass]
    [TestCategory("FMS"), TestCategory("ContactUs")]
    public class ContactUsTest : BaseTest
    {
        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatContactUsFormSubmitSuccessfullyWithoutUserLogIn()
        {
            var footer = new FooterPo(Driver);
            var contactUs = new ContactUsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Contact Us link");
            footer.ClickOnContactUs();
            footer.WaitUntilMpPageLoadingIndicatorInvisible();
            contactUs.WaitUntilFormLoaded();

            Log.Info("Step 3: Fill the details in 'Contact Us' form, click on 'Submit' button & verify thank you page gets open");
            var addContactUsData = ContactUsDataFactory.EnterDetailsInContactUsForm();
            contactUs.EnterContactUsData(addContactUsData);
            contactUs.ClickOnSubmitButton();
            contactUs.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            contactUs.WaitUntilMpPageLoadingIndicatorInvisible();

            const string expectedThankYouPageHeaderText = "Thanks!";
            var actualThankYouPageHeaderText = contactUs.GetThankYouPageHeaderText();
            Assert.AreEqual(expectedThankYouPageHeaderText.ToLower(), actualThankYouPageHeaderText.ToLower(), "The 'Thank You' page header text doesn't match");

            const string expectedThankYouPageMessage = "keep the process with these next steps...";
            var actualThankYouPageMessage = contactUs.GetThankYouPageMessageText();
            Assert.AreEqual(expectedThankYouPageMessage.ToLower(), actualThankYouPageMessage.ToLower(), "The 'Thank You' page message text doesn't match");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
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
            contactUs.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(contactUs.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for name");

            Log.Info("Step 3: Clear email field and Verify that Validation message is displayed for email field");
            contactUs.EnterName(addContactUsData.Name);
            contactUs.EnterEmail("");
            contactUs.ClickOnSubmitButton();
            contactUs.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(contactUs.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for email");

            Log.Info("Step 4: Clear message field and Verify that Validation message is displayed for message field");
            contactUs.EnterEmail(addContactUsData.Email);
            contactUs.EnterMessage("");
            contactUs.ClickOnSubmitButton();
            contactUs.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(contactUs.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for message");

        }
    }
}