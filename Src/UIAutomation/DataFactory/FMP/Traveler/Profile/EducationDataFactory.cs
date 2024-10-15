using System;
using UIAutomation.DataObjects.FMP.Traveler.Profile;

namespace UIAutomation.DataFactory.FMP.Traveler.Profile
{
    public static class EducationDataFactory
    {
        public static Education AddDataInEducationForm()
        {
            return new Education
            {
                InstitutionName = "Test TourHealth Institute of Medical",
                FieldOfStudy = "Physiology",
                City = "Lady Lake",
                State = "Florida",
                DegreeDiploma = "Bachelor",
                GraduatedDate = DateTime.Now.AddMonths(1),
            };
        }

        public static Education EditDataInEducationForm()
        {
            return new Education
            {
                InstitutionName = "Test University of Health & Medical Science",
                FieldOfStudy = "Paediatrics",
                City = "Oakland",
                State = "California",
                DegreeDiploma = "Ph.D.",
                GraduatedDate = DateTime.Now.AddMonths(6),
            };
        }
    }
}
