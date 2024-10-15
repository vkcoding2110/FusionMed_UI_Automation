using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.PageObjects.FMP.BrowseAll.Agencies;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Agencies.Recruiters
{
    [TestClass]
    [TestCategory("Recruiter"), TestCategory("FMP")]
    public class AgencySortAndFiltersTests : FmpBaseTest
    {
        private const string ExploreMenuAgenciesButton = "Agencies";
        private const string AgencyName = "Fusion Medical Staffing";
        private const string UpdateAgencyName = "Lead Healthstaff";

        [TestMethod]
        [TestCategory("MobileReady")]
        public void SortAndFilter_VerifyAgencyFilterOptionsWorksSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var exploreMenu = new ExploreMenuPo(Driver);
            var agency = new AgencyDetailPo(Driver);
            var recruitersPo = new RecruiterListingPo(Driver);
            var searchJobs = new PageObjects.FMP.Jobs.SearchPo(Driver);
            var sortAndFilter = new SortAndFilterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Browse All' button, click on 'Agencies' explore menu, Click on 'Fusion Medical Staffing' agencies menu");
            fmpHeader.ClickOnBrowseAllButton();
            exploreMenu.ClickOnExploreMenuItemsButton(ExploreMenuAgenciesButton);
            exploreMenu.ClickOnAgencyMenuItem(AgencyName);

            Log.Info("Step 3: Click on 'View all recruiters' link, Click on 'Sort & Filter' button & verify 'Sort & Filter' popup gets open");
            agency.ClickOnViewAllRecruiterLink();
            searchJobs.ClickOnSortAndFilterButton();
            Assert.IsTrue(sortAndFilter.IsSortAndFilterPopupPresent(), "Sort & Filter popup is not opened");

            Log.Info("Step 4: Click on 'Agency' filter, update agency & verify the filter tag label, results count & agency name on all recruiters card");
            const string filterOption = "Agency";
            sortAndFilter.ClickOnSortAndFilterOption(filterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(AgencyName);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(UpdateAgencyName);
            sortAndFilter.ClickOnShowAllResultsButton();
            var expectedAgencyName = recruitersPo.GetRecruiterFilterTagName();
            var expectedAgencyCount = recruitersPo.GetSearchResultsCount();
            Assert.AreEqual(expectedAgencyName, UpdateAgencyName, "Recruiter agency name is not match");
            recruitersPo.WaitUntilRecruiterCardVisible();
            var recruiterList = recruitersPo.GetAllAgencyNamesFromRecruiterCards();
            Assert.AreEqual(Convert.ToInt32(expectedAgencyCount), recruiterList.Count, "Recruiter list count is not match");
            for (var i = 1; i < recruiterList.Count; i++)
            {
                Assert.AreEqual(UpdateAgencyName, recruiterList[i], $"Recruiter agency name doesn't have {recruiterList[i]}");
            }

            Log.Info("Step 5: Click on 'Sort & Filter', click on 'Agency' filter, update agency & verify the agency name on all recruiters card");
            searchJobs.ClickOnSortAndFilterButton();
            sortAndFilter.ClickOnSortAndFilterOption(filterOption);
            sortAndFilter.ClickOnSortAndFilterSubMenuOption(AgencyName);
            sortAndFilter.ClickOnShowAllResultsButton();
            recruitersPo.WaitUntilRecruiterCardVisible();
            var expectedRecruiterList = recruitersPo.GetAllAgencyNamesFromRecruiterCards();
            for (var i = 1; i < expectedRecruiterList.Count; i++)
            {
                Assert.IsTrue(expectedRecruiterList[i].Contains(AgencyName) || expectedRecruiterList[i].Contains(UpdateAgencyName), $"Recruiter agency name doesn't have {expectedRecruiterList[i]}");
            }
        }
    }
}
