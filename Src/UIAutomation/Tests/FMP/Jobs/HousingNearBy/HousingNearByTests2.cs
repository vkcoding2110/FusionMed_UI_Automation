using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.HousingNearBy
{
    [TestClass]
    [TestCategory("HousingNearBy"), TestCategory("FMP")]
    public class HousingNearByTests2 : FmpBaseTest
    {
        private readonly string HousingNearByJobsId = GetJobUrlByStatus(Env, "housingNearbyJobs");

        [TestMethod]
        [TestCategory("Smoke")]
        public void VerifyHousingNearbyPageOpenSuccessfully()
        {

            var housingNearBy = new HousingNearByPo(Driver);
            var housingNearByPo = new HousingNearByDetailPo(Driver);
            var jobDetailPo = new JobsDetailsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl} & verify Housing NearBy section is present");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();
            Assert.IsTrue(housingNearBy.IsNearbyHousingPresent(), "Housing NearBy section not present.");

            Log.Info("Step 2: Verify first housing NearBy card is present");
            const int firstIndexValue = 0;
            Assert.IsTrue(housingNearBy.IsNthNearbyHousingCardPresent(firstIndexValue), "The first 'Housing Nearby' card not present");

            Log.Info("Step 3: Click on 'Next' button & verify first card gets hide");
            housingNearBy.ClickOnhNearbyHousingCardNextButton();
            Assert.IsFalse(housingNearBy.IsNthNearbyHousingCardPresent(firstIndexValue), "The first 'housing Near by' card is present");

            Log.Info("Step 4: Click on 'Previous' button & verify first 'Housing Near by' card is present or not");
            const int indexValue = 1;
            housingNearBy.ClickOnhNearbyHousingCardNextButton();
            housingNearBy.ClickOnhNearbyHousingCardPreviousButton();
            Assert.IsTrue(housingNearBy.IsNthNearbyHousingCardPresent(indexValue), "The first job card is not present");

            Log.Info("Step 5: Click on second 'slick-dots'  &  verify first 'Housing Nearby'");
            Driver.RefreshPage();
            housingNearBy.WaitUntilFmpPageLoadingIndicatorInvisible();
            const int secondSlickDotsIndex = 2;
            housingNearBy.ClickOnNearbyHousingCardsClickDots(secondSlickDotsIndex);
            Assert.IsFalse(housingNearBy.IsNthNearbyHousingCardPresent(firstIndexValue), "The first 'Housing Nearby' card is present");

            Log.Info("Step 7: Click on first 'slick-dots' & verify first 'Housing Nearby'");
            const int firstSlickDotsIndex = 1;
            housingNearBy.ClickOnNearbyHousingCardsClickDots(firstSlickDotsIndex);
            Assert.IsTrue(housingNearBy.IsNthNearbyHousingCardPresent(firstIndexValue), "The first 'Housing Nearby' card is not present");

            Log.Info("Step 8: Get location from job page, click on 'View All housing Link' link and Verify location is correct");
            var location = jobDetailPo.GetJobLocation();
            var stateName = location.Split(",").Last().RemoveWhitespace();
            var fullStateName = GlobalConstants.StateListWithAliasName[stateName];
            var splitLocation = location.Split(",");
            var expectedLocation = splitLocation[0] + ", " + fullStateName;
            housingNearBy.ClickOnViewAllHousingLink();
            Assert.AreEqual(expectedLocation, housingNearByPo.GetHousingNearbyLocationText(), "Location is not matched");

            Log.Info("Step 9: Verify 'Housing Nearby' page open Successfully");
            var expectedUrl = FusionMarketPlaceUrl + "housing/?city=" + splitLocation[0].Replace(" ", "%20") + "&state=" + fullStateName.Replace(" ", "%20");
            Assert.AreEqual(expectedUrl, Driver.GetCurrentUrl(), "'Housing Nearby' page Url is not matched");

            const string expectedHeaderText = "Housing Nearby";
            Assert.AreEqual(expectedHeaderText, housingNearByPo.GetHousingNearbyPageHeaderText(), "Header text is not matched");

            Log.Info("Step 10: Click on 'Back to job listing' link and verify job details page opened Successfully");
            housingNearByPo.ClickOnBackToJobListingLink();
            Assert.IsTrue(jobDetailPo.IsJobTitlePresent(), "Job details page is not open");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_VerifyBathroomFilterWorkSuccessfully()
        {
            var jobsDetailsPo2 = new HousingNearByPo(Driver);
            var housingNearByPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select Pets allowed option from Filter drop-down");
            jobsDetailsPo2.ClickOnViewAllHousingLink();
            const string menuHeader = "Bathrooms";
            IList<string> typeOptionList = new[] { "2" };
            housingNearByPo.SelectFilterOption(menuHeader, typeOptionList);

            Log.Info("Step 3: Expand All cards, Get number of bathrooms available text & verify its correct");
            housingNearByPo.ExpandAllCardDetails();
            var noOfBathroomCardsList = housingNearByPo.GetBedRoomOrBathRoomTextFromAllCard();
            var expectedNumberOfBathroomText = typeOptionList.First() + menuHeader;
            foreach (var actualText in noOfBathroomCardsList)
            {
                Assert.AreEqual(expectedNumberOfBathroomText.RemoveWhitespace(), actualText.Split("/").Last().RemoveWhitespace(), $"{menuHeader} is not present in card");
            }

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_VerifyBedroomFilterWorkSuccessfully()
        {
            var jobsDetailsPo2 = new HousingNearByPo(Driver);
            var housingNearByPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select Pets allowed option from Filter drop-down");
            jobsDetailsPo2.ClickOnViewAllHousingLink();
            const string menuHeader = "Bedrooms";
            IList<string> typeOptionList = new[] { "1" };
            housingNearByPo.SelectFilterOption(menuHeader, typeOptionList);

            Log.Info("Step 3: Expand All cards, Get number of bedrooms available text & verify its correct");
            housingNearByPo.ExpandAllCardDetails();
            var noOfBedroomsCardsList = housingNearByPo.GetBedRoomOrBathRoomTextFromAllCard();
            var expectedNumberOfBedroomText = typeOptionList.First() + menuHeader.Replace("s", "");
            foreach (var actualText in noOfBedroomsCardsList)
            {
                Assert.AreEqual(expectedNumberOfBedroomText.RemoveWhitespace(), actualText.Split("/").First().RemoveWhitespace(), $" {menuHeader} is not present in card");
            }

        }
        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady")]
        public void HousingNearBy_SortBy_VerifyHighestPriceFilterWorkSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select 'Price - Highest First' option from sort by drop-down");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string priceHighestSortByOption = "Price - Highest First";
            housingNearByDetailPo.SelectSortByFilterOption(priceHighestSortByOption);

            Log.Info("Step 3: Get price from all cards & verify its correct");
            var priceList = housingNearByDetailPo.GetPriceListFromAllCard();
            IList<double> expectedPriceList = priceList.Select(double.Parse).ToList();
            IList<double> actualPriceList = expectedPriceList.OrderByDescending(s => s).ToList();
            CollectionAssert.AreEqual(expectedPriceList.ToList(), actualPriceList.ToList(), $"{priceHighestSortByOption} filter is not matched");

        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_SortBy_VerifyLowestPriceFilterWorkSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select 'Price - Lowest First' option from sort by drop-down");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string priceLowestSortByOption = "Price - Lowest First";
            housingNearByDetailPo.SelectSortByFilterOption(priceLowestSortByOption);

            Log.Info("Step 3: Get price from card according to sort option & verify its correct");
            var priceFromCard = housingNearByDetailPo.GetPriceListFromAllCard();
            IList<double> expectedPriceList = priceFromCard.Select(double.Parse).ToList();
            IList<double> actualPriceList = expectedPriceList.OrderBy(s => s).ToList();
            CollectionAssert.AreEqual(expectedPriceList.ToList(), actualPriceList.ToList(), $" {priceLowestSortByOption} filter is not matched");
        }
    }
}
