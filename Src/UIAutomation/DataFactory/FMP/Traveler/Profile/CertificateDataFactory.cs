using System;
using UIAutomation.DataObjects.FMP.Traveler.Profile;

namespace UIAutomation.DataFactory.FMP.Traveler.Profile
{
    public static class CertificationDataFactory
    {
        public static Certification AddDataInCertification()
        {
            return new Certification
            {
                Category = "Laboratory",
                CertificationName = "AAB",
                CertificationFullName = "AAB (American Association of Bioanalysts)",
                ExpirationDate = DateTime.Now.AddMonths(1),
            };
        }

        public static Certification EditDataInCertification()
        {
            return new Certification
            {
                Category = "Therapy",
                CertificationName = "BLS (AHA)",
                CertificationFullName = "BLS (AHA) (Basic Life Support)",
                ExpirationDate = DateTime.Now.AddMonths(2),
            };
        }
    }
}