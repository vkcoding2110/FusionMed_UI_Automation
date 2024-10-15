using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMS.Home;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.Home
{
    [TestClass]
    [TestCategory("FMS"), TestCategory("RequestStaff")]
    public class RequestStaffTests : FmsBaseTest
    {
        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatRequestStaffFormWorksSuccessfullyWithoutUserLogIn()
        {
            var homePage = new HomePagePo(Driver);
            var requestStaff = new RequestStaffPo(Driver);

            Log.Info("Step 1: Navigate to 'Request Staff' page & verify 'Request Staff' page gets open");
            requestStaff.NavigateToPage();
            var expectedRequestStaffUrl = FmsUrl + "client/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedRequestStaffUrl), $"{expectedRequestStaffUrl} Healthcare Provider page page url is not matched");

            homePage.WaitUntilMpPageLoadingIndicatorInvisible();
            var requestStaffData = RequestStaffDataFactory.AddDataInRequestStaffForm();
            requestStaff.AddRequestStaffData(requestStaffData);
            requestStaff.ClickOnSubmitButton();
            requestStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            requestStaff.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Verify Request staff form submitted successfully, Success text and url is matched");
            const string expectedSuccessText = "Thank you!";
            var actualSuccessText = requestStaff.GetRequestStaffSuccessText();
            var expectedUrl = FmsUrl + "client/thank-you/";
            var actualUrl = Driver.GetCurrentUrl();
            Assert.IsTrue(Driver.IsUrlContains(expectedUrl), $"Url is not matched. Expected : {expectedUrl} , Actual : {actualUrl}");
            Assert.AreEqual(expectedSuccessText.ToLowerInvariant(), actualSuccessText.ToLowerInvariant(), "Success header text is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyValidationMessageIsDisplayedForMandatoryFieldsInRequestStaffForm()
        {
            var requestStaff = new RequestStaffPo(Driver);

            Log.Info("Step 1: Navigate to 'Request Staff' page & verify 'Request Staff' page gets open");
            requestStaff.NavigateToPage();

            Log.Info("Step 2: fill all form data except 'facility name', Click on 'Submit' button &  Verify validation message is displayed for Facility name");
            var requestStaffData = RequestStaffDataFactory.AddDataInRequestStaffForm();
            requestStaff.EnterYourName(requestStaffData.YourName);
            requestStaff.EnterPhoneNumber(requestStaffData.PhoneNumber);
            requestStaff.EnterEmailId(requestStaffData.Email);
            requestStaff.SelectProfessionalTypeDopDown(requestStaffData.ProfessionalType);
            requestStaff.SelectSpecialtyDropDown(requestStaffData.Specialty);
            requestStaff.SelectJobTypeDropDown(requestStaffData.JobType);
            requestStaff.ClickOnSubmitButton();
            requestStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(requestStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Facility name");

            Log.Info("Step 3: Clear Your name field, Click on 'Submit' button &  Verify validation message is displayed for Your name");
            requestStaff.EnterFacilityName(requestStaffData.FacilityName);
            requestStaff.EnterYourName("");
            requestStaff.ClickOnSubmitButton();
            requestStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(requestStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Your name");

            Log.Info("Step 4: Clear Phone number field, Click on 'Submit' button &  Verify validation message is displayed for Phone number");
            requestStaff.EnterYourName(requestStaffData.YourName);
            requestStaff.EnterPhoneNumber("");
            requestStaff.ClickOnSubmitButton();
            requestStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(requestStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Phone number");

            Log.Info("Step 5: Clear Email field, Click on 'Submit' button &  Verify validation message is displayed for Email");
            requestStaff.EnterPhoneNumber(requestStaffData.PhoneNumber);
            requestStaff.EnterEmailId("");
            requestStaff.ClickOnSubmitButton();
            requestStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(requestStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Email");

            Log.Info("Step 6: Clear Specialty field, Click on 'Submit' button &  Verify validation message is displayed for Specialty");
            requestStaff.EnterEmailId(requestStaffData.Email);
            requestStaff.ClearProfessionalTypeDropDown();
            requestStaff.ClickOnSubmitButton();
            requestStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(requestStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Specialty");

            Log.Info("Step 7: Clear Job type field, Click on 'Submit' button &  Verify validation message is displayed for Job type");
            requestStaff.SelectProfessionalTypeDopDown(requestStaffData.ProfessionalType);
            requestStaff.SelectSpecialtyDropDown(requestStaffData.Specialty);
            requestStaff.ClearJobTypeDropDown();
            requestStaff.ClickOnSubmitButton();
            requestStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(requestStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Job type");
        }
    }
}