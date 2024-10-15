using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Core.Candidate
{
    internal class CandidateResultsGridPo : CoreBasePo
    {
        public CandidateResultsGridPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By ShowDropdown = By.Name("candidates_length");
        private readonly By AllRows = By.CssSelector("table#candidates tbody tr");
        private static By NthRowNthColumn(int rowIndex, int columnIndex) => By.CssSelector($"table#candidates tbody tr:nth-of-type({rowIndex}) td:nth-of-type({columnIndex})");
        private static By NthRowNthColumnClick(int rowIndex, int columnIndex) => By.CssSelector($"table#candidates tbody tr:nth-of-type({rowIndex}) td:nth-of-type({columnIndex}) a");
        private readonly By IdSearchBox = By.CssSelector("table#candidates thead tr:nth-of-type(2) th:nth-of-type(1) input");
        private readonly By NameSearchBox = By.CssSelector("table#candidates thead tr:nth-of-type(2) th:nth-of-type(2) input");
        private readonly By CitySearchBox = By.CssSelector("table#candidates thead tr:nth-of-type(2) th:nth-of-type(5) input");
        private readonly By StateSearchBox = By.CssSelector("table#candidates thead tr:nth-of-type(2) th:nth-of-type(6) input");
        private readonly By RecruiterSearchBox = By.CssSelector("table#candidates thead tr:nth-of-type(2) th:nth-of-type(8) input");


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

        public void EnterDataToIdSearchTextBoxAndHitEnter(string id)
        {
            Wait.UntilElementClickable(IdSearchBox).EnterText(id);
            Wait.UntilElementClickable(IdSearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }

        public void EnterDataToNameSearchTextBoxAndHitEnter(string name)
        {
            Wait.UntilElementClickable(NameSearchBox).EnterText(name);
            Wait.UntilElementClickable(NameSearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }

        public void EnterDataToCitySearchTextBoxAndHitEnter(string city)
        {
            Wait.UntilElementClickable(CitySearchBox).EnterText(city);
            Wait.UntilElementClickable(CitySearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }

        public void EnterDataToStateSearchTextBoxAndHitEnter(string state)
        {
            Wait.UntilElementClickable(StateSearchBox).EnterText(state);
            Wait.UntilElementClickable(StateSearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }

        public void EnterDataToRecruiterSearchTextBoxAndHitEnter(string recruiter)
        {
            Wait.UntilElementClickable(RecruiterSearchBox).EnterText(recruiter);
            Wait.UntilElementClickable(RecruiterSearchBox).SendKeys(Keys.Enter);
            WaitUntilCoreProcessingTextInvisible();
        }

        public void ClickOnNthRowNthColumn(int rowIndex, int columnIndex)
        {
            Wait.UntilElementClickable(NthRowNthColumnClick(rowIndex, columnIndex)).ClickOn();
        }

    }
}
