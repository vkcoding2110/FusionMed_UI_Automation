using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMP.BrowseAll;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Header
{
    internal class HeaderPo : FmpBasePo
    {
        public HeaderPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By LogInButton = By.CssSelector("button[class*='LoginButton'] span");
        private readonly By BrowseAllButton = By.XPath("//button/span[text()='Browse All']");
        private readonly By TravelersLink = By.XPath("//div[contains(@class,'NavSection')]//a//span[text()='For Travelers']");
        private readonly By AgenciesLink = By.XPath("//a[@href='/agencies/']/span[text()='For Partners']");
        private readonly By BlogLink = By.XPath("//a/span[text()='Blog']");
        private readonly By BlogHref = By.XPath("//span[text()='Blog']/parent::a");
        private readonly By JoinUsButton = By.CssSelector("button[class*='JoinUsButton']");
        private readonly By LoggedInUserName = By.CssSelector("button[class*='LoginBadge'] div");

        // Profile badge
        private readonly By ProfileIcon = By.XPath("//div[contains(@class,'AvatarContainer')]");
        
        //Recruiter, Agency, System Admin 
        private readonly By LogInBadge = By.XPath("//div[contains(@class,'ActionSection')]//button[contains(@class,'LoginBadge')]");

        //Device Elements
        private readonly By MenuIconDevice = By.XPath("//button[contains(@class,'IconButtonStyled') and @aria-label='Open main menu']");
        private readonly By MenuHeaderText = By.XPath("//div[contains(@class,'MenuHeader')]/div[text()='Menu']");
        private readonly By LoginButtonDevice = By.XPath("//span[text()='Log In']/parent::button[contains(@class,'containedPrimary')]");
        private readonly By BrowseAllButtonDevice = By.XPath("//label[text()='Browse All']/parent::button");
        private readonly By TravelersButtonDevice = By.XPath("//label[text()='For Travelers']/parent::button");
        private readonly By AgenciesButtonDevice = By.XPath("//label[text()='For Partners']//parent::button");
        private readonly By BlogButtonDevice = By.XPath("//label[text()='Blog']/parent::a");

        //Device Methods
        public void ClickOnMenuIcon()
        {
            if (BaseTest.PlatformName == PlatformName.Android)
            {
                Wait.UntilElementClickable(MenuIconDevice).ClickOn();
                Wait.UntilElementVisible(MenuHeaderText);
            }
            else
            {
                for (var i = 1; i <= 5; i++)
                {
                    Wait.UntilElementClickable(MenuIconDevice).ClickOn();
                    if (Wait.IsElementPresent(MenuHeaderText, 2))
                    {
                        break;
                    }
                }
            }
        }
        public void ClickOnLogInButton()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                ClickOnMenuIcon();
                Wait.UntilElementVisible(LoginButtonDevice);
                Wait.UntilElementClickable(LoginButtonDevice).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(LogInButton).ClickOn();
            }
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnBrowseAllButton()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                ClickOnMenuIcon();
                Driver.JavaScriptClickOn(BrowseAllButtonDevice);
            }
            else
            {
                Wait.UntilElementVisible(BrowseAllButton);
                Driver.JavaScriptClickOn(Wait.UntilElementExists(BrowseAllButton));
            }
            new ExploreMenuPo(Driver).WaitUntilExploreHeaderTextVisible();
        }

        public void ClickOnTravelersLink()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                ClickOnMenuIcon();
                Wait.UntilElementVisible(TravelersButtonDevice);
                Wait.UntilElementClickable(TravelersButtonDevice).ClickOn();
                Wait.UntilElementInVisible(TravelersButtonDevice);
            }
            else
            {
                Wait.UntilElementClickable(TravelersLink).ClickOn();
            }

        }

        public void ClickOnAgenciesLink()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                ClickOnMenuIcon();
                Wait.UntilElementVisible(AgenciesButtonDevice).ClickOn();
                Wait.UntilElementInVisible(AgenciesButtonDevice);
            }
            else
            {
                Wait.UntilElementClickable(AgenciesLink).ClickOn();
            }

        }

        public string GetBlogHref()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                ClickOnMenuIcon();
                return Wait.UntilElementClickable(BlogButtonDevice).GetAttribute("href");
            }
            else
            {
                Wait.UntilElementVisible(BlogLink);
                return Wait.UntilElementClickable(BlogHref).GetAttribute("href");
            }
        }

        public void ClickOnJoinUsButton()
        {
            Wait.UntilElementClickable(JoinUsButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public string GetUserName()
        {
            return Wait.UntilElementVisible(LoggedInUserName).GetText().Replace("\r", " ").Replace("\n", "");
        }
        //Profile Badge
        public void ClickOnProfileIcon()
        {
            if (BaseTest.PlatformName == PlatformName.Web && BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Safari))
            {
                Driver.JavaScriptClickOn(ProfileIcon);
            }
            else
            {
                Wait.UntilElementClickable(ProfileIcon).ClickOn();
            }
            Wait.HardWait(2000);
        }
        public bool IsLogInButtonDisplayed()
        {
            return Wait.IsElementPresent(LogInButton,5);
        }

        public void ClickOnLogInBadge()
        {
            Wait.UntilElementClickable(LogInBadge).ClickOn();
        }
    }
}
