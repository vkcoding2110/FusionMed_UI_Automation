using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.BrowseAll
{
    internal class JobsPo : FmpBasePo
    {
        public JobsPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By JobsHeaderText = By.XPath("//div[contains(@class,'BrowseHeader')]//div/h6");
        private readonly By BackArrow = By.XPath("//div[contains(@class,'BrowseHeader')]//div[@class='middle']/preceding-sibling::button");
        private readonly By CloseIcon = By.CssSelector("div[class*='middle'] + button");
        private readonly By JobsMenuList = By.XPath("//button[contains(@class,'ItemWrapper-sc')]/label");

        private static By JobsMenuSubList(string subListItems) => By.XPath($"//button[contains(@class,'ItemWrapper-sc')]//label[contains(text(),'{subListItems}')]");

        public string GetJobsMenuHeaderText()
        {
            return Wait.UntilElementVisible(JobsHeaderText).GetText();
        }

        public void ClickOnBackButton()
        {
            Wait.UntilElementClickable(BackArrow).ClickOn();
        }

        public void ClickOnJobsMenuCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
        }

        public bool IsJobsMenuOpened()
        {
            return Wait.IsElementPresent(JobsHeaderText, 5);
        }

        public IList<string> GetJobsMenusList()
        {
            return Wait.UntilAllElementsLocated(JobsMenuList).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }

        public void ClickOnJobsMenuSubListItems(string subListItems)
        {
            Wait.UntilElementClickable(JobsMenuSubList(subListItems)).ClickOn();
            Wait.HardWait(2000);
        }
    }
}
