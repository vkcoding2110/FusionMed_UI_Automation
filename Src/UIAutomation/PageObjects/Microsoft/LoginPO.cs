using System;
using OpenQA.Selenium;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Microsoft
{
    internal class LoginPo : BasePo
    {
        public LoginPo(IWebDriver driver) : base(driver)
        {
        }
        
        private readonly By EmailTextBox = By.Name("loginfmt");
        private readonly By NextButton = By.CssSelector("input[value='Next']");
        private readonly By PasswordTextBox = By.Name("passwd");
        private readonly By SignInButton = By.CssSelector("input[value='Sign in']");
        private readonly By StaySignedInOptionNo = By.CssSelector("input[value='No']");
        private readonly By TryingToSignIn = By.XPath("//div[text()='Trying to sign you in']");

        public void LoginToApplication(Login login)
        {
            try
            {
                Wait.UntilElementVisible(TryingToSignIn,3);
                Wait.UntilElementInVisible(TryingToSignIn);
            }
            catch (Exception)
            {
                //Nothing
            }

            Wait.UntilElementVisible(EmailTextBox);
            Wait.HardWait(5000);
            Wait.UntilElementClickable(EmailTextBox).EnterText(login.Email);

            if (Wait.WaitIncaseElementClickable(NextButton, 10) != null)
            {
                Wait.UntilElementClickable(NextButton).ClickOn();
            }

            try
            {
                Wait.UntilElementVisible(PasswordTextBox);
                Wait.UntilElementClickable(PasswordTextBox).EnterText(login.Password);
            }
            catch(Exception)
            {
                Wait.HardWait(5000);
                Wait.UntilElementClickable(PasswordTextBox).EnterText(login.Password);
            }
            Wait.UntilElementClickable(SignInButton).ClickOn();

            if(Wait.WaitIncaseElementClickable(StaySignedInOptionNo,10)!=null)
            {
                Wait.UntilElementClickable(StaySignedInOptionNo).ClickOn();
            }
        }


    }
}
