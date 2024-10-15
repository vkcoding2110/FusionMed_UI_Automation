using System.Collections.Generic;
using UIAutomation.DataObjects.FMP.Traveler.AccountSettings;

namespace UIAutomation.DataFactory.FMP.Traveler.AccountSettings
{
    public static class CloseAccountDataFactory
    {
        public static CloseAccount DisableAndPermanentlyCloseAccountDetails()
        {
            return new CloseAccount
            {
                Reasons = new List<string> { "Not useful to my job search", "Website is difficult to use", "I’m leaving the industry", "I’m trying to reduce my online presence", "Other" },
                OtherReason = "Nothing",
                Comment = "Please close the account for testing",
                AccountText = "CLOSE"
            };
        }
    }

}