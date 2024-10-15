using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using UIAutomation.PageObjects.FMS.Careers;
using UIAutomation.PageObjects.FMS.Explore;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.Explore
{
    [TestClass]
    [TestCategory("Explore"), TestCategory("FMS")]
    public class DepartmentsTests : BaseTest
    {
        private readonly JObject JobsObject = JObject.Parse(File.ReadAllText(new FileUtil().GetBasePath() + "/TestData/FMS/Jsons/jobs.json"));

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyExploreMenuOpenedAndCloseWorkSuccessfully()
        {
            var department = new DepartmentsMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            department.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Explore' button & verify menu is opened");
            department.ClickOnExploreMenu();
            Assert.IsTrue(department.IsDepartmentsMenuOpened(), "Departments menu is not opened");

            Log.Info("Step 3: Click on 'Explore' close button & verify departments menu is closed");
            department.ClickOnCloseDepartmentMenu();
            Assert.IsFalse(department.IsDepartmentsMenuOpened(), "Departments menu is not closed");
        }

        [TestMethod]
        [TestCategory("Smoke"),TestCategory("MobileReady")]
        public void VerifyExploreMenuListItemSubItemsAreCorrect()
        {
            var department = new DepartmentsMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            department.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Explore' button, click on 'Jobs' button, click on jobs to open jobs list & verify list is matched");
            department.ClickOnExploreMenu();
            department.ClickOnJobsMenuButton();
            string[] exploreMenus = { "View All Jobs", "Cardiopulmonary", "Cath Lab", "Home Health", "Laboratory", "Long Term Care", "Radiology", "RN", "Therapy" };
            var actualJobList = department.GetJobMenuListItems();
            CollectionAssert.AreEqual(exploreMenus.ToList(), actualJobList.ToList(), "Jobs list is not matched");

        }

        [TestMethod]
        [TestCategory("Smoke"),TestCategory("MobileReady")]
        public void VerifyExploreJobsMenuSubItemAreCorrect()
        {
            var department = new DepartmentsMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            department.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Explore' button, click on jobs to open jobs list");
            department.ClickOnExploreMenu();
            department.ClickOnJobsMenuButton();

            string[] subMenus = { "Cardiopulmonary", "Cath Lab", "Home Health", "Laboratory", "Long Term Care", "Radiology", "RN", "Therapy" };
            var stepCount = 3;
            foreach (var item in subMenus)
            {
                Log.Info($"Step {stepCount}: Click on '{item}' to expand & verify list items & list header text is matched");

                department.ClickOnDepartmentsMenuSubListItems(item);
                var actualListHeader = department.GetDepartmentsMenuListHeaderText();
                var actualList = department.GetJobMenuListItems();
                var expectedCardiopulmonaryList = new CSharpHelpers().GetJsonObjectChildrenToStringList(JobsObject, item);
                Assert.AreEqual(item, actualListHeader, $"{item} list header text is not matched");
                CollectionAssert.AreEqual(expectedCardiopulmonaryList.ToList(), actualList.ToList(), $"{item} list is not matched");
                department.ClickOnDepartmentMenuPreviousButton();
                stepCount++;
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyExploreMenuWorksSuccessfully()
        {
            var department = new DepartmentsMenuPo(Driver);
            var opportunities = new OpportunitiesPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            department.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'Explore' button & click on 'Jobs' button & verify 'Jobs' menu gets open");
            department.ClickOnExploreMenu();
            department.ClickOnJobsMenuButton();
            const string expectedHeaderText = "Jobs";
            Assert.AreEqual(expectedHeaderText, department.GetDepartmentsMenuListHeaderText(), "Jobs menu is not displayed");

            Log.Info("Step 3: Click on 'Fusion Corporate Careers' link & verify 'Career' menu gets open");
            department.ClickOnDepartmentMenuPreviousButton();
            department.ClickOnFusionCorporateCareersLink();
            const string expectedCareersText = "Fusion Medical Staffing Careers";
            Assert.AreEqual(expectedCareersText, opportunities.GetCareersText(), "Careers page doesn't open");
        }
    }
}
