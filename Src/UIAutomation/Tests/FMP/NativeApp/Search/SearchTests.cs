using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Account.NativeApp;
using UIAutomation.PageObjects.FMP.Home.NativeApp;
using UIAutomation.PageObjects.FMP.NativeApp.Search;

namespace UIAutomation.Tests.FMP.NativeApp.Search
{
    [TestClass]
    [TestCategory("Search"), TestCategory("NativeAppAndroid")]
    public class SearchTests : FmpBaseTest
    {
        [TestMethod]
        public void VerifyThatSearchJobsWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var searchJobs = new SearchPo(Driver);
            var homepagePo = new HomePagePo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homepagePo.OpenHomePage();
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);

            Log.Info("Step 2: Click on search bar and Verify Search page is open");
            searchJobs.ClickOnSearchBar();
            Assert.IsTrue(searchJobs.IsSearchHeaderTextPresent(), "Search page is not open");

            Log.Info("Step 3: Enter your job ,select from the dropdown and Verify selected job is displayed");
            const string searchJobName = "CNA";
            searchJobs.EnterJobsToSearchBoxAndHitEnter(searchJobName);
            searchJobs.SelectJobFromSearchResult(searchJobName);
            var jobTitleList = searchJobs.GetJobTitle(searchJobName);
            foreach (var actualJobTitle in jobTitleList)
            {
                Assert.IsTrue(actualJobTitle.Contains(searchJobName), "Job title is not matched");
            }
        }

        [TestMethod]
        public void VerifyThatSearchBarCloseIconWorkSuccessfully()
        {
            var searchJobs = new SearchPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var homepagePo = new HomePagePo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homepagePo.OpenHomePage();
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            
            Log.Info("Step 2: Click on 'Search Bar' icon , Click on Close icon and verify search page is closed");
            searchJobs.ClickOnSearchBar();
            searchJobs.ClickOnCloseIcon();
            Assert.IsFalse(searchJobs.IsSearchHeaderTextPresent(), "Search page is open");
        }
    }
}
