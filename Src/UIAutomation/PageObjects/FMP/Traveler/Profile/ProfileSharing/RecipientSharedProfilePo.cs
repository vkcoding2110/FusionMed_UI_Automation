using System;
using System.Collections.Generic;
using System.Globalization;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.ProfileSharing
{
    internal class RecipientSharedProfilePo : FmpBasePo
    {
        public RecipientSharedProfilePo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By FirstAndLastNameText = By.XPath("//h2[contains(@class,'ProfileName')]");
        private readonly By DepartmentSpecialtyText = By.XPath("//p[contains(@class,'ProfileDepartmentSpecialty')]");
        private readonly By EmailText = By.XPath("//span[contains(@class,'ProfileEmail')]/a");
        private readonly By DownloadResumeButton = By.XPath("//div[contains(@class,'ProfileHeaderWrapper')]/button[contains(@class,'DownloadResumeButton')]");
        private readonly By DownloadDocumentButton = By.XPath("//div[contains(@class,'DocumentsSection')]//button[contains(@class,'DownloadButton')]");

        //Document Details
        private static By DocumentName(int row) => By.XPath($"//div[contains(@class,'DocumentsWrapper')][{row}]//div[contains(@class,'DocumentName')]");
        private static By DocumentType(int row) => By.XPath($"//div[contains(@class,'DocumentsWrapper')][{row}]//h3[contains(@class,'DocumentsSectionHeader')]");
        private static By DocumentUploadedDate(int row) => By.XPath($"//div[contains(@class,'DocumentsWrapper')][{row}]//div[contains(@class,'DateCreated')]");
        private static By DocumentExpirationDate(int row) => By.XPath($"//div[contains(@class,'ProfileDocumentWrapper')][{row}]//div[contains(@class,'DateExpired')]");
        private static By DocumentTypeCode(int row) => By.XPath($"//div[contains(@class,'DocumentsWrapper')][{row}]//span[contains(@class,'DocumentType')]");

        public string GetFirstAndLastName()
        {
            return Wait.UntilElementVisible(FirstAndLastNameText).GetText();
        }
        public string GetDepartmentAndSpecialtyText()
        {
            return Wait.UntilElementVisible(DepartmentSpecialtyText).GetText();
        }
        public string GetEmailText()
        {
            return Wait.UntilElementVisible(EmailText).GetText();
        }
        public void ClickOnDownloadResumeButton()
        {
            Wait.UntilElementClickable(DownloadResumeButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
            if (BaseTest.PlatformName == PlatformName.Android)
            {
                new MobileFileSelectionPo(Driver).ClickOnDownloadButton();
            }
        }

        public List<Document> GetDocumentDetailsFromDetailPage(int row)
        {
            var fileName = Wait.UntilElementVisible( DocumentName(row)).GetText();
            var documentType = Wait.UntilElementVisible(DocumentType(row), 5).GetText();
            var documentUploadedDate = Wait.UntilElementVisible(DocumentUploadedDate(row), 5).GetText();
            var documentTypeCode = Wait.UntilElementVisible(DocumentTypeCode(row), 5).GetText();
            DateTime expirationDate;
            if (Wait.IsElementPresent(DocumentExpirationDate(row)))
            {
                var date = Wait.UntilElementVisible(DocumentExpirationDate(row)).GetText().Replace("EXP\r\n", "").Replace("EXP", "");
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

        public void ClickOnDownloadDocumentButton()
        {
            Wait.UntilElementClickable(DownloadDocumentButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }
    }
}
