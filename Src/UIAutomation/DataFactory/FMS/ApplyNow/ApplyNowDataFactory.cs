using System;
using UIAutomation.DataObjects.FMS.ApplyNow;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMS.ApplyNow
{
    public static class ApplyNowDataFactory
    {
        public static QuickApplication AddDataInQuickApplyForm()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new QuickApplication
            {
                FirstName = "Testing",
                LastName = "Test",
                Email = "Test" + randomNumber + "@yopmail.com",
                Phone = "9898" + randomNumber.Remove(6),
                Category = "Cath Lab",
                Specialty = "Cath Lab RN",
                Cst = true,
                SomeoneReferredMe = true,
                ReferredBy = "test User",
                FilePath = new FileUtil().GetBasePath() + "/TestData/FMS/TestDocuments/Resume_testing.docx"
            };
        }

        public static FullApplication AddDataInFullAppForm()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new FullApplication
            {
                FirstName = "Testing",
                MiddleName = "Test",
                LastName = "Test",
                BirthDay = new DateTime(2021, 5, 12),
                Category = "Cath Lab",
                Speciality = "Cath Lab RN",
                Cst = true,
                MailingAddress = randomNumber.Remove(3) + " Ocean Ave. Union City, NJ " + randomNumber.Remove(5),
                City = "Plano",
                State = "CA",
                Zip = "123456",
                PhoneNumber = "9898" + randomNumber.Remove(6),
                AlterNativeNumber = "9898" + randomNumber.Remove(6),
                CallTime = new DateTime(2019, 5, 12, 11, 00, 00),
                Email = "Test" + randomNumber + "@yopmail.com",
                SomeoneReferredMe = true,
                ReferredBy = "test user",
                FilePath = new FileUtil().GetBasePath() + "/TestData/FMS/TestDocuments/Resume_testing.docx",
                EmergencyContact = "3412567835",
                Relationship = "Sister",
                EmergencyPhoneNumber = "3412567831",
                School = "Lotus Pmc",
                SchoolType = "Trade school",
                SchoolCity = "Wuhan",
                SchoolState = "CA",
                DegreeOrDiploma = "MBBS",
                DateGraduated = new DateTime(2019, 4, 12),
                Certification = "Medical",
                CertificationExpirationDate = new DateTime(2022, 5, 12),
                LicenseState = "CO",
                LicenseIssueDate = new DateTime(2019, 4, 12),
                LicenseExpirationDate = new DateTime(2021, 5, 12),
                ExperiencesSpeciality = "Nursing",
                ExperiencesYearsOfExperience = "1",
                DrugScreen = false,
                CriminalBackground = false,
                Limitations = false,
                LimitationList = "",
                PastEmployeeFacility = "CB Richard Ellis",
                PastEmployeeDepartment = "Cardiologists",
                PastEmployeeSupervisorName = "test user",
                PastEmployeeStartDate = new DateTime(2018, 5, 12),
                PastEmployeeEndDate = new DateTime(2019, 5, 12),
                PastEmployeeCity = "Wuhan",
                PastEmployeeState = "CA",
                PastEmployeeHours = "Full Time",
                PastEmployeePhone = randomNumber,
                GeneralDate = new DateTime(2021, 5, 12)
            };
        }
        public static DrugConsentForm AddDataDrugConsentForm()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new DrugConsentForm
            {
                FirstName = "Testing",
                LastName = "Test",
                Phone = "9898" + randomNumber.Remove(6),
                Email = "Test" + randomNumber + "@yopmail.com",
                Category = "Cath Lab",
                Speciality = "Cath Lab RN",
                Date = new DateTime(2020, 6, 20)

            };
        }

    }
}
