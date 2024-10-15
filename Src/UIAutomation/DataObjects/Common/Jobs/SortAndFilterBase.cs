using System.Collections.Generic;

namespace UIAutomation.DataObjects.Common.Jobs
{
    public class SortAndFilterBase
    {
        public List<string> SortBy { get; set; }
        public string Department { get; set; }
        public List<string> Category { get; set; }
        public List<string> Specialty { get; set; }
        public string Shift { get; set; }
        public string MinSalary { get; set; }
        public string MaxSalary { get; set; }
        public string Region { get; set; }
        public string Distance { get; set; }
    }
}
