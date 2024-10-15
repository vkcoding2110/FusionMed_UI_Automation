using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Agencies;
using UIAutomation.PageObjects.FMP.Agencies;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Agencies
{
    [TestClass]
    [TestCategory("PartnerWithUs"), TestCategory("FMP")]
    public class PartnerWithUsTests : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatLetsTalkButtonWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var agencies = new AgenciesPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Agencies', click on 'Let's Talk' Button & verify 'Partner with us' page gets open");
            fmpHeader.ClickOnAgenciesLink();
            agencies.ClickOnLetsTalkButton();
            var expectedPartnerWithUsUrl = FusionMarketPlaceUrl + "agencies/#partner-with-us";
            Assert.IsTrue(Driver.IsUrlMatched(expectedPartnerWithUsUrl), $"{expectedPartnerWithUsUrl} Partner with us page url is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatPartnerWithUsFormSubmitWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var agencies = new AgenciesPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Agencies' link");
            fmpHeader.ClickOnAgenciesLink();

            Log.Info("Step 3: Fill data in 'Partner with us' form, click on 'Submit' button and verify success Message");
            var partnerWithUsData = PartnerWithUsDataFactory.AddPartnerWithUsFormDetails();
            agencies.AddDataInPartnerWithUsForm(partnerWithUsData);
            const string expectedSuccessMessage = "Message Sent!";
            Assert.AreEqual(expectedSuccessMessage, agencies.GetSuccessMessageText(), "Success Message doesn't match");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatPartnerWithUsFormValidationMessage()
        {
            var fmpHeader = new HeaderPo(Driver);
            var agencies = new AgenciesPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Agencies' link");
            fmpHeader.ClickOnAgenciesLink();

            Log.Info("Step 3: Do not enter 'Name', click on 'Submit' button & verify validation message is displayed");
            var partnerWithUsData = PartnerWithUsDataFactory.AddPartnerWithUsFormDetails();
            agencies.EnterName("");
            agencies.EnterTitle(partnerWithUsData.Title);
            agencies.EnterWorkEmail(partnerWithUsData.WorkEmail);
            agencies.EnterPhoneNumber(partnerWithUsData.PhoneNumber);
            agencies.EnterAgencyName(partnerWithUsData.AgencyName);
            agencies.EnterNumberOfRecruiters(partnerWithUsData.NumberOfRecruiters);
            agencies.ClickOnRequestPartnerInformationButton();
            const string expectedValidationMessage = "This field is required.";
            Assert.AreEqual(expectedValidationMessage, agencies.GetNameValidationMessageText(), "Validation Message doesn't match");

            Log.Info("Step 4: Do not enter 'Title', click on 'Submit' button & verify validation message is displayed");
            agencies.EnterName(partnerWithUsData.Name);
            agencies.EnterTitle("");
            agencies.ClickOnRequestPartnerInformationButton();
            Assert.AreEqual(expectedValidationMessage, agencies.GetTitleValidationMessageText(), "Validation Message doesn't match");

            Log.Info("Step 5: Do not enter 'Work Email', click on 'Submit' button & verify validation message is displayed");
            agencies.EnterTitle(partnerWithUsData.Title);
            agencies.EnterWorkEmail("");
            agencies.ClickOnRequestPartnerInformationButton();
            Assert.AreEqual(expectedValidationMessage, agencies.GetEmailValidationMessageText(), "Validation Message doesn't match");

            Log.Info("Step 6: Do not enter 'Phone Number', click on 'Submit' button & verify validation message is displayed");
            agencies.EnterWorkEmail(partnerWithUsData.WorkEmail);
            agencies.EnterPhoneNumber("");
            agencies.GetPhoneNumberValidationMessageText();
            agencies.ClickOnRequestPartnerInformationButton();
            Assert.AreEqual(expectedValidationMessage, agencies.GetPhoneNumberValidationMessageText(), "Validation Message doesn't match");

            Log.Info("Step 7: Do not enter 'AgencyName', click on 'Submit' button & verify validation message is displayed");
            agencies.EnterPhoneNumber(partnerWithUsData.PhoneNumber);
            agencies.EnterAgencyName("");
            agencies.GetAgencyNameValidationMessageText();
            agencies.ClickOnRequestPartnerInformationButton();
            Assert.AreEqual(expectedValidationMessage, agencies.GetAgencyNameValidationMessageText(), "Validation Message doesn't match");
        }
    }
}
