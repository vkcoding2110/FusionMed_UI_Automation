using OpenQA.Selenium;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.DataObjects.FMP.Account;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Account
{
    internal class FmpLoginPo : FmpBasePo
    {

        public FmpLoginPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By LoginPageHeader = By.XPath("//h2[@class='page-header']");
        private readonly By UsernameTextBox = By.CssSelector("input#Username");
        private readonly By PasswordTextBox = By.CssSelector("input#Password");
        private readonly By SubmitButton = By.XPath("//button[text()='Submit']");
        private readonly By ContinueButton = By.XPath("//button[text()='Continue']");
        private readonly By CloseButton = By.XPath("//button[contains(@class,'close-icon-wrapper')]");
        private readonly By ForgetPasswordLink = By.XPath("//a[text()='Forgot Password?']");
        private readonly By SignUpLink = By.XPath("//a[text()=' Sign up']");
        private readonly By BackButton = By.CssSelector("button[class='back-arrow cancel']");
        private readonly By CancelButton = By.XPath("//button[text()='cancel']");
        private readonly By ValidationMessageForEmailField = By.CssSelector("label#Username-error");
        private readonly By ValidationMessageForPasswordField = By.CssSelector("label#Password-error");
        private readonly By ValidationMessageForInValidData = By.XPath("//li[text()='Invalid username or password']");

        public string GetLoginPageHeader()
        {
            Wait.UntilElementVisible(LoginPageHeader);
            return Wait.UntilElementVisible(LoginPageHeader).GetText();
        }

        public void LoginToApplication(Login login)
        {
            Wait.UntilElementVisible(UsernameTextBox).EnterText(login.Email);
            Wait.UntilElementClickable(ContinueButton).ClickOn();
            Wait.WaitTillElementCountIsLessThan(ContinueButton, 1);
            Wait.UntilElementVisible(PasswordTextBox).EnterText(login.Password);
            Wait.UntilElementClickable(SubmitButton).ClickOn();
        }
        public void LoginToApplication(SignUp signUp)
        {
            Wait.UntilElementClickable(UsernameTextBox).EnterText(signUp.Email);
            Wait.UntilElementClickable(ContinueButton).ClickOn();
            Wait.WaitTillElementCountIsLessThan(ContinueButton, 1);
            Wait.UntilElementClickable(PasswordTextBox).EnterText(signUp.Password);
            Wait.UntilElementClickable(SubmitButton).ClickOn();
            Wait.WaitTillElementCountIsLessThan(SubmitButton, 1);
        }

        public void EnterEmailIdAndContinue(Login login)
        {
            Wait.UntilElementClickable(UsernameTextBox).EnterText(login.Email);
            Driver.JavaScriptClickOn(ContinueButton);
            Wait.UntilElementInVisible(ContinueButton, 5);
        }

        public void ClickOnCloseButton()
        {
            Wait.UntilElementClickable(CloseButton).ClickOn();
            Wait.UntilElementInVisible(CloseButton);
        }

        public void ClickOnForgetPasswordLink()
        {
            Wait.UntilElementClickable(ForgetPasswordLink).ClickOn();
            Wait.WaitTillElementCountIsLessThan(ForgetPasswordLink, 1);
        }

        public void ClickOnSignUpLink()
        {
            Wait.UntilElementClickable(SignUpLink).ClickOn();
            Wait.UntilElementInVisible(SignUpLink);
        }

        public void ClickOnBackButton()
        {
            Driver.JavaScriptClickOn(BackButton);
            Wait.WaitTillElementCountIsLessThan(BackButton, 1);
        }

        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
            Wait.WaitTillElementCountIsLessThan(CancelButton, 1);
        }
        public bool IsUserNameInputBoxPresent()
        {
            return Wait.IsElementPresent(UsernameTextBox);
        }
        public void ClickOnContinueButton()
        {
            Wait.UntilElementClickable(ContinueButton).ClickOn();
        }
        public string GetValidationMessageForEmailField()
        {
            return Wait.UntilElementVisible(ValidationMessageForEmailField).GetText();
        }
        public void EnterEmail(string email)
        {
            Wait.UntilElementClickable(UsernameTextBox).EnterText(email);
        }
        public void ClickOnSubmitButton()
        {
            Wait.UntilElementClickable(SubmitButton).ClickOn();
            Wait.WaitTillElementCountIsLessThan(SubmitButton, 1);
        }
        public string GetValidationMessageForPasswordField()
        {
            return Wait.UntilElementVisible(ValidationMessageForPasswordField).GetText();
        }
        public void EnterInValidPassword()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            Wait.UntilElementVisible(PasswordTextBox).EnterText("Password_Test" + randomNumber);
        }
        public bool IsValidationMessageDisplayedForInValidData()
        {
            return Wait.IsElementPresent(ValidationMessageForInValidData);
        }
        public void EnterIncorrectPasswordMultipleTimes()
        {
            for (var i = 0; i < 5; i++)
            {
                if (Wait.IsElementPresent(SubmitButton))
                {
                    Wait.UntilElementVisible(PasswordTextBox).EnterText(new CSharpHelpers().GenerateRandomNumber().ToString());
                    Wait.UntilElementClickable(SubmitButton).ClickOn();
                    Wait.HardWait(2000);
                }
                else
                {
                    break;
                }
            }
        }

    }
}

