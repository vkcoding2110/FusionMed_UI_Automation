using OpenQA.Selenium;

namespace UIAutomation.PageObjects.FMS
{
    internal class FmsBasePo : BasePo
    {
        public FmsBasePo(IWebDriver driver) : base(driver)
        {

        }

        private readonly By MpPageLoadingIndicator = By.CssSelector("div[class*='loaderstyles__BackgroundModal'] div");
        private readonly By MpSubmitFormLoadingIndicator =
            By.CssSelector("svg[class*='formLoaderstyles__FormLoaderIcon']");

        // Validation message for all forms(quick application,full application,drug consent,request staff,contact us) of Marketplace
        private readonly By FormValidationMessage = By.XPath("//div[@class='validation-message']");

        public void WaitUntilMpPageLoadingIndicatorInvisible()
        {
            try
            {
                Wait.UntilElementVisible(MpPageLoadingIndicator, 20);
            }
            catch
            {
                //Nothing
            }
            Wait.UntilElementInVisible(MpPageLoadingIndicator, 90);
            Wait.HardWait(2000);
        }

        public void WaitUntilMpSubmitFormLoadingIndicatorInvisible()
        {
            try
            {
                Wait.UntilElementVisible(MpSubmitFormLoadingIndicator);
            }
            catch
            {
                //Nothing
            }
            Wait.UntilElementInVisible(MpSubmitFormLoadingIndicator, 90);
            Wait.HardWait(2000);
        }

        // Validation message for all forms(quick apply,full apply,drug consent,request staff,contact us) of Marketplace
        public bool IsMpFormValidationMessageDisplayed()
        {
            return Wait.IsElementPresent(FormValidationMessage);
        }
    }
}
