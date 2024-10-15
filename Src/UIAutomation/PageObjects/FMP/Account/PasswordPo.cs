using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Account;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Account
{
    internal class PasswordPo : FmpBasePo
    {
        public PasswordPo(IWebDriver driver) : base(driver)
        {
        }

        //Progress Bar Gray Line 
        private readonly By FilledProgressBar = By.XPath("//div[@class='grayed-line']//div");

        //Password
        private readonly By SignUpPassword =
            By.XPath("//div[@class='form-label-group-validation']//div//input[@id='Password']");

        private readonly By PasswordCheerText =
            By.XPath(
                "//div[@class='form-label-group-validation']//div//label[contains(text(),'an awesome password!')]");

        private readonly By ConfirmPasswordCheerText =
            By.XPath("//div[@class='form-label-group-validation']//div//label[contains(text(),'good to go!')]");

        private readonly By SignUpConfirmPassword =
            By.XPath("//div[@class='form-label-group-validation']//div//input[@id='ConfirmPassword']");

        //Submit button
        private readonly By SubmitButton = By.XPath("//div[@class='button-container']//button[text()='Submit']");

        //Password Validation
        private readonly By PasswordValidationMessage = By.CssSelector("label#Password-error");
        private readonly By ConfirmPasswordValidationMessage = By.CssSelector("label#ConfirmPassword-error");

        private readonly By BackButton = By.CssSelector("button[class='back-arrow cancel']");

        private readonly By CancelButton = By.CssSelector("div button[class='btn btn-secondary cancel']");

        private readonly By CloseIcon = By.CssSelector("button[class='close-icon-wrapper cancel']");

        public bool IsFilledProgressBarDisplayed()
        {
            return Wait.IsElementPresent(FilledProgressBar, 10);
        }

        public void EnterPassword(SignUp signUp)
        {
            Wait.UntilElementVisible(SignUpPassword).EnterText(signUp.Password);
        }

        public void EnterConfirmPassword(SignUp signUp)
        {
            Wait.UntilElementVisible(SignUpConfirmPassword).EnterText(signUp.Password);
        }

        public bool IsPasswordCheerTextDisplayed()
        {
            return Wait.IsElementPresent(PasswordCheerText);
        }

        public bool IsConfirmPasswordCheerTextDisplayed()
        {
            return Wait.IsElementPresent(ConfirmPasswordCheerText);
        }

        public void ClickOnSubmitButton()
        {
            Wait.UntilElementVisible(SubmitButton, 10);
            Wait.UntilElementClickable(SubmitButton).ClickOn();
        }

        public void FillFormAndSubmit(SignUp signUp)
        {
            Wait.UntilElementVisible(SignUpPassword).EnterText(signUp.Password);
            Wait.UntilElementVisible(SignUpConfirmPassword).EnterText(signUp.Password);
            Wait.HardWait(2000);
            Wait.UntilElementClickable(SubmitButton).ClickOn();
        }

        public string GetPasswordValidationMessage()
        {
            return Wait.UntilElementVisible(PasswordValidationMessage).GetText();
        }

        public string GetConfirmPasswordValidationMessage()
        {
            return Wait.UntilElementVisible(ConfirmPasswordValidationMessage).GetText();
        }

        public string GetValidationMessageConfirmPassword()
        {
            return Wait.UntilElementVisible(ConfirmPasswordValidationMessage).GetText();
        }

        public void ClickOnBackButton()
        {
            Wait.UntilElementClickable(BackButton).ClickOn();
            Wait.WaitTillElementCountIsLessThan(BackButton, 1);
        }

        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
            Wait.WaitTillElementCountIsLessThan(CloseIcon, 1);
        }
    }
}