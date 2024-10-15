using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account.NativeApp;
using UIAutomation.PageObjects.FMP.Home.NativeApp;
using UIAutomation.PageObjects.FMP.NativeApp.More;
using UIAutomation.PageObjects.FMP.Traveler.Profile.MyDocuments.NativeApp;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.SetUpTearDown.FMP.NativeApp;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.MyDocuments.NativeApp
{

    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("NativeAppAndroid")]
    public class MyDocumentsTests : FmpBaseTest
    {

        private static readonly Login UserLogin = GetLoginUsersByType("MyDocumentsTest");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteAllDocumentDetails(UserLogin);
        }

        [TestMethod]
        public void VerifyThatAddDocumentsWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var homepagePo = new HomePagePo(Driver);
            var documentPagePo = new AddDocumentPo(Driver);
            var documentDetailPo = new DocumentDetailPo(Driver);
            var moreMenu = new MoreMenuPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homepagePo.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilAppLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Profile icon and Click on 'Add document', upload the document");
            homepagePo.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            var uploadDocument = DocumentsDataFactory.UploadDocumentFile();
            documentPagePo.UploadDocumentFromDevice(uploadDocument.First());
            documentPagePo.SelectDocumentType(uploadDocument.First().DocumentType);
            documentPagePo.ClickOnUploadDocumentButton();
            homepagePo.WaitUntilAppLoadingIndicatorInvisible();

            Log.Info("Step 3: Verify that document uploaded on document profile detail page successfully");
            var actualDocumentType = documentDetailPo.GetDocumentType();
            var actualDocumentTitle = documentDetailPo.GetDocumentFileName();
            Assert.AreEqual(uploadDocument.First().DocumentType, actualDocumentType, "Document type is not matched");
            Assert.AreEqual(uploadDocument.First().FileName.ToLowerInvariant().Replace(".pdf", ""), actualDocumentTitle.ToLowerInvariant(), "Document title is not matched");

            try
            {
                documentDetailPo.DeleteAllDocumentsDetails();
            }
            catch
            {
                //Nothing
            }
        }
        [TestMethod]
        public void VerifyCloseAndCancelButtonWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var homepagePo = new HomePagePo(Driver);
            var documentPagePo = new AddDocumentPo(Driver);
            var moreMenu = new MoreMenuPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homepagePo.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilAppLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Profile icon, Click on 'Add document'");
            homepagePo.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            var uploadDocument = DocumentsDataFactory.UploadDocumentFile();
            documentPagePo.UploadDocumentFromDevice(uploadDocument.First());

            Log.Info("Step 3: Click on 'Close icon' and verify document pop-up is closed");
            documentPagePo.ClickOnCloseIcon();
            Assert.IsFalse(documentPagePo.IsAddDocumentPopUpDisplayed(), "'Add document' pop-up is open'");

            Log.Info("Step 4: Click on 'Cancel' button and verify document pop-up is closed");
            documentPagePo.UploadDocumentFromDevice(uploadDocument.First());
            documentPagePo.ClickOnCancelButton();
            Assert.IsFalse(documentPagePo.IsAddDocumentPopUpDisplayed(), "'Add document'  pop-up is open'");
        }

        [TestMethod]
        public void VerifyShareDocumentWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var homepagePo = new HomePagePo(Driver);
            var documentPagePo = new AddDocumentPo(Driver);
            var documentDetailPo = new DocumentDetailPo(Driver);
            var mobileFileSelectionPo = new MobileFileSelectionPo(Driver);
            var moreMenu = new MoreMenuPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homepagePo.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilAppLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Profile icon and Click on 'Add document', upload the document");
            homepagePo.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            var uploadDocument = DocumentsDataFactory.UploadDocumentFile();
            documentPagePo.UploadDocumentFromDevice(uploadDocument.First());
            const string fileUploadType = "Resume";
            documentPagePo.SelectDocumentType(fileUploadType);
            documentPagePo.ClickOnUploadDocumentButton();
            homepagePo.WaitUntilAppLoadingIndicatorInvisible();

            Log.Info("Step 3: Click on menu button ,Select 'Share' option and verify share pop up is displayed");
            documentDetailPo.ClickOnDocumentMenuButton();
            documentDetailPo.ClickOnShareDocumentButton();
            var fileName = mobileFileSelectionPo.GetSharedFileName();
            Assert.AreEqual(uploadDocument.First().FileName.ToLowerInvariant(), fileName, "Shared file name is not matched");
            Assert.IsTrue(mobileFileSelectionPo.IsSharePopUpDisplayed(), "Share pop up id not displayed");

            try
            {
                Driver.Back();
                documentDetailPo.DeleteAllDocumentsDetails();
            }
            catch
            {
                //Nothing
            }
        }

        [TestMethod]
        public void VerifyEditDocumentWorkSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var homepagePo = new HomePagePo(Driver);
            var documentPagePo = new AddDocumentPo(Driver);
            var documentDetailPo = new DocumentDetailPo(Driver);
            var editDocumentPo = new EditDocumentPo(Driver);
            var moreMenu = new MoreMenuPo(Driver);

            Log.Info("Step 1: Open App & Login into the app");
            homepagePo.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilAppLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Profile icon and Click on 'Add document', upload the document");
            homepagePo.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            var uploadDocument = DocumentsDataFactory.UploadDocumentFile();
            documentPagePo.UploadDocumentFromDevice(uploadDocument.First());
            documentPagePo.SelectDocumentType(uploadDocument.First().DocumentType);
            documentPagePo.ClickOnUploadDocumentButton();
            homepagePo.WaitUntilAppLoadingIndicatorInvisible();

            Log.Info("Step 3: Click on 'Menu' button, Select 'Edit document' and Verify 'Edit document' pop up is displayed");
            documentDetailPo.ClickOnDocumentMenuButton();
            Assert.IsTrue(editDocumentPo.IsEditDocumentPopUpDisplayed(), "'Edit document' pop up is not displayed");

            Log.Info("Step 4: Update document detail and verify document detail is updated successfully");
            var updatedDocument = DocumentsDataFactory.EditDocumentFile();
            editDocumentPo.EditDocumentDetails(updatedDocument);
            var actualDocumentType = documentDetailPo.GetDocumentType();
            var actualDocumentTitle = documentDetailPo.GetDocumentFileName();
            Assert.AreEqual(updatedDocument.DocumentType, actualDocumentType, "Document type is not matched");
            Assert.AreEqual(updatedDocument.FileName.ToLowerInvariant(), actualDocumentTitle.ToLowerInvariant(), "Document file name is not matched");
            try
            {
                documentDetailPo.DeleteAllDocumentsDetails();
            }
            catch
            {
                //Nothing
            }
        }
    }
}
