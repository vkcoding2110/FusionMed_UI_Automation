using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.MyDocuments.NativeApp
{
    internal class AddDocumentPo : FmpBasePo
    {
        public AddDocumentPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By UploadDocumentButton = By.XPath("//*[@text='Upload Document']");
        private readonly By DocumentTypeDropDown = By.XPath("//*[@text='Type']");
        private readonly By DeleteFileButton = By.XPath("//*[@text='Delete File']");
        private readonly By CloseIcon = By.XPath("//*[contains(@text,'Select Type of Document')]//parent::android.view.ViewGroup/android.widget.Button");
        private readonly By CancelButton = By.XPath("//*[@text='cancel']");
        private static By FileType(string file) => By.XPath($"//*[@class='android.widget.TextView'][@text='{file}']");


        public void UploadDocumentFromDevice(Document doc)
        {
            new DocumentDetailPo(Driver).ClickOnAddDocumentButton();
            new MobileFileSelectionPo(Driver).SelectFileFromDevice(doc.FilePath + doc.FileName);
            Wait.HardWait(2000);
        }

        public void ClickOnUploadDocumentButton()
        {
            Wait.UntilElementClickable(UploadDocumentButton).ClickOn();
        }

        public void SelectDocumentType(string doc)
        {
            Wait.UntilElementClickable(DocumentTypeDropDown).ClickOn();
            Wait.UntilElementClickable(FileType(doc)).ClickOn();
            Wait.HardWait(1000);
        }
        public void ClickOnDeleteFileButton()
        {
            Wait.UntilElementClickable(DeleteFileButton).ClickOn();
        }
        public void ClickOnCancelButton()
        {
            Wait.UntilElementClickable(CancelButton).ClickOn();
            Wait.UntilElementInVisible(CancelButton, 5);
        }

        public void ClickOnCloseIcon()
        {
            Wait.UntilElementClickable(CloseIcon).ClickOn();
            Wait.UntilElementInVisible(CloseIcon, 5);
        }

        public bool IsAddDocumentPopUpDisplayed()
        {
            return Wait.IsElementPresent(UploadDocumentButton, 5);
        }
    }
}
