using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.Components;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.MyDocuments
{
    internal class EditDocumentPo : FmpBasePo
    {
        public EditDocumentPo(IWebDriver driver) : base(driver)
        {
        }

        //Edit Document
        private readonly By EditDocumentHeaderText = By.XPath("//h5[contains(@class,'EditHeaderText')]");
        private readonly By FileName = By.CssSelector("input#file-name");
        private readonly By DocumentType = By.XPath("//div[contains(@class,'formControl')]//select[@id='document-type']");
        private readonly By ExpirationDateInput = By.CssSelector("input#document-expiration-date");
        private readonly By SaveChangesButton = By.XPath("//button[contains(@class,'DocumentEditButton')]/span[text()='Save Changes']");

        public void EditDocumentDetails(Document document)
        {
            var datePicker = new DatePickerPo(Driver);
            Wait.UntilElementVisible(FileName).EnterText(document.FileName, true);
            Wait.UntilElementVisible(DocumentType).SelectDropdownValueByText(document.DocumentType);
            datePicker.SelectMonthAndYear(document.ExpirationDate, ExpirationDateInput);
            Wait.UntilElementClickable(SaveChangesButton, 10).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public string GetEditDocumentHeaderText()
        {
            return Wait.UntilElementVisible(EditDocumentHeaderText).GetText();
        }
    }
}
