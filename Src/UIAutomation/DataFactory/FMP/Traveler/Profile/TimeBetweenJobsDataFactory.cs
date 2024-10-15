using System;
using System.Collections.Generic;
using UIAutomation.DataObjects.FMP.Traveler.Profile;

namespace UIAutomation.DataFactory.FMP.Traveler.Profile
{
    public static class TimeBetweenJobsDataFactory
    {
        public static List<TimeBetweenJobs> AddTimeBetweenJobsDetails()
        {
            return new List<TimeBetweenJobs>
            {
                new TimeBetweenJobs
                {
                    TimeOff = true,
                    StartDate = DateTime.Now.AddYears(-2),
                    EndDate = DateTime.Now.AddYears(-1),
                    City = "Omaha",
                    State = "Nebraska",
                    NonMedicalField = true
                },
                new TimeBetweenJobs
                {
                    TimeOff = true,
                    StartDate = DateTime.Now.AddYears(-1),
                    EndDate = DateTime.Now.AddMonths(-3),
                    City = "Lady Lake",
                    State = "Indiana",
                    NonMedicalField = true
                }
            };
        }

        public static TimeBetweenJobs UpdateTimeBetweenJobsDetails()
        {
            return new TimeBetweenJobs
            {
                TimeOff = true,
                StartDate = DateTime.Now.AddYears(-1),
                EndDate = DateTime.Now.AddMonths(-4),
                City = "Akiak",
                State = "California",
                NonMedicalField = true
            };
        }
    }
}
