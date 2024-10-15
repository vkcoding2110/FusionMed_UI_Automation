using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Footer;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Footer
{
    [TestClass]
    [TestCategory("Footer"), TestCategory("FMP")]
    public class FooterTests2 : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyPoliciesLinkWorkSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on Traveler Terms And Agreement Link & verify Traveler Terms And Agreement page is opened");
            footer.ClickOnTravelerTermsAndAgreementLink();
            var expectedTravelerTermsAndAgreementUrl = FusionMarketPlaceUrl + "healthcare-travelers/terms/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedTravelerTermsAndAgreementUrl), $"{ expectedTravelerTermsAndAgreementUrl}Traveler Terms And Agreement page url is not matched");

            Log.Info("Step 3: Click on Traveler Privacy Policy Link & verify Traveler Privacy Policy page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOnTravelerPrivacyPolicyLink();
            var expectedTravelerPrivacyPolicyUrl = FusionMarketPlaceUrl + "healthcare-travelers/privacy-policy/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedTravelerPrivacyPolicyUrl), $"{ expectedTravelerPrivacyPolicyUrl} Traveler Privacy Policy URL page url is not matched");

            Log.Info("Step 4: Click on Partner Terms Agreement Link & verify Partner Terms Agreement page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOnPartnerTermsAgreementLink();
            var expectedPartnerTermsAgreementUrl = FusionMarketPlaceUrl + "agencies/terms/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedPartnerTermsAgreementUrl), $"{ expectedPartnerTermsAgreementUrl}Partner Terms Agreement page url is not matched");

            Log.Info("Step 5: Click on Partner Privacy Policy Link & verify Partner Privacy Policy page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOnPartnerPrivacyPolicyLink();
            var expectedPartnerPrivacyPolicyUrl = FusionMarketPlaceUrl + "agencies/privacy-policy/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedPartnerPrivacyPolicyUrl), $"{ expectedPartnerPrivacyPolicyUrl} Partner Privacy Policy URL page url is not matched");

            Log.Info("Step 6: Click on Employment Policies Link & verify Employment Policies page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOneEmploymentPoliciesLink();
            var expectedEmploymentPoliciesUrl = FusionMarketPlaceUrl + "policies/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedEmploymentPoliciesUrl), $"{ expectedEmploymentPoliciesUrl} Employment Policies url is not matched");
        }


        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyFusionMedicalStaffingLogoShouldOpenHomePageSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on Fusion medical staffing logo & verify Fusion medical staffing page is opened");
            footer.ClickFusionMedicalStaffingLogo();
            var actualGoldSealOfApprovalUrl = Driver.GetCurrentUrl();
            Assert.IsTrue(actualGoldSealOfApprovalUrl.Contains(FusionMarketPlaceUrl), "Fusion medical staffing url is not matched");
        }
    }
}