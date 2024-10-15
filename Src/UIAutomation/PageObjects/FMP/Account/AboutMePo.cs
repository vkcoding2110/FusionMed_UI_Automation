using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Account;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Account
{
    internal class AboutMePo : FmpBasePo
    {
        public AboutMePo(IWebDriver driver) : base(driver)
        {
        }


        //Add 'About Me' sign up data 
        private readonly By SignUpFirstNameTextBox = By.CssSelector("input#FirstName");
        private readonly By SignUpLastNameTextBox = By.CssSelector("input#LastName");
        private readonly By SignUpEmailTextBox = By.XPath("//input[@placeholder='Email']");
        private readonly By SignUpPhoneTextBox = By.XPath("//input[@placeholder='Phone']");

        //Continue button 
        private readonly By SignUpContinueButton = By.XPath("//div[@class='button-container']//button[@type='submit'][text()='Continue']");

        //Validation Message
        private readonly By ValidationMessage = By.CssSelector("label[class='error custom-error']");

        private readonly By UnFilledProgressBar = By.CssSelector("div[class='register-progress-bar-about-me-wrapper']");

        private readonly By CancelButton = By.CssSelector("div button[class='btn btn-secondary cancel']");

        private readonly By CloseIcon = By.CssSelector("button[class='close-icon-wrapper cancel'] ");

        public void AddDataAboutMeInSignUpForm(SignUp signUp)
        {
            Wait.UntilElementVisible(SignUpFirstNameTextBox).EnterText(signUp.FirstName);
            Wait.UntilElementVisible(SignUpLastNameTextBox).EnterText(signUp.LastName);
            Wait.UntilElementVisible(SignUpEmailTextBox).EnterText(signUp.Email);
            if (Wait.IsElementPresent(SignUpPhoneTextBox,3))
            {
                Wait.UntilElementVisible(SignUpPhoneTextBox).EnterText(signUp.Phone);
            }
            Wait.UntilElementClickable(SignUpContinueButton).ClickOn();
            Wait.UntilElementInVisible(SignUpContinueButton);
        }

        public void ClickOnSignUpContinueButton()
        {
            Wait.UntilElementVisible(SignUpContinueButton, 10);
            Wait.UntilElementClickable(SignUpContinueButton).ClickOn();
        }

        public string GetValidationMessage()
        {
            return Wait.UntilElementVisible(ValidationMessage).GetText();
        }

        public bool IsUnFilledProgressBarDisplayed()
        {
            return Wait.IsElementPresent(UnFilledProgressBar, 20);
        }

        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
            Wait.WaitTillElementCountIsLessThan(CancelButton, 1);
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
            Wait.WaitTillElementCountIsLessThan(CloseIcon, 1);
        }

        public void AddGuestUserDataInSignUpForm(SignUp signUp)
        {
            Wait.UntilElementVisible(SignUpFirstNameTextBox).EnterText(signUp.FirstName);
            Wait.UntilElementVisible(SignUpLastNameTextBox).EnterText(signUp.LastName);
            Wait.UntilElementClickable(SignUpContinueButton).ClickOn();
            Wait.UntilElementInVisible(SignUpContinueButton);
        }

        public string GetEmail()
        {
            return Wait.UntilElementVisible(SignUpEmailTextBox).GetAttribute("value");
        }
    }
}
