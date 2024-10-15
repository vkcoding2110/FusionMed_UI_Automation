using System;
using System.Collections.Generic;
using System.Globalization;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.MyDocuments
{
    internal class MyDocumentsDetailsPo : FmpBasePo
    {
        public MyDocumentsDetailsPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By MyDocumentsTab = By.XPath("//button[contains(@class,'SectionTab')]/span[text()='My Documents']");
        private readonly By AddDocumentsButton = By.XPath("//button[contains(@class,'AddItemButton')]/span[text()='Add Documents']");
            
        //Document Details
        private static By DocumentNameByRow(int row) => By.XPath($"//div[contains(@class,'ProfileDocumentWrapper')][{row}]//div[contains(@class,'DocumentName')]");
        private static By DocumentType(int row) => By.XPath($"//div[contains(@class,'ProfileDocumentWrapper')][{row}]//div[contains(@class,'ProfileDocumentHeader')]");
        private static By DocumentUploadedDate(int row) => By.XPath($"//div[contains(@class,'ProfileDocumentWrapper')][{row}]//div[contains(@class,'DateCreated')]");
        private static By DocumentExpirationDate(int row) => By.XPath($"//div[contains(@class,'ProfileDocumentWrapper')][{row}]//div[contains(@class,'DateExpired')]");
        private static By DocumentTypeCode(int row) => By.XPath($"//div[contains(@class,'ProfileDocumentWrapper')][{row}]//div[contains(@class,'DocumentType')]");

        //Document Menus
        private readonly By DocumentMenuButton = By.XPath("//div[contains(@class,'DocumentMenu')]//button");
        private readonly By EditDocumentButton = By.XPath("//div[contains(@class,'StyledPopper')]//li[text()='Edit Document']");
        private readonly By DeleteDocumentButton = By.XPath("//div[contains(@class,'StyledPopper')]//li[text()='Delete']");
        private readonly By DeleteFileButton = By.XPath("//button[contains(@class,'DocumentDeleteButton')]//span[text()='Delete File']");
        private readonly By DownloadButton = By.XPath("//div[contains(@class,'StyledPopper')]//li[text()='Download']");

        //Device elements
        private readonly By AddDocumentsButtonDevice = By.XPath("//button[contains(@class,'DocumentEditButton')]/span[text()='Add Documents']");
        private static By DocumentNameByRowDevice(int row) => By.XPath($"//div[contains(@class,'InfoAccordionChildren')][{row}]//div[contains(@class,'DocumentName')]");

        public void ClickOnMyDocumentsTabButton()
        {
            if (BaseTest.PlatformName == PlatformName.Web)
            {
                Driver.JavaScriptClickOn(MyDocumentsTab);
            }
            ClickOnAddDocumentsButton();
        }

        public void ClickOnAddDocumentsButton()
        {
            if (Wait.IsElementPresent(DocumentMenuButton))
            {
                DeleteAllDocumentsDetails();
            }
            Wait.UntilElementClickable(BaseTest.PlatformName == PlatformName.Web ? AddDocumentsButton : AddDocumentsButtonDevice).ClickOn();
        }

        public bool IsAddDocumentsButtonDisplayed()
        {
            return Wait.IsElementPresent(BaseTest.PlatformName == PlatformName.Web ? AddDocumentsButton : AddDocumentsButtonDevice);
        }

        public List<Document> GetDocumentDetailsFromDetailPage(int row)
        {
            if (BaseTest.PlatformName == PlatformName.Web)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementVisible(AddDocumentsButton));
            }

            var fileName = Wait.UntilElementVisible((BaseTest.PlatformName == PlatformName.Web ? DocumentNameByRow(row) : DocumentNameByRowDevice(row))).GetText();
            var documentType = Wait.UntilElementVisible(DocumentType(row), 5).GetText();
            var documentUploadedDate = Wait.UntilElementVisible(DocumentUploadedDate(row), 5).GetText();
            var documentTypeCode = Wait.UntilElementVisible(DocumentTypeCode(row), 5).GetText();
            DateTime expirationDate;
            if (Wait.IsElementPresent(DocumentExpirationDate(row)))
            {
                 var date= Wait.UntilElementVisible(DocumentExpirationDate(row)).GetText().Replace("EXP\r\n", "").Replace("EXP", "");
                 expirationDate = DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                 expirationDate = DateTime.Now;
            }
            return new List<Document>
            {
                new Document
                {
                    FileName = fileName,
                    DocumentType = documentType,
                    DocumentUploadedDate = DateTime.ParseExact(documentUploadedDate, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    DocumentTypeCode = documentTypeCode,
                    ExpirationDate = expirationDate,
                }
            };
        }

        public void ClickOnDocumentMenuButton()
        {
            Driver.JavaScriptClickOn(Wait.UntilElementVisible(DocumentMenuButton));
        }

        public void ClickOnEditDocumentButton()
        {
            Wait.UntilElementClickable(EditDocumentButton, 5).ClickOn();
        }

        public void ClickOnDeleteDocumentButton()
        {
            Wait.UntilElementClickable(DeleteDocumentButton, 10).ClickOn();
            Wait.UntilElementClickable(DeleteFileButton, 10).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void DeleteAllDocumentsDetails()
        {
            if (BaseTest.PlatformName == PlatformName.Web)
            {
               Wait.UntilElementClickable(MyDocumentsTab, 5).ClickOn();
            }
            var allElement = Wait.UntilAllElementsLocated(DocumentMenuButton, 10).Count;
            for (var i = 0; i < allElement; i++)
            {
                ClickOnDocumentMenuButton();
                ClickOnDeleteDocumentButton();
            }
        }

        public bool IsDocumentHeaderTextDisplayed(int row)
        {
            return Wait.IsElementPresent(DocumentType(row), 5);
        }

        public void ClickOnDownloadButton()
        {
            Wait.UntilElementClickable(DownloadButton).ClickOn();
            if (BaseTest.PlatformName == PlatformName.Android)
            {
                new MobileFileSelectionPo(Driver).ClickOnDownloadButton();
            }
        }
    }
}
