using System;

namespace UIAutomation.DataObjects.FMP.Traveler.Profile
{
    public class Profile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Category { get; set; }
        public string PrimarySpecialty { get; set; }
        public string OtherSpecialty { get; set; }
        public string YearsOfExperience { get; set; }
        public bool HealthcareExperience { get; set; }
        public string AboutMe { get; set; }
        public string MailingAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public License License { get; set; }
        public string ImageFilePath { get; set; }
    }
}
