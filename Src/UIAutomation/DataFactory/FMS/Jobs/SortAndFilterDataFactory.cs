using System.Collections.Generic;
using UIAutomation.DataObjects.FMS.Jobs;

namespace UIAutomation.DataFactory.FMS.Jobs
{
    public static class SortAndFilterDataFactory
    {
        public static SortAndFilter SortAndFilterDetails()
        {
            return new SortAndFilter
            {
                SortBy = new List<string> { "Pay: Highest" },
                Category = new List<string> { "Cath Lab" },
                Specialty = new List<string> { "Cath Lab Tech" },
                Shift = "Day",
                MinSalary = "1000",
                MaxSalary = "3000",
                Region = "Midwest",
                ZipCode = "08601",
                Distance = "50 miles"
            };
        }
    }
}
