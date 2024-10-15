using System;

namespace UIAutomation.DataObjects.FMP.Traveler.Profile
{
    public class TimeBetweenJobs
    {
        public bool TimeOff { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool NonMedicalField { get; set; }
    }
}
