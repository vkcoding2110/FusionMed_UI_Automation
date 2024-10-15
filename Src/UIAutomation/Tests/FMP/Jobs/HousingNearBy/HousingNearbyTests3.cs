using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.HousingNearBy
{

    [TestClass]
    [TestCategory("HousingNearBy"), TestCategory("FMP")]
    public class HousingNearbyTests3 : FmpBaseTest
    {
        private readonly string HousingNearByJobsId = GetJobUrlByStatus(Env, "housingNearbyJobs");

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("Smoke")]
        public void HousingNearBy_SortBy_VerifyMinTermHighestFirstFilterWorkSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select 'Min. Term - Highest First' option from sort by drop-down");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string minTermSortByOption = "Min. Term - Highest First";
            housingNearByDetailPo.SelectSortByFilterOption(minTermSortByOption);

            Log.Info("Step 3: Expand all card, Get Minimum Stay from card & verify its correct");
            housingNearByDetailPo.ExpandAllCardDetails();
            var minTermFromCard = housingNearByDetailPo.GetMinimumStayTextFromAllCard();
            IList<int> expectedMinTermList = minTermFromCard.Select(int.Parse).ToList();
            IList<int> actualMinTermList = expectedMinTermList.OrderByDescending(s => s).ToList();
            CollectionAssert.AreEqual(expectedMinTermList.ToList(), actualMinTermList.ToList(), $"{minTermSortByOption} filter is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_SortBy_VerifyMinTermLowestFirstFilterWorkSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select 'Min. Term - Lowest First' option from sort by drop-down");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string minTermSortByOption = "Min. Term - Lowest First";
            housingNearByDetailPo.SelectSortByFilterOption(minTermSortByOption);

            Log.Info("Step 3: Expand all card , Get Minimum Stay from card according to sort option & verify its correct");
            housingNearByDetailPo.ExpandAllCardDetails();
            var minTermFromCard = housingNearByDetailPo.GetMinimumStayTextFromAllCard();
            IList<int> expectedMinTermList = minTermFromCard.Select(int.Parse).ToList();
            IList<int> actualPriceList = expectedMinTermList.OrderBy(s => s).ToList();
            CollectionAssert.AreEqual(expectedMinTermList.ToList(), actualPriceList.ToList(), $"{minTermSortByOption} filter is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("Smoke"), TestCategory("IosNotReady")]
        public void VerifyNearbyHousingMoreDetailsExpandAndCollapseSuccessfully()
        {
            var housingNearBy = new HousingNearByPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl} & verify Housing NearBy section is present");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearBy.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Expand First card and verify 'Book on Furnished Finder' Url is correct");
            var expectedUrl = housingNearBy.ClickOnBookOnFurnishedFinderButtonAndGetUrl();
            housingNearBy.ClickOnMoreDetailsButton();
            var actualUrl = housingNearBy.ClickOnCardBookOnFurnishedFinderLinkAndGetUrl();
            Assert.IsTrue(expectedUrl.Contains(actualUrl), "'Book on Furnished Finder' Url is not matched");
        }
        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("Smoke")]
        public void HousingNearBy_SortBy_VerifyDateAvailableSoonerFirstWorkSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & Date Available - Soonest First option from SortBy dropdown & verify Date is showing correctly");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string dateAvailableSoonerFirst = "Date Available - Soonest First";
            housingNearByDetailPo.SelectSortByFilterOption(dateAvailableSoonerFirst);
            housingNearByDetailPo.ExpandAllCardDetails();
            var expectedAvailableDateList = housingNearByDetailPo.GetAvailableDateTextFromCard();
            var actualAvailableDateList = expectedAvailableDateList.OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedAvailableDateList.ToList(), actualAvailableDateList.ToList(), "'Date Available - Sooner First' Sort By Filter is not working");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_SortBy_VerifyDateAvailableSoonerLastWorkSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & Date Available - Soonest Last option from SortBy dropdown & verify Date is showing correctly");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string dateAvailableSoonerFirst = "Date Available - Soonest Last";
            housingNearByDetailPo.SelectSortByFilterOption(dateAvailableSoonerFirst);
            housingNearByDetailPo.ExpandAllCardDetails();
            var expectedAvailableDateList = housingNearByDetailPo.GetAvailableDateTextFromCard();
            var actualAvailableDateList = expectedAvailableDateList.OrderByDescending(x => x).ToList();
            CollectionAssert.AreEqual(expectedAvailableDateList.ToList(), actualAvailableDateList.ToList(), "'Date Available - Sooner Last' Sort By Filter is not working");
        }

    }
}