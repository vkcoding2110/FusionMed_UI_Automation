using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler
{

    internal class TravelersBrandingPO : FmpBasePo
    {
        public TravelersBrandingPO(IWebDriver driver) : base(driver)
        {
        }

        private readonly By TravelersHeaderText = By.CssSelector("div[class*='HeaderMessage'] h1");
        private readonly By GetStartedButton = By.XPath("//div[contains(@class,'HeaderMessage')]/a");
        private readonly By StartYourJobSearch = By.XPath("//a[contains(@class,'ButtonStyled')]/span[text()='Start Your Job Search']");
        private readonly By LetsDoIt = By.XPath("//div[contains(@class,'Description')]/following-sibling::a");

        public string GetTravelersPageHeaderText()
        {
            return Wait.UntilElementVisible(TravelersHeaderText).GetText();
        }

        public void ClickOnGetStartedButton()
        {
            Wait.UntilElementVisible(GetStartedButton,30);
            Wait.UntilElementClickable(GetStartedButton).ClickOn();
        }

        public void ClickOnStartYourJobSearchButton()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(GetStartedButton));
            Driver.JavaScriptClickOn(Wait.UntilElementExists(StartYourJobSearch));
        }

        public void ClickOnLetsDoItButton()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(StartYourJobSearch));
            Driver.JavaScriptClickOn(Wait.UntilElementExists(LetsDoIt));
        }
    }
}

