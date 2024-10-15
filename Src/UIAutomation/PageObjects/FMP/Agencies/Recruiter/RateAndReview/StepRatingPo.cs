using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Agencies.Recruiter.RateAndReview;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter.RateAndReview
{
    internal class StepRatingPo : FmpBasePo
    {
        public StepRatingPo(IWebDriver driver) : base(driver)
        {
        }
        //Guest user
        private static By OverAllRating(int number) => By.XPath($"//label[@for='overallRating-{number}']");
        private readonly By OverAllRatingLabel = By.XPath("//label[contains(text(),'overall rating')]//following-sibling::div//div[contains(@class,'RatingLabel')]//span");
        private readonly By StarRatingProgressBar = By.XPath("//div[contains(@class,'MuiStep-completed')]//span[text()='STAR RATING']");
       
        // Validation message
        private readonly By ValidationMessageOfOverAllRatingField = By.XPath("//label[contains(text(),'overall rating')]//following-sibling::p");

        // Traveler
        private static By MeetYourNeedsRating(int number) => By.XPath($"//label[@for='listenRating-{number}']");
        private static By CommunicationRating(int number) => By.XPath($"//label[@for='communicationRating-{number}']");
        private static By KnowledgeRating(int number) => By.XPath($"//label[@for='knowledgeRating-{number}']");
        private static By ProfessionalRelationshipRating(int number) => By.XPath($"//label[@for='professionalRating-{number}']");
        private static By SupportRating(int number) => By.XPath($"//label[@for='supportRating-{number}']");
        private static By EfficiencyRating(int number) => By.XPath($"//label[@for='efficientRating-{number}']");

        private readonly By MeetYourNeedsRatingLabel = By.XPath("//label[contains(text(),'meet your needs')]//following-sibling::div//div[contains(@class,'RatingLabel')]//span");
        private readonly By CommunicationRatingLabel = By.XPath("//label[contains(text(),'Communicate')]//following-sibling::div//div[contains(@class,'RatingLabel')]//span");
        private readonly By KnowledgeRatingLabel = By.XPath("//label[contains(text(),'knowledge')]//following-sibling::div//div[contains(@class,'RatingLabel')]//span");
        private readonly By ProfessionalRelationshipRatingLabel = By.XPath("//label[contains(text(),'professional relationship')]//following-sibling::div//div[contains(@class,'RatingLabel')]//span");
        private readonly By SupportRatingLabel = By.XPath("//label[contains(text(),'Support')]//following-sibling::div//div[contains(@class,'RatingLabel')]//span");
        private readonly By EfficiencyRatingLabel = By.XPath("//label[contains(text(),'efficiently')]//following-sibling::div//div[contains(@class,'RatingLabel')]//span");   

        public void GiveOverAllRating(int overAllRating)
        {
            Wait.UntilElementClickable(OverAllRating(overAllRating)).ClickOn();
        }
        public string GetOverAllRatingLabelText()
        {
            return Wait.UntilElementVisible(OverAllRatingLabel).GetText();
        }

        public void GiveAbilitiesRating(TravelerRateAndReview travelerReview)
        {
            Wait.UntilElementClickable(MeetYourNeedsRating(travelerReview.MeetYourNeedsRating)).ClickOn();
            Wait.UntilElementClickable(CommunicationRating(travelerReview.CommunicationRating)).ClickOn();
            Wait.UntilElementClickable(KnowledgeRating(travelerReview.KnowledgeRating)).ClickOn();
            Wait.UntilElementClickable(ProfessionalRelationshipRating(travelerReview.ProfessionalRelationshipRating)).ClickOn();
            Wait.UntilElementClickable(SupportRating(travelerReview.SupportRating)).ClickOn();
            Wait.UntilElementClickable(EfficiencyRating(travelerReview.EfficiencyRating)).ClickOn();
        }

        public TravelerRateAndReview GetAbilitiesRating()
        {
            var meetYourNeeds = Wait.UntilElementVisible(MeetYourNeedsRatingLabel).GetText();
            var knowledge = Wait.UntilElementVisible(KnowledgeRatingLabel).GetText();
            var communication = Wait.UntilElementVisible(CommunicationRatingLabel).GetText();
            var professionalRelationship = Wait.UntilElementVisible(ProfessionalRelationshipRatingLabel).GetText();
            var support = Wait.UntilElementVisible(SupportRatingLabel).GetText();
            var efficiency = Wait.UntilElementVisible(EfficiencyRatingLabel).GetText();
            var ratings = FmpConstants.OverAllRatingAndMessage;
            return new TravelerRateAndReview()
            {
                MeetYourNeedsRating = ratings.FirstOrDefault(x => x.Value == meetYourNeeds).Key,
                KnowledgeRating = ratings.FirstOrDefault(x => x.Value == knowledge).Key,
                CommunicationRating = ratings.FirstOrDefault(x => x.Value == communication).Key,
                ProfessionalRelationshipRating = ratings.FirstOrDefault(x => x.Value == professionalRelationship).Key,
                SupportRating = ratings.FirstOrDefault(x => x.Value == support).Key,
                EfficiencyRating = ratings.FirstOrDefault(x => x.Value == efficiency).Key,
            };
        }

        public bool IsStarRatingProgressBarFilled()
        {
            return Wait.IsElementPresent(StarRatingProgressBar, 5);
        }
        public string GetValidationMessageOfOverAllRatingField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfOverAllRatingField).GetText();
        }
    }
}
