using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.Explore
{
    internal class DepartmentsMenuPo : FmsBasePo
    {
        public DepartmentsMenuPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By ExploreButton = By.XPath("//div[contains(@class,'HeaderActionText')][text()='Explore']");
        private readonly By DepartmentsCloseMenuButton = By.XPath("//div[contains(@class,'browsestyles__BrowseHeader')]//div[3]//button");
        private static By DepartmentsMenuItem(string item) => By.XPath($"*//button[contains(@class,'navListItemstyles__ListItem')]//span[text()='{item}']");
        private readonly By DepartmentsMenuSubList = By.XPath("//button[contains(@class,'navListItemstyles__ListItem')]//span[contains(@class,'ListItemText')]");
        private readonly By DepartmentsMenuListHeader = By.XPath("//div[contains(@class,'browsestyles__BrowseHeader')]//div[2]/span");
        private readonly By DepartmentsMenuPreviousButton = By.CssSelector("div[class*='PreviousIcon'] svg");
        private readonly By JobsMenuButton = By.XPath("//div[contains(@class,'ViewSliderStyled')]//button/span[text()='Jobs']");
        private readonly By FusionCorporateLink = By.XPath("//a[contains(@class,'NavListItemStyled')]//span[text()='Fusion Corporate Careers']");

        //Device elements
        private readonly By ExploreMenuListItemDevice = By.XPath("//div[text()='Explore']");

        public void ClickOnExploreMenu()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                new HomePagePo(Driver).ClickOnMenuButton();
                Wait.UntilElementVisible(ExploreMenuListItemDevice).ClickOn();
            }
            else
            {
                Wait.UntilElementVisible(ExploreButton).ClickOn();
            }
            Wait.HardWait(1000);
        }

        public bool IsDepartmentsMenuOpened()
        {
            return Wait.IsElementPresent(DepartmentsCloseMenuButton);
        }

        public void ClickOnCloseDepartmentMenu()
        {
            Wait.UntilElementClickable(DepartmentsCloseMenuButton).ClickOn();
            Wait.UntilElementInVisible(DepartmentsCloseMenuButton);
        }


        public IList<string> GetJobMenuListItems()
        {
            Wait.UntilElementInVisible(JobsMenuButton, 5);
            return Wait.UntilAllElementsLocated(DepartmentsMenuSubList).Select(e => e.GetText()).ToList();
        }

        public void ClickOnDepartmentsMenuSubListItems(string item)
        {
            Wait.UntilElementClickable(DepartmentsMenuItem(item)).ClickOn();
            Wait.HardWait(2000);
        }

        public string GetDepartmentsMenuListHeaderText()
        {
            return Wait.UntilElementVisible(DepartmentsMenuListHeader).GetText();
        }

        public void ClickOnDepartmentMenuPreviousButton()
        {
            Wait.UntilElementClickable(DepartmentsMenuPreviousButton).ClickOn();
            Wait.HardWait(1000);
        }

        public void ClickOnJobsMenuButton()
        {
            Wait.UntilElementClickable(JobsMenuButton).ClickOn();
            Wait.UntilElementInVisible(JobsMenuButton);
        }

        public void ClickOnFusionCorporateCareersLink()
        {
            Wait.UntilElementClickable(FusionCorporateLink).ClickOn();
            WaitUntilMpPageLoadingIndicatorInvisible();
        }
    }
}
