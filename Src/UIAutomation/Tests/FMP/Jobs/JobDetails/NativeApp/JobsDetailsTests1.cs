using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Account.NativeApp;
using UIAutomation.PageObjects.FMP.Home.NativeApp;
using UIAutomation.PageObjects.FMP.Jobs.NativeApp;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.JobDetails.NativeApp
{
    [TestClass]
    [TestCategory("Jobs"), TestCategory("NativeAppAndroid")]
    public class JobsDetailsTests1 : FmpBaseTest
    {
        [TestMethod]
        public void VerifyThatSocialMediaLinksWorksSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var jobDetails = new JobsDetailsPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);

            Log.Info("Step 2: Click on first job card & click on share icon");
            homePage.ClickOnJobCard();
            jobDetails.ClickOnShareIcon();

            Log.Info("Step 3: Click on 'Facebook' icon & verify 'Facebook' page gets open");
            jobDetails.ClickOnSocialMediaIcon(1);
            new WaitHelpers(Driver).HardWait(2000);
            var actualFacebookHref = Driver.GetNativeAppWebViewUrl();
            Assert.IsTrue(actualFacebookHref.StartsWith("https://m.facebook.com"), "The 'Facebook' url is not matched");

            Log.Info("Step 4: Click on 'Twitter' icon & verify 'Twitter' page gets open");
            Driver.Back();
            jobDetails.ClickOnSocialMediaIcon(2);
            new WaitHelpers(Driver).HardWait(2000);
            var actualTwitterHref = Driver.GetNativeAppWebViewUrl();
            Assert.IsTrue(actualTwitterHref.StartsWith("https://twitter.com"), "The 'Twitter' url is not matched");

            Log.Info("Step 5: Click on 'LinkedIn' icon & verify 'LinkedIn' page gets open");
            Driver.Back();
            jobDetails.ClickOnSocialMediaIcon(3);
            new WaitHelpers(Driver).HardWait(2000);
            var actualLinkedInHref = Driver.GetNativeAppWebViewUrl();
            Assert.IsTrue(actualLinkedInHref.StartsWith("https://www.linkedin.com"), "The 'LinkedIn' url is not matched");
        }
    }
}
