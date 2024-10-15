using System.Collections.Generic;
using UIAutomation.DataObjects.FMP.Agencies.Recruiter.Admin;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMP.Agencies.Recruiters.Admin
{
    public static class RecruiterDataFactory
    {
        public static Recruiter AddRecruitersDetails()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new Recruiter()
            {
                FirstName = "Automation",
                LastName = "TestRecruiter",
                Email = "testautomation" + randomNumber[..6] + "@yopmail.com",
                Department = new List<string> { "Laboratory" },
                AboutMe = "Inviting new recruiter as a system admin",
                PhoneNumber = "94" + randomNumber[..8],
                ImageFilePath = new FileUtil().GetBasePath() + "/TestData/FMP/TestDocuments/RecruiterImage.jpg"
            };
        }

        public static Recruiter EditRecruitersDetails()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new Recruiter()
            {
                FirstName = "TestAutomation",
                LastName = "RecruiterTest",
                Email = "testautomation" + randomNumber[..6] + "@yopmail.com",
                Department = new List<string> { "Laboratory", "Therapy", "Radiology & Imaging" },
                AboutMe = "Updating recruiter as a system admin",
                PhoneNumber = "93" + randomNumber[..8]
            };
        }
    }
}
