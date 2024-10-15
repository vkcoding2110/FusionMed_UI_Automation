using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.HousingNearBy
{
    [TestClass]
    [TestCategory("HousingNearBy"), TestCategory("FMP")]
    public class HousingNearbyTests4 : FmpBaseTest
    {
        private readonly string HousingNearByJobsId = GetJobUrlByStatus(Env, "housingNearbyJobs");

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("Smoke")]
        public void HousingNearBy_VerifySelectingMultipleFilters1WorkSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select Pets 'Allowed' option from Filter drop-down");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string menuHeaderPets = "Pets";
            IList<string> petsOptionList = new[] { "Allowed" };
            housingNearByDetailPo.SelectFilterOption(menuHeaderPets, petsOptionList);

            Log.Info("Step 3: Select Furnished 'Included' option from Filter drop-down");
            const string menuHeaderFurnished = "Furnished";
            IList<string> utilitiesOptionList = new[] { "Included" };
            housingNearByDetailPo.SelectFilterOption(menuHeaderFurnished, utilitiesOptionList);

            Log.Info("Step 4: Select Bedrooms '2 bedroom' option from Filter drop-down");
            const string menuHeaderBedrooms = "Bedrooms";
            IList<string> bedroomsOptionList = new[] { "2" };
            housingNearByDetailPo.SelectFilterOption(menuHeaderBedrooms, bedroomsOptionList);

            Log.Info("Step 5: Select Type 'House' option from Filter drop-down");
            const string menuHeaderType = "Type";
            IList<string> typeOptionList = new[] { "House" };
            housingNearByDetailPo.SelectFilterOption(menuHeaderType, typeOptionList);

            Log.Info("Step 6: Expand All cards, Get card details");
            var furnishedTextCardsList = housingNearByDetailPo.GetFurnishedTextFromAllCard();
            var typeTextCardsList = housingNearByDetailPo.GetTypeTextFromAllCard();
            housingNearByDetailPo.ExpandAllCardDetails();
            var petAllowedCardsList = housingNearByDetailPo.GetPetAllowedNotAllowedTextFromAllCard();
            var noOfBedroomsCardsList = housingNearByDetailPo.GetBedRoomOrBathRoomTextFromAllCard();

            Log.Info("Step 7: Verify selected filters are displayed correctly on cards");
            var petAllowedText = menuHeaderPets + petsOptionList.First();
            foreach (var actualText in petAllowedCardsList)
            {
                Assert.AreEqual(petAllowedText.RemoveWhitespace(), actualText.RemoveWhitespace(), "Pets Allowed/Not Allowed text is not correct");
            }

            foreach (var actualText in furnishedTextCardsList)
            {
                Assert.AreEqual(menuHeaderFurnished, actualText, "Furnished/Not Furnished text is not Correct");
            }

            var numberOfBedroomText = bedroomsOptionList.First();
            foreach (var actualText in noOfBedroomsCardsList)
            {
                Assert.AreEqual(numberOfBedroomText, actualText.Split(" /").First().Replace(" Bedroom", "").Replace("s", ""), $" {menuHeaderBedrooms} is not present in card");
            }

            foreach (var actualType in typeTextCardsList)
            {
                Assert.IsTrue(typeOptionList.Contains(actualType), "Type options text is not correct");
            }
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("Smoke")]
        public void HousingNearBy_VerifySelectingMultipleFilters2WorkSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select Utilities option from Filter drop-down");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string menuHeaderUtilities = "Utilities";
            IList<string> utilitiesOptionList = new[] { "Included" };
            housingNearByDetailPo.SelectFilterOption(menuHeaderUtilities, utilitiesOptionList);

            Log.Info("Step 3: Select Bathrooms '2' option from Filter drop-down");
            const string menuHeaderBathrooms = "Bathrooms";
            IList<string> bathroomOptionList = new[] { "2" };
            housingNearByDetailPo.SelectFilterOption(menuHeaderBathrooms, bathroomOptionList);

            Log.Info("Step 4: Select single Min.Stay option from Filter drop-down");
            const string menuHeaderMinTerm = "Min. Term";
            IList<string> minTermOptionList = new[] { "30" };
            housingNearByDetailPo.SelectFilterOption(menuHeaderMinTerm, minTermOptionList);

            Log.Info("Step 5: Expand All cards, Get card details");
            var utilitiesTextCardsList = housingNearByDetailPo.GetUtilitiesTextFromAllCard();
            housingNearByDetailPo.ExpandAllCardDetails();
            var noOfBathroomCardsList = housingNearByDetailPo.GetBedRoomOrBathRoomTextFromAllCard();
            var minStayTextCardsList = housingNearByDetailPo.GetMinimumStayTextFromAllCard();

            Log.Info("Step 6: Verify selected filters are displayed correctly on cards");
            var utilitiesText = menuHeaderUtilities + utilitiesOptionList.First();
            foreach (var actualText in utilitiesTextCardsList)
            {
                Assert.AreEqual(utilitiesText.RemoveWhitespace(), actualText.RemoveWhitespace(), "Utilities Include/Not Include text is not Correct");
            }

            var numberOfBathroomText = bathroomOptionList.First() + menuHeaderBathrooms;
            foreach (var actualText in noOfBathroomCardsList)
            {
                Assert.AreEqual(numberOfBathroomText.RemoveWhitespace(), actualText.Split("/").Last().RemoveWhitespace(), $"{menuHeaderBathrooms} is not present in card");
            }

            foreach (var actualText in minStayTextCardsList)
            {
                Assert.AreEqual(minTermOptionList.First(), actualText, "Min Stay text is not correct");
            }
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("Smoke")]
        public void HousingNearBy_SelectingPetOptionFromFilterAndDateAvailableSoonerFirstOptionFromSortByWorksSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select Pets 'Allowed' option from Filter drop-down");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string menuHeaderPets = "Pets";
            IList<string> petsOptionList = new[] { "Not Allowed" };
            housingNearByDetailPo.SelectFilterOption(menuHeaderPets, petsOptionList);

            Log.Info("Step 3: Select Date Available - Soonest First option from SortBy dropdown");
            const string dateAvailableSoonerFirst = "Date Available - Soonest First";
            housingNearByDetailPo.SelectSortByFilterOption(dateAvailableSoonerFirst);

            Log.Info("Step 4: Expand All cards, Get card details & verify Selected filter and Sort by option displayed correctly");
            housingNearByDetailPo.ExpandAllCardDetails();
            var petAllowedText = menuHeaderPets + petsOptionList.First();
            var petAllowedCardsList = housingNearByDetailPo.GetPetAllowedNotAllowedTextFromAllCard();
            foreach (var actualText in petAllowedCardsList)
            {
                Assert.AreEqual(petAllowedText.RemoveWhitespace(), actualText.RemoveWhitespace(), "Pets Allowed/Not Allowed text is not correct");
            }

            var availableDateList = housingNearByDetailPo.GetAvailableDateTextFromCard();
            var actualAvailableDateList = availableDateList.OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(availableDateList.ToList(), actualAvailableDateList.ToList(), "'Date Available - Sooner First' Sort By Filter is not working");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_SelectingFurnishedOptionFromFilterAndDateAvailableSoonerLastOptionFromSortByWorksSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & Select Furnished 'Included' option from Filter drop-down");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string menuHeaderFurnished = "Furnished";
            IList<string> utilitiesOptionList = new[] { "Included" };
            housingNearByDetailPo.SelectFilterOption(menuHeaderFurnished, utilitiesOptionList);

            Log.Info("Step 3: Select Date Available - Soonest Last option from SortBy dropdown");
            const string dateAvailableSoonerFirst = "Date Available - Soonest Last";
            housingNearByDetailPo.SelectSortByFilterOption(dateAvailableSoonerFirst);

            Log.Info("Step 4: Expand All cards, Get card details & verify Selected filter and Sort by option displayed correctly");
            var furnishedTextCardsList = housingNearByDetailPo.GetFurnishedTextFromAllCard();
            housingNearByDetailPo.ExpandAllCardDetails();
            foreach (var actualText in furnishedTextCardsList)
            {
                Assert.AreEqual(menuHeaderFurnished, actualText, "Furnished/Not Furnished text is not Correct");
            }

            var availableDateList = housingNearByDetailPo.GetAvailableDateTextFromCard();
            var actualAvailableDateList = availableDateList.OrderByDescending(x => x).ToList();
            CollectionAssert.AreEqual(availableDateList.ToList(), actualAvailableDateList.ToList(), "'Date Available - Sooner Last' Sort By Filter is not working");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_SelectingMultipleBedroomsFilterOptionsAndPriceHighestFirstOptionFromSortByWorksSuccessfully()
        {
            var housingNearByPo = new HousingNearByPo(Driver);
            var housingNearByDetailPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on 'View all housing' link & select multiple bedrooms option from Filter drop-down");
            housingNearByPo.ClickOnViewAllHousingLink();
            const string menuHeader = "Bedrooms";
            IList<string> bedroomsOptionList = new[] { "1", "2" };
            housingNearByDetailPo.SelectFilterOption(menuHeader, bedroomsOptionList);

            Log.Info("Step 3: Select 'Price - Highest First' option from sort by drop-down");
            const string priceHighestSortByOption = "Price - Highest First";
            housingNearByDetailPo.SelectSortByFilterOption(priceHighestSortByOption);

            Log.Info("Step 4: Expand All cards, Get card details & verify Selected filter and Sort by option displayed correctly");
            var priceListFromAllCard = housingNearByDetailPo.GetPriceListFromAllCard();
            IList<double> priceList = priceListFromAllCard.Select(double.Parse).ToList();
            IList<double> actualPriceList = priceList.OrderByDescending(s => s).ToList();
            CollectionAssert.AreEqual(priceList.ToList(), actualPriceList.ToList(), $"{priceHighestSortByOption} filter is not matched");

            housingNearByDetailPo.ExpandAllCardDetails();
            var multipleBedroomsCardsList = housingNearByDetailPo.GetBedRoomOrBathRoomTextFromAllCard();
            foreach (var actualBedroom in multipleBedroomsCardsList)
            {
                var bedrooms = actualBedroom.Split(" /").First().Replace(" Bedroom", "").Replace("s", "");
                Assert.IsTrue(bedroomsOptionList.Contains(bedrooms), $"{actualBedroom} filter is not matched");
            }
        }
    }
}
