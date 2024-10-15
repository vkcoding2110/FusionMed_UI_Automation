using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.MyDocuments.NativeApp
{
    internal class DocumentDetailPo : FmpBasePo
    {
        public DocumentDetailPo(IWebDriver driver) : base(driver)
        {
        }

        private const string AddDocumentLabel = "Add Document";

        private readonly By FileName = By.XPath("//*[@text='MY DOCUMENTS']//parent::android.view.ViewGroup/following-sibling::android.view.ViewGroup/android.view.ViewGroup[1]//android.widget.TextView[3]");
        private readonly By DocumentType = By.XPath("//*[@text='MY DOCUMENTS']//parent::android.view.ViewGroup/following-sibling::android.view.ViewGroup/android.widget.TextView");
        private readonly By AddDocumentButton = By.XPath("//android.widget.Button//android.widget.TextView[@text='Add Document']");
        private readonly By DocumentMenuButton = By.XPath("//*[@text='MY DOCUMENTS']//parent::android.view.ViewGroup//parent::android.view.ViewGroup[1]//android.widget.Button");
        private readonly By DocumentMenuButtonCount = By.XPath("//*[@text='MY DOCUMENTS']//parent::android.view.ViewGroup/following-sibling::android.view.ViewGroup/android.view.ViewGroup");
        private readonly By DeleteDocumentButton = By.XPath("//*[@text='Delete']");
        private readonly By DocumentDetailMissingText = By.XPath("//*[@text='MY DOCUMENTS']//parent::android.view.ViewGroup/following-sibling::android.view.ViewGroup/android.view.ViewGroup//android.widget.TextView[contains(@text,'missing some information.')]");
        private readonly By ShareDocumentButton = By.XPath("//*[@text='Share']");
        private readonly By EditDocumentButton = By.XPath("//*[@text='Edit Document']");

        public void ClickOnAddDocumentButton()
        {
            Driver.ScrollToElementByText(AddDocumentLabel);
            Wait.UntilElementClickable(AddDocumentButton).ClickOn();
        }
        public void ClickOnDocumentMenuButton()
        {
            Driver.ScrollToElementByText(AddDocumentLabel);
            Wait.UntilElementClickable(DocumentMenuButton).ClickOn();
        }

        public void ClickOnShareDocumentButton()
        {
            Wait.UntilElementClickable(ShareDocumentButton).ClickOn();
        }

        public void ClickOnEditDocumentButton()
        {
            Wait.UntilElementClickable(EditDocumentButton).ClickOn();
        }

        public void ClickOnDeleteDocumentButton()
        {
            Wait.UntilElementClickable(DeleteDocumentButton).ClickOn();
        }
        public string GetDocumentFileName()
        {
            return Wait.UntilElementVisible(FileName).GetText().Replace(".pdf","");
        }
        public string GetDocumentType()
        {
            return Wait.UntilElementVisible(DocumentType).GetText();
        }
        public void DeleteAllDocumentsDetails()
        {
            Driver.ScrollToElementByText(AddDocumentLabel);
            if (Wait.IsElementPresent(DocumentDetailMissingText, 5)) return;
            var allElement = Wait.UntilAllElementsLocated(DocumentMenuButtonCount, 10).Count;
            for (var i = 0; i < allElement - 1; i++)
            {
                ClickOnDocumentMenuButton();
                ClickOnDeleteDocumentButton();
                new AddDocumentPo(Driver).ClickOnDeleteFileButton();
            }
        }
    }
}
