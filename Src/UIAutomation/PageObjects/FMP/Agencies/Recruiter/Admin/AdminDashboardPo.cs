using OpenQA.Selenium;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter.Admin
{
    internal class AdminDashboardPo : FmpBasePo
    {
        public AdminDashboardPo(IWebDriver driver) : base(driver)
        {
        }

        private static By ReviewsSubMenuTabs(string item) => By.XPath($"//div[contains(@class,'SubMenuTabs')]/div/div/button//span[text()='{item}']");
        private readonly By ReviewsDropDown = By.XPath("//div[contains(@class,'formControl')]/select");
        private static By ButtonName(string buttonName) => By.XPath($"//div[contains(@class,'ActionRowWrapper')]//button//span[text()='{buttonName}']");

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FusionMarketPlaceUrl}admin/");
            WaitUntilFmpPageLoadingIndicatorInvisible();
            WaitUntilFmpTextLoadingIndicatorInvisible();
        }

        public void ClickOnReviewsSubMenuTabs(string item)
        {
            Wait.UntilElementClickable(ReviewsSubMenuTabs(item)).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void SelectDropDownValue(string item)
        {
            Wait.UntilElementClickable(ReviewsDropDown).SelectDropdownValueByText(item);
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public string GetSelectedDropDownValue()
        {
            return Wait.UntilElementVisible(ReviewsDropDown).SelectDropdownGetSelectedValue();
        }

        public void ClickOnButton(string buttonName)
        {
            Wait.UntilElementClickable(ButtonName(buttonName)).ClickOn();
        }
    }
}