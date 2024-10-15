using OpenQA.Selenium;

namespace UIAutomation.PageObjects.Core
{
    internal class CoreBasePo : BasePo
    {
        public CoreBasePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By ProcessingCard = By.XPath("//div[contains(@class,'processing card')][contains(text(),'Processing')]");

        public void WaitUntilCoreProcessingTextInvisible()
        {
            try
            {
                Wait.UntilElementVisible(ProcessingCard, 10);
            }
            catch
            {
                //Nothing
            }
            Wait.UntilElementInVisible(ProcessingCard, 90);
            Wait.HardWait(2000);
        }
    }
}
