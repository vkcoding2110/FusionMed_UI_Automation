using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Home
{
    internal class AboutUsPo : FmpBasePo
    {
        public AboutUsPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By FindYourNextJobButton = By.XPath("//a[contains(@class,'ButtonStyled')]/span[text()='Find Your Next Job']");
        private readonly By PartnerWithUsButton = By.XPath("//a[contains(@class,'ButtonStyled')]/span[text()='Partner With Us']");
        private readonly By MediaInquiriesHref = By.XPath("//div[contains(@class,'Paragraph')]/p[contains(text(),'Our communications team loves working with journalists')]/a");
        private readonly By PublicRelationsHref = By.XPath("//div[contains(@class,'Paragraph')]/h4[text()='Public Relations']/following-sibling::p[3]/a");

        public void ClickOnFindYourNextJobButton()
        {
            Wait.UntilElementVisible(FindYourNextJobButton);
            Driver.JavaScriptClickOn(Wait.UntilElementExists(FindYourNextJobButton));
        }

        public void ClickOnPartnerWithUsButton()
        {
            Driver.JavaScriptClickOn(Wait.UntilElementExists(PartnerWithUsButton));
        }

        public string GetMediaInquiriesHref()
        {
            return Wait.UntilElementClickable(MediaInquiriesHref).GetAttribute("href");
        }

        public string GetPublicRelationsHref()
        {
            return Wait.UntilElementClickable(PublicRelationsHref).GetAttribute("href").ToLower();
        }
    }
}
