using System.Collections.Generic;

namespace UIAutomation.DataObjects.FMP.Agencies.Recruiter.Admin
{
    public class Recruiter
    {
        public string RecruiterName { get; set; }
        public Agency RecruiterAgency { get; set; }
        public List<string> Specialty { get; set; }
        public string AboutMe { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Department { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageFilePath { get; set; }
    }
}
