using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.Core.Common;
using UIAutomation.PageObjects.Microsoft;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.Core
{
    [TestClass]
    [TestCategory("Core"), TestCategory("SidebarMenu")]
    public class SidebarMenuTests: BaseTest
    {
        [TestMethod]
        public void VerifyThatSidebarExpandedCollapsedOnHamburgerIconClickSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var header = new HeaderPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info("Step 3: Verify that by default Left Nav is expanded");
            Assert.IsTrue(leftNav.IsLeftNavExpanded(), "Left Nav isn't expanded by default");

            Log.Info("Step 4: Click on Hamburger icon and verify that left nav is collapsed");
            header.ClickOnHamBurgerMenu();
            Assert.IsTrue(leftNav.IsLeftNavCollapsed(), "Left Nav isn't collapsed");

            Log.Info("Step 5: Click on Hamburger icon again and verify that left nav is expanded again");
            header.ClickOnHamBurgerMenu();
            Assert.IsTrue(leftNav.IsLeftNavExpanded(), "Left Nav isn't expanded");
        }   

        [TestMethod]
        public void VerifyThatAllLeftNavMenuWorksAsExpected()
        {
            var microsoftLogin = new LoginPo(Driver);
            var leftNav = new LeftNavPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info("Step 3: Click on Facility menu and verify that Facility grid is displayed");
            leftNav.ClickOnFacilityMenu();
            Assert.IsTrue(resultGrid.GetGridHeader().Contains("Facility"), "User isn't on Facility grid");

            Log.Info("Step 4: Click on Candidate menu and verify that Candidate grid is displayed");
            leftNav.ClickOnCandidateMenu();
            Assert.IsTrue(resultGrid.GetGridHeader().Contains("Candidate"), "User isn't on Candidate grid");

            Log.Info("Step 5: Click on Assignment menu and verify that Assignment grid is displayed");
            leftNav.ClickOnAssignmentMenu();
            Assert.IsTrue(resultGrid.GetGridHeader().Contains("Assignment"), "User isn't on Assignment grid");

            Log.Info("Step 6: Click on Administration > Entities  menu and verify that Entities grid is displayed");
            leftNav.ClickOnAdministrationMenu();
            leftNav.ClickOnEntitiesMenu();
            leftNav.WaitUntilProcessingIndicatorInvisible();
            Assert.IsTrue(resultGrid.GetGridHeader().Contains("Package Items"), "User isn't on Entities grid");

            Log.Info("Step 7: Click on Administration > Specifications  menu and verify that Specifications grid is displayed");
            leftNav.ClickOnSpecificationsMenu();
            leftNav.WaitUntilProcessingIndicatorInvisible();
            Assert.IsTrue(resultGrid.GetGridHeader().Contains("Specifications"), "User isn't on Specifications grid");

            Log.Info("Step 8: Click on Administration > Subcategories menu and verify that Subcategories grid is displayed");
            leftNav.ClickOnSubcategoriesMenu();
            leftNav.WaitUntilProcessingIndicatorInvisible();
            Assert.IsTrue(resultGrid.GetGridHeader().Contains("Subcategories"), "User isn't on Subcategories grid");

            Log.Info("Step 9: Click on Administration > Packages menu and verify that Packages grid is displayed");
            leftNav.ClickOnPackagesMenu();
            leftNav.WaitUntilProcessingIndicatorInvisible();
            Assert.IsTrue(resultGrid.GetGridHeader().Contains("Packages"), "User isn't on Packages grid");

            Log.Info("Step 10: Click on Administration > Rule Search menu and verify that Assignment Rule Search grid is displayed");
            leftNav.ClickOnRuleSearchMenu();
            leftNav.WaitUntilProcessingIndicatorInvisible();
            Assert.IsTrue(resultGrid.GetGridHeader().Contains("Assignment Rule Search"), "User isn't on Assignment Rule Search grid");

            Log.Info("Step 11: Click on Dashboard menu and verify that Dashboard grid is displayed");
            leftNav.ClickOnDashboardMenu();
            Driver.RefreshPage();
            Assert.IsTrue(resultGrid.GetGridHeader().Contains("Dashboard"), "User isn't on Dashboard grid");

        }
    }
}
