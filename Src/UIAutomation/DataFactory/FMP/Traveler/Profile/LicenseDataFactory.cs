using System;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMP.Traveler.Profile
{
    public static class LicenseDataFactory
    {

        public static License AddLicenseDetails()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new License
            {
                State = "Alaska",
                Compact = true,
                ExpirationDate = DateTime.Now.AddMonths(5),
                LicenseNumber = randomNumber,
            };
        }
        public static License EditLicenseDetails()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new License
            {
                State = "Florida",
                Compact = false,
                ExpirationDate = DateTime.Now.AddMonths(10),
                LicenseNumber = randomNumber,
            };
        }

    }
}
