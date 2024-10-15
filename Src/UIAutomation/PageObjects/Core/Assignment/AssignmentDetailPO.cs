using System.Linq;
using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Core.Assignment
{
    internal class AssignmentDetailPo : CoreBasePo
    {

        public AssignmentDetailPo(IWebDriver driver) : base(driver)
        {
        }

        //Main Grid
        private readonly By AssignmentProcessingCard = By.XPath("//div[@id='assignmentspecifications_processing']");
        private readonly By HeaderText = By.CssSelector("div.header span");
        private static By NthRowNthColumn(int rowIndex, int colIndex) => By.CssSelector($"table#assignmentfiles tbody tr:nth-of-type({rowIndex}) td:nth-of-type({colIndex})");
        private static By MagnifyingGlassForNthResult(int rowIndex, int colIndex) => By.CssSelector($"table#assignmentspecifications tbody tr:nth-of-type({rowIndex}) td:nth-of-type({colIndex}) i.fa-search");
        private readonly By GridIFrame = By.CssSelector("*[id*='v-pills-pane-assignment-'] iframe");
        private static By FileIconForNthResult(int nth) => By.CssSelector($"table#assignmentspecifications tbody tr:nth-of-type({nth}) span.pl-2 span.pr-2 i");

        private static By BinocularsFileNameNthResult(int nth) => By.CssSelector($"table#assignmentspecifications tbody tr:nth-of-type({nth}) span[title*='Preview']");
        private static By BinocularsPopupHeaderText => By.CssSelector("div#preview-document div.header h5");
        private readonly By BinocularsCloseButton = By.CssSelector("div#preview-document h5 a i.fa");
        private readonly By BinocularsPopUp = By.CssSelector("div.preview-pdf-document");

        private static By CsNotesForNthResult(int nth) => By.CssSelector($"table#assignmentspecifications tbody tr:nth-of-type({nth}) i.fas.fa-edit");
        private readonly By CsNotesPopupHeaderText = By.CssSelector("div#note-modal-content.modal-dialog.modal-lg h5.modal-title.pr-2");

        private readonly By CsNotesInputBox = By.CssSelector("div.k-multiselect-wrap input");
        private static By CsNotesDropDownSelectValue(string name) => By.XPath($"//ul[@id='selectEmailAddress_listbox']//li[text()='{name}']");
        private readonly By CsNotesSelectedFirstDropDownValue = By.CssSelector("#selectEmailAddress_taglist li");

        private readonly By CsNotesTextArea = By.Id("note-text");
        private readonly By CsNotesAddNoteButton = By.Id("upload-form-input");
        private readonly By CsNotesEmailNotificationDropDown = By.Id("selectEmailAddress");
        private static By CsNotesNthRowNthColumn(int rowIndex, int colIndex) => By.CssSelector($"table#noteEdit tbody tr:nth-of-type({rowIndex}) td:nth-of-type({colIndex})");
        private readonly By CsNotesPopupCloseButton = By.CssSelector("div#noteModal div.modal-footer button");

        private readonly By CompletedDateInputbox = By.Id("datevalue");

        private readonly By SpecificationCheckBox = By.CssSelector("table#assignmentspecifications tbody input[id*='subcheckbox'][type='checkbox']");

        //Files Model
        private readonly By FilesModelLocator = By.CssSelector("div#file-modal-body.modal-body");
        private readonly By FilesModelChooseFromCandidateFilesButton = By.CssSelector("div#unassigned-file-modal-btn");
        private readonly By FilesModelChooseFileInput = By.CssSelector("div.form-group input.form-control");
        private readonly By FilesModelUploadFileButton = By.CssSelector("div.form-group button#upload-form-input");
        private readonly By FilesModelClosePopupButton = By.CssSelector("div#fileModal div.modal-footer button");
        private static By FilesModelGridNthRowNthColumnLocator(int rowIndex, int columnIndex) => By.CssSelector($"table#fileEdit tbody tr:nth-of-type({rowIndex}) td:nth-of-type({columnIndex})");
        private static By FilesModelGridRemoveFileButton(int nth) => By.CssSelector($"table#fileEdit tbody tr:nth-of-type({nth}) td:nth-of-type(7) span");
        private readonly By FilesModelGridAllRowsLocator = By.CssSelector("table#fileEdit tbody tr");
        private static By FilesModelGridActiveInActiveButton(int nth) => By.CssSelector($"table#fileEdit tbody tr:nth-of-type({nth}) td:nth-of-type(2) button");

        //Unassigned Files Model
        private readonly By UnAssignedFilesModelLocator = By.CssSelector("div#unassigned-candidate-file-modal-content");
        private readonly By UnAssignedFilesModelCloseButton = By.CssSelector("div#unassigned-candidate-file-modal-content div.modal-footer button");
        private static By UnAssignedFilesModelGridNthRowNthColumnLocator(int rowIndex, int columnIndex) => By.CssSelector($"table#unassigned_candidate_file tbody tr:nth-of-type({rowIndex}) td:nth-of-type({columnIndex})");
        private static By UnAssignedFilesModelAssignButton(int rowIndex) => By.CssSelector($"table#unassigned_candidate_file tbody tr:nth-of-type({rowIndex}) td button.add-candidate-file");
        private readonly By AssignSpecificProcessingIndicator = By.Id("assignmentspecifications_processing");

        //Main Grid

        public void WaitUntilAssignmentProcessingInvisible()
        {
            Wait.UntilElementInVisible(AssignmentProcessingCard, 60);
        }

        public string GetHeaderText()
        {
            Driver.SwitchToDefaultIframe();
            Driver.SwitchToIframe(Wait.UntilElementExists(GridIFrame));
            return Wait.UntilElementExists(HeaderText).GetText();
        }

        public string GetNthRowNthColumnData(int row, int col)
        {
            return Wait.UntilElementExists(NthRowNthColumn(row, col)).GetText();
        }

        public void ClickOnMagnifyingGlass(int row, int col)
        {
            Wait.UntilElementVisible(MagnifyingGlassForNthResult(row, col)).ClickOn();
            Wait.UntilElementVisible(FilesModelChooseFromCandidateFilesButton);
        }

        public void SwitchToIFrame()
        {
            Wait.UntilElementInVisible(AssignSpecificProcessingIndicator);
            Driver.SwitchToDefaultIframe();
            Driver.SwitchToIframe(Wait.UntilElementExists(GridIFrame));
        }

        public void ClickOnFileIcon(int nth)
        {
            Wait.UntilElementClickable(FileIconForNthResult(nth)).ClickOn();
            Wait.HardWait(3000);
        }


        public void ClickOnBinocularsIcon(int nth)
        {
            Wait.UntilElementClickable(BinocularsFileNameNthResult(nth)).ClickOn();
            Wait.HardWait(3000);
        }

        public string GetFileNameOfBinocular(int nth)
        {
            return Wait.UntilElementExists(BinocularsFileNameNthResult(nth)).GetAttribute("data-file-name");
        }

        public bool IsBinocularsPopupOpened()
        {
            return Wait.IsElementPresent(BinocularsPopUp);
        }

        public string GetBinocularsHeaderText()
        {
            return Wait.UntilElementVisible(BinocularsPopupHeaderText).GetText();
        }

        public void BinocularsFileClickOnCloseButton()
        {
            Wait.UntilElementClickable(BinocularsCloseButton).ClickOn();
            Wait.HardWait(3000);
        }

        public void ClickOnCsNoteButton(int nth)
        {
            Driver.SwitchToDefaultIframe();
            Driver.SwitchToIframe(Wait.UntilElementExists(GridIFrame));
            Wait.UntilElementClickable(CsNotesForNthResult(nth)).ClickOn();
        }


        public string GetCsNotePopupHeaderText()
        {
            return Wait.UntilElementExists(CsNotesPopupHeaderText).GetText();
        }

        public void CsNoteSelectDropDownValue(string name)
        {                        
            Wait.UntilElementClickable(CsNotesInputBox).ClickOn();
            Wait.UntilElementClickable(CsNotesDropDownSelectValue(name)).ClickOn();
        }

        public string GetCsNoteDropDownFirstSelectedValue()
        {
            return Wait.UntilElementVisible(CsNotesSelectedFirstDropDownValue).GetText();
        }

        public void CsNoteAddTextToNoteTextArea(string note)
        {
            Wait.UntilElementClickable(CsNotesTextArea).Click();
            Wait.UntilElementVisible(CsNotesTextArea).EnterText(note);
        }

        public void CsNoteClickOnAddNoteButton()
        {
            Wait.UntilElementVisible(CsNotesAddNoteButton);
            Wait.UntilElementClickable(CsNotesAddNoteButton).ClickOn();
            Wait.HardWait(10000); //It takes time to reflect 'Note'
        }

        public string GetCsNotesNthRowNthColumnText(int row, int col)
        {
            return Wait.UntilElementVisible(CsNotesNthRowNthColumn(row,col)).GetText();

        }

        public bool GetSpecificationCheclBoxStatus()
        {
            return Wait.UntilElementVisible(SpecificationCheckBox).IsElementSelected();
        }

        public void ClickOnSpecificationCheckBox(bool select)
        {
            Wait.UntilElementClickable(SpecificationCheckBox).Check(select);
            Wait.HardWait(5000);
        }

        public void UpdateCompletedDate(string date)
        {
            Driver.JavaScriptSetValue(Wait.UntilElementVisible(CompletedDateInputbox), date);

        }

        public string GetCompletedDateAttribute()
        {
            return Wait.UntilElementClickable(CompletedDateInputbox).GetAttribute("value");
        }

        //Files Model

        public bool IsFilesModelOpened()
        {
            return Wait.IsElementPresent(FilesModelLocator);
        }

        public void ClickOnChooseCandidateFileButton()
        {
            Wait.UntilElementVisible(FilesModelChooseFromCandidateFilesButton).ClickOn();
            Wait.HardWait(5000);
        }

        public string FilesGetDataFromNthRowNthColumn(int rowIndex, int columnIndex)
        {
            return Wait.UntilElementClickable(FilesModelGridNthRowNthColumnLocator(rowIndex, columnIndex)).GetText();
        }

        public void FilesClickOnNthRowNthColumn(int rowIndex, int columnIndex)
        {
            Wait.UntilElementClickable(FilesModelGridNthRowNthColumnLocator(rowIndex, columnIndex)).ClickOn();
        }

        public int FilesGetCountOfRows()
        {
            return Wait.WaitIncaseElementClickable(FilesModelGridAllRowsLocator) != null ? Wait.UntilAllElementsLocated(FilesModelGridAllRowsLocator).Where(e => e.Displayed).ToList().Count : 0;
        }

        public bool FilesIsFilePresent(string fileName)
        {

            var totalRows = FilesGetCountOfRows();
            for (var i = 1; i <= totalRows; i++)
            {
                if (FilesGetDataFromNthRowNthColumn(i, 4).Equals(fileName))
                {
                    return true;
                }
            }
            return false;
        }

        public void FilesClickOnCloseButton()
        {
            Wait.UntilElementClickable(FilesModelClosePopupButton).ClickOn();
            Wait.HardWait(3000);
        }

        public void ClickOnRemoveFileButtonAndAcceptAlert(int nth)
        {
            Wait.UntilElementClickable(FilesModelGridRemoveFileButton(nth)).ClickOn();
            Driver.AcceptAlert();
            Wait.HardWait(5000);
        }

        public void FileRemoveAllExistingFiles()
        {
            var totalRows = FilesGetCountOfRows();
            for (var i = 1; i <= totalRows; i++)
            {
                ClickOnRemoveFileButtonAndAcceptAlert(1);
            }
        }

        public void FilesChooseFileAndUploadFile(string path)
        {
            Wait.UntilElementClickable(FilesModelChooseFileInput).SendKeys(path);
            Wait.UntilElementClickable(FilesModelUploadFileButton).ClickOn();
            Wait.HardWait(10000);
        }

        public string GetTextFromActiveInActiveButton(int nth)
        {
            return Wait.UntilElementClickable(FilesModelGridActiveInActiveButton(nth)).GetText();
        }

        public void ClickOnActiveInActiveButton(int nth)
        {
            Wait.UntilElementClickable(FilesModelGridActiveInActiveButton(nth)).ClickOn();
            Wait.HardWait(3000);
        }


        //Unassigned Files Model
        public bool IsUnAssignedFilesModelOpened()
        {
            return Wait.IsElementPresent(UnAssignedFilesModelLocator);
        }

        public string UnAssignedFilesGetDataFromNthRowNthColumn(int rowIndex, int columnIndex)
        {
            return Wait.UntilElementClickable(UnAssignedFilesModelGridNthRowNthColumnLocator(rowIndex, columnIndex)).GetText();
        }

        public void UnAssignedFilesClickOnNthRowNthColumn(int rowIndex, int columnIndex)
        {
            Wait.UntilElementClickable(UnAssignedFilesModelGridNthRowNthColumnLocator(rowIndex, columnIndex)).ClickOn();
            Wait.HardWait(3000);
        }

        public void UnAssignedFilesClickOnCloseButton()
        {
            Wait.UntilElementClickable(UnAssignedFilesModelCloseButton).ClickOn();
            Wait.HardWait(5000);
        }

        public void UnAssignedFilesClickOnAssignButton(int rowIndex)
        {
            Wait.UntilElementClickable(UnAssignedFilesModelAssignButton(rowIndex)).ClickOn();
            Wait.HardWait(3000);
        }
    }
}
