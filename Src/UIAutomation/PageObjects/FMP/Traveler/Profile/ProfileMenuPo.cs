using System;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile
{
    internal class ProfileMenuPo : FmpBasePo
    {
        public ProfileMenuPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By ProfileMenuItem = By.XPath("//h6[text()='Profile']");
        private readonly By JobPreferenceMenuItem = By.XPath("//h6[text()='Job Preferences']//parent::div");
        private readonly By LogOutButton = By.XPath("//div//h6[text()='Log out']");
        private readonly By CloseButton = By.CssSelector("div[class*='AccountHeader'] button");
        private readonly By RecentlyViewedJobText = By.CssSelector("h6[class*='ViewedItemLabel']");
        private readonly By ClearAllRecentlyViewedJob = By.XPath("//div[contains(@class,'RecentlyViewedClearButton')]/h6[text()='Clear all']");
        private readonly By ProfileArrow = By.CssSelector("div[class*='ArrowContainer'] svg");
        private readonly By RecentlyViewMenuItem = By.CssSelector("div[class*='AccordionSummaryStyled']");
        private readonly By JobApplicationText = By.XPath("//div[contains(@class,'AccountNavItem')]//div//h6[text()='Job Applications']");

        //Recruiter, Agency, System Admin 
        private static By AccountNavMenuItem(string accountNavText) => By.XPath($"//div[contains(@class,'AccountNavItem')]//div//h6[text()='{accountNavText}']");

        //assert
        private readonly By UserFirstName = By.XPath("//div[contains(@class,'BannerHeaderText')]/h5");

        public string GetUserFirstNameFromProfileMenu()
        {
            return Wait.UntilElementVisible(UserFirstName).GetText().Split(" ").First();
        }
        public void ClickOnProfileMenuItem()
        {
            Wait.WaitIncaseElementClickable(ProfileMenuItem).ClickOn();
        }
        public void ClickOnJobPreferenceMenuItem()
        {
            Wait.UntilElementClickable(JobPreferenceMenuItem).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public void ClickOnRecentlyViewItem()
        {
            if (!Convert.ToBoolean(Wait.UntilElementVisible(RecentlyViewMenuItem).GetAttribute("aria-expanded")))
            {
                Wait.WaitIncaseElementClickable(RecentlyViewMenuItem).ClickOn();
            }
        }
        public string GetRecentlyViewedJobText()
        {
            return Wait.UntilElementVisible(RecentlyViewedJobText).GetText();
        }
        public void ClickOnClearAllRecentlyViewedJob()
        {
            Wait.UntilElementClickable(ClearAllRecentlyViewedJob).ClickOn();
            Wait.WaitTillElementCountIsLessThan(ClearAllRecentlyViewedJob, 1);
        }
        public bool IsRecentlyViewedJobTextIsPresent()
        {
            return Wait.IsElementPresent(RecentlyViewedJobText,5);
        }
        public void ClickOnCloseButton()
        {
            Wait.WaitIncaseElementClickable(CloseButton).ClickOn();
        }
        public void ClickOnLogOutButton()
        {
            if (BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Safari))
            {
                Driver.JavaScriptClickOn(LogOutButton);
            }
            else
            {
                Wait.WaitIncaseElementClickable(LogOutButton).ClickOn();
            }
        }

        public bool IsLogoutButtonPresent()
        {
            return Wait.IsElementPresent(LogOutButton, 5);
        }

        public void ClickOnProfileArrow()
        {
            Wait.UntilElementVisible(ProfileArrow);
            Wait.UntilElementClickable(ProfileArrow).ClickOn();
            Wait.WaitTillElementCountIsLessThan(ProfileArrow, 1);
        }

        public void ClickOnJobApplicationText()
        {
            Wait.UntilElementClickable(JobApplicationText).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
            Driver.RefreshPage();
            WaitUntilFmpTextLoadingIndicatorInvisible();
        }

        public void ClickOnAccountNavMenuItem(string accountNavText)
        {
            Wait.UntilElementClickable(AccountNavMenuItem(accountNavText)).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
    }
}
