using UIAutomation.DataObjects.FMP.TravelerProfile.ProfileDashboard;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMP.Traveler.Profile
{
    public static class ReferencesDataFactory
    {
        public static Reference AddReferenceDetail()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new Reference
            {
                FirstName = "Test",
                LastName = "Testing",
                Title = "Referred by",
                WorkTogether = "Advocate Childrens Hospital - Oak Lawn",
                Relationship = "Supervisor",
                PhoneNumber = "99" + randomNumber.Substring(0, 8),
                Email = "TestTesting08" + "@yopmail.com"
            };
        }

        public static Reference EditReferenceDetail()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new Reference
            {
                FirstName = "Testing",
                LastName = "Test",
                Title = "Testing reference",
                WorkTogether = "Forbes Hospital",
                Relationship = "Doctor",
                PhoneNumber = "82" + randomNumber.Substring(0, 8),
                Email = "TestTesting80" + "@yopmail.com"
            };
        }
    }
}
