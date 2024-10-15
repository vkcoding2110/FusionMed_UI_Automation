using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Core.Common
{
    internal class HeaderPo : CoreBasePo
    {
        public HeaderPo(IWebDriver driver) : base(driver)
        {
        }
        
        private readonly By HamBurgerIcon = By.Id("menu_toggle");
        private readonly By ProfileName = By.ClassName("user-profile");
        private readonly By FindTextBox = By.Id("globalSearch");

        public void ClickOnHamBurgerMenu()
        {
            Wait.UntilElementClickable(HamBurgerIcon).ClickOn();
        }

        public string GetProfileName()
        {
            return Wait.UntilElementVisible(ProfileName).GetText();
        }

        public void EnterDataToFindTextBox(string data)
        {
            Wait.UntilElementClickable(FindTextBox).EnterText(data);
        }


    }
}
