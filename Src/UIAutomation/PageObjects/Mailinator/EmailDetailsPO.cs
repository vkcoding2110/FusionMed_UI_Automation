using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Mailinator
{
    internal class EmailDetailsPo : BasePo
    {
        public EmailDetailsPo(IWebDriver driver) : base(driver)
        {
        }

        private By ButtonOrLink(string buttonOrLinkName) => By.XPath($"//span//a[text()='{buttonOrLinkName}']");
        private readonly By MessageBodyIframe = By.CssSelector(".x_content iframe#msg_body");
      
        public void ClickOnButtonOrLink(string buttonOrLinkName)
        {
            Driver.SwitchToIframe(Wait.UntilElementExists(MessageBodyIframe));
            Wait.UntilElementVisible(ButtonOrLink(buttonOrLinkName));
            Wait.UntilElementClickable(ButtonOrLink(buttonOrLinkName)).ClickOn();
            Driver.SelectWindowByIndex(1);
        }
    }
}
