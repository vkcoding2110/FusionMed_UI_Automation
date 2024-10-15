using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Agencies.Recruiter.Admin;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Agencies.Recruiter.Admin
{
    internal class AgencyAdminListPo : FmpBasePo
    {
        public AgencyAdminListPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By AgencyAdminTab = By.XPath("//span[text()='Agency Admins']//parent::button");
        private readonly By InviteAgencyAdminButton = By.XPath("//span[text()='Invite Agency Admin']//parent::button");
        private readonly By FirstNameTextBox = By.CssSelector("input[name='AgencyAdmins.0.firstName']");
        private readonly By LastNameTextBox = By.CssSelector("input[name='AgencyAdmins.0.lastName']");
        private readonly By EmailTextBox = By.CssSelector("input[name='AgencyAdmins.0.email']");
        private readonly By DeleteAgencyAdminButton = By.XPath("//span[text()='Delete Agency Admin']//parent::button");
        private readonly By InviteAgencyAdminCheckIcon = By.CssSelector("div[class*='ButtonCell'] button svg[class*='CheckIconStyled']");
        private readonly By DeleteButton = By.XPath("//div[contains(@class,'TooltipStyled')]//button/span[text()='Delete']");
        private readonly By InviteAgencyAdminValidationText = By.XPath("//div[contains(@class,'MuiPopover-paper')]");
        private readonly By InviteAgencyAdminValidationCloseIcon = By.XPath("//div[contains(@class,'MuiPopover-paper')]/button");
        private readonly By InviteAgencyAdminCloseIcon = By.CssSelector("div[class*='ButtonCell'] button svg[class*='CloseIconStyled']");
        private readonly By EditButton = By.XPath("//div[contains(@class,'TooltipStyled')]//button/span[text()='Edit']");
        private readonly By InnerWrapperPopUpHeaderText = By.CssSelector("div[class*='InnerWrapper'] h5");
        private readonly By InnerWrapperPopUpMessage = By.CssSelector("div[class*='InnerWrapper'] p");
        private readonly By InnerWrapperCancelButton = By.XPath("//div[contains(@class,'InnerWrapper')]//button/span[text()='cancel']");
        private readonly By DiscardChanges = By.XPath("//div[contains(@class,'InnerWrapper')]//button/span[text()='Discard Changes']");
        private readonly By RowPerPageDropDown = By.XPath("//select[@aria-label='rows per page']");
        private readonly By NumberOfRowPerPage = By.XPath("//div[@role='rowgroup']//div[@role='row']");
        private readonly By RowPerPage = By.XPath("//p[contains(@class,'TablePagination')][2]");
        private static By AgencyAdminByNthRowNthColumn(int rowIndex, int columnIndex) => By.XPath($"//div[@class='tbody']/div[@role='row'][{rowIndex}]/div[@role='cell'][{columnIndex}]");
        private static By ActionsButton(int rowIndex) => By.XPath($"//div[@class='tbody']/div[@role='row'][{rowIndex}]//div[contains(@class,'ButtonCell')]/button");
        private static By AgencyAdminByNthColumn(int columnIndex) => By.XPath($"//div[@class='tbody']/div[@role='row']/div[@role='cell'][{columnIndex}]");
        private static By SortByButton(string text) => By.XPath($"//div[contains(@role,'columnheader')]//div[text()='{text}']//preceding-sibling::div/button");
        private static By MaximizeMinimizeButton(string buttonName) => By.XPath($"//div[contains(@class,'ActionRowWrapper')]//button//span[text()='{buttonName}']");
        public void ClickOnAgencyAdminTab()
        {
            Wait.UntilElementClickable(AgencyAdminTab).ClickOn();
        }

        public int GetNumberOfRecordPresentOnGrid()
        {
            var rowPerPage = Wait.UntilElementVisible(RowPerPage).GetText();
            var numberOfRecords = rowPerPage.Split("of").Last().RemoveWhitespace();
            var numberOfRecordsPerPage = rowPerPage.Split("of").First().Split("-").Last();
            var actualNumberOfRecords = int.Parse(numberOfRecords);
            var actualNumberOfRecordsPerPage = int.Parse(numberOfRecordsPerPage);
            return actualNumberOfRecords <= actualNumberOfRecordsPerPage ? actualNumberOfRecords : actualNumberOfRecordsPerPage;
        }

        public void ClickOnInviteAgencyAdminButton()
        {
            Wait.UntilElementClickable(InviteAgencyAdminButton).ClickOn();
        }
        public void EnterAgencyAdminDetails(AgencyAdmin agencyAdmin)
        {
            Wait.UntilElementVisible(FirstNameTextBox).EnterText(agencyAdmin.FirstName, true);
            Wait.UntilElementVisible(LastNameTextBox).EnterText(agencyAdmin.LastName, true);
            if (Wait.IsElementEnabled(EmailTextBox, 3))
            {
                Wait.UntilElementVisible(EmailTextBox).EnterText(agencyAdmin.Email);
            }
            ClickOnInviteAgencyAdminCheckIcon();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public AgencyAdmin GetAgencyAdminDetails(int rowIndex)
        {
            var firstname = Wait.UntilElementVisible(AgencyAdminByNthRowNthColumn(rowIndex, 2)).GetText();
            var lastname = Wait.UntilElementVisible(AgencyAdminByNthRowNthColumn(rowIndex, 3)).GetText();
            var email = Wait.UntilElementVisible(AgencyAdminByNthRowNthColumn(rowIndex, 4)).GetText();

            return new AgencyAdmin()
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
            };
        }

        public void ClickOnDeleteAgencyAdminButton(int rowIndex)
        {
            ClickOnActionsButton(rowIndex);
            ClickOnDeleteButton();
            Wait.UntilElementClickable(DeleteAgencyAdminButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
        public IList<string> GetAgencyAdminByNthColumn(int columnIndex)
        {
            return Wait.UntilAllElementsLocated(AgencyAdminByNthColumn(columnIndex)).Where(e => e.Displayed).Select(x => x.GetText()).ToList();
        }
        public void ClickOnActionsButton(int rowIndex)
        {
            Wait.HardWait(5000);
            Wait.UntilElementVisible(ActionsButton(rowIndex));
            Driver.JavaScriptClickOn(Wait.UntilElementExists(ActionsButton(rowIndex)));
        }
        public void ClickOnDeleteButton()
        {
            Wait.UntilElementClickable(DeleteButton).ClickOn();
        }
        public void ClickOnInviteAgencyAdminCheckIcon()
        {
            Wait.UntilElementClickable(InviteAgencyAdminCheckIcon).ClickOn();
        }
        public string GetInviteAgencyAdminValidationText()
        {
            return Wait.UntilElementVisible(InviteAgencyAdminValidationText).GetText();
        }
        public void ClickOnInviteAgencyAdminValidationCloseIcon()
        {
            Driver.JavaScriptClickOn(InviteAgencyAdminValidationCloseIcon);
            Wait.UntilElementInVisible(InviteAgencyAdminValidationCloseIcon);
            ClickOnInviteAgencyAdminCloseIcon();
        }
        public void ClickOnInviteAgencyAdminCloseIcon()
        {
            Wait.UntilElementClickable(InviteAgencyAdminCloseIcon).ClickOn();
        }
        public bool IsInviteAgencyAdminCloseIconDisplayed()
        {
            return Wait.IsElementPresent(InviteAgencyAdminCloseIcon, 3);
        }
        public void ClickOnEditButton()
        {
            Driver.JavaScriptClickOn(Wait.UntilElementExists(EditButton));
        }
        public string GetInnerWrapperPopUpHeaderText()
        {
            return Wait.UntilElementVisible(InnerWrapperPopUpHeaderText).GetText();
        }
        public string GetInnerWrapperPopUpMessage()
        {
            return Wait.UntilElementVisible(InnerWrapperPopUpMessage).GetText();
        }
        public bool IsInnerWrapperPopUpHeaderTextDisplayed()
        {
            return Wait.IsElementPresent(InnerWrapperPopUpHeaderText, 10);
        }
        public void ClickOnInnerWrapperCancelButton()
        {
            Wait.UntilElementClickable(InnerWrapperCancelButton).ClickOn();
            Wait.UntilElementInVisible(InnerWrapperCancelButton, 5);
        }
        public void ClickOnDiscardChangesButton()
        {
            Wait.UntilElementClickable(DiscardChanges).ClickOn();
            Wait.UntilElementInVisible(DiscardChanges, 5);
        }
        public void SelectRowPerPageDropDown(string row)
        {
            Wait.UntilElementClickable(RowPerPageDropDown).SelectDropdownValueByText(row);
        }
        public List<string> GetColumnData(int columnIndex)
        {
            var gridSize = GetNumberOfRecordPresentOnGrid();
            var elementList = new List<IWebElement>();
            var columnList = new List<string>();
            do
            {
                Wait.HardWait(1000);
                var elements = Wait.UntilAllElementsLocated(NumberOfRowPerPage).Where(e => e.Displayed).ToList();
                for (var i = 1; i <= elements.Count; i++)
                {
                    columnList.Add(Wait.UntilElementVisible(AgencyAdminByNthRowNthColumn(i, columnIndex)).GetText());
                    elementList.Add(elements[i - 1]);
                }
                Driver.JavaScriptScrollToElement(elementList[^1]);
                columnList = columnList.Distinct().ToList();
            } while (columnList.Count < gridSize);
            return columnList;
        }
        public void ClickOnSortByButton(string text)
        {
            Wait.UntilElementClickable(SortByButton(text)).ClickOn();
        }
        public void ClickOnMaximizeMinimizeButton(string buttonName)
        {
            Wait.UntilElementClickable(MaximizeMinimizeButton(buttonName)).ClickOn();
        }
        public bool IsButtonNameDisplayed(string buttonName)
        {
            return Wait.IsElementPresent(MaximizeMinimizeButton(buttonName), 8);
        }
    }
}
