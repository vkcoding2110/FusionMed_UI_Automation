using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter.RateAndReview
{
    internal class StepSuccessPo : FmpBasePo
    {
        public StepSuccessPo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By PreviewProgressBar = By.XPath("//div[contains(@class,'MuiStep-completed')]//span[text()='PREVIEW']");
        private readonly By SubmitReviewButton = By.XPath("//span[text()='Submit Review']//parent::button");
        private readonly By EditReviewButton = By.XPath("//span[text()='Edit Review']//parent::button");
        private readonly By SelectedRecruiterTypeText = By.XPath("//p[contains(@class,'PreviewSubHeader')]");
        private readonly By ViewProfileButton = By.XPath("//button[contains(@title,'Profile')]");
        private readonly By ReviewDate = By.XPath("//p[contains(@class,'PreviewDate')]");
        private readonly By OverAllRatingOfRecruiter = By.XPath("//span[contains(@class,'RecruiterReadOnlyRatings')]");
        private readonly By UserName = By.XPath("//p[contains(@class,'PreviewHeader')]");
        private readonly By PreviewYourReviewMessage = By.XPath("//p[contains(text(),'Preview your review')]");
        private readonly By ReviewMessage = By.XPath("//p[contains(@class,'PreviewCopy')]");
        private readonly By ThankYouMessage = By.XPath("//p[contains(text(),'Thank you for sharing your feedback')]");
        
        //Validation message
        private readonly By InappropriateContentMessage = By.XPath("//div[contains(@class,'ReviewPageContent')]//p[contains(@class,'PreviewStatusCopy')]");

        public bool IsPreviewProgressBarFilled()
        {
            return Wait.IsElementPresent(PreviewProgressBar, 5);
        }
        public void ClickOnSubmitReviewButton()
        {
            Driver.JavaScriptClickOn(SubmitReviewButton);
        }
        public void ClickOnEditReviewButton()
        {
            Wait.UntilElementClickable(EditReviewButton).ClickOn();
        }
        public void ClickOnViewProfileButton()
        {
            Wait.UntilElementClickable(ViewProfileButton).ClickOn();
        }
        public string GetSelectedDetailOfRecruiter()
        {
            return Wait.UntilElementVisible(SelectedRecruiterTypeText).GetText();
        }
        public string GetReviewDate()
        {
            return Wait.UntilElementVisible(ReviewDate).GetText().Replace("Reviewed", "").RemoveWhitespace();
        }
        public string GetOverAllRatingStar()
        {
            return Wait.UntilElementVisible(OverAllRatingOfRecruiter).GetAttribute("aria-label").Replace("Stars", "").RemoveWhitespace();
        }
        public string GetUserName()
        {
            return Wait.UntilElementVisible(UserName).GetText().ToLowerInvariant();
        }
        public string GetInappropriateContentMessage()
        {
            return Wait.UntilElementVisible(InappropriateContentMessage,5).GetText();
        }
        public bool IsPreviewYourReviewMessageDisplayed()
        {
            return Wait.IsElementPresent(PreviewYourReviewMessage, 5);
        }

        public bool IsThankYouMessageDisplayed()
        {
            return Wait.IsElementPresent(ThankYouMessage, 5);
        }

        public string GetReviewMessage()
        {
            return Wait.UntilElementVisible(ReviewMessage).GetText();
        }
    }
}
