using UIAutomation.DataObjects.FMP.Agencies.Recruiter.Admin;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMP.Agencies.Recruiters.Admin
{
    public static class AgencyAdminDataFactory
    {
        public static AgencyAdmin AddAgencyAdminDetails()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new AgencyAdmin()
            {
                FirstName = "Automation_Agency" + randomNumber[..2],
                LastName = "Automation_Admin"+ randomNumber[..2],
                Email = "testautomation" + randomNumber[..3] + "@yopmail.com",
            };
        }

        public static AgencyAdmin EditAgencyAdminDetails()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new AgencyAdmin()
            {
                FirstName = "Edited_Automation_Agency" + randomNumber[..2],
                LastName = "Edited_Automation_Admin" + randomNumber[..2],
                Email = "testautomation" + randomNumber[..4] + "@yopmail.com",
            };
        }
    }
}
