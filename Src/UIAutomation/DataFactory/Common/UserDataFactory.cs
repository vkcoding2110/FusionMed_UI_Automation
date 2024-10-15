using UIAutomation.DataObjects.Common.Account;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.Common
{
    public static class UserDataFactory
    {
        public static UserInformation AddUserInformation()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new UserInformation
            {
                FirstName = "Test" + randomNumber,
                LastName = "User" + randomNumber,
                Email = "test" + randomNumber + "@yopmail.com",
                PhoneNumber = "987654" + randomNumber.Remove(4),
                State = "Alaska",
                HearAboutUs = "Referral",
                ReferredBy = "Cara bloom",
                ResumeFilePath = new FileUtil().GetBasePath() + "/TestData/FMS/Resume_testing.docx"
            };
        }
    }
}
