using OpenQA.Selenium;
using System.Linq;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Jobs
{
    internal class HousingNearByPo : FmpBasePo
    {
        public HousingNearByPo(IWebDriver driver) : base(driver)
        {
        }

        //Housing NearBy Jobs Cards
        private readonly By ViewAllHousingLink = By.XPath("//div[contains(@class,'HousingDeckWrapper')]//a[contains(@class,'ViewAll')]");
        private readonly By BookOnFurnishedFinderCardHref = By.XPath("//div[contains(@class,'slick-current')]/div/div[1]//span[text()='Book on Furnished Finder']/parent::a");
        private readonly By MoreDetailsButton = By.XPath("//div[contains(@class,'slick-current')]/div/div[1]//button/span[text()='More Details']");
        private readonly By MoreDetailsPopupFurnishedFinderCardHref = By.XPath("//div[contains(@class,'slick-current')]/div/div[1]//div[contains(@class,'SideDetailsContent')]/a");
        private readonly By LessDetailsText = By.XPath("//div[contains(@class,'slick-current')]/div//div[1]//span[text()='Less Details']");
        private readonly By NearbyHousingHeaderText = By.XPath("//div[contains(@class,'HousingDeckWrapper')]/div/div/h2");
        private readonly By JobCardHeaderText = By.XPath("//div[contains(@class,'indexstyles__JobsDeckWrapper')]/div/div/h2");
        private static By NthNearbyHousingCard(int index) => By.XPath($"//div[contains(@class,'slick-track')]//div[@data-index='{index}']/div/div[contains(@class,'HousingCardWrapper')]");
        private readonly By NearbyHousingCardNextButton = By.XPath("//div[contains(@class,'HousingDeckWrapper')]//button[contains(@class,'slick-arrow slick-next')]");
        private readonly By NearbyHousingCardPreviousButton = By.XPath("//div[contains(@class,'HousingDeckWrapper')]//button[contains(@class,'slick-arrow slick-prev')]");
        private static By NearbyHousingCardsClickDots(int index) => By.XPath($"//div[contains(@class,'HousingDeckWrapper')]//ul[@class='slick-dots']/li[{index}]");
        private readonly By FurnishedFinderCardButton = By.XPath("//div[contains(@class,'HousingCardWrapper')][1]//span[text()='Book on Furnished Finder']/parent::button");
        private readonly By CardBookOnFurnishedFinderLink = By.XPath("//div[contains(@class,'SideDetails')]/div/div/following-sibling::button");
        private const string FurnishedFinderPageTitle = "Furnished Finder";

        public bool IsNearbyHousingPresent()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(JobCardHeaderText));
            return Wait.IsElementPresent(NearbyHousingHeaderText, 3);
        }

        public bool IsNthNearbyHousingCardPresent(int index)
        {
            return Wait.IsElementDisplayed(NthNearbyHousingCard(index), 3);
        }

        public void ClickOnhNearbyHousingCardNextButton()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(JobCardHeaderText));
            Wait.UntilElementClickable(NearbyHousingCardNextButton).ClickOn();
            Wait.HardWait(1000);
        }

        public void ClickOnhNearbyHousingCardPreviousButton()
        {
            Wait.UntilElementClickable(NearbyHousingCardPreviousButton).ClickOn();
            Wait.HardWait(1000);
        }

        public string GetBookOnFurnishedFinderCardHref()
        {
            return Wait.UntilElementVisible(BookOnFurnishedFinderCardHref).GetAttribute("href");
        }

        public void ClickOnMoreDetailsButton()
        {
            Wait.UntilElementClickable(MoreDetailsButton).ClickOn();
            Wait.UntilElementInVisible(MoreDetailsButton);
        }

        public string GetMoreDetailsPopupFurnishedFinderCardHref()
        {
            return Wait.UntilElementVisible(MoreDetailsPopupFurnishedFinderCardHref).GetAttribute("href");
        }

        public bool IsLessDetailsTextPresent()
        {
            return Wait.IsElementPresent(LessDetailsText, 3);
        }

        public void ClickOnLessDetailsTextButton()
        {
            Wait.UntilElementClickable(LessDetailsText).ClickOn();
        }

        public void ClickOnNearbyHousingCardsClickDots(int index)
        {
            Wait.UntilElementClickable(NearbyHousingCardsClickDots(index)).ClickOn();
            Wait.HardWait(1000);
        }

        //Housing NearBy Jobs Cards
        public void ClickOnViewAllHousingLink()
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(JobCardHeaderText));
            Wait.UntilElementClickable(ViewAllHousingLink).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public string ClickOnBookOnFurnishedFinderButtonAndGetUrl()
        {
            Driver.JavaScriptClickOn(FurnishedFinderCardButton);
            Wait.HardWait(2000);
            Driver.SelectWindowByTitle(FurnishedFinderPageTitle);
            Wait.HardWait(1000);
            var expectedUrl = Driver.GetCurrentUrl();
            Driver.CloseWindowByTitle(FurnishedFinderPageTitle);
            Wait.HardWait(1000);
            Driver.SelectWindowByTitle("Fusion Marketplace");
            return expectedUrl;
        }
        public string ClickOnCardBookOnFurnishedFinderLinkAndGetUrl()
        {
            Wait.UntilElementVisible(CardBookOnFurnishedFinderLink);
            Driver.JavaScriptClickOn(Wait.UntilAllElementsLocated(CardBookOnFurnishedFinderLink).First(e => e.IsDisplayed()));
            Wait.HardWait(1000);
            Driver.SelectWindowByTitle(FurnishedFinderPageTitle);
            Wait.HardWait(1000);
            return Driver.GetCurrentUrl();
        }
    }
}
