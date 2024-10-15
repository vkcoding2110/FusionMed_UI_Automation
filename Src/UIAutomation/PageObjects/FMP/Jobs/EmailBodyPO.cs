using System.Collections.Generic;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Jobs.JobDetails;
using UIAutomation.Enum;
using UIAutomation.PageObjects.YopMail;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Jobs
{
    internal class EmailBodyPo : EmailListingGridPo
    {
        public EmailBodyPo(IWebDriver driver) : base(driver)
        {
        }

        public static readonly Dictionary<string, string> JobDetailsDict = new Dictionary<string, string>
        {
             { "AgencyName", "Agency:" },
             { "JobId", "Job:" },
             {"Facility","Facility:" },
             {"FacilityType","Facility Type:" },
             {"NumberOfBeds","Number of Beds:"},
             {"Shift","Shift:"},
             {"AssignmentLength","Assignment Length:"},
             {"JobQuantity","Job Quantity:" },
             {"Type","Type:" },
        };


        //Sharing Job with others Email Body
        private static By JobDetails(string categoryName) => By.XPath($" //div[@id='mail']/div/table/following-sibling::table/tbody//span[text()='{JobDetailsDict[categoryName]} ']/following-sibling::span");

        //Sharing Job with others Email Body
        public Job GetJobDetailsFromEmailPage()
        {
            Driver.SwitchToIframe(BaseTest.PlatformName.Equals(PlatformName.Web)
                ? Wait.UntilElementExists(MessageBodyIframe)
                : Wait.UntilElementExists(InboxMobMailFrame));
            var jobDetails = new Job
            {
                AgencyName = Wait.UntilElementVisible(JobDetails("AgencyName")).GetText(),
                Id = Wait.UntilElementVisible(JobDetails("JobId")).GetText(),
                Facility = Wait.UntilElementVisible(JobDetails("Facility")).GetText(),
                FacilityType = Wait.UntilElementVisible(JobDetails("FacilityType")).GetText(),
                NumberOfBeds = Wait.UntilElementVisible(JobDetails("NumberOfBeds")).GetText(),
                Shift = Wait.UntilElementVisible(JobDetails("Shift")).GetText(),
                AssignmentLength = Wait.UntilElementVisible(JobDetails("AssignmentLength")).GetText(),
                Quality = Wait.UntilElementVisible(JobDetails("JobQuantity")).GetText(),
                Type = Wait.UntilElementVisible(JobDetails("Type")).GetText()
            };
            Driver.SwitchToDefaultIframe();
            return jobDetails;
        }
    }
}
