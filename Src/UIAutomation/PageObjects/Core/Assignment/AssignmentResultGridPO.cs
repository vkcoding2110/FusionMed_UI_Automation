using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Core.Assignment
{
    internal class AssignmentResultGridPo : CoreBasePo
    {
        public AssignmentResultGridPo(IWebDriver driver) : base(driver)
        {
        }
        
        private readonly By ShowDropdown= By.Name("assignments_length");
        private readonly By AllRows = By.CssSelector("table#assignments tbody tr");
        private static By NthRowNthColumn(int rowIndex, int columnIndex) => By.CssSelector($"div[class*='dataTables_wrapper'] table#assignments tbody tr:nth-of-type({rowIndex}) td:nth-of-type({columnIndex})");
        private static By NthRowNthColumnLink(int rowIndex, int columnIndex) => By.CssSelector($"div[class*='dataTables_wrapper'] table#assignments tbody tr:nth-of-type({rowIndex}) td:nth-of-type({columnIndex}) a");

        private readonly By AssignSearchBox = By.CssSelector("table#assignments thead tr:nth-of-type(2) th:nth-of-type(1) input");
        private readonly By NameSearchBox = By.CssSelector("table#assignments thead tr:nth-of-type(2) th:nth-of-type(4) input");
        private readonly By SpecialitySearchBox = By.CssSelector("table#assignments thead tr:nth-of-type(2) th:nth-of-type(7) input");
        private readonly By FacilitySearchBox = By.CssSelector("table#assignments thead tr:nth-of-type(2) th:nth-of-type(9) input");
        private readonly By RecruiterSearchBox = By.CssSelector("table#assignments thead tr:nth-of-type(2) th:nth-of-type(10) input");
        private readonly By ClientManagerSearchBox = By.CssSelector("table#assignments thead tr:nth-of-type(2) th:nth-of-type(11) input");
        private readonly By CsSearchBox = By.CssSelector("table#assignments thead tr:nth-of-type(2) th:nth-of-type(12) input");
        private readonly By BackgroundSearchBox = By.CssSelector("table#assignments thead tr:nth-of-type(2) th:nth-of-type(13) input");
        private readonly By GridFrame = By.XPath("//iframe[@class='iframe fullheight']");
        public void SwitchToIFrame()
        {
            Driver.SwitchToDefaultIframe();
            Driver.SwitchToIframe(Wait.UntilElementExists(GridFrame));
        }
        
        public void SelectShowDropdownValue(string value)
        {
            Wait.UntilElementClickable(ShowDropdown).SelectDropdownValueByText(value);
            Wait.HardWait(5000);
        }

        public int GetCountOfRows()
        {
            Wait.HardWait(5000);
            return Wait.UntilAllElementsLocated(AllRows).Where(e => e.Displayed).ToList().Count;
        }

        public string GetDataFromNthRowNthColumn(int rowIndex, int columnIndex)
        {
            return Wait.UntilElementClickable(NthRowNthColumn(rowIndex, columnIndex)).GetText();
        }

        public void EnterDataToAssignSearchTextBoxAndHitEnter(string assign)
        {
            Wait.UntilElementClickable(AssignSearchBox).EnterText(assign);
            Wait.UntilElementClickable(AssignSearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }

        public void EnterDataToNameSearchTextBoxAndHitEnter(string name)
        {
            Wait.UntilElementClickable(NameSearchBox).EnterText(name);
            Wait.UntilElementClickable(NameSearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }

        public void EnterDataToSpecialitySearchTextBoxAndHitEnter(string speciality)
        {
            Wait.UntilElementClickable(SpecialitySearchBox).EnterText(speciality);
            Wait.UntilElementClickable(SpecialitySearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }


        public void EnterDataToFacilitySearchTextBoxAndHitEnter(string facility)
        {
            Wait.UntilElementClickable(FacilitySearchBox).EnterText(facility);
            Wait.UntilElementClickable(FacilitySearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }

        public void EnterDataToRecruiterSearchTextBoxAndHitEnter(string recruiter)
        {
            Wait.UntilElementClickable(RecruiterSearchBox).EnterText(recruiter);
            Wait.UntilElementClickable(RecruiterSearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }

        public void EnterDataToClientManagerSearchTextBoxAndHitEnter(string clientManager)
        {
            Wait.UntilElementClickable(ClientManagerSearchBox).EnterText(clientManager);
            Wait.UntilElementClickable(ClientManagerSearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }

        public void EnterDataToCsSearchTextBoxAndHitEnter(string cs)
        {
            Wait.UntilElementClickable(CsSearchBox).EnterText(cs);
            Wait.UntilElementClickable(CsSearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }

        public void EnterDataToBackgroundSearchTextBoxAndHitEnter(string background)
        {
            Wait.UntilElementClickable(BackgroundSearchBox).EnterText(background);
            Wait.UntilElementClickable(BackgroundSearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }

        public void ClickOnNthRowNthColumn(int rowIndex, int columnIndex)
        {
            Wait.UntilElementClickable(NthRowNthColumnLink(rowIndex, columnIndex)).ClickOn();
        }

       
    }
}
