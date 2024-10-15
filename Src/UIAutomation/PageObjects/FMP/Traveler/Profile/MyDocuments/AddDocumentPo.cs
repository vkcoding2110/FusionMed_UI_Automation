using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.MyDocuments
{
    internal class AddDocumentPo : FmpBasePo
    {
        public AddDocumentPo(IWebDriver driver) : base(driver)
        {
        }

        //Add Document 
        private readonly By AddDocumentsTitleText = By.XPath("//div[contains(@class,'DialogTitle')]");
        private readonly By UploadDocumentFileInput = By.XPath("//button[contains(@class,'AttachmentDiv')]/input");
        private static By DocumentTypeDropDown(int index) => By.XPath($"//div[contains(@class,'DroppedFile-sc')][{index}]//select[contains(@class,'MuiInputBase-input')]");
        private readonly By UploadDocumentsButton = By.XPath("//button[contains(@class,'UploadDocumentsButton')]");
        private readonly By AddMoreButton = By.XPath("//button[contains(@class,'AddMoreDocuments')]//span[text()='Add More']");

        //Close Icon, Cancel Button & Back Button
        private readonly By SelectTypeOfPopUpCloseIcon = By.XPath("//div[contains(@class,'CloseIconWrapper')]//*[local-name()='svg']");
        private readonly By SelectTypeOfPopUpCancelButton = By.XPath("//button[contains(@class,'CancelUpload')]");
        private readonly By SelectTypeOfPopUpBackIcon = By.XPath("//div[contains(@class,'BackArrowWrapper')]");
        private readonly By AddDocumentPopUpCloseIcon = By.XPath("//div[text()='Add Documents']//preceding-sibling::div[contains(@class,'CloseIconWrapper-sc')]//*[local-name()='svg']");
        private readonly By UploadDocumentDeviceButton = By.XPath("//button[contains(@class,'AttachmentDiv')]/p/span[text()='upload documents from your device']");
        private static By DroppedFileList(int row) => By.XPath($"//div[contains(@class,'DroppedFile-sc')][{row}]//div[contains(@class,'DroppedFileName-sc')]");


        public string GetAddDocumentsPopUpTitleText()
        {
            return Wait.UntilElementVisible(AddDocumentsTitleText).GetText();
        }

        public void UploadDocumentFromDevice(Document doc, int rowIndex)
        {
            switch (BaseTest.PlatformName)
            {
                case PlatformName.Web:
                    AddDocumentFromDevice(doc);
                    Wait.HardWait(2000);
                    Wait.UntilElementVisible(DocumentTypeDropDown(rowIndex), 10).SelectDropdownValueByText(doc.DocumentType);
                    break;
                case PlatformName.Android:
                    Wait.UntilElementClickable(UploadDocumentDeviceButton).ClickOn();
                    new MobileFileSelectionPo(Driver).SelectFileFromDevice(doc.FilePath + doc.FileName);
                    Wait.HardWait(2000);
                    Wait.UntilElementVisible(DocumentTypeDropDown(rowIndex), 10).SelectDropdownValueByText(doc.DocumentType);
                    break;
                case PlatformName.Ios:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AddDocumentFromDevice(Document doc)
        {
            var path = doc.FilePath + doc.FileName;
            Wait.UntilElementExists(UploadDocumentFileInput).SendKeys(path);
        }

        public void ClickOnUploadDocumentsButton()
        {
            Wait.UntilElementClickable(UploadDocumentsButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public List<Document> GetDroppedFilesFromSelectDocumentsPopUp(int row)
        {
            var fileName = Wait.UntilElementVisible(DroppedFileList(row)).GetText();
            var documentType = Wait.UntilElementVisible(DocumentTypeDropDown(row)).SelectDropdownGetSelectedValue();
            return new List<Document>
            {
                new Document
                {
                    FileName = fileName,
                    DocumentType = documentType,
                }
            };
        }

        public void ClickOnSelectTypeOfPopUpCloseIcon()
        {
            Wait.HardWait(1000);
            Wait.UntilElementClickable(SelectTypeOfPopUpCloseIcon,5).ClickOn();
        }

        public void ClickOnSelectTypeOfPopUpCancelButton()
        {
            Wait.UntilElementClickable(SelectTypeOfPopUpCancelButton, 10).ClickOn();
        }

        public void ClickOnSelectTypeOfPopUpBackIcon()
        {
            Wait.UntilElementClickable(SelectTypeOfPopUpBackIcon, 10).ClickOn();
            Wait.WaitTillElementCountIsLessThan(SelectTypeOfPopUpBackIcon, 1);
        }

        public bool IsSelectTypeOfDocumentPopUpOpened()
        {
            return Wait.IsElementPresent(AddDocumentsTitleText,5);
        }

        public void ClickOnAddMoreButton()
        {
            Wait.UntilElementClickable(AddMoreButton, 5).ClickOn();
        }

        public void ClickOnAddDocumentPopUpCloseIcon()
        {
            Wait.UntilElementClickable(AddDocumentPopUpCloseIcon,5).ClickOn();
        }

        public bool IsAddDocumentsPopUpOpened()
        {
            return Wait.IsElementPresent(AddDocumentPopUpCloseIcon, 5);
        }
    }
}
