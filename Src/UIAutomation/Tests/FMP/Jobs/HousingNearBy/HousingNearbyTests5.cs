using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.HousingNearBy
{
    [TestClass]
    [TestCategory("HousingNearBy"), TestCategory("FMP")]
    public class HousingNearbyTests5 : FmpBaseTest
    {
        private readonly string HousingNearByJobsId = GetJobUrlByStatus(Env, "housingNearbyJobs");

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_VerifyFilterCancelButtonWorkSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link, Get Type text from all cards, Select 'Type' filter option & get 'Type' text details");
            housingNearByPo.ClickOnViewAllHousingLink();
            var typeTextCardsList = housingNearByDetailPo.GetTypeTextFromAllCard();
            const string menuHeader = "Type";
            IList<string> typeOptionList = new[] { "Condo" };
            housingNearByDetailPo.SelectFilterOption(menuHeader, typeOptionList);
            var actualTypeTextCardsList = housingNearByDetailPo.GetTypeTextFromAllCard();
            Assert.AreNotEqual(typeTextCardsList, actualTypeTextCardsList, "The 'Type' filter options are same");

            Log.Info("Step 3: Click on filter 'Cancel' button & verify cards detail gets reset");
            housingNearByDetailPo.ClickOnFilterCancelButton();
            var actualTypeTextCardsListAfterCancelButton = housingNearByDetailPo.GetTypeTextFromAllCard();
            CollectionAssert.AreEqual(typeTextCardsList.ToList(), actualTypeTextCardsListAfterCancelButton.ToList(), "The 'Type' texts are not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_SortBy_VerifyDefaultOptionWorksInSortByFilter()
        {
            var housingNearBy = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearBy.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select 'Min. Term - Highest First' allowed option from sort by drop-down");
            housingNearBy.ClickOnViewAllHousingLink();
            housingNearByDetailPo.ExpandAllCardDetails();
            var priceFromCardBeforeSelectFilter = housingNearByDetailPo.GetMinimumStayTextFromAllCard();
            housingNearByDetailPo.ExpandAllCardDetails();
            const string sortByOption = "Min. Term - Highest First";
            housingNearByDetailPo.SelectSortByFilterOption(sortByOption);

            Log.Info("Step 3: Expand all card , Get Minimum Stay from card according to sort option & verify cart");
            housingNearByDetailPo.ExpandAllCardDetails();
            var priceFromCardAfterSelectFilter = housingNearByDetailPo.GetMinimumStayTextFromAllCard();
            CollectionAssert.AreNotEqual(priceFromCardBeforeSelectFilter.ToList(), priceFromCardAfterSelectFilter.ToList(), "Type list is matched");

            Log.Info($"Step 4: Click on 'Default' and verify sort by drop-down selected text");
            housingNearByDetailPo.ExpandAllCardDetails();
            const string defaultSortByOption = "Default";
            housingNearByDetailPo.SelectSortByFilterOption(defaultSortByOption);
            var sortByFilterText = housingNearByDetailPo.SelectSortByFilterOption();
            Assert.AreEqual(defaultSortByOption, sortByFilterText, "Sort by option text is not matched");

            Log.Info($"Step 4:Expand all card, and verify allowed option from sort by drop-down");
            housingNearByDetailPo.ExpandAllCardDetails();
            var actualPriceFromCard = housingNearByDetailPo.GetMinimumStayTextFromAllCard();
            CollectionAssert.AreEqual(priceFromCardBeforeSelectFilter.ToList(), actualPriceFromCard.ToList(), "Default list is not  matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_VerifyBathroom_Utilities_TypeFilterAndMinTermLowestSortByFilterWorkSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select Bathroom , Min term , Type options from Filter drop-down");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string bathRoomMenuHeader = "Bathrooms";
            IList<string> bathRoomOptionList = new[] { "1" };
            housingNearByDetailPo.SelectFilterOption(bathRoomMenuHeader, bathRoomOptionList);
            const string utilitiesMenuHeader = "Utilities";
            IList<string> utilitiesOptionList = new[] { "Included" };
            housingNearByDetailPo.SelectFilterOption(utilitiesMenuHeader, utilitiesOptionList);
            const string typeMenuHeader = "Type";
            IList<string> typeOptionList = new[] { "Apartment" };
            housingNearByDetailPo.SelectFilterOption(typeMenuHeader, typeOptionList);

            Log.Info("Step 3: Select 'Min. Term - Lowest First' option from sort by drop-down");
            const string minTermSortByOption = "Min. Term - Lowest First";
            housingNearByDetailPo.SelectSortByFilterOption(minTermSortByOption);

            Log.Info("Step 4: Expand All cards and Get Bathrooms , Utilities , Type option text & Get Minimum Stay from card according to sort option verify its correct");
            var utilitiesTextCardsList = housingNearByDetailPo.GetUtilitiesTextFromAllCard();
            var utilitiesText = utilitiesMenuHeader + utilitiesOptionList.First();
            housingNearByDetailPo.ExpandAllCardDetails();
            var noOfBathroomCardsList = housingNearByDetailPo.GetBedRoomOrBathRoomTextFromAllCard();
            var numberOfBathroomText = bathRoomOptionList.First() + bathRoomMenuHeader.Replace("s", "");
            var oneTypeTextCardsList = housingNearByDetailPo.GetTypeTextFromAllCard();
            var minTermFromCard = housingNearByDetailPo.GetMinimumStayTextFromAllCard();
            IList<int> minTermList = minTermFromCard.Select(int.Parse).ToList();
            IList<int> actualPriceList = minTermList.OrderBy(s => s).ToList();
            foreach (var actualText in noOfBathroomCardsList)
            {
                Assert.AreEqual(numberOfBathroomText.RemoveWhitespace(), actualText.Split("/").Last().RemoveWhitespace(), $"{bathRoomMenuHeader} is not present in card");
            }
            foreach (var actualText in utilitiesTextCardsList)
            {
                Assert.AreEqual(utilitiesText.RemoveWhitespace(), actualText.RemoveWhitespace(), $" {utilitiesMenuHeader} Utilities text is not Correct");
            }
            foreach (var actualText in oneTypeTextCardsList)
            {
                Assert.AreEqual(typeOptionList.First(), actualText, $"{typeMenuHeader} Type text is not correct");
            }
            CollectionAssert.AreEqual(minTermList.ToList(), actualPriceList.ToList(), $"{minTermSortByOption} filter is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_Filter_VerifyFilterOptionSelectAndDeselectWorkSuccessfully()
        {
            var housingNearBy = new HousingNearByPo(Driver);
            var housingNearByPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select Type option from Filter drop-down");
            const string menuHeader = "Type";
            IList<string> typeOptionList = new[] { "Apartment" };
            housingNearBy.ClickOnViewAllHousingLink();
            var typeTextCardsListBeforeSelectFilter = housingNearByPo.GetTypeTextFromAllCard();
            housingNearByPo.SelectFilterOption(menuHeader, typeOptionList);

            Log.Info("Step 3: Get Type text, verify type list & again open Type filter and verify filter is still selected");
            var typeTextCardsListAfterSelectFilter = housingNearByPo.GetTypeTextFromAllCard();
            CollectionAssert.AreNotEqual(typeTextCardsListBeforeSelectFilter.ToList(), typeTextCardsListAfterSelectFilter.ToList(), "Type list is matched");
            housingNearByPo.ClickOnFilterDropDown();
            foreach (var option in typeOptionList)
            {
                Assert.IsTrue(housingNearByPo.IsFilterOptionSelected(menuHeader, option), "Checkbox is not selected");
            }

            Log.Info("Step 4: Deselect 'Type' option from 'filter' and verify 'type' card list is matched");
            housingNearByPo.CloseFilterDropdown();
            housingNearByPo.SelectFilterOption(menuHeader, typeOptionList);
            var actualTypeTextCardsList = housingNearByPo.GetTypeTextFromAllCard();
            CollectionAssert.AreEqual(typeTextCardsListBeforeSelectFilter.ToList(), actualTypeTextCardsList.ToList(), "Type list is not  matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_SelectingUtilitiesFilterOptionsAndPriceLowestFirstOptionFromSortByWorksSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info($"Step 2: Click on 'View all housing' link & select Utilities option from Filter drop-down");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string menuHeader = "Utilities";
            IList<string> optionList = new[] { "Included" };
            housingNearByDetailPo.SelectFilterOption(menuHeader, optionList);

            Log.Info("Step 3: Select 'Price - Lowest First' option from sort by drop-down");
            const string priceLowestSortByOption = "Price - Lowest First";
            housingNearByDetailPo.SelectSortByFilterOption(priceLowestSortByOption);

            Log.Info("Step 4: Expand All cards, Get card details & verify Selected filter and Sort by option displayed correctly");
            var utilitiesTextCardsList = housingNearByDetailPo.GetUtilitiesTextFromAllCard();
            var expectedUtilitiesText = menuHeader + optionList.First();
            foreach (var actualText in utilitiesTextCardsList)
            {
                Assert.AreEqual(expectedUtilitiesText.RemoveWhitespace(), actualText.RemoveWhitespace(), "Utilities Include/Not Include text is not Correct");
            }

            var priceFromCard = housingNearByDetailPo.GetPriceListFromAllCard();
            IList<double> expectedPriceList = priceFromCard.Select(double.Parse).ToList();
            IList<double> actualPriceList = expectedPriceList.OrderBy(s => s).ToList();
            CollectionAssert.AreEqual(expectedPriceList.ToList(), actualPriceList.ToList(), $" {priceLowestSortByOption} filter is not matched");
        }
    }
}
