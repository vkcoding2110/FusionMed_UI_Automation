using OpenQA.Selenium;

namespace UIAutomation.PageObjects.FMP
{
    internal class FmpBasePo : BasePo
    {
        public FmpBasePo(IWebDriver driver) : base(driver)
        {

        }

        private readonly By FmpPageLoadingIndicator = By.CssSelector("div[class*='MuiBackdrop-root'] span");
        private readonly By FmpTextLoadingIndicator = By.XPath("//div[contains(@class,'AuthLoaderWrapper')]//p[text()='Verifying account, hang tight a moment...']");

        //NativeApp
        private readonly By LoadingIndicator = By.XPath("//*[@class='android.widget.ProgressBar']");

        public void WaitUntilFmpPageLoadingIndicatorInvisible()
        {
            try
            {
                Wait.UntilElementVisible(FmpPageLoadingIndicator, 10);
            }
            catch
            {
                //Nothing
            }
            Wait.UntilElementInVisible(FmpPageLoadingIndicator, 90);
            Wait.HardWait(2000);
        }

        public void WaitUntilFmpTextLoadingIndicatorInvisible()
        {
            Wait.UntilElementInVisible(FmpTextLoadingIndicator,30);
        }

        //NativeApp
        public void WaitUntilAppLoadingIndicatorInvisible()
        {
            try
            {
                Wait.UntilElementVisible(LoadingIndicator, 10);
            }
            catch
            {
                //Nothing
            }
            Wait.UntilElementInVisible(LoadingIndicator, 60);
            Wait.HardWait(2000);
        }
    }
}