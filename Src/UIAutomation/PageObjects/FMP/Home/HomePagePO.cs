using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Home
{
    internal class HomePagePo : FmpBasePo
    {
        public HomePagePo(IWebDriver driver) : base(driver)
        {
        }

        //Blog
        private static By FirsCardText(int card) => By.XPath($"//div[contains(@class,'BlogCard')][{card}]//div[contains(@class,'CardText')]//h3");
        private static By NthBlogCard(int card) => By.XPath($"//div[contains(@class,'BlogCard')][{card}]//div[contains(@class,'CardText')]");
        private readonly By BlogHeaderText = By.CssSelector("span#hs_cos_wrapper_name");

        //Nth Job card

        private readonly By FirstJobCardTitle = By.XPath("//section//div[contains(@class,'JobName')]");
        private readonly By ViewJobsButton = By.XPath("//span[text()='View Jobs']");
        private readonly By PartnerWithUsButton = By.XPath("//span[text()='Partner with Us']");


        //JobApplication
        private static By CardTitleText(int index) => By.XPath($"//div[@data-index= '{index}']//div[contains(@class,'ItemWrapper')]/h5");
        private static By CardDescriptionText(int index) => By.XPath($"//div[@data-index= '{index}']//div[contains(@class,'ItemWrapper')]/p");
        private static By TravelerCardButton(int index) => By.XPath($"//div[@data-index= '{index}']//div[contains(@class,'ItemWrapper')]/button");
        private static By BenefitsGroupsOptionsTitleButton(string buttonName) => By.XPath($"//div[contains(@class,'BenefitsGroupOptions')]//span[text()='{buttonName}']/parent::button");
        private static By PartnerWithUsHref(int index) => By.XPath($"//div[@data-index= '{index}']//div[contains(@class,'temWrapper')]/a");
        private readonly By CreateATravelerProfileButton = By.XPath("//a[contains(@class,'ButtonStyled')]/span[text()='Create a Traveler profile']");

        //DeviceElement
        private readonly By JobApplicationNextButtonDevice = By.XPath("//div[contains(@class,'OptionsSelectWrapper')]/button[@aria-label ='next benefits']");
        private readonly By BenefitsGroupOptionsDevice = By.XPath("//div[contains(@class,'BenefitsGroupOptions')]/div/div[contains(@class,'MuiTabs-flexContainerVertical')]");
        private readonly By BenefitsGroupOptionsHeaderTextDevice = By.XPath("//div[contains(@class,'BenefitsCarouselWrapper')]/h2[contains(text(),'A staffing marketplace')]");

        //Blog
        public string GetBlogTitle(int blog)
        {
            return Wait.UntilElementClickable(FirsCardText(blog)).GetText();
        }
        public void ClickOnNthBlogCard(int card)
        {
            Driver.JavaScriptClickOn(NthBlogCard(card));
            Wait.UntilElementInVisible(NthBlogCard(card));
        }
        public string GetBlogHeaderText()
        {
            return Wait.UntilElementVisible(BlogHeaderText).GetText();
        }

        public void WaitUntilFirstJobCardTitleGetDisplayed()
        {
            Wait.WaitUntilTextRefreshed(FirstJobCardTitle);
        }

        public bool IsPartnerWithUsButtonDisplayed()
        {
            return Wait.IsElementPresent(PartnerWithUsButton, 3);
        }
        public bool IsViewJobsButtonDisplayed()
        {
            return Wait.IsElementPresent(ViewJobsButton, 3);
        }
        public void ClickOnViewJobsButton()
        {
            Wait.UntilElementClickable(ViewJobsButton).ClickOn();
        }
        public void ClickOnPartnerWithUsButton()
        {
            Wait.UntilElementClickable(PartnerWithUsButton).ClickOn();
        }

        //JObApplication
        public string GetCardTitleText(int index)
        {
            switch (BaseTest.PlatformName)
            {
                case PlatformName.Android:
                    Driver.MoveToElement(Wait.UntilElementExists(JobApplicationNextButtonDevice));
                    break;
                case PlatformName.Ios:
                    Driver.JavaScriptScrollToElement(Wait.UntilElementExists(BenefitsGroupOptionsHeaderTextDevice));
                    break;
            }
            return Wait.UntilAllElementsLocated(CardTitleText(index)).First(x => x.IsDisplayed()).GetText();
        }
        public string GetCardDescriptionText(int index)
        {
            return Wait.UntilAllElementsLocated(CardDescriptionText(index)).First(x => x.IsDisplayed()).GetText();
        }
        public void ClickOnCreateAProfileButton(int index)
        {
            Wait.UntilAllElementsLocated(TravelerCardButton(index)).First(e => e.Displayed).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public void ClickOnBenefitsGroupsOptionsTitleButton(string buttonName)
        {
            Driver.JavaScriptClickOn(BenefitsGroupsOptionsTitleButton(buttonName));
        }
        public string GetPartnerWithUsHref(int index)
        {
            return Wait.UntilElementVisible(PartnerWithUsHref(index)).GetAttribute("href");
        }
        public bool IsCardButtonPresent(int index)
        {
            return Wait.IsElementPresent(PartnerWithUsHref(index), 5);
        }
        public void ClickCreateATravelerProfileButton()
        {
            Wait.UntilElementClickable(CreateATravelerProfileButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public void ClickOnJobApplicationNextButtonDevice()
        {
            Wait.UntilElementClickable(JobApplicationNextButtonDevice).ClickOn();
        }
        public string GetBenefitsGroupOptionsDevice()
        {
            return Wait.UntilElementClickable(BenefitsGroupOptionsDevice).GetText();
        }
    }
}

