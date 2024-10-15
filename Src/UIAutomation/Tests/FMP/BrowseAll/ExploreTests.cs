using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.BrowseAll
{
    [TestClass]
    [TestCategory("Explore"), TestCategory("FMP")]
    public class ExploreTests : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyExploreMenuOpenedAndClosedWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button & verify 'Explore' popup gets open");
            fmpHeader.ClickOnBrowseAllButton();
            exploreMenu.WaitUntilExploreMenuOpen();
            Assert.IsTrue(exploreMenu.IsExploreMenuOpened(), "Explore menu is not opened");

            Log.Info("Step 3: Click on 'Close' icon & verify 'Explore' popup gets closed");
            exploreMenu.ClickOnExploreMenuCloseIcon();
            Assert.IsFalse(exploreMenu.IsExploreMenuOpened(), "Explore menu is still opened");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatExploreMenuListItemsAreCorrect()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();
            string[] exploreMenus = { "Jobs", "Agencies",};
            var actualExploreMenusList = exploreMenu.GetExploreMenusList();
            CollectionAssert.AreEqual(exploreMenus.ToList(), actualExploreMenusList.ToList(), "Explore menu list is not matched");
        }
    }
}
