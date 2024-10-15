using UIAutomation.DataObjects.FMP.Agencies;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMP.Agencies
{
    public static class PartnerWithUsDataFactory
    {
        public static PartnerWithUs AddPartnerWithUsFormDetails()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new PartnerWithUs
            {
                Name = "Test",
                Title = "Test ",
                WorkEmail = "Test" + randomNumber + "@yopmail.com",
                PhoneNumber = "987654" + randomNumber.Remove(4),
                AgencyName = "Test Testing",
                NumberOfRecruiters = "2"
            };
        }
    }
}
