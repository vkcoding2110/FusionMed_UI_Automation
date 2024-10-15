using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.YopMail
{
    internal class EmailListingGridPo : BasePo
    {
        public EmailListingGridPo(IWebDriver driver) : base(driver)
        {
        }

        //Header 
        private readonly By RefreshButton = By.CssSelector("div.wminboxheader button#refresh");

        //Left Nav
        private readonly By AllEmailRows = By.XPath("//div[@class='m']");
        private readonly By SubjectInfo = By.XPath("//div[@class='m']//span[@class='lmf']");
        private readonly By InboxIframe = By.CssSelector("iframe#ifinbox");

        //Email Body
        private static By ButtonOrLink(string buttonOrLinkName) => By.XPath($"//span//a[text()='{buttonOrLinkName}']");
        protected readonly By MessageBodyIframe = By.XPath("//iframe[@id='ifmail']");
        private readonly By MessageBodyText = By.XPath("//div[@id='mail']//span/parent::td[1]");

        //Sender Email Text
        private readonly By SenderEmailText = By.XPath("//div[@class='md text zoom nw f24']/span");

        //Device Elements
        protected readonly By SenderEmailTextDevice = By.XPath("//div[contains(@class,'zoom')]//span[contains(@class,'ellipsis b')]");
        protected readonly By InboxMobMailFrame = By.CssSelector("iframe#ifmobmail");

        //Left Nav
        public void WaitTillMessageReceived(string subject, int waitInSeconds)
        {
            for (var i = 0; i < waitInSeconds; i++)
            {
                Driver.SwitchToIframe(Wait.UntilElementExists(InboxIframe));
                var isMessageFound = false;
                if (Wait.IsElementPresent(AllEmailRows, 3))
                {
                    isMessageFound = Wait.UntilAllElementsLocated(AllEmailRows, 10).Where(e => e.Displayed).ToList()
                        .Select(element => element.GetText()).Any(rowText => rowText.Contains(subject));
                }

                if (!isMessageFound)
                {
                    Driver.SwitchToDefaultIframe();
                    Wait.UntilElementClickable(RefreshButton).ClickOn();
                    Wait.HardWait(1000);
                }
                else
                {
                    break;
                }
            }
        }

        public void OpenEmail(string subject, int waitInSeconds = 10)
        {
            WaitTillMessageReceived(subject, waitInSeconds);
            Wait.UntilElementClickable(SubjectInfo).ClickOn();
            Driver.SwitchToDefaultIframe();
        }

        //Email Body
        public void ClickOnButtonOrLink(string buttonOrLinkName)
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.HardWait(3000);
                Driver.SwitchToIframe(Wait.UntilElementExists(InboxMobMailFrame));
                var url = Wait.UntilElementClickable(ButtonOrLink(buttonOrLinkName)).GetAttribute("href");
                Driver.NavigateTo(url);
                Driver.SwitchToDefaultIframe();
            }
            else
            {
                Wait.HardWait(3000);
                Driver.SwitchToIframe(Wait.UntilElementExists(MessageBodyIframe));
                var url = Wait.UntilElementClickable(ButtonOrLink(buttonOrLinkName)).GetAttribute("href");
                Driver.NavigateTo(url);
                Driver.SwitchToDefaultIframe();
            }
        }

        public string GetHRefOfButton(string buttonOrLinkName)
        {
            Wait.HardWait(3000);
            Driver.SwitchToIframe(Wait.UntilElementExists(BaseTest.PlatformName != PlatformName.Web ? InboxMobMailFrame : MessageBodyIframe));
            var hRef = Wait.UntilElementClickable(ButtonOrLink(buttonOrLinkName)).GetAttribute("href");
            Driver.SwitchToDefaultIframe();
            return hRef;
        }

        public string GetSenderEmailText()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Driver.SwitchToIframe(Wait.UntilElementExists(InboxMobMailFrame));
                var text = Wait.UntilElementExists(SenderEmailTextDevice).GetText();
                Driver.SwitchToDefaultIframe();
                return text;
            }
            else
            {
                Driver.SwitchToIframe(Wait.UntilElementExists(MessageBodyIframe));
                var text = Wait.UntilElementVisible(SenderEmailText).GetText();
                Driver.SwitchToDefaultIframe();
                return text;
            }
        }

        public string GetMessageBodyText()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Driver.SwitchToIframe(Wait.UntilElementExists(InboxMobMailFrame));
            }
            else
            {
                Driver.SwitchToIframe(Wait.UntilElementExists(MessageBodyIframe));
            }
            var text = Wait.UntilElementExists(MessageBodyText).GetText();
            Driver.SwitchToDefaultIframe();
            return text;
        }
    }
}