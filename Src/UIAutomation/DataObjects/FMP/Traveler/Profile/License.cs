using System;

namespace UIAutomation.DataObjects.FMP.Traveler.Profile
{
    public class License
    {
        public string State { get; set; }
        public string StateAlias { get; set; }
        public bool Compact { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string LicenseNumber { get; set; }
        public string ExpirationMonth { get; set; }

        public string ExpirationYear { get; set; }
    }
}
