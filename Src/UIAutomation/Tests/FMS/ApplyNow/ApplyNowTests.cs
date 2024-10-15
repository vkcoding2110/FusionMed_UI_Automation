using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.ApplyNow
{
    [TestClass]
    [TestCategory("ApplyNow"), TestCategory("FMS")]
    public class ApplyNowTests : BaseTest
    {

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyApplyNowButtonWorkSuccessfully()
        {
            var homePage = new HomePagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            homePage.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Apply Now' button icon & verify Apply Page is opened");
            homePage.ClickOnApplyNowButton();
            homePage.WaitUntilMpPageLoadingIndicatorInvisible();
            var expectedUrl = FmsUrl + "apply/quick-application/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedUrl), $"{expectedUrl} Apply page url is not matched");
        }
    }
}
