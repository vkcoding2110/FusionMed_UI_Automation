using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter.Admin
{
    internal class RecruiterSharedProfilePo : FmpBasePo
    {
        public RecruiterSharedProfilePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By ViewDocumentButton = By.XPath("//div[contains(@class,'TooltipStyled')]//button/span[text()='View Documents']");
        private readonly By DownloadResumeButton = By.XPath("//div[contains(@class,'TooltipStyled')]//button/span[text()='Download Resume']");
        private readonly By SearchInput = By.CssSelector("input[class*='ProfileShareSearch']");

        public void ClickOnViewDocumentButton()
        {
            Wait.UntilElementClickable(ViewDocumentButton).ClickOn();
        }
        public void ClickOnDownloadResumeButton()
        {
            Driver.JavaScriptClickOn(DownloadResumeButton);
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public void EnterDataToSearchTextBox(string nameOrEmail)
        {
            Wait.UntilElementClickable(SearchInput).EnterText(nameOrEmail, true);
        }
    }
}