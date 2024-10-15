using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Agencies.Recruiter.RateAndReview;
using UIAutomation.Enum.Recruiters;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter.RateAndReview
{
    internal class StepAboutMePo : FmpBasePo
    {
        public StepAboutMePo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By NextButton = By.XPath("//span[text()='Next']//parent::button");
        private static By UserTypeRadioButton(string userType) => By.XPath($"//label[contains(text(),'specific traveler')]//following-sibling::div//span[text()='{userType}']");
        private readonly By UserTypeRadioButtonInput = By.XPath("//span[text()='Client']//parent::label//input");
        private readonly By TravelerUserRadioButtonInput = By.XPath("//span[text()='Traveler']//parent::label//input");
        private readonly By OtherUserTypeTextBox = By.XPath("//input[@name='roleTypeOther']");
        private readonly By RecruiterTypeOccasionallyRadioButton = By.XPath("//label[contains(text(),'interacted with')]//following-sibling::div//span[contains(text(),'occasionally')]");
        private readonly By RecruiterTypeFewTimesRadioButton = By.XPath("//label[contains(text(),'interacted with')]//following-sibling::div//span[contains(text(),'each other')]");
        private readonly By RecruiterTypeRegularlyRadioButton = By.XPath("//label[contains(text(),'interacted with')]//following-sibling::div//span[contains(text(),'regularly')]");
        private readonly By RecruiterTypeJustOnceRadioButton = By.XPath("//label[contains(text(),'interacted with')]//following-sibling::div//span[contains(text(),'Once')]");
        private readonly By RecruiterTypeRadioButtonInput = By.XPath("//span[text()='4-9 times, we connect occasionally']//parent::label//input");
        private readonly By AboutMeProgressBar = By.XPath("//div[contains(@class,'MuiStep-completed')]//span[text()='ABOUT ME']");
        private readonly By LogOutButton = By.XPath("//span[text()='Log out']//parent::button");
        private readonly By TravalJobsDropdown = By.CssSelector("select#interactionTypeUId");

        // Validation message
        private readonly By ValidationMessageOfUserRoleTypeRadioButton = By.XPath("//label[contains(text(),'specific traveler')]//following-sibling::div//div[contains(@class,'Error')]");
        private readonly By ValidationMessageOfRecruiterInteractionRadioButton = By.XPath("//label[contains(text(),'interacted with')]//following-sibling::div//div[contains(@class,'Error')]");

        public void SelectUserTypeRadioButton(RateAndReviewBase describeUser)
        {
            if (describeUser.UserRoleType == RoleType.Client)
            {
                Driver.JavaScriptClickOn(Wait.UntilElementExists(UserTypeRadioButton(RoleType.Client.ToString())));
            }
            else
            {
                Wait.UntilElementClickable(UserTypeRadioButton(RoleType.Other.ToString())).ClickOn();
                Wait.UntilElementVisible(OtherUserTypeTextBox).EnterText(describeUser.OtherUserType);
            }
        }
        public void SelectTravelJobsDropdown(string numberOfTravelJobs)
        {
            Wait.UntilElementClickable(TravalJobsDropdown).SelectDropdownValueByText(numberOfTravelJobs, Driver);
        }
        public void SelectRecruiterTypeRadioButton(RateAndReviewBase interactionWithRecruiter)
        {
            switch (interactionWithRecruiter.InteractionWithRecruiter)
            {
                case InteractionType.SeveralTimes:
                    Wait.UntilElementClickable(RecruiterTypeOccasionallyRadioButton).ClickOn();
                    break;
                case InteractionType.JustOnce:
                    Wait.UntilElementClickable(RecruiterTypeJustOnceRadioButton).ClickOn();
                    break;
                case InteractionType.ManyTimes:
                    Wait.UntilElementClickable(RecruiterTypeRegularlyRadioButton).ClickOn();
                    break;
                case InteractionType.FewTimes:
                    Wait.UntilElementClickable(RecruiterTypeFewTimesRadioButton).ClickOn();
                    break;
                default:
                    Wait.UntilElementClickable(RecruiterTypeOccasionallyRadioButton).ClickOn();
                    break;

            }
        }

        public bool IsUserTypeRadioButtonSelected()
        {
            return Wait.UntilElementExists(UserTypeRadioButtonInput).IsElementSelected();
        }
        public bool IsUserTypeRadioButtonEnabled()
        {
            return Wait.IsElementEnabled(UserTypeRadioButtonInput, 5);
        }
        public bool IsRecruiterTypeRadioButtonSelected()
        {
            return Wait.UntilElementExists(RecruiterTypeRadioButtonInput).IsElementSelected();
        }
        public bool IsRecruiterTypeRadioButtonEnabled()
        {
            return Wait.IsElementEnabled(RecruiterTypeOccasionallyRadioButton, 5);
        }
        public bool IsOtherUserTypeFieldEnabled()
        {
            return Wait.IsElementEnabled(OtherUserTypeTextBox, 3);
        }
        public bool IsTravelerUserTypeFieldEnabled()
        {
            return Wait.IsElementEnabled(TravelerUserRadioButtonInput,3);
        }
        public bool IsAboutMeProgressBarFilled()
        {
            return Wait.IsElementPresent(AboutMeProgressBar, 5);
        }
        public void ClickOnNextButton()
        {
            Wait.UntilElementClickable(NextButton).ClickOn();
        }
        public string GetValidationMessageOfUserRoleTypeField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfUserRoleTypeRadioButton).GetText();
        }
        public string GetValidationMessageOfInteractionWithRecruiterField()
        {
            return Wait.UntilElementVisible(ValidationMessageOfRecruiterInteractionRadioButton).GetText();
        }
        public void ClickOnLogOutButton()
        {
            Wait.UntilElementClickable(LogOutButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
    }
}
