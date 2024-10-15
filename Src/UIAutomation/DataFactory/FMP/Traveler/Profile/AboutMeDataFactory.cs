using System;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMP.Traveler.Profile
{
    public static class AboutMeDataFactory
    {
        public static DataObjects.FMP.Traveler.Profile.Profile EditProfileDetails()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new DataObjects.FMP.Traveler.Profile.Profile
            {
                FirstName = "Test",
                LastName = "Testing",
                MailingAddress = "1300 W. Benson Blvd., Suite 900 Anchorage, AK 99503",
                City = "Akiak",
                ZipCode = "99552",
                PhoneNumber = "95" + randomNumber.Substring(0, 8),
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                SocialSecurityNumber = "023456780",
                Category = "Laboratory",
                PrimarySpecialty = "CLS",
                OtherSpecialty = "MLT",
                YearsOfExperience = "4",
                HealthcareExperience = false,
                AboutMe = "Looking for good opportunity",
                License = new License
                {
                    State = "Alaska",
                    StateAlias = "AK"
                },
                ImageFilePath = new FileUtil().GetBasePath() + "/TestData/FMP/TestDocuments/UserImage.jpg"
            };
        }
        public static DataObjects.FMP.Traveler.Profile.Profile UploadProfileImage()
        {
            return new DataObjects.FMP.Traveler.Profile.Profile
            {
                ImageFilePath = new FileUtil().GetBasePath() + "/TestData/FMP/TestDocuments/EditImage.jpg"
            };
        }
    }
}
