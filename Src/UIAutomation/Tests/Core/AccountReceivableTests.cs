using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.Core;
using UIAutomation.PageObjects.Core.Common;
using UIAutomation.PageObjects.Microsoft;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.Core
{
    [TestClass]
    [TestCategory("Core"), TestCategory("Account Receivable")]
    public class AccountReceivableTests : BaseTest
    {      

        [TestMethod]
        public void VerifyAccountReceivablePageOpenedSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var accountReceivable = new AccountReceivablePo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAccountReceivable.Email} ,  password : {LoginCredentialsForAccountReceivable.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAccountReceivable);

            Log.Info("Step 3: Click on 'Accounts Receivable' from left menu & verify 'Accounts Receivable' is opened");
            leftNav.ClickOnAccountReceivableMenu();
            accountReceivable.SwitchToIFrame();
            var actualHeaderText = accountReceivable.GetAccountReceivableHeaderText();
            Assert.AreEqual("Accounts Receivable", actualHeaderText, "Header text is not matched");

        }

        [TestMethod]
        public void VerifyUserCanEnterVendorFeeSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var accountReceivable = new AccountReceivablePo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAccountReceivable.Email} ,  password : {LoginCredentialsForAccountReceivable.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAccountReceivable);

            Log.Info("Step 3: Click on 'Accounts Receivable' from left menu, enter vendor fee and verify added successfully");
            leftNav.ClickOnAccountReceivableMenu();
            accountReceivable.SwitchToIFrame();
            const string expectedVendorFee = "5";
            accountReceivable.EnterVendorFee(expectedVendorFee);
            var actualVendorFee = accountReceivable.GetVendorFeeAttribute();
            Assert.AreEqual(expectedVendorFee, actualVendorFee, "Vendor fee is not matched");
        }

        [TestMethod]
        public void VerifyUserCanEnterEscrowProcessingFeeSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var accountReceivable = new AccountReceivablePo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAccountReceivable.Email} ,  password : {LoginCredentialsForAccountReceivable.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAccountReceivable);

            Log.Info("Step 3: Click on 'Accounts Receivable' from left menu, enter Escrow Processing Fee and verify it added successfully");
            leftNav.ClickOnAccountReceivableMenu();
            accountReceivable.SwitchToIFrame();
            const string expectedEscrowProcessingFee = "100";
            accountReceivable.EnterEscrowProcessingFee(expectedEscrowProcessingFee);
            var actualEscrowProcessingFee = accountReceivable.GetEscrowProcessingAttribute();
            Assert.AreEqual(expectedEscrowProcessingFee, actualEscrowProcessingFee, "EscrowProcessingFee is not matched");
        }

        [TestMethod]
        public void VerifyChooseGpSheetWorksSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var accountReceivable = new AccountReceivablePo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAccountReceivable.Email} ,  password : {LoginCredentialsForAccountReceivable.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAccountReceivable);

            Log.Info("Step 3: Click on 'Accounts Receivable' from left menu");
            leftNav.ClickOnAccountReceivableMenu();
            accountReceivable.SwitchToIFrame();

            Log.Info($"Step 4: Click On 'Choose File' button, select file and Verify selected file attached successfully");
            var path = new FileUtil().GetBasePath() + "/TestData/Core/GreatPlains.XLSX";
            accountReceivable.ChooseGpFile(path);
            var actualFileName = accountReceivable.GetSelectedGpFileAttribute();
            Assert.IsTrue(actualFileName.Contains("GreatPlains.XLSX"), "File is not matched");
        }

        [TestMethod]
        public void VerifyChooseShiftWiseSheetWorksSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var accountReceivable = new AccountReceivablePo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAccountReceivable.Email} ,  password : {LoginCredentialsForAccountReceivable.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAccountReceivable);

            Log.Info("Step 3: Click on 'Accounts Receivable' from left menu");
            leftNav.ClickOnAccountReceivableMenu();
            accountReceivable.SwitchToIFrame();

            Log.Info($"Step 4: Click On 'Choose File' button, select file and Verify selected file attached successfully");
            var path = new FileUtil().GetBasePath() + "/TestData/Core/Shiftwise.XLSM";
            accountReceivable.ChooseShiftWiseFile(path);
            var actualFileName = accountReceivable.GetSelectedShiftWiseFileAttribute();
            Assert.IsTrue(actualFileName.Contains("Shiftwise.XLSM"), "File is not Matched");
        }

        [TestMethod]
        public void VerifyUploadFileButtonWorksSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var accountReceivable = new AccountReceivablePo(Driver);
            var fileUtil = new FileUtil();
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAccountReceivable.Email} ,  password : {LoginCredentialsForAccountReceivable.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAccountReceivable);

            Log.Info("Step 3: Click on 'Accounts Receivable' from left menu, enter vendor fee and shift wise fee");
            leftNav.ClickOnAccountReceivableMenu();
            accountReceivable.SwitchToIFrame();
            const string expectedVendorFee = "5";
            const string expectedEscrowProcessingFee = "100";
            accountReceivable.EnterVendorFee(expectedVendorFee);
            accountReceivable.EnterEscrowProcessingFee(expectedEscrowProcessingFee);

            Log.Info($"Step 4: Click On 'Choose File' button and select 'GP' file");
            var gpFilepath = new FileUtil().GetBasePath() + "/TestData/Core/GreatPlains.XLSX";
            accountReceivable.ChooseGpFile(gpFilepath);          

            Log.Info($"Step 5: Click On 'Choose File' button and select 'Shift wise' file");
            var shiftWiseFilepath = new FileUtil().GetBasePath() + "/TestData/Core/Shiftwise.XLSM";
            accountReceivable.ChooseShiftWiseFile(shiftWiseFilepath);

            Log.Info($"Step 5: Delete existing file from folder, click on 'Upload File' button and verify calculated file is downloaded successfully");           
            var downloadPath = fileUtil.GetDownloadPath();
            const string filename = "Formatted";
            fileUtil.DeleteFileInFolder(downloadPath, filename, ".XLSM");
            accountReceivable.ClickOnUploadFileButton();
            Assert.IsTrue(fileUtil.DoesFileExistInFolder(downloadPath, filename, ".XLSM", 30), "File not found!");           
        }
    }
}
