using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Header
{
    internal class SearchPo : FmpBasePo
    {
        public SearchPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By SearchBarInputBox = By.CssSelector("section[class*='SectionWrapper'] div[class*='MultiSearchWrapper'] input");
        private readonly By SelectJobsFromSearchResult = By.XPath("//div[contains(@class,'ContentList')]/div/div[2]/button[1]");
        private readonly By JobTitle = By.XPath("//div[contains(@class,'JobName')]");
        private static By JobCardFilter(string jobCard) => By.XPath($"//div[contains(@class,'FilterBarTags')]/div/span[text()='{jobCard}']");
        private readonly By SearchResultPopup = By.CssSelector("div[class*='FocusModalBodyWrapper']");
        private readonly By SearchBarCancelButton = By.CssSelector("div[class*='FocusModalHeaderCancel']");

        //Device Elements
        private readonly By SearchIconDevice = By.XPath("//div[contains(@class,'SearchInputStyled')]//input[@placeholder='Find your next job']//parent::div");
        private readonly By SelectJobsFromSearchResultAndroid = By.XPath("//div[contains(@role,'presentation')]/div[3]/div[2]//div[2]/button");
        private readonly By SelectJobsFromSearchResultsIos = By.XPath("/html/body/div[3]/div[3]/div[2]/div[2]/button/div | /html/body/div[2]/div[3]/div[2]/div[2]/button");
        private readonly By SearchResultPopupDevice = By.XPath("//div[contains(@role,'presentation')]");
        private readonly By SearchBarCancelButtonDevice = By.CssSelector("div[class*='MobileSearchCancel']");
        private readonly By SearchCloseIconDevice = By.XPath("//div[contains(@class,'MobileSearchContainer')]//button[@aria-label='Clear this search query']");
        private readonly By SearchBarInputBoxDevice = By.CssSelector("div[class*='MobileSearchContainer'] input");

        public void EnterJobsToSearchBoxAndHitEnter(string jobs)
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilAllElementsLocated(SearchIconDevice).First(e => e.Displayed).ClickOn();
                Wait.UntilElementVisible(SearchBarCancelButtonDevice, 20);                Wait.UntilAllElementsLocated(SearchBarInputBoxDevice).First(e => e.Displayed).SendKeys(jobs);            }
            else            {                Wait.UntilAllElementsLocated(SearchBarInputBox).First(e => e.Displayed).SendKeys(jobs);            }        }
        public void ClickOnJobsFromSearchResult()
        {
            var plat = BaseTest.PlatformName;
            switch (plat)
            {
                case PlatformName.Android:
                    Wait.UntilElementVisible(SearchCloseIconDevice);
                    Wait.UntilElementVisible(SelectJobsFromSearchResultAndroid).ClickOn();
                    break;
                case PlatformName.Ios:
                    Wait.UntilElementVisible(SearchCloseIconDevice);
                    Wait.UntilElementClickable(SelectJobsFromSearchResultsIos).ClickOn();
                    break;
                case PlatformName.Web:
                    Driver.JavaScriptClickOn(Wait.UntilElementExists(SelectJobsFromSearchResult));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Wait.HardWait(3000);
        }

        public IList<string> GetJobTitle()
        {
            Wait.UntilElementVisible(JobTitle, 3);
            return Wait.UntilAllElementsLocated(JobTitle).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public bool IsSelectedJobCardFilterPresent(string jobCard)
        {
            return Wait.IsElementPresent(JobCardFilter(jobCard));
        }

        public void ClickOnSearchBarInputBox()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilElementClickable(SearchIconDevice).ClickOn();
            }
            else
            {

                Wait.UntilAllElementsLocated(SearchBarInputBox).First(e => e.Displayed).ClickOn();
            }
            Wait.HardWait(3000);
        }

        public bool IsSearchResultPopupPresent()
        {
            return Wait.IsElementPresent(BaseTest.PlatformName != PlatformName.Web ? SearchResultPopupDevice : SearchResultPopup);
        }

        public void ClickOnSearchBarCancelButton()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilElementClickable(SearchBarCancelButtonDevice).ClickOn();
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
