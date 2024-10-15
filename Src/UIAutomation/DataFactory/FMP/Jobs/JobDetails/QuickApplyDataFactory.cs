using UIAutomation.DataObjects.FMP.Jobs.JobDetails;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMP.Jobs.JobDetails
{
    public static class QuickApplyDataFactory
    {
        public static QuickApply AddQuickApplyInformation()
        {
            var randomNumber1 = new CSharpHelpers().GenerateRandomNumber().ToString();
            var randomNumber2 = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new QuickApply
            {
                FirstName = "Test",
                LastName = "Testing",
                Email = "test" + randomNumber1 + randomNumber2.Remove(5) + "@yopmail.com",
                PhoneNumber = "98" + randomNumber1.Substring(0,8),
                Category = "Laboratory",
                PrimarySpecialty = "CLS",
                State = "Alaska",
                SomeoneReferredMe = true,
                ReferredBy = "test user",
                ResumeFilePath = new FileUtil().GetBasePath() + "/TestData/FMP/Testing_Resume.docx"
            };
        }
    }
}
