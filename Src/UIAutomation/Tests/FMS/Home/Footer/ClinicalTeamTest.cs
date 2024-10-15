using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMS.Home;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.PageObjects.FMS.Home.Footer;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.Home.Footer
{
    [TestClass]
    [TestCategory("Footer"), TestCategory("FMS")]
    public class ClinicalTeamTest : FmsBaseTest
    {
        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyClinicalTeamLinkAndContactUsFormWorkSuccessfully()
        {
            var footer = new FooterPo(Driver);
            var clinicalTeam = new ClinicalTeamPo(Driver);
            var contactUs = new ClinicalContactUsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Traveler & verify Traveler page is opened");
            footer.ClickOnTraveler();
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 3: Click on 'Learn more about our Clinical Team' link & Verify 'Clinical Team' page gets open");
            clinicalTeam.ClickOnClinicalTeamLink();
            var expectedClinicalTeamPageUrl = FmsUrl + "traveler/clinical-team/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedClinicalTeamPageUrl), $"{expectedClinicalTeamPageUrl} Clinical team page url is not matched");

            Log.Info("Step 4: Fill the 'Contact Us' form & verify 'Thank You' page gets open");
            var addContactUsData = ContactUsDataFactory.EnterDetailsInContactUsForm();
            contactUs.EnterClinicalContactUsData(addContactUsData);

            const string expectedThankYouPageHeaderText = "Thanks!";
            var actualThankYouPageHeaderText = contactUs.GetThankYouPageHeaderText();
            Assert.AreEqual(expectedThankYouPageHeaderText.ToLower(), actualThankYouPageHeaderText.ToLower(), "The 'Thank You' page header text doesn't match");

            const string expectedThankYouPageMessage = "keep the process with these next steps...";
            var actualThankYouPageMessage = contactUs.GetThankYouPageMessageText();
            Assert.AreEqual(expectedThankYouPageMessage.ToLower(), actualThankYouPageMessage.ToLower(), "The 'Thank You' page message text doesn't match");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatValidationMessageIsDisplayedForMandatoryFieldsInClinicalTeamContactUsForm()
        {
            var clinicalTeam = new ClinicalTeamPo(Driver);
            var contactUs = new ClinicalContactUsPo(Driver);

            Log.Info("Step 1: Navigate to 'Contact Us' page");
            clinicalTeam.NavigateToPage();

            Log.Info("Step 2: Clear 'First Name' field and Verify that Validation message is displayed for name field");
            var addContactUsData = ContactUsDataFactory.EnterDetailsInContactUsForm();
            contactUs.EnterLastName(addContactUsData.LastName);
            contactUs.EnterEmail(addContactUsData.Email);
            contactUs.EnterMessage(addContactUsData.Message);
            contactUs.ClickOnSubmitButton();
            Assert.IsTrue(contactUs.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for name");

            Log.Info("Step 3: Clear 'Last Name' field and Verify that Validation message is displayed for email field");
            contactUs.EnterFirstName(addContactUsData.Name);
            contactUs.EnterLastName("");
            contactUs.ClickOnSubmitButton();
            Assert.IsTrue(contactUs.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for email");

            Log.Info("Step 4: Clear 'Email' field and Verify that Validation message is displayed for email field");
            contactUs.EnterLastName(addContactUsData.LastName);
            contactUs.EnterEmail("");
            contactUs.ClickOnSubmitButton();
            Assert.IsTrue(contactUs.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for email");

            Log.Info("Step 5: Clear 'Message' field and Verify that Validation message is displayed for message field");
            contactUs.EnterEmail(addContactUsData.Email);
            contactUs.EnterMessage("");
            contactUs.ClickOnSubmitButton();
            Assert.IsTrue(contactUs.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for message");
        }
    }
}
