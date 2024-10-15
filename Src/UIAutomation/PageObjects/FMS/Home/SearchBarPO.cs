using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Home
{
    internal class SearchBarPo : FmsBasePo
    {
        public SearchBarPo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By SearchJobs = By.CssSelector("div[class*='searchHeaderInputstyles__MultiSearchWrapper'] input");
        private readonly By SelectJobsFromSearchResult = By.CssSelector("svg[class*='ArrowIcon']");

        private readonly By JobTitle = By.XPath("//div[contains(@class,'jobsstyles__JobName')]");
        private static By JobCardFilter(string jobCard) => By.XPath($"//div[contains(@class,'filterBardesktopstyles__FilterBarTags')]/div/span[text()='{jobCard}']");

        private readonly By SearchBarInputBox = By.CssSelector("div[class*='searchHeaderInputstyles__MultiSearchWrapper'] input");
        private readonly By SearchResultPopup = By.CssSelector("div[class*='SearchFilterWrapper']");
        private readonly By SearchBarCancelButton = By.CssSelector("div[class*='FocusModalHeaderCancel']");

        // Device elements
        private readonly By SearchBarInputBoxDevice = By.CssSelector("button[class*='mobileSearchButtonstyles']");
        private readonly By SearchJobSuggestionItemDevice = By.CssSelector("div[class*='mobileSearchHeaderstyles'] input");
        private readonly By SearchBarCancelButtonDevice = By.XPath("//div[contains(@class,'MobileSearchCancel')]");
        private readonly By SearchIconDevice = By.CssSelector("div[class*='MultiSearchIconWrapper']");
        private static By JobCardFilterDevice(string jobCard) => By.XPath($"//div[contains(@class,'filterBarmobilestyles__FilterBarTags')]/div/span[text()='{jobCard}']");
        public void EnterJobsToSearchBoxAndHitEnter(string jobs)
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                WaitUntilMpPageLoadingIndicatorInvisible();
                Wait.UntilElementVisible(SearchBarInputBoxDevice);
                Wait.UntilElementClickable(SearchBarInputBoxDevice).ClickOn();
                Wait.UntilElementClickable(SearchJobSuggestionItemDevice).EnterText(jobs);
            }
            else
            {
                Wait.UntilElementClickable(SearchJobs).EnterText(jobs);
            }
        }

        public void ClickOnJobsFromSearchResult()
        {
            Wait.UntilAllElementsLocated(SelectJobsFromSearchResult,20).First(e => e.Displayed).ClickOn();
            Wait.HardWait(3000);
        }

        public IList<string> GetJobTitle()
        {
            return Wait.UntilAllElementsLocated(JobTitle).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public bool IsSelectedJobCardFilterPresent(string jobCard)
        {
            return Wait.IsElementPresent(BaseTest.PlatformName != PlatformName.Web ? JobCardFilterDevice(jobCard) : JobCardFilter(jobCard));
        }

        public void ClickOnSearchBarInputBox()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilElementClickable(SearchBarInputBoxDevice).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(SearchBarInputBox).ClickOn();
            }
            Wait.HardWait(3000);
        }

        public bool IsSearchResultPopupPresent()
        {
            return Wait.IsElementPresent(BaseTest.PlatformName != PlatformName.Web ? SearchIconDevice : SearchResultPopup);
        }

        public void ClickOnSearchBarCancelButton()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilElementVisible(SearchBarCancelButtonDevice).ClickOn();
                Wait.UntilElementInVisible(SearchBarCancelButtonDevice);
            }
            else
            {
                Wait.UntilElementClickable(SearchBarCancelButton).ClickOn();
                Wait.UntilElementInVisible(SearchBarCancelButton);
            }
        }
    }
}
