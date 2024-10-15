using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Account
{
    internal class SignUpPo : FmpBasePo
    {
        public SignUpPo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By SignUpPageHeader = By.CssSelector(".page-header");

        public string GetSignUpPageHeader()
        {
            return Wait.UntilElementVisible(SignUpPageHeader).GetText();
        }
    }
}
