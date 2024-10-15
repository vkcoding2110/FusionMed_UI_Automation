using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.YopMail
{
    internal class EmailPo : BasePo
    {
        public EmailPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By EmailInput = By.CssSelector("div#ycptcpt input.ycptinput");
        private readonly By CheckBoxButton = By.CssSelector("div#refreshbut button.md");

        public void EnterEmailAddress(string email)
        {
            Wait.UntilElementVisible(EmailInput, 20).EnterText(email.Replace("@yopmail.com", ""));
            Wait.UntilElementClickable(CheckBoxButton).ClickOn();
            Wait.HardWait(3000);
        }
    }
}