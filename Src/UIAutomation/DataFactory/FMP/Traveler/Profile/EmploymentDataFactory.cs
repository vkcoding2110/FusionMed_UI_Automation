using System;
using System.Collections.Generic;
using UIAutomation.DataObjects.FMP.Traveler.Profile;

namespace UIAutomation.DataFactory.FMP.Traveler.Profile
{
    public static class EmploymentDataFactory
    {
        public static Employment AddEmploymentDetails()
        {
            return new Employment
            {
                Facility = "Advocate Childrens Hospital - Oak Lawn",
                FacilityOption = "Advocate Childrens Hospital - Oak Lawn - Oak Lawn, IL",
                Category = "Laboratory",
                Specialty = "CLS",
                JobSettingInput = new List<string> {"Acute Rehab", "Home Health", "Imaging Center"},
                JobType = "Direct Hire",
                SupervisorEmployment = true,
                Hours = "40",
                UnitAmount = "10",
                UnitType = "Suites",
                ChartingSystemInput = new List<string> {"Allscripts", "Cerner"},
                OtherChartingSystems = "Meditech",
                PatientRatio = "15",
                JobDescription = "Write job description here for add employment test",
                WorkHere = true,
                StartDate = DateTime.Now.AddYears(-4),
                EndDate = DateTime.Now.AddYears(-2)
            };
        }

        public static Employment UpdateEmploymentFormDetails()
        {
            return new Employment
            {
                Facility = "Forbes Hospital",
                FacilityOption = "Forbes Hospital",
                Category = "Therapy",
                Specialty = "OT",
                JobSettingInput = new List<string> { "Acute Rehab", "Home Health", "Imaging Center" }, /* Change same list as add data factory due to defect id - 56 */
                JobType = "Local Contract",
                SupervisorEmployment = false,
                Hours = "20",
                UnitAmount = "5",
                UnitType = "Bays",
                ChartingSystemInput = new List<string> { "Centricity" },
                OtherChartingSystems = "Softlab",
                PatientRatio = "22",
                JobDescription = "Updated job description",
                StartDate = DateTime.Now.AddYears(-3),
                EndDate = DateTime.Now.AddYears(-1)
            };
        }
        public static Employment AddEmploymentDetailsWithOtherFacility()
        {
            return new Employment()
            {
                Facility = "Other",
                FacilityOption = "Other - My facility isn't listed",
                OtherFacility = "My Facility",
                City = "Wilmington",
                State = "Delaware",
                Category = "Laboratory",
                Specialty = "CLS",
                JobSettingInput = new List<string> { "DME", "Inpatient Rehab" },
                JobType = "Travel",
                SupervisorEmployment = false,
                Hours = "15",
                UnitAmount = "2",
                UnitType = "Labs",
                ChartingSystemInput = new List<string> { "EPIC Beaker" },
                OtherChartingSystems = "Jignect",
                PatientRatio = "2",
                JobDescription = "Job description ",
                StartDate = DateTime.Now.AddYears(1),
                EndDate = DateTime.Now.AddYears(3)
            };
        }
    }
}
