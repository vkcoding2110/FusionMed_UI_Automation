using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Core.Candidate
{
    internal class CandidateDetailsPo : CoreBasePo
    {
        public CandidateDetailsPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By GridIFrame = By.XPath("//div[contains(@id,'pane-candidate')]//iframe[@class='iframe fullheight']");
        private readonly By HeaderText = By.CssSelector("div.header.sticky-top");
        

        public void SwitchToIFrame()
        {
            Driver.SwitchToDefaultIframe();
            Wait.UntilElementExists(GridIFrame);
            Driver.SwitchToIframe(Wait.UntilElementExists(GridIFrame));
        }

        public string GetHeaderText()
        {
            return Wait.UntilElementClickable(HeaderText).GetText();
        }
    }
}
