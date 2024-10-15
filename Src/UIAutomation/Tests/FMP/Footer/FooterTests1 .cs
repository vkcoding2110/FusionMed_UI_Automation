using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Footer;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Footer
{
    [TestClass]
    [TestCategory("Footer"), TestCategory("FMP")]
    public class FooterTests1 : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyFusionMarketplaceLinksWorksSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on FusionMarketplace Link & verify FusionMarketplace page is opened");
            footer.ClickOnFusionMarketplaceLink();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            var expectedFusionMarketplaceUrl = FusionMarketPlaceUrl;
            Assert.AreEqual(expectedFusionMarketplaceUrl, Driver.GetCurrentUrl(), $"{expectedFusionMarketplaceUrl} FusionMarketplace page url is not matched");

            Log.Info("Step 3: Click on About Us link & verify about us page is opened");
            footer.ClickOnAboutUsLink();
            var expectedAboutUsUrl = FusionMarketPlaceUrl + "about/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedAboutUsUrl), $"{expectedAboutUsUrl} About us page url is not matched");

            Log.Info("Step 4: Click on Contact Us link & verify Contact Us page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOnContactUsLink();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            var expectedContactUsUrl = FusionMarketPlaceUrl + "contact/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedContactUsUrl), $"{expectedContactUsUrl} Contact Us page url is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyTravelersLinksWorksSuccessfully()
        {
            var footer = new FooterPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on Travelers Link & verify Travelers page is opened");
            footer.ClickOnTravelersLink();
            var expectedTravelersUrl = FusionMarketPlaceUrl + "healthcare-travelers/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedTravelersUrl), $"{expectedTravelersUrl} Travelers page url is not matched");

            Log.Info("Step 3: Click on Login Link & verify Login page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOnLoginLink();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            const string expectedTitle = "LOGIN";
            var actualTitle = fmpLogin.GetLoginPageHeader();
            Assert.AreEqual(expectedTitle.ToLowerInvariant(), actualTitle.ToLowerInvariant(), "Login page title is not matched");

            Log.Info("Step 4: Click on Jobs Link & verify Jobs page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOnJobsLink();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            var expectedJobsUrl = FusionMarketPlaceUrl + "search/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedJobsUrl), $"{expectedJobsUrl} Jobs page url is not matched");

            Log.Info("Step 4: Click on 'State Vaccination Map' Link & verify 'State Vaccination Map' page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOnStateVaccinationMapLink();
            const string expectedStateVaccinationMapLink = "https://info.fusionmarketplace.com/covid19statevaccinationmap";
            var actualStateVaccinationMapLink = footer.GetStateVaccinationMapHref();
            Assert.AreEqual(expectedStateVaccinationMapLink, actualStateVaccinationMapLink, "State Vaccination Map page url doesn't match");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyBlogLinkWorksSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on Blog Link & verify Blog page is opened");
            footer.ClickOnBlogLink();
            const string expectedBlogHref = "https://blog.fusionmarketplace.com/";
            var actualBlogPageHref = footer.GetBlogHref();
            Assert.IsTrue(actualBlogPageHref.Contains(expectedBlogHref), "Blog page url is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyThatSocialMediaLinksWorksSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Facebook' icon & verify 'Facebook' page gets open");
            var actualFacebookHref = footer.GetFacebookIconHref();
            Assert.IsTrue(actualFacebookHref.Contains("https://www.facebook.com/FusionMrktplace"), "The 'Facebook' url is not matched");

            Log.Info("Step 3: Click on 'Instagram' Icon icon & verify 'Instagram' page gets open");
            var actualInstagramHref = footer.GetInstagramIconHref();
            Assert.IsTrue(actualInstagramHref.Contains("https://www.instagram.com/fusionmrktplace/"), "The 'Instagram' url is not matched");

            Log.Info("Step 4: Click on 'LinkedIn' icon & verify 'LinkedIn' page gets open");
            var actualLinkedInHref = footer.GetLinkedInIconHref();
            Assert.IsTrue(actualLinkedInHref.Contains("https://www.linkedin.com/company/fusionmrktplace"), "The 'LinkedIn' url is not matched");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyHealthcareAgenciesLinksWorksSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on Healthcare Agencies & verify Partner with us fusion marketplace page is opened");
            footer.ClickOnHealthcareAgencies();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            var expectedHealthcareProviderUrl = FusionMarketPlaceUrl + "agencies/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedHealthcareProviderUrl), $"{expectedHealthcareProviderUrl} Healthcare Agencies page url is not matched");

            Log.Info("Step 3: Click on Become a partner & verify Partner with fusion marketplace Page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOnBecomePartnerLink();
            var expectedBecomePartnerUrl = FusionMarketPlaceUrl + "agencies/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedBecomePartnerUrl), $"{expectedBecomePartnerUrl} Become partner page url is not matched");

            Log.Info("Step 4: Click on Fusion medical staffing link & Verify Fusion medical staffing Page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOnFusionMedicalStaffingLink();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            var expectedFusionMedicalStaffingUrl = FusionMarketPlaceUrl + "agencies/fusion-medical-staffing/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedFusionMedicalStaffingUrl), $"{expectedFusionMedicalStaffingUrl} Fusion medical staffing page url is not matched");

            Log.Info("Step 5: Click on Get med staffing link & Verify Get med staffing Page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOnGetMedStaffingLink();
            var expectedGetMedicalStaffingUrl = FusionMarketPlaceUrl + "agencies/getmed-staffing-inc/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedGetMedicalStaffingUrl), $"{expectedGetMedicalStaffingUrl} Get med staffing page url is not matched");

            Log.Info("Step 6: Click on Aequor health care Agency link & Verify Aequor health care Agency Page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOnAequorHealthcareAgencyLink();
            var expectedAequorHealthcareAgencyLinkUrl = FusionMarketPlaceUrl + "agencies/aequor-healthcare/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedAequorHealthcareAgencyLinkUrl), $"{expectedAequorHealthcareAgencyLinkUrl} Aequor healthcare agency page url is not matched");

            Log.Info("Step 7: Click on Lead health care Agency link & Verify Lead health care Agency Page is opened");
            Driver.Back();
            footer.WaitUntilFmpPageLoadingIndicatorInvisible();
            footer.ClickOnLeadHealthcareAgencyLink();
            var expectedLeadHealthcareAgencyLinkUrl = FusionMarketPlaceUrl + "agencies/lead-healthstaff/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedLeadHealthcareAgencyLinkUrl), $"{expectedLeadHealthcareAgencyLinkUrl} Lead healthcare page url is not matched");
        }
    }
}
