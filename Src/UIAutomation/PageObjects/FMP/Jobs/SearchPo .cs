using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Jobs
{
    internal class SearchPo : FmpBasePo
    {
        public SearchPo(IWebDriver driver) : base(driver)
        {
        }

        public static string DeepSeaBlueForWindow = "rgba(34, 46, 98, 0.12)";
        public static string LightSeaGreenForMac = "rgb(34, 176, 191)";

        private readonly By ViewAllJobsPageHeaderText = By.XPath("//div[contains(@class,'FilterTagWrapper')]//span[@class='MuiChip-label']");
        private readonly By SortAndFilterButton = By.XPath("//span[@class='MuiButton-label' and contains(text(),'Filter')]");
        private readonly By JobsPageFilterText = By.XPath("//div/following-sibling::div/span[contains(@class,'MuiChip-label')]");
        private readonly By JobsRelatedToMeText = By.CssSelector("div[class*='SearchResultsWrapper'] div[class*='JobsDeckWrapper'] div h2");
        private readonly By JobNameText = By.XPath("//div[contains(@class,'SearchResultsWrapper')]//div[contains(@class,'JobsDeckWrapper')]//div[contains(@class,'slick-slide')]//div[contains(@class,'JobName')]");

        //Job Card Details
        private readonly By JobCardJobTitle = By.XPath("//div[contains(@class,'JobCardBody')]//div[contains(@class,'JobName')]");
        private readonly By JobCardJobLocation = By.XPath("//div[contains(@class,'JobCardBody')]//div[contains(@class,'Location')]");
        private readonly By JobCardButton = By.XPath("//div[contains(@class,'JobCardFooter')]//button");
        private readonly By AgencyCardAgencyName = By.XPath("//div[@class='agency-info']//div[contains(@class,'JobName')]");
        private readonly By JobCardAgencyName = By.XPath("//div[contains(@class,'JobCardFooter')]/div[contains(@class,'Agency')]");
        private static By SearchResultWeeklyPayCard(string min, string max) => By.XPath($"//span[contains(@class,'MuiChip-label')][text()='${min} - ${max}']");
        private readonly By JobCardFooterQuickOrReviewAgencyOptionButton = By.XPath("//button[contains(@class,'FooterButton')]//span[text()='Quick Apply'] | //a[contains(@class,'FooterButton')]//span[text()='Review Options']");
        public static By JobCardMultipleAgencyName(int index) => By.XPath($"//div[contains(@class,'JobCardContainer')][{index}]//div[contains(@class,'AgencyName')][contains(text(),'Multiple')]");
        private readonly By JobCard = By.XPath("//ul[@class='slick-dots']//preceding-sibling::div[@class='slick-list']//button//parent::div//preceding-sibling::a[contains(@class,'MuiCardActionArea')]");
        private readonly By FirstJobCardTitle = By.XPath("//section//div[contains(@class,'JobName')]");
        private readonly By FirstQuickApplyButton = By.XPath("//div[contains(@class,'slick-active slick-current')]//span[text()='Quick Apply']//parent::button");
        private readonly By QuickApplyButton = By.CssSelector("button[class*='FooterButton']");
        private readonly By JobsRecentlyAddedNextButton = By.CssSelector("button[class*='slick-arrow slick-next'] svg");
        private readonly By JobsRecentlyAddedPreviousButton = By.CssSelector("button[class*='slick-arrow slick-prev'] svg");
        private readonly By FirstJobCardLocation = By.XPath("//div[contains(@class,'slick-active slick-current')]//div[contains(@class,'JobCardBody')]//div[contains(@class,'JobLocation')]");
        private readonly By FirstJobCardHours = By.XPath("//div[contains(@class,'slick-active slick-current')]//div[contains(@class,'JobCardBody')]//div[contains(@class,'PayAndHoursRow')]//div[contains(@class,'PayAndHoursColumn')][1]/div[2]");
        private readonly By FirstJobCardPayAmount = By.XPath("//div[contains(@class,'slick-active slick-current')]//div[contains(@class,'JobCardBody')]//div[contains(@class,'PayAndHoursRow')]//div[contains(@class,'PayAndHoursColumn')][2]");
        private readonly By FirstJobCardNoPayPackage = By.XPath("//div[contains(@class,'slick-active slick-current')]//span[contains(text(),'Please contact us')]");
        private static By JobCards(int index) => By.XPath($"//div[contains(@class,'JobsDeckCarouselStyled')]//button[contains(@class,'slick-prev')]//following-sibling::div//div[@data-index='{index}'][@aria-hidden='false']");
        //Pagination 
        private readonly By PaginationBackButton = By.XPath("//div[contains(@class,'PaginationWrapper')]//ul/li[1]/button");
        private static By PaginationNthButton(int nthButton) => By.XPath($"//div[contains(@class,'PaginationWrapper')]//ul//li/button[text()='{nthButton}']");
        private readonly By PaginationNextButton = By.XPath("//button[@aria-label='Go to next page']");
        private readonly By PaginationLastButton = By.XPath("//button[@aria-label='Go to next page']/parent::li/preceding-sibling::li[1]/button");

        //SelectCategoryAndSpecialty
        private static readonly By CategoryDropDown = By.XPath($"//div[contains(@class,'FormControlStyled')]/label[text()='Category']/following-sibling::div");
        private static readonly By SpecialtyDropDown = By.XPath($"//div[contains(@class,'FormControlStyled')]/label[text()='Specialty']/following-sibling::div");
        private static By SelectFilterOptions(string option) => By.XPath($"//div[contains(@class,'MuiPopover-paper')]//span/following-sibling::div/span[text()='{option}']");
        private static By CategoryAndSpecialtyOption(string option) => By.XPath($"//span[text()='{option}']/parent::div/parent::li");
        private readonly By SearchResultCategoryAndSpecialty = By.XPath("//div[contains(@class,'clickableColorPrimary')]/span[1]");

        //Job Quantity
        private static By JobQuantity(int minQuantity, int maxQuantity) => By.XPath($"//span[contains(@class,'MuiChip-label')][text()='Jobs Available: {minQuantity} - {maxQuantity}']");
        private static By NthJobCard(int index) => By.XPath($"//div[contains(@class,'SearchResultsSingle')]//div[contains(@class,'JobCardContainer')][{index}]/a");
        //Device Elements
        private readonly By ViewAllJobsPageHeaderTextDevice = By.XPath("//div[contains(@class,'FilterTagWrapper')]//span[@class='MuiChip-label MuiChip-labelSmall']");

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FusionMarketPlaceUrl}search/");
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public void ClickOnJobCard()
        {
            WaitUntilFirstJobCardTitleGetDisplayed();
            Wait.UntilElementExists(JobCard);
            Wait.UntilAllElementsLocated(JobCard).First(x => x.IsDisplayed()).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public void WaitUntilFirstJobCardTitleGetDisplayed()
        {
            Wait.WaitUntilTextRefreshed(FirstJobCardTitle);
        }
        public void ClickOnFirstQuickApplyButton()
        {
            Wait.UntilAllElementsLocated(FirstQuickApplyButton).First(e => e.Displayed).ClickOn();
        }

        public void ClickOnQuickApplyButton()
        {
            Wait.UntilAllElementsLocated(QuickApplyButton).First(e => e.Displayed).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public bool IsNthJobCardPresent(int index)
        {
            return Wait.IsElementPresent(JobCards(index),3);
        }
        public string GetJobCardJobTitle()
        {
            //Refreshing page if `Quick Apply` button isn't available
            if (!Wait.IsElementPresent(FirstQuickApplyButton, 3))
            {
                Driver.RefreshPage();
            }
            if (!BaseTest.PlatformName.Equals(PlatformName.Web))
            {
                for (var i = 0; i < 11; i++)
                {
                    if (!Wait.IsElementPresent(FirstQuickApplyButton, 3))
                    {
                        ClickOnJobsRecentlyAddedNextButton();
                    }
                    else
                    {
                        return Wait.UntilAllElementsLocated(JobCardJobTitle).First(x => x.IsDisplayed()).GetText();
                    }
                }
            }
            else
            {
                return Wait.UntilAllElementsLocated(JobCardJobTitle).First(x => x.IsDisplayed()).GetText();
            }
            return null;
        }
        public void ClickOnJobsRecentlyAddedNextButton()
        {
            Wait.UntilElementClickable(JobsRecentlyAddedNextButton).ClickOn();
        }

        public void ClickOnJobsRecentlyAddedPreviousButton()
        {
            Wait.UntilElementClickable(JobsRecentlyAddedPreviousButton).ClickOn();
        }
        public string GetJobCardAgencyName()
        {
            if (!Wait.IsElementPresent(FirstQuickApplyButton, 3))
            {
                Driver.RefreshPage();
            }
            if (!BaseTest.PlatformName.Equals(PlatformName.Web))
            {
                for (var i = 0; i < 11; i++)
                {
                    if (!Wait.IsElementPresent(FirstQuickApplyButton, 3))
                    {
                        ClickOnJobsRecentlyAddedNextButton();
                    }
                    else
                    {
                        return Wait.UntilAllElementsLocated(JobCardAgencyName).First(x => x.IsDisplayed()).GetText();
                    }
                }
            }
            else
            {
                return Wait.UntilAllElementsLocated(JobCardAgencyName).First(x => x.IsDisplayed()).GetText();
            }
            return null;
        }
        public string GetJobLocation()
        {
            return Wait.UntilAllElementsLocated(FirstJobCardLocation).First(x => x.IsDisplayed()).GetText();
        }
        public string GetJobHours()
        {
            return IsJobHourPresent() ? Wait.UntilAllElementsLocated(FirstJobCardHours).First(x => x.IsDisplayed()).GetText().RemoveEndOfTheLineCharacter().RemoveWhitespace() : null;
        }
        public string GetJobPayAmount()
        {
            return !IsJobPayAmountPresent() ? Wait.UntilAllElementsLocated(FirstJobCardPayAmount).First(x => x.IsDisplayed()).GetText().RemoveEndOfTheLineCharacter().RemoveWhitespace() : null;
        }
        public bool IsJobPayAmountPresent()
        {
            return Wait.IsElementPresent(FirstJobCardNoPayPackage, 5);
        }

        public bool IsJobHourPresent()
        {
            return Wait.IsElementPresent(FirstJobCardHours, 5);
        }

        public IList<string> GetAllJobCardTitle()
        {
            Wait.UntilElementVisible(JobCardJobTitle, 30);
            return Wait.UntilAllElementsLocated(JobCardJobTitle).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public IList<string> GetAllJobCardLocation()
        {
            Wait.UntilElementVisible(JobCardButton);
            return Wait.UntilAllElementsLocated(JobCardJobLocation).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public IList<string> GetAllJobCardAgencyName()
        {
            Wait.UntilElementVisible(JobCardAgencyName);
            return Wait.UntilAllElementsLocated(JobCardAgencyName).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public IList<string> GetAllAgencyNameFromAgencyCard()
        {
            Wait.UntilElementVisible(AgencyCardAgencyName);
            return Wait.UntilAllElementsLocated(AgencyCardAgencyName).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public void ClickOnSortAndFilterButton()
        {
            try
            {
                Wait.UntilElementClickable(SortAndFilterButton).ClickOn();
            }
            catch (Exception)
            {
                Wait.HardWait(4000);
                Wait.UntilElementClickable(SortAndFilterButton).ClickOn();
            }
            Wait.WaitTillElementCountIsLessThan(SortAndFilterButton, 1);
        }

        public bool IsSalaryCardPresentOnSearchResultPage(string min, string max)
        {
            return Wait.IsElementPresent(SearchResultWeeklyPayCard(min, max));
        }

        public void WaitUntilJobCardVisible()
        {
            Wait.HardWait(1000);
            Wait.UntilElementVisible(JobCardFooterQuickOrReviewAgencyOptionButton);
        }

        //View all jobs
        public string GetViewAllJobsPageHeaderText()
        {
            return Wait.UntilElementVisible(BaseTest.PlatformName != PlatformName.Web ? ViewAllJobsPageHeaderTextDevice : ViewAllJobsPageHeaderText).GetText();
        }

        public bool IsPaginationBackButtonEnabled()
        {
            return Wait.IsElementEnabled(PaginationBackButton);
        }

        public bool IsPaginationNthButtonSelected(int nthButton)
        {
            WaitUntilJobCardVisible();
            Wait.HardWait(1000);
            var colorProperty = Wait.UntilElementExists(PaginationNthButton(nthButton)).GetCssValue("background-color");
            return colorProperty.Equals(DeepSeaBlueForWindow);
        }

        public void ClickOnPaginationNextButton()
        {
            Wait.UntilElementVisible(PaginationNextButton);
            Wait.UntilElementClickable(PaginationNextButton).ClickOn();
            WaitUntilJobCardVisible();
        }

        public void ClickOnPaginationNthButton(int nthButton)
        {
            Wait.UntilElementVisible(PaginationNthButton(nthButton));
            Wait.UntilElementClickable(PaginationNthButton(nthButton)).ClickOn();
            WaitUntilJobCardVisible();
        }

        public void ClickOnPaginationBackButton()
        {
            Wait.UntilElementVisible(PaginationBackButton);
            Wait.UntilElementClickable(PaginationBackButton).ClickOn();
            WaitUntilJobCardVisible();
        }

        public void ClickOnPaginationLastButton()
        {
            Wait.UntilElementClickable(PaginationLastButton);
            Driver.JavaScriptClickOn(PaginationLastButton);
            WaitUntilJobCardVisible();
        }

        public bool IsPaginationLastButtonSelected()
        {
            WaitUntilJobCardVisible();
            var colorProperty = Wait.UntilElementExists(PaginationLastButton).GetCssValue("background-color");
            return colorProperty.Equals(DeepSeaBlueForWindow);
        }

        public bool IsPaginationNextButtonEnabled()
        {
            return Wait.IsElementEnabled(PaginationNextButton);
        }

        public void ClickOnCategoryDropDown()
        {
            Wait.UntilElementVisible(CategoryDropDown);
            Wait.UntilElementClickable(CategoryDropDown).ClickOn();
        }

        public void ClickOnSpecialtyDropDown()
        {
            Wait.UntilElementVisible(SpecialtyDropDown);
            Wait.UntilElementClickable(SpecialtyDropDown).ClickOn();
        }

        public void SelectCategoryAndSpecialtyOptions(IList<string> categoryMenus)
        {
            foreach (var item in categoryMenus)
            {
                Wait.UntilElementVisible(SelectFilterOptions(item));
                Wait.UntilElementClickable(SelectFilterOptions(item)).ClickOn();
            }
            Wait.UntilElementVisible(CategoryAndSpecialtyOption(categoryMenus.First())).SendKeys(Keys.Escape);
            Wait.UntilElementInVisible(SelectFilterOptions(categoryMenus.First()));
        }

        public IList<string> GetSearchPageCriteria()
        {
            Wait.UntilElementVisible(SearchResultCategoryAndSpecialty);
            return Wait.UntilAllElementsLocated(SearchResultCategoryAndSpecialty).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public string GetJobQuantity(int minQuantity, int maxQuantity)
        {
            return Wait.UntilElementVisible(JobQuantity(minQuantity, maxQuantity)).GetText();
        }

        public void ClickOnNthJobCardCard(int index)
        {
            Wait.UntilElementClickable(NthJobCard(index)).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public string GetJobsPageFilterText()
        {
            return Wait.UntilElementVisible(JobsPageFilterText, 10).GetText();
        }

        public string GetJobsRelatedToMeText()
        {
            return Wait.UntilElementVisible(JobsRelatedToMeText).GetText();
        }

        public IList<string> GetJobNameFromCards()
        {
            return Wait.UntilAllElementsLocated(JobNameText).Where(e => e.Displayed).Select(e => e.GetText().ToLowerInvariant()).ToList();
        }
    }
}
