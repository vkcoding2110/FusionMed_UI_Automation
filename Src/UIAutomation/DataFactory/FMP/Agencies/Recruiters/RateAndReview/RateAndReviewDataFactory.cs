using UIAutomation.DataObjects.FMP.Agencies.Recruiter.RateAndReview;
using UIAutomation.Enum.Recruiters;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMP.Agencies.Recruiters.RateAndReview
{
    public class RateAndReviewDataFactory : RateAndReviewBase
    {
        public static RateAndReviewBase AddRateAndReviewDetail()
        {
            return new()
            {
                UserRoleType = RoleType.Client,
                OtherUserType = "Guest Test User",
                InteractionWithRecruiter = InteractionType.SeveralTimes,
                OverallRating = 4,
                ReviewScale = 5,
                ReviewMessage = "Testing, Reviewing as a guest",
                NumberOfTravelJobs = "4"
            };
        }

        public static TravelerRateAndReview AddRateAndReviewDetailAsATraveler()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new TravelerRateAndReview()
            {
                NumberOfTravelJobs = "2",
                InteractionWithRecruiter = InteractionType.ManyTimes,
                OverallRating = 5,
                ReviewScale = 8,
                ReviewMessage = "Testing, Reviewing as a Traveler" + randomNumber[..3],
                MeetYourNeedsRating = 3,
                CommunicationRating = 4,
                KnowledgeRating = 4,
                ProfessionalRelationshipRating = 3,
                SupportRating = 4,
                EfficiencyRating = 3
            };
        }
        public static TravelerRateAndReview EditRateAndReviewDetailAsATraveler()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new TravelerRateAndReview()
            {
                NumberOfTravelJobs = "3",
                InteractionWithRecruiter = InteractionType.FewTimes,
                OverallRating = 3,
                ReviewScale = 4,
                ReviewMessage = "Testing,Editing Review as a Traveler" + randomNumber[..5],
                MeetYourNeedsRating = 2,
                CommunicationRating = 3,
                KnowledgeRating = 3,
                ProfessionalRelationshipRating = 2,
                SupportRating = 3,
                EfficiencyRating = 2
            };
        }
        public static RateAndReviewBase AddRateAndReviewDetailAsAOtherUser()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new RateAndReviewBase()
            {
                UserRoleType = RoleType.Other,
                OtherUserType = "Recruiter",
                InteractionWithRecruiter = InteractionType.JustOnce,
                OverallRating = 3,
                ReviewScale = 6,
                ReviewMessage = "Testing, Reviewing as a Recruiter" + randomNumber[..4],
            };
        }
    }
}
