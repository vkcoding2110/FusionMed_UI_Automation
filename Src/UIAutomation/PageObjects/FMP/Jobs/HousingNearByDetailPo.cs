using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Jobs
{
    internal class HousingNearByDetailPo : FmpBasePo
    {
        public HousingNearByDetailPo(IWebDriver driver) : base(driver)
        {
        }


        private readonly By HousingNearbyPageHeaderText = By.XPath("//div[contains(@class,'SubheaderWrapper')]/h2");
        private readonly By FilterDropDown = By.XPath("//div[contains(@class,'LocationRow')]//div[contains(@class,'FormControlStyled')]//div[@id='housing-filter']");
        private readonly By FilterDropDownValuesUtilitiesSectionText = By.XPath("//div[text()='Utilities']");
        private readonly By SortByDropDown = By.XPath("//div[contains(@class,'SelectGroupStyled')]//select[contains(@class,'MuiFilledInput-input')]");
        private static By SelectFilterOptionByMenuHeader(string menuHeader, string option) => By.XPath($"//div[contains(@class,'MenuItemSubheader')][text()='{menuHeader}']/following::li//span[text()='{option}']");
        private readonly By FilterFirstInput = By.XPath("//ul[@aria-labelledby='housing-filter-label']/li//input");
        private readonly By AllCards = By.XPath("//div[contains(@class,'HousingCardWrapper')]");
        private static By ExpandCardButton(int index) => By.XPath($"//div[contains(@class,'HousingCardWrapper')][{index}]//button[contains(@class,'DetailsButton')]");
        private readonly By FilterCancelButton = By.XPath("//div[contains(@class,'MultiSelectStyled')]//button[contains(@class,'IconButtonStyled')]");
        private static By FilterCheckBoxSelected(string menuHeader, string option) => By.XPath($"//div[contains(@class,'MenuItemSubheader')][text()='{menuHeader}']/following::li//span[text()='{option}']//parent::div//preceding-sibling::span//input");

        //Card Property
        private readonly By PetAllowedNotAllowedText = By.XPath("//div[contains(@class,'HousingCardWrapper')]//div[contains(@class,'SideDetailsContent')]/p[1]");
        private readonly By UtilitiesIncludeNotIncludeText = By.XPath("//div[contains(@class,'QuickInfoChip')][1]/span[contains(@class,'labelSmall')]");
        private readonly By BedRoomOrBathRoomAllowedText = By.XPath("//div[contains(@class,'HousingCardWrapper')]//div[contains(@class,'SideDetailsContent')]/p[2]");
        private readonly By FurnishedIncludeNotIncludeText = By.XPath("//div[contains(@class,'QuickInfoChip')][2]/span[contains(@class,'labelSmall')]");
        private readonly By MinimumStayText = By.XPath("//div[contains(@class,'HousingCardWrapper')]//div[contains(@class,'SideDetailsContent')]/p[4]");
        private readonly By TypeText = By.XPath("//div[contains(@class,'CardTitle')]/label[1]");
        private readonly By HousingNearbyLocationText = By.XPath("//div[contains(@class,'LocationRow')]/div/p");
        private readonly By BackToJobListingLink = By.XPath("//div[contains(@class,'ResultsRow')]/a");
        private readonly By PriceText = By.XPath("//div[contains(@class,'CardTitle')]//label[@class='text-right']");
        private readonly By AvailableDateText = By.XPath(" //div[contains(@class,'HousingCardWrapper')]//div[contains(@class,'SideDetailsContent')]/p[3]");
        public void SelectFilterOption(string menuHeader, IList<string> optionList)
        {
            if (BaseTest.PlatformName == PlatformName.Web)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(HousingNearbyPageHeaderText));
            }
            Wait.HardWait(2000); // To avoid flakiness
            Wait.UntilElementClickable(FilterDropDown).ClickOn();
            foreach (var option in optionList)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(SelectFilterOptionByMenuHeader(menuHeader, option))).ClickOn();
            }

            if (BaseTest.PlatformName == PlatformName.Ios)
            {
                Wait.UntilElementExists(FilterDropDownValuesUtilitiesSectionText).PressEscKey();
            }
            else
            {
                Wait.UntilElementExists(FilterFirstInput).PressEscKey();
            }
        }
        public void SelectSortByFilterOption(string option)
        {
            if (BaseTest.PlatformName == PlatformName.Web)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(HousingNearbyPageHeaderText));
            }
            Wait.HardWait(2000); // To avoid flakiness
            Wait.UntilElementClickable(SortByDropDown).SelectDropdownValueByText(option, Driver);
        }

        public void ExpandAllCardDetails()
        {
            var cardCount = Wait.UntilAllElementsLocated(AllCards).Where(e => e.Displayed).ToList().Count();
            for (var i = 1; i <= cardCount; i++)
            {
                if (BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Safari))
                {
                    Driver.JavaScriptClickOn(Wait.UntilElementClickable(ExpandCardButton(i)));
                }
                else
                {
                    Wait.UntilElementClickable(ExpandCardButton(i)).ClickOn();
                }
            }
        }

        //Card Property
        public IList<string> GetPetAllowedNotAllowedTextFromAllCard()
        {
            return Wait.UntilAllElementsLocated(PetAllowedNotAllowedText).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }
        public IList<string> GetUtilitiesTextFromAllCard()
        {
            return Wait.UntilAllElementsLocated(UtilitiesIncludeNotIncludeText).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }
        public IList<string> GetFurnishedTextFromAllCard()
        {
            return Wait.UntilAllElementsLocated(FurnishedIncludeNotIncludeText).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }
        public IList<string> GetMinimumStayTextFromAllCard()
        {
            return Wait.UntilAllElementsLocated(MinimumStayText).Where(e => e.Displayed).Select(e => e.GetText().Replace("Minimum Stay:", "").Replace("days", "").RemoveWhitespace()).ToList();
        }
        public IList<string> GetTypeTextFromAllCard()
        {
            return Wait.UntilAllElementsLocated(TypeText).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }
        public IList<string> GetBedRoomOrBathRoomTextFromAllCard()
        {
            return Wait.UntilAllElementsLocated(BedRoomOrBathRoomAllowedText).Where(e => e.Displayed).Select(e => e.GetText()).ToList();
        }
        public string GetHousingNearbyLocationText()
        {
            return Wait.UntilElementVisible(HousingNearbyLocationText).GetText();
        }
        public IList<string> GetPriceListFromAllCard()
        {
            return Wait.UntilAllElementsLocated(PriceText).Where(e => e.Displayed).Select(e => e.GetText().Replace("$", "").Replace("/month", "").Replace("\r\n", "").Replace(",", "")).Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public IList<DateTime> GetAvailableDateTextFromCard()
        {
            return Wait.UntilAllElementsLocated(AvailableDateText).Select(e => DateTime.ParseExact(e.GetText().Replace("Available Date: ", ""), "MMMM d, yyyy", CultureInfo.InvariantCulture)).ToList();
        }
        public string GetHousingNearbyPageHeaderText()
        {
            return Wait.UntilElementVisible(HousingNearbyPageHeaderText).GetText();
        }

        public void ClickOnBackToJobListingLink()
        {
            Wait.UntilElementClickable(BackToJobListingLink).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void ClickOnFilterCancelButton()
        {
            Wait.HardWait(2000); // To avoid flakiness
            Wait.UntilElementClickable(FilterCancelButton).ClickOn();
            Wait.UntilElementInVisible(FilterCancelButton, 3);
        }

        public string SelectSortByFilterOption()
        {
            return Wait.UntilElementClickable(SortByDropDown).SelectDropdownGetSelectedValue();
        }

        public void ClickOnFilterDropDown()
        {
            Wait.HardWait(2000); // To avoid flakiness
            Wait.UntilElementExists(FilterDropDown).ClickOn();
            Wait.HardWait(2000); // To avoid flakiness
        }

        public bool IsFilterOptionSelected(string menuHeader, string option)
        {
            return Wait.UntilElementExists(FilterCheckBoxSelected(menuHeader, option)).IsElementSelected();
        }

        public void CloseFilterDropdown()
        {
            Wait.UntilElementExists(FilterFirstInput).PressEscKey();
            Wait.UntilElementInVisible(FilterFirstInput);
        }
        public void MoveToSortByFilter()
        {
            if (BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                Driver.MoveToElement(Wait.UntilElementExists(FilterDropDown));
            }
        }
    }
}
