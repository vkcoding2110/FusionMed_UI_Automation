using System.Collections.Generic;
using UIAutomation.Enum.Recruiters;

namespace UIAutomation.Utilities
{
    public class FmpConstants
    {
        // Attached reference message
        public const string EmploymentDeleteRecordReferenceMessage = "If you would like to delete this employment, references below must be deleted first.";

        //Validation messages
        public const string MandatoryFieldValidationMessage = "This field is required.";
        public const string MandatoryDateFieldValidationMessage = "Please select a valid date.";
        public const string CheckYourEmailMessage = "Look for the verification email in your inbox from noreply@fusionmarketplace.com and follow the link provided. You’ll be directed to log in from your web browser once your email is confirmed.";

        //Recruiter Rating And Reviews
        public static  Dictionary<int, string> OverAllRatingAndMessage = new()
        {
            {1, "Terrible"},
            {2, "Bad"},
            {3, "Just okay"},
            {4, "Good"},
            {5,"Amazing!"}
        };

        public static  Dictionary<System.Enum, string> SelectedRecruiterType = new()
        {
                {InteractionType.JustOnce, "once" },
                {InteractionType.FewTimes,"a few times" },
                {InteractionType.SeveralTimes,"several times" },
                {InteractionType.ManyTimes,"many times" }
        };

        public const string RecruiterNoReviews = "AutomationRecruiterNoReviews";
        public const string Recruiter2Star = "AutomationRecruiter2Star";
        public const string RecruiterFiveReviews = "AutomationRecruiterFiveReviews";
        public const string RecruiterForResponseCrud = "RecruiterForResponseCRUD";
        public const string AgencyAdmin = "AgencyAdminTests";
        public const string SystemAdmin = "SystemAdminTests";
        public const string MyOverallRatingsDescriptionText = "Your overall ratings display on your profile, along with all public reviews. Only public reviews contribute to your overall ratings.";

        public const string InappropriateMessage = "Shit";

        //Jobs
        public const string JobsText = "Jobs";

        public static  List<string> AgencyList = new() { "View All Agencies", "Test Agency"};
    }
}