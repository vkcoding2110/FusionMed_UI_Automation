using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.NativeApp.Search
{
    internal class SearchPo : FmpBasePo
    {

        public SearchPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By SearchBar = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'Clear all')]/parent::android.widget.Button/parent::android.view.ViewGroup/parent::android.view.ViewGroup/parent::android.widget.HorizontalScrollView/parent::android.view.ViewGroup/preceding-sibling::android.view.ViewGroup/android.view.ViewGroup[2]");
        private readonly By CloseIcon = By.XPath("//*[@text='Search']/following-sibling::android.view.ViewGroup[1]/android.widget.Button");
        private readonly By SearchBarInput = By.XPath("//*[@content-desc='search']//following-sibling::android.widget.EditText");
        private readonly By SearchBarHeaderText = By.XPath("//*[@text='Search']");
        private static By JobsSearchResult(string job) => By.XPath($"//android.widget.ScrollView//android.widget.TextView[1][contains(@text,'{job}')]");
        private readonly By SearchCloseIconDevice = By.XPath("//*[@content-desc='clear']");
        private static By JobTitle(string jobTitle) => By.XPath($"//*[@text='{jobTitle}']");

        public void ClickOnSearchBar()
        {
            Wait.UntilElementVisible(SearchBar).ClickOn();
        }

        public bool IsSearchHeaderTextPresent()
        {
            return Wait.IsElementPresent(SearchBarHeaderText, 4);
        }

        public void EnterJobsToSearchBoxAndHitEnter(string jobs)
        {
            Wait.UntilAllElementsLocated(SearchBarInput).First(e => e.Displayed).SendKeys(jobs);
        }
        public void SelectJobFromSearchResult(string jobName)
        {
            Wait.UntilElementVisible(SearchCloseIconDevice);
            Wait.UntilElementClickable(JobsSearchResult(jobName)).ClickOn();
        }
        public IList<string> GetJobTitle(string job)
        {
            return Wait.UntilAllElementsLocated(JobTitle(job)).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
            Wait.UntilElementInVisible(CloseIcon);
        }
    }
}
