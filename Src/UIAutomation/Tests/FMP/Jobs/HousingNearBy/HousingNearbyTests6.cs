using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.HousingNearBy
{
    [TestClass]
    [TestCategory("HousingNearBy"), TestCategory("FMP")]
    public class HousingNearbyTests6 : FmpBaseTest
    {
        private readonly string HousingNearByJobsId = GetJobUrlByStatus(Env, "housingNearbyJobs");

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_SelectingTypeFilterOptionsAndMinTermHighestFirstOptionFromSortByWorksSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info($"Step 2: Click on 'View all housing' link & select Type option from Filter drop-down");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string menuHeader = "Type";
            IList<string> multipleTypeOptionList = new[] { "Apartment", "Condo" };
            housingNearByDetailPo.SelectFilterOption(menuHeader, multipleTypeOptionList);

            Log.Info("Step 3: Select 'Min. Term - Highest First' option from sort by drop-down");
            const string minTermSortByOption = "Min. Term - Highest First";
            housingNearByDetailPo.SelectSortByFilterOption(minTermSortByOption);

            Log.Info("Step 4: Expand All cards, Get card details & verify Selected filter and Sort by option displayed correctly");
            var multipleTypeTextCardsList = housingNearByDetailPo.GetTypeTextFromAllCard();
            foreach (var actualType in multipleTypeTextCardsList)
            {
                Assert.IsTrue(multipleTypeOptionList.Contains(actualType), "Type options text is not correct");
            }

            housingNearByDetailPo.ExpandAllCardDetails();
            var minTermFromCard = housingNearByDetailPo.GetMinimumStayTextFromAllCard();
            IList<int> expectedMinTermList = minTermFromCard.Select(int.Parse).ToList();
            IList<int> actualMinTermList = expectedMinTermList.OrderByDescending(s => s).ToList();
            CollectionAssert.AreEqual(expectedMinTermList.ToList(), actualMinTermList.ToList(), $"{minTermSortByOption} filter is not matched");
        }
    }
}
