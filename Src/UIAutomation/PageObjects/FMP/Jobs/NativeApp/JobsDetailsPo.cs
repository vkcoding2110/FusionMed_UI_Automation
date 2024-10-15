using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Jobs.NativeApp
{
    internal class JobsDetailsPo : FmpBasePo
    {
        public JobsDetailsPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By ShareIcon = By.XPath("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[2]/android.widget.Button[3] | //android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup[3]/android.widget.Button[3]");
        private static By SocialMediaIcon(int index) => By.XPath($"//android.widget.TextView[@text='Email Job']/parent::android.widget.Button//parent::android.view.ViewGroup/preceding-sibling::android.widget.Button[{index}]");
        private readonly By JobTitle = By.XPath("//*[@class='android.widget.TextView'][contains(@text, 'Job')]/following-sibling::android.widget.TextView[1]");

        public void ClickOnShareIcon()
        {
            Wait.UntilElementClickable(ShareIcon).ClickOn();
        }

        public void ClickOnSocialMediaIcon(int index)
        {
            Wait.UntilElementClickable(SocialMediaIcon(index)).ClickOn();
        }
        public string GetJobCardTitle()
        {
            return Wait.UntilElementVisible(JobTitle).GetText();
        }
    }
}