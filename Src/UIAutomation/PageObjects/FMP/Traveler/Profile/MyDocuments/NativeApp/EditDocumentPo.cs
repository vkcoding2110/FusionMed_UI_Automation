using System;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.MyDocuments.NativeApp
{

    internal class EditDocumentPo : FmpBasePo
    {

        public EditDocumentPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By EditFileNameTextBox = By.XPath("//android.widget.TextView[contains(@text,'Name')]//parent::android.view.ViewGroup//parent::android.view.ViewGroup//android.widget.EditText");
        private readonly By EditDocumentPopupHeaderText = By.XPath("//*[@text='Edit Document']");
        private readonly By DocumentTypeDropDown = By.XPath("//*[@text='Type *']");
        private readonly By MonthDropDown = By.XPath("//android.widget.TextView[contains(@text,'Month')]");
        private readonly By YearDropDown = By.XPath("//android.widget.TextView[contains(@text,'Year')]");
        private readonly By SaveChangesButton = By.XPath("//*[@text='Save Changes']//parent::android.widget.Button");
        private static By FileType(string file) => By.XPath($"//*[@class='android.widget.TextView'][@text='{file}']");

        public bool IsEditDocumentPopUpDisplayed()
        {
            return Wait.IsElementPresent(EditDocumentPopupHeaderText, 5);
        }
        public void EditDocumentDetails(Document document)
        {
            new DocumentDetailPo(Driver).ClickOnEditDocumentButton();
            Wait.UntilElementVisible(EditFileNameTextBox).EnterText(document.FileName);
            Wait.UntilElementClickable(DocumentTypeDropDown).ClickOn();
            Wait.UntilElementClickable(FileType(document.DocumentType)).ClickOn();
            Wait.HardWait(1000);
            SelectExpirationMonth(document.DocumentUploadedDate);
            SelectExpirationYear(document.DocumentUploadedDate);
            Wait.UntilElementClickable(SaveChangesButton, 10).ClickOn();
            WaitUntilAppLoadingIndicatorInvisible();
        }

        public void SelectExpirationMonth(DateTime date)
        {
            var month = date.ToString("MMMM");
            Wait.UntilElementClickable(MonthDropDown).ClickOn();
            Wait.UntilElementClickable(FileType(month)).ClickOn();
        }

        public void SelectExpirationYear(DateTime date)
        {
            var year = date.ToString("yyyy");
            Wait.UntilElementClickable(YearDropDown).ClickOn();
            Wait.UntilElementClickable(FileType(year)).ClickOn();
        }
    }
}
