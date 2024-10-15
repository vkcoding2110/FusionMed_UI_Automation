using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.ApplyNow
{
    internal class ThanksPagePo : FmsBasePo
    {
        public ThanksPagePo(IWebDriver driver) : base(driver)
        {
        }

        private static readonly By ThanksForApplyText = By.XPath("//div[contains(@class,'ThankYouHeaderTitle')]");
        private static readonly By BackToJobSearchButton = By.XPath("//div[contains(@class,'ActionsWrapper')]/a/span[text()='Back to job search']");

        private static readonly By FusionMarketplaceHref = By.XPath("//a[contains(text(),'Fusion Marketplace?')]");
        private static By FillOutFullAppHref(string formName) => By.XPath($"//div[contains(@class,'FillOutYourNextStep')]//a[contains(text(),'{formName}')]");

        public string GetFillOutAppHref(string formName)
        {
            return Wait.UntilElementVisible(FillOutFullAppHref(formName)).GetAttribute("href");
        }

        public string GetFormSubmitSuccessText()
        {
            return Wait.UntilElementVisible(ThanksForApplyText).GetText();
        }

        public void ClickOnBackToJobSearchButton()
        {
            Wait.UntilElementClickable(BackToJobSearchButton).ClickOn();
            Wait.UntilElementInVisible(BackToJobSearchButton,3);
        }

        public string GetFusionMarketplaceHref()
        {
            return Wait.UntilElementVisible(FusionMarketplaceHref).GetAttribute("href");
        }
        public bool IsFillOutFormUrlPresent(string formName)
        {
            return Wait.IsElementPresent(FillOutFullAppHref(formName));
        }
    }
}
