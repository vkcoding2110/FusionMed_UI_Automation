using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMS.Careers;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.Careers
{
    [TestClass]
    [TestCategory("Home"), TestCategory("FMS")]
    public class OpportunitiesTests : FmsBaseTest
    {
        [TestMethod]
        public void VerifyJobDetailsAreCorrect()
        {
            var footer = new FooterPo(Driver);
            var opportunities = new OpportunitiesPo(Driver);
            var opportunitiesDetails = new OpportunitiesDetailsPagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Careers link");
            footer.ClickOnCareersOption();
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 3 : Click on first five job card and Verify each job detail is correct");
            for (var i = 1; i < 5; i++)
            {
                var expectedJobTitle = opportunities.GetJobTitle(i);
                var expectedJobCategory = opportunities.GetJobCategory(i);
                var expectedRequisitionNumber = opportunities.GetRequisitionNumber(i);
                var expectedSchedule = opportunities.GetSchedule(i);
                var expectedAddress = opportunities.GetAddress(i);
                var expectedJobPostedDate = opportunities.GetJobPostedDateText(i);
                opportunities.ClickOnNthJobCard(i);
                var actualJobTitle = opportunitiesDetails.GetJobTitle();
                var actualCategory = opportunitiesDetails.GetJobCategory();
                var actualRequisitionNumber = opportunitiesDetails.GetRequisitionNumber();
                var actualSchedule = opportunitiesDetails.GetSchedule();
                var actualAddress = opportunitiesDetails.GetAddress();
                var actualJobPostedDate = opportunitiesDetails.GetPostedDateText();
                Assert.AreEqual(expectedJobTitle, actualJobTitle, "Job 'Title' doesn't match");
                Assert.AreEqual(expectedJobCategory, actualCategory, "Job 'Category' doesn't match");
                Assert.AreEqual(expectedRequisitionNumber, actualRequisitionNumber, "Job 'Requisition Number' doesn't match");
                Assert.AreEqual(expectedSchedule.Insert(4, "-").RemoveWhitespace(), actualSchedule,
                    "Job schedule doesn't match");
                Assert.AreEqual(expectedAddress.RemoveWhitespace(), actualAddress.RemoveWhitespace(), "Job 'Address' doesn't match");
                Assert.AreEqual(expectedJobPostedDate, actualJobPostedDate, "Job 'Date' doesn't match");
                opportunitiesDetails.ClickOnBackToCareerPage();
            }
        }

        [TestMethod]
        public void VerifyJobSearchFunctionalityWorkSuccessfully()
        {
            var footer = new FooterPo(Driver);
            var opportunities = new OpportunitiesPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Careers link");
            footer.ClickOnCareersOption();
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 3: Enter job name in search bar and hit enter ");
            const string searchJobName = "Front-End Developer";
            opportunities.EnterJobsToSearchBoxAndHitEnter(searchJobName);

            Log.Info($"Step 4: Verify that  the result is returned, having names = {searchJobName}");
            var actualJobCardTitle = opportunities.GetFirstJobCardTitle();
            Assert.AreEqual(searchJobName, actualJobCardTitle, "Job title doesn't match");
        }

        [TestMethod]
        public void VerifySearchFilterWorksSuccessfully()
        {
            var footer = new FooterPo(Driver);
            var opportunities = new OpportunitiesPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Careers link");
            footer.ClickOnCareersOption();
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 3: Click on 'Location' dropdown, enter location in search bar, hit enter and verify 'Location' in job card ");
            const string expectedLocation = "Corporate Office | Omaha, NE 68164, USA";
            opportunities.SelectCompanyLocationDropDown(expectedLocation);
            var jobCardCount = opportunities.GetAllJobCardCount();
            for (var i = 1; i < jobCardCount; i++)
            {
                var actualLocation = opportunities.GetAddress(i).RemoveWhitespace();
                Assert.AreEqual(expectedLocation.RemoveWhitespace().Replace("|",""), actualLocation, "Job 'Location' doesn't match");
            }

            Log.Info("Step 4: Click on 'Job Category' dropdown, enter category name in search bar, hit enter and verify 'Category' in job card");
            const string expectedJobCategory = "Technology";
            opportunities.ClickOnResetAllButton();
            opportunities.SelectJobCategory(expectedJobCategory);
            var categoryJobCardCount = opportunities.GetAllJobCardCount();
            for (var i = 1; i < categoryJobCardCount; i++)
            {
                var actualJobCategory = opportunities.GetJobCategory(i).RemoveWhitespace();
                Assert.AreEqual(expectedJobCategory.RemoveWhitespace(), actualJobCategory, "Job 'category' doesn't match");
            }

            Log.Info("Step 5: Click on 'Schedule' dropdown, enter category name in search bar, hit enter and verify 'Schedule' in job card");
            const string expectedSchedule = "Full Time";
            opportunities.ClickOnResetAllButton();
            opportunities.SelectScheduleDropDown(expectedSchedule);
            var scheduleJobCardCount = opportunities.GetAllJobCardCount();
            for (var i = 1; i < scheduleJobCardCount; i++)
            {
                var actualSchedule = opportunities.GetSchedule(i).RemoveWhitespace();
                Assert.AreEqual(expectedSchedule.RemoveWhitespace(), actualSchedule, "Job 'Schedule' doesn't match");
            }
        }
    }
}
