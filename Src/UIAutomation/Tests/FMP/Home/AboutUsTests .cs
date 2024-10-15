using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Footer;
using UIAutomation.PageObjects.FMP.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Home
{
    [TestClass]
    [TestCategory("AboutUs"), TestCategory("FMP")]
    public class AboutUsTests : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatAboutUsPageFindYourNextPartnerWithUsJobButtonsAndEmailLinksWorksSuccessfully()
        {
            var footer = new FooterPo(Driver);
            var aboutUs = new AboutUsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on About Us link");
            footer.ClickOnAboutUsLink();

            Log.Info("Step 3: Click on 'Find Your Next Job' Button and verify 'Healthcare-travelers' page gets opened");
            aboutUs.ClickOnFindYourNextJobButton();
            var expectedHealthCareTravelersUrl = FusionMarketPlaceUrl + "healthcare-travelers/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedHealthCareTravelersUrl), $"{expectedHealthCareTravelersUrl} Healthcare-travelers page url is not matched");

            Log.Info("Step 4: Click on 'Partner With Us' Button and verify 'agencies' page gets opened");
            Driver.Back();
            aboutUs.ClickOnPartnerWithUsButton();
            var expectedAgenciesUrl = FusionMarketPlaceUrl + "agencies/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedAgenciesUrl), $"{expectedAgenciesUrl} agencies page url is not matched");

            Log.Info("Step 4:verify 'Media Inquiries' And 'Public Relations' Href");
            Driver.Back();
            const string expectedMediaInquiriesHref = "mailto:press@fusionmarketplace.com";
            var actualMediaInquiriesHref = aboutUs.GetMediaInquiriesHref();
            Assert.AreEqual(expectedMediaInquiriesHref, actualMediaInquiriesHref, "Media Inquiries url is not matched");
        }
    }
}
