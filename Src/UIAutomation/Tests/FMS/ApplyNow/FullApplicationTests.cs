using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMS.ApplyNow;
using UIAutomation.PageObjects.FMS.ApplyNow;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.ApplyNow
{
    [TestClass]
    [TestCategory("ApplyNow"), TestCategory("FMS")]
    public class FullApplicationTests : BaseTest
    {

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyFullApplicationWorkSuccessfully()
        {
            var quickApplication = new QuickApplicationPo(Driver);
            var applyNow = new ApplyNowPo(Driver);
            var fullApp = new FullApplicationPo(Driver);
            var thanksPage = new ThanksPagePo(Driver);

            Log.Info("Step 1: Navigate to 'Apply Now' page");
            quickApplication.NavigateToPage();

            Log.Info("Step 2: Click on 'Full Application' link & verify 'Full Application' form gets open");
            applyNow.ClickOnFullApplication();
            applyNow.WaitUntilMpPageLoadingIndicatorInvisible();
            var expectedFullApplicationUrl = FmsUrl + FmsConstants.FullApplicationUrl;
            Assert.IsTrue(Driver.IsUrlMatched(expectedFullApplicationUrl), $"{expectedFullApplicationUrl} Full application Url is not matched");

            Log.Info("Step 3: Verify Referred by check box is selected or not ");
            fullApp.WaitUntilFirstNameDisplayed();
            Assert.IsFalse(fullApp.IsReferredByCheckboxSelected(), "'Someone referred me' checkbox is selected");

            Log.Info("Step 4: Fill data in full application & click on 'Submit' button");
            var fullAppDate = ApplyNowDataFactory.AddDataInFullAppForm();
            fullApp.AddFullApplicationData(fullAppDate);
            fullApp.ClickOnSubmitButton();

            Log.Info("Step 5: Verify Applying success text, Url skills checklist url & Url contains firstName, LastName & PhoneNumber");
            const string formName = "skills checklist";
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            fullApp.WaitUntilMpPageLoadingIndicatorInvisible();
            var expectedUrl = FmsUrl + "apply/full-application/thank-you/";
            var actualSuccessText = thanksPage.GetFormSubmitSuccessText();
            Assert.AreEqual(FmsConstants.QuickAndFullAppSubmissionThanksForApplyText.ToLower(), actualSuccessText.ToLower(), "Success header text is not matched");
            Assert.IsTrue(Driver.IsUrlContains(expectedUrl), $"expectedUrl: {expectedUrl}, actualUrl{Driver.GetCurrentUrl()} Url is not matched");
            Assert.IsTrue(Driver.IsUrlContains(fullAppDate.FirstName), $"First Name{fullAppDate.FirstName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(fullAppDate.LastName), $"Last Name{fullAppDate.LastName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(fullAppDate.PhoneNumber), $"Phone Number{fullAppDate.PhoneNumber} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(thanksPage.IsFillOutFormUrlPresent(formName), "Skills Checklist link is not present");
            var expectedSkillsCheckListUrl = FmsUrl + FmsConstants.SkillsCheckListUrl;
            var actualSkillsCheckListUrl = thanksPage.GetFillOutAppHref(formName);
            Assert.AreEqual(expectedSkillsCheckListUrl, actualSkillsCheckListUrl, "Skills checklist Href is not matched");

            Log.Info("Step 6: Click on 'Back To Job Search' button, verify 'Search' page is opened");
            var searchUrl = FmsUrl + "search/";
            thanksPage.ClickOnBackToJobSearchButton();
            Assert.IsTrue(Driver.GetCurrentUrl().StartsWith(searchUrl), "Search 'URL' not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyValidationMessageIsDisplayedForMandatoryFieldsInFullApplicationForm()
        {
            var fullApp = new FullApplicationPo(Driver);

            Log.Info("Step 1: Navigate to 'Full Application Form' page");
            fullApp.NavigateToPage();

            Log.Info("Step 2: Fill all mandatory fields except first name, click on submit button & verify validation message is displayed for firstname");
            var fullAppData = ApplyNowDataFactory.AddDataInFullAppForm();
            fullApp.EnterLastName(fullAppData.LastName);
            fullApp.EnterBirthDate(fullAppData);
            fullApp.SelectCategory(fullAppData.Category);
            fullApp.SelectSpecialty(fullAppData.Speciality);
            fullApp.EnterMailingAddress(fullAppData.MailingAddress);
            fullApp.EnterCity(fullAppData.City);
            fullApp.SelectState(fullAppData.State);
            fullApp.EnterZipCode(fullAppData.Zip);
            fullApp.EnterMobilePhone(fullAppData.PhoneNumber);
            fullApp.EnterBestTimeToCall(fullAppData);
            fullApp.EnterEmail(fullAppData.Email);
            fullApp.ClickOnSomeoneReferredMe(fullAppData.SomeoneReferredMe);
            fullApp.EnterReferredByText(fullAppData.ReferredBy);
            fullApp.EnterEmergencyContact(fullAppData.EmergencyContact);
            fullApp.EnterRelationShip(fullAppData.Relationship);
            fullApp.EnterEmergencyContactNumber(fullAppData.EmergencyPhoneNumber);
            fullApp.SelectDrugScreenTest(fullAppData);
            fullApp.SelectCriminalBackgroundCheck(fullAppData);
            fullApp.SelectLimitation(fullAppData);
            fullApp.SelectTermsAndCondition();
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for first name field");

            Log.Info("Step 3 : Clear Last name field and click on submit button & verify validation message is displayed for lastname");
            fullApp.EnterFirstName(fullAppData.FirstName);
            fullApp.EnterLastName("");
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for lastname");

            Log.Info("Step 4 : Clear category field and click on submit button & verify validation message is displayed for category");
            fullApp.EnterLastName(fullAppData.LastName);
            fullApp.ClearCategory();
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for category");

            Log.Info("Step 5: Clear mailing address field and click on submit button & verify validation message is displayed for mailing address");
            fullApp.SelectCategory(fullAppData.Category);
            fullApp.SelectSpecialty(fullAppData.Speciality);
            fullApp.EnterMailingAddress("");
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for mailing address");

            Log.Info("Step 6: Clear city field and click on submit button  & verify validation message is displayed for city");
            fullApp.EnterMailingAddress(fullAppData.Email);
            fullApp.EnterCity("");
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for city");

            Log.Info("Step 7: Clear state field and click on submit button & verify validation message is displayed for state");
            fullApp.EnterCity(fullAppData.City);
            fullApp.ClearState();
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for state");

            Log.Info("Step 8: Clear zipcode field and click on submit button & verify validation message is displayed for zipcode");
            fullApp.SelectState(fullAppData.State);
            fullApp.EnterZipCode("");
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for zipcode");

            Log.Info("Step 9: Clear mobile number field and click on submit button & verify validation message is displayed for mobile number");
            fullApp.EnterZipCode(fullAppData.Zip);
            fullApp.EnterMobilePhone("");
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for mobile number");

            Log.Info("Step 10: Clear best time to call field and click on submit button & verify validation message is displayed for best time to call");
            fullApp.EnterMobilePhone(fullAppData.PhoneNumber);
            fullApp.ClearBestTimeToCall("");
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for mobile best time to call");

            Log.Info("Step 11: Clear email field and click on submit button & verify validation message is displayed for email");
            fullApp.EnterBestTimeToCall(fullAppData);
            fullApp.EnterEmail("");
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for email");

            Log.Info("Step 12: Clear Referred by field and click on submit button & verify validation message is displayed for Referred by");
            fullApp.EnterEmail(fullAppData.Email);
            fullApp.EnterReferredByText("");
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsReferredByFieldDisplayed(),"referred by field is not displayed");
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Referred by");

            Log.Info("Step 13: Clear emergency contact field and click on submit button & verify validation message is displayed for  emergency contact");
            fullApp.EnterReferredByText(fullAppData.ReferredBy);
            fullApp.EnterEmergencyContact("");
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for  emergency contact");

            Log.Info("Step 14: Clear relationship field and click on submit button and click on submit button & verify validation message is displayed for relationship");
            fullApp.EnterEmergencyContact(fullAppData.EmergencyContact);
            fullApp.EnterRelationShip("");
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for relationship");

            Log.Info("Step 15: Clear emergency phone number field and click on submit button & verify validation message is displayed for emergency phone number ");
            fullApp.EnterRelationShip(fullAppData.Relationship);
            fullApp.EnterEmergencyContactNumber("");
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for emergency phone number ");

            Log.Info("Step 16 : Clear terms and condition field and click on submit button & verify validation message is displayed for terms and condition ");
            fullApp.EnterEmergencyContactNumber(fullAppData.EmergencyPhoneNumber);
            fullApp.SelectTermsAndCondition();
            fullApp.ClickOnSubmitButton();
            fullApp.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(fullApp.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for terms and condition ");
        }
    }
}
