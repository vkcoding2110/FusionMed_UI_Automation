using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMP.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Jobs.HousingNearBy
{
    [TestClass]
    [TestCategory("HousingNearBy"), TestCategory("FMP")]
    public class HousingNearByTests1 : FmpBaseTest
    {
        private readonly string HousingNearByJobsId = GetJobUrlByStatus(Env, "housingNearbyJobs");

        [TestMethod]
        public void HousingNearBy_VerifyPetsFilterWorkSuccessfully()
        {
            var housingNearBy = new HousingNearByPo(Driver);
            var housingNearByPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info($"Step 2: Click on 'View all housing' link & select Pets allowed option from Filter drop-down");
            housingNearBy.ClickOnViewAllHousingLink();
            const string menuHeader = "Pets";
            IList<string> optionList = new[] { "Allowed" };
            housingNearByPo.SelectFilterOption(menuHeader, optionList);

            Log.Info($"Step 3: Expand All cards, Get Pets Allowed/Not Allowed text & verify its correct");
            housingNearByPo.ExpandAllCardDetails();
            var petAllowedCardsList = housingNearByPo.GetPetAllowedNotAllowedTextFromAllCard();
            var expectedPetAllowedText = menuHeader + optionList.First();
            foreach (var actualText in petAllowedCardsList)
            {
                Assert.AreEqual(expectedPetAllowedText.RemoveWhitespace(), actualText.RemoveWhitespace(), "Pets Allowed/Not Allowed text is not correct");
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_VerifyUtilitiesFilterWorkSuccessfully()
        {
            var housingNearBy = new HousingNearByPo(Driver);
            var housingNearByPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info($"Step 2: Click on 'View all housing' link & select Utilities option from Filter drop-down");
            const string menuHeader = "Utilities";
            IList<string> optionList = new[] { "Included" };
            housingNearBy.ClickOnViewAllHousingLink();
            housingNearByPo.SelectFilterOption(menuHeader, optionList);

            Log.Info($"Step 3: Get Utilities Include/Not Include text & verify its correct");
            var utilitiesTextCardsList = housingNearByPo.GetUtilitiesTextFromAllCard();
            var expectedUtilitiesText = menuHeader + optionList.First();
            foreach (var actualText in utilitiesTextCardsList)
            {
                Assert.AreEqual(expectedUtilitiesText.RemoveWhitespace(), actualText.RemoveWhitespace(), "Utilities Include/Not Include text is not Correct");
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_VerifyFurnishedFilterWorkSuccessfully()
        {
            var housingNearBy = new HousingNearByPo(Driver);
            var housingNearByPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info($"Step 2: Click on 'View all housing' link & select Furnished option from Filter drop-down");
            const string menuHeader = "Furnished";
            IList<string> optionList = new[] { "Included" };
            housingNearBy.ClickOnViewAllHousingLink();
            housingNearByPo.SelectFilterOption(menuHeader, optionList);

            Log.Info($"Step 3: Get Furnished/Not Furnished text & verify its correct");
            var furnishedTextCardsList = housingNearByPo.GetFurnishedTextFromAllCard();
            foreach (var actualText in furnishedTextCardsList)
            {
                Assert.AreEqual(menuHeader, actualText, "Furnished/Not Furnished text is not Correct");
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_VerifyMinTermFilterWorkSuccessfully()
        {
            var housingNearBy = new HousingNearByPo(Driver);
            var housingNearByPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearBy.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info($"Step 2: Click on 'View all housing' link & select single Min.Stay option from Filter drop-down");
            const string menuHeader = "Min. Term";
            IList<string> optionList = new[] { "30" };
            housingNearBy.ClickOnViewAllHousingLink();
            housingNearByPo.SelectFilterOption(menuHeader, optionList);

            Log.Info($"Step 3: Get Min. stay text & verify its correct");
            housingNearByPo.ExpandAllCardDetails();
            var minStayTextCardsList = housingNearByPo.GetMinimumStayTextFromAllCard();
            foreach (var actualText in minStayTextCardsList)
            {
                Assert.AreEqual(optionList.First(), actualText, "Min Stay text is not Correct");
            }

            Log.Info($"Step 4: select Multiple Min.Stay option from Filter drop-down");
            Driver.RefreshPage();
            housingNearByPo.MoveToSortByFilter();
            IList<string> multipleOptionList = new[] { "30", "90" };
            housingNearByPo.SelectFilterOption(menuHeader, multipleOptionList);
            housingNearByPo.ExpandAllCardDetails();
            var minStayMultipleTextCardsList = housingNearByPo.GetMinimumStayTextFromAllCard();
            foreach (var actualType in minStayMultipleTextCardsList)
            {
                Assert.IsTrue(multipleOptionList.Contains(actualType), "Min stay text is not correct");
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void HousingNearBy_VerifyTypeFilterWorkSuccessfully()
        {
            var housingNearBy = new HousingNearByPo(Driver);
            var housingNearByPo = new HousingNearByDetailPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            var jobsDetailUrl = FusionMarketPlaceUrl + "/jobs/" + HousingNearByJobsId;
            Driver.NavigateTo(jobsDetailUrl);
            housingNearByPo.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info($"Step 2: Click on 'View all housing' link & select Type option from Filter drop-down");
            const string menuHeader = "Type";
            IList<string> typeOptionList = new[] { "Apartment" };
            housingNearBy.ClickOnViewAllHousingLink();
            housingNearByPo.SelectFilterOption(menuHeader, typeOptionList);

            Log.Info($"Step 3: Get Type text & verify its correct");
            var oneTypeTextCardsList = housingNearByPo.GetTypeTextFromAllCard();
            foreach (var actualText in oneTypeTextCardsList)
            {
                Assert.AreEqual(typeOptionList.First(), actualText, "Type text is not correct");
            }

            Log.Info($"Step 4: Refresh the page, select multiple 'Type' options");
            Driver.RefreshPage();
            IList<string> multipleTypeOptionList = new[] { "Apartment", "Condo" };
            housingNearByPo.SelectFilterOption(menuHeader, multipleTypeOptionList);

            Log.Info($"Step 5: Verify selected Type options are displayed in cards");
            var multipleTypeTextCardsList = housingNearByPo.GetTypeTextFromAllCard();
            foreach (var actualType in multipleTypeTextCardsList)
            {
                Assert.IsTrue(multipleTypeOptionList.Contains(actualType), "Type options text is not correct");
            }
        }
    }
}