using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter
{
    internal class RecruiterListingPo : FmpBasePo
    {
        public RecruiterListingPo(IWebDriver driver) : base(driver)
        {
        }
        private readonly By FirstCard = By.XPath("//div[contains(@class,'CardsContainer')]/div//div[contains(@class,'RecruiterInfo')]/h3");
        private readonly By AgencyName = By.XPath("//div[contains(@class,'CardsContainer')]/div//h5");
        private readonly By FilterTag = By.XPath("//div/div[contains(@class,'FilterTagWrapper')][2]/span");
        private readonly By RecruiterName = By.XPath("//div[contains(@class,'CardsContainer')]/div[1]//h3");
        private readonly By SpecialtyName = By.XPath("//div[contains(@class,'CardsContainer')]/div[1]//h4");
        private readonly By SearchResultsCount = By.XPath("//div[contains(@class,'SearchResultsCount')]/span");
        private static By RecruiterCard(string recruiter) => By.XPath($"//div[contains(@class,'CardWrapper')]//div[contains(@class,'RecruiterInfo')]/h3[text()='{recruiter}']");
        public void WaitUntilRecruiterCardVisible()
        {
            Wait.UntilElementVisible(FirstCard, 10);
        }
        public IList<string> GetAllAgencyNamesFromRecruiterCards()
        {
            Wait.UntilElementVisible(AgencyName, 40);
            return Wait.UntilAllElementsLocated(AgencyName).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }
        public string GetRecruiterFilterTagName()
        {
            return Wait.UntilElementVisible(FilterTag).GetText();
        }
        public void ClickOnFirstRecruiterCard()
        {
            Wait.UntilElementClickable(FirstCard).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public string GetFirstRecruiterSpecialty()
        {
            return Wait.UntilElementVisible(SpecialtyName).GetText();
        }
        public string GetFirstRecruiterName()
        {
            return Wait.UntilElementVisible(RecruiterName).GetText().ToLowerInvariant();
        }
        public string GetFirstRecruiterAgencyName()
        {
            return Wait.UntilElementVisible(AgencyName).GetText().ToLowerInvariant();
        }
        public string GetSearchResultsCount()
        {
            Wait.HardWait(4000);
            return Wait.UntilElementVisible(SearchResultsCount, 20).GetText().Replace("results", "");
        }
        public void ClickOnRecruiterCard(string recruiter)
        {
            Wait.UntilElementClickable(RecruiterCard(recruiter)).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
    }
}
