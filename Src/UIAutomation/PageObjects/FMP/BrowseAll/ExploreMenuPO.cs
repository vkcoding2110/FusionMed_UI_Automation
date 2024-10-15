using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.BrowseAll
{
    internal class ExploreMenuPo : FmpBasePo
    {
        public ExploreMenuPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By ExploreHeaderText = By.XPath("//div[contains(@class,'BrowseHeader')]/div/h6");
        private readonly By ExploreMenuCloseIcon = By.CssSelector("div[class*='middle'] + button");
        private readonly By ExploreMenusList = By.XPath("//div[contains(@class,'SlideWrapper-sc')]//button[contains(@class,'ItemWrapper')]/label");

        // Explore menu Agency list
        private static By ExploreMenuItemsButtons(string button) => By.XPath($"//div[contains(@class,'SlideWrapper')]//button[contains(@class,'ItemWrapper-sc')]/label[text()='{button}']");
        private readonly By HeaderText = By.XPath("//div[@class='middle']/h6");
        private readonly By BackButton = By.XPath("//div[contains(@class,'BrowseHeader')]//button[1]");
        private readonly By AgencyMenuList = By.XPath("//button[contains(@class,'ItemWrapper-sc')]/label");
        private static By AgencyMenuListItem(string agency) => By.XPath($"//button[contains(@class,'ItemWrapper')]//label[text()='{agency}']");
        private readonly By ViewAllJobsText = By.XPath("//div[contains(@class,'BrowseContainer')]//label[text()='View All Jobs']/parent::button");

        public void ClickOnAgencyMenuItem(string agency)
        {
            Driver.JavaScriptClickOn(AgencyMenuListItem(agency));
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public IList<string> GetAgencyMenuList()
        {
            return Wait.UntilAllElementsLocated(AgencyMenuList).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }
        public string GetAgencyMenuHeaderText()
        {
            return Wait.UntilElementVisible(HeaderText).GetText();
        }

        public void ClickOnBackButton()
        {
            Wait.UntilElementClickable(BackButton).ClickOn();
        }
        public bool IsExploreMenuOpened()
        {
            return Wait.IsElementPresent(ExploreHeaderText, 5);
        }
        public void WaitUntilExploreHeaderTextVisible()
        {
            Wait.UntilElementVisible(ExploreHeaderText);
        }
        public void ClickOnExploreMenuCloseIcon()
        {
            Wait.UntilElementClickable(ExploreMenuCloseIcon).ClickOn();
        }
        public IList<string> GetExploreMenusList()
        {
            if (BaseTest.PlatformName != PlatformName.Ios)
            {
                Wait.UntilElementVisible(ExploreMenusList);
            }
            return Wait.UntilAllElementsLocated(ExploreMenusList).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }

        public void ClickOnExploreMenuItemsButton(string button)
        {
            Wait.HardWait(1000);
            Driver.JavaScriptClickOn(Wait.UntilAllElementsLocated(ExploreMenuItemsButtons(button)).FirstOrDefault(e => e.IsDisplayed()));
            Wait.UntilElementInVisible(ExploreMenuItemsButtons(button));
        }

        public void WaitUntilExploreMenuOpen()
        {
            Wait.UntilElementVisible(ExploreHeaderText);
        }
        public void ClickOnViewAllJobsLinkText(string menuItem)
        {
            new HeaderPo(Driver).ClickOnBrowseAllButton();
            ClickOnAgencyMenuItem(menuItem);
            Wait.UntilElementClickable(ViewAllJobsText).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
    }
}