using System.Collections.Generic;

namespace UIAutomation.DataObjects.FMP.Traveler.AccountSettings
{
    public class CloseAccount
    {
        public List<string> Reasons { get; set; }
        public string OtherReason { get; set; }
        public string Comment { get; set; }
        public string AccountText { get; set; }
    }
}