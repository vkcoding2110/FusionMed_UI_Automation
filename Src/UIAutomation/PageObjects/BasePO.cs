using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects
{
    internal class BasePo
    {
        protected IWebDriver Driver;
        protected WaitHelpers Wait;

        public BasePo(IWebDriver driver)
        {
            this.Driver = driver;
            Wait = new WaitHelpers(driver);
        }
        public void NavigateToUrl(string url)
        {
            Driver.NavigateTo(url);
        }
    }
}
