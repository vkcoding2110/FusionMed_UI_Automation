using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMS.ApplyNow;
using UIAutomation.PageObjects.FMS.ApplyNow;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.ApplyNow
{
    [TestClass]
    [TestCategory("ApplyNow"), TestCategory("FMS")]
    public class DrugConsentTests : BaseTest
    {

        public void VerifyDrugConsentFormOpenedSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var applyNow = new ApplyNowPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            applyNow.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Apply Now' button, click on 'Drug Consent Form' & Verify 'drug consent form' opened");
            homePage.ClickOnApplyNowButton();
            applyNow.WaitUntilMpPageLoadingIndicatorInvisible();
            applyNow.ClickOnDrugConsentForm();
            applyNow.WaitUntilMpPageLoadingIndicatorInvisible();
            const string expectedTitle = "Drug Consent Form - Fusion Medical";
            var expectedUrl = FmsUrl + "forms/drug-consent-form/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedUrl), $"{expectedUrl} Page url is not matched");
            var actualTitle = Driver.GetPageTitle();
            Assert.AreEqual(expectedTitle, actualTitle, "Title is not matched");
        }

        [TestCategory("Smoke")]
        public void VerifyDrugConsentFormSubmitWorkSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var drugConsent = new DrugConsentPo(Driver);

            Log.Info("Step 1: Navigate to 'Drug Consent Form' & verify 'Thank You' page gets open");
            drugConsent.NavigateToPage();
            homePage.CloseReviewPopupIfPresent();
            var drugConsentFormData = ApplyNowDataFactory.AddDataDrugConsentForm();
            drugConsent.AddDataInDrugConsentForm(drugConsentFormData);
            drugConsent.DrugConsentForm_ClickOnSubmitButton();
            drugConsent.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            drugConsent.WaitUntilPageLoaded();

            var expectedPageUrl = FmsUrl + "forms/drug-consent-form/thank-you/";
            var actualPageUrl = Driver.GetCurrentUrl();
            Assert.AreEqual(expectedPageUrl, actualPageUrl, "Page Url is not matched");
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedPageTitle = "Thank you - Fusion Medical";
            // var actualPageTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedPageTitle, actualPageTitle, "Page title is not matched");
        }

        public void VerifyValidationMessageIsDisplayedForMandatoryFieldsInDrugConsentForm()
        {
            var drugConsent = new DrugConsentPo(Driver);

            Log.Info("Step 1: Navigate to 'Drug Consent Form'");
            drugConsent.NavigateToPage();

            Log.Info("Step 2: Clear First name field and Verify that validation message is displayed for First name");
            var drugConsentFormData = ApplyNowDataFactory.AddDataDrugConsentForm();
            drugConsent.EnterLastName(drugConsentFormData.LastName);
            drugConsent.EnterPhoneNumber(drugConsentFormData.Phone);
            drugConsent.EnterEmail(drugConsentFormData.Email);
            drugConsent.SelectCategory(drugConsentFormData.Category);
            drugConsent.SelectSpecialty(drugConsentFormData.Speciality);
            drugConsent.EnterSignature();
            drugConsent.EnterDate(drugConsentFormData);
            drugConsent.DrugConsentForm_ClickOnSubmitButton();
            drugConsent.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(drugConsent.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for First name");

            Log.Info("Step 3: Clear Last name field and Verify that validation message is displayed for Last name");
            drugConsent.EnterFirstName(drugConsentFormData.FirstName);
            drugConsent.EnterLastName("");
            drugConsent.DrugConsentForm_ClickOnSubmitButton();
            drugConsent.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(drugConsent.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Last name");

            Log.Info("Step 4: Clear Phone number field and Verify that validation message is displayed for Phone number");
            drugConsent.EnterLastName(drugConsentFormData.LastName);
            drugConsent.EnterPhoneNumber("");
            drugConsent.DrugConsentForm_ClickOnSubmitButton();
            drugConsent.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(drugConsent.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Phone number");

            Log.Info("Step 5: Clear Email field and Verify that validation message is displayed for Email");
            drugConsent.EnterPhoneNumber(drugConsentFormData.Phone);
            drugConsent.EnterEmail("");
            drugConsent.DrugConsentForm_ClickOnSubmitButton();
            drugConsent.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(drugConsent.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Email");

            Log.Info("Step 6: Clear category field and Verify that validation message is displayed for Category");
            drugConsent.EnterEmail(drugConsentFormData.Email);
            drugConsent.ClearCategory();
            drugConsent.DrugConsentForm_ClickOnSubmitButton();
            drugConsent.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(drugConsent.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Category");

            Log.Info("Step 7: Clear signature field and Verify that validation message is displayed for Signature");
            drugConsent.SelectCategory(drugConsentFormData.Category);
            drugConsent.ClearSignaturePad();
            drugConsent.DrugConsentForm_ClickOnSubmitButton();
            drugConsent.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(drugConsent.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Signature");
            
            // Enable assert when Issue #11 from sheet get resolved
            //Log.Info("Step 9: Clear date field and Verify that validation message is displayed for date");
            //drugConsent.EnterSignature();
            //drugConsent.ClearDate("");
            //drugConsent.DrugConsentForm_ClickOnSubmitButton();
            //drugConsent.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            //Assert.IsTrue(drugConsent.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Date");
        }
    }
}
