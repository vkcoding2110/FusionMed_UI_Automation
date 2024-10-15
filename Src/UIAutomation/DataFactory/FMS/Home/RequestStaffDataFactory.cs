using System;
using UIAutomation.DataObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMS.Home
{
    public static class RequestStaffDataFactory
    {
        public static RequestStaff AddDataInRequestStaffForm()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new RequestStaff
            {
                FacilityName = "Home Health",
                YourName = "TestUser",
                PhoneNumber = "9898" + randomNumber.Remove(6),
                Email = "Test" + randomNumber + "@yopmail.com",
                SolutionType = "COVID-19 Testing",
                ProfessionalType = "Therapy",
                Specialty = "Physical Therapist - PT",
                JobType = "Permanent",
                StartDate = new DateTime(2021, 5, 12),
                Message = "We will get back to you soon"
            };
        }

    }
}