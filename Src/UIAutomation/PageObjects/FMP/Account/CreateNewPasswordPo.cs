using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Account
{
    internal class CreateNewPasswordPo : FmpBasePo
    {
        public CreateNewPasswordPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By CreateNewPasswordHeaderText = By.XPath("//div[@class='reset-password-container']//h5");

        private readonly By CreateNewPassword =
            By.XPath("//div[@class='form-label-group-validation']//div//input[@id='Password']");

        private readonly By CreateNewConfirmPassword =
            By.XPath("//div[@class='form-label-group-validation']//div//input[@id='ConfirmPassword']");

        private readonly By SubmitButton = By.XPath("//div[@class='button-container']//button[text()='Submit']");

        private readonly By InvalidConfirmPasswordErrorMessage =
            By.CssSelector("div[class='form-label-group-validation'] div label#ConfirmPassword-error");

        //Validation Message
        private readonly By ErrorMessage = By.XPath("//div[contains(@class,'validation-summary-errors')]//li");

        //Cheer Text Password and Confirm Password
        private readonly By CreateNewPasswordCheerText =
            By.XPath(
                "//div[@class='form-label-group-validation']//div//label[contains(text(),'an awesome password!')]");

        private readonly By CreateNewConfirmPasswordCheerText =
            By.XPath("//div[@class='form-label-group-validation']//div//label[contains(text(),'good to go!')]");



        public string GetCreateNewPasswordHeaderText()
        {
            return Wait.UntilElementVisible(CreateNewPasswordHeaderText).GetText();
        }

        public void ClickOnCreateNewPasswordSubmitButton()
        {
            Wait.UntilElementVisible(SubmitButton, 10);
            Wait.UntilElementClickable(SubmitButton).ClickOn();
        }

        public string GetErrorMessage()
        {
            return Wait.UntilElementVisible(ErrorMessage).GetText();
        }

        public void EnterPassword(string password)
        {
            Wait.UntilElementVisible(CreateNewPassword).EnterText(password);
        }

        public void EnterInvalidConfirmPassword(string invalidConfirmPInvalidation)
        {
            Wait.UntilElementVisible(CreateNewConfirmPassword).EnterText(invalidConfirmPInvalidation);
        }

        public void EnterNewPasswordAndConfirmPassword(string newPassword)
        {
            Wait.UntilElementVisible(CreateNewPassword, 30).EnterText(newPassword);
            Wait.UntilElementVisible(CreateNewConfirmPassword, 30).EnterText(newPassword);
        }

        public string GetInvalidConfirmPasswordErrorMessage()
        {
            return Wait.UntilElementVisible(InvalidConfirmPasswordErrorMessage).GetText();
        }

        public bool IsCreateNewPasswordCheerTextDisplayed()
        {
            return Wait.IsElementPresent(CreateNewPasswordCheerText);
        }

        public void ClickOnCreateNewConfirmPassword(string newConfirmPReconfirmation)
        {
            Wait.UntilElementVisible(CreateNewConfirmPassword).EnterText(newConfirmPReconfirmation);
        }

        public bool IsConfirmPasswordCheerTextDisplayed()
        {
            return Wait.IsElementPresent(CreateNewConfirmPasswordCheerText);
        }
    }
}
