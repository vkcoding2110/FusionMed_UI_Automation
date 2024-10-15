using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMS.Explore;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.Home
{
    [TestClass]
    [TestCategory("HomePage"), TestCategory("FMS")]
    public class HomePageTests1 : BaseTest
    {
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyBrowseJobOpenedAndClosedSuccessfully()
        {
            var homePage = new HomePagePo(Driver);
            var departmentsMenu = new DepartmentsMenuPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            homePage.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Explore menu & verify jobs menu is opened & jobs menu list is matched with expected job list");
            departmentsMenu.ClickOnExploreMenu();
            const string expectedHeaderText = "Explore";
            var actualHeaderText = homePage.GetJobsOrRecruiterMenuHeaderText();
            Assert.AreEqual(expectedHeaderText, actualHeaderText, "Header text is not matched");
            departmentsMenu.ClickOnJobsMenuButton();

            IList<string> expectedList = new[] { "View All Jobs", "Cardiopulmonary", "Cath Lab", "Home Health", "Laboratory", "Long Term Care", "Radiology", "RN", "Therapy" };
            var actualList = homePage.GetJobsOrRecruiterMenuListItems();
            CollectionAssert.AreEqual(expectedList.ToList(), actualList.ToList(), "List is not matched");

            Log.Info("Step 3: Click on close menu & verify jobs menu is closed");
            homePage.ClickOnJobsOrRecruiterMenuCloseButton();
            Assert.IsFalse(homePage.IsJobsOrRecruiterMenuPresent(), "Jobs menu is present");
        }

        // Menu
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyMenuOpenAndClosedSuccessfully()
        {
            var homePage = new HomePagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            homePage.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on menu icon & verify menu is opened");
            homePage.ClickOnMenuButton();
            const string expectedMenuHeaderText = "Menu";
            var actualMenuHeaderText = homePage.GetMenuHeaderText();
            Assert.AreEqual(expectedMenuHeaderText, actualMenuHeaderText, "Menu Header text is not matched");

            Log.Info("Step 3: Verify menu list items is correct");
            var actualList = homePage.GetMenuListItems();
            var expectedList = PlatformName != PlatformName.Web ? new[] { "Explore", "Jobs", "Traveler", "Client", "Student", "Blog", "Apply Now", "Fusion Corporate Careers" } : new[] { "Jobs", "Traveler", "Client", "Student", "Blog" , "Fusion Corporate Careers" };
            CollectionAssert.AreEqual(expectedList.ToList(), actualList.ToList(), "List is not matched");
            Log.Info("Step 4: Click on close button & verify menu is closed");
            homePage.ClickOnCloseMenuButton();
            Assert.IsFalse(homePage.IsMenuPresent(), "Menu is present");
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void VerifyMenuListItemsWorkSuccessfully()
        {
            var homePage = new HomePagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);

            Log.Info("Step 2: Click on menu icon, Select 'Jobs' option & verify Jobs page is opened");
            homePage.WaitUntilMpPageLoadingIndicatorInvisible();
            homePage.ClickOnMenuButton();
            homePage.ClickOnNthMenuListItem("Jobs");
            homePage.WaitUntilMpPageLoadingIndicatorInvisible();
            const string expectedJobsTitle = "Search Results";
            var expectedJobsUrl = FmsUrl + "search/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedJobsUrl), $"{expectedJobsUrl} Jobs page url is not matched");
            var actualJobsTitle = Driver.GetPageTitle();
            Assert.AreEqual(expectedJobsTitle, actualJobsTitle, "Jobs page title is not matched");

            Log.Info("Step 3: Click on menu icon, Select 'Traveler' option & verify traveler page is opened");
            Driver.Back();
            homePage.ClickOnMenuButton();
            homePage.ClickOnNthMenuListItem("Traveler");
            const string expectedTravelerTitle = "Finding the Best Traveling Medical Jobs | Fusion Medical Staffing";
            var expectedTravelerUrl = FmsUrl + "traveler/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedTravelerUrl), $"{expectedTravelerUrl} Traveler page title is not matched");
            var actualTravelerTitle = Driver.GetPageTitle();
            Assert.AreEqual(expectedTravelerTitle, actualTravelerTitle, "Traveler page title is not matched");

            Log.Info("Step 4: Click on menu icon, Select 'Client' option & verify Client page is opened");
            Driver.Back();
            homePage.ClickOnMenuButton();
            homePage.ClickOnNthMenuListItem("Client");
            const string expectedClientTitle = "Medical Facilities & Hospital Staffing Agency | Fusion Medical Staffing";
            var expectedClientUrl = FmsUrl + "client/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedClientUrl), $"{expectedClientUrl} Client page title is not matched");
            var actualClientTitle = Driver.GetPageTitle();
            Assert.AreEqual(expectedClientTitle, actualClientTitle, "Client page title is not matched");

            Log.Info("Step 5: Click on menu icon, Select 'Student' option & verify Client page is opened");
            Driver.Back();
            homePage.ClickOnMenuButton();
            homePage.ClickOnNthMenuListItem("Student");
            const string expectedStudentTitle = "Student Outreach - Fusion Medical";
            var expectedStudentUrl = FmsUrl + "student/";
            Assert.IsTrue(Driver.IsUrlMatched(expectedStudentUrl), $"{expectedStudentUrl} Student page title is not matched");
            var actualStudentTitle = Driver.GetPageTitle();
            Assert.AreEqual(expectedStudentTitle, actualStudentTitle, "Student page title is not matched");

            Log.Info("Step 6: Click on menu icon, Select 'Blog' option & verify Blog is opened");
            Driver.Back();
            homePage.ClickOnMenuButton();
            homePage.ClickOnNthMenuListItem("Blog");
            new WaitHelpers(Driver).HardWait(3000);
            const string expectedBlogTitle = "Medical Staffing Agency Blog | Fusion Medical Staffing";
            const string expectedBlogUrl = "https://blog.fusionmedstaff.com/";
            Assert.IsTrue(Driver.IsUrlContains(expectedBlogUrl), $"{expectedBlogUrl} Blog url is not matched");
            var actualBlogTitle = Driver.GetPageTitle();
            Assert.AreEqual(expectedBlogTitle, actualBlogTitle, "Blog title is not matched");
        }

        //Blogs
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyBlogsWorkSuccessfully()
        {
            var homePage = new HomePagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            homePage.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on First Blog and verify title, header & text");
            const int blogFirst = 1;
            var expectedFirstBlogTitleAndHeaderText = homePage.GetBlogTitle(blogFirst);
            homePage.ClickOnNthBlogCard(blogFirst);
            var actualFirstBlogHeaderText = homePage.GetBlogHeaderText();
            var actualFirstBlogTitle = Driver.GetPageTitle();
            Assert.AreEqual(expectedFirstBlogTitleAndHeaderText, actualFirstBlogHeaderText, "First Blog header text is not matched");
            Assert.AreEqual(expectedFirstBlogTitleAndHeaderText.RemoveWhitespace(), actualFirstBlogTitle.RemoveWhitespace(), "First Blog title is not matched");

            Log.Info("Step 3: Click on second Blog and verify title & header text");
            Driver.Back();
            homePage.WaitUntilMpPageLoadingIndicatorInvisible();
            const int blogSecond = 2;
            var expectedSecondBlogTitleAndHeaderText = homePage.GetBlogTitle(blogSecond);
            homePage.ClickOnNthBlogCard(blogSecond);
            var actualSecondBlogTitle = Driver.GetPageTitle();
            var actualSecondBlogHeaderText = homePage.GetBlogHeaderText();
            Assert.AreEqual(expectedSecondBlogTitleAndHeaderText, actualSecondBlogHeaderText, "Second Blog header text is not matched");
            Assert.AreEqual(expectedSecondBlogTitleAndHeaderText.RemoveWhitespace(), actualSecondBlogTitle.RemoveWhitespace(), "second Blog title is not matched");

            Log.Info("Step 4: Click on third Blog and verify title & header text");
            Driver.Back();
            homePage.WaitUntilMpPageLoadingIndicatorInvisible();
            const int blogThird = 3;
            var expectedThirdBlogTitleAndHeaderText = homePage.GetBlogTitle(blogThird);
            homePage.ClickOnNthBlogCard(blogThird);
            var actualThirdBlogTitle = Driver.GetPageTitle();
            var actualThirdBlogHeaderText = homePage.GetBlogHeaderText();
            Assert.AreEqual(expectedThirdBlogTitleAndHeaderText, actualThirdBlogHeaderText, "Third Blog header text is not matched");
            Assert.AreEqual(expectedThirdBlogTitleAndHeaderText.RemoveWhitespace(), actualThirdBlogTitle.RemoveWhitespace(), "Third Blog title is not matched");
        }
    }
}
