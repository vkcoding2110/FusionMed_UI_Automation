using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Traveler.Profile.MyDocuments;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.MyDocuments
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class MyDocumentsTests1 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByType("MyDocumentsTest");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteAllDocumentsDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatAddDocumentsWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var fmpHeader = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var myDocumentsDetails = new MyDocumentsDetailsPo(Driver);
            var addDocumentsPopUp = new AddDocumentPo(Driver);


            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'My Document' tab, click on 'Add Document' button & verify popup gets open");
            myDocumentsDetails.ClickOnMyDocumentsTabButton();
            Assert.IsTrue(myDocumentsDetails.IsAddDocumentsButtonDisplayed(), "The 'Add Document' button is not displayed");
            const string expectedAddDocumentsPopUpTitleText = "Add Documents";
            var actualAddDocumentsPopUpTitleText = addDocumentsPopUp.GetAddDocumentsPopUpTitleText();
            Assert.AreEqual(expectedAddDocumentsPopUpTitleText, actualAddDocumentsPopUpTitleText, "The 'Add Document' popup is not opened");

            Log.Info("Step 6: Click on 'upload documents from your device' link, Select document, click on 'Upload Document' button & verify document gets added");
            var uploadDocument = DocumentsDataFactory.UploadDocumentFile();
            const int droppedDocumentRowNumber = 1;
            addDocumentsPopUp.UploadDocumentFromDevice(uploadDocument.First(), droppedDocumentRowNumber);
            var droppedFilesFromSelectDocumentPopUp = addDocumentsPopUp.GetDroppedFilesFromSelectDocumentsPopUp(droppedDocumentRowNumber);
            Assert.AreEqual(uploadDocument.First().FileName.ToLowerInvariant(), droppedFilesFromSelectDocumentPopUp.First().FileName.ToLowerInvariant(), "The 'File Name' doesn't match");
            Assert.AreEqual(uploadDocument.First().DocumentType.ToLowerInvariant(), droppedFilesFromSelectDocumentPopUp.First().DocumentType.ToLowerInvariant(), "The  document 'Type' doesn't match");
            addDocumentsPopUp.ClickOnUploadDocumentsButton();

            Log.Info("Step 7: Verify that document uploaded on document profile detail page successfully");
            const int documentRowNumber = 1;
            var actualUploadedDocumentDetails = myDocumentsDetails.GetDocumentDetailsFromDetailPage(documentRowNumber);
            Assert.AreEqual(uploadDocument.First().FileName.ToLowerInvariant(), actualUploadedDocumentDetails.First().FileName.ToLowerInvariant(), "The 'Document Name' is not matched");
            Assert.AreEqual(uploadDocument.First().DocumentType.ToLowerInvariant(), actualUploadedDocumentDetails.First().DocumentType.ToLowerInvariant(), "The 'Document Type' is not matched");
            Assert.AreEqual(uploadDocument.First().DocumentUploadedDate.ToString("MM/dd/yyyy"), actualUploadedDocumentDetails.First().DocumentUploadedDate.ToString("MM/dd/yyyy"), $"The document uploaded 'Date' is not matched Expected: {uploadDocument.First().DocumentUploadedDate} & Actual: {actualUploadedDocumentDetails.First().DocumentUploadedDate}");
            Assert.AreEqual(uploadDocument.First().DocumentTypeCode.ToLowerInvariant(), actualUploadedDocumentDetails.First().DocumentTypeCode.ToLowerInvariant(), "The document uploaded 'Date' is not matched");

            //Clean up
            try
            {
                myDocumentsDetails.ClickOnDocumentMenuButton();
                myDocumentsDetails.ClickOnDeleteDocumentButton();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatEditDocumentWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var fmpHeader = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var myDocumentsDetails = new MyDocumentsDetailsPo(Driver);
            var addDocumentsPopUp = new AddDocumentPo(Driver);
            var editDocumentsPopUp = new EditDocumentPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'My Document' tab & click on 'Add Document' button");
            myDocumentsDetails.ClickOnMyDocumentsTabButton();

            Log.Info("Step 6: Click on 'upload documents from your device' link, Select document, click on 'Upload Document' button");
            var uploadDocument = DocumentsDataFactory.UploadDocumentFile();
            const int row = 1;
            addDocumentsPopUp.UploadDocumentFromDevice(uploadDocument.First(), row);
            addDocumentsPopUp.ClickOnUploadDocumentsButton();

            Log.Info("Step 7: Click on 'Document Menu' button & click on 'Edit Document' button & verify 'Edit Document' popup gets open");
            myDocumentsDetails.ClickOnDocumentMenuButton();
            myDocumentsDetails.ClickOnEditDocumentButton();
            const string expectedEditDocumentsPopUpHeaderText = "Edit Document";
            Assert.AreEqual(expectedEditDocumentsPopUpHeaderText, editDocumentsPopUp.GetEditDocumentHeaderText(), "The 'Edit Document' popup is not opened");

            Log.Info("Step 8: Edit the document details, click on 'Save changes' button & verify details gets updated successfully");
            var editDocument = DocumentsDataFactory.EditDocumentFile();
            editDocumentsPopUp.EditDocumentDetails(editDocument);
            const int documentRowNumber = 1;
            var actualEditedDocumentDetails = myDocumentsDetails.GetDocumentDetailsFromDetailPage(documentRowNumber);
            Assert.AreEqual(editDocument.FileName.ToLowerInvariant() + editDocument.DocumentTypeCode, actualEditedDocumentDetails.First().FileName.ToLowerInvariant(), "The 'Document Name' is not matched");
            Assert.AreEqual(editDocument.DocumentType, actualEditedDocumentDetails.First().DocumentType, "The 'Document Type' is not matched");
            Assert.AreEqual(editDocument.DocumentUploadedDate.ToString("MM/dd/yyyy"), actualEditedDocumentDetails.First().DocumentUploadedDate.ToString("MM/dd/yyyy"), $"The document uploaded 'Date' is not matched Expected: {editDocument.DocumentUploadedDate} & Actual: {actualEditedDocumentDetails.First().DocumentUploadedDate}");
            Assert.AreEqual(editDocument.ExpirationDate.ToString("MM/yyyy"), actualEditedDocumentDetails.First().ExpirationDate.ToString("MM/yyyy"), "The 'Expiration Date' is not matched");

            //Clean up
            try
            {
                myDocumentsDetails.ClickOnDocumentMenuButton();
                myDocumentsDetails.ClickOnDeleteDocumentButton();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatDeleteDocumentButtonWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var fmpHeader = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var myDocumentsDetails = new MyDocumentsDetailsPo(Driver);
            var addDocumentsPopUp = new AddDocumentPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'My Document' tab & click on 'Add Document' button");
            myDocumentsDetails.ClickOnMyDocumentsTabButton();

            Log.Info("Step 6: Click on 'upload documents from your device' link, Select document, click on 'Upload Document' button");
            var uploadDocument = DocumentsDataFactory.UploadDocumentFile();
            const int index = 1;
            addDocumentsPopUp.UploadDocumentFromDevice(uploadDocument.First(), index);
            addDocumentsPopUp.ClickOnUploadDocumentsButton();
            const int documentRowNumber = 1;
            Assert.IsTrue(myDocumentsDetails.IsDocumentHeaderTextDisplayed(documentRowNumber), "The document details are not displayed");

            Log.Info("Step 7: Click on 'Document Menu' button & click on 'Delete' button & verify document details deleted successfully");
            myDocumentsDetails.ClickOnDocumentMenuButton();
            myDocumentsDetails.ClickOnDeleteDocumentButton();
            Assert.IsFalse(myDocumentsDetails.IsDocumentHeaderTextDisplayed(documentRowNumber), "The document details are still displayed");
        }

        [TestMethod]
        [TestCategory("Smoke"),TestCategory("MobileReady"),TestCategory("IosNotReady")]
        public void VerifyThatDownloadDocumentButtonWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var fmpHeader = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var myDocumentsDetails = new MyDocumentsDetailsPo(Driver);
            var addDocumentsPopUp = new AddDocumentPo(Driver);
            var fileUtil = new FileUtil();

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'My Document' tab & click on 'Add Document' button");
            myDocumentsDetails.ClickOnMyDocumentsTabButton();

            Log.Info("Step 6: Click on 'upload documents from your device' link, Select document, click on 'Upload Document' button");
            var uploadDocument = DocumentsDataFactory.UploadDocumentFile();
            const int index = 1;
            addDocumentsPopUp.UploadDocumentFromDevice(uploadDocument.First(), index);
            addDocumentsPopUp.ClickOnUploadDocumentsButton();

            Log.Info("Step 7: Delete existing file from folder, click on 'Download' button and verify 'Document' file is downloaded successfully");
            var downloadPath = fileUtil.GetDownloadPath();
            const string filename = "Certification";
            if (PlatformName != PlatformName.Web)
            {
                new MobileFileSelectionPo(Driver).DeleteExistingFileFromEmulator(filename.ToLowerInvariant() + ".pdf");
            }
            else
            {
                fileUtil.DeleteFileInFolder(downloadPath, filename.ToLowerInvariant(), ".pdf");
            }
            myDocumentsDetails.ClickOnDocumentMenuButton();
            myDocumentsDetails.ClickOnDownloadButton();
            Assert.IsTrue(
                PlatformName != PlatformName.Web
                    ? new MobileFileSelectionPo(Driver).IsFilePresentOnDevice(filename)
                    : fileUtil.DoesFileExistInFolder(downloadPath, filename.ToLowerInvariant(), ".pdf", 15),
                $"File - {filename} not found!");
            //Clean up
            try
            {
                myDocumentsDetails.ClickOnDocumentMenuButton();
                myDocumentsDetails.ClickOnDeleteDocumentButton();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("MobileReady"), TestCategory("IosNotReady")]
        public void VerifyThatSelectTypeOfDocumentPopUpCloseIconCancelButtonAndBackIconWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var fmpHeader = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var myDocumentsDetails = new MyDocumentsDetailsPo(Driver);
            var addDocumentsPopUp = new AddDocumentPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'My Document' tab & click on 'Add Document' button");
            myDocumentsDetails.ClickOnMyDocumentsTabButton();

            Log.Info("Step 6: Click on 'upload documents from your device' link, Select document, click on 'Upload Document' button");
            var uploadDocument = DocumentsDataFactory.UploadDocumentFile();
            addDocumentsPopUp.AddDocumentFromDevice(uploadDocument.First());

            Log.Info("Step 7: Click on 'Close' icon & verify popup gets close");
            addDocumentsPopUp.ClickOnSelectTypeOfPopUpCloseIcon();
            Assert.IsFalse(addDocumentsPopUp.IsSelectTypeOfDocumentPopUpOpened(), "Select type of document Popup is still opened");

            Log.Info("Step 8: Click on 'upload documents from your device' link, Select document, click on 'Upload Document' button");
            myDocumentsDetails.ClickOnAddDocumentsButton();
            var addDocument = DocumentsDataFactory.UploadDocumentFile();
            addDocumentsPopUp.AddDocumentFromDevice(addDocument.First());

            Log.Info("Step 9: Click on 'Cancel' button & verify popup gets close");
            addDocumentsPopUp.ClickOnSelectTypeOfPopUpCancelButton();
            Assert.IsFalse(addDocumentsPopUp.IsSelectTypeOfDocumentPopUpOpened(), "Select type of document Popup is still opened");
        }
    }
}
