using System;
using UIAutomation.DataObjects.FMS.ApplyNow;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMS.ApplyNow
{
    public static class SkillsCheckListDataFactory
    {
        public static SkillsCheckList AddDataInSurveyForm()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new SkillsCheckList()
            {
                OtherDetails = "Test-Automation" + randomNumber.Remove(3),
                ApplicationAgreement = true,
                Date = new DateTime(2021, 6, 20),
                FirstName = "Testing",
                LastName = "Test",
                Category = "Home Health",
                Specialty = "Home Health Clinical Manager",
                ApplicantEmail = "Test" + randomNumber + "@yopmail.com",
                ApplicantPhone = "9898" + randomNumber.Remove(6)
            };
        }
    }
}
