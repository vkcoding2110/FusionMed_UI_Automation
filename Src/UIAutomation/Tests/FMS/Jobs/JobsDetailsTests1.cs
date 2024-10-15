using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.FMS.Jobs;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.Jobs
{
    [TestClass]
    [TestCategory("Jobs"), TestCategory("FMS")]
    public class JobsDetailsTests1 : FmsBaseTest
    {
        private readonly string ClosedJobId = GetJobsStatus(Env, "closedJobs");

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatDisabledApplyForThisJobButtonForClosedJobs()
        {
            var jobsDetails = new JobsDetailsPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            jobsDetails.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Navigate to Job detail page & verify closed job message and 'Apply for this jobs' button gets disabled");
            var jobsDetailUrl = FmsUrl + "/jobs/" + ClosedJobId;
            Driver.NavigateTo(jobsDetailUrl);
            jobsDetails.WaitUntilMpPageLoadingIndicatorInvisible();
            Assert.IsFalse(jobsDetails.IsApplyForThisJobButtonEnabled(), "The 'Apply for this job' button is not disabled");

            const string expectedJobTitleFilledText = "(Filled)";
            var actualJobTitleFilledText = jobsDetails.GetJobTitle();
            Assert.IsTrue(actualJobTitleFilledText.EndsWith(expectedJobTitleFilledText), $"The filled text is not matched  Expected : {expectedJobTitleFilledText}, Actual : {actualJobTitleFilledText}");

            var expectedClosedJobMessage = $"Uh Oh!\r\nLooks like this job has been filled.\r\nClick here to browse all our other awesome {actualJobTitleFilledText}jobs!";
            var actualClosedJobMessage = jobsDetails.GetFilledJobMessage();
            Assert.AreEqual(expectedClosedJobMessage.Replace("(" + "Filled)", "").RemoveWhitespace(), actualClosedJobMessage.RemoveWhitespace(), "The closed job message is not matched");
        }
    }
}
