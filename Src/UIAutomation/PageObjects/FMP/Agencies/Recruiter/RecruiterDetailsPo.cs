using OpenQA.Selenium;
using System.Linq;
using UIAutomation.DataObjects.FMP.Agencies;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter
{
    internal class RecruiterDetailsPo : FmpBasePo
    {
        public RecruiterDetailsPo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By RecruiterName = By.XPath("//div[contains(@class,'HeaderDetails')]/h1");
        private readonly By RecruiterSpecialty = By.XPath("//span[contains(@class,'HeaderDepartmentsSpan')]");
        private readonly By RecruiterAgency = By.XPath("//div[contains(@class,'HeaderWrapper')]/img");
        private readonly By RecruiterAboutMe = By.XPath("//div[contains(@class,'DetailsAboutMeWrapper')]//p");
        private readonly By ReadMoreLink = By.XPath("//div[contains(@class,'DetailsAboutMeWrapper')]//p/a");
        private readonly By RecruiterProfilePhoto = By.XPath("//div[contains(@class,'AvatarWrapper')]/div/img");
        private readonly By ReviewThisRecruiterButton = By.XPath("//div[contains(@class,'HeaderButtonsWrapper')]//a/span[text()='Review this Recruiter']");
        private readonly By LearnMoreAboutAgencyLink = By.XPath("//div[contains(@class,'DetailsLocationInfoWrapper')]//div/a[1]");
        private readonly By BrowseJobsLink = By.XPath("//div[contains(@class,'DetailsLocationInfoWrapper')]//div/a[2]");
        private readonly By ReviewRecruiterLink = By.XPath("//div[contains(@class,'RecruiterRatingsHeaderText')]/a");
        private readonly By RecruiterRating = By.XPath("//div[contains(@class,'RecruiterRatingContainer')]/span");
        private readonly By RecruiterStarRating = By.XPath("//div[contains(@class,'RecruiterRatingContainer')]//span[@class='MuiRating-decimal']");
        private readonly By ReviewText = By.XPath("//div[contains(@class,'TypeFrequentcyDateContainer')]");
        private readonly By UserRoleTypeName = By.XPath("//div[contains(@class,'NamePhotoContainer')]");
        private readonly By UserRoleTypeText = By.XPath("//div[contains(@class,'TypeFrequentcyDateContainer')]");
        private readonly By ReviewDate = By.XPath("//div[contains(@class,'ReviewDate')]");
        private readonly By ReviewRecruiterButton = By.XPath("//span[text()='Review this Recruiter']//parent::a");
        private readonly By ReviewMessage = By.XPath("//div[contains(@class,'ReviewTextContainer')]");
        private readonly By RateAndReviewHeaderText = By.XPath("//div[contains(@class,'RecruiterInfoContainer')]/div/following-sibling::h3");
        private readonly By EditAboutMeButton = By.XPath("//button[contains(@class,'EditButton')]/span[text()='Edit About Me']");
        private readonly By PhoneNumberText = By.XPath("//div[contains(@class,'DetailsContactWrapper')]/div[text()='Call or Text:']//a");

        //Respond On Review
        private readonly By ResponseOnReviewTextFromFirstReview = By.XPath("//div[contains(@class,'ReviewColumn')][1]/div[contains(@class,'ReviewContainer')][1]/div[contains(@class,'ReviewTextContainer')]");

        public DataObjects.FMP.Agencies.Recruiter.Admin.Recruiter GetRecruiterDetail()
        {
            var recruiterName = Wait.UntilElementVisible(RecruiterName).GetText().ToLowerInvariant();
            var recruiterAgencyName =  Wait.UntilElementVisible(RecruiterAgency).GetAttribute("alt");
            var specialties = Wait.UntilAllElementsLocated(RecruiterSpecialty).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
            ClickOnReadMoreLink();
            var recruiterAboutMeDetails = Wait.UntilElementVisible(RecruiterAboutMe).GetText().Replace("Read less", "").ToLowerInvariant();
            string phoneNumber = null;
            if (Wait.IsElementPresent(PhoneNumberText, 5))
            {
                phoneNumber = Wait.UntilElementVisible(PhoneNumberText).GetText();
            }
            return new DataObjects.FMP.Agencies.Recruiter.Admin.Recruiter()
            {
                RecruiterName = recruiterName,
                Specialty = specialties,
                RecruiterAgency = new Agency
                {
                    Name = recruiterAgencyName,
                },
                AboutMe = recruiterAboutMeDetails,
                PhoneNumber = phoneNumber
            };
        }
        public void ClickOnReadMoreLink()
        {
            if (Wait.IsElementPresent(ReadMoreLink, 5))
            {
                Wait.UntilElementClickable(ReadMoreLink).ClickOn();
            }
        }
        public string GetRecruiterProfilePhotoUrl()
        {
            return Wait.UntilElementVisible(RecruiterProfilePhoto).GetAttribute("src");
        }
        public void ClickOnReviewThisRecruiterButton()
        {
            Wait.UntilElementClickable(ReviewThisRecruiterButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public string GetRateAndReviewHeaderText()
        {
            return Wait.UntilElementVisible(RateAndReviewHeaderText).GetText();
        }
        public string GetLearnMoreAboutAgencyHref()
        {
            return Wait.UntilElementVisible(LearnMoreAboutAgencyLink).GetAttribute("href");
        }
        public string GetBrowseJobsHref()
        {
            return Wait.UntilElementVisible(BrowseJobsLink).GetAttribute("href");
        }
        public void ClickOnReviewRecruiterLink()
        {
            Wait.UntilElementClickable(ReviewRecruiterLink).ClickOn();
        }
        public void ClickOnHeaderRecruiterReview()
        {
            Wait.UntilElementClickable(RecruiterRating).ClickOn();
        }
        public bool IsReviewSectionOfProfileDisplayed()
        {
            return Wait.IsElementDisplayed(RecruiterRating, 5);
        }
        public bool IsStarRatingOfReviewSectionDisplayed()
        {
            return Wait.IsElementPresent(RecruiterStarRating, 5);
        }
        public bool IsReviewTextDisplayed()
        {
            return Wait.IsElementDisplayed(ReviewText, 5);
        }
        public string GetUserRoleTypeName()
        {
            return Wait.UntilElementVisible(UserRoleTypeName).GetText().ToLowerInvariant();
        }
        public string GetSelectedUserTypeText()
        {
            return Wait.UntilElementVisible(UserRoleTypeText).GetText().Replace("Reviewed", "").RemoveEndOfTheLineCharacter();
        }
        public string GetReviewDate()
        {
            return Wait.UntilElementVisible(ReviewDate).GetText().Replace("Reviewed", "").RemoveWhitespace();
        }
        public void ClickOnReviewRecruiterButton()
        {
            Wait.UntilElementClickable(ReviewRecruiterButton).ClickOn();
            WaitUntilFmpTextLoadingIndicatorInvisible();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public bool IsReviewRecruiterButtonDisabled()
        {
            return Wait.UntilElementExists(ReviewRecruiterButton).IsElementAttributeValueMatched("aria-disabled","true");
        }
        public string GetRecruiterAgencyName()
        {
            return Wait.UntilElementVisible(RecruiterAgency).GetAttribute("alt");
        }
        public string GetRecruiterName()
        {
            return Wait.UntilElementVisible(RecruiterName).GetText().ToLowerInvariant();
        }

        public string GetReviewMessage()
        {
            return Wait.UntilElementVisible(ReviewMessage).GetText();
        }

        //Respond On Review
        public string GetResponseTextFromFirstReview()
        {
            return Wait.UntilElementVisible(ResponseOnReviewTextFromFirstReview).GetText();
        }

        public bool IsReviewTextPresent()
        {
            return Wait.IsElementPresent(ReviewText, 5);
        }

        public void ClickOnEditAboutMeButton()
        {
            Wait.UntilElementClickable(EditAboutMeButton).ClickOn();
        }
    }
}
