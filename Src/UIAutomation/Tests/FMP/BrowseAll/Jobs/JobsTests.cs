using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.BrowseAll.Jobs
{
    [TestClass]
    [TestCategory("Explore"), TestCategory("FMP")]
    public class JobsTests : FmpBaseTest
    {
        private readonly JObject JobsObject = JObject.Parse(File.ReadAllText(new FileUtil().GetBasePath() + "/TestData/FMP/Jsons/jobs.json"));

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatJobsMenuOpenedSuccessfullyAndCloseIconAndBackButtonWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var jobsMenu = new JobsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 3: Click on 'Jobs' button & verify 'Jobs' menu popup gets open");
            const string exploreMenuButton = "Jobs";
            exploreMenu.ClickOnExploreMenuItemsButton(exploreMenuButton);
            Assert.AreEqual(exploreMenuButton, jobsMenu.GetJobsMenuHeaderText(), "Jobs menu header text doesn't match");

            Log.Info("Step 4: Click on 'Back' Arrow & verify 'Explore' menu popup gets open");
            jobsMenu.ClickOnBackButton();
            Assert.IsTrue(exploreMenu.IsExploreMenuOpened(), "Explore menu is not opened");

            Log.Info("Step 5: Click on jobs menu 'Close' icon & verify popup gets closed");
            exploreMenu.ClickOnExploreMenuItemsButton(exploreMenuButton);
            jobsMenu.ClickOnJobsMenuCloseIcon();
            Assert.IsFalse(jobsMenu.IsJobsMenuOpened(), "Jobs menu is still opened");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatJobsMenuListItemsAreCorrect()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var jobsMenu = new JobsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 3: Click on 'Jobs' button & verify jobs lists is matched");
            const string exploreMenuButton = "Jobs";
            exploreMenu.ClickOnExploreMenuItemsButton(exploreMenuButton);
            var expectedJobsList = new CSharpHelpers().GetJsonObjectChildrenToStringList(JobsObject,exploreMenuButton);
            var actualJobsList = jobsMenu.GetJobsMenusList();
            for (var i = 0; i < actualJobsList.Count; i++)
            {
                Assert.IsTrue(actualJobsList[i].StartsWith(expectedJobsList[i]), "Jobs list is not matched");
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatJobsMenuSubListItemsAreCorrect()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var jobsMenu = new JobsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button");
            fmpHeader.ClickOnBrowseAllButton();

            Log.Info("Step 3: Click on 'Jobs' button & verify jobs lists is matched");
            const string exploreMenuButton = "Jobs";
            exploreMenu.ClickOnExploreMenuItemsButton(exploreMenuButton);

            var expectedJobsSubList = new CSharpHelpers().GetJsonObjectChildrenToStringList(JobsObject, exploreMenuButton);
            expectedJobsSubList.Remove("View All Jobs");
            var stepCount = 4;
            foreach (var item in expectedJobsSubList)
            {
                Log.Info($"Step {stepCount}: Click on '{item}' to expand & verify list items & list header text is matched");
                if (expectedJobsSubList.Contains(item))
                {
                    jobsMenu.ClickOnJobsMenuSubListItems(item);
                }
                var actualListHeader = jobsMenu.GetJobsMenuHeaderText();
                var actualJobsList = jobsMenu.GetJobsMenusList();
                var expectedJobsList = new CSharpHelpers().GetJsonObjectChildrenToStringList(JobsObject, item);
                Assert.AreEqual(item, actualListHeader, $"{item} list header text is not matched");
                for (var i = 0; i < actualJobsList.Count; i++)
                {
                    Assert.IsTrue(actualJobsList[i].StartsWith(expectedJobsList[i]), "Jobs list is not matched");
                }
                jobsMenu.ClickOnBackButton();
                stepCount++;
            }
        }
    }
}
