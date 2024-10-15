using System;
using System.Collections.Generic;
using UIAutomation.DataObjects.FMP.Jobs;

namespace UIAutomation.DataFactory.FMP.Jobs
{
    public static class SortAndFilterDataFactory
    {
        public static SortAndFilter SortAndFilterDetails()
        {
            return new SortAndFilter()
            {
                SortBy = new List<string> { "Pay: Highest", "Start Date" },
                Department = "Agencies",
                Agency = "Fusion Medical Staffing",
                Category = new List<string> { "Laboratory", "Therapy" },
                Specialty = new List<string> { "CLS", "OT" },
                Shift = "Day",
                MinSalary = "1000",
                MaxSalary = "3000",
                JobType = "Travel",
                FacilityType = new List<string> { "Hospice", "Hospital" },
                StartDate = DateTime.Now.AddMonths(-5),
                MinJobQuantity = 1,
                MaxJobQuantity = 10,
                Region = "West",
                City = "Minot, ND",
                Distance = "50 miles"
            };
        }
    }
}