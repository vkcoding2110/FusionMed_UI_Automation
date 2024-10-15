using System;
using System.Collections.Generic;
using UIAutomation.DataObjects.Common.Jobs;

namespace UIAutomation.DataObjects.FMP.Jobs
{
    public class SortAndFilter : SortAndFilterBase
    {
        public string Agency { get; set; }
        public string JobType { get; set; }
        public List<string> FacilityType { get; set; }
        public DateTime StartDate { get; set; }
        public int MinJobQuantity { get; set; }
        public int MaxJobQuantity { get; set; }
        public string City { get; set; }
    }
}
