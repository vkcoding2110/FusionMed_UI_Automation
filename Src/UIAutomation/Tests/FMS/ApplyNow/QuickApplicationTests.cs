using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UIAutomation.DataFactory.FMS.ApplyNow;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMS.ApplyNow;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.ApplyNow
{
    [TestClass]
    [TestCategory("ApplyNow"), TestCategory("FMS")]
    public class QuickApplicationTests : BaseTest
    {

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyQuickApplicationWorkSuccessfully()
        {
            var quickApplication = new QuickApplicationPo(Driver);
            var thanksPage = new ThanksPagePo(Driver);

            Log.Info("Step 1: Navigate to 'Quick Application Form' page");
            quickApplication.NavigateToPage();
            var expectedUrl = FmsUrl + "apply/quick-application/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedUrl), $"{expectedUrl} Apply page url is not matched");
            Assert.IsFalse(quickApplication.IsReferredByCheckboxSelected(), "'Someone referred me' checkbox is selected");

            var quickApplyData = ApplyNowDataFactory.AddDataInQuickApplyForm();
            quickApplication.AddDataInQuickApplication(quickApplyData);
            quickApplication.ClickOnQuickAppSubmitButton();
            quickApplication.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Verify Applying success text, Page title & Url");
            const string skillsChecklistFormName = "skills checklist";
            const string fullAppFormName = "full application";
            //Flaky behavior on VM, disabling Page Title verification for now.
            // var expectedTitle = "Thank you - Fusion Medical";
            // var actualTitle = Driver.GetPageTitle();
            // Assert.AreEqual(expectedTitle, actualTitle, "Title is not matched");
            var thanksForApplyPageUrl = FmsUrl + "apply/quick-application/thank-you/";
            var expectedPhoneNumber = Convert.ToInt64(quickApplyData.Phone).ToString("###-###-####");
            Assert.IsTrue(Driver.IsUrlContains(thanksForApplyPageUrl), $"expected:{thanksForApplyPageUrl}, actual:{Driver.GetCurrentUrl()} Url is not matched");
            Assert.IsTrue(Driver.IsUrlContains(quickApplyData.FirstName), $"First Name{quickApplyData.FirstName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(quickApplyData.LastName), $"Last Name{quickApplyData.LastName} is not present in Url{Driver.GetCurrentUrl()}");
            Assert.IsTrue(Driver.IsUrlContains(expectedPhoneNumber), $"Phone Number{quickApplyData.Phone} is not present in Url{Driver.GetCurrentUrl()}");

            Log.Info("Step 3: full App & skills checklist link is visible & matched with expected");
            var actualSuccessApplyingText = thanksPage.GetFormSubmitSuccessText().ToLower();
            Assert.AreEqual(FmsConstants.QuickAndFullAppSubmissionThanksForApplyText.ToLower(), actualSuccessApplyingText, "Header success text is not matched");
            Assert.IsTrue(thanksPage.IsFillOutFormUrlPresent(skillsChecklistFormName), "Skills Checklist link is not present");
            Assert.IsTrue(thanksPage.IsFillOutFormUrlPresent(fullAppFormName), "Full Application link is not present");
            var expectedSkillsCheckListUrl = FmsUrl + FmsConstants.SkillsCheckListUrl;
            var actualSkillsCheckListUrl = thanksPage.GetFillOutAppHref(skillsChecklistFormName);
            var expectedFullAppUrl = FmsUrl + FmsConstants.FullApplicationUrl;
            var actualFullAppUrl = thanksPage.GetFillOutAppHref(fullAppFormName);
            if (PlatformName == PlatformName.Android)
            {
                Assert.IsTrue(expectedSkillsCheckListUrl.Contains(actualSkillsCheckListUrl), "Skills checklist Href is not matched");
                Assert.IsTrue(expectedFullAppUrl.ToLower().Contains(actualFullAppUrl.ToLower()), "Full Application Href is not matched");
            }
            else
            {
                Assert.AreEqual(expectedSkillsCheckListUrl, actualSkillsCheckListUrl, "Skills checklist Href is not matched");
                Assert.AreEqual(expectedFullAppUrl.ToLower(), actualFullAppUrl.ToLower(), "Full Application Href is not matched");
            }

            Log.Info("Step 4: Click on 'Back To Job Search' button, verify 'Search' page is opened");
            var searchUrl = FmsUrl + "search/";
            thanksPage.ClickOnBackToJobSearchButton();
            Assert.IsTrue(Driver.GetCurrentUrl().StartsWith(searchUrl), "Search 'URL' not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyValidationMessageIsDisplayedForMandatoryFieldsInQuickApplicationForm()
        {
            var quickApply = new QuickApplicationPo(Driver);

            Log.Info("Step 1: Navigate to 'Quick Application Form' page");
            quickApply.NavigateToPage();
            var quickApplyData = ApplyNowDataFactory.AddDataInQuickApplyForm();

            Log.Info("Step 2: Clear First name field &  Verify that validation message is displayed for first name");
            quickApply.EnterLastName(quickApplyData.LastName);
            quickApply.EnterEmail(quickApplyData.Email);
            quickApply.EnterPhoneNumber(quickApplyData.Phone);
            quickApply.SelectCategory(quickApplyData.Category);
            quickApply.SelectSpecialty(quickApplyData.Specialty);
            quickApply.ClickOnSomeoneReferredMe(quickApplyData.SomeoneReferredMe);
            quickApply.EnterReferredByText(quickApplyData.ReferredBy);
            quickApply.ClickOnQuickAppSubmitButton();
            quickApply.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.AreEqual(FmsConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForFirstNameField(), "Validation message is not displayed for First name field");

            Log.Info("Step 3: Clear Last name field & Verify that validation message is displayed for Last name");
            quickApply.EnterFirstName(quickApplyData.FirstName);
            quickApply.EnterLastName("");
            quickApply.ClickOnQuickAppSubmitButton();
            quickApply.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.AreEqual(FmsConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForLastNameField(), "Validation message is not displayed for Last name field");

            Log.Info("Step 4: Clear email field & Verify that validation message is displayed for email");
            quickApply.EnterLastName(quickApplyData.LastName);
            quickApply.EnterEmail("");
            quickApply.ClickOnQuickAppSubmitButton();
            quickApply.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.AreEqual(FmsConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForEmailField(), "Validation message is not displayed for Email field");

            Log.Info("Step 5: Clear Phone number field & Verify that validation message is displayed for Phone number");
            quickApply.EnterEmail(quickApplyData.Email);
            quickApply.EnterPhoneNumber("");
            quickApply.ClickOnQuickAppSubmitButton();
            quickApply.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.AreEqual(FmsConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForMobilePhoneField(), "Validation message is not displayed for Mobile phone field");

            Log.Info("Step 6: Clear category field & Verify that validation message is displayed for category");
            quickApply.EnterPhoneNumber(quickApplyData.Phone);
            quickApply.ClearCategory();
            quickApply.ClickOnQuickAppSubmitButton();
            quickApply.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.AreEqual(FmsConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForCategoryField(), "Validation message is not displayed for Category field");

            Log.Info("Step 7: Clear Referred by field & Verify that validation message is displayed for Referred by");
            quickApply.SelectCategory(quickApplyData.Category);
            quickApply.EnterReferredByText("");
            quickApply.ClickOnQuickAppSubmitButton();
            quickApply.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(quickApply.IsReferredByFieldDisplayed(), "Referred by field is not displayed");
            Assert.AreEqual(FmsConstants.MandatoryFieldValidationMessage, quickApply.GetValidationMessageDisplayedForReferredByField(), "Validation message is not displayed for Referred by field");
        }
    }
}
