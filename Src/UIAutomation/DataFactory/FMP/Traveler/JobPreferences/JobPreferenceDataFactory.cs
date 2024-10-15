using System;
using System.Collections.Generic;
using UIAutomation.DataObjects.FMP.Traveler.JobPreferences;

namespace UIAutomation.DataFactory.FMP.Traveler.JobPreferences
{
    public static class JobPreferenceDataFactory
    {
        public static JobPreference AddPreferenceDetails()
        {
            return new JobPreference
            {
                Departments = new List<string> { "Therapy"},
                Specialties = new List<string> { "OT" },
                States = new List<string> { "California" },
                Cities = new List<string>{ "Alameda, CA" },
                ShiftType = new List<string> { "Day" },
                JobType = new List<string> { "Direct Hire" },
                MinSalary ="2500",
                MaxSalary ="5000",
                StartDate = DateTime.Now.AddDays(5),
                StartNow = true
            };
        }
        public static JobPreference EditJobPreferenceDetails()
        {
            return new JobPreference
            {
                Departments = new List<string> { "Laboratory","Radiology & Imaging" },
                Specialties = new List<string> {"CLS","Lab Assistant" },
                States = new List<string> {"Alaska","California"},
                Cities = new List<string>{ "Apple Valley, CA", "Arcata, CA" },
                ShiftType =new List<string>{ "Night"},
                JobType = new List<string> {"Direct Hire"},
                MinSalary = "2000",
                MaxSalary = "5000",
                StartDate = DateTime.Now.AddDays(10),
                StartNow = false
            };
        }
        public static JobPreference AddJobPreferenceDetailToValidate()
        {
            return new JobPreference
            {
                States = new List<string> { "Alabama", "Alaska" , "Arizona" , "Arkansas", "California" },
                Cities = new List<string> { "Abbeville, AL", "Abernant, AL", "Acampo, CA", "Acton, CA", "Adak, AK", "Adamsville, AL", "Addison, AL", "Adelanto, CA", "Adger, AL", "Adin, CA" },
                ShiftType = new List<string> { "D/N Rotate", "Day", "Evening" },
                JobType = new List<string> { "Per Diem" , "Rapid Response" , "Temp-to-Hire" }
            };
        }
    }
}

