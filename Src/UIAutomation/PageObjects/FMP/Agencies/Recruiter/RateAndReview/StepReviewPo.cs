using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Agencies.Recruiter.RateAndReview;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter.RateAndReview
{
    internal class StepReviewPo : FmpBasePo
    {
        public StepReviewPo(IWebDriver driver) : base(driver)
        {
        }
        private static By RateAndReviewScale(int scale) => By.XPath($"//span[@data-index='{scale}']");
        private readonly By ReviewTextArea = By.XPath("//textarea[@name='reviewText']");
        private readonly By ScaleAndReviewProgressBar = By.XPath("//div[contains(@class,'MuiStep-completed')]//span[text()='SCALE & REVIEW']");

        // Validation message
        private readonly By ValidationMessageOfReviewScale = By.XPath("//span[contains(@class,'MuiSlider')]//following-sibling::p");
        private readonly By ValidationMessageOfReviewMessageTextarea = By.XPath("//label[contains(text(),'Write a review')]//following-sibling::p");

        public void ScrollRateAndReviewScale(RateAndReviewBase scale)
        {
            Wait.UntilElementClickable(RateAndReviewScale(scale.ReviewScale)).ClickOn();
        }
        public void AddReviewForRecruiter(string review)
        {
            Wait.UntilElementClickable(ReviewTextArea).Click();
            Wait.UntilElementVisible(ReviewTextArea).EnterText(review,true);
        }
        public bool IsScaleAndReviewProgressBarFilled()
        {
            return Wait.IsElementPresent(ScaleAndReviewProgressBar, 5);
        }
        public string GetValidationMessageOfReviewScale()
        {
            return Wait.UntilElementVisible(ValidationMessageOfReviewScale).GetText();
        }
        public string GetValidationMessageOfReviewMessageTextarea()
        {
            return Wait.UntilElementVisible(ValidationMessageOfReviewMessageTextarea).GetText();
        }

    }
}
