using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMS.Home;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.Home
{
    [TestClass]
    [TestCategory("FMS"), TestCategory("RequestOnSiteStaff")]
    public class RequestOnSiteStaffTests : FmsBaseTest
    {
        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatRequestOnSiteStaffFormWorksSuccessfullyWithoutUserLogIn()
        {
            var homePage = new HomePagePo(Driver);
            var footer= new FooterPo(Driver);
            var requestStaff= new RequestStaffPo(Driver);
            var requestOnSiteStaff = new RequestOnSiteStaffPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            homePage.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Navigate to Work safety solution page");
            footer.ClickOnRequestStaff();
            requestStaff.ClickOnWorkPlaceSafetySolution();

            var expectedRequestOnSiteStaffUrl = FmsUrl + "workplace-safety-solutions/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedRequestOnSiteStaffUrl), $"{expectedRequestOnSiteStaffUrl} Work place safety url is not matched");

            Log.Info("Step 3: Add data in Request on-site staff form");
            var requestStaffData = RequestStaffDataFactory.AddDataInRequestStaffForm();
            requestOnSiteStaff.EnterRequestOnSiteStaffDetails(requestStaffData);
            requestOnSiteStaff.ClickOnSubmitButton();
            requestOnSiteStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            requestOnSiteStaff.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 4: Verify Request on-site staff form submitted successfully, Success text and url is matched");
            const string expectedSuccessText = "Help is on the way!";
            var actualSuccessText = requestOnSiteStaff.GetRequestOnSiteStaffSuccessText();
            Assert.AreEqual(expectedSuccessText.ToLowerInvariant(), actualSuccessText.ToLowerInvariant(), "Success header text is not matched");
            
            var expectedUrl = FmsUrl + "workplace-safety-solutions/thank-you/";
            var actualUrl = Driver.GetCurrentUrl();
            Assert.AreEqual(expectedUrl, actualUrl, "Thank-you page url is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyValidationMessageIsDisplayedForMandatoryFieldsInRequestOnSiteStaffForm()
        {
            var homePage = new HomePagePo(Driver);
            var requestOnSiteStaff = new RequestOnSiteStaffPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            homePage.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Navigate to Work safety solution page");
            requestOnSiteStaff.NavigateToPage();

            Log.Info("Step 3: fill all form data except 'facility name', Click on 'Submit' button &  Verify validation message is displayed for Facility name");
            var requestStaffData = RequestStaffDataFactory.AddDataInRequestStaffForm();
            requestOnSiteStaff.EnterYourName(requestStaffData.YourName);
            requestOnSiteStaff.EnterPhoneNumber(requestStaffData.PhoneNumber);
            requestOnSiteStaff.EnterEmailAddress(requestStaffData.Email);
            requestOnSiteStaff.SelectSolutionTypeDropDown(requestStaffData.SolutionType);
            requestOnSiteStaff.SelectJobTypeDropDown(requestStaffData.JobType);
            requestOnSiteStaff.EnterStartDate(requestStaffData.StartDate);
            requestOnSiteStaff.EnterMessage(requestStaffData.Message);
            requestOnSiteStaff.ClickOnSubmitButton();
            requestOnSiteStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(requestOnSiteStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Facility name");

            Log.Info("Step 4: Clear Your name field, Click on 'Submit' button &  Verify validation message is displayed for Your name");
            requestOnSiteStaff.EnterFacilityName(requestStaffData.FacilityName);
            requestOnSiteStaff.EnterYourName("");
            requestOnSiteStaff.ClickOnSubmitButton();
            requestOnSiteStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(requestOnSiteStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Your name");

            Log.Info("Step 5: Clear Phone number field, Click on 'Submit' button &  Verify validation message is displayed for Phone number");
            requestOnSiteStaff.EnterYourName(requestStaffData.YourName);
            requestOnSiteStaff.EnterPhoneNumber("");
            requestOnSiteStaff.ClickOnSubmitButton();
            requestOnSiteStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(requestOnSiteStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Phone number");

            Log.Info("Step 6: Clear Email field, Click on 'Submit' button &  Verify validation message is displayed for Email");
            requestOnSiteStaff.EnterPhoneNumber(requestStaffData.PhoneNumber);
            requestOnSiteStaff.EnterEmailAddress("");
            requestOnSiteStaff.ClickOnSubmitButton();
            requestOnSiteStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(requestOnSiteStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Email");

            Log.Info("Step 7: Clear Solution type field, Click on 'Submit' button &  Verify validation message is displayed for Solution type");
            requestOnSiteStaff.EnterEmailAddress(requestStaffData.Email);
            requestOnSiteStaff.ClearSolutionTypeDropDown();
            requestOnSiteStaff.ClickOnSubmitButton();
            requestOnSiteStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(requestOnSiteStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for Solution type");

            Log.Info("Step 8: Clear job type field, Click on 'Submit' button &  Verify validation message is displayed for job type");
            requestOnSiteStaff.SelectSolutionTypeDropDown(requestStaffData.SolutionType);
            requestOnSiteStaff.ClearJobTypeDropDown();
            requestOnSiteStaff.ClickOnSubmitButton();
            requestOnSiteStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            Assert.IsTrue(requestOnSiteStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for job type");

            // Enable assert when Issue #25 from sheet get resolved
            //Log.Info("Step 9: Clear start date field, Click on 'Submit' button &  Verify validation message is displayed for start date");
            //requestOnSiteStaff.SelectJobTypeDropDown(requestStaffData.JobType);
            //requestOnSiteStaff.ClearStartDate("");
            //requestOnSiteStaff.ClickOnSubmitButton();
            //requestOnSiteStaff.WaitUntilMpSubmitFormLoadingIndicatorInvisible();
            //Assert.IsTrue(requestOnSiteStaff.IsMpFormValidationMessageDisplayed(), "validation message is not displayed for start date");
        }
    }
}

