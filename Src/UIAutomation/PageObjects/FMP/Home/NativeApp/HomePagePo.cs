using OpenQA.Selenium;
using System.Linq;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Home.NativeApp
{
    internal class HomePagePo : FmpBasePo
    {
        public HomePagePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By FeatureOneNextButton = By.XPath("//*[@text='Next']//parent::android.widget.Button");
        private readonly By FeatureThreeLetsGoButton = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'go')]//parent::android.widget.Button");

        //Menu bar
        private readonly By MoreMenuButton = By.XPath("*//android.widget.TextView[@text='MORE']");
        private readonly By JobPreferencesButton = By.XPath("*//android.widget.TextView[@text='JOB PREFERENCES']");

        //Quick Apply
        private readonly By QuickApplyButton = By.XPath("//*[@text='Quick Apply']//parent::android.widget.Button");
        private readonly By JobCard = By.XPath("//android.widget.HorizontalScrollView/parent::android.view.ViewGroup/following-sibling::android.view.ViewGroup//android.widget.ImageView");

        //Sort&Filter
        private readonly By SortAndFilterButton = By.XPath("//android.widget.HorizontalScrollView/following-sibling::android.view.ViewGroup");
        public void OpenHomePage()
        {
            Wait.HardWait(3000);
            Wait.UntilElementClickable(FeatureOneNextButton,30).Click();
            Wait.UntilElementClickable(FeatureOneNextButton,30).Click();
            Wait.UntilElementClickable(FeatureThreeLetsGoButton,30).Click();
            Wait.UntilElementInVisible(FeatureThreeLetsGoButton);
        }

        public void ClickOnMoreMenuButton()
        {
            Wait.HardWait(5000);
            Wait.UntilElementClickable(MoreMenuButton).ClickOn();
        }

        public void ClickOnQuickApplyButton()
        {
            Wait.UntilElementClickable(QuickApplyButton).ClickOn();
        }

        public void ClickOnPreferencesMenuButton()
        {
            Wait.UntilElementClickable(JobPreferencesButton).ClickOn();
            WaitUntilAppLoadingIndicatorInvisible();
        }

        public void ClickOnJobCard()
        {
            Wait.UntilElementExists(JobCard);
            Wait.UntilAllElementsLocated(JobCard).First(x => x.IsDisplayed()).ClickOn();
            WaitUntilAppLoadingIndicatorInvisible();
        }
        public void ClickOnSortAndFilterButton()
        {
            Wait.UntilElementClickable(SortAndFilterButton).ClickOn();
        }
    }
}
