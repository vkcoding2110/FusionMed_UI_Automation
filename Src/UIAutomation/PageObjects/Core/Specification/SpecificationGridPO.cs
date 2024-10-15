using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Core.Specification
{
    internal class SpecificationGridPo : CoreBasePo
    {
        public SpecificationGridPo(IWebDriver driver) : base(driver)
        {
        }

        
        //Main Grid
        private readonly By GridIFrame = By.XPath("(//*[@class='iframe fullheight'])[2]");
        private readonly By AllRows = By.CssSelector("table#specificationdetails tbody tr");
        private static By GridInputTextBoxNthColumn(int indexColumn) => By.CssSelector($"table#specificationdetails thead tr:nth-of-type(2) th:nth-of-type({indexColumn}) input");
        private static By NthRowNthColumn(int indexRow, int indexColumn) => By.CssSelector($"table#specificationdetails tbody tr:nth-of-type({indexRow}) td:nth-of-type({indexColumn})");
        private static By NthRowStatus(int indexRow) => By.CssSelector($"table#specificationdetails tbody tr:nth-of-type({indexRow}) td:nth-of-type(14) #statusBadge");
  

        //Auditor Notes
        private readonly By AuditorNotesTextArea = By.Id("note-text");
        private readonly By AuditorNotesAddNoteButton = By.Id("upload-form-input");
        private By AuditorNotesNthRowNthColumn(int rowIndex, int colIndex) => By.CssSelector($"table#noteEdit tbody tr:nth-of-type({rowIndex}) td:nth-of-type({colIndex})");
        private readonly By AuditorNotesCloseButton = By.CssSelector("div#noteModal div.modal-footer button");
        private readonly By AuditorNotesHeaderText = By.CssSelector("div#noteModal h5.modal-title.pr-2");
        private readonly By AuditorNotesInputBox = By.CssSelector("div.k-multiselect-wrap input");
        private readonly By AuditorNotesDropDownFirstValue = By.CssSelector("#selectEmailAddress_listbox li");
        private readonly By AuditNotesSelectedFirstDropDownValue = By.CssSelector("#selectEmailAddress_taglist li");

        //Pending
        private static By NthRowApproveButton(int indexRow) => By.CssSelector($"table#specificationdetails tbody tr:nth-of-type({indexRow}) td:nth-of-type(14) .btn-success");
        private static By NthRowDeniedButton(int indexRow) => By.CssSelector($"table#specificationdetails tbody tr:nth-of-type({indexRow}) td:nth-of-type(14) .btn-danger");
        private static By NthRowPendingButton(int indexRow) =>  By.CssSelector($"table#specificationdetails tbody tr:nth-of-type({indexRow}) td:nth-of-type(14) .btn-warning");
        private static By StatusBadgeText(string status) => By.XPath($"//span[@id='statusBadge'][contains(text(),'{status}')]");
        
        //Left Nav
        private readonly By AuditPendingMenuCloseButton = By.CssSelector("#v-pills-tab-auditordashboard-pending-0 i.fa-window-close");
        private readonly By AuditDeniedMenuCloseButton = By.CssSelector("div#v-pills-tab i.fa.fa-window-close");
        private readonly By AuditNaMenuCloseButton = By.CssSelector("#v-pills-tab-auditordashboard-NA-124 i.fa-window-close");


        //Main Grid
        public void SwitchToIFrame()
        {
            WaitUntilCoreProcessingTextInvisible(); //Wait.UntilElementInVisible(ProcessingIndicator);
            Driver.SwitchToDefaultIframe();
            Driver.SwitchToIframe(Wait.UntilElementExists(GridIFrame));
        }   

        public int GetCountOfRows()
        {
            return Wait.UntilAllElementsLocated(AllRows).Where(e => e.Displayed).ToList().Count;
        }

        public void EnterDataToGridInputTextBoxNthColumn(int col, string data)
        {
            Wait.UntilElementClickable(GridInputTextBoxNthColumn(col)).EnterText(data);
            Wait.HardWait(5000);
        }

        public string GetNthRowNthColumnData(int row, int col)
        {
            return Wait.UntilElementExists(NthRowNthColumn(row, col)).GetText();
        }

        public string GetNthRowStatus(int indexRow)
        {
            return Wait.UntilElementVisible(NthRowStatus(indexRow)).GetText();
        }

       
        //Auditor Notes
        public void AuditorNotesAddNotesInTextArea(string note)
        {
            Wait.UntilElementClickable(AuditorNotesTextArea).Click();
            Wait.UntilElementVisible(AuditorNotesTextArea).EnterText(note);
        }

        public string AuditorNotesGetNotes()
        {
          return Wait.UntilElementVisible(AuditorNotesTextArea).GetAttribute("value");
        }

        public void AuditorNotesClickOnAddNoteButton()
        {
            Wait.UntilElementClickable(AuditorNotesAddNoteButton).ClickOn();
            Wait.HardWait(2000);
        }

        public string AuditorNotesGetNthRowNthColumnText(int row, int col)
        {
            return Wait.UntilElementVisible(AuditorNotesNthRowNthColumn(row, col)).GetText();
        }

        public void AuditorNotesClickOnCloseButton() 
        {
            Wait.UntilElementClickable(AuditorNotesCloseButton).ClickOn();
            Wait.HardWait(5000);
        }

        public string AuditorNotesGetHeaderText()
        {
            return Wait.UntilElementVisible(AuditorNotesHeaderText).GetText();
        }

        public string AuditorNotesSelectFirstDropDownValue()
        {
            Wait.UntilElementClickable(AuditorNotesInputBox).ClickOn();
            var value = Wait.UntilElementClickable(AuditorNotesDropDownFirstValue).GetText();
            Wait.UntilElementClickable(AuditorNotesDropDownFirstValue).ClickOn();
            return value;
        }

        public string GetAuditNotesDropDownFirstSelectedValue()
        {
            return Wait.UntilElementVisible(AuditNotesSelectedFirstDropDownValue).GetText();
        }


        //Pending

        public void ClickOnNthRowPendingButton(int indexRow)
        {
            Wait.UntilElementClickable(NthRowPendingButton(indexRow)).ClickOn();
            Wait.HardWait(5000);
        }

        public void ClickOnNthRowNthApproveButton(int indexRow) 
        {
            Wait.UntilElementClickable(NthRowApproveButton(indexRow)).ClickOn();
            Wait.UntilElementVisible(StatusBadgeText("Approved"));
        }

        public void ClickOnNthRowDeniedButton(int indexRow)
        {
            Wait.UntilElementClickable(NthRowDeniedButton(indexRow)).ClickOn();
            Wait.HardWait(5000);
        }


        //Left Nav
        public void AuditPendingMenuClickOnCloseButton()
        {
            Driver.SwitchToDefaultIframe();
            Wait.UntilElementClickable(AuditPendingMenuCloseButton).ClickOn();
        }      
        public void AuditDeniedMenuClickOnCloseButton()
        {
            Driver.SwitchToDefaultIframe();
            Wait.UntilElementClickable(AuditDeniedMenuCloseButton).ClickOn();
        }
        public void AuditNAMenuClickOnCloseButton()
        {
            Driver.SwitchToDefaultIframe();
            Wait.UntilElementClickable(AuditNaMenuCloseButton).ClickOn();
        }
    }
}
