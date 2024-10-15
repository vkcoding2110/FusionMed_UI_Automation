using System;
using System.Collections.Generic;

namespace UIAutomation.DataObjects.FMP.Traveler.Profile
{
    public class Employment
    {
        public string Facility { get; set; }
        public string FacilityOption { get; set; }
        public string OtherFacility { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Category { get; set; }
        public string Specialty { get; set; }
        public List<string> JobSettingInput { get; set; }
        public string JobType { get; set; }
        public bool SupervisorEmployment { get; set; }
        public string Hours { get; set; }
        public string UnitAmount { get; set; }
        public string UnitType { get; set; }
        public List<string> ChartingSystemInput { get; set; }
        public string OtherChartingSystems { get; set; }
        public string PatientRatio { get; set; }
        public string JobDescription { get; set; }
        public bool WorkHere { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
