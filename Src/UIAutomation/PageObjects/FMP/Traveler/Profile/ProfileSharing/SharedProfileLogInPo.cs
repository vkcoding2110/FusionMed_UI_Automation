using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.ProfileSharing
{
    internal class SharedProfileLogInPo : FmpBasePo
    {
        public SharedProfileLogInPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By SharedProfileLogInSignUpHeaderText = By.CssSelector("div[class*='SharedProfilePage'] h1[class*='HeaderStyled']");
        private readonly By CreateTravelerProfileButton = By.XPath("//div[contains(@class,'SharedProfileContent')]//button/span[contains(text(),'Traveler')]");
        private readonly By LoginButton = By.XPath("//button[contains(@class,'ButtonStyled')]/span[text()='Log In']");
        private readonly By SignUpAsAGuestLink = By.XPath("//div[contains(@class,'SharedProfileContent')]//span[text()='Sign up here']/parent::a");

        public string GetSharedProfileLogInSignUpHeaderText()
        {
            return Wait.UntilElementVisible(SharedProfileLogInSignUpHeaderText).GetText();
        }

        public void ClickOnCreateTravelerProfileButton()
        {
            Wait.UntilElementClickable(CreateTravelerProfileButton).ClickOn();
        }

        public void ClickOnMyProfileSharingLoginButton()
        {
            Wait.UntilElementClickable(LoginButton).ClickOn();
        }

        public void ClickOnSignUpAsAGuestLink()
        {
            Wait.UntilElementClickable(SignUpAsAGuestLink).ClickOn();
        }
    }
}
