using System;
using System.Collections.Generic;

namespace UIAutomation.DataObjects.FMP.Traveler.JobPreferences
{
    public class JobPreference
    {
        public List<string> Departments { get; set; }
        public List<string> Specialties { get; set; }
        public List<string> States { get; set; }
        public List<string> Cities { get; set; }
        public List<string> ShiftType { get; set; }
        public List<string> JobType { get; set; }
        public string MinSalary { get; set; }
        public string MaxSalary { get; set; }
        public DateTime StartDate { get; set; }
        public bool StartNow { get; set; }
    }
}
