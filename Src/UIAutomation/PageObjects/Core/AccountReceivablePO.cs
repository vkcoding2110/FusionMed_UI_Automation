using OpenQA.Selenium;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.Core
{
    internal class AccountReceivablePo : CoreBasePo
    {
        public AccountReceivablePo(IWebDriver driver) : base(driver)
        {
        } 

        private readonly By Iframe = By.CssSelector(".iframe.fullheight");
        private readonly By AccountReceivableHeader = By.CssSelector("div.header span.lead");
        private readonly By VendorFeeInput = By.Id("txtVendorFee");
        private readonly By EscrowProcessingFeeInput = By.Id("txtProcessingFee");
        private readonly By ChooseGpSheetInput = By.Id("FileUploadGP");
        private readonly By ChooseShiftWiseSheetInput = By.Id("FileUploadShiftwise");
        private readonly By UploadFileButton = By.Id("upload-form-input");



        public void SwitchToIFrame()
        {
            Driver.SwitchToDefaultIframe();
            Driver.SwitchToIframe(Wait.UntilElementExists(Iframe));
        }

        public string GetAccountReceivableHeaderText()
        {
            return Wait.UntilElementVisible(AccountReceivableHeader).GetText();
        }

        public void EnterVendorFee(string rateInPercentage)
        {
            Wait.UntilElementVisible(VendorFeeInput).EnterText(rateInPercentage);
        }

        public string GetVendorFeeAttribute()
        {
            return Wait.UntilElementVisible(VendorFeeInput).GetAttribute("value");
        }

        public void EnterEscrowProcessingFee(string rateInDollar)
        {
            Wait.UntilElementVisible(EscrowProcessingFeeInput).EnterText(rateInDollar);
        }

        public string GetEscrowProcessingAttribute()
        {
            return Wait.UntilElementVisible(EscrowProcessingFeeInput).GetAttribute("value");
        }

        public void ChooseGpFile(string path)
        {
            Wait.UntilElementClickable(ChooseGpSheetInput).SendKeys(path);
            Wait.HardWait(3000);
        }

        public string GetSelectedGpFileAttribute()
        {
            return Wait.UntilElementClickable(ChooseGpSheetInput).GetAttribute("value");
        }

        public void ChooseShiftWiseFile(string path)
        {
            Wait.UntilElementClickable(ChooseShiftWiseSheetInput).SendKeys(path);
            Wait.HardWait(3000);
        }

        public string GetSelectedShiftWiseFileAttribute()
        {
            return Wait.UntilElementClickable(ChooseShiftWiseSheetInput).GetAttribute("value");
        }

        public void ClickOnUploadFileButton()
        {
            Wait.UntilElementClickable(UploadFileButton).ClickOn();
        }
    }
}
