using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Home
{
    internal class HomePagePo : FmsBasePo
    {
        public HomePagePo(IWebDriver driver) : base(driver)
        {
        }


        private readonly By JobsOrRecruiterMenuHeaderText = By.XPath("//div[contains(@class,'browsestyles__BrowseHeader')]//div[2]");
        private readonly By JobsOrRecruiterMenuCloseButton = By.XPath("//div[contains(@class,'browsestyles__BrowseHeader')]//div[3]/button");
        private readonly By JobsOrRecruiterMenuList = By.XPath("//button[contains(@class,'ListItem-sc')]//span[contains(@class,'ListItemText')]");

        //Drawer Menu
        private readonly By MenuButton = By.CssSelector("button[aria-label='Open main menu']");
        private readonly By MenuHeader = By.CssSelector("div[class*='mainMenustyles__NavigationHeader'] span");
        private readonly By CloseMenuButton = By.CssSelector("div[class*='mainMenustyles__NavigationHeader'] button");
        private readonly By MenuList = By.XPath("//div[contains(@class,'mainMenustyles__NavLinkMain')]");

        //Device Elements
        private readonly By MenuButtonAndroid = By.XPath("//android.widget.Button[contains(@text,'menu')]");

        //Apply Now
        private readonly By ApplyNowButtonInput = By.CssSelector("a[class*='headerstyles__QuickApplyButton'] span");

        private readonly By ReviewPopup = By.CssSelector("div[class*='sentry-error-embed']");
        private readonly By ReviewPopupCloseButton = By.XPath("//div[contains(@class,'submit clearfix')]//a[text()='Close']");


        //Blogs
        private static By NthBlogCard(int card) => By.XPath($"//div[contains(@class,'BlogCard')][{card}]//div[contains(@class,'CardText')]");

        private static By FirsCardText(int card) => By.XPath($"//div[contains(@class,'BlogCard')][{card}]//p");
        private readonly By BlogHeaderText = By.CssSelector("span#hs_cos_wrapper_name");

        private static By MenuListItem(string item) => By.XPath($"//div[contains(@class,'mainMenustyles__NavLinkMain')][text()='{item}']");

        //View AllJobs
        private readonly By ViewAllJobsLabel = By.XPath("//a[contains(@class,'ViewAll')][text()='View all jobs']");

        //Device elements
        private readonly By ExploreMenuApplyNowListItemDevice = By.XPath("//a/div[text()='Apply Now']");


        public string GetJobsOrRecruiterMenuHeaderText()
        {
            return Wait.UntilElementClickable(JobsOrRecruiterMenuHeaderText).GetText();
        }

        public IList<string> GetJobsOrRecruiterMenuListItems()
        {
            return Wait.UntilAllElementsLocated(JobsOrRecruiterMenuList).Select(e => e.GetText()).ToList();
        }

        public void ClickOnJobsOrRecruiterMenuCloseButton()
        {
            Wait.UntilElementClickable(JobsOrRecruiterMenuCloseButton).ClickOn();
            Wait.UntilElementInVisible(JobsOrRecruiterMenuCloseButton);
        }

        public bool IsJobsOrRecruiterMenuPresent()
        {
            return Wait.IsElementPresent(JobsOrRecruiterMenuHeaderText, 3);
        }

        // Menu
        public void ClickOnMenuButton()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                if (BaseTest.PlatformName.Equals(PlatformName.Android))
                {
                    Driver.NativeAppClickOn(MenuButtonAndroid);
                }
                else
                {
                    Driver.JavaScriptClickOn(Wait.UntilElementExists(MenuButton));
                }
            }
            else
            {
                Wait.UntilElementClickable(MenuButton).ClickOn();
            }
        }

        public string GetMenuHeaderText()
        {
            return Wait.UntilElementVisible(MenuHeader).GetText();
        }

        public IList<string> GetMenuListItems()
        {
            return Wait.UntilAllElementsLocated(MenuList).Select(e => e.GetText().Replace("Fusion Medical Staffing", "")).ToList();
        }

        public void ClickOnCloseMenuButton()
        {
            Wait.UntilElementClickable(CloseMenuButton).ClickOn();
            Wait.UntilElementInVisible(CloseMenuButton);
        }

        public bool IsMenuPresent()
        {
            return Wait.IsElementPresent(MenuHeader, 8);
        }

        public void ClickOnNthMenuListItem(string item)
        {
            Driver.JavaScriptClickOn(Wait.UntilElementExists(MenuListItem(item)));
        }

        //Apply Now
        public void ClickOnApplyNowButton()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                new HomePagePo(Driver).ClickOnMenuButton();
                Wait.UntilElementVisible(ExploreMenuApplyNowListItemDevice).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(ApplyNowButtonInput).ClickOn();
            }
        }

        public void CloseReviewPopupIfPresent()
        {
            var popup = Wait.IsElementPresent(ReviewPopup);
            if (!popup) return;
            Wait.HardWait(3000);
            Wait.UntilElementClickable(ReviewPopupCloseButton).ClickOn();
            Wait.UntilElementInVisible(ReviewPopup);
        }

        //Blogs
        public void ClickOnNthBlogCard(int card)
        {
            Wait.UntilElementClickable(NthBlogCard(card)).ClickOn();
            Wait.HardWait(3000);
        }

        public string GetBlogHeaderText()
        {
            return Wait.UntilElementVisible(BlogHeaderText).GetText();
        }

        public string GetBlogTitle(int blog)
        {
            return Wait.UntilElementClickable(FirsCardText(blog)).GetText();
        }

        //View AllJobs
        public void ClickOnViewAllJobsLabel()
        {
            Wait.UntilElementClickable(ViewAllJobsLabel).ClickOn();
            Wait.UntilElementInVisible(ViewAllJobsLabel);
        }
    }
}
