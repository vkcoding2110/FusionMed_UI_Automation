using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter.Admin
{
    internal class RecruitersListPo : FmpBasePo
    {
        public RecruitersListPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By RecruiterHeaderText = By.XPath("//section[contains(@class,'SectionWrapper')]//div[contains(@class,'SubheaderWrapper')]/h2");
        private readonly By InviteRecruiterButton = By.XPath("//div[contains(@class,'ActionRowWrapper')]//button/span[text()='Invite Recruiter']");
        private readonly By DepartmentHeaderText = By.XPath("//div[@class='header-text'][text()='Department']");
        private static By DepartmentHelperText(int rowIndex) => By.XPath($"//div[@class='tbody']/div[@role='row'][{rowIndex}]//div[contains(@class,'DepartmentHelper')]");
        private static By RecruiterByNthRowNthColumn(int rowIndex, int columnIndex) => By.XPath($"//div[@class='tbody']/div[@role='row'][{rowIndex}]/div[@role='cell'][{columnIndex}]");
        private static By RecruiterByNthColumn(int columnIndex) => By.XPath($"//div[@class='tbody']//div[@role='row']/div[@role='cell'][{columnIndex}]");
        private static By ActionsButton(int rowIndex) => By.XPath($"//div[@class='tbody']/div[@role='row'][{rowIndex}]//div[contains(@class,'ButtonCell')]/button");
        private readonly By EditButton = By.XPath("//div[contains(@class,'TooltipStyled')]//button/span[text()='Edit']");
        private readonly By InnerWrapperPopUpHeaderText = By.CssSelector("div[class*='InnerWrapper'] h5");
        private readonly By InnerWrapperPopUpMessage = By.CssSelector("div[class*='InnerWrapper'] p");
        private readonly By InnerWrapperCancelButton = By.XPath("//div[contains(@class,'InnerWrapper')]//button/span[text()='cancel']");
        private readonly By DeleteButton = By.XPath("//div[contains(@class,'TooltipStyled')]//button/span[text()='Delete']");
        private readonly By DeleteRecruiterButton = By.XPath("//div[contains(@class,'InnerWrapper')]//button/span[text()='Delete Recruiter']");

        private readonly By RecruiterImage = By.XPath("//div[@class='tbody']//div[contains(@class,'AvatarContainer')]//img");
        //Filter Recruiter
        private static By FilterToggleIcon(string filterText) => By.XPath($"//div[contains(@class,'thead')]//div[text()='{filterText}']//following-sibling::div/button[contains(@class,'IconButtonStyled')]");
        private readonly By DepartmentMenu = By.XPath("//div[contains(@class,'EditSelect')]//div[contains(@class,'selectMenu')]");
        private static By DepartmentOption(string text) => By.XPath($"//ul[contains(@class,'MuiMenu-list')]//*[text()='{text}']");
        private readonly By ClearFiltersButton = By.XPath("//div[contains(@class,'ActionRowWrapper')]//span[text()='Clear Filters']/parent::button");

        //View Full Screen, Minimize & Download All Review Links button
        private static By ButtonName(string buttonName) => By.XPath($"//div[contains(@class,'ActionRowWrapper')]//button//span[text()='{buttonName}']");

        //SortBy
        private static By SortByButton(string text) => By.XPath($"//div[contains(@role,'columnheader')]//div[text()='{text}']//preceding-sibling::div/button");

        // Pagination
        private readonly By NumberOfRowPerPage = By.XPath("//div[@role='rowgroup']//div[@role='row']");
        private readonly By RowPerPageDropDown = By.XPath("//select[@aria-label='rows per page']");
        private readonly By LastPageIcon = By.XPath("//button[@aria-label='last page']");
        private readonly By NextPageIcon = By.XPath("//button[@aria-label='next page']");
        private readonly By FirstPageIcon = By.XPath("//button[@aria-label='first page']");
        private readonly By PreviousPageIcon = By.XPath("//button[@aria-label='previous page']");

        public void ClickOnLastPageIcon()
        {
            Wait.UntilElementClickable(LastPageIcon).ClickOn();
        }

        public bool IsLastPageIconDisabled()
        {
            return Wait.IsElementEnabled(LastPageIcon, 3);
        }
        public bool IsNextPageIconDisabled()
        {
            return Wait.IsElementEnabled(NextPageIcon, 3);
        }
        public void ClickOnFirstPageIcon()
        {
            Wait.UntilElementClickable(FirstPageIcon).ClickOn();
        }
        public bool IsFirstPageIconDisabled()
        {
            return Wait.IsElementEnabled(FirstPageIcon, 3);
        }
        public bool IsPreviousPageIconDisabled()
        {
            return Wait.IsElementEnabled(PreviousPageIcon, 3);
        }

        public bool IsRecruiterProfileImagePresent()
        {
            return Wait.IsElementPresent(RecruiterImage, 5);
        }
        public void SelectRowPerPageDropDown(string row)
        {
            Wait.UntilElementClickable(RowPerPageDropDown).SelectDropdownValueByText(row);
        }
        public List<string> GetColumnData(int columnIndex)
        {
            var gridSize = Convert.ToInt32(Wait.UntilElementVisible(RowPerPageDropDown).SelectDropdownGetSelectedValue());
            var elementList = new List<IWebElement>();
            var columnList = new List<string>();
            do
            {
                Wait.HardWait(1000);
                var elements = Wait.UntilAllElementsLocated(NumberOfRowPerPage).Where(e => e.Displayed).ToList();
                for (var i = 1; i <= elements.Count; i++)
                {
                    columnList.Add(Wait.UntilElementVisible(RecruiterByNthRowNthColumn(i, columnIndex)).GetText());
                    elementList.Add(elements[i - 1]);
                }
                Driver.JavaScriptScrollToElement(elementList[^1]);
                columnList = columnList.Distinct().ToList();
            } while (columnList.Count < gridSize);
            return columnList;
        }
        public string GetRecruiterHeaderText()
        {
            return Wait.UntilElementVisible(RecruiterHeaderText).GetText();
        }

        public void ClickOnInviteRecruiterButton()
        {
            Wait.UntilElementClickable(InviteRecruiterButton).ClickOn();
        }

        public void SearchRecruitersByFilter(string filterText, string recruiters)
        {
            Driver.JavaScriptClickOn(FilterToggleIcon(filterText));
            var element = (IWebElement)((IJavaScriptExecutor)Driver).ExecuteScript("return document.querySelector(\"input[placeholder='Text Contains']\")");
            element.SendKeys(recruiters);
            element.SendKeys(Keys.Enter);
        }

        public void SearchRecruitersByDepartmentFilter(string filterText, string recruiters)
        {
            Driver.JavaScriptClickOn(FilterToggleIcon(filterText));
            Wait.UntilElementClickable(DepartmentMenu).ClickOn();
            Wait.UntilElementClickable(DepartmentOption(recruiters)).ClickOn();
        }



        public DataObjects.FMP.Agencies.Recruiter.Admin.Recruiter GetRecruiterDetails(int rowIndex)
        {
            var name = Wait.UntilElementVisible(RecruiterByNthRowNthColumn(rowIndex, 2)).GetText();
            var email = Wait.UntilElementVisible(RecruiterByNthRowNthColumn(rowIndex, 3)).GetText();
            List<string> department = null;
            if (Wait.IsElementPresent(DepartmentHeaderText, 3))
            {
                department = Wait.IsElementPresent(DepartmentHelperText(rowIndex), 5) ? (List<string>)new CSharpHelpers().StringToList(Wait.UntilElementVisible(DepartmentHelperText(rowIndex)).GetAttribute("title"), ',') : Wait.UntilAllElementsLocated(RecruiterByNthRowNthColumn(rowIndex, 5)).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
            }

            return new DataObjects.FMP.Agencies.Recruiter.Admin.Recruiter()
            {
                FirstName = name,
                Email = email,
                Department = department,

            };
        }

        public DateTime GetRecruiterDateText(int rowIndex)
        {
            return DateTime.ParseExact(Wait.UntilElementVisible(RecruiterByNthRowNthColumn(rowIndex, 4)).GetText(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }

        public void ClickOnActionsButton(int rowIndex)
        {
            Wait.HardWait(1000);
            Wait.UntilElementClickable(ActionsButton(rowIndex)).ClickOn();
        }

        public void ClickOnEditButton()
        {
            Wait.HardWait(1000);
            Wait.UntilElementClickable(EditButton).ClickOn();
        }

        public string GetInnerWrapperPopUpHeaderText()
        {
            return Wait.UntilElementVisible(InnerWrapperPopUpHeaderText).GetText();
        }

        public bool IsInnerWrapperPopUpHeaderTextDisplayed()
        {
            return Wait.IsElementPresent(InnerWrapperPopUpHeaderText, 10);
        }

        public string GetInnerWrapperPopUpMessage()
        {
            return Wait.UntilElementVisible(InnerWrapperPopUpMessage).GetText();
        }

        public void ClickOnInnerWrapperCancelButton()
        {
            Wait.UntilElementClickable(InnerWrapperCancelButton).ClickOn();
            Wait.UntilElementInVisible(InnerWrapperCancelButton, 5);
        }


        public void ClickOnDeleteButton()
        {
            Wait.UntilElementClickable(DeleteButton).ClickOn();
        }

        public void ClickOnDeleteRecruiterButton(int rowIndex)
        {
            ClickOnActionsButton(rowIndex);
            ClickOnDeleteButton();
            Wait.UntilElementClickable(DeleteRecruiterButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public bool IsRecruiterDisplayed(int rowIndex, int columnIndex)
        {
            return Wait.IsElementPresent(RecruiterByNthRowNthColumn(rowIndex, columnIndex), 5);
        }

        public void ClickOnClearFiltersButton()
        {
            Wait.UntilElementClickable(ClearFiltersButton).ClickOn();
        }

        public void ClickOnButton(string buttonName)
        {
            Wait.UntilElementClickable(ButtonName(buttonName)).ClickOn();
        }

        public bool IsButtonNameDisplayed(string buttonName)
        {
            return Wait.IsElementPresent(ButtonName(buttonName), 8);
        }

        public IList<string> GetRecruitersByNthColumn(int columnIndex)
        {
            return Wait.UntilAllElementsLocated(RecruiterByNthColumn(columnIndex)).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }

        public List<string> GetDepartmentColumnList(int columnIndex)
        {
            var elements = Wait.UntilAllElementsLocated(RecruiterByNthColumn(columnIndex)).Where(e => e.Displayed);
            var data = new List<string>();
            foreach (var e in elements)
            {
                var text = e.GetText();
                if (text.Contains('+'))
                {
                    text = e.FindElement(By.XPath("//div[contains(@class,'DepartmentHelper')]")).GetAttribute("title");
                }
                data.Add(text);
            }

            return data;
        }

        public bool IsClearFilterButtonEnabled()
        {
            return Wait.IsElementEnabled(ClearFiltersButton, 5);
        }

        public void ClickOnSortByButton(string text)
        {
            Wait.UntilElementClickable(SortByButton(text)).ClickOn();
        }
    }
}