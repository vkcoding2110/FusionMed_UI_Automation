using UIAutomation.Enum.Recruiters;

namespace UIAutomation.DataObjects.FMP.Agencies.Recruiter.RateAndReview
{
    public class RateAndReviewBase
    {
        public RoleType UserRoleType { get; set; }
        public InteractionType InteractionWithRecruiter { get; set; }
        public string OtherUserType { get; set; }
        public int OverallRating { get; set; }
        public int ReviewScale { get; set; }
        public string ReviewMessage { get; set; }
        public string NumberOfTravelJobs { get; set; }
    }
}
